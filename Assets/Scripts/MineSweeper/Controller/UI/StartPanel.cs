using DG.Tweening;
using MineSweeper.Theme;
using UnityEngine;
using UnityEngine.UI;

namespace MineSweeper.Normal {
    public class StartPanel : AbstractController {
        private Transform settingPanelTransform;

        private void Start() {
            //设置主题
            SetTheme();

            //设置侦听器
            settingPanelTransform = transform.parent.Find("SettingPanel");

            RatioAdjuster.Instance.OnHeightOrWidthChanged += SetLocationWhenScreenChange;

            transform.Find("StartBtn").GetComponent<Button>().onClick.AddListener(() => {
                //SceneManager.LoadScene("MainGame");
                SceneLoader.Instance.LoadScene("MainGame");
                AudioManager.Instance.PlayAudio("a1");
            });

            transform.Find("SettingBtn").GetComponent<Button>().onClick.AddListener(() => {
                settingPanelTransform.DOMove(new Vector3(Screen.width / 2f, Screen.height / 2f), 0.5f);
                transform.DOMove(new Vector3(-Screen.width / 2f, Screen.height / 2f), 0.5f);
                AudioManager.Instance.PlayAudio("a1");
            });

            transform.Find("QuitBtn").GetComponent<Button>().onClick.AddListener(() => {
                AudioManager.Instance.PlayAudio("a1");
                Application.Quit(0);
            });
        }

        public void SetTheme() {
            var theme = ThemeManager.Instance.GetTheme();

            GetComponent<Image>().color = theme.BGColor;
            transform.Find("Title").GetComponent<Text>().color = theme.TitleFontColor;


            transform.Find("StartBtn").Find("Text").GetComponent<Text>().color = theme.ButtonFontColor;
            transform.Find("StartBtn").GetComponent<Image>().sprite = theme.ButtonSprite;
            var spriteState = transform.Find("StartBtn").GetComponent<Button>().spriteState;
            spriteState.pressedSprite = theme.ButtonPressedSprite;
            transform.Find("StartBtn").GetComponent<Button>().spriteState = spriteState;

            transform.Find("SettingBtn").Find("Text").GetComponent<Text>().color = theme.ButtonFontColor;
            transform.Find("SettingBtn").GetComponent<Image>().sprite = theme.ButtonSprite;
            spriteState = transform.Find("SettingBtn").GetComponent<Button>().spriteState;
            spriteState.pressedSprite = theme.ButtonPressedSprite;
            transform.Find("SettingBtn").GetComponent<Button>().spriteState = spriteState;

            transform.Find("QuitBtn").Find("Text").GetComponent<Text>().color = theme.ButtonFontColor;
            transform.Find("QuitBtn").GetComponent<Image>().sprite = theme.ButtonSprite;
            spriteState = transform.Find("QuitBtn").GetComponent<Button>().spriteState;
            spriteState.pressedSprite = theme.ButtonPressedSprite;
            transform.Find("QuitBtn").GetComponent<Button>().spriteState = spriteState;
        }

        private void SetLocationWhenScreenChange() {
            transform.position = new Vector3(Screen.width / 2f, Screen.height / 2f);
        }

        private void OnDestroy() {
            transform.DOKill();
            if (RatioAdjuster.IsExist)
                RatioAdjuster.Instance.OnHeightOrWidthChanged -= SetLocationWhenScreenChange;
        }
    }
}