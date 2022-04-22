using QFramework;
using UnityEngine;

namespace MineSweeper {
    public class InputManager : AbstractController {
        private void Update() {
            if (Input.GetMouseButtonUp(0)) {
                var hitGrid = GetHitOnGrid();
                if (hitGrid != null) {
                    this.SendCommand(new SweepMineCommand(hitGrid.Row, hitGrid.Line));
                }
            }
        }


        private MineGrid GetHitOnGrid() {
            var hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
            if (hit && hit.collider.CompareTag("Grid")) {
                return hit.collider.GetComponent<MineGrid>();
            }

            return null;
        }
    }
}