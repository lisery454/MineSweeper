using UnityEngine;
using UnityEngine.UI;

namespace MineSweeper {
    public class StartPanel : MonoBehaviour {
        private Transform settingPanelTransform;

        private void Start() {
            settingPanelTransform = transform.parent.Find("SettingPanel");

            transform.Find("StartBtn").GetComponent<Button>().onClick.AddListener(() => {
                settingPanelTransform.gameObject.SetActive(true);
            });
        }
    }
}