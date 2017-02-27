using System.Windows.Controls;
using DesAlgorithm;

namespace DesApplication
{
    class Logger : ILogger
    {
        private readonly TextBox textBox;

        public Logger(TextBox textBox)
        {
            this.textBox = textBox;
        }

        public void Log(string message)
        {
            textBox.Text += $"{message}\n";
        }
    }
}