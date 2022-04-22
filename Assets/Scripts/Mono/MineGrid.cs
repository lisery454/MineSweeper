using UnityEngine;

namespace MineSweeper {
    public class MineGrid : MonoBehaviour {
        public int Row { get; set; }
        public int Line { get; set; }

        public void ShowNum(int num) {
            transform.Find("Num").gameObject.SetActive(true);
            var textMesh = transform.Find("Num").GetComponent<TextMesh>();

            textMesh.text = num.ToString();
        }

        public void ShowMine() {
            transform.Find("Mine").gameObject.SetActive(true);
            //this.SendCommand<GameEndCommand>();
        }
    }
}