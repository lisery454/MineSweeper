using MineSweeper.Theme;
using QFramework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace MineSweeper {
    public class EndUI : AbstractController {
        private static readonly int IsGameOver = Animator.StringToHash("IsGameOver");

        private void Start() {
            //设置主题
            var theme = ThemeManager.Instance.GetTheme();

            transform.Find("GameOverPanel").Find("BG").GetComponent<Image>().sprite = theme.EndUIBGSprite;
            transform.Find("GameOverPanel").Find("TitleText").GetComponent<Text>().color = theme.EndUITitleColor;
            transform.Find("GameOverPanel").Find("TimeText").GetComponent<Text>().color = theme.EndUITitleColor;
            transform.Find("GameOverPanel").Find("BackBtn").GetComponent<Image>().sprite = theme.ButtonSprite;
            var spriteState = transform.Find("GameOverPanel").Find("BackBtn").GetComponent<Button>().spriteState;
            spriteState.pressedSprite = theme.ButtonPressedSprite;
            transform.Find("GameOverPanel").Find("BackBtn").GetComponent<Button>().spriteState = spriteState;
            transform.Find("GameOverPanel").Find("BackBtn").Find("Text").GetComponent<Text>().color =
                theme.ButtonFontColor;
            
            
            this.RegisterEvent<GameOverEvent>(OnGameOver).UnRegisterWhenGameObjectDestroyed(gameObject);

            transform.Find("GameOverPanel/BackBtn").GetComponent<Button>().onClick.AddListener(() => {
                SceneManager.LoadScene("StartUI");
            });
        }

        private void OnGameOver(GameOverEvent e) {
            transform.Find("GameOverPanel").gameObject.SetActive(true);

            transform.Find("GameOverPanel/TitleText").GetComponent<Text>().text = e.IsWin ? "You Win!" : "You Lose!";

            transform.Find("GameOverPanel/TimeText").GetComponent<Text>().text = "use time: " + e.UseSeconds + "s";

            GetComponent<Animator>().SetBool(IsGameOver, true);
        }
    }
}