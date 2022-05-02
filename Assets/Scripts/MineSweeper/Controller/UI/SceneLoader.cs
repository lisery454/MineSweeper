using System.Collections;
using MineSweeper.Theme;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : Singleton<SceneLoader> {
    private Animator animator;

    protected override void Awake() {
        base.Awake();
        animator = GetComponent<Animator>();
    }

    public void LoadScene(string sceneName) {
        StartCoroutine(LoadSceneCoroutine(sceneName));
    }

    private IEnumerator LoadSceneCoroutine(string sceneName) {
        animator.Play("LoadScene" + ThemeManager.Instance.GetTheme().ThemeName);
        yield return new WaitForSeconds(1f);
        SceneManager.LoadSceneAsync(sceneName).completed += OnSceneLoadSuccess;
    }

    private void OnSceneLoadSuccess(AsyncOperation obj) {
        animator.Play("LoadSceneSuccess" + ThemeManager.Instance.GetTheme().ThemeName);
    }
}