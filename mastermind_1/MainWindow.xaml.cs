using Microsoft.VisualBasic;
using System.ComponentModel;
using System.Text;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace mastermind_1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DispatcherTimer timer = new DispatcherTimer();
        int timeRemaining = 10; 
        int attempts = 1;
        string[] chosenColor = new string[4];
        string[] allColors = { "white", "green", "blue", "red", "orange", "yellow" };
        int score;
        private string playerName;
        public MainWindow()
        {
            
            InitializeComponent();

            this.KeyDown += (s, e) =>
            {
                if (e.Key == Key.F12 && Keyboard.Modifiers == ModifierKeys.Control)
                {
                    ToggleDebug();
                }
            };

            timer.Tick += Timer_Tick;
                timer.Interval = new TimeSpan(0, 0, 1);

            GenerateCode();



            codeTextBox.Text = $"{string.Join(", ", chosenColor)}";
            FillComboBoxes(ref allColors);
            
        }

private void GenerateFeedback(string[] userColors) {

    int correctposition = 0;
    int correctColor = 0;

    Dictionary<string, int> colorCounts = new Dictionary<string, int>();

    foreach(var color in chosenColor)
    {
        if (colorCounts.ContainsKey(color))
        {
            colorCounts[color]++;
        }
        else
        {
            colorCounts[color] = 1;
        }

    }
}
        private void UpdateHistory(string[] userColors, string feedback ) {

        string attemptInfo = $"attempts: {attempts}, {string.Join(", ", userColors)} - feedback: {feedback}";
        }
        
        private void GenerateCode()
        {

            Random rnd = new Random();
            for (int i = 0; i < 4; i++)
            {
                int color = rnd.Next(allColors.Length);
                chosenColor[i] = allColors[color];
            }

            StartCountDown(); 
        }
        //stop countdown timer 
        private void StopCountDown()
        {
            timer.Stop();

        }
        private void Timer_Tick(object sender, EventArgs e)
        {
            timeRemaining--;
            timerLabel.Content = $"Tijd: {timeRemaining}";
           
            if (timeRemaining <= 0)
            {
                StopCountDown(); 
                HandleAttempt(); 
            }
        }
        //start countdown timer from 10 seconds and if the player click "check code" start new attempts
        private void StartCountDown()
        {
            timeRemaining = 10; 
            timer.Start(); 
          
        }
        private void UpdateTimerLabel()
        {
            timerLabel.Content = $"Tijd: {timeRemaining}";
        }
        private void ChoosingLabelColors(object sender, RoutedEventArgs e)
        {
        
            //
            ComboBox comboBox = (ComboBox)sender;
            if (comboBox.SelectedItem != null)
            {
                string selectedColor = comboBox.SelectedItem.ToString();
               
                SolidColorBrush colorBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString(selectedColor));

                switch (comboBox.Name)
                {
                    case "firstComboBox":
                        firstLabel.Background = colorBrush;
/*                        firstLabel.Content = selectedColor;
*/
                        break;
                    case "secondComboBox":
                        secondLabel.Background = colorBrush;
/*                        secondLabel.Content = selectedColor;
*/                        break;
                    case "thirdComboBox":
                        thirdLabel.Background = colorBrush;
/*                        thirdLabel.Content = selectedColor;
*/                        break;
                    case "fourthComboBox":
                        fourthLabel.Background = colorBrush;
/*                        fourthLabel.Content = selectedColor;
*/                        break;
                }
            }

        }
        private void HandleAttempt()
        {
            
           
            if (attempts > 10) 
            {
                
                StopCountDown(); 
                return;
            }
            attempts++;
            Mastermind.Title = $"Poging {attempts}";
            StartCountDown(); 
        }
        private void FillComboBoxes(ref string[] items)
        {
            foreach (var item in items)
            {
                firstComboBox.Items.Add(item);
                secondComboBox.Items.Add(item);
                thirdComboBox.Items.Add(item);
                fourthComboBox.Items.Add(item);
            }

          
        }
        //make the textbox visible or unvisible by pressing ctrl F12
        private void ToggleDebug()
        {
          
            if(codeTextBox.Visibility == Visibility.Hidden)
            {
                codeTextBox.Visibility = Visibility.Visible;
            }
            else
            {
                codeTextBox.Visibility = Visibility.Hidden;

            }
        }


        private void SetBorderColor(int index, Color color)
        {
            SolidColorBrush borderBrush = new SolidColorBrush(color);
            switch (index)
            {
                case 0:
                    firstLabel.BorderBrush = borderBrush;
                    firstLabel.BorderThickness = new Thickness(2);
                    break;
                case 1:
                    secondLabel.BorderBrush = borderBrush;
                    secondLabel.BorderThickness = new Thickness(2);
                    break;
                case 2:
                    thirdLabel.BorderBrush = borderBrush;
                    thirdLabel.BorderThickness = new Thickness(2);
                    break;
                case 3:
                    fourthLabel.BorderBrush = borderBrush;
                    fourthLabel.BorderThickness = new Thickness(2);
                    break;
            }
        }
        private void controlButton_Click(object sender, RoutedEventArgs e)
        {
            HandleAttempt();



            string[] userPickedColors =  {
                                 firstComboBox.SelectedItem.ToString(),
                                 secondComboBox.SelectedItem.ToString(),
                                 thirdComboBox.SelectedItem.ToString(),
                                 fourthComboBox.SelectedItem.ToString() 
            };  

            for (int i = 0; i< userPickedColors.Length; i++)
            {
                if (userPickedColors[i] == chosenColor[i])
                {
                    SetBorderColor(i, Colors.DarkRed);

                }
                else if(chosenColor.Contains(userPickedColors[i])) {
                   
                    SetBorderColor(i, Colors.Wheat);

                }
                else
                {
                    SetBorderColor(i, Colors.Transparent);

                }
            }

        }

      
        private string StartGame()
        {
            string playerName = string.Empty;
            while (string.IsNullOrWhiteSpace(playerName)) {
                
                playerName = Interaction.InputBox("Wat is your name?", "Name");
               
                
                if (string.IsNullOrWhiteSpace(playerName))
                {
                    MessageBox.Show("Naam mag niet leeg zijn", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            
            return playerName;
        }
        private void NieuwSpel_Click(object sender, RoutedEventArgs e)
        {
            string playerName = StartGame();
            attempts = 1;
            GenerateCode();
            }
        private void StopGame(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Game is finished!", "FINISH", MessageBoxButton.OK);
        }
        private void HighScore_Click(object sender, RoutedEventArgs e)
        {
            
            int[] highestScores = new int[15];

            for (int i = 0; i < highestScores.Length; i++) {
                if (score > highestScores[i])
                {
                    highestScores[i] = score;
                }
            }
            MessageBox.Show($"{userName} - {attempts} pogingen - {highestScores}/100 ");

        }

        private void Afsluiten_Click(object sender, RoutedEventArgs e)
        {
            this.Close();

        }

        private void AantalPogingen_Click(object sender, KeyEventArgs e)
        {
            int attemptsNumber;
            attemptsNumber = int.Parse(Interaction.InputBox("How many tries do you want for the game?", "poging"));
            if (attemptsNumber >= 3 || attemptsNumber <= 20)
            {
                if(e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9 )
                {
                    e.Handled = false;
                }
                else if (e.Key >= Key.D0 && e.Key <= Key.D9 && e.KeyboardDevice.Modifiers == ModifierKeys.Shift) {
                    e.Handled = false;
                }
                else
                {
                    e.Handled= true;
                }
            }
            else
            {
                MessageBox.Show("inter a number between 3 and 20", "attempts", MessageBoxButton.OK);
            }
            while (attempts == attemptsNumber)
            {
               // StopGame();
            };
        }
    }
}