using System;
using System.Collections.Generic;
using System.Linq;
using QFramework;
using UnityEngine;

namespace MineSweeper {
    public class LevelManager : AbstractController {
        [SerializeField] private MineGrid mineGridPrefab;

        [SerializeField] private float gridInterval;

        private List<List<MineGrid>> grids;

        private GridModel gridModel;

        private void Start() {
            gridModel = this.GetModel<GridModel>();
            this.RegisterEvent<GameStartEvent>(InitGrids).UnRegisterWhenGameObjectDestroyed(gameObject);
            this.RegisterEvent<ShowMineOrNumEvent>(ShowAssignedGrid).UnRegisterWhenGameObjectDestroyed(gameObject);

            //for test -----------------------------------------
            this.SendCommand(new GameStartCommand(
                gridModel.MineNum, gridModel.RowNum, gridModel.LineNum));
            //for test -----------------------------------------
        }

        private void InitGrids(GameStartEvent e) {
            var rowNum = e.RowNum;
            var lineNum = e.LineNum;
            //清空之前的格子
            if (grids != null && grids.Count != 0) {
                foreach (var grid in grids.SelectMany(gridRow => gridRow)) {
                    Destroy(grid.gameObject);
                }
            }

            grids = new List<List<MineGrid>>();

            var gridRoot = transform.Find("Grids");

            //生成新的格子
            for (var r = 0; r < rowNum; r++) {
                grids.Add(new List<MineGrid>());
                for (var l = 0; l < lineNum; l++) {
                    var grid = Instantiate(mineGridPrefab, new Vector3(l, r, 0) * gridInterval, Quaternion.identity);
                    grid.transform.SetParent(gridRoot, false);
                    grid.Line = l;
                    grid.Row = r;
                    grids[r].Add(grid);
                }
            }

            this.SendCommand<InitGridStateCommand>();
        }

        private void ShowAssignedGrid(ShowMineOrNumEvent e) {
            foreach (var (r, l) in e.gridsToBeShowed) {
                if (gridModel.IsMine.Value[r, l]) grids[r][l].ShowMine();
                else grids[r][l].ShowNum(gridModel.AroundMineNum.Value[r, l]);
            }
        }
    }
}