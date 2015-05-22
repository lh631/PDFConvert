using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Management;
using Controls;
using System.Runtime.InteropServices;
using System.Globalization;
using System.Resources;
using System.Diagnostics;

namespace PDF_Convert
{

    public partial class MainInfo : Form
    {
        [DllImport("user32")]
        public static extern int ReleaseCapture();
        [DllImport("user32")]
        public static extern int SendMessage(IntPtr hwnd, int msg, int wp, int lp);
        //物理CPU核数
        int cpuNumber = 1;
        //逻辑CPU核数
        int cpuLogicalNumber = 1;
        //文件队列
        public Queue<ListViewItem> fileQueue = new Queue<ListViewItem>();
        //导航栏目
        Convert.FORMAT format = new Convert.FORMAT();
        private SynchronizationContext syncContext = null;
        //列表集合
        public Dictionary<string, bool> diclst = new Dictionary<string, bool>();
        public bool isClose = false;
        //是否已注册
        public static bool isReg = false;
        ini_config ini = new ini_config("config.ini");
        string language = string.Empty;
        public static ResourceManager rm = new ResourceManager(typeof(MainInfo));
        Thread[] thread;
        //线程管理当前文件集合
        public Dictionary<string, int> dicThreadManagement = new Dictionary<string, int>();
        string encodingCode = string.Empty;
        public MainInfo()
        {

            language = ini.read_ini("language");
            if (string.IsNullOrEmpty(language))
                language = System.Globalization.CultureInfo.InstalledUICulture.Name;
            encodingCode = new reg().get_machine_code();
            syncContext = SynchronizationContext.Current;
            // Thread.CurrentThread.CurrentUICulture = new CultureInfo(language);
            Control.CheckForIllegalCrossThreadCalls = false;
            InitializeComponent();
            switch (language.ToLower())
            {
                case "zh-cn":
                    SetZhCn();
                    break;
                case "en":
                    SetEn();
                    break;
                default:
                    SetZhCn();
                    break;
            }
        }

        private void MainInfo_Load(object sender, EventArgs e)
        {

            //comboBoxPage.Width = lstFile.Columns[2].Width;
            //lstFile.Controls.Add(comboBoxPage);
            try
            {
                ManagementClass c = new ManagementClass(
  new ManagementPath("Win32_Processor"));
                // Get the properties in the class
                ManagementObjectCollection moc = c.GetInstances();
                foreach (ManagementObject mo in moc)
                {
                    PropertyDataCollection properties = mo.Properties;
                    cpuNumber = System.Convert.ToInt32(properties["NumberOfCores"].Value);
                    cpuLogicalNumber = System.Convert.ToInt32(properties["NumberOfLogicalProcessors"].Value);
                    cpuNumber = cpuNumber >= cpuLogicalNumber ? cpuNumber : cpuLogicalNumber;
                    if (cpuNumber > 1)
                    {
                        cpuNumber = cpuNumber - 1;
                    }


                }
                //图片是否合并
                string isMerger = ini.read_ini("isMerger");
                cbIsMerger.Checked = isMerger == "1" ? true : false;
                if (new reg().Is_Reg())
                {
                    this.lblTitle.Text = Encrypt.APPTITLE + " " + rm.GetString("OfficialVersion") + " v" + Version.version;
                    isReg = true;
                    // pltext.Visible = true;
                }
                else
                {
                    this.lblTitle.Text = Encrypt.APPTITLE + " " + rm.GetString("FreeTrialVersion") + " v" + Version.version;
                    isReg = false;
                    //pltext.Visible = false;

                }
                //菜单导航功能按钮选择
                string type = ini.read_ini("Type");
                if (!string.IsNullOrEmpty(type))
                {
                    int select = 0;
                    int.TryParse(type, out select);
                    MenuSeletect(select);
                }
                //保存文本框路径
                string targetDic = ini.read_ini("TargetDic");
                if (targetDic != string.Empty)
                {
                    this.txtOutPath.OutText = targetDic;
                }
                else
                {
                    this.txtOutPath.OutText = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\";
                }
                //输出路径默认选择
                string out_ = ini.read_ini("Out");
                if (string.IsNullOrEmpty(out_) || out_ == "1")
                {
                    this.rdoNewPath.Checked = false;
                    this.rdoPath.Checked = true;
                    this.btnBrowse.IsEnable = false;
                    this.btnBrowse.ButtonBackIMG = Properties.Resources.lookEnable;
                }
                else
                {
                    this.rdoNewPath.Checked = true;
                    this.rdoPath.Checked = false;
                    this.btnBrowse.IsEnable = true;
                    this.btnBrowse.ButtonBackIMG = Properties.Resources.look;
                }
                this.txtWidth.Text = ini.read_ini("PicX") == string.Empty ? "700" : ini.read_ini("PicX");
                this.txtHeight.Text = ini.read_ini("PicY") == string.Empty ? "500" : ini.read_ini("PicY");
                thread = new Thread[cpuNumber];
                for (int i = 0; i < thread.Length; i++)
                {

                    thread[i] = new Thread(new ParameterizedThreadStart(WorkThread));
                    thread[i].IsBackground = true;
                    thread[i].Start(i);
                }
            }
            catch { }



        }

        /// <summary>
        /// 设置简体中文语言
        /// </summary>
        private void SetZhCn()
        {
            Thread.CurrentThread.CurrentUICulture = new CultureInfo("zh-CN");
            this.upbFile2Word.ButtonText = rm.GetString("upbFile2Word");
            this.upbFile2Excel.ButtonText = rm.GetString("upbFile2Excel");
            this.upbFile2PPT.ButtonText = rm.GetString("upbFile2PPT");
            this.upbFile2HTML.ButtonText = rm.GetString("upbFile2HTML");
            this.upbIMG2PDF.ButtonText = rm.GetString("upbIMG2PDF");
            this.upbFile2TXT.ButtonText = rm.GetString("upbFile2TXT");
            this.upbFile2IMG.ButtonText = rm.GetString("upbFile2IMG");
            this.upbDoc2PDF.ButtonText = rm.GetString("upbDoc2PDF");
            this.upbPPT2PDF.ButtonText = rm.GetString("upbPPT2PDF");
            this.upbExcel2PDF.ButtonText = rm.GetString("upbExcel2PDF");
            this.btnReg.ButtonText = rm.GetString("btnReg");
            this.btnHelp.ButtonText = rm.GetString("btnHelp");
            this.lblTitle.Text = rm.GetString("lblTitle.Text");
            this.btnAddFiles.ButtonText = rm.GetString("btnAddFiles.Text");
            this.btnFolder.ButtonText = rm.GetString("btnFolder.Text");
            this.btnClear.ButtonText = rm.GetString("btnClear");
            this.lstFile.IndexText = rm.GetString("Number");
            this.lstFile.FileNameText = rm.GetString("FileName");
            this.lstFile.SelectPageText = rm.GetString("ConversionPages");
            this.lstFile.StatusText = rm.GetString("Status");
            this.lstFile.OperateText = rm.GetString("Operate");
            this.cbIsMerger.Text = rm.GetString("cbIsMerger.Text");
            this.lblPPTSize.Text = rm.GetString("PPTSize");
            this.lblWidth.Text = rm.GetString("Width");
            this.lblHeight.Text = rm.GetString("Height");
            this.btnStart.ButtonText = rm.GetString("btnStart.Text");
            this.rdoPath.Text = rm.GetString("rdoPath.Text");
            this.rdoNewPath.Text = rm.GetString("rdoNewPath.Text");
            this.btnBrowse.ButtonText = rm.GetString("btnBrowse.Text");
            this.btnCourse.ButtonText = rm.GetString("btnCourse");
            this.btnBuy.ButtonText = rm.GetString("btnBuy");
            this.tsmSoftwareUpgrade.Text = rm.GetString("tsmSoftwareUpgrade");
            this.tsmLanguageSelection.Text = rm.GetString("tsmLanguageSelection");
            //this.comboBoxPage.Text = rm.GetString("PageTips");
            //this.tsmCn.Text = rm.GetString("tsmCn.Text");
            //this.tsmEnglish.Text = rm.GetString("tsmEnglish.Text");
            this.tsmAboutUs.Text = rm.GetString("tsmAboutUs");
            this.tsmBuy.Text = rm.GetString("tsmBuy");
            this.btnQQ.ButtonText = rm.GetString("btnQQ");
            this.btnPhone.ButtonText = rm.GetString("btnPhone");
            this.comboBoxPage.BackGroundText = rm.GetString("PageTips");
            lstFile.ConversionPageDefaultText = rm.GetString("ALL");
            this.lstFile.OpenButtonText = rm.GetString("Open");
            pbLogo.BackgroundImage = Properties.Resources.logo_030;
            pbLogo.Size = new System.Drawing.Size(206, 40);
            //btnPhone.Visible = true;
            //btnQQ.Visible = true;
            plPPT.Location = new Point(6, 42);
            plPPT.Size = new System.Drawing.Size(271, 35);
            lblPPTSize.Location = new Point(4, 5);
            lblPPTSize.Size = new System.Drawing.Size(89, 17);
            lblWidth.Location = new Point(97, 5);
            lblWidth.Size = new System.Drawing.Size(20, 17);
            txtWidth.Location = new Point(118, 5);
            txtWidth.Size = new System.Drawing.Size(59, 21);
            lblHeight.Location = new Point(185, 5);
            lblHeight.Size = new System.Drawing.Size(20, 17);
            txtHeight.Location = new Point(208, 4);
            txtHeight.Size = new System.Drawing.Size(59, 21);
            cbIsMerger.Location = new Point(20, 53);
            cbIsMerger.Size = new System.Drawing.Size(186, 16);
            rdoPath.Location = new Point(15, 110);
            rdoPath.Size = new System.Drawing.Size(139, 24);
            rdoNewPath.Location = new Point(166, 110);
            rdoNewPath.Size = new System.Drawing.Size(111, 24);
            txtOutPath.Location = new Point(283, 110);
            btnBrowse.Location = new Point(652, 108);
            btnStart.Location = new Point(280, 34);
            btnCourse.Location = new Point(17, 17);
            btnCourse.Size = new System.Drawing.Size(115, 38);
            btnCourse.ButtonTextFont = new System.Drawing.Font("微软雅黑", 14);
            btnBuy.ButtonTextFont = new System.Drawing.Font("微软雅黑", 14);
            btnBuy.Location = new Point(144, 17);
            btnBuy.Size = new System.Drawing.Size(116, 38);
            btnQQ.Location = new Point(269, 17);
            btnQQ.Size = new System.Drawing.Size(203, 38);
            btnQQ.ButtonImage = Properties.Resources.qq;
            btnQQ.FromType = 2;
            btnPhone.Location = new Point(474, 17);
            btnPhone.Size = new System.Drawing.Size(273, 38);
        }

        /// <summary>
        /// 设置英文语言
        /// </summary>
        private void SetEn()
        {
            Thread.CurrentThread.CurrentUICulture = new CultureInfo("en");
            this.upbFile2Word.ButtonText = rm.GetString("upbFile2Word");
            this.upbFile2Excel.ButtonText = rm.GetString("upbFile2Excel");
            this.upbFile2PPT.ButtonText = rm.GetString("upbFile2PPT");
            this.upbFile2HTML.ButtonText = rm.GetString("upbFile2HTML");
            this.upbIMG2PDF.ButtonText = rm.GetString("upbIMG2PDF");
            this.upbFile2TXT.ButtonText = rm.GetString("upbFile2TXT");
            this.upbFile2IMG.ButtonText = rm.GetString("upbFile2IMG");
            this.upbDoc2PDF.ButtonText = rm.GetString("upbDoc2PDF");
            this.upbPPT2PDF.ButtonText = rm.GetString("upbPPT2PDF");
            this.upbExcel2PDF.ButtonText = rm.GetString("upbExcel2PDF");
            this.btnReg.ButtonText = rm.GetString("btnReg");
            this.btnHelp.ButtonText = rm.GetString("btnHelp");
            this.lblTitle.Text = rm.GetString("lblTitle.Text");
            this.btnAddFiles.ButtonText = rm.GetString("btnAddFiles.Text");
            this.btnFolder.ButtonText = rm.GetString("btnFolder.Text");
            this.btnClear.ButtonText = rm.GetString("btnClear");
            this.lstFile.IndexText = rm.GetString("Number");
            this.lstFile.FileNameText = rm.GetString("FileName");
            this.lstFile.SelectPageText = rm.GetString("ConversionPages");
            this.lstFile.StatusText = rm.GetString("Status");
            this.lstFile.OperateText = rm.GetString("Operate");
            this.cbIsMerger.Text = rm.GetString("cbIsMerger.Text");
            this.lblPPTSize.Text = rm.GetString("PPTSize");
            this.lblWidth.Text = rm.GetString("Width");
            this.lblHeight.Text = rm.GetString("Height");
            this.btnStart.ButtonText = rm.GetString("btnStart.Text");
            this.rdoPath.Text = rm.GetString("rdoPath.Text");
            this.rdoNewPath.Text = rm.GetString("rdoNewPath.Text");
            this.btnBrowse.ButtonText = rm.GetString("btnBrowse.Text");
            this.btnCourse.ButtonText = rm.GetString("btnCourse");
            this.btnBuy.ButtonText = rm.GetString("btnBuy");
            this.tsmSoftwareUpgrade.Text = rm.GetString("tsmSoftwareUpgrade");
            this.tsmLanguageSelection.Text = rm.GetString("tsmLanguageSelection");
            //this.tsmCn.Text = rm.GetString("tsmCn.Text");
            //this.tsmEnglish.Text = rm.GetString("tsmEnglish.Text");
            this.tsmAboutUs.Text = rm.GetString("tsmAboutUs");
            this.tsmBuy.Text = rm.GetString("tsmBuy");
            this.btnQQ.ButtonText = rm.GetString("btnQQ");
            this.btnPhone.ButtonText = rm.GetString("btnPhone");
            this.comboBoxPage.BackGroundText = rm.GetString("PageTips");
            lstFile.ConversionPageDefaultText = rm.GetString("ALL");
            this.lstFile.OpenButtonText = rm.GetString("Open");
            pbLogo.BackgroundImage = Properties.Resources.logo_050;
            pbLogo.Size = new System.Drawing.Size(206, 40);
            // btnQQ.Visible = false;
            // btnPhone.Visible = false;
            plPPT.Location = new Point(3, 34);
            plPPT.Size = new System.Drawing.Size(344, 35);
            lblPPTSize.Location = new Point(5, 9);
            lblPPTSize.Size = new System.Drawing.Size(116, 17);
            lblWidth.Location = new Point(116, 10);
            lblWidth.Size = new System.Drawing.Size(42, 17);
            txtWidth.Location = new Point(163, 10);
            txtWidth.Size = new System.Drawing.Size(59, 21);
            lblHeight.Location = new Point(228, 11);
            lblHeight.Size = new System.Drawing.Size(46, 17);
            txtHeight.Location = new Point(280, 10);
            txtHeight.Size = new System.Drawing.Size(59, 21);
            cbIsMerger.Location = new Point(20, 53);
            cbIsMerger.Size = new System.Drawing.Size(264, 16);
            rdoPath.Location = new Point(12, 86);
            rdoPath.Size = new System.Drawing.Size(214, 24);
            rdoNewPath.Location = new Point(12, 121);
            rdoNewPath.Size = new System.Drawing.Size(130, 24);
            txtOutPath.Location = new Point(148, 119);
            btnBrowse.Location = new Point(531, 119);
            btnStart.Location = new Point(365, 53);
            btnCourse.Location = new Point(17, 17);
            btnCourse.Size = new System.Drawing.Size(80, 38);
            btnCourse.ButtonTextFont = new System.Drawing.Font("微软雅黑", 12);
            btnBuy.ButtonTextFont = new System.Drawing.Font("微软雅黑", 12);
            btnBuy.Location = new Point(103, 17);
            btnBuy.Size = new System.Drawing.Size(72, 38);
            btnQQ.Location = new Point(182, 17);
            btnQQ.Size = new System.Drawing.Size(232, 38);
            btnQQ.ButtonImage = Properties.Resources.emailnew;
            btnQQ.FromType = 7;
            btnPhone.Location = new Point(423, 17);
            btnPhone.Size = new System.Drawing.Size(273, 38);
        }



        public void PostURL(TempUrl obj)
        {
            this.syncContext.Post(URL, obj);
        }
        public void URL(object obj)
        {
            if (obj != null)
            {
                TempUrl url = obj as TempUrl;
                Version.Post("http://all.jsocr.com/", Version.GetParamName("Data"), "PDF转换器", Version.GetParamName("Version"), encodingCode, url.Target, url.MehodObject);
            }

        }
        public void PopReg()
        {
            if (!isReg)
            {
                this.syncContext.Post(RegTigs, null);
            }

        }
        private void RegTigs(object obj)
        {
            if (lstFile.IsAllFinished && fileQueue.Count == 0)
            {
                RegTips frm = new RegTips();
                frm.StartPosition = FormStartPosition.Manual;
                frm.Location = this.PointToScreen(new Point(400, this.lstFile.Location.Y + 30));
                DialogResult dr = frm.ShowDialog();
                if (dr == System.Windows.Forms.DialogResult.OK)
                {
                    RegDlg reg = new RegDlg();
                    reg.StartPosition = FormStartPosition.Manual;
                    reg.Location = this.PointToScreen(new Point(250, lstFile.Location.Y - 10));
                    reg.ShowDialog();
                    if (new reg().Is_Reg())
                    {
                        this.lblTitle.Text = Encrypt.APPTITLE + " " + rm.GetString("OfficialVersion") + " v" + Version.version;
                        isReg = true;
                        // pltext.Visible = true;
                    }
                    else
                    {
                        this.lblTitle.Text = Encrypt.APPTITLE + " " + rm.GetString("FreeTrialVersion") + " v" + Version.version;
                        isReg = false;
                        // pltext.Visible = false;
                    }
                }
            }

        }


        public void UpdateProcess(TempClass info)
        {
            if (info.index < 0) return;
            this.syncContext.Post(SetProcess, info);
        }

        private void SetProcess(object obj)
        {
            TempClass tmp = (TempClass)obj;
            if (tmp.index < 0) return;
            this.lstFile.SetStausPV(tmp.index, tmp.cur);
        }



        /// <summary>
        /// 获取文件夹下的PDF文件
        /// </summary>
        /// <param name="filePath">文件夹路径</param>
        private void GetFolder(string filePath)
        {
            DirectoryInfo folder = new DirectoryInfo(filePath);
            bool show_flag = true;
            foreach (FileInfo file in folder.GetFiles())
            {
                string extensions = file.Extension.ToLower();
                string fileName = string.Empty;
                switch (format)
                {
                    case Convert.FORMAT.File2WORD:
                        {
                            if (extensions == ".pdf" || extensions == ".xls" || extensions == ".xlsx" || extensions == ".ppt" || extensions == ".pptx")
                            {
                                fileName = file.FullName;

                            }
                        } break;
                    case Convert.FORMAT.File2EXCEL:
                        {
                            if (extensions == ".pdf" || extensions == ".docx" || extensions == ".doc" || extensions == ".ppt" || extensions == ".pptx")
                            {
                                fileName = file.FullName;

                            }
                        } break;
                    case Convert.FORMAT.File2PPT:
                        {
                            if (extensions == ".pdf" || extensions == ".docx" || extensions == ".doc" || extensions == ".xls" || extensions == ".xlsx")
                            {
                                fileName = file.FullName;

                            }
                        } break;
                    case Convert.FORMAT.File2HTML:
                        {
                            if (extensions == ".pdf" || extensions == ".docx" || extensions == ".doc" || extensions == ".xls" || extensions == ".xlsx" || extensions == ".ppt" || extensions == ".pptx")
                            {
                                fileName = file.FullName;

                            }
                        } break;
                    case Convert.FORMAT.IMG2PDF:
                        {
                            if (extensions == ".jpg" || extensions == ".jpeg" || extensions == ".gif" || extensions == ".bmp" || extensions == ".png" || extensions == ".tiff" || extensions == ".tif")
                            {
                                fileName = file.FullName;

                            }
                        } break;
                    case Convert.FORMAT.File2TXT:
                        {
                            if (extensions == ".pdf" || extensions == ".docx" || extensions == ".doc" || extensions == ".xls" || extensions == ".xlsx" || extensions == ".ppt" || extensions == ".pptx")
                            {
                                fileName = file.FullName;

                            }
                        } break;
                    case Convert.FORMAT.File2IMG:
                        {
                            if (extensions == ".pdf" || extensions == ".docx" || extensions == ".doc" || extensions == ".xls" || extensions == ".xlsx" || extensions == ".ppt" || extensions == ".pptx")
                            {
                                fileName = file.FullName;

                            }
                        } break;

                    case Convert.FORMAT.DOC2PDF:
                        {
                            if (extensions == ".docx" || extensions == ".doc")
                            {
                                fileName = file.FullName;

                            }
                        } break;

                    case Convert.FORMAT.PPT2PDF:
                        {
                            if (extensions == ".ppt" || extensions == ".pptx")
                            {
                                fileName = file.FullName;

                            }
                        } break;
                    case Convert.FORMAT.Excel2PDF:
                        {
                            if (extensions == ".xls" || extensions == ".xlsx")
                            {
                                fileName = file.FullName;

                            }
                        } break;

                }
                if (string.IsNullOrEmpty(fileName)) continue;
                if (diclst.ContainsKey(fileName))
                {
                    if (show_flag && openFileDialog.FileNames.Length == 1)
                    {
                        show_flag = false;
                        MessageBox.Show(string.Format(rm.GetString("msg9"), Path.GetFileName(fileName))
                               , rm.GetString("Tips"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                        //MessageBox.Show("您添加的文件 " + Path.GetFileName(fileName) + " 已存在,我们将会自动过滤这些文件!"
                        //    , "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else if (show_flag && openFileDialog.FileNames.Length != 1)
                    {
                        show_flag = false;
                        MessageBox.Show(rm.GetString("msg1")
                               , rm.GetString("Tips"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                        //MessageBox.Show("您添加的部分文件已存在,我们将会自动过滤这些文件!"
                        //    , "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    continue;
                }
                if (!fileName.Contains("$"))
                {
                    ItemInfomation info = new ItemInfomation(fileName);
                    lstFile.ConversionPageDefaultText = rm.GetString("ALL");
                    lstFile.AddFile(info);
                    diclst.Add(fileName, false);
                }


            }



        }

        /// <summary>
        /// 导航菜单默认选中
        /// </summary>
        /// <param name="index"></param>
        private void MenuSeletect(int select)
        {
            switch (select)
            {
                case 0:
                    upbFile2Word.Selected = true;
                    format = Convert.FORMAT.File2WORD;
                    break;
                case 1:
                    upbFile2Excel.Selected = true;
                    format = Convert.FORMAT.File2EXCEL;
                    break;
                case 2:
                    upbFile2PPT.Selected = true;
                    this.plPPT.Visible = true;
                    format = Convert.FORMAT.File2PPT;
                    break;
                case 3:
                    upbFile2HTML.Selected = true;
                    format = Convert.FORMAT.File2HTML;
                    break;
                case 4:
                    upbIMG2PDF.Selected = true;
                    this.cbIsMerger.Visible = true;
                    format = Convert.FORMAT.IMG2PDF;
                    break;
                case 5:
                    upbFile2TXT.Selected = true;
                    format = Convert.FORMAT.File2TXT;
                    break;
                case 6:
                    upbFile2IMG.Selected = true;
                    format = Convert.FORMAT.File2IMG;
                    break;
                case 7:
                    upbDoc2PDF.Selected = true;
                    format = Convert.FORMAT.DOC2PDF;
                    break;
                case 8:
                    upbPPT2PDF.Selected = true;
                    format = Convert.FORMAT.PPT2PDF;
                    break;
                case 9:
                    upbExcel2PDF.Selected = true;
                    format = Convert.FORMAT.Excel2PDF;
                    break;
                default:
                    upbFile2Word.Selected = true;
                    format = Convert.FORMAT.File2WORD;
                    break;
            }
        }

        private string GetTaskName()
        {
            string tackName = string.Empty;
            switch (format)
            {
                case Convert.FORMAT.File2WORD:
                    {
                        tackName = rm.GetString("FileTo") + " Word";
                    } break;
                case Convert.FORMAT.File2EXCEL:
                    {
                        tackName = rm.GetString("FileTo") + " EXCEL";
                    } break;
                case Convert.FORMAT.File2PPT:
                    {
                        tackName = rm.GetString("FileTo") + " PPT";
                    } break;
                case Convert.FORMAT.File2HTML:
                    {
                        tackName = rm.GetString("FileTo") + " HTML";
                    } break;
                case Convert.FORMAT.IMG2PDF:
                    {
                        tackName = rm.GetString("IMG") + rm.GetString("Turn") + " PDF";
                    } break;
                case Convert.FORMAT.File2TXT:
                    {
                        tackName = rm.GetString("FileTo") + " TXT";
                    } break;
                case Convert.FORMAT.File2IMG:
                    {
                        tackName = rm.GetString("FileTo") + rm.GetString("IMG");
                    } break;

                case Convert.FORMAT.DOC2PDF:
                    {
                        tackName = "Word " + rm.GetString("Turn") + " PDF";
                    } break;

                case Convert.FORMAT.PPT2PDF:
                    {
                        tackName = "PPT " + rm.GetString("Turn") + " PDF";
                    } break;
                case Convert.FORMAT.Excel2PDF:
                    {
                        tackName = "Excel " + rm.GetString("Turn") + " PDF";
                    } break;

            }
            return tackName;
        }

        private bool IsMatched(string file_name, Convert.FORMAT format)
        {

            bool result = false;

            string suffix = Path.GetExtension(file_name).ToUpper();
            if (format == Convert.FORMAT.File2WORD)
            {
                if (suffix == ".PDF" || suffix == ".XLS" || suffix == ".XLSX" || suffix == ".PPT" || suffix == ".PPTX")
                {
                    result = true;
                }
            }
            else if (format == Convert.FORMAT.File2EXCEL)
            {
                if (suffix == ".PDF" || suffix == ".DOC" || suffix == ".DOCX" || suffix == ".PPT" || suffix == ".PPTX")
                {
                    result = true;
                }
            }
            else if (format == Convert.FORMAT.File2PPT)
            {
                if (suffix == ".PDF" || suffix == ".DOC" || suffix == ".DOCX" || suffix == ".XLS" || suffix == ".XLSX")
                {
                    result = true;
                }
            }
            else if (format == Convert.FORMAT.File2IMG || format == Convert.FORMAT.File2HTML || format == Convert.FORMAT.File2TXT)
            {
                if (suffix == ".PDF" || suffix == ".DOC" || suffix == ".DOCX" || suffix == ".PPT" || suffix == ".PPTX" || suffix == ".XLS" || suffix == ".XLSX")
                {
                    result = true;
                }
            }

            else if (format == Convert.FORMAT.IMG2PDF)
            {
                if (suffix == ".JPG" || suffix == ".JPEG" || suffix == ".GIF" || suffix == ".BMP" || suffix == ".PNG" || suffix == ".TIF" || suffix == ".TIFF")
                {
                    return result = true; ;
                }
            }
            else if (format == Convert.FORMAT.DOC2PDF)
            {
                if (suffix == ".DOC" || suffix == ".DOCX")
                {
                    result = true;
                }
            }
            else if (format == Convert.FORMAT.Excel2PDF)
            {
                if (suffix == ".XLS" || suffix == ".XLSX")
                {
                    result = true;
                }
            }

            else if (format == Convert.FORMAT.PPT2PDF)
            {
                if (suffix == ".PPT" || suffix == ".PPTX")
                {
                    result = true;
                }
            }




            return result;
        }

        private void WorkThread(object pram)
        {

            try
            {
                //队列线程索引
                int thr_index = System.Convert.ToInt32(pram);

                ListViewItem lv = null;
                Convert ins;
                while (true)
                {
                    if (isClose) break;
                    lock (fileQueue)
                    {
                        if (fileQueue != null && fileQueue.Count > 0)
                        {
                            lv = fileQueue.Dequeue();

                        }
                    }
                    if (lv != null && ((ItemInfomation)lv.Tag).Status != StatusType.Done)
                    {
                        string fileName = ((ItemInfomation)lv.Tag).FileFullPath;
                        string sourseName = Path.GetFileNameWithoutExtension(fileName);
                        string soursePath = Path.GetDirectoryName(fileName) + "\\" + sourseName;
                        string path = this.rdoPath.Checked ? soursePath : this.txtOutPath.OutText + sourseName;
                        ins = new Convert(fileName, path, format, this);
                        ItemInfomation info = (ItemInfomation)lv.Tag;
                        info.Status = StatusType.Start;
                        lstFile.SetStausPV(lv.Index, 0);
                        info.FileFullConvertPath = fileName;
                        lv.Tag = info;
                        if (!dicThreadManagement.ContainsKey(fileName))
                        {
                            dicThreadManagement.Add(fileName, thr_index);
                        }
                        if (!ins.Can_work())
                        {
                            diclst.Remove(fileName);
                            this.lstFile.RemoveFile(lv.Index);
                            lv = null;
                            if (lstFile.IsAllFinished)
                            {
                                btnStart.Enabled = true;
                                btnStart.BackgroundImage = Properties.Resources.startConversion;
                            }
                            //if (ins.Get_err_msg() != string.Empty)
                            //{
                            //    MessageBox.Show(ins.Get_err_msg(), rm.GetString("Tips"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                            //    break;
                            //}
                        }
                        else
                        {
                            ins.Save(this, Path.GetExtension(fileName), lv.Index, lv);
                        }
                        // dicThreadManagement.Remove(fileName);
                        ins.Close();

                        PopReg();
                    }

                    Thread.Sleep(500);
                }

            }
            catch
            {


            }



        }





        private void comboBoxPage_Leave(object sender, EventArgs e)
        {
            string text = comboBoxPage.Text;
            try
            {
                if (text != rm.GetString("ALL"))
                {
                    if (lstFile.SelectedItems.Count > 0)
                    {
                        string[] sp_text = text.Split('-');
                        if (sp_text.Length != 2)
                        {
                            //MessageBox.Show("选择页数格式错误,请重新输入", "错误", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            lstFile.SelectedItems[0].SubItems[2].Text = rm.GetString("ALL");
                            return;
                        }
                        if (System.Convert.ToInt32(sp_text[0]) > System.Convert.ToInt32(sp_text[1]))
                        {
                            MessageBox.Show(rm.GetString("msg5"), rm.GetString("Error"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                            //MessageBox.Show("起始页应小于等于最终页,请重新输入", "错误", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            lstFile.SelectedItems[0].SubItems[2].Text = rm.GetString("ALL");
                            return;
                        }
                        lstFile.SelectedItems[0].SubItems[2].Text = comboBoxPage.Text;
                    }


                }
            }
            catch
            {
                //MessageBox.Show("选择页数格式错误,请重新输入", "错误", MessageBoxButtons.OK, MessageBoxIcon.Information);
                if (lstFile.SelectedItems.Count > 0)
                {
                    lstFile.SelectedItems[0].SubItems[2].Text = rm.GetString("ALL");
                }

            }
            pltext.Visible = false;
        }

        private void comboBoxPage_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (string.IsNullOrEmpty(comboBoxPage.Text.Trim()))
                {
                    comboBoxPage.Text = rm.GetString("ALL");
                }
                lstFile.SelectedItems[0].SubItems[2].Text = comboBoxPage.Text;
                pltext.Visible = false;
                lstFile.Focus();
            }
        }

        private void upbFile2Word_Click(object sender, EventArgs e)
        {
            cbIsMerger.Visible = false;
            plPPT.Visible = false;
            // comboBoxPage.Enabled = true;
            string name = ((Control)sender).Name;
            switch (name)
            {
                case "upbFile2Word":
                    format = Convert.FORMAT.File2WORD;
                    break;
                case "upbFile2Excel":
                    format = Convert.FORMAT.File2EXCEL;
                    break;
                case "upbFile2PPT":
                    plPPT.Visible = true;
                    format = Convert.FORMAT.File2PPT;
                    break;
                case "upbFile2HTML":
                    format = Convert.FORMAT.File2HTML;
                    break;
                case "upbIMG2PDF":
                    cbIsMerger.Visible = true;
                    // comboBoxPage.Enabled = false;
                    format = Convert.FORMAT.IMG2PDF;
                    break;
                case "upbFile2TXT":
                    format = Convert.FORMAT.File2TXT;
                    break;
                case "upbFile2IMG":
                    format = Convert.FORMAT.File2IMG;
                    break;
                case "upbDoc2PDF":
                    format = Convert.FORMAT.DOC2PDF;
                    break;
                case "upbPPT2PDF":
                    format = Convert.FORMAT.PPT2PDF;
                    break;
                case "upbExcel2PDF":
                    format = Convert.FORMAT.Excel2PDF;
                    break;
                default:
                    break;
            }
            SetButtonUnSelect(((Control)sender).Name);
        }

        private void SetButtonUnSelect(string name)
        {
            foreach (Control c in plNavigation.Controls)
            {
                if (c.Name != name)
                    ((ucPicButton)c).Selected = false;
            }
        }

        private void pictureBox3_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                this.contextMenuStrip1.Show(pbMenu, 0, pbMenu.Height);
            }
        }




        private void WinformClose()
        {
            ini_config ini = new ini_config("config.ini");
            ini.write_ini("TargetDic", this.txtOutPath.OutText);
            ini.write_ini("PicX", this.txtWidth.Text);
            ini.write_ini("PicY", this.txtHeight.Text);
            ini.write_ini("Type", System.Convert.ToInt32(format).ToString());
            ini.write_ini("Out", this.rdoPath.Checked ? "1" : "0");
            ini.write_ini("isMerger", this.cbIsMerger.Checked ? "0" : "1");
            isClose = true;
            //this.Dispose();
            System.Environment.Exit(-1);
            this.Close();

        }

        private void btnAddFiles_MouseEnter(object sender, EventArgs e)
        {
            this.btnAddFiles.ButtonBackIMG = Properties.Resources.afterAddFile;
        }

        private void btnAddFiles_MouseLeave(object sender, EventArgs e)
        {
            this.btnAddFiles.ButtonBackIMG = Properties.Resources.addFile;
        }


        private void btnStart_MouseEnter(object sender, EventArgs e)
        {
            this.btnStart.BackgroundImage = Properties.Resources.afterConversion;
        }

        private void btnStart_MouseLeave(object sender, EventArgs e)
        {
            if (((Control)sender).Enabled != false)
            {
                this.btnStart.BackgroundImage = Properties.Resources.startConversion;
            }

        }

        private void btnFolder_MouseEnter(object sender, EventArgs e)
        {
            this.btnFolder.ButtonBackIMG = Properties.Resources.afterFolder;

        }

        private void btnFolder_MouseLeave(object sender, EventArgs e)
        {
            this.btnFolder.ButtonBackIMG = Properties.Resources.addFolder;
        }


        /// <summary>
        /// 验证列表是否包含其他类型文件
        /// </summary>
        private bool VerifyList(int index = -1)
        {
            bool result = false;
            if (index == -1)
            {
                foreach (ListViewItem lv in lstFile.Items)
                {
                    string suffix = lv.SubItems[1].Text;
                    ItemInfomation Info = ((ItemInfomation)lv.Tag);
                    if (Info != null && Info.Status == StatusType.Done)
                    {
                        suffix = Info.FileFullConvertPath;
                    }
                    if (!IsMatched(suffix, format))
                    {

                        if (!result)
                        {
                            MessageBox.Show(string.Format(rm.GetString("msg3"), GetTaskName()), rm.GetString("Tips"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                            //MessageBox.Show(string.Format("您选择的是{0}，但您添加的文件中含有其他类型的文件,我们将会移除相应的文件", GetTaskName()), "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            result = true;
                        }
                        diclst.Remove(((ItemInfomation)lv.Tag).FileFullPath);
                        lv.Remove();
                    }


                }
            }
            else
            {
                if (lstFile.Items.Count > 0)
                {
                    string suffix = lstFile.Items[index].SubItems[1].Text;
                    if (!IsMatched(suffix, format))
                    {
                        if (!result)
                        {
                            MessageBox.Show(string.Format(rm.GetString("msg3"), GetTaskName()), rm.GetString("Tips"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                            //MessageBox.Show(string.Format("您选择的是{0}，但您添加的文件中含有其他类型的文件,我们将会移除相应的文件", GetTaskName()), "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            result = true;
                        }
                        diclst.Remove(((ItemInfomation)lstFile.Items[index].Tag).FileFullPath);
                        this.lstFile.RemoveFile(index);

                    }
                }

            }


            return result;
        }


        /// <summary>
        /// 暂停开始状态事件
        /// </summary>
        /// <param name="index"></param>
        /// <param name="status"></param>
        private void lstFile_OnStatusButtonClicked(int index, StatusType status)
        {
            this.pltext.Visible = false;
            if (status == StatusType.Start)
            {
                ((ItemInfomation)lstFile.Items[index].Tag).Status = StatusType.Pause;
                //发送请求信息 
                TempUrl t = new TempUrl("主程序", "列表点击开始");
                PostURL(t);

            }
            else if (status == StatusType.Pause)
            {

                ((ItemInfomation)lstFile.Items[index].Tag).Status = StatusType.Start;
                //发送请求信息 
                TempUrl t = new TempUrl("主程序", "列表点击暂停");
                PostURL(t);

            }
            else if (status == StatusType.Ready)
            {
                if (!VerifyList(index))
                {
                    fileQueue.Enqueue(this.lstFile.Items[index]);
                    ((ItemInfomation)lstFile.Items[index].Tag).Status = StatusType.Start;
                    lstFile.SetStausPV(index, ((ItemInfomation)lstFile.Items[index].Tag).PersentValue);
                    //开启新线程
                    for (int j = 0; j < thread.Length; j++)
                    {
                        if (thread[j].ThreadState == System.Threading.ThreadState.Stopped)
                        {
                            thread[j] = new Thread(new ParameterizedThreadStart(WorkThread));
                            thread[j].IsBackground = true;
                            thread[j].Start(j);
                        }
                    }
                }
            }

        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {

            if (DialogResult.OK == folderBrowserDialog.ShowDialog())
            {
                this.txtOutPath.OutText = folderBrowserDialog.SelectedPath;

            }

        }

        private void btnBrowse_MouseEnter(object sender, EventArgs e)
        {
            this.btnBrowse.ButtonBackIMG = Properties.Resources.lookhover;
        }

        private void btnBrowse_MouseLeave(object sender, EventArgs e)
        {
            this.btnBrowse.ButtonBackIMG = Properties.Resources.look;
        }

        /// <summary>
        /// 列表删除
        /// </summary>
        /// <param name="index"></param>
        private void lstFile_OnDeleteButtonClicked(int index)
        {
            if (index >= 0)
            {

                ItemInfomation Info = ((ItemInfomation)lstFile.Items[index].Tag);
                diclst.Remove(Info.FileFullPath);
                if (dicThreadManagement.Count > 0 && dicThreadManagement.ContainsKey(Info.FileFullPath))
                {
                    int i = dicThreadManagement[Info.FileFullPath];
                    //dicThreadManagement.Remove(Info.FileFullPath);
                    //终止当前线程
                    thread[i].Abort();


                }

                this.lstFile.RemoveFile(index);
                //开启新线程
                for (int j = 0; j < thread.Length; j++)
                {
                    if (thread[j].ThreadState == System.Threading.ThreadState.Stopped)
                    {
                        thread[j] = new Thread(new ParameterizedThreadStart(WorkThread));
                        thread[j].IsBackground = true;
                        thread[j].Start(j);
                    }
                }
                // dicThreadManagement.Remove(Info.FileFullPath);
                if (lstFile.IsAllFinished)
                {
                    btnStart.Enabled = true;
                    btnStart.BackgroundImage = Properties.Resources.startConversion;

                }

                //发送请求信息 
                TempUrl t = new TempUrl("主程序", "列表删除[" + Info.FileFullPath + "]文件");
                PostURL(t);
            }

        }

        /// <summary>
        /// 列表打开文件
        /// </summary>
        /// <param name="index"></param>
        private void lstFile_OnOpenFileButtonClicked(int index)
        {
            ItemInfomation Info = ((ItemInfomation)lstFile.Items[index].Tag);
            string filePath = Info.FileFullPath;
            if (Info.Status == StatusType.Done)
            {
                filePath = Info.FileFullConvertPath;
            }
            try
            {
                System.Diagnostics.Process.Start(filePath);
                //发送请求信息 
                TempUrl t = new TempUrl("主程序", "列表打开[" + filePath + "]文件");
                PostURL(t);
            }
            catch (Exception ex)
            {
                MessageBox.Show(rm.GetString("OpenFileButton"));

            }

        }

        /// <summary>
        /// 列表打开文件夹
        /// </summary>
        /// <param name="index"></param>
        private void lstFile_OnOpenDirectoryButtonClicked(int index)
        {
            ItemInfomation Info = ((ItemInfomation)lstFile.Items[index].Tag);
            string filePath = Info.FileFullPath;
            if (Info.Status == StatusType.Done)
            {
                filePath = Info.FileFullConvertPath;
            }
            try
            {
                string path = @"/select," + filePath + "";
                System.Diagnostics.Process.Start("explorer.exe", path);
                //发送请求信息 
                TempUrl t = new TempUrl("主程序", "列表打开文件夹[" + path + "]");
                PostURL(t);
            }
            catch
            {


            }


        }




        private void MainInfo_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, 274, 61440 + 9, 0);
            }

        }



        private void MainInfo_FormClosing(object sender, FormClosingEventArgs e)
        {
            WinformClose();
        }



        private void tsmEnglish_Click(object sender, EventArgs e)
        {

            ini.write_ini("language", "en");
            SetEn();
            if (isReg)
            {
                this.lblTitle.Text = Encrypt.Refresh() + " " + rm.GetString("OfficialVersion") + " v" + Version.version;

            }
            else
            {
                this.lblTitle.Text = Encrypt.Refresh() + " " + rm.GetString("FreeTrialVersion") + " v" + Version.version;

            }
            //发送请求信息 
            TempUrl t = new TempUrl("主程序", "选择英文");
            PostURL(t);
        }

        private void tsmCn_Click(object sender, EventArgs e)
        {

            ini.write_ini("language", "zh-CN");
            SetZhCn();
            if (isReg)
            {
                this.lblTitle.Text = Encrypt.Refresh() + " " + rm.GetString("OfficialVersion") + " v" + Version.version;

            }
            else
            {
                this.lblTitle.Text = Encrypt.Refresh() + " " + rm.GetString("FreeTrialVersion") + " v" + Version.version;

            }
            //发送请求信息 
            TempUrl t = new TempUrl("主程序", "选择中文");
            PostURL(t);
        }


        private void tsmAboutUs_Click(object sender, EventArgs e)
        {
            Process.Start("http://www.xjpdf.com/software/pdfConvert/guanyu/?version=" + Version.version);
            //发送请求信息 
            TempUrl t = new TempUrl("主程序", "点击关于我们");
            PostURL(t);
        }

        private void tsmBuy_Click(object sender, EventArgs e)
        {

            Process.Start("http://www.xjpdf.com/software/pdfConvert/buy/?version=" + Version.version + "&machine=" + new reg().get_machine_code());
            //发送请求信息 
            TempUrl t = new TempUrl("主程序", "点击购买软件");
            PostURL(t);
        }

        int m_EditIndex;
        private void lstFile_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left && format != Convert.FORMAT.IMG2PDF)
            {
                for (int i = 0; i < lstFile.Items.Count; i++)
                {
                    if (lstFile.Items[i].Selected)
                    {
                        ItemInfomation info = (ItemInfomation)lstFile.Items[i].Tag;
                        if (info != null)
                        {
                            if (info.Status != StatusType.Ready)
                                return;
                        }
                    }

                    if (lstFile.Items[i].SubItems[2].Bounds.Contains(e.X, e.Y))
                    {
                        pltext.Location = new Point(lstFile.SelectedItems[0].SubItems[2].Bounds.Left + lstFile.Location.X + 2, lstFile.SelectedItems[0].SubItems[2].Bounds.Top + lstFile.Location.Y + 1);
                        this.pltext.Visible = true;
                        this.pltext.Width = lstFile.SelectedItems[0].SubItems[2].Bounds.Width;
                        if (lstFile.SelectedItems[0].SubItems[2].Text != rm.GetString("ALL"))
                        {
                            comboBoxPage.Text = lstFile.SelectedItems[0].SubItems[2].Text;
                        }
                        else
                        {
                            comboBoxPage.Text = string.Empty;
                        }

                        m_EditIndex = lstFile.SelectedItems[0].Index;
                        return;
                    }
                }
            }
            if (!string.IsNullOrEmpty(comboBoxPage.Text))
            {
                if (lstFile.SelectedItems.Count > 0 && lstFile.SelectedItems[0].Index == m_EditIndex)
                {
                    lstFile.SelectedItems[0].SubItems[2].Text = comboBoxPage.Text.Replace("\r\n", string.Empty);
                    comboBoxPage.Text = string.Empty;
                }

            }
            pltext.Visible = false;
        }

        private void btnAddFiles_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                openFileDialog.FileName = "";
                if (format == Convert.FORMAT.File2WORD)
                {
                    openFileDialog.Filter = "Any文件(*.pdf,*.xls,*.xlsx,*.ppt,*.pptx)|*.pdf;*.xls;*.xlsx;*.ppt;*.pptx;";
                }
                else if (format == Convert.FORMAT.File2EXCEL)
                {
                    openFileDialog.Filter = "Any文件(*.pdf,*.ppt,*.pptx,*.doc,*.docx)|*.pdf;*.ppt;*.pptx;*.doc;*.docx";
                }
                else if (format == Convert.FORMAT.File2PPT)
                {
                    openFileDialog.Filter = "Any文件(*.pdf,*.xls,*.xlsx,*.doc,*.docx)|*.pdf;*.xls;*.xlsx;*.doc;*.docx";
                }
                else if (format == Convert.FORMAT.File2IMG)
                {
                    openFileDialog.Filter = "Any文件(*.pdf,*.ppt,*.pptx,*.doc,*.docx,*.xls,*.xlsx)|*.pdf;*.ppt;*.pptx;*.doc;*.docx;*.xls;*.xlsx";
                }
                else if (format == Convert.FORMAT.File2TXT)
                {
                    openFileDialog.Filter = "Any文件(*.pdf,*.ppt,*.pptx,*.doc,*.docx,*.xls,*.xlsx)|*.pdf;*.ppt;*.pptx;*.doc;*.docx;*.xls;*.xlsx";
                }
                else if (format == Convert.FORMAT.File2HTML)
                {
                    openFileDialog.Filter = "Any文件(*.pdf,*.ppt,*.pptx,*.doc,*.docx,*.xls,*.xlsx)|*.pdf;*.ppt;*.pptx;*.doc;*.docx;*.xls;*.xlsx";
                }
                else if (format == Convert.FORMAT.IMG2PDF)
                {
                    openFileDialog.Filter = "图片文件(*.jpg,*.jpeg,*.gif,*.bmp,*.png,*.tif,*.tiff)|*.jpg;*.jpeg;*.gif;*.bmp;*.png;*.tif;*.tiff";
                }
                else if (format == Convert.FORMAT.DOC2PDF)
                {
                    openFileDialog.Filter = "Word文件(*.doc,*.docx)|*.doc;*.docx";
                }
                else if (format == Convert.FORMAT.Excel2PDF)
                {
                    openFileDialog.Filter = "Excel文件(*.xls,*.xlsx)|*.xls;*.xlsx";
                }
                else if (format == Convert.FORMAT.PPT2PDF)
                {
                    openFileDialog.Filter = "PowerPoint文件(*.ppt,*.pptx)|*.ppt;*.pptx";
                }
                bool show_flag = true;


                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    foreach (string file_name in openFileDialog.FileNames)
                    {
                        if (diclst.ContainsKey(file_name))
                        {
                            if (show_flag && openFileDialog.FileNames.Length == 1)
                            {
                                show_flag = false;
                                MessageBox.Show(string.Format(rm.GetString("msg9"), Path.GetFileName(file_name))
                                    , rm.GetString("Tips"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                                //MessageBox.Show("您添加的文件 " + Path.GetFileName(file_name) + " 已存在,我们将会自动过滤这些文件!"
                                //    , "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            else if (show_flag && openFileDialog.FileNames.Length != 1)
                            {
                                show_flag = false;
                                //MessageBox.Show("您添加的部分文件已存在,我们将会自动过滤这些文件!"
                                //    , "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                MessageBox.Show(rm.GetString("msg1")
                                   , rm.GetString("Tips"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            continue;

                        }
                        ItemInfomation info = new ItemInfomation(file_name);
                        lstFile.ConversionPageDefaultText = rm.GetString("ALL");
                        lstFile.AddFile(info);
                        diclst.Add(file_name, false);

                    }
                }

                //发送请求信息 
                TempUrl t = new TempUrl("主程序", "添加文档");
                PostURL(t);
            }
        }

        private void btnFolder_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                if (DialogResult.OK == folderBrowserDialog.ShowDialog())
                {
                    GetFolder(folderBrowserDialog.SelectedPath);
                    //发送请求信息 
                    TempUrl t = new TempUrl("主程序", "添加文件夹");
                    PostURL(t);
                }
            }
        }

        private void btnStart_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                VerifyList();
                this.pltext.Visible = false;
                if (lstFile.Items.Count > 0 && this.lstFile.IsAllFinished)
                {
                    this.btnStart.Enabled = false;
                    //plNavigation.Enabled = false;
                    this.btnStart.BackgroundImage = Properties.Resources.startnot;

                }
                for (int i = 0; i < lstFile.Items.Count; i++)
                {
                    fileQueue.Enqueue(this.lstFile.Items[i]);

                }
                for (int j = 0; j < thread.Length; j++)
                {
                    if (thread[j].ThreadState == System.Threading.ThreadState.Stopped)
                    {
                        thread[j] = new Thread(new ParameterizedThreadStart(WorkThread));
                        thread[j].IsBackground = true;
                        thread[j].Start(j);
                    }
                }

                //发送请求信息 
                TempUrl t = new TempUrl("主程序", "启动" + GetTaskName());
                PostURL(t);
            }
        }

        private void btnBrowse_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                if (DialogResult.OK == folderBrowserDialog.ShowDialog())
                {
                    this.txtOutPath.OutText = folderBrowserDialog.SelectedPath;

                }
            }
        }

        private void lstFile_DragDrop(object sender, DragEventArgs e)
        {
            bool show_flag = true;
            if (((string[])e.Data.GetData(DataFormats.FileDrop)) != null)
            {

                foreach (string file_name in ((string[])e.Data.GetData(DataFormats.FileDrop)))
                {

                    string fileExt = Path.GetExtension(file_name);
                    if (fileExt == ".pdf" || fileExt == ".xls" || fileExt == ".xlsx" || fileExt == ".ppt" || fileExt == ".pptx" || fileExt == ".doc" ||
                        fileExt == ".docx" || fileExt == ".jpg" || fileExt == ".jpeg" || fileExt == ".gif" || fileExt == ".bmp" || fileExt == ".png" || fileExt == ".tiff" || fileExt == ".tif")
                    {
                        if (diclst.ContainsKey(file_name))
                        {
                            if (show_flag && openFileDialog.FileNames.Length == 1)
                            {
                                show_flag = false;
                                MessageBox.Show(string.Format(rm.GetString("msg9"), Path.GetFileName(file_name))
                                    , rm.GetString("Tips"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                                //MessageBox.Show("您添加的文件 " + Path.GetFileName(file_name) + " 已存在,我们将会自动过滤这些文件!"
                                //    , "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            else if (show_flag && openFileDialog.FileNames.Length != 1)
                            {
                                show_flag = false;
                                //MessageBox.Show("您添加的部分文件已存在,我们将会自动过滤这些文件!"
                                //    , "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                MessageBox.Show(rm.GetString("msg1")
                                   , rm.GetString("Tips"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            continue;

                        }
                        ItemInfomation info = new ItemInfomation(file_name);
                        lstFile.ConversionPageDefaultText = rm.GetString("ALL");
                        lstFile.AddFile(info);
                        diclst.Add(file_name, false);
                    }
                    else if (string.IsNullOrEmpty(fileExt))
                    {
                        GetFolder(file_name);
                    }


                }

            }
            lstFile.Invalidate();
        }

        private void lstFile_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.Copy;
            }
            else
            {
                e.Effect = e.AllowedEffect;
            }
        }

        private void lstFile_DragLeave(object sender, EventArgs e)
        {
            lstFile.InsertionMark.Index = -1;
        }

        private void lstFile_DragOver(object sender, DragEventArgs e)
        {
            // 获得鼠标坐标  
            //Point point = lstFile.PointToClient(new Point(e.X, e.Y));
            //// 返回离鼠标最近的项目的索引  
            //int index = lstFile.InsertionMark.NearestIndex(point);
            //// 确定光标不在拖拽项目上  
            //if (index > -1)
            //{
            //    Rectangle itemBounds = lstFile.GetItemRect(index);
            //    if (point.X > itemBounds.Left + (itemBounds.Width / 2))
            //    {
            //        lstFile.InsertionMark.AppearsAfterItem = true;
            //    }
            //    else
            //    {
            //        lstFile.InsertionMark.AppearsAfterItem = false;
            //    }
            //}
            //lstFile.InsertionMark.Index = index;
            //lstFile.Invalidate();
        }

        private void btnReg_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                RegDlg frm = new RegDlg();
                frm.StartPosition = FormStartPosition.Manual;
                frm.Location = this.PointToScreen(new Point(250, lstFile.Location.Y - 10));
                frm.ShowDialog();
                if (new reg().Is_Reg())
                {
                    this.lblTitle.Text = Encrypt.APPTITLE + " " + rm.GetString("OfficialVersion") + " v" + Version.version;
                    isReg = true;
                    // pltext.Visible = true;
                }
                else
                {
                    this.lblTitle.Text = Encrypt.APPTITLE + " " + rm.GetString("FreeTrialVersion") + " v" + Version.version;
                    isReg = false;
                    // pltext.Visible = false;
                }
                //发送请求信息 
                TempUrl t = new TempUrl("主程序", "点击注册");
                PostURL(t);
            }
        }

        private void btnHelp_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                Process.Start("http://www.xjpdf.com/software/pdfConvert/help/?version=" + Version.version);

                //发送请求信息 
                TempUrl t = new TempUrl("主程序", "点击帮助");
                PostURL(t);
            }
        }

        private void btnClear_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {

                ClearListTips frm = new ClearListTips();
                frm.StartPosition = FormStartPosition.Manual;
                frm.Location = this.PointToScreen(new Point(400, this.lstFile.Location.Y + 30));
                DialogResult dr = frm.ShowDialog();
                if (dr == System.Windows.Forms.DialogResult.OK)
                {
                    diclst.Clear();
                    this.lstFile.Items.Clear();
                    fileQueue.Clear();
                    //this.comboBoxPage.Visible = false;
                    this.btnStart.Enabled = true;
                    this.pltext.Visible = false;
                    this.btnStart.BackgroundImage = Properties.Resources.startConversion;
                    for (int i = 0; i < thread.Length; i++)
                    {
                        thread[i].Abort();
                    }
                }
                //发送请求信息 
                TempUrl t = new TempUrl("主程序", "清空列表");
                PostURL(t);
            }
        }

        private void btnCourse_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                Process.Start("http://www.xjpdf.com/software/pdfConvert/jiaocheng/?version=" + Version.version);
                //发送请求信息 
                TempUrl t = new TempUrl("主程序", "点击在线教程");
                PostURL(t);
            }
        }

        private void btnBuy_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                Process.Start("http://www.xjpdf.com/software/pdfConvert/buy/?version=" + Version.version + "&machine=" + encodingCode);
                //发送请求信息 
                TempUrl t = new TempUrl("主程序", "点击购买软件");
                PostURL(t);
            }
        }

        private void btnQQ_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                switch (ini.read_ini("language").ToLower())
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

                //发送请求信息 
                TempUrl t = new TempUrl("主程序", "点击在线QQ");
                PostURL(t);
            }
        }

        private void pbMinimize_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                this.WindowState = FormWindowState.Minimized;
            }
        }

        private void pbClose_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {

                WinformClose();
            }
        }

        private void txtWidth_KeyPress(object sender, KeyPressEventArgs e)
        {

            if (!(Char.IsNumber(e.KeyChar)) && e.KeyChar != (char)8)
            {
                e.Handled = true;
            }
        }

        private void txtHeight_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(Char.IsNumber(e.KeyChar)) && e.KeyChar != (char)8)
            {
                e.Handled = true;
            }
        }

        private void rdoPath_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                btnBrowse.IsEnable = false;
                this.btnBrowse.ButtonBackIMG = Properties.Resources.lookEnable;
            }
        }

        private void rdoNewPath_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                btnBrowse.IsEnable = true;
                this.btnBrowse.ButtonBackIMG = Properties.Resources.look;
            }
        }

        private void tsmSoftwareUpgrade_Click(object sender, EventArgs e)
        {
            UpdateTips frm = new UpdateTips();
            frm.StartPosition = FormStartPosition.Manual;
            frm.Location = this.PointToScreen(new Point(400, this.lstFile.Location.Y + 30));
            DialogResult dr = frm.ShowDialog();
            //发送请求信息 
            TempUrl t = new TempUrl("主程序", "在线升级");
            PostURL(t);
        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel7_Paint(object sender, PaintEventArgs e)
        {

        }

        private void lblTitle_Click(object sender, EventArgs e)
        {

        }

        private void pbLogo_Click(object sender, EventArgs e)
        {

        }












    }

    public class TempClass
    {
        public int index { get; set; }
        public int cur { get; set; }
        public TempClass(int index, int cur)
        {
            this.index = index;
            this.cur = cur;

        }
    }

    public class TempUrl
    {
        public string Target { get; set; }
        public string MehodObject { get; set; }
        public TempUrl(string Target, string MehodObject)
        {
            this.Target = Target;
            this.MehodObject = MehodObject;

        }
    }
}
