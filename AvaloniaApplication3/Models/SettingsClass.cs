using System;
using Avalonia.Styling;

namespace AvaloniaApplication3.Models
{
    [Serializable]
    public class SettingsClass
    {
        public int InterfaceTextSize { get; set; }
        public ThemeVariant Theme { get; set; }
        public int ImageSize { get; set; }
        public SettingsClass(int size, ThemeVariant tw, int imageSize) 
        {
            InterfaceTextSize = size;
            Theme = tw;
            ImageSize = imageSize;
        }
        public SettingsClass()
        {
            Theme = Avalonia.Styling.ThemeVariant.Default;
            InterfaceTextSize = 26;
            ImageSize = 200;
        }
        
    }
}
