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

namespace u3_Summative_Brushey_WordCutter
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            System.IO.StreamReader streamReader = new System.IO.StreamReader("../../../../toomanywords.txt");
            System.IO.StreamWriter streamWriter = new System.IO.StreamWriter("../../../../words.txt");

            try
            {
                while (!streamReader.EndOfStream)
                {
                    string word = streamReader.ReadLine();

                    if (word.Length < 8 && word.Length > 1)
                    {
                        streamWriter.Write(word + "\r" + "\n");
                    }
                    else if (word.Length < 1 && word.Contains("I") || word.Contains("A"))
                    {
                        streamWriter.Write(word + "\r" + "\n");
                    }
                }
                streamWriter.Flush();
                streamWriter.Close();
                streamReader.Close();
            }

            catch
            {

            }
        }
    }
}
