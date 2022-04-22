using QFramework;
using UnityEngine;

namespace MineSweeper {
    public class AbstractController : MonoBehaviour, IController {
        public IArchitecture GetArchitecture() {
            return MineSweeperGame.Interface;
        }
    }
}