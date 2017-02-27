using System.Windows;
using DesAlgorithm;

namespace DesApplication
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly Des des;
        private string currentKey;
        public string CurrentKey
        {
            get { return currentKey; }
            set { KeyTextBox.Text = currentKey = value; }
        }

        public MainWindow()
        {
            InitializeComponent();
            ILogger logger = new Logger(LogTextBox);
            des = new Des(logger);
            CurrentKey = des.GenerateKey();
        }

        private void GenerateKeyButton_Click(object sender, RoutedEventArgs e)
        {
            CurrentKey = des.GenerateKey();
        }
    }
}