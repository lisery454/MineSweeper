namespace MineSweeper.Normal {
    public class MarkGridEvent {
        public int Row { get; set; }
        public int Line { get; set; }

        public bool IsMark { get; set; }

        public MarkGridEvent(int row, int line, bool isMark) {
            Row = row;
            Line = line;
            IsMark = isMark;
        }
    }
}