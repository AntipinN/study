using Avalonia.Controls;
using Avalonia.Media;
using AvaloniaApplication3.Models;
using static AvaloniaApplication3.Models.SumOfFractionGame;
using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.Generic;
using System.Windows.Input;
using CommunityToolkit.Mvvm.Input;
using System.Linq;
using System;
using Avalonia.VisualTree;
using System.Diagnostics;
using Avalonia.Threading;
using System.Text;
using System.Text.RegularExpressions;
using Avalonia.Input;
using Avalonia.Layout;
using Avalonia;
using System.IO;
using System.Text.Json;
using Avalonia.Styling;
using Avalonia.Controls.Shapes;
using System.Xml.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text.Json.Serialization;
using Avalonia.Media.Imaging;
using Avalonia.Platform;
using Avalonia.Data;

namespace AvaloniaApplication3.ViewModels
{
    public partial class MainWindowViewModel : ViewModelBase
    {
        public MainWindowViewModel()
        {
            SettingsSerializer();

            SettingsInit();
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

            SetSettings = new RelayCommand(setSettings);
            UpdateVisualValue = new RelayCommand(UpdateValue);
            SetDefaultValuesForSettings = new RelayCommand(SetDefaultSettings);
            GetInstructionCommand = new RelayCommand(GetInstructionFromData);
            GetInstructionFromData();

        }


#pragma warning disable CA1822 // Mark members as static


        #region


        #endregion

        #region Settings
        [ObservableProperty]
        private int interfaceTextSize = 26;
        partial void OnInterfaceTextSizeChanging(int value)
        {
            InterfaceTitleSize = value + 12;
            InterfaceTextSizeChanging = value;
            InstructionImageSize = InstructionImageSize + value * 2;
        }
        [ObservableProperty]
        private int interfaceTitleSize = 26 + 12;
        [ObservableProperty]
        private int soundSize = 5;
        [ObservableProperty]
        private int musicSize = 5;
        [ObservableProperty]
        private int interfaceTextSizeChanging = 26;
        [ObservableProperty]
        private int instructionImageSize = 200;
        [ObservableProperty]
        private int publicInstrImageSize = 200;
        partial void OnPublicInstrImageSizeChanging(int value)
        {
            InstructionImageSize = value + InterfaceTextSize * 2;
        }
        void setSettings()
        {
            InterfaceTextSize = InterfaceTextSizeChanging;
            if (LightTheme)
            {
                ThemeVary = Avalonia.Styling.ThemeVariant.Light;
            }
            else if (DarkTheme)
            {
                ThemeVary = Avalonia.Styling.ThemeVariant.Dark;
            }
            else
            {
                ThemeVary = Avalonia.Styling.ThemeVariant.Default;
            }
            UpdateValue();
            SettingsSerializer();
        }
        void UpdateValue()
        {
            InterfaceTextSizeChanging = InterfaceTextSize;
            if (ThemeVary == ThemeVariant.Light)
            {
                LightTheme = true;
                DefaultTheme = false;
                DarkTheme = false;

            }
            else if (ThemeVary == ThemeVariant.Dark)
            {
                DarkTheme = true;
                LightTheme = false;
                DefaultTheme = false;
            }
            else
            {
                DefaultTheme = true;
                DarkTheme = false;
                LightTheme = false;
            }

        }
        void SetDefaultSettings()
        {
            InterfaceTextSize = 26;
            InterfaceTextSizeChanging = InterfaceTextSize;
            ThemeVary = Avalonia.Styling.ThemeVariant.Default;
            UpdateValue();
            SettingsSerializer();
        }

        [ObservableProperty]
        Avalonia.Styling.ThemeVariant themeVary = ThemeVariant.Default;
        [ObservableProperty]
        bool lightTheme = false;
        [ObservableProperty]
        bool darkTheme = false;
        [ObservableProperty]
        bool defaultTheme = true;
        void SettingsInit()
        {
            try
            {

                SettingsClass settings;
                using (FileStream fs = new FileStream("AllRes/Settings.json", FileMode.Open))
                {
                    settings = JsonSerializer.Deserialize<SettingsClass>(fs);
                }

                InterfaceTextSize = settings.InterfaceTextSize;
                PublicInstrImageSize = settings.ImageSize;
            }
            catch (Exception e)
            {
                ErrorHandlerSaverClass.HandleError(e);
            }

            //ThemeVary = settings.Theme;
            if (ThemeVary == ThemeVariant.Default)
            {
                DefaultTheme = true;
                DarkTheme = false;
                LightTheme = false;
            }
            else if (ThemeVary == ThemeVariant.Light)
            {
                LightTheme = true;
                DefaultTheme = false;
                DarkTheme = false;
            }
            else
            {
                DarkTheme = true;
                LightTheme = false;
                DefaultTheme = false;
            }
        }
        void SettingsSerializer()
        {
            SettingsClass settings = new SettingsClass(InterfaceTextSize, ThemeVary, PublicInstrImageSize);
            using (FileStream fs = new FileStream("AllRes/Settings.json", FileMode.Create))
            {
                JsonSerializer.Serialize(fs, settings);
            }
        }
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

        #region Instruction

        [ObservableProperty]
        private Text_for_presentation instructionText;

        [ObservableProperty]
        private List<TextBlock> textBlockInstruction = new List<TextBlock>();

        [ObservableProperty]
        private List<WrapPanel> viewBlockInstruction = new List<WrapPanel>();
        void GetInstructionFromData()
        {
            if (InstructionText is null)
            {
                InstructionText = new Text_for_presentation();
                for (int i = 0; i < InstructionText.text.Count; i++)
                {

                    TextBlock tb = new TextBlock()
                    {
                        Classes = { "InstructionTB" },
                        Text = InstructionText.text[i]
                    };
                    TextBlockInstruction.Add(tb);

                    WrapPanel s = new WrapPanel() { Orientation = Orientation.Horizontal, Classes = { "VerHorAllCenter" } };

                    foreach (string w in InstructionText.view[i])
                    {
                        Button button;
                        if (w.Contains("avares"))
                        {
                            Bitmap bit = new Bitmap(AssetLoader.Open(new System.Uri(w)));
                            Image image = new Image() { Source = bit, [!Image.HeightProperty] = new Binding("InstructionImageSize") };

                            s.Children.Add(image);
                        }
                        else
                        {


                            Regex regex = new Regex(@"(\(?[a-zA-Z0-9]+((∗|\+|\-)?[a-zA-Z0-9]+)*\)?/\(?[a-zA-Z0-9]+((∗|\+|\-)?[a-zA-Z0-9]+)*\)?)|([0-9]+/[0-9]+)|([<]*[=]+[>]*|([0-9]+))|([;])|([a-zA-Z]/[a-zA-Z])|([\D]+/[\D]+)");
                            if (w.Length > 0)
                            {
                                bool entered = false;
                                foreach (Match contentElement in regex.Matches(w))
                                {
                                    Thickness margin = new Thickness(1, 0, 1, 0);
                                    button = new Button() { Classes = { "GameButtons" }, Margin = margin };
                                    StackPanel sp = new StackPanel();
                                    if (contentElement.ToString().Contains('/'))
                                    {
                                        string[] wm = contentElement.ToString().Split(new char[] { '/', '(', ')' }, StringSplitOptions.RemoveEmptyEntries);
                                        string middle = string.Concat(Enumerable.Repeat("―", Math.Max(wm[0].Length, wm[1].Length)));
                                        sp.Children.Add(new TextBlock() { Text = wm[0], Classes = { "GameTextBlock" } });
                                        sp.Children.Add(new TextBlock() { Text = middle, Height = 10, TextAlignment = TextAlignment.Center, VerticalAlignment = Avalonia.Layout.VerticalAlignment.Center });
                                        sp.Children.Add(new TextBlock() { Text = wm[1], Classes = { "GameTextBlock" } });
                                        button.Content = sp;
                                    }
                                    else
                                    {
                                        button.Content = new TextBlock() { Text = contentElement.ToString(), Classes = { "GameTextBlock" }, VerticalAlignment = VerticalAlignment.Center };
                                    }
                                    s.Children.Add(button);
                                    entered = true;
                                }
                                if (!entered)
                                {
                                    button = new Button() { Classes = { "GameButtons" } };
                                    button.Content = new TextBlock() { Text = new string(w), Classes = { "GameTextBlock" }, VerticalAlignment = VerticalAlignment.Center };
                                    s.Children.Add(button);

                                }
                            }
                        }
                    }
                    ViewBlockInstruction.Add(s);
                    //Thread.Sleep(50);
                }
            }
        }

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


        TextBox Numerator;
        TextBox Denominator;
        [ObservableProperty]
        string errorMessage = "";
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

        public ICommand GetInstructionCommand { get; }

        public ICommand SetSettings { get; }
        public ICommand UpdateVisualValue { get; }

        public ICommand SetDefaultValuesForSettings { get; }
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
            else if (EasyMultiplyGameCreated)
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
            foreach (int i in gameTime)
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
                if (sb.Length > 0)
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
            if (dateTime.Second != 0)
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
            Wrapper_fractions gameObjects = Game.GetExpression();
            foreach (string o in gameObjects.term)
            {
                Button tmpButtonOne = new Button() { Classes = { "GameButtons" } };
                Button tmpButtonTwo = new Button() { Classes = { "GameButtons" } };
                Fraction currentFraction;
                if (gameObjects.fraction_substitutions.TryGetValue(o, out currentFraction))
                {
                    CreateGameSubFunkForFraction(ref currentFraction, ref gameObjects.term, ref tmpButtonOne, ref tmpButtonTwo, ref i);
                }
                else
                {
                    tmpButtonOne.Content = new TextBlock() { Text = o, Classes = { "GameTextBlock" }, VerticalAlignment = Avalonia.Layout.VerticalAlignment.Center };
                    tmpButtonTwo.Content = new TextBlock() { Text = o, Classes = { "GameTextBlock" }, VerticalAlignment = Avalonia.Layout.VerticalAlignment.Center };
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
        void CreateGameSubFunkForFraction(ref Fraction currentFraction, ref List<string> gameObjects, ref Button tmpButtonOne, ref Button tmpButtonTwo, ref int i)
        {
            string numerator = currentFraction.Numerator.ToString();
            string denominator = currentFraction.Denominator.ToString();
            string middle = string.Concat(Enumerable.Repeat("―", Math.Max(numerator.Length, denominator.Length)));
            StackPanel st = new StackPanel() { Classes = { "VerHorAllCenter" } };

            st.Children.Add(new TextBlock() { Text = numerator, Classes = { "GameTextBlock" } });
            st.Children.Add(new TextBlock() { Text = middle, Height = 10, Classes = { "GameTextBlock" }, VerticalAlignment = Avalonia.Layout.VerticalAlignment.Center });
            st.Children.Add(new TextBlock() { Text = denominator, Classes = { "GameTextBlock" } });

            tmpButtonOne.Content = st;

            middle = string.Concat(Enumerable.Repeat("―", Math.Max(numerator.Length, denominator.Length)));
            st = new StackPanel() { Classes = { "VerHorAllCenter" } };

            if (i < gameObjects.Count - 1)
            {
                st.Children.Add(new TextBlock() { Text = numerator, Classes = { "GameTextBlock" } });
                st.Children.Add(new TextBlock() { Text = middle, Height = 10, TextAlignment = TextAlignment.Center, VerticalAlignment = Avalonia.Layout.VerticalAlignment.Center });
                st.Children.Add(new TextBlock() { Text = denominator, Classes = { "GameTextBlock" } });
            }
            else
            {
                TextBox tb = new TextBox() { Text = "", TextAlignment = TextAlignment.Justify, HorizontalContentAlignment = Avalonia.Layout.HorizontalAlignment.Center, MaxLength = 14 };
                tb.TextChanging += TextChanged; //Костыль для корректного отображения горизонтального выравнивания
                tb.KeyDown += TextFilter; //Фильтрация всех символов кроме цифр и обработка клавиши Enter
                tb.PastingFromClipboard += PastingIntercept;
                st.Children.Add(tb);

                Numerator = tb; //сохраняем TextBox числителя для отслеживания

                st.Children.Add(new TextBlock() { Text = middle, Height = 10, TextAlignment = TextAlignment.Center, VerticalAlignment = Avalonia.Layout.VerticalAlignment.Center, });
                tb = new TextBox() { Text = "", TextAlignment = TextAlignment.Justify, HorizontalContentAlignment = Avalonia.Layout.HorizontalAlignment.Center, MaxLength = 14 };
                tb.TextChanging += TextChanged; //Костыль для корректного отображения горизонтального выравнивания
                tb.KeyDown += TextFilter; //Фильтрация всех символов кроме цифр и обработка клавиши Enter
                tb.PastingFromClipboard += PastingIntercept;
                st.Children.Add(tb);

                Denominator = tb; //Сохраняем TextBox знаменателя для отслеживания
            }

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
            if (e.PhysicalKey == PhysicalKey.Enter || e.PhysicalKey == PhysicalKey.NumPadEnter)
            {
                VerifyAnswerCommand.Execute(sender);
            }
            if (s is not null && reg.IsMatch(s))
            {
                e.Handled = true;
            }
        }
        void PastingIntercept(object sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            e.Handled = true;
        }
        public void VerifyAnswer()
        {
            IsEqusaisonIncorrect = false;

            Regex checkForCorrectLine = new Regex(@"[1-9]+[0-9]*");
            Match denominatorCorrect = checkForCorrectLine.Match(Denominator.Text);
            ErrorMessage = "";
            if (Numerator is not null && Numerator.Text is not null && Numerator.Text.Length > 1 && Numerator.Text[0] == '0')
            {
                ErrorMessage += "Числитель не может начинаться с нуля!";
            }
            if (denominatorCorrect.Length < Denominator.Text.Length)
            {
                if (errorMessage.Length > 0)
                {
                    ErrorMessage += '\n';
                }
                ErrorMessage += "Знаменатель не может быть нулём или начинаться с него!";
            }
            if (ErrorMessage.Length > 0)
            {
                IsEqusaisonIncorrect = true;
                GenerateEquasionIsActive = false;
                return;
            }
            string Answer = Numerator.Text + "/" + Denominator.Text;
            if (!CurrentGame.CheckEquasion(Answer))
            {
                IsEqusaisonIncorrect = true;
                GenerateEquasionIsActive = false;
                return;
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
