using System.Windows;

namespace EEGFilesRename
{
    /// <summary>
    /// Логика взаимодействия для WindowErrorFile.xaml
    /// </summary>
    public partial class WindowErrorFile : Window
    {
        public WindowErrorFile()
        {
            InitializeComponent();
        }

        private void ButtonExit_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
