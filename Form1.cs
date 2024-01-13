using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FibonacciBackgroundWorker
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            backgroundWorker1.WorkerReportsProgress = true;
            backgroundWorker1.WorkerSupportsCancellation = true;
        }

        private void StartButton_Click(object sender, EventArgs e)
        {
            if (!backgroundWorker1.IsBusy)
            {
                StopButton.Enabled = true;
                listBox1.Items.Clear();
                backgroundWorker1.RunWorkerAsync();
            }
        }

        private void StopButton_Click(object sender, EventArgs e)
        {
            if (backgroundWorker1.WorkerSupportsCancellation == true)
            {
                backgroundWorker1.CancelAsync();
            }
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = sender as BackgroundWorker;

            for (int i = 0; i <= 400; i++)
            {
                if (worker.CancellationPending)
                {
                    e.Cancel = true;
                    break;
                }
                else
                {
                    System.Threading.Thread.Sleep(1);
                    worker.ReportProgress(Convert.ToInt32(Convert.ToDouble(i)/400.0*100.0),i);
                }
            }
        }

        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            listBox1.Items.Add(e.UserState);
            //listBox1.SelectedItem = e.UserState;
            listBox1.SelectedIndex = listBox1.Items.Count - 1;
            progressBar1.Value = e.ProgressPercentage;
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e) 
        {

            if (e.Cancelled)
            {
                MessageBox.Show("計算工作被取消");
            }
            else
            {
                MessageBox.Show("計算完成");
            }
        }
    }
}
