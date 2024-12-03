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
using Avalonia.Controls.Platform;
using System.Text;

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
        List<int> gameTime = new List<int>();
        [ObservableProperty]
        string resultsOfTheGame = "";

        [ObservableProperty]
        List<object> equasion = new List<object>() { "Bober" };
        [ObservableProperty]
        List<object> answerEquasion = new List<object>() { "Bober" };

        [ObservableProperty]
        bool isEqusaisonIncorrect = false;
        [ObservableProperty]
        bool isEqusaisonIncorrectPicture = false;
        [ObservableProperty]
        bool generateEquasionIsActive = false;
        [ObservableProperty]
        bool generateEquasionIsActivePicture = false;

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
            SaveResults();
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
        void SaveResults()
        {
            int seconds = 0;
            foreach(int i in gameTime)
            {
                seconds += i;
            }
            if (gameTime.Count > 0)
            {
                DateTime dateTime = new DateTime();
                dateTime = dateTime.AddSeconds(seconds / gameTime.Count);

                ResultsOfTheGame = $"Решено {StringResultEquasionPresenter(gameTime.Count)}!\nСреднее время решения {TimePresenter(ref dateTime)}.";
            }
            else
            {
                ResultsOfTheGame = "Уравнений решено не было.\nНичего страшного, попробуй еще раз!";
            }
        }
        string StringResultEquasionPresenter(int count)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(count);
            sb.Append(' ');
            if (count % 100 > 20 || count % 100 < 10)
            {
                switch (count % 10)
                {
                    case 1:
                        sb.Append("уравнение");
                        break;
                    case 2:
                    case 3:
                    case 4:
                        sb.Append("уравнения");
                        break;
                    default:
                        sb.Append("уравнений");
                        break;
                }
            }
            else
            {
                sb.Append("уравнений");
            }
            
            return sb.ToString();
        }
        string TimePresenter(ref DateTime dateTime)
        {
            StringBuilder sb = new StringBuilder();
            if (dateTime.Hour != 0)
            {
                sb.Append(dateTime.Hour);
                sb.Append(' ');
                if (dateTime.Hour % 100 > 20 || dateTime.Hour % 100 < 10)
                {
                    switch (dateTime.Hour % 10)
                    {
                        case 1:
                            sb.Append("час");
                            break;
                        case 2:
                        case 3:
                        case 4:
                            sb.Append("часа");
                            break;
                        default:
                            sb.Append("часов");
                            break;
                    }
                }
                else
                {
                    sb.Append("часов");
                }
            }
            if (dateTime.Minute != 0)
            {
                if(sb.Length > 0)
                {
                    sb.Append(' ');
                }
                sb.Append(dateTime.Minute);
                sb.Append(' ');
                if (dateTime.Minute % 100 > 20 || dateTime.Minute % 100 < 10)
                {
                    switch (dateTime.Minute % 10)
                    {
                        case 1:
                            sb.Append("минуа");
                            break;
                        case 2:
                        case 3:
                        case 4:
                            sb.Append("минуы");
                            break;
                        default:
                            sb.Append("минут");
                            break;
                    }
                }
                else
                {
                    sb.Append("минут");
                }
            }
            if(dateTime.Second != 0)
            {
                if (sb.Length > 0)
                {
                    sb.Append(' ');
                }
                sb.Append(dateTime.Second);
                sb.Append(' ');
                if (dateTime.Second % 100 > 20 || dateTime.Second % 100 < 10)
                {
                    switch (dateTime.Second % 10)
                    {
                        case 1:
                            sb.Append("секунда");
                            break;
                        case 2:
                        case 3:
                        case 4:
                            sb.Append("секунды");
                            break;
                        default:
                            sb.Append("секунд");
                            break;
                    }
                }
                else
                {
                    sb.Append("секунд");
                }
            }
            return sb.ToString();
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
            gameTime.Clear();
        }
        void PauseTimer()
        {
            stopwatchGame.Stop();
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
                Button tmpButtonOne = new Button() { Classes = {"GameButtons"} };
                Button tmpButtonTwo = new Button() { Classes = {"GameButtons"} };
                if (list.Length > 1)
                {
                    CreateGameSubFunkForFraction(ref list, ref gameObjects, ref tmpButtonOne, ref tmpButtonTwo, ref i);
                }
                else
                {
                    tmpButtonOne.Content = new TextBlock() { Text = list[0], Classes = { "GameTextBlock" }, VerticalAlignment = Avalonia.Layout.VerticalAlignment.Center };
                    tmpButtonTwo.Content = new TextBlock() { Text = list[0], Classes = { "GameTextBlock" }, VerticalAlignment = Avalonia.Layout.VerticalAlignment.Center };
                }
                ButtonsTextBlock.Add(tmpButtonOne);
                ButtonsTextBox.Add(tmpButtonTwo);
                i++;
            }
            Equasion = ButtonsTextBlock;
            AnswerEquasion = ButtonsTextBox;
            GenerateEquasionIsActive = false;
        }
        void CreateGameSubFunkForFraction(ref string[] list, ref List<object> gameObjects,  ref Button tmpButtonOne, ref Button tmpButtonTwo, ref int i )
        {
            string middle = string.Concat(Enumerable.Repeat("―", Math.Max(list[0].Length, list[1].Length)));
            StackPanel st = new StackPanel() { Classes = { "VerHorAllCenter" } };

            st.Children.Add(new TextBlock() { Text = list[0], Classes = { "GameTextBlock" } });
            st.Children.Add(new TextBlock() { Text = middle, Height = 10, Classes = { "GameTextBlock" }, VerticalAlignment = Avalonia.Layout.VerticalAlignment.Center });
            st.Children.Add(new TextBlock() { Text = list[1], Classes = { "GameTextBlock" } });

            tmpButtonOne.Content = st;

            middle = string.Concat(Enumerable.Repeat("―", Math.Max(list[0].Length, list[1].Length)));
            st = new StackPanel() { Classes = { "VerHorAllCenter" } };

            if (i < gameObjects.Count - 1)
            {
                st.Children.Add(new TextBlock() { Text = list[0], Classes = { "GameTextBlock" } });
            }
            else
            {
                TextBox tb = new TextBox() { Text = "", TextAlignment = TextAlignment.Justify, HorizontalContentAlignment = Avalonia.Layout.HorizontalAlignment.Center };
                tb.TextChanging += TextChanged; //Костыль для корректного отображения горизонтального выравнивания
                st.Children.Add(tb);
            }
            st.Children.Add(new TextBlock() { Text = middle, Height = 10, TextAlignment = TextAlignment.Center, VerticalAlignment = Avalonia.Layout.VerticalAlignment.Center });
            st.Children.Add(new TextBlock() { Text = list[1], Classes = { "GameTextBlock" } });
            //tmpButton.Content = $"{list[0]}{middle}{list[1]}";

            tmpButtonTwo.Content = st;
        }
        //Функция принудительного обновления отрисовки кнопок на ListBox.
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
                                GenerateEquasionIsActive = false;
                                return;
                            }
                        }
                    }
                }
            }
            GenerateEquasionIsActive = true;
            if (stopwatchGame.IsRunning)
            {
                PauseTimer();
                gameTime.Add((int)stopwatchGame.Elapsed.TotalSeconds);
            }

        }


        #endregion



#pragma warning restore CA1822 // Mark members as static
    }
}
