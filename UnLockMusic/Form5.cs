using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UnLockMusic
{
    public partial class Form5 : Form
    {
        // 定义一个委托用于传递 returndata
        public delegate void ReturndataEventHandler(object sender, ReturndataEventArgs e);

        // 定义一个事件用于传递 returndata
        public event ReturndataEventHandler ReturndataReady;

        // ReturndataEventArgs 类用于封装 returndata
        public class ReturndataEventArgs : EventArgs
        {
            public Returndata Returndata { get; set; }
        }


        public  struct  Returndata
        {
            public string songname;
            public string singer;
            public string path;
            public string classes;
           

        }
        public Returndata returndata;
        
        public Form5()
        {
            InitializeComponent();
        }

        //获取音乐路径,点击按钮获取文件夹路径
        //private string button1_Click(object sender, EventArgs e)
        //{
        //    folderBrowserDialog1.Description = "请选择文件夹";
           

        //    // 如果用户点击了确定按钮
        //    if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
        //    {
        //        // 获取用户选择的文件夹路径
        //        string selectedFolderPath = folderBrowserDialog1.SelectedPath;
        //        return  selectedFolderPath;
        //    }
        //    return null;
        //}

        //
        private void button1_Click2(object sender, EventArgs e)
        {
           
            {
                // 设置对话框的标题和过滤器
                openFileDialog1.Title = "选择文件";
                openFileDialog1.Filter = "文本文件 (*.flac)|*.flac|所有文件 (*.*)|*.*";

                // 如果用户点击了确定按钮
                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    // 获取用户选择的文件路径
                    string selectedFilePath = openFileDialog1.FileName;
                    aloneTextBox1.Text = selectedFilePath;
                    returndata.path = selectedFilePath;

                }
                else returndata.path = null;
            }
        }

        private void cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void confirm_Click(object sender, EventArgs e)
        {
            returndata.songname = songnameTextBox1.Text;
            returndata.singer = singerTextBox2.Text;
            returndata.classes = classesTextBox3.Text;
            if (returndata.path != null && returndata.songname != null && returndata.singer != null && returndata.classes != null)
            {
                // 触发事件并将 returndata 作为事件参数传递给主窗口
                ReturndataReady?.Invoke(this, new ReturndataEventArgs { Returndata = returndata });

                this.Close();
            }
            else aloneTextBox1.Text = "请将信息补全后在次确认";
        }
    }
}
