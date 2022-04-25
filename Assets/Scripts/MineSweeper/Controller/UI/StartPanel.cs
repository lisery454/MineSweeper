using UnityEngine;
using UnityEngine.UI;

namespace MineSweeper {
    public class StartPanel : AbstractController {
        private Transform settingPanelTransform;

        private void Start() {
            settingPanelTransform = transform.parent.Find("SettingPanel");

            transform.Find("StartBtn").GetComponent<Button>().onClick.AddListener(() => {
                settingPanelTransform.gameObject.SetActive(true);
            });

            transform.Find("QuitBtn").GetComponent<Button>().onClick.AddListener(() => { Application.Quit(0); });
        }
    }
}