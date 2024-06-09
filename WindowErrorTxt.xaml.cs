using System.Windows;

namespace EEGFilesRename
{
    /// <summary>
    /// Логика взаимодействия для WindowErrorTxt.xaml
    /// </summary>
    public partial class WindowErrorTxt : Window
    {
        public WindowErrorTxt()
        {
            InitializeComponent();
        }

        private void ButtonExit_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
