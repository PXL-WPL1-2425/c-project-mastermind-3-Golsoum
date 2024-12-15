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
        private string[] highScores = new string[15];
        private int highScoreCount = 0;
        private int maxAttempts = 10;
        private List<string> playerNames = new List<string>();
        private int currentPlayerIndex = 0;
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

        private void UpdateTitle()
        {
            Mastermind.Title = $"player: {playerName}, attempt: {attempts} from {maxAttempts}";
        }
        private void AddHighScore(string playerName, int attempts)
        {
            string highScoreEntry = $"{playerName} - {attempts} pogingen - {score} /100";

            if (highScoreCount < 15)
            {
                highScores[highScoreCount] = highScoreEntry;
                highScoreCount++;
            }
            else
            {
                for(int i =1; i< highScores.Length; i++)
                {
                    highScores[i - 1] = highScores[i];
                   
                }
                highScores[highScores.Length - 1] = highScoreEntry;
            }
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
            
           
            if (attempts > maxAttempts) 
            {
                
                StopCountDown();
                StopGame();
                MessageBox.Show("You reached maximum number of attempts", "Game ended", MessageBoxButton.OK);
                return;
            }
            attempts++;
            UpdateTitle();
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

      
        private List<string> StartGame()
        {
            string inputName;
            string response = string.Empty;

            do
            {
                inputName = Microsoft.VisualBasic.Interaction.InputBox("Add a name:", "Add name");

                if (string.IsNullOrWhiteSpace(inputName))
                {
                    MessageBox.Show("Name should not be empty", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                    continue;

                }

                playerNames.Add(inputName);

                response = Microsoft.VisualBasic.Interaction.InputBox("Do you want to add a new name? yes/no", "Add New Name");
            } while (!String.IsNullOrWhiteSpace(response) && response.Equals("yes", StringComparison.OrdinalIgnoreCase));

            MessageBox.Show($"registered names: {string.Join(", ", playerNames).ToUpper()}", "PLAYERS NAME", MessageBoxButton.OK,MessageBoxImage.Information);
            
            
            return playerNames;
        }
        private void NieuwSpel_Click(object sender, RoutedEventArgs e)
        {
             playerNames = StartGame();
            attempts = 1;
            currentPlayerIndex = 0;
            if (playerNames.Count > 0) { 
            
            playerName = playerNames[currentPlayerIndex];
                MessageBox.Show($"It's {playerName} turn", "Upate name",MessageBoxButton.OK);
            }
            else
            {
                MessageBox.Show("No name registered!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            GenerateCode();
            UpdateTitle();
            }
        private void StopGame()
        {
            AddHighScore(playerName, attempts);
            MessageBox.Show("Game is finished!", "FINISH", MessageBoxButton.OK, MessageBoxImage.Information);
        }
        private void HighScore_Click(object sender, RoutedEventArgs e)
        {
           StringBuilder highScoreList = new StringBuilder("Highscores:\n");
            for(int i = 0; i < highScoreCount; i ++)
            {
                highScoreList.AppendLine(highScores[i]);
            }
            
            
            MessageBox.Show(highScoreList.ToString(), "Highscores", MessageBoxButton.OK, MessageBoxImage.Information);

        }

        private void Afsluiten_Click(object sender, RoutedEventArgs e)
        {
            this.Close();

        }

        private void AantalPogingen_Click(object sender, RoutedEventArgs e)
        {
            string input;
            int attemptsNumber;
            do
            {
               input = Interaction.InputBox("How many tries do you want for the game?", "poging", maxAttempts.ToString());

                if(!int.TryParse(input, out attemptsNumber) || attemptsNumber < 3 || attemptsNumber > 20 )
                {
                    MessageBox.Show("Add a number between 3 and 20", "Invalid number", MessageBoxButton.OK);
                }
            } while(attemptsNumber > 3 || attemptsNumber > 20);
            maxAttempts = attemptsNumber;
        }
    }
}