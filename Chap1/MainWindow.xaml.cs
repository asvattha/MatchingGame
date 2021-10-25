using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

namespace Chap1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DispatcherTimer timer = new DispatcherTimer();
        int tenthsOfSecondsElapsed;
        int matchesFound;


        TextBlock lastTextBlockClicked;
        bool findingMatch = false;
        public MainWindow()
        {
            InitializeComponent();

            addTextBlock();

            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += Timer_Tick;
            SetUpGame();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            tenthsOfSecondsElapsed++;
            timeTextBlock.Text = (tenthsOfSecondsElapsed).ToString("0s");
            if(matchesFound == 8)
            {
                timer.Stop();
                timeTextBlock.Text = timeTextBlock.Text + " - Play again?";
            }
        }

        private void addTextBlock()
        {
           for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    TextBlock t = new TextBlock();
                    t.Text = "?";
                    t.HorizontalAlignment = HorizontalAlignment.Center;
                    t.VerticalAlignment = VerticalAlignment.Center;
                    t.TextWrapping = TextWrapping.NoWrap;
                    t.FontSize = 36;
                    Grid.SetRow(t, i);
                    Grid.SetColumn(t, j);
                    MainGrid.Children.Add(t);
                    t.MouseDown += new MouseButtonEventHandler(TextBlock_MouseDown);
                }
            }
        }

        private void SetUpGame()
        {
            List<string> animalEmoji = new List<string>()
            {
                "🎅", "🎅",
                "🙈", "🙈",
                "🦮", "🦮",
                "🐎", "🐎",
                "🐽", "🐽",
                "🦘", "🦘",
                "🦜", "🦜",
                "🦋", "🦋",
            };

            Random random = new Random();

            foreach(TextBlock t in MainGrid.Children.OfType<TextBlock>())
            {
                if(t.Name != "timeTextBlock")
                {
                    t.Visibility = Visibility.Visible;
                    int index = random.Next(animalEmoji.Count);
                    string nextEmoji = animalEmoji[index];
                    t.Text = nextEmoji;
                    animalEmoji.RemoveAt(index);
                }   
            }

            timer.Start();
            tenthsOfSecondsElapsed = 0;
            matchesFound = 0;

        }

        private void TextBlock_MouseDown(object sender, MouseButtonEventArgs e)
        {
            TextBlock t = sender as TextBlock;
            if(findingMatch == false)
            {
                t.Visibility = Visibility.Hidden;
                lastTextBlockClicked = t;
                findingMatch = true;
            }
            else if (t.Text == lastTextBlockClicked.Text)
            {
                matchesFound++;
                t.Visibility = Visibility.Hidden;
                findingMatch = false;
            }
            else
            {
                lastTextBlockClicked.Visibility = Visibility.Visible;
                findingMatch = false;
            }
        }

        private void TimeTextBlock_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if(matchesFound == 8)
            {
                SetUpGame();
            }
        }
    }
}
