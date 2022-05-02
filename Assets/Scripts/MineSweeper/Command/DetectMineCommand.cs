using QFramework;

namespace MineSweeper.Normal {
    public class DetectMineCommand : AbstractCommand {
        private int Row { get; set; }
        private int Line { get; set; }

        public DetectMineCommand(int row, int line) {
            Row = row;
            Line = line;
        }

        protected override void OnExecute() {
            this.GetSystem<MineSystem>().DetectMine(Row, Line);
        }
    }
}