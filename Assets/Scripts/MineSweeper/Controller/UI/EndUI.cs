using MineSweeper.Theme;
using QFramework;
using UnityEngine;
using UnityEngine.UI;

namespace MineSweeper {
    public class EndUI : AbstractController {
        private static readonly int IsGameOver = Animator.StringToHash("IsGameOver");
        private Text TimeText;
        private TimeSystem TimeSystem;
        private const float timeRefreshInterval = 0.5f;
        private float t;
        private bool IsRefreshTime = true;

        private void Start() {
            //设置主题
            SetTheme();

            TimeText = transform.Find("TimeText").GetComponent<Text>();
            TimeSystem = this.GetSystem<TimeSystem>();

            this.RegisterEvent<GameOverEvent>(OnGameOver).UnRegisterWhenGameObjectDestroyed(gameObject);

            transform.Find("GameOverPanel/BackBtn").GetComponent<Button>().onClick.AddListener(() => {
                SceneLoader.Instance.LoadScene("StartUI");
                AudioManager.Instance.PlayAudio("a1");
            });

            this.RegisterEvent<GameOverEvent>(e => { IsRefreshTime = false; })
                .UnRegisterWhenGameObjectDestroyed(gameObject);
        }

        private void Update() {
            if (IsRefreshTime) {
                if (t > timeRefreshInterval) {
                    TimeText.text = $"Time: {TimeSystem.GetSecond()} s";
                    t %= timeRefreshInterval;
                }

                t += Time.deltaTime;
            }
        }

        private void OnGameOver(GameOverEvent e) {
            AudioManager.Instance.PlayAudio(e.IsWin ? "a6" : "a5");

            transform.Find("GameOverPanel/TitleText").GetComponent<Text>().text = e.IsWin ? "You Win!" : "You Lose!";

            transform.Find("GameOverPanel/TimeText").GetComponent<Text>().text = "use time: " + e.UseSeconds + "s";

            GetComponent<Animator>().SetBool(IsGameOver, true);
        }

        private void SetTheme() {
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
            transform.Find("TimeText").GetComponent<Text>().color = theme.TitleFontColor;
        }
    }
}