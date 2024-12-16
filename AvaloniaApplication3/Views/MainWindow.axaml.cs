using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.VisualTree;
using System;
using System.Linq;


namespace AvaloniaApplication3.Views
{
    public partial class MainWindow : Window
    {
        
        public MainWindow()
        {
            InitializeComponent();
            InstructionView.PageTransition = new CustomFade(new TimeSpan(5000000));
            InstructionText.PageTransition = new CustomFade(new TimeSpan(5000000));
        }
        private void Button_Click_MainMenu(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            switch ((sender as Button).Content.ToString())
            {
                case "Играть":
                    {
                        MainMenu.IsVisible = false;
                        MainMenu.IsEnabled = false;
                        EquasionChoiceMenu.IsVisible = true;
                        EquasionChoiceMenu.IsEnabled= true;
                        break;
                    }
                case "Инструкция":
                    {
                        MainMenu.IsVisible = false;
                        InstructionText.SelectedIndex = 0;
                        InstructionMenu.IsVisible = true;
                        break;
                    }
                case "Настройки":
                    {
                        MainMenu.IsVisible = false;
                        MainMenu.IsEnabled = false;
                        SettingsMenu.IsVisible = true;
                        SettingsMenu.IsEnabled = true;
                        break;
                    }
                case "Выйти":
                    {
                        Environment.Exit(0);
                        break;
                    }
            }
            FocusManager?.ClearFocus();
        }

        private void Button_Click_BackFromInstructionMenu(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            InstructionMenu.IsVisible = false;
            MainMenu.IsVisible = true;
            InstructionView.SelectedIndex = 0;
            InstructionText.SelectedIndex = 0; 
            FocusManager?.ClearFocus();
        }

        private void Button_Click_Settings(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            switch((sender as Button).Content.ToString())
            {
                case "Применить":
                    {
                        //App.RequestedThemeVariantProperty = Avalonia.Styling.ThemeVariant.Dark;

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
                        SettingsMenu.IsEnabled = false;
                        MainMenu.IsVisible = true;
                        MainMenu.IsEnabled = true;
                        break;
                    }
            }
            FocusManager?.ClearFocus();
        }

        private void Button_Click_EquasionChoice(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            switch ((sender as Button).Content.ToString())
            {
                case "Сложение":
                    {
                        EquasionChoiceMenu.IsVisible = false;
                        EquasionChoiceMenu.IsEnabled = false;
                        SummMenu.IsVisible = true;
                        SummMenu.IsEnabled = true;
                        break;
                    }
                case "Вычитаниe":
                    {
                        EquasionChoiceMenu.IsVisible = false;
                        EquasionChoiceMenu.IsEnabled = false;
                        SubtractMenu.IsVisible = true;
                        SubtractMenu.IsEnabled = true;
                        break;
                    }
                case "Умножение":
                    {
                        EquasionChoiceMenu.IsVisible = false;
                        EquasionChoiceMenu.IsEnabled = false;
                        MultiplyMenu.IsVisible = true;
                        MultiplyMenu.IsEnabled = true;
                        break;
                    }
                default:
                    {
                        EquasionChoiceMenu.IsVisible = false;
                        EquasionChoiceMenu.IsEnabled = false;
                        MainMenu.IsVisible = true;
                        MainMenu.IsEnabled = true;
                        break;
                    }
            }
            FocusManager?.ClearFocus();
        }

        private void Button_Click_DifficultyMenu(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            ((sender as Button).Parent as StackPanel).IsVisible = false;
            if ((sender as Button).Content.ToString() == "Назад")
            {
                EquasionChoiceMenu.IsVisible = true;
                EquasionChoiceMenu.IsEnabled = true;
            }
            else
            {
                Playground.IsVisible = true;
                Playground.IsEnabled = true;
            }
            FocusManager?.ClearFocus();
        }
        private void Button_Click_EndGame(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            Playground.IsVisible = false;
            Playground.IsEnabled = false;
            EndGameStatistics.IsVisible = true;
            EndGameStatistics.IsEnabled = true;
            FocusManager?.ClearFocus();
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
            EndGameStatistics.IsEnabled = false;
            MainMenu.IsVisible = true;
            MainMenu.IsEnabled = true;
            FocusManager?.ClearFocus();
        }

        public void Next(object sender, RoutedEventArgs args)
        {
            if (InstructionText.Items.Count-1 > InstructionText.SelectedIndex)
            {
                InstructionText.SelectedIndex++;
                InstructionView.SelectedIndex++;
            }
            TextBlock_SizeChanged_ChangeTextBlockWidth(this, new Avalonia.Controls.SizeChangedEventArgs(null));
        }

        public void Previous(object sender, RoutedEventArgs args)
        {
            if (InstructionText.SelectedIndex > 0)
            {
                InstructionText.SelectedIndex--;
                InstructionView.SelectedIndex--;
            }
            TextBlock_SizeChanged_ChangeTextBlockWidth(this, new Avalonia.Controls.SizeChangedEventArgs(null));
        }

        public void TextBlock_SizeChanged_ChangeTextBlockWidth(object? sender, Avalonia.Controls.SizeChangedEventArgs e)
        {
            
            //if(sender is not null &&
            //    (sender as TextBlock).Parent is Carousel &&
            //    ((sender as TextBlock).Parent as Carousel).Parent is StackPanel &&
            //    (((sender as TextBlock).Parent as Carousel).Parent as StackPanel).Parent is Button &&
            //    ((((sender as TextBlock).Parent as Carousel).Parent as StackPanel).Parent as Button).Parent is Grid &&
            //    (((((sender as TextBlock).Parent as Carousel).Parent as StackPanel).Parent as Button).Parent as Grid).Parent is Panel &&
            //    ((((((sender as TextBlock).Parent as Carousel).Parent as StackPanel).Parent as Button).Parent as Grid).Parent as Panel).Parent is Grid &&
            //    (((((((sender as TextBlock).Parent as Carousel).Parent as StackPanel).Parent as Button).Parent as Grid).Parent as Panel).Parent as Grid).Parent is Window
            //    )
            //(sender as TextBlock).MinWidth = ((((((((sender as TextBlock).Parent as Carousel).Parent as StackPanel).Parent as Button).Parent as Grid).Parent as Panel).Parent as Grid).Parent as Window).Width - 300 - 20;
            foreach(TextBlock tb in InstructionText.Items)
            {
                tb.Width = TheWindow.Width - TeachersImage.Width - 203 - tb.FontSize*2;
            }
            
        }


    }
}