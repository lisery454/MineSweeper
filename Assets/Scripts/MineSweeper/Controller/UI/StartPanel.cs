using MineSweeper.Theme;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace MineSweeper {
    public class StartPanel : AbstractController {
        private Transform settingPanelTransform;

        private void Start() {
            //设置主题
            SetTheme();

            //设置侦听器
            settingPanelTransform = transform.parent.Find("SettingPanel");

            transform.Find("StartBtn").GetComponent<Button>().onClick.AddListener(() => {
                SceneManager.LoadScene("MainGame");
            });

            transform.Find("SettingBtn").GetComponent<Button>().onClick.AddListener(() => {
                settingPanelTransform.gameObject.SetActive(true);
            });

            transform.Find("QuitBtn").GetComponent<Button>().onClick.AddListener(() => { Application.Quit(0); });
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
    }
}