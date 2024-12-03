using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Interactivity;
using Avalonia.VisualTree;
using AvaloniaApplication3;
using System;
using System.ComponentModel;
using System.Linq;
using Tmds.DBus.Protocol;

namespace AvaloniaApplication3.Views
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        private void Button_Click_MainMenu(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            switch ((sender as Button).Content.ToString())
            {
                case "Играть":
                    {
                        MainMenu.IsVisible = false;
                        DegreeChooseMenu.IsVisible = true;
                        break;
                    }
                case "Инструкция":
                    {
                        break;
                    }
                case "Настройки":
                    {
                        MainMenu.IsVisible = false;
                        SettingsMenu.IsVisible = true;
                        break;
                    }
                case "Выйти":
                    {
                        Environment.Exit(0);
                        break;
                    }
            }
        }

        private void Button_Click_Settings(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            switch((sender as Button).Content.ToString())
            {
                case "Применить":
                    {
                        break;
                    }
                case "По умолчанию":
                    {
                        TextSize.Value = 26;
                        SoundSize.Value = 5;
                        MusicSize.Value = 5;
                        break;
                    }
                case "Назад":
                    {
                        SettingsMenu.IsVisible = false;
                        MainMenu.IsVisible = true;
                        break;
                    }
            }
        }

        private void Button_Click_FifthDegree(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            FifthDegree.IsVisible = false;
            Playground.IsVisible = true;
        }
        private void Button_Click_SixthDegree(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            SixthDegree.IsVisible = false;
            Playground.IsVisible = true;
        }

        private void Button_Click_ChoseDegree(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            switch((sender as Button).Content)
            {
                case "5 класс":
                    {
                        DegreeChooseMenu.IsVisible = false;
                        FifthDegree.IsVisible = true;
                        break;
                    }
                case "6 класс":
                    {
                        DegreeChooseMenu.IsVisible = false;
                        SixthDegree.IsVisible = true;
                        break;
                    }
            }
        }


        private void Button_Click_ChosenDegreeBack(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            switch ((sender as Button).Parent.Name)
            {
                case "FifthDegree":
                    {
                        FifthDegree.IsVisible = false;
                        DegreeChooseMenu.IsVisible = true;
                        break;
                    }
                case "SixthDegree":
                    {
                        SixthDegree.IsVisible = false;
                        DegreeChooseMenu.IsVisible = true;
                        break;
                    }
            }
        }
        private void Button_Click_DegreeMenuBack(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            DegreeChooseMenu.IsVisible = false;
            MainMenu.IsVisible = true;
        }

        private void Button_Click_EndGame(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            Playground.IsVisible = false;
            EndGameStatistics.IsVisible = true;
        }

        private void Button_SizeChanged(object? sender, Avalonia.Controls.SizeChangedEventArgs e)
        {
            AcceptButton.Width = (sender as Button).DesiredSize.Width - 20;
        }

        private void Grid_SizeChanged_ChangeButtonWidth(object? sender, Avalonia.Controls.SizeChangedEventArgs e)
        {
            int maxImageCheckSize = 150;
            foreach(Control element in (sender as Grid).GetVisualChildren())
            {
                if(element is Button)
                {
                    (element as Button).MaxWidth = (sender as Grid).ColumnDefinitions[0].ActualWidth - 1;
                }
                if(element is Image)
                {
                    (element as Image).MaxHeight = (new double[] { (sender as Grid).ColumnDefinitions[0].ActualWidth - 1, (sender as Grid).RowDefinitions[0].ActualHeight - 1, maxImageCheckSize }).Min();
                    (element as Image).MaxWidth = (element as Image).MaxHeight;
                }
                
            }
        }

        private void Button_Click_GoToMainMenu(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            EndGameStatistics.IsVisible = false;
            MainMenu.IsVisible = true;
        }
    }
}