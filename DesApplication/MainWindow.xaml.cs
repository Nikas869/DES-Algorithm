using System.Windows;
using DesAlgorithm;

namespace DesApplication
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string currentKey;
        public string CurrentKey
        {
            get { return currentKey; }
            set { KeyTextBox.Text = currentKey = value; }
        }

        public MainWindow()
        {
            InitializeComponent();
            CurrentKey = Des.GenerateKey();
        }
    }
}