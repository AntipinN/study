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
                case "������":
                    {
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

        private void Button_Click_GenerateGame(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            
        }

        
    }
}