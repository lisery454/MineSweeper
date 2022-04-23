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
        }

        private void InitGridState(int firstR, int firstL) {
            var rowNum = gridModel.RowNum.Value;
            var lineNum = gridModel.LineNum.Value;
            var mineNum = gridModel.MineNum.Value;
            InitIsMine(rowNum, lineNum, mineNum, firstR, firstL);
            InitAroundMineNum(rowNum, lineNum);
            InitIsSweeped(rowNum, lineNum);
            InitIsMarked(rowNum, lineNum);
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
            for (int i = 0, j = 0; i < mineNum; i++, j++) {
                int r, l;
                do {
                    var index = randomMineSelecter[j];
                    r = index / lineNum;
                    l = index % lineNum;
                    j++;
                } while (Distance(r, l, firstR, firstL) <= 2);

                gridModel.IsMine.Value[r, l] = true;
            }
        }

        private static int Distance(int r1, int l1, int r2, int l2) {
            return Mathf.Abs(r1 - r2) + Mathf.Abs(l1 - l2);
        }

        private void InitAroundMineNum(int rowNum, int lineNum) {
            gridModel.AroundMineNum.Value = new int[rowNum, lineNum];
            var gridModelIsMine = gridModel.IsMine;
            //对每个格子
            for (var r = 0; r < rowNum; r++) {
                for (var l = 0; l < lineNum; l++) {
                    //如果是雷
                    if (gridModelIsMine.Value[r, l]) {
                        //周围的格子全部加一雷数
                        for (var dr = -1; dr <= 1; dr++) {
                            for (var dl = -1; dl <= 1; dl++) {
                                if (r + dr >= 0 && r + dr < rowNum && l + dl >= 0 && l + dl < lineNum)
                                    gridModel.AroundMineNum.Value[r + dr, l + dl]++;
                            }
                        }

                        //自己本身不用加一
                        gridModel.AroundMineNum.Value[r, l]--;
                    }
                }
            }
        }

        private void InitIsSweeped(int rowNum, int lineNum) {
            gridModel.IsShowed.Value = new bool[rowNum, lineNum];
        }

        private void InitIsMarked(int rowNum, int lineNum) {
            gridModel.IsMarked.Value = new bool[rowNum, lineNum];
        }

        public void SweepMine(int row, int line) {
            if (IsFirstSweep) {
                IsFirstSweep = false;
                InitGridState(row, line);
            }

            var gridsToBeShowed = new List<Tuple<int, int>>();
            Sweep(row, line, gridModel.RowNum, gridModel.LineNum, gridsToBeShowed);
            if (gridsToBeShowed.Count != 0) this.SendEvent(new ShowMineOrNumEvent {gridsToBeShowed = gridsToBeShowed});
        }

        private void Sweep(int r, int l, int rowNum, int lineNum, List<Tuple<int, int>> gridsToBeShowed) {
            if (gridModel.IsMarked.Value[r, l]) return;
            if (gridModel.IsShowed.Value[r, l]) return;
            gridModel.IsShowed.Value[r, l] = true;

            var aroundMineNum = gridModel.AroundMineNum.Value[r, l];
            var isMine = gridModel.IsMine.Value[r, l];

            gridsToBeShowed.Add(new Tuple<int, int>(r, l));

            if (aroundMineNum == 0 && !isMine) {
                for (var dr = -1; dr <= 1; dr++) {
                    for (var dl = -1; dl <= 1; dl++) {
                        if (dr == 0 && dl == 0) continue;

                        if (r + dr >= 0 && r + dr < rowNum && l + dl >= 0 && l + dl < lineNum)
                            Sweep(r + dr, l + dl, rowNum, lineNum, gridsToBeShowed);
                    }
                }
            }
        }

        public void CheckWin() {
            var isMine = gridModel.IsMine.Value;
            var isShowed = gridModel.IsShowed.Value;
            var lineNum = gridModel.LineNum.Value;
            var rowNum = gridModel.RowNum.Value;

            //判断有没有雷踩到了
            for (var r = 0; r < rowNum; r++)
            for (var l = 0; l < lineNum; l++)
                if (isMine[r, l] && isShowed[r, l])
                    this.SendEvent(new GameOverEvent(false, timeSystem.GetSecond()));


            //判断有没有赢,把所有雷都找到了
            for (var r = 0; r < rowNum; r++)
            for (var l = 0; l < lineNum; l++)
                if (!isMine[r, l] && !isShowed[r, l])
                    return;

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
            var lineNum = gridModel.LineNum.Value;
            var rowNum = gridModel.RowNum.Value;

            if (isShowed[r, l] && !isMine[r, l]) {
                var aroundMarkedNum = 0;
                for (var dr = -1; dr <= 1; dr++) {
                    for (var dl = -1; dl <= 1; dl++) {
                        if (dr == 0 && dl == 0) continue;
                        var nr = r + dr;
                        var nl = l + dl;
                        if (nr >= 0 && nr < rowNum && nl >= 0 && nl < lineNum)
                            if (isMarked[nr, nl] && !isShowed[nr, nl])
                                aroundMarkedNum++;
                    }
                }

                if (aroundMarkedNum == aroundMineNum[r, l]) {
                    for (var dr = -1; dr <= 1; dr++) {
                        for (var dl = -1; dl <= 1; dl++) {
                            if (dr == 0 && dl == 0) continue;
                            var nr = r + dr;
                            var nl = l + dl;
                            if (nr >= 0 && nr < rowNum && nl >= 0 && nl < lineNum)
                                if (!isMarked[nr, nl] && !isShowed[nr, nl])
                                    SweepMine(nr, nl);
                        }
                    }
                }
            }
        }
    }
}