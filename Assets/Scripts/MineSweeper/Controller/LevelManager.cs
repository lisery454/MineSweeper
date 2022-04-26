using System;
using System.Collections.Generic;
using System.Linq;
using MineSweeper.Theme;
using QFramework;
using UnityEngine;

namespace MineSweeper {
    public class LevelManager : AbstractController {
        [SerializeField] private MineGrid mineGridPrefab;
        private List<List<MineGrid>> grids;

        private GridModel gridModel;
        private float gridInterval;

        private void Start() {
            //设置主题
            var theme = ThemeManager.Instance.GetTheme();
            transform.Find("BG").GetComponent<SpriteRenderer>().color = theme.BGColor;

            mineGridPrefab.transform.Find("FrontRoot/HighLight").GetComponent<SpriteRenderer>().sprite =
                theme.GridHighLightSprite;
            mineGridPrefab.transform.Find("FrontRoot/Mark").GetComponent<SpriteRenderer>().sprite = theme.MarkSprite;
            mineGridPrefab.transform.Find("Mine").GetComponent<SpriteRenderer>().sprite = theme.MineSprite;
            mineGridPrefab.transform.GetComponent<SpriteRenderer>().sprite = theme.GridSprite;
            mineGridPrefab.transform.Find("Back").GetComponent<SpriteRenderer>().sprite = theme.GridBackSprite;
            mineGridPrefab.transform.Find("Num/Mesh").GetComponent<TextMesh>().color = theme.GridNumColor;


            gridModel = this.GetModel<GridModel>();
            gridInterval = gridModel.GridInterval;
            this.RegisterEvent<GameStartEvent>(e => {
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
                        var grid = Instantiate(mineGridPrefab, new Vector3(l, r, 0) * gridInterval,
                            Quaternion.identity);
                        grid.transform.SetParent(gridRoot, false);
                        grid.Line = l;
                        grid.Row = r;
                        grids[r].Add(grid);
                    }
                }

                gridRoot.position = -new Vector3(lineNum * gridInterval / 2, rowNum * gridInterval / 2, 0);

                gridModel.IsShowed.Value = new bool[rowNum, lineNum];
                gridModel.IsMarked.Value = new bool[rowNum, lineNum];
            }).UnRegisterWhenGameObjectDestroyed(gameObject);
            this.RegisterEvent<ShowMineOrNumEvent>(e => {
                foreach (var (r, l) in e.gridsToBeShowed) {
                    if (gridModel.IsMine.Value[r, l]) grids[r][l].ShowMine();
                    else grids[r][l].ShowNum(gridModel.AroundMineNum.Value[r, l]);
                }

                this.SendCommand<CheckWinCommand>();
            }).UnRegisterWhenGameObjectDestroyed(gameObject);
            this.RegisterEvent<MarkGridEvent>(e => { grids[e.Row][e.Line].ShowMark(e.IsMark); })
                .UnRegisterWhenGameObjectDestroyed(gameObject);
            this.RegisterEvent<HighLightGridEvent>(e => { grids[e.Row][e.Line].ShowHighLight(); })
                .UnRegisterWhenGameObjectDestroyed(gameObject);
            this.RegisterEvent<NotHighLightGridEvent>(e => { grids[e.Row][e.Line].NotShowHighLight(); })
                .UnRegisterWhenGameObjectDestroyed(gameObject);
            this.RegisterEvent<DownGridEvent>(e => { grids[e.Row][e.Line].ShowBack(); })
                .UnRegisterWhenGameObjectDestroyed(gameObject);
            this.RegisterEvent<UpGridEvent>(e => { grids[e.Row][e.Line].NotShowBack(); })
                .UnRegisterWhenGameObjectDestroyed(gameObject);

            //for test -----------------------------------------
            this.SendCommand(new GameStartCommand(gridModel.MineNum, gridModel.RowNum, gridModel.LineNum));
            //for test -----------------------------------------
        }
    }
}