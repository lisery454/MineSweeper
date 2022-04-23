using UnityEngine;

namespace MineSweeper {
    public class MineGrid : MonoBehaviour {
        public int Row { get; set; }
        public int Line { get; set; }

        private GameObject markObj;
        private GameObject numObj;
        private GameObject mineObj;

        private void Start() {
            markObj = transform.Find("Mark").gameObject;
            numObj = transform.Find("Num").gameObject;
            mineObj = transform.Find("Mine").gameObject;
        }

        public void ShowNum(int num) {
            markObj.SetActive(false);
            numObj.SetActive(true);
            var textMesh = numObj.GetComponent<TextMesh>();

            textMesh.text = num.ToString();
        }

        public void ShowMark(bool isMark) {
            markObj.SetActive(isMark);
        }

        public void ShowMine() {
            markObj.SetActive(false);
            mineObj.SetActive(true);
        }
    }
}