using Server;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace ServerGUI
{
    public partial class MainForm : Form
    {
        private string _pathToOpenA = string.Empty;
        private string _pathToOpenB = string.Empty;
        private string _pathToSave = string.Empty;
        private string _ipAddress = "127.0.0.1";
        private int _port = 11000;
        private double _eps = 0.001;
        private string[] _loadingArray = new string[] { "—", " \\", " |", " /" };
        private int _loadingIterator = 0;
        public MainForm()
        {
            InitializeComponent();
            openFileDialog1.Filter = "All files(*.*)|*.*";
            openFileDialogB.Filter = "All files(*.*)|*.*";
        }
        private bool ValidateIPv4(string ipString)
        {
            if (String.IsNullOrWhiteSpace(ipString))
            {
                return false;
            }

            string[] splitValues = ipString.Split('.');
            if (splitValues.Length != 4)
            {
                return false;
            }

            byte tempForParsing;

            return splitValues.All(r => byte.TryParse(r, out tempForParsing));
        }
        private void GetSettingsFromFile()
        {
            var lines = File.ReadAllLines("..\\..\\..\\..\\settings.txt");
            if (ValidateIPv4(lines[0]))
                _ipAddress = lines[0];
            int port;
            if (int.TryParse(lines[1], out port) && port > 0 && port < 65535)
                _port = port;
            double eps;
            if (double.TryParse(lines[2], out eps))
                _eps = eps;
        }
        private string UpdateLoadingArray()
        {
            _loadingIterator += 1;
            if (_loadingIterator >= _loadingArray.Length)
                _loadingIterator = 0;
            return _loadingArray[_loadingIterator];
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.Cancel)
                return;
            _pathToOpenA= openFileDialog1.FileName;
            Alabel.Text = "...\\" + _pathToOpenA.Split("\\").Last();
            Alabel.ForeColor = Color.Black;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (openFileDialogB.ShowDialog() == DialogResult.Cancel)
                return;
            _pathToOpenB = openFileDialogB.FileName;
            Blabel.Text = "...\\" + _pathToOpenB.Split("\\").Last();
            Blabel.ForeColor = Color.Black;
        }


        private void button3_Click(object sender, EventArgs e)
        {
            if (saveFileDialog.ShowDialog() == DialogResult.Cancel)
                return;
            _pathToSave= saveFileDialog.FileName;
            Slabel.Text = "...\\" + _pathToSave.Split("\\").Last();
            Slabel.ForeColor = Color.Black;
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            CClabel.Text = trackBar1.Value.ToString();
        }

        private async void ExecButton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(_pathToOpenA) || !File.Exists(_pathToOpenA))
            {
                MessageBox.Show("Enter valid path to file of A!");
                Alabel.ForeColor = Color.Red;
                return; 
            }
            if (string.IsNullOrEmpty(_pathToOpenB) || !File.Exists(_pathToOpenB))
            {
                MessageBox.Show("Enter valid path to file of B!");
                Blabel.ForeColor = Color.Red;
                return;
            }
            if (string.IsNullOrEmpty(_pathToSave) || !File.Exists(_pathToSave))
            {
                MessageBox.Show("Enter valid path to save file of answers!");
                Slabel.ForeColor = Color.Red;
                return;
            }
            //GetSettingsFromFile();
            resLabel.Text = "Opening file\n" + Alabel.Text;
            var progress = new Progress<int>(i =>
            {
                loadingLab.Text = UpdateLoadingArray();
            });
            try
            {
                ExecButton.Enabled = false;
                seidelBut.Enabled = false;
                var a = await Task.Run(() => FileWorker.ReadMatrix(_pathToOpenA, progress));
                resLabel.Text = "Opening file\n" + Blabel.Text;
                var b = await Task.Run(() => FileWorker.ReadVector(_pathToOpenB, progress));
                float[][] matrix = ServerWorker.ConcatenateAB(a, b);
                this.progressBar.Minimum = 0;
                this.progressBar.Maximum = a.GetLength(0);
                this.progressBar.Step = 1;
                resLabel.Text = "Calculating...";
                this.progressBar.Value = 0;
                var progressBar = new Progress<int>(percent =>
                {
                    this.progressBar.Value = percent;
                    loadingLab.Text = UpdateLoadingArray();

                });
                using (var server = new GaussSolverTcpServer(_port, _ipAddress, matrix, trackBar1.Value))
                {

                    var result = await Task.Run(() => server.Run(progressBar));
                    var ans = ServerWorker.BackwardMode(result);
                    var loss = ServerWorker.GetAccuracy(a, b, ans);
                    FileWorker.WriteVector(_pathToSave, ans, progress);
                    resLabel.Text = $"Done!\nExecution time:\n{server.LastExecutionTime} ms.\nAverage accuracy:\n{loss}%";
                }
            }
            catch (Exception ex)
            {
                resLabel.Text = $"Error!\n{ex.Message}";
            }
            ExecButton.Enabled = true;
            seidelBut.Enabled = true;

        }

        private async void seidelBut_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(_pathToOpenA) || !File.Exists(_pathToOpenA))
            {
                MessageBox.Show("Enter valid path to file of A!");
                Alabel.ForeColor = Color.Red;
                return;
            }
            if (string.IsNullOrEmpty(_pathToOpenB) || !File.Exists(_pathToOpenB))
            {
                MessageBox.Show("Enter valid path to file of B!");
                Blabel.ForeColor = Color.Red;
                return;
            }
            if (string.IsNullOrEmpty(_pathToSave) || !File.Exists(_pathToSave))
            {
                MessageBox.Show("Enter valid path to save file of answers!");
                Slabel.ForeColor = Color.Red;
                return;
            }
            resLabel.Text = "Opening file\n" + Alabel.Text;
            var progress = new Progress<int>(i =>
            {
                loadingLab.Text = UpdateLoadingArray();
            });
            GetSettingsFromFile();
            try
            {
                ExecButton.Enabled = false;
                seidelBut.Enabled = false;
                var a = await Task.Run(() => FileWorker.ReadMatrix(_pathToOpenA, progress));
                resLabel.Text = "Opening file\n" + Blabel.Text;
                var b = await Task.Run(() => FileWorker.ReadVector(_pathToOpenB, progress));
                if (a.GetLength(0) != b.Length)
                {
                    MessageBox.Show("Dimensions of A and B must be the same!");
                    return;
                }
                float[][] matrix = ServerWorker.ConcatenateAB(a, b);
                resLabel.Text = "Calculating...";
                this.progressBar.Value = 0;
                var watch = new Stopwatch();
                watch.Start();
                var result = await Task.Run(() => SeidelSolver.Seidel.Solve(matrix, progress, _eps));
                var loss = ServerWorker.GetAccuracy(a, b, result);
                FileWorker.WriteVector(_pathToSave, result, progress);
                watch.Stop();
                resLabel.Text = $"Done!\nExecution time:\n{watch.ElapsedMilliseconds} ms.\nAverage accuracy:\n{loss}%";
            }
            catch (Exception ex)
            {
                resLabel.Text = $"Error!\n{ex.Message}";
            }
            seidelBut.Enabled = true;
            ExecButton.Enabled = true;
        }
     }
}
