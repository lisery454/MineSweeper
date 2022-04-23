using QFramework;
using UnityEngine;

namespace MineSweeper {
    public class InputManager : AbstractController {
        private bool IsCheckInput;
        private bool IsLeftMouse;
        private bool IsRightMouse;

        private bool IsLeftMouseUp;
        private bool IsRightMouseUp;

        private void Start() {
            this.RegisterEvent<GameStartEvent>(e => { IsCheckInput = true; })
                .UnRegisterWhenGameObjectDestroyed(gameObject);
            this.RegisterEvent<GameOverEvent>(e => { IsCheckInput = false; })
                .UnRegisterWhenGameObjectDestroyed(gameObject);
        }

        private void Update() {
            if (!IsCheckInput) return;

            IsLeftMouse = Input.GetMouseButton(0);
            IsRightMouse = Input.GetMouseButton(1);
            IsLeftMouseUp = Input.GetMouseButtonUp(0);
            IsRightMouseUp = Input.GetMouseButtonUp(1);

            if (IsLeftMouseUp && !IsRightMouse) {
                var hitGrid = GetHitOnGrid();
                if (hitGrid != null) {
                    this.SendCommand(new SweepMineCommand(hitGrid.Row, hitGrid.Line));
                }
            }

            if (IsRightMouseUp && !IsLeftMouse) {
                var hitGrid = GetHitOnGrid();
                if (hitGrid != null) {
                    this.SendCommand(new MarkMineCommand(hitGrid.Row, hitGrid.Line));
                }
            }

            if (IsLeftMouseUp && IsRightMouseUp ||
                IsLeftMouseUp && IsRightMouse ||
                IsRightMouseUp && IsLeftMouse) {
                var hitGrid = GetHitOnGrid();
                if (hitGrid != null) {
                    this.SendCommand(new DetectMineCommand(hitGrid.Row, hitGrid.Line));
                }
            }
        }


        private MineGrid GetHitOnGrid() {
            Debug.Assert(Camera.main != null, "Camera.main != null");
            var hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
            if (hit && hit.collider.CompareTag("Grid")) {
                return hit.collider.GetComponent<MineGrid>();
            }

            return null;
        }
    }
}