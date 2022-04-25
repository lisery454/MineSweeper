using QFramework;

namespace MineSweeper {
    public class DownGridCommand : AbstractCommand {
        private int Row { get; }
        private int Line { get; }

        public DownGridCommand(int row, int line) {
            Row = row;
            Line = line;
        }

        protected override void OnExecute() {
            this.GetSystem<MineSystem>().DownGrid(Row, Line);
        }
    }

    public class UpGridCommand : AbstractCommand {
        private int Row { get; }
        private int Line { get; }

        public UpGridCommand(int row, int line) {
            Row = row;
            Line = line;
        }

        protected override void OnExecute() {
            this.GetSystem<MineSystem>().UpGrid(Row, Line);
        }
    }
    
    
    public class DownAroundGridsCommand : AbstractCommand {
        private int Row { get; }
        private int Line { get; }

        public DownAroundGridsCommand(int row, int line) {
            Row = row;
            Line = line;
        }

        protected override void OnExecute() {
            this.GetSystem<MineSystem>().DownAroundGrids(Row, Line);
        }
    }
    
    public class UpAroundGridsCommand : AbstractCommand {
        private int Row { get; }
        private int Line { get; }

        public UpAroundGridsCommand(int row, int line) {
            Row = row;
            Line = line;
        }

        protected override void OnExecute() {
            this.GetSystem<MineSystem>().UpAroundGrids(Row, Line);
        }
    }
}