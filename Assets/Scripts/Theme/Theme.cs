using UnityEngine;

namespace MineSweeper.Theme {
    [CreateAssetMenu(fileName = "Theme", menuName = "Custom/Theme", order = 1)]
    public class Theme : ScriptableObject {
        public string ThemeName;
        public Color TitleFontColor = Color.white;
        public Color ButtonFontColor = Color.white;
        public Sprite ButtonSprite;
        public Sprite ButtonPressedSprite;
        public Color BGColor = Color.white;
        public Color TipColor = Color.white;
        public Sprite InputFieldSprite;
        public Color InputFieldFontColor = Color.white;
        public Sprite GridSprite;
        public Sprite GridBackSprite;
        public Sprite GridHighLightSprite;
        public Sprite MineSprite;
        public Sprite MarkSprite;
        public Sprite EndUIBGSprite;
        public Color EndUITitleColor = Color.white;
        public Color GridNumColor = Color.white;
        public Color CaretColor = Color.white;
        public Color SelectionColor = Color.white;
    }
}