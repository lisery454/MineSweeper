using System.Collections;
using MineSweeper.Theme;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : SingletonInOneScene<SceneLoader> {
    private Animator animator;

    protected override void Awake() {
        base.Awake();
        animator = GetComponent<Animator>();
    }

    private void Start() {
        animator.Play("LoadSceneSuccess" + ThemeManager.Instance.GetTheme().ThemeName);
    }


    public void LoadScene(string sceneName) {
        StartCoroutine(LoadSceneCoroutine(sceneName));
    }

    private IEnumerator LoadSceneCoroutine(string sceneName) {
        animator.Play("LoadScene" + ThemeManager.Instance.GetTheme().ThemeName);
        yield return new WaitForSeconds(1f);
        var operation = SceneManager.LoadSceneAsync(sceneName);
        while (!operation.isDone) //当场景没有加载完毕
        {
            yield return null;
        }
    }
}