using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SavePicsWRU
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void GetPics_Click(object sender, EventArgs e)
        {
            

            for (int i = 0; i < 999; i++)
            {


                string url = "http://portal.wru.com.cn/showImg.do?yhm=159";
                int order = 30001 + i;
                url += order.ToString();



                HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(url);
                webRequest.Method = "GET";
                //复制上次会话的Cookie，不复制就无法通过验证
                webRequest.Headers.Add("cookie", webBrowser1.Document.Cookie);
                HttpWebResponse webResponse = (HttpWebResponse)webRequest.GetResponse();
                //webBrowser1.Navigate(url);

                Debug.WriteLine("url:" + order);


                

                //创建文件写入对象
                using (FileStream fileWrite = new FileStream((@"F:\YunGuan\159" + order + ".png"), FileMode.Create))
                {
                    //指定文件一次读取时的字节长度
                    byte[] by = new byte[1024 * 1024 * 10];
                    int count = 0;
                    while (true)
                    {
                        //将文件转换为二进制数据保存到内存中，同时返回读取字节的长度
                        count = webResponse.GetResponseStream().Read(by, 0, by.Length);
                        if (count == 0)//文件是否全部转换为二进制数据
                        {
                            break;
                        }
                        //将二进制数据转换为文件对象并保存到指定的物理路径中
                        fileWrite.Write(by, 0, count);
                    }
                    //MessageBox.Show("OK");
                }

                Debug.WriteLine("Finished");

            }
        }




    }
}
