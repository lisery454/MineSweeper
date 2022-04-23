using QFramework;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


namespace MineSweeper {
    public class SettingPanel : AbstractController {
        private InputField RowInput;
        private InputField LineInput;
        private InputField MineInput;

        private GridModel gridModel;

        private void Start() {
            gridModel = this.GetModel<GridModel>();
            RowInput = transform.Find("RowInput").GetComponent<InputField>();
            LineInput = transform.Find("LineInput").GetComponent<InputField>();
            MineInput = transform.Find("MineInput").GetComponent<InputField>();
            transform.Find("StartBtn").GetComponent<Button>().onClick.AddListener(() => {
                if (TestIfInputOK(RowInput.text, LineInput.text, MineInput.text)) {
                    gridModel.MineNum.Value = int.Parse(MineInput.text);
                    gridModel.LineNum.Value = int.Parse(LineInput.text);
                    gridModel.RowNum.Value = int.Parse(RowInput.text);
                    SceneManager.LoadScene("MainGame");
                }
            });
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