using QFramework;

namespace MineSweeper {
    public class GameStartCommand : AbstractCommand {
        protected override void OnExecute() {
            this.SendEvent(new GameStartEvent(LineNum, RowNum, MineNum));
        }

        private int MineNum { get; }

        private int RowNum { get; }

        private int LineNum { get; }

        public GameStartCommand(int mineNum, int rowNum, int lineNum) {
            MineNum = mineNum;
            RowNum = rowNum;
            LineNum = lineNum;
        }
    }
}