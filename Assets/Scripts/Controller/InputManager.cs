using QFramework;
using UnityEngine;
using Debug = System.Diagnostics.Debug;

namespace MineSweeper {
    public class InputManager : AbstractController {
        private bool IsCheckInput;
        private bool IsLeftMouseDown;
        private bool IsRightMouseDown;

        private void Start() {
            this.RegisterEvent<GameStartEvent>(e => { IsCheckInput = true; })
                .UnRegisterWhenGameObjectDestroyed(gameObject);
            this.RegisterEvent<GameOverEvent>(e => { IsCheckInput = false; })
                .UnRegisterWhenGameObjectDestroyed(gameObject);
        }

        private void Update() {
            if (!IsCheckInput) return;

            IsLeftMouseDown = Input.GetMouseButton(0);
            IsRightMouseDown = Input.GetMouseButton(1);

            if (Input.GetMouseButtonUp(0) && !IsRightMouseDown) {
                var hitGrid = GetHitOnGrid();
                if (hitGrid != null) {
                    this.SendCommand(new SweepMineCommand(hitGrid.Row, hitGrid.Line));
                }
            }

            if (Input.GetMouseButtonUp(1) && !IsLeftMouseDown) {
                var hitGrid = GetHitOnGrid();
                if (hitGrid != null) {
                    this.SendCommand(new MarkMineCommand(hitGrid.Row, hitGrid.Line));
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