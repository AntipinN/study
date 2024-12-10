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
using System.Text.RegularExpressions;
using Avalonia.Input;

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
            
            CreateMediumSumGameCommand = new RelayCommand(CreateMediumSumGame);
            CreateMediumSubtractGameCommand = new RelayCommand(CreateMediumSubtractGame);
            CreateMediumMultiplyGameCommand = new RelayCommand(CreateMediumMultiplyGame);
            
            CreateHardSumGameCommand = new RelayCommand(CreateHardSumGame);
            CreateHardSubtractGameCommand = new RelayCommand(CreateHardSubtractGame);
            CreateHardMultiplyGameCommand = new RelayCommand(CreateHardMultiplyGame);

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

        #region Menu Locale
        [ObservableProperty]
        string easy = "Лёгкий";
        [ObservableProperty]
        string medium = "Средний";
        [ObservableProperty]
        string hard = "Высокий";
        [ObservableProperty]
        static string back = "Назад";
        
        #endregion

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

        GameGenerator MediumSumGame = new GameGenerator(new MediumDifficultyEquasionSum(), new FractionEquasionSolver(), new UniversalOPZFormer());
        GameGenerator MediumSubtractGame = new GameGenerator(new MediumDifficultyEquasionSubtraction(), new FractionEquasionSolver(), new UniversalOPZFormer());
        GameGenerator MediumMultiplyGame = new GameGenerator(new MediumDifficultyEquasionMultiply(), new FractionEquasionSolver(), new UniversalOPZFormer());

        GameGenerator HardSumGame = new GameGenerator(new HighDifficultyEquasionSum(), new FractionEquasionSolver(), new UniversalOPZFormer());
        GameGenerator HardSubractGame = new GameGenerator(new HighDifficultyEquasionSubtraction(), new FractionEquasionSolver(), new UniversalOPZFormer());
        GameGenerator HardMultiplyGame = new GameGenerator(new HighDifficultyEquasionMultiply(), new FractionEquasionSolver(), new UniversalOPZFormer());

        GameGenerator CurrentGame;

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
        bool EasySubtractGameCreated = false;
        public ICommand CreateEasyMultiplyGameCommand { get; }
        bool EasyMultiplyGameCreated = false;
        public ICommand CreateMediumSumGameCommand { get; }
        bool MediumSumGameCreated = false;
        public ICommand CreateMediumSubtractGameCommand { get; }
        bool MediumSubtractGameCreated = false;
        public ICommand CreateMediumMultiplyGameCommand { get; }
        bool MediumMultiplyGameCreated = false;
        public ICommand CreateHardSumGameCommand { get; }
        bool HardSumGameCreated = false;
        public ICommand CreateHardSubtractGameCommand { get; }
        bool HardSubtractGameCreated = false;
        public ICommand CreateHardMultiplyGameCommand { get; }
        bool HardMultiplyGameCreated = false;

        public ICommand VerifyAnswerCommand { get; }

        public ICommand EndGameCommand { get; }
        #endregion

        #region Methods Region

        #region Game Creators
        void ContuneGame()
        {
            if (EasySumGameCreated)
            {
                CreateGame(EasySumGame);
            }
            else if (EasySubtractGameCreated)
            {
                CreateGame(EasySubtractGame);
            }
            else if(EasyMultiplyGameCreated)
            {
                CreateGame(EasyMultiplyGame);
            }
            else if (MediumSumGameCreated)
            {
                CreateGame(MediumSumGame);
            }
            else if (MediumSubtractGameCreated)
            {
                CreateGame(MediumSubtractGame);
            }
            else if (MediumMultiplyGameCreated)
            {
                CreateGame(MediumMultiplyGame);
            }
            else if (HardSumGameCreated)
            {
                CreateGame(HardSumGame);
            }
            else if (HardSubtractGameCreated)
            {
                CreateGame(HardSubractGame);
            }
            else if (HardMultiplyGameCreated)
            {
                CreateGame(HardMultiplyGame);
            }
        }
        

        void CreateEasySumGame()
        {
            EasySumGameCreated = true;
            CreateGame(EasySumGame);
        }
        void CreateEasySubtractGame()
        {
            EasySubtractGameCreated = true;
            CreateGame(EasySubtractGame);
        }
        void CreateEasyMultiplyGame()
        {
            EasyMultiplyGameCreated = true;
            CreateGame(EasyMultiplyGame);
        }

        void CreateMediumSumGame()
        {
            MediumSumGameCreated = true;
            CreateGame(MediumSumGame);
        }
        void CreateMediumSubtractGame()
        {
            MediumSubtractGameCreated = true;
            CreateGame(MediumSubtractGame);
        }
        void CreateMediumMultiplyGame()
        {
            MediumMultiplyGameCreated = true;
            CreateGame(MediumMultiplyGame);
        }

        void CreateHardSumGame()
        {
            HardSumGameCreated = true;
            CreateGame(HardSumGame);
        }
        void CreateHardSubtractGame()
        {
            HardSubtractGameCreated = true;
            CreateGame(HardSubractGame);
        }
        void CreateHardMultiplyGame()
        {
            HardMultiplyGameCreated = true;
            CreateGame(HardMultiplyGame);
        }
        #endregion

        void EndGame()
        {
            EasySumGameCreated = false;
            EasySubtractGameCreated = false;
            EasyMultiplyGameCreated = false;
            MediumSumGameCreated = false;
            MediumSubtractGameCreated = false;
            MediumMultiplyGameCreated = false;
            HardSumGameCreated = false;
            HardSubtractGameCreated = false;
            HardMultiplyGameCreated = false;
            IsEqusaisonIncorrect = false;
            SaveResults();
            StopTimer();
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
                            sb.Append("минута");
                            break;
                        case 2:
                        case 3:
                        case 4:
                            sb.Append("минуты");
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
            CurrentGame = Game;
            List<object> ButtonsTextBlock = new List<object>();
            List<object> ButtonsTextBox = new List<object>();
            int i = 0;
            List<string> gameObjects = Game.GetExpression();
            foreach (string o in gameObjects)
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
            StartTimer();
        }
        void CreateGameSubFunkForFraction(ref string[] list, ref List<string> gameObjects,  ref Button tmpButtonOne, ref Button tmpButtonTwo, ref int i )
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
                st.Children.Add(new TextBlock() { Text = middle, Height = 10, TextAlignment = TextAlignment.Center, VerticalAlignment = Avalonia.Layout.VerticalAlignment.Center });
                st.Children.Add(new TextBlock() { Text = list[1], Classes = { "GameTextBlock" } });
            }
            else
            {
                TextBox tb = new TextBox() {Text = "", TextAlignment = TextAlignment.Justify, HorizontalContentAlignment = Avalonia.Layout.HorizontalAlignment.Center};
                tb.TextChanging += TextChanged; //Костыль для корректного отображения горизонтального выравнивания
                tb.KeyDown += TextFilter; //Фильтрация всех символов кроме цифр и обработка клавиши Enter
                st.Children.Add(tb);
                st.Children.Add(new TextBlock() { Text = middle, Height = 10, TextAlignment = TextAlignment.Center, VerticalAlignment = Avalonia.Layout.VerticalAlignment.Center });
                tb = new TextBox() { Text = "", TextAlignment = TextAlignment.Justify, HorizontalContentAlignment = Avalonia.Layout.HorizontalAlignment.Center };
                tb.TextChanging += TextChanged; //Костыль для корректного отображения горизонтального выравнивания
                tb.KeyDown += TextFilter; //Фильтрация всех символов кроме цифр и обработка клавиши Enter
                st.Children.Add(tb);
            }
            //st.Children.Add(new TextBlock() { Text = middle, Height = 10, TextAlignment = TextAlignment.Center, VerticalAlignment = Avalonia.Layout.VerticalAlignment.Center });
            //st.Children.Add(new TextBlock() { Text = list[1], Classes = { "GameTextBlock" } });
            //tmpButton.Content = $"{list[0]}{middle}{list[1]}";

            tmpButtonTwo.Content = st;
        }
        //Функция принудительного обновления отрисовки кнопок на ListBox.
        void TextChanged(object sender, EventArgs e)
        {
            foreach (Control c in ((sender as TextBox).Parent as StackPanel).GetVisualChildren())
            {
                c.InvalidateMeasure();
            }
        }
        void TextFilter(object sender, KeyEventArgs e)
        {
            Regex reg = new Regex(@"\D");
            string s = e.KeySymbol;
            if(e.PhysicalKey == PhysicalKey.Enter)
            {
                VerifyAnswerCommand.Execute(sender);
            } 
            if (s is not null && reg.IsMatch(s))
            {
                e.Handled = true;
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
                            if (j < st.Children.Count - 1)
                            {
                                string Answer = (st.Children[j] as TextBox).Text + "/";
                                if (st.Children[j + 2] is TextBlock)
                                {
                                    Answer += (st.Children[j + 2] as TextBlock).Text;
                                }
                                else
                                {
                                    Answer += (st.Children[j + 2] as TextBox).Text;
                                }
                                string Origin = ((((Equasion[i] as Button).Content as StackPanel).Children[j]) as TextBlock).Text + "/" + ((((Equasion[i] as Button).Content as StackPanel).Children[j + 2]) as TextBlock).Text;
                                if (!CurrentGame.CheckEquasion(Answer, Origin))
                                {
                                    IsEqusaisonIncorrect = true;
                                    GenerateEquasionIsActive = false;
                                    return;
                                }
                                //if ((st.Children[j] as TextBox).Text != ((((Equasion[i]as Button).Content as StackPanel).Children[j]) as TextBlock).Text)
                                //{
                                //    
                                //}
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
