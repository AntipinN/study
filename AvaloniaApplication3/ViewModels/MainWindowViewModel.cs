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
using System.Linq;
using System;
using Avalonia.VisualTree;
using System.Threading;
using System.Diagnostics;
using Avalonia.Threading;
using System.Timers;

namespace AvaloniaApplication3.ViewModels
{
    public partial class MainWindowViewModel : ViewModelBase
    {
        public MainWindowViewModel()
        {
            GenerateTimer();
            CreateGameCommand = new RelayCommand(ContuneGame);
            CreateEasySumGameCommand = new RelayCommand(CreateEasySumGame);
            CreateEasySubtractGameCommand = new RelayCommand(CreateEasySubtractGame);
            CreateEasyMultiplyGameCommand = new RelayCommand(CreateEasyMultiplyGame);
       
            VerifyAnswerCommand = new RelayCommand(VerifyAnswer);
            EndGameCommand = new RelayCommand(EndGame);
        }


#pragma warning disable CA1822 // Mark members as static
        #region Settings
        [ObservableProperty]
        private int interfaceTextSize = 26;
        partial void OnInterfaceTextSizeChanging(int value)
        {
            InterfaceTitleSize = value + 12;

        }
        [ObservableProperty]
        private int interfaceTitleSize = 26 + 12;
        [ObservableProperty]
        private int soundSize = 5;
        [ObservableProperty]
        private int musicSize = 5;
        #endregion


        #region Game Data
        [ObservableProperty]
        List<object> equasion = new List<object>() { "Bober" };
        [ObservableProperty]
        List<object> answerEquasion = new List<object>() { "Bober" };

        [ObservableProperty]
        bool isEqusaisonIncorrect = false;
        [ObservableProperty]
        string isEquasionCorrectTextBlock = "Твой ответ неверен, попробуй пересчитать еще раз!";
        [ObservableProperty]
        bool generateEquasionIsActive = true;

        GameGenerator EasySumGame = new GameGenerator(new EasyDifficultyEquasionSum(), new FractionEquasionSolver(), new UniversalOPZFormer());
        GameGenerator EasySubtractGame = new GameGenerator(new EasyDifficultyEquasionSubtraction(), new FractionEquasionSolver(), new UniversalOPZFormer());
        GameGenerator EasyMultiplyGame = new GameGenerator(new EasyDifficultyEquasionMultiply(), new FractionEquasionSolver(), new UniversalOPZFormer());

        [ObservableProperty]
        string timerString = "";

        private DispatcherTimer _timer = new DispatcherTimer();
        Stopwatch stopwatchGame = new Stopwatch();
        #endregion

        #region Commands Region
        public ICommand CreateGameCommand { get; }
        public ICommand CreateEasySumGameCommand { get; }
        bool EasySumGameCreated = false;
        public ICommand CreateEasySubtractGameCommand { get; }
        bool EasySibtractGameCreated = false;
        public ICommand CreateEasyMultiplyGameCommand { get; }
        bool EasyMultiplyGameCreated = false;
        public ICommand CreateMediumSumGameCommand { get; }
        bool MediumSumGameCreated = false;
        public ICommand CreateMediumSubtractGameCommand { get; }
        bool MediumSubtractGameCreated = false;
        public ICommand CreateMediumMultiplyGameCommand { get; }
        bool MediumMultiplyGameCreated = false;
        public ICommand VerifyAnswerCommand { get; }

        public ICommand EndGameCommand { get; }
        #endregion

        #region Methods Region

        void ContuneGame()
        {
            if (EasySumGameCreated)
            {
                CreateGame(EasySumGame);
            }
            else if (EasySibtractGameCreated)
            {
                CreateGame(EasySubtractGame);
            }
            else if(EasyMultiplyGameCreated)
            {
                CreateGame(EasyMultiplyGame);
            }
        }
        void EndGame()
        {
            EasySumGameCreated = false;
            EasySibtractGameCreated = false;
            EasyMultiplyGameCreated = false;
            MediumSumGameCreated = false;
            MediumSubtractGameCreated = false;
            MediumMultiplyGameCreated = false;
            IsEqusaisonIncorrect = false;

            StopTimer();
        }
        void CreateEasySumGame()
        {
            EasySumGameCreated = true;
            CreateGame(EasySumGame);
        }
        void CreateEasySubtractGame()
        {
            EasySibtractGameCreated= true;
            CreateGame(EasySubtractGame);
        }

        void CreateEasyMultiplyGame()
        {
            EasyMultiplyGameCreated = true;
            CreateGame(EasyMultiplyGame);
        }

        #region TimerRealization
        void GenerateTimer()
        {
            _timer.Interval = TimeSpan.FromSeconds(1);
            _timer.Tick += Timer_Tick;
            UpdateTimerText();
        }
        private void Timer_Tick(object sender, EventArgs e)
        {
            UpdateTimerText();
        }
        private void UpdateTimerText()
        {
            TimerString = stopwatchGame.Elapsed.ToString(@"mm\:ss");
        }
        void StartTimer()
        {
            stopwatchGame.Restart();
            _timer.Start();
        }
        void StopTimer()
        {
            stopwatchGame.Stop();
            stopwatchGame.Reset();
            _timer.Stop();
            UpdateTimerText();
        }
        #endregion

        void CreateGame(GameGenerator Game)
        {
            StartTimer();
            List<object> ButtonsTextBlock = new List<object>();
            List<object> ButtonsTextBox = new List<object>();
            int i = 0;
            List<object> gameObjects = Game.GetExpression();
            foreach (object o in gameObjects)
            {
                string[] list = o.ToString().Split(new char[] { '/', '\n' }, StringSplitOptions.RemoveEmptyEntries);
                Button tmpButtonOne = new Button() { HorizontalContentAlignment = Avalonia.Layout.HorizontalAlignment.Center, Background = Brushes.White };
                Button tmpButtonTwo = new Button() { HorizontalContentAlignment = Avalonia.Layout.HorizontalAlignment.Center, Background = Brushes.White };
                if (list.Length > 1)
                {
                    string middle = string.Concat(Enumerable.Repeat("―", Math.Max(list[0].Length, list[1].Length)));
                    StackPanel st = new StackPanel() { HorizontalAlignment = Avalonia.Layout.HorizontalAlignment.Center, VerticalAlignment = Avalonia.Layout.VerticalAlignment.Center };
                    
                    st.Children.Add(new TextBlock() { Text = list[0], TextAlignment = TextAlignment.Center, MaxHeight = InterfaceTextSize });
                    st.Children.Add(new TextBlock() { Text = middle, Height = 10, TextAlignment = TextAlignment.Center, VerticalAlignment = Avalonia.Layout.VerticalAlignment.Center });
                    st.Children.Add(new TextBlock() { Text = list[1], TextAlignment = TextAlignment.Center, MaxHeight = InterfaceTextSize });

                    //tmpButton.Content = $"{list[0]}{middle}{list[1]}";

                    tmpButtonOne.Content = st;

                    middle = string.Concat(Enumerable.Repeat("―", Math.Max(list[0].Length, list[1].Length)));
                    st = new StackPanel() { HorizontalAlignment = Avalonia.Layout.HorizontalAlignment.Center, VerticalAlignment = Avalonia.Layout.VerticalAlignment.Center };

                    if (i < gameObjects.Count - 1)
                    {
                        st.Children.Add(new TextBlock() { Text = list[0], TextAlignment = TextAlignment.Center, MaxHeight = InterfaceTextSize });
                    }
                    else
                    {
                        TextBox tb = new TextBox() { Text = "", TextAlignment = TextAlignment.Justify, HorizontalContentAlignment =Avalonia.Layout.HorizontalAlignment.Center };
                        tb.TextChanging += TextChanged; //Костыль для корректного отображения горизонтального выравнивания
                        st.Children.Add(tb);
                    }
                    st.Children.Add(new TextBlock() { Text = middle, Height = 10, TextAlignment = TextAlignment.Center, VerticalAlignment = Avalonia.Layout.VerticalAlignment.Center });
                    st.Children.Add(new TextBlock() { Text = list[1], TextAlignment = TextAlignment.Center, MaxHeight = InterfaceTextSize });
                    //tmpButton.Content = $"{list[0]}{middle}{list[1]}";

                    tmpButtonTwo.Content = st;
                }
                else
                {
                    tmpButtonOne.Content = new TextBlock() { Text = list[0], TextAlignment = TextAlignment.Center, MaxHeight = InterfaceTextSize, VerticalAlignment = Avalonia.Layout.VerticalAlignment.Center };
                    tmpButtonTwo.Content = new TextBlock() { Text = list[0], TextAlignment = TextAlignment.Center, MaxHeight = InterfaceTextSize, VerticalAlignment = Avalonia.Layout.VerticalAlignment.Center };
                }
                ButtonsTextBlock.Add(tmpButtonOne);
                ButtonsTextBox.Add(tmpButtonTwo);
                i++;
            }
            Equasion = ButtonsTextBlock;
            AnswerEquasion = ButtonsTextBox;
            GenerateEquasionIsActive = false;
        }
        void TextChanged(object sender, EventArgs e)
        {
            foreach(Control c in ((sender as TextBox).Parent as StackPanel).GetVisualChildren())
            {
                c.InvalidateMeasure();
            }
        }
        public void VerifyAnswer()
        {
            IsEqusaisonIncorrect = false;
            for (int i = 0; i < AnswerEquasion.Count; i++)
            {
                if((AnswerEquasion[i] is Button) && (AnswerEquasion[i] as Button).Content is StackPanel)
                {
                    StackPanel st = ((AnswerEquasion[i] as Button).Content as StackPanel);

                    for (int j = 0; j < st.Children.Count; j++)
                    {
                        if (st.Children[j] is TextBox)
                        {
                            if ((st.Children[j] as TextBox).Text != ((((Equasion[i]as Button).Content as StackPanel).Children[j]) as TextBlock).Text)
                            {
                                IsEqusaisonIncorrect = true;
                                return;
                            }
                        }
                    }
                }
            }
            GenerateEquasionIsActive = true;
        }


        #endregion
        

#pragma warning restore CA1822 // Mark members as static
    }
}
