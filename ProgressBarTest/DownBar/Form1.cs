using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DownBar
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        /// <summary>
        /// 显示进度
        /// </summary>
        /// <param name="val"></param>
        private void ProgressBar_Value(int val)
        {
            progressBar1.Value = val;
            label1.Text = val.ToString() + "%";
        }
        /// <summary>
        /// 下载文件
        /// </summary>
        /// <param name="url"></param>
        /// <param name="savefile"></param>
        /// <param name="downloadProgressChanged"></param>
        /// <param name="downloadFileCompleted"></param>
        private void DownloadFile(string url, string savefile, Action<int> downloadProgressChanged, Action downloadFileCompleted)
        {
            WebClient client = new WebClient();
            if (downloadProgressChanged != null)
            {
                client.DownloadProgressChanged += delegate (object sender, DownloadProgressChangedEventArgs e)
                {
                    if (this.InvokeRequired)
                    {


                    }
                    else
                    {
                        this.Invoke(downloadProgressChanged, e.ProgressPercentage);
                    }
                   
                };
            }
            if (downloadFileCompleted != null)
            {
                client.DownloadFileCompleted += delegate (object sender, AsyncCompletedEventArgs e)
                {
                    this.Invoke(downloadFileCompleted);
                };
            }
            client.DownloadFileAsync(new Uri(url), savefile);
        }
        private void DownnLoadFinished()
        {

        }

        private void btnDownLoad_Click(object sender, EventArgs e)
        {
            DownloadFile("http://localhost:8080/movie.zip", Application.StartupPath+"\\movie.zip", ProgressBar_Value, null);
        }
    }
}
