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
using System.IO;
namespace u3_Summative_Brushey_Scrabble
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //defines variables that will be used throughout the program
        string letters;
        //the 7 character string that will be used to spell words
        string remaining_letters;
        // the 1-7 character string containing the characters not yet used in each word
        int matches;
        //the number of characters shared by the letters string and word being tested
        int non_matches;
        //the number of blank spaces representing letters in the tested word
        int number_of_blanks;
        //the number of blanks in the letters string
        string word;
        //the word being tested
        string alphabet = "abcdefghijklmnopqrstuvwxyz";
        //the letters not used by the letters string

        public MainWindow()
        {
            InitializeComponent();
            ScrabbleGame sg = new ScrabbleGame();
            //MessageBox.Show(sg.drawInitialTiles());

            StreamReader streamReader = new StreamReader("../../../../words.txt");
            StreamWriter streamWriter = new StreamWriter("Output.txt");

            letters = sg.drawInitialTiles().ToLower();

            int position = 0;
            for (int i = 0; i < letters.Length; i++)
            {
                if (alphabet.Contains(letters[i]))
                {
                    position = alphabet.IndexOf(letters[i]);
                    alphabet = alphabet.Remove(position, 1);
                }
            }


            try
            {
                while (!streamReader.EndOfStream)
                {
                    if (!letters.Contains(" "))
                    {
                        word = streamReader.ReadLine().ToLower();
                        matches = 0;
                        remaining_letters = letters;

                        for (int i = 0; i < word.Length; i++)
                        {
                            if (alphabet.Contains(word[i]))
                            {
                                break;
                            }
                            else if (remaining_letters.Contains(word[i]))
                            {
                                matches++;
                                position = remaining_letters.IndexOf(word[i]);
                                remaining_letters = remaining_letters.Remove(position, 1);
                            }
                        }
                        if (matches == word.Length)
                        {
                            if (word.Length == 1 && word.Contains('a') || word.Contains('i'))
                            {
                                streamWriter.Write(word + "\r" + "\n");
                            }
                            else if (word.Length > 1)
                            {
                                streamWriter.Write(word + "\r" + "\n");
                            }
                        }
                    }
                    else if (letters.Contains(" "))
                    {
                        for (int i = 0; i < letters.Length; i++)
                        {
                            if (letters[i] == ' ')
                            {
                                number_of_blanks++;
                            }
                        }
                        word = streamReader.ReadLine().ToLower();
                        matches = 0;
                        non_matches = 0;
                        remaining_letters = letters;
                        number_of_blanks = 0;
                        Console.WriteLine("Word " + word);

                        for (int i = 0; i < word.Length; i++)
                        {
                            if (remaining_letters.Contains(word[i]))
                            {
                                matches++;
                                position = remaining_letters.IndexOf(word[i]);
                                remaining_letters = remaining_letters.Remove(position, 1);
                            }
                            else if (!remaining_letters.Contains(word[i]))
                            {
                                if (remaining_letters.Contains(' '))
                                {
                                    non_matches++;
                                    position = remaining_letters.IndexOf(' ');
                                    remaining_letters = remaining_letters.Remove(position, 1);
                                }
                            }
                        }
                        if (matches + non_matches == word.Length)
                        {

                            if (word.Length == 1 && word.Contains('a') || word.Contains('i'))
                            {
                                streamWriter.Write(word + "\r" + "\n");
                            }
                            else if (word.Length > 1)
                            {
                                streamWriter.Write(word + "\r" + "\n");
                            }
                        }
                    }
                }
                streamWriter.Flush();
                streamWriter.Close();
                streamReader.Close();


            }
            catch
            {

            }

            MessageBox.Show("Your letters are: " + letters);
            System.Diagnostics.Process.Start("Output.txt");
            Application.Current.Shutdown();
        }
    }
}
