using System;
using Avalonia.Styling;

namespace AvaloniaApplication3.Models
{
    [Serializable]
    public class SettingsClass
    {
        public int InterfaceTextSize { get; set; }
        public ThemeVariant Theme { get; set; }

        public SettingsClass(int size, ThemeVariant tw) 
        {
            InterfaceTextSize = size;
            Theme = tw;
        }
        public SettingsClass()
        {
            Theme = Avalonia.Styling.ThemeVariant.Default;
            InterfaceTextSize = 26;
        }
        
    }
}
