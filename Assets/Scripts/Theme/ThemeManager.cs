using System.Collections.Generic;
using UnityEngine;

namespace MineSweeper.Theme {
    public class ThemeManager : Singleton<ThemeManager> {
        [SerializeField] private List<Theme> themes;
        [SerializeField] private string currentThemeName = "ThemeZen";

        public ThemeManager() {
            
        }

        public void SetTheme(string themeName) {
            currentThemeName = themeName;
        }

        public Theme GetTheme() {
            var theme = themes.Find(t => t.ThemeName == currentThemeName);
            return theme;
        }
    }
}