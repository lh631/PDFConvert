namespace PDF_Convert
{
    partial class RegDlg
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RegDlg));
            this.panel1 = new System.Windows.Forms.Panel();
            this.pbLogo = new System.Windows.Forms.PictureBox();
            this.pbClose = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.btnPhone = new PDF_Convert.ucPicStatusBar();
            this.btnQQ = new PDF_Convert.ucPicStatusBar();
            this.btnReg = new PDF_Convert.ucPicBrowseBar();
            this.btnActive = new PDF_Convert.ucPicBrowseBar();
            this.txtRegCode = new PDF_Convert.ucTextBoxBar();
            this.txtMachineCode = new PDF_Convert.ucTextBoxBar();
            this.btnBrowse = new PDF_Convert.ucPicBrowseBar();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbLogo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbClose)).BeginInit();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(189)))), ((int)(((byte)(255)))));
            this.panel1.Controls.Add(this.pbLogo);
            this.panel1.Controls.Add(this.pbClose);
            resources.ApplyResources(this.panel1, "panel1");
            this.panel1.Name = "panel1";
            this.panel1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panel1_MouseDown);
            // 
            // pbLogo
            // 
            this.pbLogo.BackColor = System.Drawing.Color.Transparent;
            this.pbLogo.BackgroundImage = global::PDF_Convert.Properties.Resources.logo_03;
            resources.ApplyResources(this.pbLogo, "pbLogo");
            this.pbLogo.Name = "pbLogo";
            this.pbLogo.TabStop = false;
            // 
            // pbClose
            // 
            this.pbClose.BackColor = System.Drawing.Color.Transparent;
            this.pbClose.BackgroundImage = global::PDF_Convert.Properties.Resources.regClose;
            this.pbClose.Cursor = System.Windows.Forms.Cursors.Hand;
            resources.ApplyResources(this.pbClose, "pbClose");
            this.pbClose.Name = "pbClose";
            this.pbClose.TabStop = false;
            this.pbClose.Click += new System.EventHandler(this.pbClose_Click);
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(102)))), ((int)(((byte)(2)))));
            this.label3.Name = "label3";
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.Name = "label4";
            // 
            // label5
            // 
            resources.ApplyResources(this.label5, "label5");
            this.label5.Name = "label5";
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(231)))), ((int)(((byte)(231)))));
            this.panel2.Controls.Add(this.label7);
            this.panel2.Controls.Add(this.label6);
            resources.ApplyResources(this.panel2, "panel2");
            this.panel2.Name = "panel2";
            this.panel2.Paint += new System.Windows.Forms.PaintEventHandler(this.panel2_Paint);
            // 
            // label7
            // 
            resources.ApplyResources(this.label7, "label7");
            this.label7.BackColor = System.Drawing.Color.Transparent;
            this.label7.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(135)))), ((int)(((byte)(135)))), ((int)(((byte)(135)))));
            this.label7.Name = "label7";
            // 
            // label6
            // 
            resources.ApplyResources(this.label6, "label6");
            this.label6.BackColor = System.Drawing.Color.Transparent;
            this.label6.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(135)))), ((int)(((byte)(135)))), ((int)(((byte)(135)))));
            this.label6.Name = "label6";
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = global::PDF_Convert.Properties.Resources.registrlogo;
            resources.ApplyResources(this.pictureBox2, "pictureBox2");
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.TabStop = false;
            // 
            // btnPhone
            // 
            this.btnPhone.ButtonImage = global::PDF_Convert.Properties.Resources.phonecom;
            this.btnPhone.ButtonText = "400-668-5572 / 181-2107-4602";
            this.btnPhone.ButtonTextFont = new System.Drawing.Font("微软雅黑", 11F);
            this.btnPhone.Cursor = System.Windows.Forms.Cursors.Default;
            resources.ApplyResources(this.btnPhone, "btnPhone");
            this.btnPhone.FromType = 5;
            this.btnPhone.Name = "btnPhone";
            // 
            // btnQQ
            // 
            this.btnQQ.ButtonImage = global::PDF_Convert.Properties.Resources.qqcn;
            this.btnQQ.ButtonText = "在线QQ:4006685572";
            this.btnQQ.ButtonTextFont = new System.Drawing.Font("微软雅黑", 11F);
            this.btnQQ.Cursor = System.Windows.Forms.Cursors.Hand;
            resources.ApplyResources(this.btnQQ, "btnQQ");
            this.btnQQ.FromType = 4;
            this.btnQQ.Name = "btnQQ";
            this.btnQQ.MouseClick += new System.Windows.Forms.MouseEventHandler(this.btnQQ_MouseClick);
            // 
            // btnReg
            // 
            this.btnReg.BackgroundImage = global::PDF_Convert.Properties.Resources.zhuce;
            this.btnReg.ButtonBackIMG = global::PDF_Convert.Properties.Resources.zhuce;
            this.btnReg.ButtonForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.btnReg.ButtonText = "购买正式版";
            this.btnReg.ButtonTextFont = new System.Drawing.Font("微软雅黑", 10F, System.Drawing.FontStyle.Bold);
            this.btnReg.CausesValidation = false;
            this.btnReg.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnReg.IsEnable = true;
            resources.ApplyResources(this.btnReg, "btnReg");
            this.btnReg.Name = "btnReg";
            this.btnReg.MouseClick += new System.Windows.Forms.MouseEventHandler(this.btnReg_MouseClick);
            // 
            // btnActive
            // 
            this.btnActive.BackgroundImage = global::PDF_Convert.Properties.Resources.resisterbut;
            this.btnActive.ButtonBackIMG = global::PDF_Convert.Properties.Resources.resisterbut;
            this.btnActive.ButtonForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.btnActive.ButtonText = "开始激活";
            this.btnActive.ButtonTextFont = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold);
            this.btnActive.CausesValidation = false;
            this.btnActive.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnActive.IsEnable = true;
            resources.ApplyResources(this.btnActive, "btnActive");
            this.btnActive.Name = "btnActive";
            this.btnActive.MouseClick += new System.Windows.Forms.MouseEventHandler(this.btnActive_MouseClick);
            this.btnActive.MouseEnter += new System.EventHandler(this.btnActive_MouseEnter);
            this.btnActive.MouseLeave += new System.EventHandler(this.btnActive_MouseLeave);
            // 
            // txtRegCode
            // 
            resources.ApplyResources(this.txtRegCode, "txtRegCode");
            this.txtRegCode.IsReadOnly = false;
            this.txtRegCode.Name = "txtRegCode";
            this.txtRegCode.OutText = "";
            // 
            // txtMachineCode
            // 
            this.txtMachineCode.BackColor = System.Drawing.Color.White;
            resources.ApplyResources(this.txtMachineCode, "txtMachineCode");
            this.txtMachineCode.IsReadOnly = true;
            this.txtMachineCode.Name = "txtMachineCode";
            this.txtMachineCode.OutText = "";
            // 
            // btnBrowse
            // 
            resources.ApplyResources(this.btnBrowse, "btnBrowse");
            this.btnBrowse.ButtonBackIMG = ((System.Drawing.Image)(resources.GetObject("btnBrowse.ButtonBackIMG")));
            this.btnBrowse.ButtonForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.btnBrowse.ButtonText = "浏览";
            this.btnBrowse.ButtonTextFont = new System.Drawing.Font("微软雅黑", 10F);
            this.btnBrowse.IsEnable = true;
            this.btnBrowse.Name = "btnBrowse";
            // 
            // RegDlg
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.btnPhone);
            this.Controls.Add(this.btnQQ);
            this.Controls.Add(this.btnReg);
            this.Controls.Add(this.btnActive);
            this.Controls.Add(this.txtRegCode);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.txtMachineCode);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "RegDlg";
            this.Load += new System.EventHandler(this.RegDlg_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.RegDlg_Paint);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbLogo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbClose)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.PictureBox pbClose;
        private System.Windows.Forms.PictureBox pbLogo;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        public ucTextBoxBar txtMachineCode;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private ucTextBoxBar txtRegCode;
        public ucPicBrowseBar btnBrowse;
        public ucPicBrowseBar btnActive;
        public ucPicBrowseBar btnReg;
        private ucPicStatusBar btnPhone;
        private ucPicStatusBar btnQQ;
    }
}