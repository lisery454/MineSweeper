using QFramework;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace MineSweeper {
    public class EndUI : AbstractController {
        private void Start() {
            this.RegisterEvent<GameOverEvent>(OnGameOver).UnRegisterWhenGameObjectDestroyed(gameObject);

            transform.Find("GameOverPanel/BackBtn").GetComponent<Button>().onClick.AddListener(() => {
                SceneManager.LoadScene("StartUI");
            });
        }

        private void OnGameOver(GameOverEvent e) {
            transform.Find("GameOverPanel").gameObject.SetActive(true);

            transform.Find("GameOverPanel/TitleText").GetComponent<Text>().text = e.IsWin ? "You Win!" : "You Lose!";

            transform.Find("GameOverPanel/TimeText").GetComponent<Text>().text = "use time: " + e.UseSeconds + "s";
        }
    }
}