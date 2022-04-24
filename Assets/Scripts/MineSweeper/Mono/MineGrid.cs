using UnityEngine;

namespace MineSweeper {
    public class MineGrid : MonoBehaviour {
        public int Row { get; set; }
        public int Line { get; set; }

        private GameObject markObj;
        private GameObject numObj;
        private GameObject mineObj;
        public GameObject backObj;

        private void Start() {
            markObj = transform.Find("Mark").gameObject;
            numObj = transform.Find("Num").gameObject;
            mineObj = transform.Find("Mine").gameObject;
            backObj = transform.Find("Back").gameObject;
        }

        public void ShowNum(int num) {
            markObj.SetActive(false);
            numObj.SetActive(true);
            backObj.SetActive(true);
            var textMesh = numObj.transform.Find("Mesh").GetComponent<TextMesh>();

            textMesh.text = num != 0 ? num.ToString() : "";
        }

        public void ShowMark(bool isMark) {
            markObj.SetActive(isMark);
        }

        public void ShowMine() {
            markObj.SetActive(false);
            mineObj.SetActive(true);
            backObj.SetActive(true);
        }
    }
}