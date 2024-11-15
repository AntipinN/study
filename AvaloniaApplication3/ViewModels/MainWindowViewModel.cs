using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Media;
using Avalonia.Rendering.Composition;
using AvaloniaApplication3.Models;
using static AvaloniaApplication3.Models.SumOfFractionGame;
using AvaloniaApplication3.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.Generic;
using System.Windows.Input;
using CommunityToolkit.Mvvm.Input;

namespace AvaloniaApplication3.ViewModels
{
    public partial class MainWindowViewModel : ViewModelBase
    {
        public MainWindowViewModel()
        {
            CreateGameCommand = new RelayCommand(CreateGame);
        }


#pragma warning disable CA1822 // Mark members as static
        public string Greeting => "Welcome to Avalonia!";
        public Brush MainBackgroundColor => new SolidColorBrush(Color.FromRgb(255, 152, 152), 1);
        public Brush MainBorderColor => new SolidColorBrush(Color.FromRgb(255, 153, 255), 1);
        public Color MainTextColor => Color.FromRgb(213, 0, 127);

        [ObservableProperty]
        private int interfaceTextSize = 26;
        
        partial void OnInterfaceTextSizeChanging(int value)
        {
            InterfaceTitleSize = value + 12;
            OnPropertyChanged(nameof(InterfaceTitleSize));
        }

        [ObservableProperty]
        private int interfaceTitleSize = 26 + 12;
        [ObservableProperty]
        private int soundSize = 5;
        [ObservableProperty]
        private int musicSize = 5;

        [ObservableProperty]
        List<object> equasion = new List<object>() { "Bober" };

        public ICommand CreateGameCommand { get; } 

        public void CreateGame()
        {
            GameGenerator Game = new GameGenerator(new EasyDifficultyEquasion(), new FractionEquasionSolver(), new UniversalOPZFormer());
            Equasion = Game.GetExpression();
        }



        private DropShadowDirectionEffect shadow;
        public DropShadowDirectionEffect Shadow { get => shadow; set => shadow = new DropShadowDirectionEffect() { BlurRadius = 4, Color = Colors.Black, Opacity = 0.4, Direction =20}; }


        

#pragma warning restore CA1822 // Mark members as static
    }
}
