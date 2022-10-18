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
using PSHostsFile;

namespace ChromeTranslate
{
    public partial class Form1 : Form
    {
        private const string TranslateAddress = "translate.googleapis.com";
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            var host = HostsFile.Get().FirstOrDefault(x => x.Hostname == TranslateAddress);
            if (host != null)
            {
                txtIp.Text = host.Address;
            }
        }

        private void btnQueryIp_Click(object sender, EventArgs e)
        {
            var ips = Dns.GetHostAddresses("translate.google.cn");
            if (ips == null || ips.Length == 0)
            {
                MessageBox.Show("未找到合适的Ip，请手动查询");
                return;
            }

            txtIp.Text = ips[0].ToString();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (txtIp.Text == "")
            {
                MessageBox.Show("Ip地址不能为空！");
                return;
            }

            if (!IPAddress.TryParse(txtIp.Text, out _))
            {
                MessageBox.Show("您输入的Ip地址有误，请检查！");
                return;
            }

            try
            {
                HostsFile.Set(TranslateAddress, txtIp.Text);
                MessageBox.Show("修改成功，赶紧去试试吧。");
            }
            catch (Exception exception)
            {
                MessageBox.Show($"修改失败了，大概可能是\"{exception.Message}\"的原因。");
            }
            
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}