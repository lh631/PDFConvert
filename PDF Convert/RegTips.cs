using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Resources;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace PDF_Convert
{
    public partial class RegTips : Form
    {
        [DllImport("user32")]
        public static extern int ReleaseCapture();
        [DllImport("user32")]
        public static extern int SendMessage(IntPtr hwnd, int msg, int wp, int lp);
        ini_config ini = new ini_config("config.ini");
        string language = string.Empty;
        public static ResourceManager rm = new ResourceManager(typeof(MainInfo));
        public RegTips()
        {

            language = ini.read_ini("language");
            if (string.IsNullOrEmpty(language))
                language = System.Globalization.CultureInfo.InstalledUICulture.Name;
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(language);
            InitializeComponent();

        }
        private void ClearListTips_Load(object sender, EventArgs e)
        {
            this.btnConfirm.ButtonText = rm.GetString("Buy");
            this.btnOver.ButtonText = rm.GetString("RegActive");
            switch (language.ToLower())
            {
                case "zh-cn":
                    this.BackgroundImage = Properties.Resources.ch_zn;
                    break;
                case "en":
                    this.BackgroundImage = Properties.Resources.en;
                    break;
                default:
                    this.BackgroundImage = Properties.Resources.ch_zn;
                    break;
            }
        }
        private void pbClose_MouseClick(object sender, MouseEventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void btnConfirm_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                Process.Start("http://www.xjpdf.com/software/pdfConvert/buy/?version=" + Version.version + "&machine=" + new reg().get_machine_code());
                //this.DialogResult = DialogResult.Cancel;
                //this.Close();
                //发送请求信息 
                Version.Post("http://all.jsocr.com/", Version.GetParamName("Data"), "PDF转换器", Version.GetParamName("Version"), new reg().get_reg_code(), "免费试用版提示窗口",  "购买正式版");
            }
        }

        private void btnOver_MouseClick(object sender, MouseEventArgs e)
        {

            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                //发送请求信息 
                Version.Post("http://all.jsocr.com/", Version.GetParamName("Data"), "PDF转换器", Version.GetParamName("Version"), new reg().get_reg_code(), "免费试用版提示窗口", "注册并激活");
                this.DialogResult = DialogResult.OK;
                this.Close();

            }
        }

        private void ClearListTips_Paint(object sender, PaintEventArgs e)
        {
            ControlPaint.DrawBorder(e.Graphics, this.ClientRectangle,
                       Color.FromArgb(45, 164, 244), 2, ButtonBorderStyle.Solid,
                       Color.FromArgb(45, 164, 244), 2, ButtonBorderStyle.Solid,
                       Color.FromArgb(45, 164, 244), 2, ButtonBorderStyle.Solid,
                       Color.FromArgb(45, 164, 244), 2, ButtonBorderStyle.Solid);
            //DrawBroder画不上底部边框 用DrawLine补充上
            e.Graphics.DrawLine(new Pen(Color.FromArgb(45, 164, 244), 3), 0, panel1.Height - 2, panel1.Width, panel1.Height - 2);

        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, 274, 61440 + 9, 0);
            }
        }




    }
}
