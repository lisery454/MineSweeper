using UnityEngine;

namespace MineSweeper {
    public class MineGrid : MonoBehaviour {
        public int Row { get; set; }
        public int Line { get; set; }

        private GameObject markObj;
        private GameObject numObj;
        private GameObject mineObj;
        private GameObject backObj;
        private GameObject highLightObj;

        private GameObject frontRoot;


        private void Start() {
            frontRoot = transform.Find("FrontRoot").gameObject;

            markObj = frontRoot.transform.Find("Mark").gameObject;
            highLightObj = frontRoot.transform.Find("HighLight").gameObject;

            numObj = transform.Find("Num").gameObject;
            mineObj = transform.Find("Mine").gameObject;
            backObj = transform.Find("Back").gameObject;
        }

        public void ShowNum(int num) {
            ShowBack();
            numObj.SetActive(true);
            var textMesh = numObj.transform.Find("Mesh").GetComponent<TextMesh>();
            textMesh.text = num != 0 ? num.ToString() : "";
        }

        public void ShowMark(bool isMark) {
            markObj.SetActive(isMark);
        }

        public void ShowMine() {
            ShowBack();
            mineObj.SetActive(true);
        }

        public void ShowBack() {
            backObj.SetActive(true);
            frontRoot.SetActive(false);
        }

        public void NotShowBack() {
            backObj.SetActive(false);
            frontRoot.SetActive(true);
        }

        public void ShowHighLight() {
            if (highLightObj != null)
                highLightObj.SetActive(true);
        }

        public void NotShowHighLight() {
            if (highLightObj != null)
                highLightObj.SetActive(false);
        }
    }
}