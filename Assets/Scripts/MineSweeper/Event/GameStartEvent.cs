namespace MineSweeper {
    public class GameStartEvent {
        public int LineNum, RowNum;
        public int MineNum;

        public GameStartEvent(int lineNum, int rowNum, int mineNum) {
            LineNum = lineNum;
            RowNum = rowNum;
            MineNum = mineNum;
        }
    }
}