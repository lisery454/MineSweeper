using MineSweeper.Theme;
using QFramework;
using UnityEngine.UI;


namespace MineSweeper {
    public class SettingPanel : AbstractController {
        private InputField RowInput;
        private InputField LineInput;
        private InputField MineInput;

        private GridModel gridModel;

        private void Start() {
            //设置主题
            SetTheme();

            gridModel = this.GetModel<GridModel>();
            RowInput = transform.Find("RowInput").GetComponent<InputField>();
            LineInput = transform.Find("LineInput").GetComponent<InputField>();
            MineInput = transform.Find("MineInput").GetComponent<InputField>();
            transform.Find("SetRLMBtn").GetComponent<Button>().onClick.AddListener(() => {
                if (TestIfInputOK(RowInput.text, LineInput.text, MineInput.text)) {
                    gridModel.MineNum.Value = int.Parse(MineInput.text);
                    gridModel.LineNum.Value = int.Parse(LineInput.text);
                    gridModel.RowNum.Value = int.Parse(RowInput.text);
                    transform.Find("Tip").GetComponent<Text>().text = "<!>Set Successfully<!>";
                }
            });

            transform.Find("BackBtn").GetComponent<Button>().onClick.AddListener(() => {
                gameObject.SetActive(false);
            });

            transform.Find("ThemeTitle/ThemeSeaBtn").GetComponent<Button>().onClick.AddListener(() => {
                ThemeManager.Instance.SetTheme("ThemeSea");
                SetTheme();
                transform.parent.Find("StartPanel").GetComponent<StartPanel>().SetTheme();
            });
            transform.Find("ThemeTitle/ThemeZenBtn").GetComponent<Button>().onClick.AddListener(() => {
                ThemeManager.Instance.SetTheme("ThemeZen");
                SetTheme();
                transform.parent.Find("StartPanel").GetComponent<StartPanel>().SetTheme();
            });
            transform.Find("ThemeTitle/ThemeFlowerBtn").GetComponent<Button>().onClick.AddListener(() => {
                ThemeManager.Instance.SetTheme("ThemeFlower");
                SetTheme();
                transform.parent.Find("StartPanel").GetComponent<StartPanel>().SetTheme();
            });
        }

        private void SetTheme() {
            var theme = ThemeManager.Instance.GetTheme();

            transform.GetComponent<Image>().color = theme.BGColor;

            transform.Find("Title").GetComponent<Text>().color = theme.TitleFontColor;
            transform.Find("ThemeTitle").GetComponent<Text>().color = theme.TitleFontColor;

            transform.Find("RowInput").Find("Title").GetComponent<Text>().color = theme.TitleFontColor;
            transform.Find("RowInput").GetComponent<Image>().sprite = theme.InputFieldSprite;
            transform.Find("RowInput").GetComponent<InputField>().caretColor = theme.CaretColor;
            transform.Find("RowInput").GetComponent<InputField>().selectionColor = theme.SelectionColor;
            transform.Find("RowInput").Find("Text").GetComponent<Text>().color = theme.InputFieldFontColor;

            transform.Find("LineInput").Find("Title").GetComponent<Text>().color = theme.TitleFontColor;
            transform.Find("LineInput").GetComponent<Image>().sprite = theme.InputFieldSprite;
            transform.Find("LineInput").GetComponent<InputField>().caretColor = theme.CaretColor;
            transform.Find("LineInput").GetComponent<InputField>().selectionColor = theme.SelectionColor;
            transform.Find("LineInput").Find("Text").GetComponent<Text>().color = theme.BGColor;

            transform.Find("MineInput").Find("Title").GetComponent<Text>().color = theme.TitleFontColor;
            transform.Find("MineInput").GetComponent<Image>().sprite = theme.InputFieldSprite;
            transform.Find("MineInput").GetComponent<InputField>().caretColor = theme.CaretColor;
            transform.Find("MineInput").GetComponent<InputField>().selectionColor = theme.SelectionColor;
            transform.Find("MineInput").Find("Text").GetComponent<Text>().color = theme.InputFieldFontColor;

            transform.Find("Tip").GetComponent<Text>().color = theme.TipColor;

            transform.Find("SetRLMBtn").GetComponent<Image>().sprite = theme.ButtonSprite;
            var spriteState = transform.Find("SetRLMBtn").GetComponent<Button>().spriteState;
            spriteState.pressedSprite = theme.ButtonPressedSprite;
            transform.Find("SetRLMBtn").GetComponent<Button>().spriteState = spriteState;
            transform.Find("SetRLMBtn").Find("Text").GetComponent<Text>().color = theme.ButtonFontColor;

            transform.Find("BackBtn").GetComponent<Image>().sprite = theme.ButtonSprite;
            spriteState = transform.Find("BackBtn").GetComponent<Button>().spriteState;
            spriteState.pressedSprite = theme.ButtonPressedSprite;
            transform.Find("BackBtn").GetComponent<Button>().spriteState = spriteState;
            transform.Find("BackBtn").Find("Text").GetComponent<Text>().color = theme.ButtonFontColor;
        }

        private bool TestIfInputOK(string rowText, string lineText, string mineText) {
            if (int.TryParse(rowText, out var rowNum)) {
                if (int.TryParse(lineText, out var lineNum)) {
                    if (int.TryParse(mineText, out var mineNum)) {
                        if (rowNum * lineNum <= mineNum + 20) {
                            transform.Find("Tip").GetComponent<Text>().text = "<!>the mineNum is too big<!>";
                            return false;
                        }

                        if (rowNum <= 0 || lineNum <= 0 || mineNum <= 0) {
                            transform.Find("Tip").GetComponent<Text>().text = "<!>must be positive number<!>";
                            return false;
                        }

                        if (rowNum >= 30) {
                            transform.Find("Tip").GetComponent<Text>().text = "<!>the rowNum is too big<!>";
                            return false;
                        }

                        if (lineNum >= 40) {
                            transform.Find("Tip").GetComponent<Text>().text = "<!>the lineNum is too big<!>";
                            return false;
                        }

                        if (mineNum >= 800) {
                            transform.Find("Tip").GetComponent<Text>().text = "<!>the mineNum is too big<!>";
                            return false;
                        }

                        return true;
                    }
                }
            }

            return false;
        }
    }
}