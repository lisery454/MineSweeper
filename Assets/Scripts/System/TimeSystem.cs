using System;
using QFramework;

namespace MineSweeper {
    public class TimeSystem : AbstractSystem {
        protected override void OnInit() {
            this.RegisterEvent<GameStartEvent>(e => { startTime = DateTime.Now; });
        }

        private DateTime startTime = DateTime.Now;

        public int GetSecond() {
            return (int) (DateTime.Now - startTime).TotalSeconds;
        }
    }
}