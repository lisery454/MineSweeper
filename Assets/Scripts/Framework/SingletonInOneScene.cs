using UnityEngine;

public abstract class SingletonInOneScene<T> : MonoBehaviour where T : Component {
    private static T instance;

    public static T Instance => instance;

    protected virtual void Awake() {
        if (instance == null) {
            instance = this as T;
        }
        else {
            Destroy(gameObject);
        }
    }
}