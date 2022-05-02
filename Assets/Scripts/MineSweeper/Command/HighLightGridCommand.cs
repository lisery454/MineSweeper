using QFramework;

namespace MineSweeper.Normal {
    public class HighLightGridCommand : AbstractCommand {
        private int Row { get; }
        private int Line { get; }

        public HighLightGridCommand(int row, int line) {
            Row = row;
            Line = line;
        }

        protected override void OnExecute() {
            this.GetSystem<MineSystem>().HighLightGrid(Row, Line);
        }
    }

    public class NotHighLightGridCommand : AbstractCommand {
        private int Row { get; }
        private int Line { get; }

        public NotHighLightGridCommand(int row, int line) {
            Row = row;
            Line = line;
        }

        protected override void OnExecute() {
            this.GetSystem<MineSystem>().NotHighLightGrid(Row, Line);
        }
    }
}