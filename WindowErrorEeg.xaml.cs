using System.Windows;

namespace EEGFilesRename
{
    /// <summary>
    /// Логика взаимодействия для WindowErrorEeg.xaml
    /// </summary>
    public partial class WindowErrorEeg : Window
    {
        public WindowErrorEeg()
        {
            InitializeComponent();
        }

        private void ButtonExit_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
