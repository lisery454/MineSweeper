using System;
using Screen = UnityEngine.Device.Screen;

public class RatioAdjuster : Singleton<RatioAdjuster> {
    private int ScreenHeight;
    private int ScreenWidth;

    public Action OnHeightOrWidthChanged = null;


    private void Update() {
        if (ScreenHeight != Screen.height || ScreenWidth != Screen.width) {
            OnHeightOrWidthChanged?.Invoke();
            ScreenHeight = Screen.height;
            ScreenWidth = Screen.width;
        }
    }
}