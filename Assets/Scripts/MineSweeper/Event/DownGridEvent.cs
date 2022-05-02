namespace MineSweeper {
    public class DownGridEvent {
        public int Row { get; }
        public int Line { get; }

        public DownGridEvent(int row, int line) {
            Row = row;
            Line = line;
        }
    }

    public class UpGridEvent {
        public int Row { get; }
        public int Line { get; }

        public UpGridEvent(int row, int line) {
            Row = row;
            Line = line;
        }
    }
}