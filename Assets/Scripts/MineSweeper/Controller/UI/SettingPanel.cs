using DG.Tweening;
using MineSweeper.Theme;
using QFramework;
using UnityEngine;
using UnityEngine.UI;


namespace MineSweeper {
    public class SettingPanel : AbstractController {
        private InputField RowInput;
        private InputField LineInput;
        private InputField MineInput;

        private GridModel gridModel;

        private Transform startPanelTransform;

        private void Start() {
            //设置主题
            SetTheme();

            RatioAdjuster.Instance.OnHeightOrWidthChanged += SetLocationWhenScreenChange;

            gridModel = this.GetModel<GridModel>();
            RowInput = transform.Find("RowInput").GetComponent<InputField>();
            LineInput = transform.Find("LineInput").GetComponent<InputField>();
            MineInput = transform.Find("MineInput").GetComponent<InputField>();
            startPanelTransform = transform.parent.Find("StartPanel");
            transform.Find("SetRLMBtn").GetComponent<Button>().onClick.AddListener(() => {
                if (TestIfInputOK(RowInput.text, LineInput.text, MineInput.text)) {
                    gridModel.MineNum.Value = int.Parse(MineInput.text);
                    gridModel.LineNum.Value = int.Parse(LineInput.text);
                    gridModel.RowNum.Value = int.Parse(RowInput.text);
                    transform.Find("Tip").GetComponent<Text>().text = "<!>Set Successfully<!>";
                    AudioManager.Instance.PlayAudio("a3");
                }
                else {
                    AudioManager.Instance.PlayAudio("a2");
                }
            });

            transform.Find("BackBtn").GetComponent<Button>().onClick.AddListener(() => {
                startPanelTransform.DOMove(new Vector3(Screen.width / 2f, Screen.height / 2f), 0.5f);
                transform.DOMove(new Vector3(Screen.width * 3 / 2f, Screen.height / 2f), 0.5f);
                AudioManager.Instance.PlayAudio("a1");
            });

            transform.Find("ThemeTitle/ThemeSeaBtn").GetComponent<Button>().onClick.AddListener(() => {
                ThemeManager.Instance.SetTheme("Sea");
                SetTheme();
                transform.parent.Find("StartPanel").GetComponent<StartPanel>().SetTheme();
                AudioManager.Instance.PlayAudio("a3");
            });
            transform.Find("ThemeTitle/ThemeZenBtn").GetComponent<Button>().onClick.AddListener(() => {
                ThemeManager.Instance.SetTheme("Zen");
                SetTheme();
                transform.parent.Find("StartPanel").GetComponent<StartPanel>().SetTheme();
                AudioManager.Instance.PlayAudio("a3");
            });
            transform.Find("ThemeTitle/ThemeFlowerBtn").GetComponent<Button>().onClick.AddListener(() => {
                ThemeManager.Instance.SetTheme("Flower");
                SetTheme();
                transform.parent.Find("StartPanel").GetComponent<StartPanel>().SetTheme();
                AudioManager.Instance.PlayAudio("a3");
            });
        }

        private void SetTheme() {
            var theme = ThemeManager.Instance.GetTheme();

            transform.GetComponent<Image>().color = theme.BGColor;

            transform.Find("Title").GetComponent<Text>().color = theme.TitleFontColor;
            transform.Find("ThemeTitle").GetComponent<Text>().color = theme.TitleFontColor;

            var rowInput = transform.Find("RowInput");
            rowInput.Find("Title").GetComponent<Text>().color = theme.TitleFontColor;
            rowInput.GetComponent<Image>().sprite = theme.InputFieldSprite;
            rowInput.GetComponent<InputField>().caretColor = theme.CaretColor;
            rowInput.GetComponent<InputField>().selectionColor = theme.SelectionColor;
            rowInput.Find("Text").GetComponent<Text>().color = theme.InputFieldFontColor;

            var lineInput = transform.Find("LineInput");
            lineInput.Find("Title").GetComponent<Text>().color = theme.TitleFontColor;
            lineInput.GetComponent<Image>().sprite = theme.InputFieldSprite;
            lineInput.GetComponent<InputField>().caretColor = theme.CaretColor;
            lineInput.GetComponent<InputField>().selectionColor = theme.SelectionColor;
            lineInput.Find("Text").GetComponent<Text>().color = theme.InputFieldFontColor;

            var mineInput =  transform.Find("MineInput");
            mineInput.Find("Title").GetComponent<Text>().color = theme.TitleFontColor;
            mineInput.GetComponent<Image>().sprite = theme.InputFieldSprite;
            mineInput.GetComponent<InputField>().caretColor = theme.CaretColor;
            mineInput.GetComponent<InputField>().selectionColor = theme.SelectionColor;
            mineInput.Find("Text").GetComponent<Text>().color = theme.InputFieldFontColor;

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

        private void SetLocationWhenScreenChange() {
            transform.position = new Vector3(Screen.width * 3 / 2f, Screen.height / 2f);
        }

        private void OnDestroy() {
            transform.DOKill();
            if (RatioAdjuster.IsExist)
                RatioAdjuster.Instance.OnHeightOrWidthChanged -= SetLocationWhenScreenChange;
        }
    }
}