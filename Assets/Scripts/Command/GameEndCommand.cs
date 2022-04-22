using QFramework;
using UnityEngine;

namespace MineSweeper {
    public class GameEndCommand : AbstractCommand {
        protected override void OnExecute() {
            Debug.Log("game over");
        }
    }
}