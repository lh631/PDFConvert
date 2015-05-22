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
    public partial class ClearListTips : Form
    {

        ini_config ini = new ini_config("config.ini");
        string language = string.Empty;
        public static ResourceManager rm = new ResourceManager(typeof(MainInfo));
        public ClearListTips()
        {

            language = ini.read_ini("language");
            if (string.IsNullOrEmpty(language))
                language = System.Globalization.CultureInfo.InstalledUICulture.Name;
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(language);
            InitializeComponent();

        }
        private void ClearListTips_Load(object sender, EventArgs e)
        {
            this.btnConfirm.ButtonText = rm.GetString("btnConfirm");
            this.btnOver.ButtonText = rm.GetString("btnOver");
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
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }

        private void btnOver_MouseClick(object sender, MouseEventArgs e)
        {

            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                this.DialogResult = DialogResult.Cancel;
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




    }
}
