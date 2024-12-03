using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Data;
using Avalonia.Interactivity;
using Avalonia.VisualTree;
using AvaloniaApplication3;
using AvaloniaApplication3.ViewModels;
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
                case "������":
                    {
                        MainMenu.IsVisible = false;
                        EquasionChoiceMenu.IsVisible = true;
                        break;
                    }
                case "����������":
                    {
                        break;
                    }
                case "���������":
                    {
                        MainMenu.IsVisible = false;
                        SettingsMenu.IsVisible = true;
                        break;
                    }
                case "�����":
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
                case "���������":
                    {
                        break;
                    }
                case "�� ���������":
                    {
                        TextSize.Value = 26;
                        SoundSize.Value = 5;
                        MusicSize.Value = 5;
                        break;
                    }
                case "�����":
                    {
                        SettingsMenu.IsVisible = false;
                        MainMenu.IsVisible = true;
                        break;
                    }
            }
        }

        private void Button_Click_EquasionChoice(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            switch ((sender as Button).Content.ToString())
            {
                case "��������":
                    {
                        EquasionChoiceMenu.IsVisible = false;
                        SummMenu.IsVisible = true;
                        break;
                    }
                case "��������e":
                    {
                        EquasionChoiceMenu.IsVisible = false;
                        SubtractMenu.IsVisible = true;
                        break;
                    }
                case "���������":
                    {
                        EquasionChoiceMenu.IsVisible = false;
                        MultiplyMenu.IsVisible = true;
                        break;
                    }
                default:
                    {
                        EquasionChoiceMenu.IsVisible = false;
                        MainMenu.IsVisible = true;
                        break;
                    }
            }
        }

        private void Button_Click_DifficultyMenu(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            ((sender as Button).Parent as StackPanel).IsVisible = false;
            if ((sender as Button).Content.ToString() == "�����")
            {
                EquasionChoiceMenu.IsVisible = true;
            }
            else
            {
                Playground.IsVisible = true;
            }
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