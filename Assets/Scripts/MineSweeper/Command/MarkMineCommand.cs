using QFramework;

namespace MineSweeper {
    public class MarkMineCommand : AbstractCommand {
        private int Row { get; }

        private int Line { get; }

        public MarkMineCommand(int row, int line) {
            Row = row;
            Line = line;
        }

        protected override void OnExecute() {
            this.GetSystem<MineSystem>().MarkMineCommand(Row, Line);
        }
    }
}