using System.Collections.Generic;
using QFramework;

namespace MineSweeper {
    public class SweepMineCommand : AbstractCommand {
        private int Row { get; }

        private int Line { get; }

        public SweepMineCommand(int row, int line) {
            Row = row;
            Line = line;
        }

        protected override void OnExecute() {
            this.GetSystem<MineSystem>().SweepMine(Row, Line);
        }
    }
}