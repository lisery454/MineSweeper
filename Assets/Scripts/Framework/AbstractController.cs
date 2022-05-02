using QFramework;
using UnityEngine;

namespace MineSweeper.Normal {
    public class AbstractController : MonoBehaviour, IController {
        public IArchitecture GetArchitecture() {
            return MineSweeperGame.Interface;
        }
    }
}