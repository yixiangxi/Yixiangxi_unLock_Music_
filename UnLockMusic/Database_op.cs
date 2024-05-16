using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using MySql.Data.MySqlClient;
using System.Data;
using System.Windows.Forms;
using static Vanara.PInvoke.CM_PARTIAL_RESOURCE_DESCRIPTOR;
using System.Windows.Media;
using NAudio.Wave;
using NAudio.Codecs;
using FlacBox;

namespace UnLockMusic
{
    class Database_op

    {

        // MySQL数据库连接信息
        static string  connectionString = "server=localhost;database=yixiangxi_music;uid=root;password=123456";

       
        public void SaveMusic(string path,string songname,string singer,string classes )
        {
            // 连接到数据库
            MySqlConnection connection = new MySqlConnection(connectionString);
            try
            {
                connection.Open();
                string musicFile = path;
                //读取音乐文件数据
                byte[] musicData = File.ReadAllBytes(musicFile);
                MySqlCommand command = new MySqlCommand("INSERT INTO song (songName, source,singer,className) VALUES (@name, @data,@singer,@classes)", connection);
                command.Parameters.AddWithValue("@name", songname);
                command.Parameters.AddWithValue("@data", musicData);
                command.Parameters.AddWithValue("@singer", singer);
                command.Parameters.AddWithValue("@classes", classes);
                command.ExecuteNonQuery();

                connection.Close();
            }
            catch (Exception ex)
            {
                //Console.WriteLine("An error occurred: " + ex.Message);
                string error = "An error occurred: " + ex.Message;
                Console.WriteLine(error);
            }
            finally
            {
                connection.Close();
            }
   

        }



        public DataTable Sellect_all()
        {
            // 连接到数据库
            MySqlConnection connection = new MySqlConnection(connectionString);
            try
            {
                connection.Open();
                string query = "SELECT songName, singer, className, songID FROM song;";
                MySqlCommand command = new MySqlCommand(query, connection);
                MySqlDataAdapter adapter = new MySqlDataAdapter(command);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);
                return dataTable;
                //DataGridView1.DataSource = dataTable;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: 出错啦" + ex.Message);
                return null;
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
            }

        }

        //模糊搜索
        public DataTable FuzzySearchMusic(string keyword)
        {
            // 连接到数据库
            MySqlConnection connection = new MySqlConnection(connectionString);
            try
            {
                connection.Open();
                string query = "SELECT songName, singer, className, songID FROM song WHERE songName LIKE @keyword OR singer LIKE @keyword OR className LIKE @keyword;";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@keyword", "%" + keyword + "%");
                MySqlDataAdapter adapter = new MySqlDataAdapter(command);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);
                return dataTable;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: 出错啦" + ex.Message);
                return null;
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
            }
        }

        public byte[] playmusic(int id)
        {
            //与数据库建立连接
            MySqlConnection connection = new MySqlConnection(connectionString);
            connection.Open();
            try
            {
                string query = "SELECT source FROM song WHERE songID = @songID"; 
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@songID", id); 

                 byte[] audioData = (byte[])command.ExecuteScalar();

                return audioData;

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
                return new byte[0];
            }
            finally
            {
                if (connection != null && connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
            }
        }

        




    }
}
