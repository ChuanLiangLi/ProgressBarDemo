using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProgressBarTest
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Debug.WriteLine("主线程："+ Thread.CurrentThread.ManagedThreadId);
            //子线程工作
            new Thread(new ThreadStart( StartDownLoad)).Start();
        }
        public  void StartDownLoad()
        {
            DownLoader down = new DownLoader();
            down.DownLoadProgress += new DownLoader.DownLoadProgressHandler( Down_DownLoadProgress);
            down.Start();
        }

        private void Down_DownLoadProgress(long toatl, long current)
        {
           if(this.InvokeRequired)
            {
                Debug.WriteLine("子线程：" + Thread.CurrentThread.ManagedThreadId);
                this.Invoke(new DownLoader.DownLoadProgressHandler(Down_DownLoadProgress),new object [] {toatl,current });
            }
           else
            {
                Debug.WriteLine("主线程：" + Thread.CurrentThread.ManagedThreadId);
                this.progressBar1.Maximum = (int)toatl;
                this.progressBar1.Value = (int)current;
            }
        }
    }
    public class DownLoader
    {
        public delegate void DownLoadProgressHandler(long toatl,long current);
        public event DownLoadProgressHandler DownLoadProgress;
        public  void Start()
        {
            for (int i = 0; i <=100; i++)
            {
                if (DownLoadProgress != null)
                {
                    DownLoadProgress(100,i);
                }
                System.Threading.Thread.Sleep(100);
            }
        }
    }
}
