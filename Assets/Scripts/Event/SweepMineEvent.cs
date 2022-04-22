namespace MineSweeper {
    public class SweepMineEvent {
        public int Row { get; }

        public int Line { get; }

        public SweepMineEvent(int row, int line) {
            Row = row;
            Line = line;
        }
    }
}