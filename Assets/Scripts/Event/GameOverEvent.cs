namespace MineSweeper {
    public class GameOverEvent {
        public bool IsWin { get; set; }
        public int UseSeconds { get; set; }

        public GameOverEvent(bool isWin, int useSeconds) {
            IsWin = isWin;
            UseSeconds = useSeconds;
        }
    }
}