using QFramework;

namespace MineSweeper {
    public class CheckWinCommand : AbstractCommand{
        protected override void OnExecute() {
            this.GetSystem<MineSystem>().CheckWin();
        }
    }
}