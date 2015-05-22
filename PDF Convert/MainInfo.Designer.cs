namespace PDF_Convert
{
    partial class MainInfo
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理全部正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainInfo));
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader6 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader7 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.panel2 = new System.Windows.Forms.Panel();
            this.plNavigation = new System.Windows.Forms.Panel();
            this.upbExcel2PDF = new Controls.ucPicButton();
            this.upbPPT2PDF = new Controls.ucPicButton();
            this.upbDoc2PDF = new Controls.ucPicButton();
            this.upbFile2IMG = new Controls.ucPicButton();
            this.upbFile2TXT = new Controls.ucPicButton();
            this.upbIMG2PDF = new Controls.ucPicButton();
            this.upbFile2HTML = new Controls.ucPicButton();
            this.upbFile2PPT = new Controls.ucPicButton();
            this.upbFile2Excel = new Controls.ucPicButton();
            this.upbFile2Word = new Controls.ucPicButton();
            this.panel3 = new System.Windows.Forms.Panel();
            this.pbLogo = new System.Windows.Forms.PictureBox();
            this.panel6 = new System.Windows.Forms.Panel();
            this.btnBrowse = new PDF_Convert.ucPicBrowseBar();
            this.btnStart = new PDF_Convert.ucPicButtonBar();
            this.plPPT = new System.Windows.Forms.Panel();
            this.txtHeight = new System.Windows.Forms.TextBox();
            this.lblHeight = new System.Windows.Forms.Label();
            this.txtWidth = new System.Windows.Forms.TextBox();
            this.lblWidth = new System.Windows.Forms.Label();
            this.lblPPTSize = new System.Windows.Forms.Label();
            this.txtOutPath = new PDF_Convert.ucTextBoxBar();
            this.cbIsMerger = new System.Windows.Forms.CheckBox();
            this.rdoNewPath = new System.Windows.Forms.RadioButton();
            this.rdoPath = new System.Windows.Forms.RadioButton();
            this.pltext = new System.Windows.Forms.Panel();
            this.comboBoxPage = new PDF_Convert.ucTextBoxListView();
            this.panel5 = new System.Windows.Forms.Panel();
            this.btnFolder = new PDF_Convert.ucPicButtonBar();
            this.btnAddFiles = new PDF_Convert.ucPicButtonBar();
            this.btnClear = new PDF_Convert.ucPicClearBar();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsmSoftwareUpgrade = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmLanguageSelection = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmCn = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmEnglish = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmAboutUs = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmBuy = new System.Windows.Forms.ToolStripMenuItem();
            this.lstFile = new Controls.ListViewPlus();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnReg = new PDF_Convert.ucPicNavigationBar();
            this.btnHelp = new PDF_Convert.ucPicNavigationBar();
            this.lblTitle = new System.Windows.Forms.Label();
            this.pbClose = new System.Windows.Forms.PictureBox();
            this.pbMinimize = new System.Windows.Forms.PictureBox();
            this.pbMenu = new System.Windows.Forms.PictureBox();
            this.panel7 = new System.Windows.Forms.Panel();
            this.btnPhone = new PDF_Convert.ucPicStatusBar();
            this.btnQQ = new PDF_Convert.ucPicStatusBar();
            this.btnBuy = new PDF_Convert.ucPicStatusBar();
            this.btnCourse = new PDF_Convert.ucPicStatusBar();
            this.panel2.SuspendLayout();
            this.plNavigation.SuspendLayout();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbLogo)).BeginInit();
            this.panel6.SuspendLayout();
            this.plPPT.SuspendLayout();
            this.pltext.SuspendLayout();
            this.panel5.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbClose)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbMinimize)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbMenu)).BeginInit();
            this.panel7.SuspendLayout();
            this.SuspendLayout();
            // 
            // openFileDialog
            // 
            this.openFileDialog.FileName = "*.PDF";
            this.openFileDialog.Multiselect = true;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.plNavigation);
            this.panel2.Controls.Add(this.panel3);
            resources.ApplyResources(this.panel2, "panel2");
            this.panel2.Name = "panel2";
            // 
            // plNavigation
            // 
            this.plNavigation.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(172)))), ((int)(((byte)(254)))));
            this.plNavigation.Controls.Add(this.upbExcel2PDF);
            this.plNavigation.Controls.Add(this.upbPPT2PDF);
            this.plNavigation.Controls.Add(this.upbDoc2PDF);
            this.plNavigation.Controls.Add(this.upbFile2IMG);
            this.plNavigation.Controls.Add(this.upbFile2TXT);
            this.plNavigation.Controls.Add(this.upbIMG2PDF);
            this.plNavigation.Controls.Add(this.upbFile2HTML);
            this.plNavigation.Controls.Add(this.upbFile2PPT);
            this.plNavigation.Controls.Add(this.upbFile2Excel);
            this.plNavigation.Controls.Add(this.upbFile2Word);
            resources.ApplyResources(this.plNavigation, "plNavigation");
            this.plNavigation.Name = "plNavigation";
            // 
            // upbExcel2PDF
            // 
            resources.ApplyResources(this.upbExcel2PDF, "upbExcel2PDF");
            this.upbExcel2PDF.BottomLine = true;
            this.upbExcel2PDF.ButtonImage = global::PDF_Convert.Properties.Resources.img;
            this.upbExcel2PDF.ButtonText = "ExcelToPDF";
            this.upbExcel2PDF.ButtonTextFont = new System.Drawing.Font("黑体", 13F);
            this.upbExcel2PDF.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(253)))));
            this.upbExcel2PDF.Name = "upbExcel2PDF";
            this.upbExcel2PDF.Selected = false;
            this.upbExcel2PDF.Click += new System.EventHandler(this.upbFile2Word_Click);
            // 
            // upbPPT2PDF
            // 
            resources.ApplyResources(this.upbPPT2PDF, "upbPPT2PDF");
            this.upbPPT2PDF.BottomLine = true;
            this.upbPPT2PDF.ButtonImage = global::PDF_Convert.Properties.Resources.img;
            this.upbPPT2PDF.ButtonText = "PptToPDF";
            this.upbPPT2PDF.ButtonTextFont = new System.Drawing.Font("黑体", 13F);
            this.upbPPT2PDF.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(253)))));
            this.upbPPT2PDF.Name = "upbPPT2PDF";
            this.upbPPT2PDF.Selected = false;
            this.upbPPT2PDF.Click += new System.EventHandler(this.upbFile2Word_Click);
            // 
            // upbDoc2PDF
            // 
            resources.ApplyResources(this.upbDoc2PDF, "upbDoc2PDF");
            this.upbDoc2PDF.BottomLine = true;
            this.upbDoc2PDF.ButtonImage = global::PDF_Convert.Properties.Resources.img;
            this.upbDoc2PDF.ButtonText = "WordToPDF";
            this.upbDoc2PDF.ButtonTextFont = new System.Drawing.Font("黑体", 13F);
            this.upbDoc2PDF.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(253)))));
            this.upbDoc2PDF.Name = "upbDoc2PDF";
            this.upbDoc2PDF.Selected = false;
            this.upbDoc2PDF.Click += new System.EventHandler(this.upbFile2Word_Click);
            // 
            // upbFile2IMG
            // 
            resources.ApplyResources(this.upbFile2IMG, "upbFile2IMG");
            this.upbFile2IMG.BottomLine = true;
            this.upbFile2IMG.ButtonImage = global::PDF_Convert.Properties.Resources.filetoimg;
            this.upbFile2IMG.ButtonText = "AnyToIMG";
            this.upbFile2IMG.ButtonTextFont = new System.Drawing.Font("黑体", 13F);
            this.upbFile2IMG.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(253)))));
            this.upbFile2IMG.Name = "upbFile2IMG";
            this.upbFile2IMG.Selected = false;
            this.upbFile2IMG.Click += new System.EventHandler(this.upbFile2Word_Click);
            // 
            // upbFile2TXT
            // 
            resources.ApplyResources(this.upbFile2TXT, "upbFile2TXT");
            this.upbFile2TXT.BottomLine = true;
            this.upbFile2TXT.ButtonImage = global::PDF_Convert.Properties.Resources.txt;
            this.upbFile2TXT.ButtonText = "AnyToTXT";
            this.upbFile2TXT.ButtonTextFont = new System.Drawing.Font("黑体", 13F);
            this.upbFile2TXT.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(253)))));
            this.upbFile2TXT.Name = "upbFile2TXT";
            this.upbFile2TXT.Selected = false;
            this.upbFile2TXT.Click += new System.EventHandler(this.upbFile2Word_Click);
            // 
            // upbIMG2PDF
            // 
            resources.ApplyResources(this.upbIMG2PDF, "upbIMG2PDF");
            this.upbIMG2PDF.BottomLine = true;
            this.upbIMG2PDF.ButtonImage = global::PDF_Convert.Properties.Resources.img;
            this.upbIMG2PDF.ButtonText = "IMGToPDF";
            this.upbIMG2PDF.ButtonTextFont = new System.Drawing.Font("黑体", 13F);
            this.upbIMG2PDF.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(253)))));
            this.upbIMG2PDF.Name = "upbIMG2PDF";
            this.upbIMG2PDF.Selected = false;
            this.upbIMG2PDF.Click += new System.EventHandler(this.upbFile2Word_Click);
            // 
            // upbFile2HTML
            // 
            resources.ApplyResources(this.upbFile2HTML, "upbFile2HTML");
            this.upbFile2HTML.BottomLine = true;
            this.upbFile2HTML.ButtonImage = global::PDF_Convert.Properties.Resources.html;
            this.upbFile2HTML.ButtonText = "AnyToHTML";
            this.upbFile2HTML.ButtonTextFont = new System.Drawing.Font("黑体", 13F);
            this.upbFile2HTML.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(253)))));
            this.upbFile2HTML.Name = "upbFile2HTML";
            this.upbFile2HTML.Selected = false;
            this.upbFile2HTML.Click += new System.EventHandler(this.upbFile2Word_Click);
            // 
            // upbFile2PPT
            // 
            resources.ApplyResources(this.upbFile2PPT, "upbFile2PPT");
            this.upbFile2PPT.BottomLine = true;
            this.upbFile2PPT.ButtonImage = global::PDF_Convert.Properties.Resources.ppt;
            this.upbFile2PPT.ButtonText = "AnyToPPT";
            this.upbFile2PPT.ButtonTextFont = new System.Drawing.Font("黑体", 13F);
            this.upbFile2PPT.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(253)))));
            this.upbFile2PPT.Name = "upbFile2PPT";
            this.upbFile2PPT.Selected = false;
            this.upbFile2PPT.Click += new System.EventHandler(this.upbFile2Word_Click);
            // 
            // upbFile2Excel
            // 
            resources.ApplyResources(this.upbFile2Excel, "upbFile2Excel");
            this.upbFile2Excel.BottomLine = true;
            this.upbFile2Excel.ButtonImage = global::PDF_Convert.Properties.Resources.excel;
            this.upbFile2Excel.ButtonText = "AnyToExcel";
            this.upbFile2Excel.ButtonTextFont = new System.Drawing.Font("黑体", 13F);
            this.upbFile2Excel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(253)))));
            this.upbFile2Excel.Name = "upbFile2Excel";
            this.upbFile2Excel.Selected = false;
            this.upbFile2Excel.Click += new System.EventHandler(this.upbFile2Word_Click);
            // 
            // upbFile2Word
            // 
            resources.ApplyResources(this.upbFile2Word, "upbFile2Word");
            this.upbFile2Word.BottomLine = true;
            this.upbFile2Word.ButtonImage = ((System.Drawing.Image)(resources.GetObject("upbFile2Word.ButtonImage")));
            this.upbFile2Word.ButtonText = "AnyToWord";
            this.upbFile2Word.ButtonTextFont = new System.Drawing.Font("黑体", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.upbFile2Word.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(253)))));
            this.upbFile2Word.Name = "upbFile2Word";
            this.upbFile2Word.Selected = false;
            this.upbFile2Word.Click += new System.EventHandler(this.upbFile2Word_Click);
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.White;
            this.panel3.BackgroundImage = global::PDF_Convert.Properties.Resources.leftbg_01;
            this.panel3.Controls.Add(this.pbLogo);
            resources.ApplyResources(this.panel3, "panel3");
            this.panel3.Name = "panel3";
            this.panel3.Paint += new System.Windows.Forms.PaintEventHandler(this.panel3_Paint);
            // 
            // pbLogo
            // 
            this.pbLogo.BackColor = System.Drawing.Color.Transparent;
            this.pbLogo.BackgroundImage = global::PDF_Convert.Properties.Resources.logo_050;
            resources.ApplyResources(this.pbLogo, "pbLogo");
            this.pbLogo.Name = "pbLogo";
            this.pbLogo.TabStop = false;
            this.pbLogo.Click += new System.EventHandler(this.pbLogo_Click);
            // 
            // panel6
            // 
            this.panel6.BackColor = System.Drawing.Color.White;
            this.panel6.Controls.Add(this.btnBrowse);
            this.panel6.Controls.Add(this.btnStart);
            this.panel6.Controls.Add(this.plPPT);
            this.panel6.Controls.Add(this.txtOutPath);
            this.panel6.Controls.Add(this.cbIsMerger);
            this.panel6.Controls.Add(this.rdoNewPath);
            this.panel6.Controls.Add(this.rdoPath);
            resources.ApplyResources(this.panel6, "panel6");
            this.panel6.Name = "panel6";
            // 
            // btnBrowse
            // 
            resources.ApplyResources(this.btnBrowse, "btnBrowse");
            this.btnBrowse.ButtonBackIMG = ((System.Drawing.Image)(resources.GetObject("btnBrowse.ButtonBackIMG")));
            this.btnBrowse.ButtonForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.btnBrowse.ButtonText = "浏览";
            this.btnBrowse.ButtonTextFont = new System.Drawing.Font("微软雅黑", 10F);
            this.btnBrowse.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnBrowse.IsEnable = true;
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.MouseClick += new System.Windows.Forms.MouseEventHandler(this.btnBrowse_MouseClick);
            this.btnBrowse.MouseEnter += new System.EventHandler(this.btnBrowse_MouseEnter);
            this.btnBrowse.MouseLeave += new System.EventHandler(this.btnBrowse_MouseLeave);
            // 
            // btnStart
            // 
            this.btnStart.BackgroundImage = global::PDF_Convert.Properties.Resources.startConversion;
            this.btnStart.ButtonBackIMG = global::PDF_Convert.Properties.Resources.startConversion;
            this.btnStart.ButtonText = "开始转换";
            this.btnStart.Cursor = System.Windows.Forms.Cursors.Hand;
            resources.ApplyResources(this.btnStart, "btnStart");
            this.btnStart.Name = "btnStart";
            this.btnStart.MouseClick += new System.Windows.Forms.MouseEventHandler(this.btnStart_MouseClick);
            this.btnStart.MouseEnter += new System.EventHandler(this.btnStart_MouseEnter);
            this.btnStart.MouseLeave += new System.EventHandler(this.btnStart_MouseLeave);
            // 
            // plPPT
            // 
            this.plPPT.Controls.Add(this.txtHeight);
            this.plPPT.Controls.Add(this.lblHeight);
            this.plPPT.Controls.Add(this.txtWidth);
            this.plPPT.Controls.Add(this.lblWidth);
            this.plPPT.Controls.Add(this.lblPPTSize);
            resources.ApplyResources(this.plPPT, "plPPT");
            this.plPPT.Name = "plPPT";
            // 
            // txtHeight
            // 
            resources.ApplyResources(this.txtHeight, "txtHeight");
            this.txtHeight.Name = "txtHeight";
            this.txtHeight.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtHeight_KeyPress);
            // 
            // lblHeight
            // 
            resources.ApplyResources(this.lblHeight, "lblHeight");
            this.lblHeight.Name = "lblHeight";
            // 
            // txtWidth
            // 
            resources.ApplyResources(this.txtWidth, "txtWidth");
            this.txtWidth.Name = "txtWidth";
            this.txtWidth.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtWidth_KeyPress);
            // 
            // lblWidth
            // 
            resources.ApplyResources(this.lblWidth, "lblWidth");
            this.lblWidth.Name = "lblWidth";
            // 
            // lblPPTSize
            // 
            resources.ApplyResources(this.lblPPTSize, "lblPPTSize");
            this.lblPPTSize.Name = "lblPPTSize";
            // 
            // txtOutPath
            // 
            this.txtOutPath.BackColor = System.Drawing.Color.White;
            resources.ApplyResources(this.txtOutPath, "txtOutPath");
            this.txtOutPath.IsReadOnly = true;
            this.txtOutPath.Name = "txtOutPath";
            this.txtOutPath.OutText = "D:\\";
            // 
            // cbIsMerger
            // 
            resources.ApplyResources(this.cbIsMerger, "cbIsMerger");
            this.cbIsMerger.BackColor = System.Drawing.Color.Transparent;
            this.cbIsMerger.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.cbIsMerger.Name = "cbIsMerger";
            this.cbIsMerger.UseVisualStyleBackColor = false;
            // 
            // rdoNewPath
            // 
            resources.ApplyResources(this.rdoNewPath, "rdoNewPath");
            this.rdoNewPath.BackColor = System.Drawing.Color.Transparent;
            this.rdoNewPath.Name = "rdoNewPath";
            this.rdoNewPath.UseVisualStyleBackColor = false;
            this.rdoNewPath.MouseClick += new System.Windows.Forms.MouseEventHandler(this.rdoNewPath_MouseClick);
            // 
            // rdoPath
            // 
            resources.ApplyResources(this.rdoPath, "rdoPath");
            this.rdoPath.BackColor = System.Drawing.Color.Transparent;
            this.rdoPath.ForeColor = System.Drawing.Color.Black;
            this.rdoPath.Name = "rdoPath";
            this.rdoPath.UseVisualStyleBackColor = false;
            this.rdoPath.MouseClick += new System.Windows.Forms.MouseEventHandler(this.rdoPath_MouseClick);
            // 
            // pltext
            // 
            this.pltext.BackColor = System.Drawing.SystemColors.Window;
            this.pltext.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pltext.Controls.Add(this.comboBoxPage);
            resources.ApplyResources(this.pltext, "pltext");
            this.pltext.Name = "pltext";
            // 
            // comboBoxPage
            // 
            resources.ApplyResources(this.comboBoxPage, "comboBoxPage");
            this.comboBoxPage.BackGroundText = "请输入,如2-17";
            this.comboBoxPage.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.comboBoxPage.Img = null;
            this.comboBoxPage.Name = "comboBoxPage";
            this.comboBoxPage.KeyDown += new System.Windows.Forms.KeyEventHandler(this.comboBoxPage_KeyUp);
            this.comboBoxPage.Leave += new System.EventHandler(this.comboBoxPage_Leave);
            // 
            // panel5
            // 
            this.panel5.BackColor = System.Drawing.Color.White;
            this.panel5.Controls.Add(this.btnFolder);
            this.panel5.Controls.Add(this.btnAddFiles);
            this.panel5.Controls.Add(this.btnClear);
            resources.ApplyResources(this.panel5, "panel5");
            this.panel5.Name = "panel5";
            // 
            // btnFolder
            // 
            this.btnFolder.BackgroundImage = global::PDF_Convert.Properties.Resources.addFolder;
            this.btnFolder.ButtonBackIMG = global::PDF_Convert.Properties.Resources.addFolder;
            this.btnFolder.ButtonText = "添加文件夹";
            this.btnFolder.Cursor = System.Windows.Forms.Cursors.Hand;
            resources.ApplyResources(this.btnFolder, "btnFolder");
            this.btnFolder.Name = "btnFolder";
            this.btnFolder.MouseClick += new System.Windows.Forms.MouseEventHandler(this.btnFolder_MouseClick);
            this.btnFolder.MouseEnter += new System.EventHandler(this.btnFolder_MouseEnter);
            this.btnFolder.MouseLeave += new System.EventHandler(this.btnFolder_MouseLeave);
            // 
            // btnAddFiles
            // 
            resources.ApplyResources(this.btnAddFiles, "btnAddFiles");
            this.btnAddFiles.ButtonBackIMG = ((System.Drawing.Image)(resources.GetObject("btnAddFiles.ButtonBackIMG")));
            this.btnAddFiles.ButtonText = "添加文档";
            this.btnAddFiles.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAddFiles.Name = "btnAddFiles";
            this.btnAddFiles.MouseClick += new System.Windows.Forms.MouseEventHandler(this.btnAddFiles_MouseClick);
            this.btnAddFiles.MouseEnter += new System.EventHandler(this.btnAddFiles_MouseEnter);
            this.btnAddFiles.MouseLeave += new System.EventHandler(this.btnAddFiles_MouseLeave);
            // 
            // btnClear
            // 
            this.btnClear.ButtonImage = ((System.Drawing.Image)(resources.GetObject("btnClear.ButtonImage")));
            this.btnClear.ButtonText = "Clear List";
            this.btnClear.Cursor = System.Windows.Forms.Cursors.Hand;
            resources.ApplyResources(this.btnClear, "btnClear");
            this.btnClear.Name = "btnClear";
            this.btnClear.MouseClick += new System.Windows.Forms.MouseEventHandler(this.btnClear_MouseClick);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmSoftwareUpgrade,
            this.tsmLanguageSelection,
            this.tsmAboutUs,
            this.tsmBuy});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            resources.ApplyResources(this.contextMenuStrip1, "contextMenuStrip1");
            // 
            // tsmSoftwareUpgrade
            // 
            this.tsmSoftwareUpgrade.Name = "tsmSoftwareUpgrade";
            resources.ApplyResources(this.tsmSoftwareUpgrade, "tsmSoftwareUpgrade");
            this.tsmSoftwareUpgrade.Click += new System.EventHandler(this.tsmSoftwareUpgrade_Click);
            // 
            // tsmLanguageSelection
            // 
            this.tsmLanguageSelection.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmCn,
            this.tsmEnglish});
            this.tsmLanguageSelection.Name = "tsmLanguageSelection";
            resources.ApplyResources(this.tsmLanguageSelection, "tsmLanguageSelection");
            // 
            // tsmCn
            // 
            this.tsmCn.Name = "tsmCn";
            resources.ApplyResources(this.tsmCn, "tsmCn");
            this.tsmCn.Click += new System.EventHandler(this.tsmCn_Click);
            // 
            // tsmEnglish
            // 
            this.tsmEnglish.Name = "tsmEnglish";
            resources.ApplyResources(this.tsmEnglish, "tsmEnglish");
            this.tsmEnglish.Click += new System.EventHandler(this.tsmEnglish_Click);
            // 
            // tsmAboutUs
            // 
            this.tsmAboutUs.Name = "tsmAboutUs";
            resources.ApplyResources(this.tsmAboutUs, "tsmAboutUs");
            this.tsmAboutUs.Click += new System.EventHandler(this.tsmAboutUs_Click);
            // 
            // tsmBuy
            // 
            this.tsmBuy.Name = "tsmBuy";
            resources.ApplyResources(this.tsmBuy, "tsmBuy");
            this.tsmBuy.Click += new System.EventHandler(this.tsmBuy_Click);
            // 
            // lstFile
            // 
            this.lstFile.AllowDrop = true;
            this.lstFile.ConversionPageDefaultText = null;
            this.lstFile.FileNameText = "文件名称";
            this.lstFile.FileNameWidth = 275;
            resources.ApplyResources(this.lstFile, "lstFile");
            this.lstFile.FullRowSelect = true;
            this.lstFile.GridLines = true;
            this.lstFile.IndexText = "编号";
            this.lstFile.IndexWidth = 55;
            this.lstFile.ItemFont = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lstFile.Name = "lstFile";
            this.lstFile.OpenButtonText = "打开";
            this.lstFile.OperateText = "操作";
            this.lstFile.OperateWidth = 130;
            this.lstFile.OwnerDraw = true;
            this.lstFile.SelectPageText = "选择页码";
            this.lstFile.SelectPageWidth = 130;
            this.lstFile.StatusText = "状态";
            this.lstFile.StatusWidth = 140;
            this.lstFile.UseCompatibleStateImageBehavior = false;
            this.lstFile.View = System.Windows.Forms.View.Details;
            this.lstFile.OnStatusButtonClicked += new Controls.ListViewPlus.StatusChangedDelegate(this.lstFile_OnStatusButtonClicked);
            this.lstFile.OnOpenFileButtonClicked += new Controls.ListViewPlus.OpenFileDelegate(this.lstFile_OnOpenFileButtonClicked);
            this.lstFile.OnOpenDirectoryButtonClicked += new Controls.ListViewPlus.OpenDirectoryDelegate(this.lstFile_OnOpenDirectoryButtonClicked);
            this.lstFile.OnDeleteButtonClicked += new Controls.ListViewPlus.DeleteButtonDelegate(this.lstFile_OnDeleteButtonClicked);
            this.lstFile.DragDrop += new System.Windows.Forms.DragEventHandler(this.lstFile_DragDrop);
            this.lstFile.DragEnter += new System.Windows.Forms.DragEventHandler(this.lstFile_DragEnter);
            this.lstFile.DragOver += new System.Windows.Forms.DragEventHandler(this.lstFile_DragOver);
            this.lstFile.DragLeave += new System.EventHandler(this.lstFile_DragLeave);
            this.lstFile.MouseClick += new System.Windows.Forms.MouseEventHandler(this.lstFile_MouseClick);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Transparent;
            this.panel1.BackgroundImage = global::PDF_Convert.Properties.Resources.topbg_02;
            this.panel1.Controls.Add(this.btnReg);
            this.panel1.Controls.Add(this.btnHelp);
            this.panel1.Controls.Add(this.lblTitle);
            this.panel1.Controls.Add(this.pbClose);
            this.panel1.Controls.Add(this.pbMinimize);
            this.panel1.Controls.Add(this.pbMenu);
            resources.ApplyResources(this.panel1, "panel1");
            this.panel1.Name = "panel1";
            this.panel1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.MainInfo_MouseDown);
            // 
            // btnReg
            // 
            this.btnReg.ButtonImage = ((System.Drawing.Image)(resources.GetObject("btnReg.ButtonImage")));
            this.btnReg.ButtonText = "Help";
            this.btnReg.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnReg.FromType = 1;
            resources.ApplyResources(this.btnReg, "btnReg");
            this.btnReg.Name = "btnReg";
            this.btnReg.MouseClick += new System.Windows.Forms.MouseEventHandler(this.btnReg_MouseClick);
            // 
            // btnHelp
            // 
            this.btnHelp.ButtonImage = global::PDF_Convert.Properties.Resources.help;
            this.btnHelp.ButtonText = "Help";
            this.btnHelp.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnHelp.FromType = 2;
            resources.ApplyResources(this.btnHelp, "btnHelp");
            this.btnHelp.Name = "btnHelp";
            this.btnHelp.MouseClick += new System.Windows.Forms.MouseEventHandler(this.btnHelp_MouseClick);
            // 
            // lblTitle
            // 
            resources.ApplyResources(this.lblTitle, "lblTitle");
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(150)))), ((int)(((byte)(150)))), ((int)(((byte)(150)))));
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Click += new System.EventHandler(this.lblTitle_Click);
            this.lblTitle.MouseDown += new System.Windows.Forms.MouseEventHandler(this.MainInfo_MouseDown);
            // 
            // pbClose
            // 
            this.pbClose.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pbClose.Image = global::PDF_Convert.Properties.Resources.close;
            resources.ApplyResources(this.pbClose, "pbClose");
            this.pbClose.Name = "pbClose";
            this.pbClose.TabStop = false;
            this.pbClose.MouseClick += new System.Windows.Forms.MouseEventHandler(this.pbClose_MouseClick);
            // 
            // pbMinimize
            // 
            this.pbMinimize.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pbMinimize.Image = global::PDF_Convert.Properties.Resources.small;
            resources.ApplyResources(this.pbMinimize, "pbMinimize");
            this.pbMinimize.Name = "pbMinimize";
            this.pbMinimize.TabStop = false;
            this.pbMinimize.MouseClick += new System.Windows.Forms.MouseEventHandler(this.pbMinimize_MouseClick);
            // 
            // pbMenu
            // 
            this.pbMenu.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pbMenu.Image = global::PDF_Convert.Properties.Resources.big;
            resources.ApplyResources(this.pbMenu, "pbMenu");
            this.pbMenu.Name = "pbMenu";
            this.pbMenu.TabStop = false;
            this.pbMenu.MouseClick += new System.Windows.Forms.MouseEventHandler(this.pictureBox3_MouseClick);
            // 
            // panel7
            // 
            this.panel7.BackColor = System.Drawing.Color.Transparent;
            this.panel7.BackgroundImage = global::PDF_Convert.Properties.Resources.bootombg_13;
            this.panel7.Controls.Add(this.btnPhone);
            this.panel7.Controls.Add(this.btnQQ);
            this.panel7.Controls.Add(this.btnBuy);
            this.panel7.Controls.Add(this.btnCourse);
            resources.ApplyResources(this.panel7, "panel7");
            this.panel7.Name = "panel7";
            this.panel7.Paint += new System.Windows.Forms.PaintEventHandler(this.panel7_Paint);
            // 
            // btnPhone
            // 
            this.btnPhone.ButtonImage = global::PDF_Convert.Properties.Resources.phone;
            this.btnPhone.ButtonText = "400-668-5572 / 181-2107-4602";
            this.btnPhone.ButtonTextFont = new System.Drawing.Font("微软雅黑", 12F);
            this.btnPhone.Cursor = System.Windows.Forms.Cursors.Default;
            resources.ApplyResources(this.btnPhone, "btnPhone");
            this.btnPhone.FromType = 3;
            this.btnPhone.Name = "btnPhone";
            // 
            // btnQQ
            // 
            this.btnQQ.ButtonImage = global::PDF_Convert.Properties.Resources.qq;
            this.btnQQ.ButtonText = "support@prosooner.com";
            this.btnQQ.ButtonTextFont = new System.Drawing.Font("微软雅黑", 12F);
            this.btnQQ.Cursor = System.Windows.Forms.Cursors.Hand;
            resources.ApplyResources(this.btnQQ, "btnQQ");
            this.btnQQ.FromType = 2;
            this.btnQQ.Name = "btnQQ";
            this.btnQQ.MouseClick += new System.Windows.Forms.MouseEventHandler(this.btnQQ_MouseClick);
            // 
            // btnBuy
            // 
            this.btnBuy.ButtonImage = global::PDF_Convert.Properties.Resources.money;
            this.btnBuy.ButtonText = "Purchase";
            this.btnBuy.ButtonTextFont = new System.Drawing.Font("微软雅黑", 12F);
            this.btnBuy.Cursor = System.Windows.Forms.Cursors.Hand;
            resources.ApplyResources(this.btnBuy, "btnBuy");
            this.btnBuy.FromType = 1;
            this.btnBuy.Name = "btnBuy";
            this.btnBuy.MouseClick += new System.Windows.Forms.MouseEventHandler(this.btnBuy_MouseClick);
            // 
            // btnCourse
            // 
            this.btnCourse.ButtonImage = ((System.Drawing.Image)(resources.GetObject("btnCourse.ButtonImage")));
            this.btnCourse.ButtonText = "Help";
            this.btnCourse.ButtonTextFont = new System.Drawing.Font("微软雅黑", 12F);
            this.btnCourse.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCourse.FromType = 0;
            resources.ApplyResources(this.btnCourse, "btnCourse");
            this.btnCourse.Name = "btnCourse";
            this.btnCourse.MouseClick += new System.Windows.Forms.MouseEventHandler(this.btnCourse_MouseClick);
            // 
            // MainInfo
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pltext);
            this.Controls.Add(this.panel5);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel6);
            this.Controls.Add(this.panel7);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.lstFile);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.Name = "MainInfo";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainInfo_FormClosing);
            this.Load += new System.EventHandler(this.MainInfo_Load);
            this.panel2.ResumeLayout(false);
            this.plNavigation.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbLogo)).EndInit();
            this.panel6.ResumeLayout(false);
            this.panel6.PerformLayout();
            this.plPPT.ResumeLayout(false);
            this.plPPT.PerformLayout();
            this.pltext.ResumeLayout(false);
            this.pltext.PerformLayout();
            this.panel5.ResumeLayout(false);
            this.contextMenuStrip1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbClose)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbMinimize)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbMenu)).EndInit();
            this.panel7.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader6;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader5;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.ColumnHeader columnHeader7;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel plNavigation;
        private Controls.ucPicButton upbIMG2PDF;
        private Controls.ucPicButton upbFile2HTML;
        private Controls.ucPicButton upbFile2PPT;
        private Controls.ucPicButton upbFile2Excel;
        private Controls.ucPicButton upbFile2Word;
        private System.Windows.Forms.Panel panel3;
        private Controls.ucPicButton upbPPT2PDF;
        private Controls.ucPicButton upbDoc2PDF;
        private Controls.ucPicButton upbFile2IMG;
        private Controls.ucPicButton upbFile2TXT;
        private System.Windows.Forms.Panel panel7;
        private Controls.ucPicButton upbExcel2PDF;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.RadioButton rdoPath;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.PictureBox pbLogo;
        private System.Windows.Forms.PictureBox pbMenu;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem tsmSoftwareUpgrade;
        private System.Windows.Forms.ToolStripMenuItem tsmLanguageSelection;
        private System.Windows.Forms.ToolStripMenuItem tsmCn;
        private System.Windows.Forms.ToolStripMenuItem tsmEnglish;
        private System.Windows.Forms.ToolStripMenuItem tsmAboutUs;
        private System.Windows.Forms.ToolStripMenuItem tsmBuy;
        private System.Windows.Forms.PictureBox pbMinimize;
        private System.Windows.Forms.PictureBox pbClose;
        private System.Windows.Forms.Label lblTitle;
        private ucPicStatusBar btnCourse;
        private ucPicStatusBar btnBuy;
        private ucPicNavigationBar btnHelp;
        private ucPicClearBar btnClear;
        private ucPicStatusBar btnPhone;
        private ucPicStatusBar btnQQ;
        public ucTextBoxBar txtOutPath;
        public Controls.ListViewPlus lstFile;
        private System.Windows.Forms.RadioButton rdoNewPath;
        private System.Windows.Forms.Panel plPPT;
        private System.Windows.Forms.Label lblHeight;
        private System.Windows.Forms.Label lblWidth;
        private System.Windows.Forms.Label lblPPTSize;
        public System.Windows.Forms.TextBox txtHeight;
        public System.Windows.Forms.TextBox txtWidth;
        public System.Windows.Forms.CheckBox cbIsMerger;
        private ucTextBoxListView comboBoxPage;
        private System.Windows.Forms.Panel pltext;
        private ucPicButtonBar btnAddFiles;
        private ucPicButtonBar btnFolder;
        public ucPicButtonBar btnStart;
        private ucPicBrowseBar btnBrowse;
        private ucPicNavigationBar btnReg;
    }
}

