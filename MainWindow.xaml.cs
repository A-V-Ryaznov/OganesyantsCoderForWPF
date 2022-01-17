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
using Microsoft.Win32;

namespace OganesyantsCoderForWPF
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        static public string Scrambler(string str, string textKey)
        {
            byte[] text = new byte[str.Length];
            text = Encoding.Unicode.GetBytes(str);
            byte[] scrambled = new byte[text.Length];
            for (int i = 0; i < text.Length; i++)
            {
                scrambled[i] = (byte)(text[i] ^ int.Parse(textKey));
            }
            return Encoding.Unicode.GetString(scrambled);
        }
        static int GeneratorRandomValue()
        {
            Random rnd = new Random();
            int value = rnd.Next(int.MinValue, int.MaxValue);
            return value;
        }
        public MainWindow()
        {
            InitializeComponent();

        }

        private void ResultButton_Click(object sender, RoutedEventArgs e)
        {
            if (SourseBox.Text!="")
            {

                try
                {
                     ResultBox.Text = Scrambler(SourseBox.Text, KeyBox.Text);
                }
                catch
                {
                     KeyBox.Text = Convert.ToString(GeneratorRandomValue());
                     ResultBox.Text = Scrambler(SourseBox.Text, KeyBox.Text);
                }
            }
            else
            {
                MessageBox.Show("Вы не ввели текст в поле \"Ваш текст\".", "Сообщение");
            }
        }
        private void KeyCreateButton_Click_1(object sender, RoutedEventArgs e)
        {
            KeyBox.Text = Convert.ToString(GeneratorRandomValue());
        }

        private void SourseClearButton_Click(object sender, RoutedEventArgs e)
        {
            SourseBox.Clear();
        }

        private void KeyCopyButton_Click(object sender, RoutedEventArgs e)
        {
            Clipboard.SetText(KeyBox.Text);
        }

        private void ResultCopyButton_Click(object sender, RoutedEventArgs e)
        {
            Clipboard.SetText(ResultBox.Text);
        }

        private void ResultSaveButton_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.Filter = "Результат работы (*.txt)|*.txt";
            if (saveFileDialog1.ShowDialog() == true)
            {
                using (StreamWriter sw = new StreamWriter(saveFileDialog1.OpenFile(), System.Text.Encoding.UTF8))
                {
                    sw.Write(ResultBox.Text);
                    sw.Close();
                }
            }
        }

        private void SourseOpenButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();

            openFileDialog.Filter = "Ваш файл (*.txt)|*.txt";

            if (openFileDialog.ShowDialog() == true)
            {
                FileInfo fileInfo = new FileInfo(openFileDialog.FileName);

                StreamReader reader = new StreamReader(fileInfo.Open(FileMode.Open, FileAccess.Read), Encoding.GetEncoding(1251));

                SourseBox.Text = reader.ReadToEnd();

                reader.Close();
                return;
            }
            }

        private void InstructionButton_Click(object sender, RoutedEventArgs e)
        {
            new InstructionWindow().Show();
        }
    }
}
