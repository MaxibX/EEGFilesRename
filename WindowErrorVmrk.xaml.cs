using System.Windows;

namespace EEGFilesRename
{
    /// <summary>
    /// Логика взаимодействия для WindowErrorVmrk.xaml
    /// </summary>
    public partial class WindowErrorVmrk : Window
    {
        public WindowErrorVmrk()
        {
            InitializeComponent();
        }

        private void ButtonExit_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
