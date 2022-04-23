using QFramework;

namespace MineSweeper {
    public class InitGridStateCommand : AbstractCommand {
        private MineSystem mineSystem;

        protected override void OnExecute() {
            mineSystem = this.GetSystem<MineSystem>();
            mineSystem.InitGridState();
        }
    }
}