using System.Windows.Forms.DataVisualization.Charting;

namespace Practice_4
{
    public partial class Form1 : Form
    {
        public string folderPath { get; set; }

        public int N { get; set; } = 0;

        public Form1()
        {
            InitializeComponent();

            chooseFolderButton.Click += OpenFolderBrowser_Click;
            NUpDown.ValueChanged += NUpDown_ValueChanged;
            fileProcessingButton.Click += FileProcessing_Click;
        }

        private void OpenFolderBrowser_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();

            if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                folderPath = folderBrowserDialog.SelectedPath;
                choosenFolderLabel.Text = folderPath;
            }
        }

        private void NUpDown_ValueChanged(object sender, EventArgs e)
        {
            N = (int)NUpDown.Value;
            NLabel.Text = "N = " + NUpDown.Value.ToString();
        }

        private void FileProcessing_Click(object sender, EventArgs e)
        {
            
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}