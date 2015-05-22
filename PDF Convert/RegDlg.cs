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
    public partial class RegDlg : Form
    {
        ini_config ini = new ini_config("config.ini");
        string language = string.Empty;
        ResourceManager rm = new ResourceManager(typeof(MainInfo));
        [DllImport("user32")]
        public static extern int ReleaseCapture();
        [DllImport("user32")]
        public static extern int SendMessage(IntPtr hwnd, int msg, int wp, int lp);
        public RegDlg()
        {
            language = ini.read_ini("language");
            if (string.IsNullOrEmpty(language))
                language = System.Globalization.CultureInfo.InstalledUICulture.Name;
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(language);
            InitializeComponent();
        }

        private void pbClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        private void btnActive_MouseEnter(object sender, EventArgs e)
        {
            this.btnActive.BackgroundImage = Properties.Resources.refgistebua;
        }
        private void btnActive_MouseLeave(object sender, EventArgs e)
        {
            this.btnActive.BackgroundImage = Properties.Resources.resisterbut;

        }


        private void RegDlg_Load(object sender, EventArgs e)
        {
            txtMachineCode.OutText = new reg().get_machine_code();
            this.btnReg.ButtonText = rm.GetString("Purchase");
            this.btnActive.ButtonText = rm.GetString("btnActive.Text");
            this.btnQQ.ButtonText = rm.GetString("btnQQ");
            this.btnPhone.ButtonText = rm.GetString("btnPhone");
            if (MainInfo.isReg)
            {
                string regCode = ini.read_ini("RegCode");
                string code = string.Empty;
                if (!string.IsNullOrEmpty(regCode))
                {

                    for (int i = 0; i < regCode.Length; i++)
                    {
                        code += "*";
                    }
                }
                txtRegCode.OutText = code;
                txtRegCode.Enabled = false;
                this.label2.Text = rm.GetString("Activated");
                this.btnActive.ButtonText = rm.GetString("Activated");
                this.btnReg.Visible = false;
                this.label3.Visible = false;
                this.btnActive.Enabled = false;
            }
            switch (language.ToLower())
            {
                case "zh-cn":

                    pbLogo.BackgroundImage = Properties.Resources.logo_03;
                    this.btnReg.Location = new Point(400, 106);
                    this.btnQQ.ButtonImage = Properties.Resources.qqcn;
                    this.btnQQ.Size = new Size(202, 38);
                    break;
                case "en":

                    pbLogo.BackgroundImage = Properties.Resources.logo_05;
                    this.btnReg.Location = new Point(430, 106);
                    this.btnQQ.ButtonImage = Properties.Resources.emailen;
                    this.btnQQ.Size = new Size(222, 27);
                    this.btnQQ.Location = new Point(132, 311);
                    this.btnQQ.FromType = 6;
                    this.btnPhone.Location = new Point(368, 311);
                    this.btnPhone.Size = new Size(278, 27);
                    break;

            }


        }

        private void btnActive_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                if (new reg().get_reg_code() == this.txtRegCode.OutText)
                {
                    if (new reg().write_reg_code(this.txtRegCode.OutText))
                    {
                        //MessageBox.Show("写入注册码成功!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        MessageBox.Show(rm.GetString("RegisteredSuccessfully"), rm.GetString("Tips"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                        //发送请求信息 
                        Version.Post("http://all.jsocr.com/", Version.GetParamName("Data"), "PDF转换器", Version.GetParamName("Version"), new reg().get_reg_code(), "注册窗口", "激活成功" + this.txtRegCode.OutText);
                    }
                    else
                    {
                        //MessageBox.Show("写入注册码失败!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        MessageBox.Show(rm.GetString("RegistrationFailed"), rm.GetString("Tips"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                        //发送请求信息 
                        Version.Post("http://all.jsocr.com/", Version.GetParamName("Data"), "PDF转换器", Version.GetParamName("Version"), new reg().get_reg_code(), "注册窗口", "激活失败" + this.txtRegCode.OutText);
                    }
                }
                else
                {
                    //MessageBox.Show("您输入的注册码不正确，如果您无注册码，请及时购买！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    MessageBox.Show(rm.GetString("msg13"), rm.GetString("Tips"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                this.DialogResult = DialogResult.Abort;
                this.Close();
            }
        }

        private void btnReg_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                Process.Start("http://www.xjpdf.com/software/pdfConvert/buy/?version=" + Version.version + "&machine=" + new reg().get_machine_code());
                //发送请求信息 
                Version.Post("http://all.jsocr.com/", Version.GetParamName("Data"), "PDF转换器", Version.GetParamName("Version"), new reg().get_reg_code(), "注册窗口", "购买正式版");
            }
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, 274, 61440 + 9, 0);
            }
        }

        private void RegDlg_Paint(object sender, PaintEventArgs e)
        {
            ControlPaint.DrawBorder(e.Graphics, this.ClientRectangle,
                 Color.FromArgb(45, 164, 244), 2, ButtonBorderStyle.Solid,
                 Color.FromArgb(45, 164, 244), 2, ButtonBorderStyle.Solid,
                 Color.FromArgb(45, 164, 244), 2, ButtonBorderStyle.Solid,
                 Color.FromArgb(45, 164, 244), 2, ButtonBorderStyle.Solid);
            //DrawBroder画不上底部边框 用DrawLine补充上
            e.Graphics.DrawLine(new Pen(Color.FromArgb(45, 164, 244), 3), 0, this.Height - 2, this.Width, this.Height - 2);
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {
            ControlPaint.DrawBorder(e.Graphics, this.ClientRectangle,
                 Color.FromArgb(45, 164, 244), 2, ButtonBorderStyle.Solid,
                 Color.FromArgb(45, 164, 244), 0, ButtonBorderStyle.Solid,
                 Color.FromArgb(45, 164, 244), 2, ButtonBorderStyle.Solid,
                 Color.FromArgb(45, 164, 244), 2, ButtonBorderStyle.Solid);
            //DrawBroder画不上底部边框 用DrawLine补充上
            e.Graphics.DrawLine(new Pen(Color.FromArgb(45, 164, 244), 3), 0, panel2.Height - 2, panel2.Width, panel2.Height - 2);
        }

        private void btnQQ_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                switch (language.ToLower())
                {
                    case "zh-cn":
                        Process.Start("http://www.xjpdf.com/software/pdfConvert/qq/?version=" + Version.version);
                        break;
                    case "en":
                        Process.Start("http://www.xjpdf.com/software/pdfConvert/qq/?version=" + Version.version);
                        break;
                    default:
                        Process.Start("http://www.xjpdf.com/software/pdfConvert/qq/?version=" + Version.version);
                        break;
                }

            }
        }




    }
}
