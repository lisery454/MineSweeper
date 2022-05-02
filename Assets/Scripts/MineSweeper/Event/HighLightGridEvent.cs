namespace MineSweeper {
    public class HighLightGridEvent {
        public int Row { get; }
        public int Line { get; }

        public HighLightGridEvent(int row, int line) {
            Row = row;
            Line = line;
        }
    }

    public class NotHighLightGridEvent {
        public int Row { get; }
        public int Line { get; }

        public NotHighLightGridEvent(int row, int line) {
            Row = row;
            Line = line;
        }
    }
}