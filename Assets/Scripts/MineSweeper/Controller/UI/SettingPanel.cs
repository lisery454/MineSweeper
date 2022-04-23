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
                gridModel.MineNum.Value = int.Parse(MineInput.text);
                gridModel.LineNum.Value = int.Parse(LineInput.text);
                gridModel.RowNum.Value = int.Parse(RowInput.text);
                SceneManager.LoadScene("MainGame");
            });
        }
    }
}