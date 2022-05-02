using QFramework;

namespace MineSweeper.Normal {
    public class CheckWinCommand : AbstractCommand{
        protected override void OnExecute() {
            this.GetSystem<MineSystem>().CheckWin();
        }
    }
}