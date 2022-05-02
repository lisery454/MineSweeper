using QFramework;

namespace MineSweeper.Normal {
    public class MineSweeperGame : Architecture<MineSweeperGame> {
        protected override void Init() {
            RegisterModel(new GridModel());
            RegisterSystem(new MineSystem());
            RegisterSystem(new TimeSystem());
        }
    }
}