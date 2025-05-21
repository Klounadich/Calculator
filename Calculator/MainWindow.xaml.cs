using System.Data;
using System.Diagnostics;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Calculator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            
            InitializeComponent();
            foreach (UIElement element in Calculator.Children) 
            { 
                if (element is Button)
                {
                   ((Button) element).Click += Button_Click;
                }

                
            }
            this.PreviewKeyDown += Key_Input;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string str =(string)((Button) e.OriginalSource).Content;
            if (str == "AC")
            {
                Text.Text = string.Empty;
            }
            else if (str == "=") 
            { 
                string val = new DataTable().Compute(Text.Text,null).ToString();
                Text.Text = val;
            }
            else
            {
                Text.Text += str;
            }
        }
        private void Key_Input(object sender, KeyEventArgs e)
        {

            bool isDigit = (e.Key >= Key.D0 && e.Key <= Key.D9) ||
                  (e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9);
            try
            {
                if (isDigit && e.IsDown)
                {
                    Text.Text += (e.Key - Key.D0).ToString();
                }
                else if (e.Key == Key.Multiply || e.Key == Key.OemPlus || e.Key == Key.OemMinus || e.Key == Key.Divide)
                {
                    Text.Text += KeyToOperator(e.Key);
                }
                else if (e.Key == Key.Enter)
                {
                    string val = new DataTable().Compute(Text.Text, null).ToString();
                    Text.Text = val;
                }
                else if (e.Key == Key.Delete)
                {
                    Text.Text = string.Empty;
                }
                else if (e.Key == Key.J)
                {
                    Text.Text = "JS ХУЙНЯ";
                }

                else if (e.Key == Key.Back)
                {
                    if (!string.IsNullOrEmpty(Text.Text))
                    {
                        Text.Text = Text.Text.Substring(0, Text.Text.Length - 1);
                    }
                }
                else
                {
                    Debug.WriteLine("Введена не цифра");
                }
            }
            catch (Exception ex) 
            { 
                Debug.WriteLine(ex.ToString());
                Text.Text = "Ошибка ввода . Сбросьте на AC";
            }
            }

        private string KeyToOperator(Key key)
        {
            switch (key)
            {
                case Key.OemPlus: return "+";
                case Key.OemMinus: return "-";
                case Key.Multiply: return "*";
                case Key.Divide: return "/";
                default: return "";
            }
        }
    }
}