using AxWMPLib;
using LightTalkChatBox;
using ReaLTaiizor.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Org.BouncyCastle.Asn1.Crmf;


namespace UnLockMusic
{


    public partial class Form3 : Form
    {

        private int m_intFormat = 0;                    //保存格式：0-歌名，1-歌名（副标题），2-歌名（副标题）[歌手]，3-歌名[歌手]
        private string m_strDocument = "Music";         //保存音乐的文件夹
        private string m_strTempData = "TempData";      //临时文件夹
        private string m_strLog = "log.txt";            //日志文件
        private string m_strConfig = "config.txt";      //配置文件
        private bool isSelectAllMusic = false;          //将全选按钮和取消按钮合并

        private const int numbering = 1;
        private const int name = 2;
        private const int singer = 3;
        private const int album = 4;

        //定义回调
        private delegate void SetTipCallBack(string strText);
        private delegate void SetMusicInfoCallBack(string FilePath, string Title, string Author, string Description = "");
        private delegate void SetDataGVscanCallBack(int rowNum, clsMusic music);

        //声明回调
        private SetTipCallBack TipCallBack;
        private SetMusicInfoCallBack MusicInfoCallBack;
        private SetDataGVscanCallBack DataGVscanCallBack;

        //构造函数
        public Form3()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 判断歌曲是否能下载
        /// </summary>
        /// <param name="iRow">歌曲所在行</param>
        /// <returns>返回一个布尔值表示是否可以下载</returns>
        private bool RowCanDownload(int iRow)
        {
            if (dataGVscan.Rows[iRow].Cells["dgvCanDownload"].Value.ToString() == "1")
                return true;
            else
                return false;
        }


        //播放音乐函数
        private void PlayMusic(object oRow)
        {
            //lblTopTip.Text = "";
            int iRow = (int)oRow;
            string strDownloadURL = "";
            enmMusicSource emsSource = (enmMusicSource)dataGVscan.Rows[iRow].Cells["dgvSource"].Value;
            string strDownloadInfo = dataGVscan.Rows[iRow].Cells["dgvDownloadInfo"].Value.ToString();
            string strDisplayName = dataGVscan.Rows[iRow].Cells["dgvDisplayName"].Value.ToString();
            string strID = dataGVscan.Rows[iRow].Cells["dgvID"].Value.ToString();
            string strFileName = m_strDocument + "\\" + m_strTempData;
            clsMusicOperation mop = new clsMusicOperation();
            clsHttpDownloadFile hdf = new clsHttpDownloadFile();
            try
            {
                if (!Directory.Exists(strFileName))   //如果不存在就创建 临时文件夹  
                    Directory.CreateDirectory(strFileName);

                strDownloadURL = mop.GetMusicDownloadURL(strDownloadInfo, emsSource);
                if (strDownloadURL == "")
                {
                    WaitBar.Invoke((MethodInvoker)(() =>
                    {
                        WaitBar.Hide();
                    }));
                    lblTip.Invoke((MethodInvoker)(() =>
                    {
                        lblTip.Text = "无法获取歌曲“" + strDisplayName + "”的下载地址，试听失败！";
                    }));
                    WriteLog("无法获取歌曲“" + strDisplayName + "”的下载地址，试听失败！");
                    return;
                }
                strFileName = strFileName + "\\" + strDownloadInfo + mop.GetFileFormat(); //临时文件夹 + dgvDownloadInfo + 格式

                if (!(File.Exists(strFileName))) //不存在缓存，才下载
                    if (!hdf.Download(strDownloadURL, strFileName))
                    {
                        WaitBar.Invoke((MethodInvoker)(() =>
                        {
                            WaitBar.Hide();
                        }));
                        lblTip.Invoke((MethodInvoker)(() =>
                        {
                            lblTip.Text = "该音乐受版权保护，获取音乐缓存失败。";
                        }));
                        return;
                    }
                WaitBar.Invoke((MethodInvoker)(() =>
                {
                    WaitBar.Hide();
                }));
                //播放音乐
                lblTip.Invoke((MethodInvoker)(() =>
                {
                    lblTip.Text = "播放音乐序号：" + strID + "  歌曲名：" + strDisplayName;
                }));
                axWindowsMediaPlayer2.URL = strFileName;
                axWindowsMediaPlayer2.Ctlcontrols.play();
                //lblMusicTime.Text = axWindowsMediaPlayer1.currentMedia.durationString;
                timer1.Enabled = true;
            }
            catch (Exception e)
            {
                WaitBar.Invoke((MethodInvoker)(() =>
                {
                    WaitBar.Hide();
                }));
                lblTip.Invoke((MethodInvoker)(() =>
                {
                    lblTip.Text = "发生错误，错误信息：" + e.Message;
                }));
                WriteLog(strDisplayName + " 试听发生错误，错误信息：" + e.Message);
            }

        }

        //下载音乐函数
        /// <summary>
        /// 下载音乐
        /// </summary>
        /// <param name="iRow">列表中的行</param>
        private void DownloadMusic(int iRow)
        {
            TipCallBack = new SetTipCallBack(SetTipText);//实例化回调
            MusicInfoCallBack = new SetMusicInfoCallBack(SetMusicInfo);
            //lblTip.Text = "开启下载进程……";
            object oRow = (object)iRow;
            Thread thd = new Thread(new ParameterizedThreadStart(DownloadMusicThread));
            thd.Start(oRow);

        }
        /// <summary>
        /// 下载音乐的线程
        /// </summary>
        /// <param name="oRow">object oRow = (object)iRow;</param>
        private void DownloadMusicThread(object oRow)
        {
            int iRow = (int)oRow;
            string strDownloadURL = "";
            string strName = dataGVscan.Rows[iRow].Cells["dgvName"].Value.ToString();
            string strSinger = dataGVscan.Rows[iRow].Cells["dgvSinger"].Value.ToString();
            string strDownloadInfo = dataGVscan.Rows[iRow].Cells["dgvDownloadInfo"].Value.ToString();
            //int intSource = Convert.ToInt32(dataGVscan.Rows[iRow].Cells["dgvSource"].Value);
            enmMusicSource emsSource = (enmMusicSource)dataGVscan.Rows[iRow].Cells["dgvSource"].Value;
            string strFileName = m_strDocument;
            clsMusicOperation mop = new clsMusicOperation();
            clsHttpDownloadFile hdf = new clsHttpDownloadFile();
            string strTempFile = m_strDocument + "\\" + m_strTempData + "\\" + strDownloadInfo;//临时文件
            string strFormat = ".mp3";
            int i = 0;
            bool bolExistsTempFile = false;//是否存在缓存
            bool bolDownloadStatus = false;//是否下载成功

            //lblTip.Text = "正在下载……";
            //SetTipText("正在下载……" + strName);
            lblTip.Invoke(TipCallBack, "正在下载…… " + strName);//调用回调//因为线程无法条用主线的一些控件，必须通过回调函数实现
            try
            {
                if (!Directory.Exists(strFileName))   //如果不存在 m_strDocument 就创建  
                    Directory.CreateDirectory(strFileName);

                if ((File.Exists(strTempFile + ".mp3")) || (File.Exists(strTempFile + ".m4a")))//如果存在缓存，不再下载，直接复制
                {
                    bolExistsTempFile = true;
                    if ((File.Exists(strTempFile + ".mp3")))
                    {
                        strTempFile = strTempFile + ".mp3";
                        strFormat = ".mp3";
                    }
                    if (File.Exists(strTempFile + ".m4a"))
                    {
                        strTempFile = strTempFile + ".m4a";
                        strFormat = ".m4a";
                    }
                }
                else    //如果不存在，则需要先获取URL，再下载
                {
                    strDownloadURL = mop.GetMusicDownloadURL(strDownloadInfo, emsSource);
                    if (strDownloadURL == "")
                    {
                        lblTip.Invoke(TipCallBack, "无法获取歌曲“" + strName + "”的下载地址，下载失败！");
                        WriteLog("无法获取歌曲“" + strName + "”的下载地址，下载失败！");
                        return;
                    }
                    strFormat = mop.GetFileFormat();
                }

                strFileName = strFileName + "\\" + GetFileName(iRow) + strFormat;//设置文件名
                while (File.Exists(strFileName))  //如果存在文件，则添加序号，直到获取到一个可创建的文件名
                {
                    i++;
                    if (i == 1)
                        strFileName = strFileName.Insert(strFileName.Length - 4, "(" + i + ")");//
                    else
                        strFileName = strFileName.Replace("(" + (i - 1) + ")", "(" + i + ")");
                }

                if (bolExistsTempFile)
                {
                    File.Copy(strTempFile, strFileName, true);//三个参数分别是源文件路径，存储路径，若存储路径有相同文件是否替换
                    //File.Move(strTempFile.Replace("\\" + m_strTempData, ""), strFileName);//修改文件名
                    bolDownloadStatus = true;
                }
                else
                {
                    if (hdf.Download(strDownloadURL, strFileName))
                    {
                        //SetTipText(strName + "下载完成！");
                        //lblTip.Text = "下载完成！";
                        //mop.ChangeFileAttribute(strFileName, dataGVscan.Rows[iRow].Cells["dgvName"].Value.ToString(), dataGVscan.Rows[iRow].Cells["dgvSinger"].Value.ToString());
                        bolDownloadStatus = true;
                    }
                    else
                    {
                        //SetTipText("存在歌曲下载失败，请查看日志！");
                        //lblTip.Text = "下载失败，该名称歌曲可能已经存在，请修改Music文件下的同名歌曲；或者网络出现问题，请检查网络！";
                        WriteLog("歌曲：" + strName + " 下载失败，可能网络出现问题，请检查网络！");
                        //MessageBox.Show("歌曲：" + strName + " 下载失败，该名称歌曲可能已经存在，请修改Music文件下的同名歌曲；或者网络出现问题，请检查网络！");
                        lblTip.Invoke(TipCallBack, "歌曲：" + strName + " 下载失败，可能网络出现问题，请检查网络！");
                        bolDownloadStatus = false;
                    }
                }
                if (bolDownloadStatus)
                {
                    WriteLog(strName + " 下载完成！");
                    lblTip.Invoke(TipCallBack, strName + " 下载完成！");
                    if (strFormat.ToLower() == ".mp3")
                        //axWindowsMediaPlayer2.Invoke(MusicInfoCallBack, strFileName, strName, strSinger, "");
                        SetMusicInfo(strFileName, strName, strSinger);//设置歌曲信息
                }
            }
            catch (Exception e)
            {
                //SetTipText("下载线程发生错误，请查看日志！错误信息：" + e.Message);
                //lblTip.Text = "发生错误，错误信息：" + e.Message;
                WriteLog("歌曲：" + strName + " 下载失败，错误信息：" + e.Message);
                //MessageBox.Show("歌曲：" + strName + " 下载失败，错误信息：" + e.Message);
                lblTip.Invoke(TipCallBack, "歌曲：" + strName + " 下载失败，错误信息：" + e.Message);
            }
        }

        /// <summary>
        /// 返回要保存的文件名，未包括后缀格式
        /// </summary>
        /// <param name="iRow"></param>
        /// <returns></returns>
        private string GetFileName(int iRow)
        {
            string strResult = "";
            switch (m_intFormat)
            {
                case 0:
                    strResult = dataGVscan.Rows[iRow].Cells["dgvSinger"].Value.ToString() + " - " + dataGVscan.Rows[iRow].Cells["dgvName"].Value.ToString();
                    break;
                case 1:
                    strResult = dataGVscan.Rows[iRow].Cells["dgvSubheading"].Value.ToString();
                    strResult = dataGVscan.Rows[iRow].Cells["dgvName"].Value.ToString() + (strResult == "" ? "" : ("（" + strResult + "）"));
                    break;
                case 2:
                    strResult = dataGVscan.Rows[iRow].Cells["dgvSubheading"].Value.ToString();
                    strResult = dataGVscan.Rows[iRow].Cells["dgvName"].Value.ToString() + (strResult == "" ? "" : ("（" + strResult + "）")) + "[" + dataGVscan.Rows[iRow].Cells["dgvSinger"].Value.ToString() + "]";
                    break;
                case 3:
                    strResult = dataGVscan.Rows[iRow].Cells["dgvName"].Value.ToString() + "[" + dataGVscan.Rows[iRow].Cells["dgvSinger"].Value.ToString() + "]";
                    break;
                case 4:
                    strResult = dataGVscan.Rows[iRow].Cells["dgvName"].Value.ToString() + "(" + dataGVscan.Rows[iRow].Cells["dgvSinger"].Value.ToString() + ")";
                    break;
                case 6:
                    strResult = dataGVscan.Rows[iRow].Cells["dgvName"].Value.ToString() + " - " + dataGVscan.Rows[iRow].Cells["dgvSinger"].Value.ToString();
                    break;
                case 5:
                default:
                    strResult = dataGVscan.Rows[iRow].Cells["dgvName"].Value.ToString();
                    break;
            }
            //创建文件时提示：不支持给定路径的格式
            strResult = strResult.Replace("/", "_").Replace("\\", "_").Replace(":", "_").Replace("*", "_").Replace("?", "_").Replace("\"", "_").Replace("\'", "_").Replace("<", "_").Replace(">", "_").Replace("|", "_");

            return strResult;
        }


        private void dataGVscan_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            lblTip.Text = "";
            if (!RowCanDownload(dataGVscan.CurrentRow.Index))
            {
                lblTip.Text = "该音乐无法下载。";
                return;
            }

            if ((dataGVscan.CurrentCell.ColumnIndex == 2) || (dataGVscan.CurrentCell.OwningColumn.Name == "dgvPlayMusic"))
            {
                WaitBar.ResetText();
                WaitBar.Show();
                //WaitBar.MarqueeAnimationSpeed = 30;
                new Thread(new ParameterizedThreadStart(PlayMusic)).Start(dataGVscan.CurrentRow.Index);
                return;
            }

            //Download
            if (dataGVscan.CurrentCell.OwningColumn.Name == "dgvDownload")
            {
                DownloadMusic(Convert.ToInt32(dataGVscan.CurrentRow.Index));
                lblTopTip.Text = "this is add download";
                return;
            }

            if (this.dataGVscan.CurrentRow.Cells[0].EditedFormattedValue.ToString() == "True")
            {
                this.dataGVscan.CurrentRow.Cells[0].Value = false;
            }
            else
            {
                //for (int i = 0; i < this.dataGVscan.RowCount; i++)
                //{
                //    this.dataGVscan.Rows[i].Cells[0].Value = false;
                //}
                this.dataGVscan.CurrentRow.Cells[0].Value = true;

            }
        }

        /// <summary>
        /// 写日志
        /// </summary>
        /// <param name="strText">写入内容</param>
        private void WriteLog(string strText)
        {
            strText = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss") + "  " + strText;

            if (File.Exists(m_strLog))
            {
                FileStream fs = new FileStream(m_strLog, FileMode.Append, FileAccess.Write);
                StreamWriter sw = new StreamWriter(fs);
                sw.WriteLine(strText);
                sw.Flush();
                sw.Close();
            }
            else
            {
                FileStream fs = new FileStream(m_strLog, FileMode.Create);
                StreamWriter sw = new StreamWriter(fs);
                sw.WriteLine(strText);
                sw.Flush();
                sw.Close();
            }
        }

        //全选音乐列表
        private void selectAll()
        {
            isSelectAllMusic = !isSelectAllMusic;
            if (isSelectAllMusic)
            {
                for (int i = 0; i < this.dataGVscan.RowCount; i++)
                {
                    if (RowCanDownload(i))
                        this.dataGVscan.Rows[i].Cells[0].Value = true;
                }
            }
            else
            {
                for (int i = 0; i < this.dataGVscan.RowCount; i++)
                {
                    this.dataGVscan.Rows[i].Cells[0].Value = false;
                }
            }
        }


        //下载选择的音乐
        private void downloadSelected()
        {
            for (int i = 0; i < dataGVscan.RowCount; i++)
            {
                if (dataGVscan.Rows[i].Cells[0].EditedFormattedValue.ToString() == "True")
                {
                    DownloadMusic(i);
                }
            }
        }

        private void dataGVscan_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.ColumnIndex == 0)
            {
                selectAll();
            }
            if (e.ColumnIndex == 7)
            {
                downloadSelected();
            }
        }


        private void ClearForm()
        {
            lblTopTip.Text = "点击歌曲名称可以试听。";
            lblTip.Text = "";
        }

        //设置提示词
        private void SetTipText(string strText)
        {
            lblTip.Text = strText;
        }

        //设置播放器信息,使用com组件，使用自带的axWindowsMediaPlayer2
        private void SetMusicInfo(string FilePath, string Title, string Author, string Description = "")
        {
            axWindowsMediaPlayer2.URL = "";
            axWindowsMediaPlayer2.URL = FilePath;
            axWindowsMediaPlayer2.Ctlcontrols.play();
            axWindowsMediaPlayer2.currentMedia.setItemInfo("Title", Title);
            axWindowsMediaPlayer2.currentMedia.setItemInfo("Author", Author);
            axWindowsMediaPlayer2.currentMedia.setItemInfo("Description", Description);
            axWindowsMediaPlayer2.URL = "";
        }

        private void cmbFormat_SelectedIndexChanged(object sender, EventArgs e)
        {
            m_intFormat = cmbFormat.SelectedIndex;
        }

        //初始化播放器设置
        private void init()
        {
            //设置 播放器 的初始设置和位置
            axWindowsMediaPlayer2.settings.autoStart = true;
            axWindowsMediaPlayer2.settings.volume = 100;
            axWindowsMediaPlayer2.Width = dataGVscan.Width;
            axWindowsMediaPlayer2.Height = 45;
            axWindowsMediaPlayer2.Top = dataGVscan.Top + dataGVscan.Height + 30;
            axWindowsMediaPlayer2.Left = dataGVscan.Left;

            cmbFormat.Items.Clear();
            cmbFormat.Items.Add("歌名");
            cmbFormat.Items.AddRange(new object[] {
            "歌手 - 歌名",
            "歌名（副标题）",
            "歌名（副标题）[歌手]",
            "歌名[歌手]",
            "歌名（歌手）",
            "歌名",
            "歌名 - 歌手"});
            cmbFormat.SelectedIndex = 0;
        }

        /*
         *  向音乐列表中增加一项
         *  params:
         *        rowNum:当前行号
         *        music:clsMusic类型项
         *  return;none
         */
        private void AddAMusic(int rowNum, clsMusic music)
        {
            dataGVscan.Rows.Add();
            dataGVscan.Rows[rowNum].Cells[numbering].Value = rowNum + 1;
            dataGVscan.Rows[rowNum].Cells[name].Value = music.Name + (music.Subheading == "" ? "" : music.Subheading);
            dataGVscan.Rows[rowNum].Cells[singer].Value = music.Singer;
            dataGVscan.Rows[rowNum].Cells[album].Value = music.Class;
            dataGVscan.Rows[rowNum].Cells["dgvSource"].Value = music.Source;
            dataGVscan.Rows[rowNum].Cells["dgvName"].Value = music.Name;
            dataGVscan.Rows[rowNum].Cells["dgvSubheading"].Value = music.Subheading;
            if (music.CanDownload)
            {
                dataGVscan.Rows[rowNum].Cells["dgvCanDownload"].Value = "1";
            }
            else
            {
                dataGVscan.Rows[rowNum].Cells["dgvCanDownload"].Value = "0";
                dataGVscan.Rows[rowNum].DefaultCellStyle.BackColor = Color.Silver;
            }

            switch (music.Source)
            {
                case enmMusicSource.QQ:
                    dataGVscan.Rows[rowNum].Cells[5].Value = "QQ音乐";
                    dataGVscan.Rows[rowNum].Cells["dgvDownloadInfo"].Value = music.DownloadInfo;
                    break;
                case enmMusicSource.Kg:
                    dataGVscan.Rows[rowNum].Cells[5].Value = "酷狗音乐";
                    dataGVscan.Rows[rowNum].Cells["dgvDownloadInfo"].Value = music.DownloadInfo;
                    break;
                case enmMusicSource.Kw:
                    dataGVscan.Rows[rowNum].Cells[5].Value = "酷我音乐";
                    dataGVscan.Rows[rowNum].Cells["dgvDownloadInfo"].Value = music.DownloadInfo;
                    break;
                case enmMusicSource.Wyy:
                    dataGVscan.Rows[rowNum].Cells[5].Value = "网易云音乐";
                    dataGVscan.Rows[rowNum].Cells["dgvDownloadInfo"].Value = music.DownloadInfo;
                    break;
                default:
                    dataGVscan.Rows[rowNum].Cells[5].Value = "";
                    break;
            }

        }



        //搜索音乐
        private void SearchMusic()
        {
            TipCallBack = new SetTipCallBack(SetTipText);//实例化回调
            MusicInfoCallBack = new SetMusicInfoCallBack(SetMusicInfo);
            DataGVscanCallBack = new SetDataGVscanCallBack(AddAMusic);
            Thread searchMusicThread = new Thread(new ThreadStart(GetMusicList));
            searchMusicThread.Start();
        }

        //获取音乐列表
        private void GetMusicList()
        {
            string strName = txbSerch.Text;
            clsMusicOperation mop = new clsMusicOperation();
            List<clsMusic> lmsc = new List<clsMusic>();

            try
            {
                dataGVscan.Invoke((MethodInvoker)(() =>  //清空列表
                {
                    dataGVscan.Rows.Clear();
                }));
                lmsc = mop.GetMusicList(strName);
                for (int i = 0; i < lmsc.Count(); i++)
                {
                    WaitBar.Invoke((MethodInvoker)(() =>
                    {
                        WaitBar.Hide();
                    }));
                    dataGVscan.Invoke(DataGVscanCallBack, i, lmsc[i]);
                }
                WaitBar.Invoke((MethodInvoker)(() =>
                {
                    WaitBar.Hide();
                }));
                // todo: 发生错误，返回参数，显示音乐列表
                lblTip.Invoke(TipCallBack, "搜索完毕。（tip:网络报错会导致报错）");
            }
            catch (Exception e)
            {
                // todo: 发生错误，返回参数，不显示音乐列表
                lblTip.Invoke(TipCallBack, "发生错误，错误信息：" + e.Message);
                WriteLog("搜索发生错误，错误信息：" + e.Message);
            }
        }


        //搜索点击事件
        private void btnSerch_Click(object sender, EventArgs e)
        {
            WaitBar.ResetText();
            WaitBar.Show();
            //WaitBar.MarqueeAnimationSpeed = 30;
            ClearForm();
            SearchMusic();
        }
        
        private void txbSerch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 13)
                btnSerch_Click(sender, e);
        }

       

    

        private void dataGVscan_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {
            
            lblTip.Text = "";
            if (!RowCanDownload(dataGVscan.CurrentRow.Index))
            {
                lblTip.Text = "该音乐无法下载。";
                return;
            }

            if ((dataGVscan.CurrentCell.ColumnIndex == 2) || (dataGVscan.CurrentCell.OwningColumn.Name == "dgvPlayMusic"))
            {
                WaitBar.ResetText();
                WaitBar.Show();
                //WaitBar.MarqueeAnimationSpeed = 30;
                new Thread(new ParameterizedThreadStart(PlayMusic)).Start(dataGVscan.CurrentRow.Index);
                return;
            }

            //Download
            if (dataGVscan.CurrentCell.OwningColumn.Name == "dgvDownload")
            {
                DownloadMusic(Convert.ToInt32(dataGVscan.CurrentRow.Index));
                lblTopTip.Text = "this is add download";
                return;
            }

            if (this.dataGVscan.CurrentRow.Cells[0].EditedFormattedValue.ToString() == "True")
            {
                this.dataGVscan.CurrentRow.Cells[0].Value = false;
            }
            else
            {
                //for (int i = 0; i < this.dataGVscan.RowCount; i++)
                //{
                //    this.dataGVscan.Rows[i].Cells[0].Value = false;
                //}
                this.dataGVscan.CurrentRow.Cells[0].Value = true;

            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            btnSerch_Click(sender, e);
        }


        //打开文件保存目录
        private void btnOpenDirectory_Click(object sender, EventArgs e)
        {
            if (Directory.Exists(m_strDocument))//存在，直接打开
                System.Diagnostics.Process.Start("explorer.exe", m_strDocument);
            else                                //不存在，创建，打开
            {
                Directory.CreateDirectory(m_strDocument);
                System.Diagnostics.Process.Start("explorer.exe", m_strDocument);
            }
        }

        //循环播放
        private void loopPlay_CheckedChanged(object sender, EventArgs e)
        {
            CheckState isLoopPlay = loopPlay.CheckState;
            if (isLoopPlay == CheckState.Checked)
            {
                axWindowsMediaPlayer1.settings.setMode("loop", true);
            }
            else
            {
                axWindowsMediaPlayer1.settings.setMode("loop", false);
            }
        }

        private void folderBrowserDialog1_HelpRequest(object sender, EventArgs e)
        {

        }

        private async void submmit_Click(object sender, EventArgs e)
        {

               
                chatBox.addChatBubble(ChatBox.BubbleSide.RIGHT, richTextBoxEdit1.Text, "靓仔", "110", @"temp\testProfile2.png");
                string output = await TalktoTongyi(richTextBoxEdit1.Text);
            /*output 变量之前使用 await 关键字，告诉编译器等待 TalktoTongyi 方法完成并返回字符串结果后再继续执行后续代码。*/
                chatBox.addChatBubble(ChatBox.BubbleSide.LEFT, output, "通义千问", "110", @"temp\testProfile1.png");
        }

        private async Task<string> TalktoTongyi(string input)
        {

            Tongyi tongyi = new Tongyi();
            string responseBody = await tongyi.CallQWen(input);
            string textValue = ParseTextFromResponseBody(responseBody);
            return textValue;
        }

        // 新增方法解析responseBody中的"text"值
        private string ParseTextFromResponseBody(string responseBody)
        {
            // 将 JSON 字符串解析为 JObject
            JObject json = JObject.Parse(responseBody);

            // 提取 "output" 对象
            JObject output = (JObject)json["output"];

            // 提取 "text" 字段的值
            string textValue = (string)output["text"];

            return textValue;
        }


        //导入本地音乐到数据库
        private void input_Click(object sender, EventArgs e)
        {
            Form5 form5 = new Form5();
            form5.ReturndataReady += Form5_ReturndataReady;
            form5.ShowDialog();
        }
        //
        private void Form5_ReturndataReady(object sender, Form5.ReturndataEventArgs e)
        {
            // 在这里获取传递的 returndata
            Form5.Returndata returndata = e.Returndata;
            Database_op database_Op = new Database_op();
            database_Op.SaveMusic(returndata.path, returndata.songname, returndata.singer, returndata.classes);

           
        }


        //搜索所有
        private void button2_Click(object sender, EventArgs e)
        {
            Database_op database_Op = new Database_op();
            string key = hopeTextBox2.Text;
            if(key== "唰，一下，很快啊") {
                poisonDataGridView1.DataSource = database_Op.Sellect_all();
                poisonDataGridView1.Columns["play"].Visible = true;
            }
            else
            {
                poisonDataGridView1.DataSource = database_Op.FuzzySearchMusic(key);
                poisonDataGridView1.Columns["play"].Visible = true;
            }
         

        }

        private void poisonDataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            Database_op database_Op = new Database_op();
            // 检查是否点击了按钮列
            if (e.ColumnIndex == poisonDataGridView1.Columns["play"].Index && e.RowIndex >= 0)

            {// 从数据库中获取音乐数据
                int id =  Convert.ToInt32(poisonDataGridView1.Rows[e.RowIndex].Cells["songID"].Value);
                byte[] data = database_Op.playmusic(id);
                
                // 将音乐数据写入临时文件
                string tempFilePath = Path.GetTempFileName();
                File.WriteAllBytes(tempFilePath, data);
                

                // 设置 AxWindowsMediaPlayer 控件播放临时文件
                axWindowsMediaPlayer1.URL = tempFilePath;
            }
        }

    }
}
