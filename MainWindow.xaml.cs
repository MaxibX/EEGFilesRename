using Microsoft.Win32;
using System.IO;
using System.Windows;

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
        private static string? inFilePathVmrk = "";
        private static string? inFilePathVhdr = "";
        private static string? inFilePathEeg = "";
        private static string? inFile = "";
        private static string? inDirPath = "";
        private static string? eegExt = "";
        private static string? outFilePathVmrk = "";
        private static string? outFilePathVhdr = "";
        private static string? outFilePathEeg = "";
        private void ButtonFile_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog OpenFileDialog1 = new OpenFileDialog();

            OpenFileDialog1.ShowDialog();
            string inFilePath = OpenFileDialog1.FileName;
            if (Path.GetDirectoryName(inFilePath) == null || Path.GetDirectoryName(inFilePath) == string.Empty)
            {
                return;
            }
            else
            {
                inDirPath = Path.GetDirectoryName(inFilePath);
            }
            string inFileExt = Path.GetExtension(inFilePath);
            if (inFileExt == ".vmrk" || inFileExt == ".vhdr" || inFileExt == ".eeg" || inFileExt == ".avg" || inFileExt == ".seg")
            {
                inFile = Path.GetFileNameWithoutExtension(inFilePath);
                inFilePathVmrk = inDirPath + "\\" + inFile + ".vmrk";
                inFilePathVhdr = inDirPath + "\\" + inFile + ".vhdr";
                textFile.Text = Path.GetFileName(inFilePath);
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
            if (!Path.Exists(inFilePathVmrk))
            {
                WindowErrorVmrk windowErrorVmrk = new WindowErrorVmrk();
                windowErrorVmrk.Owner = this;
                windowErrorVmrk.Show();
                return;
            }
            if (!Path.Exists(inFilePathVhdr))
            {
                WindowErrorVhdr windowErrorVhdr = new WindowErrorVhdr();
                windowErrorVhdr.Owner = this;
                windowErrorVhdr.Show();
                return;
            }
            outFilePathVmrk = inDirPath + "\\" + txtName.Text + ".vmrk";
            outFilePathVhdr = inDirPath + "\\" + txtName.Text + ".vhdr";

            StreamReader readerVmrk = new StreamReader(inFilePathVmrk);
            StreamReader readerVhdr = new StreamReader(inFilePathVhdr);
            StreamWriter writerVmrk = new StreamWriter(outFilePathVmrk, false);
            StreamWriter writerVhdr = new StreamWriter(outFilePathVhdr, false);
            string? line, line1;
            while (!readerVmrk.EndOfStream)
            {
                line = readerVmrk.ReadLine();
                if (line != null && line.Contains("DataFile="))
                {
                    if (line.Split('.').Length > 1)
                    {
                        eegExt = "." + line.Split('.')[line.Split('.').Length - 1];
                    }
                    line1 = "DataFile=" + txtName.Text + eegExt;
                    writerVmrk.WriteLine(line1);
                }
                else
                {
                    writerVmrk.WriteLine(line);
                }
            }
            while (!readerVhdr.EndOfStream)
            {
                line = readerVhdr.ReadLine();
                if (line != null && line.Contains("DataFile="))
                {
                    line1 = "DataFile=" + txtName.Text + eegExt;
                    writerVhdr.WriteLine(line1);
                }
                else if (line != null && line.Contains("MarkerFile="))
                {
                    line1 = "MarkerFile=" + txtName.Text + ".vmrk";
                    writerVhdr.WriteLine(line1);
                }
                else
                {
                    writerVhdr.WriteLine(line);
                }
            }
            readerVmrk.Close();
            readerVhdr.Close();
            writerVmrk.Close();
            writerVhdr.Close();
            File.Delete(inFilePathVmrk);
            File.Delete(inFilePathVhdr);
            inFilePathEeg = inDirPath + "\\" + inFile + eegExt;
            outFilePathEeg = inDirPath + "\\" + txtName.Text + eegExt;
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
            textFile.Text = string.Empty;
            txtName.Text = string.Empty;
        }

        private void ButtonInfo_Click(object sender, RoutedEventArgs e)
        {
            WindowInfo windowInfo = new WindowInfo();
            windowInfo.Owner = this;
            windowInfo.Show();
        }
    }
}