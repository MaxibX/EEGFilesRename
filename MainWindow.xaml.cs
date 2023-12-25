using Microsoft.Win32;
using System.Diagnostics;
using System.IO;
using System.Reflection.PortableExecutable;
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

namespace EEGFilesRename
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        private static string inFilePathVmrk;
        private static string inFilePathVhdr;
        private static string inFilePathEeg;
        private static string inDirPath;
        private static string eegExt;
        private static string outFilePathVmrk;
        private static string outFilePathVhdr;
        private static string outFilePathEeg;
        private void ButtonFile_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog OpenFileDialog1 = new OpenFileDialog();

            OpenFileDialog1.ShowDialog();
            string inFilePath = OpenFileDialog1.FileName;
            if (System.IO.Path.GetDirectoryName(inFilePath) == null || System.IO.Path.GetDirectoryName(inFilePath) == string.Empty)
            {
                return;
            }
            else
            {
                inDirPath = System.IO.Path.GetDirectoryName(inFilePath);
            }
            string inFileExt = System.IO.Path.GetExtension(inFilePath);
            if (inFileExt == ".vmrk" || inFileExt == ".vhdr" || inFileExt == ".eeg" || inFileExt == ".avg" || inFileExt == ".seg")
            {
                inFilePathVmrk = inDirPath + "\\" + System.IO.Path.GetFileNameWithoutExtension(inFilePath) + ".vmrk";
                inFilePathVhdr = inDirPath + "\\" + System.IO.Path.GetFileNameWithoutExtension(inFilePath) + ".vhdr";
                textFile.Text = System.IO.Path.GetFileName(inFilePath);
            }
            else
            {
                WindowErrorFile windowErrorFile = new WindowErrorFile();
                windowErrorFile.Owner = this;
                windowErrorFile.Show();
                return;
            }
        }

        private void ButtonStart_Click(object sender, RoutedEventArgs e)
        {
            if (txtName.Text == "")
            {
                WindowErrorTxt windowErrorTxt = new WindowErrorTxt();
                windowErrorTxt.Owner = this;
                windowErrorTxt.Show();
                return;
            }
            if (!System.IO.Path.Exists(inFilePathVmrk))
            {
                WindowErrorVmrk windowErrorVmrk = new WindowErrorVmrk();
                windowErrorVmrk.Owner = this;
                windowErrorVmrk.Show();
                return;
            }
            if (!System.IO.Path.Exists(inFilePathVhdr))
            {
                WindowErrorVhdr windowErrorVhdr = new WindowErrorVhdr();
                windowErrorVhdr.Owner = this;
                windowErrorVhdr.Show();
                return;
            }
            string outDirPath = inDirPath; // to do
            outFilePathVmrk = outDirPath + "\\" + txtName.Text + ".vmrk";
            outFilePathVhdr = outDirPath + "\\" + txtName.Text + ".vhdr";
            
            StreamReader readerVmrk = new StreamReader(inFilePathVmrk);
            StreamReader readerVhdr = new StreamReader(inFilePathVhdr);
            StreamWriter writerVmrk = new StreamWriter(outFilePathVmrk, false);
            StreamWriter writerVhdr = new StreamWriter(outFilePathVhdr, false);
            string line1;
            while (!readerVmrk.EndOfStream)
            {
                string line = readerVmrk.ReadLine();
                if (line.Contains("DataFile"))
                {
                    eegExt = "." + line.Split('.')[line.Split('.').Length - 1];
                    line1 = "DataFile=" + txtName.Text + "." + eegExt;
                    writerVmrk.WriteLine(line1);
                }
                else
                {
                    writerVmrk.WriteLine(line);
                }
            }
            while (!readerVhdr.EndOfStream)
            {
                string line = readerVhdr.ReadLine();
                if (line.Contains("DataFile"))
                {
                    line1 = "DataFile=" + txtName.Text + "." + line.Split('.')[line.Split('.').Length - 1];
                    writerVhdr.WriteLine(line1);
                }
                else if (line.Contains("MarkerFile"))
                {
                    line1 = "MarkerFile=" + txtName.Text + ".vmrk";
                    writerVhdr.WriteLine(line1);
                }
                else
                {
                    writerVhdr.WriteLine(line);
                }
            }
            writerVmrk.Close();
            writerVhdr.Close();
            inFilePathEeg = inDirPath + "\\" + System.IO.Path.GetFileNameWithoutExtension(inFilePathVmrk) + eegExt;
            outFilePathEeg = outDirPath + "\\" + txtName.Text + eegExt;
            try
            {
                File.Move(inFilePathEeg, outFilePathEeg);
            }
            catch
            {
                WindowErrorEeg windowErrorEeg = new WindowErrorEeg();
                windowErrorEeg.Owner = this;
                windowErrorEeg.Show();
                return;
            }
            WindowSuccess windowSuccess = new WindowSuccess();
            windowSuccess.Owner = this;
            windowSuccess.Show();
        }

        private void ButtonInfo_Click(object sender, RoutedEventArgs e)
        {
            WindowInfo windowInfo = new WindowInfo();
            windowInfo.Owner = this;
            windowInfo.Show();
        }
    }
}