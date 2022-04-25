using QFramework;
using UnityEngine;

namespace MineSweeper {
    public class ResolutionSolver : AbstractController {
        private GridModel gridModel;
        private float gridInterval;
        private float resolutionAdjustInterval;

        private void Start() {
            gridModel = this.GetModel<GridModel>();
            gridInterval = gridModel.GridInterval;
            resolutionAdjustInterval = gridModel.ResolutionAdjustInterval;
            
            AdjustResolution();
        }

        private void AdjustResolution() {
            var R = gridModel.RowNum.Value * gridInterval + 2 * resolutionAdjustInterval;
            var L = gridModel.LineNum.Value * gridInterval + 2 * resolutionAdjustInterval;

            var W = Screen.width;
            var H = Screen.height;

            if (R / L > (float) H / W) {
                Debug.Assert(Camera.main != null, "Camera.main != null");
                Camera.main.orthographicSize = R / 2;
            }
            else {
                Debug.Assert(Camera.main != null, "Camera.main != null");
                Camera.main.orthographicSize = L * H / W / 2;
            }
        }
    }
}