using System;
using System.Collections.Generic;
using QFramework;
using UnityEngine;
using Random = UnityEngine.Random;

namespace MineSweeper {
    public class MineSystem : AbstractSystem {
        private GridModel gridModel;
        private TimeSystem timeSystem;

        private bool IsFirstSweep;

        protected override void OnInit() {
            gridModel = this.GetModel<GridModel>();
            timeSystem = this.GetSystem<TimeSystem>();
            this.RegisterEvent<GameStartEvent>(OnGameStart);
        }

        private void OnGameStart(GameStartEvent e) {
            IsFirstSweep = true;
            InitGridSize();
            InitAroundGrids();
        }

        private void InitGridSize() {
            var rowNum = gridModel.RowNum.Value;
            var lineNum = gridModel.LineNum.Value;

            var BindGridLoc = new Tuple<int, int>[rowNum][];
            for (var index = 0; index < rowNum; index++) {
                BindGridLoc[index] = new Tuple<int, int>[lineNum];
            }

            var GridSize = new int[rowNum][];
            for (var index = 0; index < rowNum; index++) {
                GridSize[index] = new int[lineNum];
            }

            for (var r = 0; r < rowNum - 1; r++) {
                for (var l = 0; l < lineNum - 1; l++) {
                    var p = Random.Range(0f, 1f);
                    if (p < 0.05f) { //生成2x2
                        if (GridSize[r][l] != 2 && GridSize[r + 1][l] != 2 &&
                            GridSize[r][l + 1] != 2 && GridSize[r + 1][l + 1] != 2) {
                            var loc = new Tuple<int, int>(r, l);
                            GridSize[r][l] = 2;
                            GridSize[r + 1][l] = 2;
                            GridSize[r][l + 1] = 2;
                            GridSize[r + 1][l + 1] = 2;
                            BindGridLoc[r][l] = loc;
                            BindGridLoc[r + 1][l] = loc;
                            BindGridLoc[r][l + 1] = loc;
                            BindGridLoc[r + 1][l + 1] = loc;
                        }
                    }
                }
            }

            for (var r = 0; r < rowNum; r++) {
                for (var l = 0; l < lineNum; l++) {
                    if (GridSize[r][l] != 2) { //生成1x1
                        var loc = new Tuple<int, int>(r, l);
                        GridSize[r][l] = 1;
                        BindGridLoc[r][l] = loc;
                    }
                }
            }

            gridModel.GridSize.Value = GridSize;
            gridModel.BindGridLoc.Value = BindGridLoc;

            this.SendEvent<GridSizeInitOver>();
        }

        private void InitGridState(int firstR, int firstL) {
            var rowNum = gridModel.RowNum.Value;
            var lineNum = gridModel.LineNum.Value;
            var mineNum = gridModel.MineNum.Value;
            InitIsMine(rowNum, lineNum, mineNum, firstR, firstL);
            InitAroundMineNum(rowNum, lineNum);
        }

        private void InitAroundGrids() {
            var rowNum = gridModel.RowNum.Value;
            var lineNum = gridModel.LineNum.Value;
            gridModel.AroundGrids.Value = new List<Tuple<int, int>>[rowNum][];
            for (var index = 0; index < rowNum; index++) {
                gridModel.AroundGrids.Value[index] = new List<Tuple<int, int>>[lineNum];
            }

            //对每个格子
            for (var r = 0; r < rowNum; r++) {
                for (var l = 0; l < lineNum; l++) {
                    //周围格子的列表
                    var gridList = new List<Tuple<int, int>>();

                    if (gridModel.GridSize.Value[r][l] == 1) { //如果1x1
                        for (var dr = -1; dr <= 1; dr++) { //先找出周围的候选格子

                            for (var dl = -1; dl <= 1; dl++) {
                                if (dr == 0 && dl == 0) continue;

                                var nr = r + dr;
                                var nl = l + dl;
                                if (nr >= 0 && nl >= 0 && nr < rowNum && nl < lineNum) { //对每个周围符合条件的格子
                                    if (gridModel.GridSize.Value[nr][nl] == 1) { //如果1x1
                                        var tuple = gridList.Find(t => t.Item1 == nr && t.Item2 == nl);
                                        if (tuple == null)
                                            gridList.Add(new Tuple<int, int>(nr, nl));
                                    }
                                    else { //如果2x2
                                        var (nnr, nnl) = gridModel.BindGridLoc.Value[nr][nl];
                                        var tuple = gridList.Find(t => t.Item1 == nnr && t.Item2 == nnl);
                                        if (tuple == null)
                                            gridList.Add(new Tuple<int, int>(nnr, nnl));
                                    }
                                }
                            }
                        }
                    }
                    else if (gridModel.GridSize.Value[r][l] == 2) { //如果2x2
                        var (br, bl) = gridModel.BindGridLoc.Value[r][l];
                        if (br == r && bl == l) { //所代表的格子就是自己,要计算周围雷数
                            //先找出周围的候选格子
                            for (var dr = -1; dr <= 2; dr++) {
                                for (var dl = -1; dl <= 2; dl++) {
                                    if (dr == 0 && dl == 0 || dr == 1 && dl == 0 ||
                                        dr == 0 && dl == 1 || dr == 1 && dl == 1) continue; //2x2格子内,不用算

                                    var nr = r + dr;
                                    var nl = l + dl;
                                    if (nr >= 0 && nl >= 0 && nr < rowNum && nl < lineNum) { //对每个周围符合条件的格子
                                        if (gridModel.GridSize.Value[nr][nl] == 1) { //如果1x1
                                            var tuple = gridList.Find(t => t.Item1 == nr && t.Item2 == nl);
                                            if (tuple == null)
                                                gridList.Add(new Tuple<int, int>(nr, nl));
                                        }
                                        else { //如果2x2
                                            var (nnr, nnl) = gridModel.BindGridLoc.Value[nr][nl];
                                            var tuple = gridList.Find(t => t.Item1 == nnr && t.Item2 == nnl);
                                            if (tuple == null)
                                                gridList.Add(new Tuple<int, int>(nnr, nnl));
                                        }
                                    }
                                }
                            }
                        }
                    }

                    gridModel.AroundGrids.Value[r][l] = gridList;
                }
            }
        }

        private void InitIsMine(int rowNum, int lineNum, int mineNum, int firstR, int firstL) {
            gridModel.IsMine.Value = new bool[rowNum, lineNum];
            //用来随机获取mineNum个元素
            var randomMineSelecter = new List<int>();
            var size = lineNum * rowNum;
            for (var i = 0; i < size; i++) {
                randomMineSelecter.Add(i);
            }

            for (var i = 0; i < size; i++) {
                var randomNum = Random.Range(0, size);
                var temp = randomMineSelecter[i];
                randomMineSelecter[i] = randomMineSelecter[randomNum];
                randomMineSelecter[randomNum] = temp;
            }

            //设置值
            for (int i = 0, j = 0; i < mineNum; i++) {
                var index = randomMineSelecter[j];
                var r = index / lineNum;
                var l = index % lineNum;
                j++;

                var (br, bl) = gridModel.BindGridLoc.Value[r][l];

                while (gridModel.IsMine.Value[br, bl] || Distance(br, bl, firstR, firstL) <= 2) {
                    index = randomMineSelecter[j];
                    r = index / lineNum;
                    l = index % lineNum;
                    j++;
                    (br, bl) = gridModel.BindGridLoc.Value[r][l];
                }

                gridModel.IsMine.Value[br, bl] = true;
            }
        }

        private static int Distance(int r1, int l1, int r2, int l2) {
            return Mathf.Abs(r1 - r2) + Mathf.Abs(l1 - l2);
        }

        private void InitAroundMineNum(int rowNum, int lineNum) {
            gridModel.AroundMineNum.Value = new int[rowNum, lineNum];
            var gridModelIsMine = gridModel.IsMine.Value;
            //对每个格子
            for (var r = 0; r < rowNum; r++) {
                for (var l = 0; l < lineNum; l++) {
                    //周围格子的列表
                    var (br, bl) = gridModel.BindGridLoc.Value[r][l];
                    if (br == r && bl == l) {
                        var gridList = gridModel.AroundGrids.Value[br][bl];

                        //查找周围的雷
                        foreach (var (x, y) in gridList) {
                            if (gridModelIsMine[x, y]) {
                                gridModel.AroundMineNum.Value[br, bl]++;
                            }
                        }
                    }
                }
            }
        }

        public void SweepMine(int row, int line) {
            if (IsFirstSweep) {
                IsFirstSweep = false;
                InitGridState(row, line);
            }

            var gridsToBeShowed = new List<Tuple<int, int>>();
            Sweep(row, line, gridsToBeShowed);
            if (gridsToBeShowed.Count != 0)
                this.SendEvent(new ShowMineOrNumEvent {
                    gridsToBeShowed = gridsToBeShowed
                });
        }

        private void Sweep(int r, int l, List<Tuple<int, int>> gridsToBeShowed) {
            var (br, bl) = gridModel.BindGridLoc.Value[r][l];
            if (gridModel.IsMarked.Value[br, bl]) return;
            if (gridModel.IsShowed.Value[br, bl]) return;
            gridModel.IsShowed.Value[br, bl] = true;

            var aroundMineNum = gridModel.AroundMineNum.Value[br, bl];
            var isMine = gridModel.IsMine.Value[br, bl];

            gridsToBeShowed.Add(new Tuple<int, int>(br, bl));

            if (aroundMineNum == 0 && !isMine) {
                var gridList = gridModel.AroundGrids.Value[r][l];
                foreach (var (x, y) in gridList) {
                    Sweep(x, y, gridsToBeShowed);
                }
            }
        }

        public void CheckWin() {
            var isMine = gridModel.IsMine.Value;
            var isShowed = gridModel.IsShowed.Value;
            var lineNum = gridModel.LineNum.Value;
            var rowNum = gridModel.RowNum.Value;

            //判断有没有雷踩到了
            for (var r = 0; r < rowNum; r++) {
                for (var l = 0; l < lineNum; l++) {
                    var (br, bl) = gridModel.BindGridLoc.Value[r][l];
                    if (isMine[br, bl] && isShowed[br, bl])
                        this.SendEvent(new GameOverEvent(false, timeSystem.GetSecond()));
                }
            }


            //判断有没有赢,把所有雷都找到了
            for (var r = 0; r < rowNum; r++) {
                for (var l = 0; l < lineNum; l++) {
                    var (br, bl) = gridModel.BindGridLoc.Value[r][l];
                    if (!isMine[br, bl] && !isShowed[br, bl])
                        return;
                }
            }

            this.SendEvent(new GameOverEvent(true, timeSystem.GetSecond()));
        }

        public void MarkMineCommand(int row, int line) {
            if (!gridModel.IsShowed.Value[row, line]) {
                gridModel.IsMarked.Value[row, line] = !gridModel.IsMarked.Value[row, line];
                this.SendEvent(new MarkGridEvent(row, line, gridModel.IsMarked.Value[row, line]));
            }
        }

        public void DetectMine(int r, int l) {
            var isMine = gridModel.IsMine.Value;
            var isShowed = gridModel.IsShowed.Value;
            var aroundMineNum = gridModel.AroundMineNum.Value;
            var isMarked = gridModel.IsMarked.Value;

            if (isShowed[r, l] && !isMine[r, l]) {
                var aroundMarkedNum = 0;
                var gridList = gridModel.AroundGrids.Value;
                foreach (var (br, bl) in gridList[r][l]) {
                    if (isMarked[br, bl] && !isShowed[br, bl])
                        aroundMarkedNum++;
                }

                if (aroundMarkedNum == aroundMineNum[r, l]) {
                    foreach (var (br, bl) in gridList[r][l]) {
                        if (!isMarked[br, bl] && !isShowed[br, bl])
                            SweepMine(br, bl);
                    }
                }
            }
        }

        public void HighLightGrid(int r, int l) {
            if (!gridModel.IsShowed.Value[r, l])
                this.SendEvent(new HighLightGridEvent(r, l));
        }

        public void NotHighLightGrid(int r, int l) {
            this.SendEvent(new NotHighLightGridEvent(r, l));
        }

        public void DownGrid(int r, int l) {
            if (!gridModel.IsShowed.Value[r, l] && !gridModel.IsMarked.Value[r, l])
                this.SendEvent(new DownGridEvent(r, l));
        }

        public void UpGrid(int r, int l) {
            if (!gridModel.IsShowed.Value[r, l] && !gridModel.IsMarked.Value[r, l])
                this.SendEvent(new UpGridEvent(r, l));
        }

        public void DownAroundGrids(int r, int l) {
            var gridList = gridModel.AroundGrids.Value;

            foreach (var (br, bl) in gridList[r][l]) {
                DownGrid(br, bl);
            }
        }

        public void UpAroundGrids(int r, int l) {
            var gridList = gridModel.AroundGrids.Value;
            
            foreach (var (br, bl) in gridList[r][l]) {
                UpGrid(br, bl);
            }
        }
    }
}