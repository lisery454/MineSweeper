using System.Collections.Generic;
using System.Linq;
using QFramework;
using UnityEngine;

namespace MineSweeper {
    public class InputManager : AbstractController {
        private bool IsCheckInput;

        MineGrid hitGrid;

        private IInputState currentInputState = new NormalInputState();

        private List<MineGrid> HighLightedGrids;
        private List<MineGrid> PressedDownGrids;
        private List<MineGrid> AroundHighLightedGrids;


        private void Start() {
            HighLightedGrids = new List<MineGrid>();
            PressedDownGrids = new List<MineGrid>();
            AroundHighLightedGrids = new List<MineGrid>();
            this.RegisterEvent<GameStartEvent>(_ => { IsCheckInput = true; })
                .UnRegisterWhenGameObjectDestroyed(gameObject);
            this.RegisterEvent<GameOverEvent>(_ => { IsCheckInput = false; })
                .UnRegisterWhenGameObjectDestroyed(gameObject);
        }

        private void Update() {
            if (!IsCheckInput) return;

            if (Input.GetMouseButtonDown(0)) {
                switch (currentInputState) {
                    case NormalInputState: {
                        ClearHighLightGrids();
                        if (GetGridOnMouse() != null) MakeGridPressDown(GetGridOnMouse());

                        break;
                    }
                    case LeftMouseInputState: {
                        ClearPressDownGrids();
                        break;
                    }
                }

                currentInputState = currentInputState.OnLeftMouseDown();

                AudioManager.Instance.PlayAudio("a7");
            }

            if (Input.GetMouseButtonDown(1)) {
                switch (currentInputState) {
                    case NormalInputState: {
                        hitGrid = GetGridOnMouse();
                        if (hitGrid != null) {
                            this.SendCommand(new MarkMineCommand(hitGrid.Row, hitGrid.Line));
                        }

                        break;
                    }
                    case LeftMouseInputState: {
                        ClearPressDownGrids();
                        break;
                    }
                }

                currentInputState = currentInputState.OnRightMouseDown();
            }

            if (Input.GetMouseButtonUp(0)) {
                switch (currentInputState) {
                    case LeftMouseInputState: {
                        ClearPressDownGrids();

                        hitGrid = GetGridOnMouse();
                        if (hitGrid != null) this.SendCommand(new SweepMineCommand(hitGrid.Row, hitGrid.Line));
                        break;
                    }
                    case LeftRightMouseInputState: {
                        ClearAroundDownGrids();
                        hitGrid = GetGridOnMouse();
                        if (hitGrid != null) this.SendCommand(new DetectMineCommand(hitGrid.Row, hitGrid.Line));
                        break;
                    }
                    case NormalInputState: {
                        ClearHighLightGrids();
                        break;
                    }
                }

                currentInputState = currentInputState.OnLeftMouseUp();
            }

            if (Input.GetMouseButtonUp(1)) {
                switch (currentInputState) {
                    case LeftRightMouseInputState: {
                        ClearAroundDownGrids();
                        hitGrid = GetGridOnMouse();
                        if (hitGrid != null) this.SendCommand(new DetectMineCommand(hitGrid.Row, hitGrid.Line));
                        break;
                    }
                    case NormalInputState: {
                        ClearHighLightGrids();
                        break;
                    }
                    case LeftMouseInputState: {
                        ClearPressDownGrids();
                        break;
                    }
                }

                currentInputState = currentInputState.OnRightMouseUp();
            }

            OnGridEnterMouse();
        }

        private void OnGridEnterMouse() {
            var mineGrid = GetGridOnMouse();
            if (mineGrid != null) {
                switch (currentInputState) {
                    case NormalInputState:
                        MakeGridHighLight(mineGrid);
                        break;
                    case LeftMouseInputState:
                        MakeGridPressDown(mineGrid);
                        break;
                    case RightMouseInputState:
                        MakeGridHighLight(mineGrid);
                        break;
                    case LeftRightMouseInputState:
                        MakeAroundGridsDown(mineGrid);
                        break;
                }
            }
        }

        private void MakeGridHighLight(MineGrid mineGrid) {
            foreach (var grid in HighLightedGrids.Where(grid => grid != mineGrid)) {
                this.SendCommand(new NotHighLightGridCommand(grid.Row, grid.Line));
            }

            HighLightedGrids.Clear();
            HighLightedGrids.Add(mineGrid);
            this.SendCommand(new HighLightGridCommand(mineGrid.Row, mineGrid.Line));
        }

        private void ClearHighLightGrids() {
            foreach (var grid in HighLightedGrids) {
                this.SendCommand(new NotHighLightGridCommand(grid.Row, grid.Line));
            }

            HighLightedGrids.Clear();
        }

        private void MakeAroundGridsDown(MineGrid mineGrid) {
            foreach (var grid in AroundHighLightedGrids.Where(grid => grid != mineGrid)) {
                this.SendCommand(new UpAroundGridsCommand(grid.Row, grid.Line));
            }

            AroundHighLightedGrids.Clear();
            AroundHighLightedGrids.Add(mineGrid);
            this.SendCommand(new DownAroundGridsCommand(mineGrid.Row, mineGrid.Line));
        }

        private void ClearAroundDownGrids() {
            foreach (var grid in AroundHighLightedGrids) {
                this.SendCommand(new UpAroundGridsCommand(grid.Row, grid.Line));
            }

            AroundHighLightedGrids.Clear();
        }

        private void MakeGridPressDown(MineGrid mineGrid) {
            foreach (var grid in PressedDownGrids.Where(grid => grid != mineGrid)) {
                this.SendCommand(new UpGridCommand(grid.Row, grid.Line));
            }

            PressedDownGrids.Clear();
            PressedDownGrids.Add(mineGrid);
            this.SendCommand(new DownGridCommand(mineGrid.Row, mineGrid.Line));
        }

        private void ClearPressDownGrids() {
            foreach (var grid in PressedDownGrids) {
                this.SendCommand(new UpGridCommand(grid.Row, grid.Line));
            }

            PressedDownGrids.Clear();
        }

        private MineGrid GetGridOnMouse() {
            System.Diagnostics.Debug.Assert(Camera.main != null, "Camera.main != null");
            var hit2D = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
            if (hit2D) {
                if (hit2D.collider.CompareTag("Grid")) {
                    return hit2D.collider.GetComponent<MineGrid>();
                }
            }

            return null;
        }
    }
}