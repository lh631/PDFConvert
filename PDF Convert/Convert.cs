using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows.Forms;
using Controls;
using System.Drawing;
using System.Threading;
using System.Resources;

namespace PDF_Convert
{
    public class Convert
    {
        private Aspose.Pdf.Document pdf_doc;
        private Aspose.Words.Document word_doc;
        private Aspose.Cells.Workbook excel_doc;
        private Aspose.Slides.Presentation ppt_doc;
        private Aspose.Pdf.Facades.PdfExtractor txt_doc;
        private FileStream fileStream;
        private int pages;
        private string outPath;
        public delegate void save_progress(int cur, int i);
        private FORMAT targetFormat;
        //是否弹出密码窗口
        private bool file_can_work = true;
        private string err_msg = "";
        ResourceManager rm = new ResourceManager(typeof(MainInfo));
        public enum FORMAT
        {
            File2WORD,
            File2EXCEL,
            File2PPT,
            File2HTML,
            IMG2PDF,
            File2TXT,
            File2IMG,
            DOC2PDF,
            PPT2PDF,
            Excel2PDF
        };

        public Convert(string file_path, string outPath, FORMAT format, MainInfo mainInfo)
        {

            try
            {


                fileStream = new FileStream(file_path, FileMode.Open, FileAccess.Read);
                this.outPath = outPath;
                this.targetFormat = format;
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains(rm.GetString("msg6")))
                {
                    MessageBox.Show(
                        string.Format(rm.GetString("msg7"), file_path), rm.GetString("Tips"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                //if (ex.Message.Contains("正由另一进程使用"))
                //{
                //    MessageBox.Show("您的 " + file_path + " 文件已打开，请先关闭文件再进行转换！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //}

                return;
            }

            try
            {
                //pdf_doc = new Aspose.Pdf.Document(fileStream, global_config.password);
                string fileType = Path.GetExtension(file_path).ToLower();

                if (fileType == ".pdf")
                {
                    pdf_doc = new Aspose.Pdf.Document(fileStream);
                    pages = pdf_doc.Pages.Count;
                }
                else if (fileType == ".doc" || fileType == ".docx")
                {
                    word_doc = new Aspose.Words.Document(fileStream);
                    pages = word_doc.PageCount;
                }
                else if (fileType == ".ppt" || fileType == ".pptx")
                {
                    ppt_doc = new Aspose.Slides.Presentation(fileStream);
                    pages = ppt_doc.Slides.Count;
                }
                else if (fileType == ".xls" || fileType == ".xlsx")
                {
                    excel_doc = new Aspose.Cells.Workbook(fileStream);
                    pages = excel_doc.Worksheets.Count;
                }
            }
            catch (Aspose.Pdf.Exceptions.InvalidPasswordException ex)
            {
                //UpdateTips frm = new UpdateTips();
                //frm.StartPosition = FormStartPosition.Manual;
                //frm.Location = this.PointToScreen(new Point(400, this.lstFile.Location.Y + 30));
                //DialogResult dr = frm.ShowDialog();
                PassWordDlg frm = new PassWordDlg(Path.GetFileName(file_path));
                frm.StartPosition = FormStartPosition.Manual;
                frm.Location = mainInfo.PointToScreen(new Point(350, mainInfo.lstFile.Location.Y + 30));
                DialogResult dr = frm.ShowDialog();
                bool ctn = true;
                while (dr == DialogResult.OK && ctn)
                {
                    try
                    {
                        fileStream = new FileStream(file_path, FileMode.Open, FileAccess.Read);
                        pdf_doc = new Aspose.Pdf.Document(fileStream, frm.new_password);
                        pages = pdf_doc.Pages.Count;
                        ctn = false;
                    }
                    catch (Aspose.Pdf.Exceptions.InvalidPasswordException)
                    {
                        dr = frm.ShowDialog();
                    }
                }

                if (dr == DialogResult.Cancel)
                {
                    err_msg = "";
                    file_can_work = false;

                }
            }
            catch (Exception)
            {
                file_can_work = false;
                //err_msg = "发生未知错误";
                err_msg = rm.GetString("msg8");
            }

        }

        /// <summary>
        /// 转换方法
        /// </summary>
        /// <param name="progress">进度条委托</param>
        /// <param name="dlg">窗口</param>
        /// <param name="fileType">文档类型</param>
        /// <param name="index">列表索引</param>
        public void Save(Form dlg = null, string fileType = "", int index = 0, ListViewItem lv = null)
        {
            if (!file_can_work)
                return;
            try
            {
                switch (targetFormat)
                {
                    case FORMAT.File2WORD:
                        {
                            FileToWord(dlg, fileType, index, lv);
                        } break;
                    case FORMAT.File2EXCEL:
                        {
                            FileToExcel(dlg, fileType, index, lv);
                        } break;
                    case FORMAT.File2PPT:
                        {
                            FileToPPT(dlg, fileType, index, lv);
                        } break;
                    case FORMAT.File2HTML:
                        {
                            FileToHTML(dlg, fileType, index, lv);
                        } break;
                    case FORMAT.IMG2PDF:
                        {
                            IMGToPDF(dlg, fileType, index, lv);
                        } break;
                    case FORMAT.File2TXT:
                        {
                            FiletToTXT(dlg, fileType, index, lv);
                        } break;
                    case FORMAT.File2IMG:
                        {
                            FileToIMG(dlg, fileType, index, lv);
                        } break;

                    case FORMAT.DOC2PDF:
                        {
                            DocToPDF(dlg, index, lv);
                        } break;

                    case FORMAT.PPT2PDF:
                        {
                            PptToPDF(dlg, index, lv);
                        } break;
                    case FORMAT.Excel2PDF:
                        {
                            XlsToPDF(dlg, index, lv);
                        } break;

                }
            }
            catch (Exception)
            {


            }

        }

        public List<int> GetPage(string pageSet)
        {
            if (!pageSet.Contains("-")) return null;
            List<int> lst = new List<int>();
            if (pageSet.Contains(","))
            {
                string[] page1 = pageSet.Split(',');
                for (int i = 0; i < page1.Length; i++)
                {
                    if (!page1[i].Contains("-"))
                    {
                        int n = 0;
                        int.TryParse(page1[i], out n);
                        lst.Add(n);
                    }
                    else
                    {
                        string[] page2 = page1[i].Split('-');
                        int start = 0;
                        int.TryParse(page2[0], out start);
                        int end = 0;
                        int.TryParse(page2[1], out end);
                        if (start >= end)
                        {
                            int temp;
                            temp = start;
                            start = end;
                            end = temp;
                        }
                        for (int j = start; j <= end; j++)
                        {
                            lst.Add(j);
                        }
                    }

                }

            }
            else
            {
                string[] page2 = pageSet.Split('-');
                if (page2.Length > 1)
                {
                    int start = 0;
                    int.TryParse(page2[0], out start);
                    int end = 0;
                    int.TryParse(page2[1], out end);
                    if (start >= end)
                    {
                        int temp;
                        temp = start;
                        start = end;
                        end = temp;
                    }
                    for (int j = start; j <= end; j++)
                    {
                        lst.Add(j);
                    }
                }
                else
                {
                    int start = 0;
                    int.TryParse(page2[0], out start);

                    for (int j = start; j <= start; j++)
                    {
                        lst.Add(j);
                    }
                }

            }


            return lst;
        }

        private void FileToWord(Form dlg = null, string fileType = null, int index = 0, ListViewItem lv = null)
        {
            outPath = outPath + ".doc";
            ((ItemInfomation)lv.Tag).FileFullConvertPath = outPath;
            Aspose.Pdf.Document pdfDoc = null;
            MainInfo mainInfo = dlg as MainInfo;
            int startRate = 0;

            if (fileType == ".pdf")
            {
                pdfDoc = pdf_doc;
                startRate = 0;

            }
            else if (fileType == ".ppt" || fileType == ".pptx")
            {
                pdfDoc = PptToPDF(dlg, lv.Index, lv);
                startRate = 50;
            }
            else if (fileType == ".xls" || fileType == ".xlsx")
            {
                pdfDoc = XlsToPDF(dlg, lv.Index, lv);
                startRate = 50;
            }
            Aspose.Words.Document new_word_doc = new Aspose.Words.Document();
            Aspose.Pdf.Document new_pdf_doc = new Aspose.Pdf.Document();
            MemoryStream ms;

            new_word_doc.ChildNodes.Clear();
            int initial = 1;
            int count = pdfDoc.Pages.Count;
            int total = count;
            List<int> pageLst = GetPage(lv.SubItems[2].Text);
            if (pageLst != null && pageLst.Count > 0)
            {
                initial = pageLst[0];
                count = pageLst[pageLst.Count - 1];
                if (count > pdfDoc.Pages.Count) count = pdfDoc.Pages.Count;
                total = count - initial + 1;

            }

            if (!MainInfo.isReg)
            {
                if (pdfDoc.Pages.Count >= 3)
                {
                    count = 3;
                    initial = 1;
                    total = 3;
                }
            }
            for (int i = initial, c = 1; i <= count; i++, c++)
            {

                if (mainInfo.isClose) break;
                try
                {
                    while (((ItemInfomation)lv.Tag).Status == StatusType.Pause)
                    {
                        Application.DoEvents();
                    }
                    ms = new MemoryStream();
                    new_pdf_doc.Pages.Add(pdfDoc.Pages[i]);
                    new_pdf_doc.Save(ms, Aspose.Pdf.SaveFormat.Doc);
                    new_word_doc.AppendDocument(new Aspose.Words.Document(ms), Aspose.Words.ImportFormatMode.KeepSourceFormatting);
                    new_pdf_doc.Pages.Delete();


                }
                catch
                {

                    continue;
                }
                finally
                {
                    if (startRate == 50)
                    {
                        mainInfo.UpdateProcess(new TempClass(lv.Index, (c * 50 / total) + 50));
                    }
                    else
                    {
                        mainInfo.UpdateProcess(new TempClass(lv.Index, c * 100 / total));

                    }
                }


            }

            try
            {
                new_word_doc.Save(outPath);

            }
            catch { }
            finally
            {
                UpdateLstState(lv, mainInfo);
            }


        }


        /// <summary>
        ///更新列表状态
        /// </summary>
        /// <param name="lv"></param>
        /// <param name="mainInfo"></param>
        public void UpdateLstState(ListViewItem lv, MainInfo mainInfo)
        {
            ((ItemInfomation)lv.Tag).Status = StatusType.Done;
            mainInfo.diclst.Remove(((ItemInfomation)lv.Tag).FileFullPath);

            if (mainInfo.lstFile.IsAllFinished && mainInfo.fileQueue.Count == 0)
            {
                mainInfo.btnStart.Enabled = true;
                mainInfo.btnStart.BackgroundImage = Properties.Resources.startConversion;
                mainInfo.lstFile.Invalidate();
            }
        }


        private void FileToIMG(Form dlg = null, string fileType = null, int index = 0, ListViewItem lv = null)
        {

            ((ItemInfomation)lv.Tag).FileFullConvertPath = outPath + "1.jpg";
            Aspose.Pdf.Document pdfDoc = null;
            MainInfo mainInfo = dlg as MainInfo;
            int startRate = 0;

            if (fileType == ".pdf")
            {
                pdfDoc = pdf_doc;
                startRate = 0;

            }
            else if (fileType == ".doc" || fileType == ".docx")
            {
                pdfDoc = DocToPDF(dlg, lv.Index, lv);
                startRate = 50;
            }
            else if (fileType == ".ppt" || fileType == ".pptx")
            {
                pdfDoc = PptToPDF(dlg, lv.Index, lv);
                startRate = 50;
            }
            else if (fileType == ".xls" || fileType == ".xlsx")
            {
                pdfDoc = XlsToPDF(dlg, lv.Index, lv);
                startRate = 50;
            }
            Aspose.Pdf.Devices.JpegDevice jpg_device = new Aspose.Pdf.Devices.JpegDevice(new Aspose.Pdf.Devices.Resolution(300), 100);
            mainInfo.UpdateProcess(new TempClass(index, startRate));

            int initial = 1;
            int count = pdfDoc.Pages.Count;
            int total = count;
            List<int> pageLst = GetPage(lv.SubItems[2].Text);
            if (pageLst != null && pageLst.Count > 0)
            {
                initial = pageLst[0];
                count = pageLst[pageLst.Count - 1];
                if (count > pdfDoc.Pages.Count) count = pdfDoc.Pages.Count;
                total = count - initial + 1;

            }
            if (!MainInfo.isReg)
            {
                if (pdfDoc.Pages.Count >= 3)
                {
                    count = 3;
                    initial = 1;
                    total = 3;
                }
            }
            for (int i = initial, c = 1; i <= count; i++, c++)
            {
                if (mainInfo.isClose) break;
                try
                {
                    while (((ItemInfomation)lv.Tag).Status == StatusType.Pause)
                    {
                        Application.DoEvents();
                    }
                    jpg_device.Process(pdfDoc.Pages[i], outPath + i.ToString() + ".jpg");

                    if (startRate == 50)
                    {
                        mainInfo.UpdateProcess(new TempClass(lv.Index, (c * 50 / total) + 50));
                    }
                    else
                    {
                        //int cur = i * 100 / pdfDoc.Pages.Count;
                        mainInfo.UpdateProcess(new TempClass(lv.Index, c * 100 / total));

                    }
                }
                catch
                {
                    if (startRate == 50)
                    {
                        mainInfo.UpdateProcess(new TempClass(lv.Index, (c * 50 / total) + 50));
                    }
                    else
                    {
                        //int cur = i * 100 / pdfDoc.Pages.Count;
                        mainInfo.UpdateProcess(new TempClass(lv.Index, c * 100 / total));

                    }
                    continue;
                }

            }
            UpdateLstState(lv, mainInfo);
        }



        private void FiletToTXT(Form dlg = null, string fileType = null, int index = 0, ListViewItem lv = null)
        {
            outPath = outPath + ".txt";
            ((ItemInfomation)lv.Tag).FileFullConvertPath = outPath;
            Aspose.Pdf.Document pdfDoc = null;
            MainInfo mainInfo = dlg as MainInfo;
            int startRate = 0;

            if (fileType == ".pdf")
            {
                pdfDoc = pdf_doc;
                startRate = 0;

            }
            else if (fileType == ".ppt" || fileType == ".pptx")
            {
                pdfDoc = PptToPDF(dlg, lv.Index, lv);
                startRate = 50;
            }
            else if (fileType == ".xls" || fileType == ".xlsx")
            {
                pdfDoc = XlsToPDF(dlg, lv.Index, lv);
                startRate = 50;
            }
            else if (fileType == ".doc" || fileType == ".docx")
            {
                pdfDoc = DocToPDF(dlg, lv.Index, lv);
                startRate = 50;
            }
            Aspose.Pdf.Facades.PdfExtractor pdf_ex = new Aspose.Pdf.Facades.PdfExtractor(pdfDoc);

            FileStream fs = new FileStream(outPath, FileMode.Create);
            pdf_ex.ExtractTextMode = 0;

            int initial = 1;
            int count = pdfDoc.Pages.Count;
            int total = count;
            List<int> pageLst = GetPage(lv.SubItems[2].Text);
            if (pageLst != null && pageLst.Count > 0)
            {
                initial = pageLst[0];
                count = pageLst[pageLst.Count - 1];
                if (count > pdfDoc.Pages.Count) count = pdfDoc.Pages.Count;
                total = count - initial + 1;

            }
            if (!MainInfo.isReg)
            {
                if (pdfDoc.Pages.Count >= 3)
                {
                    count = 3;
                    initial = 1;
                    total = 3;
                }
            }
            for (int i = initial, c = 1; i <= count; i++, c++)
            {
                if (mainInfo.isClose) break;
                try
                {
                    while (((ItemInfomation)lv.Tag).Status == StatusType.Pause)
                    {
                        Application.DoEvents();
                    }
                    pdf_ex.StartPage = i;
                    pdf_ex.EndPage = i;
                    pdf_ex.ExtractText(Encoding.UTF8);
                    pdf_ex.GetText(fs);


                }
                catch
                {
                    if (startRate == 50)
                    {
                        mainInfo.UpdateProcess(new TempClass(lv.Index, (c * 50 / total) + 50));
                    }
                    else
                    {
                        mainInfo.UpdateProcess(new TempClass(lv.Index, c * 100 / total));

                    }
                    continue;
                }
                if (startRate == 50)
                {
                    mainInfo.UpdateProcess(new TempClass(lv.Index, (c * 50 / total) + 50));
                }
                else
                {
                    mainInfo.UpdateProcess(new TempClass(lv.Index, c * 100 / total));

                }

            }
            fs.Close();

            UpdateLstState(lv, mainInfo);
        }

        private void IMGToPDF(Form dlg = null, string fileType = null, int index = 0, ListViewItem lv = null)
        {
            try
            {
                outPath = outPath + ".pdf";


                MainInfo mainInfo = dlg as MainInfo;

                //获取用户设置是否合并图片 1合并，0不合并
                string isMerger = mainInfo.cbIsMerger.Checked ? "1" : "0";
                if (isMerger == "1")
                {
                    string directoryName = Path.GetDirectoryName(outPath);

                    ((ItemInfomation)lv.Tag).FileFullConvertPath = directoryName + "\\" + Path.GetFileNameWithoutExtension(mainInfo.lstFile.Items[mainInfo.lstFile.Items.Count - 1].SubItems[1].Text) + ".pdf";

                    int count = mainInfo.lstFile.Items.Count;
                    if (!MainInfo.isReg)
                    {
                        if (count >= 3)
                        {
                            count = 3;
                        }
                    }
                    #region 将所有图片合并到一个PDF中
                    if (lv.Index == mainInfo.lstFile.Items.Count - 1)
                    {
                        Aspose.Pdf.Generator.Pdf pdfGenerator = new Aspose.Pdf.Generator.Pdf();
                        for (int i = 0; i < count; i++)
                        {

                            Aspose.Pdf.Generator.Section st = pdfGenerator.Sections.Add();

                            Aspose.Pdf.Generator.Image img = new Aspose.Pdf.Generator.Image(st);

                            st.Paragraphs.Add(img);

                            img.ImageInfo.File = ((ItemInfomation)mainInfo.lstFile.Items[i].Tag).FileFullPath;

                            //"图片文件(*.jpg,*.gif,*.bmp,*.png,*.tiff)|*.jpg;*.gif;*.bmp;*.png;*.tiff";
                            switch (fileType.ToLower())
                            {
                                case ".jpeg":
                                case ".jpg":

                                    img.ImageInfo.ImageFileType = Aspose.Pdf.Generator.ImageFileType.Jpeg;
                                    break;
                                case ".gif":

                                    img.ImageInfo.ImageFileType = Aspose.Pdf.Generator.ImageFileType.Gif;
                                    break;

                                case ".bmp":

                                    img.ImageInfo.ImageFileType = Aspose.Pdf.Generator.ImageFileType.Bmp;
                                    break;

                                case ".png":

                                    img.ImageInfo.ImageFileType = Aspose.Pdf.Generator.ImageFileType.Png;
                                    break;
                                case ".tif":
                                case ".tiff":
                                    img.ImageInfo.ImageFileType = Aspose.Pdf.Generator.ImageFileType.Tiff;
                                    break;

                                default:
                                    img.ImageInfo.ImageFileType = Aspose.Pdf.Generator.ImageFileType.Jpeg;

                                    break;
                            }
                        }


                        pdfGenerator.Save(outPath);
                        mainInfo.UpdateProcess(new TempClass(lv.Index, 100));

                    }
                    else
                    {
                        mainInfo.UpdateProcess(new TempClass(lv.Index, 100));
                    }
                    #endregion

                }
                else
                {

                    #region   将单张图片保存到一个PDF中
                    ((ItemInfomation)lv.Tag).FileFullConvertPath = outPath;
                    Aspose.Pdf.Generator.Pdf pdfGenerator = new Aspose.Pdf.Generator.Pdf();


                    Aspose.Pdf.Generator.Section st = pdfGenerator.Sections.Add();

                    Aspose.Pdf.Generator.Image img = new Aspose.Pdf.Generator.Image(st);

                    st.Paragraphs.Add(img);

                    img.ImageInfo.File = ((ItemInfomation)lv.Tag).FileFullPath;

                    //"图片文件(*.jpg,*.gif,*.bmp,*.png,*.tiff)|*.jpg;*.gif;*.bmp;*.png;*.tiff";
                    switch (fileType.ToLower())
                    {
                        case ".jpeg":
                        case ".jpg":

                            img.ImageInfo.ImageFileType = Aspose.Pdf.Generator.ImageFileType.Jpeg;
                            break;
                        case ".gif":

                            img.ImageInfo.ImageFileType = Aspose.Pdf.Generator.ImageFileType.Gif;
                            break;

                        case ".bmp":

                            img.ImageInfo.ImageFileType = Aspose.Pdf.Generator.ImageFileType.Bmp;
                            break;

                        case ".png":

                            img.ImageInfo.ImageFileType = Aspose.Pdf.Generator.ImageFileType.Png;
                            break;
                        case ".tif":
                        case ".tiff":
                            img.ImageInfo.ImageFileType = Aspose.Pdf.Generator.ImageFileType.Tiff;
                            break;

                        default:
                            img.ImageInfo.ImageFileType = Aspose.Pdf.Generator.ImageFileType.Jpeg;

                            break;
                    }
                    pdfGenerator.Save(outPath);
                    mainInfo.UpdateProcess(new TempClass(lv.Index, 100));
                    #endregion
                }


                UpdateLstState(lv, mainInfo);

            }
            catch
            {

                return;
            }

        }



        private void pdf_to_html_callback(object outPath)
        {
            try
            {
                if (!MainInfo.isReg)
                {

                    if (pdf_doc.Pages.Count > 3)
                    {
                        int[] delete_page;
                        delete_page = new int[pdf_doc.Pages.Count - 3];
                        for (int i = 4, j = 1; i <= pdf_doc.Pages.Count; i++, j++)
                        {
                            delete_page[j - 1] = i;

                        }
                        pdf_doc.Pages.Delete(delete_page);
                    }

                }

                pdf_doc.Save(outPath.ToString(), Aspose.Pdf.SaveFormat.Html);
            }
            catch (Exception ex)
            {

            }

        }

        private void FileToHTML(Form dlg = null, string fileType = null, int index = 0, ListViewItem lv = null)
        {
            ((ItemInfomation)lv.Tag).FileFullConvertPath = outPath + ".html";
            Aspose.Pdf.Document pdfDoc = null;
            MainInfo mainInfo = dlg as MainInfo;
            int startRate = 0;

            if (fileType == ".pdf")
            {
                pdfDoc = pdf_doc;
                startRate = 0;

            }
            else if (fileType == ".doc" || fileType == ".docx")
            {
                pdf_doc = DocToPDF(dlg, lv.Index, lv);
                startRate = 50;
            }
            else if (fileType == ".ppt" || fileType == ".pptx")
            {
                pdf_doc = PptToPDF(dlg, lv.Index, lv);
                startRate = 50;
            }
            else if (fileType == ".xls" || fileType == ".xlsx")
            {
                pdf_doc = XlsToPDF(dlg, lv.Index, lv);
                startRate = 50;
            }

            Thread cvrt = new Thread(pdf_to_html_callback);
            cvrt.Start(outPath + ".html");
            int cur = 1, old_cur = cur;
            int max = pdf_doc.Pages.Count;
            if (!MainInfo.isReg)
            {
                if (max >= 3)
                {
                    max = 3;
                }
            }

            mainInfo.UpdateProcess(new TempClass(lv.Index, startRate));


            while (true)
            {
                if (mainInfo.isClose) break;
                old_cur = cur;

                /*if (File.Exists(global_config.target_dic + Path.GetFileNameWithoutExtension(file_path) + "_files\\img_" + cur.ToString().PadLeft(2, '0') + ".*"))
                {
                    ++cur;
                }*/
                try
                {

                    if (Directory.GetFiles(outPath + "_files").Length != 0 || Directory.GetFiles(outPath + "_files",
                    "img_" + cur.ToString().PadLeft(2, '0') + ".*").Length != 0)
                    {
                        ++cur;
                    }

                }
                catch
                {

                }

                if (cur == max)
                    break;

                if (startRate == 50)
                {
                    if (old_cur != cur)
                    {
                        mainInfo.UpdateProcess(new TempClass(lv.Index, (cur * 50 / max) + 50));

                    }
                }
                else
                {
                    mainInfo.UpdateProcess(new TempClass(lv.Index, (cur * 100 / max)));
                }



                Thread.Sleep(30);
            }

            try
            {
                bool result = false;

                while (true)
                {
                    if (mainInfo.isClose) break;
                    if (Directory.GetFiles(outPath + "_files",
         "style.css").Length != 0)
                    {
                        result = true;
                        break;
                    }
                }
                if (result)
                {
                    mainInfo.UpdateProcess(new TempClass(lv.Index, 100));
                }

            }
            catch (Exception)
            {


            }




            UpdateLstState(lv, mainInfo);
        }


        private void FileToExcel(Form dlg = null, string fileType = null, int index = 0, ListViewItem lv = null)
        {
            outPath = outPath + ".xlsx";
            ItemInfomation info = ((ItemInfomation)lv.Tag);
            info.FileFullConvertPath = outPath;
            Aspose.Pdf.Document pdfDoc = null;
            MainInfo mainInfo = dlg as MainInfo;
            int startRate = 0;

            if (fileType == ".pdf")
            {
                pdfDoc = pdf_doc;
                startRate = 0;

            }
            else if (fileType == ".ppt" || fileType == ".pptx")
            {
                pdfDoc = PptToPDF(dlg, lv.Index, lv);
                startRate = 50;
            }
            else if (fileType == ".doc" || fileType == ".docx")
            {
                pdfDoc = DocToPDF(dlg, lv.Index, lv);
                startRate = 50;
            }
            Aspose.Cells.Workbook work_book = new Aspose.Cells.Workbook();
            Aspose.Cells.Workbook temp_book;
            MemoryStream ms;
            Aspose.Pdf.Document new_pdf_doc = new Aspose.Pdf.Document();

            work_book.Worksheets.Clear();

            int initial = 1;
            int count = pdfDoc.Pages.Count;
            int total = count;
            List<int> pageLst = GetPage(lv.SubItems[2].Text);
            if (pageLst != null && pageLst.Count > 0)
            {
                initial = pageLst[0];
                count = pageLst[pageLst.Count - 1];
                if (count > pdfDoc.Pages.Count) count = pdfDoc.Pages.Count;
                total = count - initial + 1;


            }
            if (!MainInfo.isReg)
            {
                if (pdfDoc.Pages.Count >= 3)
                {
                    count = 3;
                    initial = 1;
                    total = 3;
                }
            }
            for (int i = initial, c = 1; i <= count; i++, c++)
            {
                if (mainInfo.isClose) break;
                try
                {
                    while (((ItemInfomation)lv.Tag).Status == StatusType.Pause)
                    {
                        Application.DoEvents();
                    }
                    ms = new MemoryStream();
                    new_pdf_doc.Pages.Add(pdfDoc.Pages[i]);
                    new_pdf_doc.Save(ms, Aspose.Pdf.SaveFormat.Excel);

                    temp_book = new Aspose.Cells.Workbook(ms);
                    work_book.Worksheets.Add(i.ToString());
                    work_book.Worksheets[c - 1].Copy(temp_book.Worksheets[0]);
                    new_pdf_doc.Pages.Delete();
                }
                catch (Exception e)
                {
                    if (!mainInfo.diclst.ContainsKey(info.FileFullPath))
                        return;
                    if (startRate == 50)
                    {
                        mainInfo.UpdateProcess(new TempClass(lv.Index, (c * 50 / total) + 50));
                    }
                    else
                    {
                        int cur = c * 100 / total;
                        mainInfo.UpdateProcess(new TempClass(lv.Index, c * 100 / total));

                    }
                    continue;
                }
                if (!mainInfo.diclst.ContainsKey(info.FileFullPath))
                    return;
                if (startRate == 50)
                {
                    mainInfo.UpdateProcess(new TempClass(lv.Index, (c * 50 / total) + 50));
                }
                else
                {
                    int cur = c * 100 / total;
                    mainInfo.UpdateProcess(new TempClass(lv.Index, c * 100 / total));

                }


            }


            work_book.Save(outPath);

            UpdateLstState(lv, mainInfo);
        }

        private void FileToPPT(Form dlg = null, string fileType = null, int index = 0, ListViewItem lv = null)
        {
            outPath = outPath + ".ppt";
            ((ItemInfomation)lv.Tag).FileFullConvertPath = outPath;
            Aspose.Pdf.Document pdfDoc = null;
            MainInfo mainInfo = dlg as MainInfo;
            int startRate = 0;

            if (fileType == ".pdf")
            {
                pdfDoc = pdf_doc;
                startRate = 0;

            }
            else if (fileType == ".xls" || fileType == ".xlsx")
            {
                pdfDoc = XlsToPDF(dlg, lv.Index, lv);
                startRate = 50;
            }
            else if (fileType == ".doc" || fileType == ".docx")
            {
                pdfDoc = DocToPDF(dlg, lv.Index, lv);
                startRate = 50;
            }
            Aspose.Slides.Presentation pres = new Aspose.Slides.Presentation();
            Aspose.Pdf.Devices.JpegDevice jpg_device = new Aspose.Pdf.Devices.JpegDevice(new Aspose.Pdf.Devices.Resolution(300), 100);
            Aspose.Slides.IPPImage img_ex;
            MemoryStream ms;
            int sy;

            int initial = 1;
            int count = pdfDoc.Pages.Count;
            int total = count;
            List<int> pageLst = GetPage(lv.SubItems[2].Text);
            if (pageLst != null && pageLst.Count > 0)
            {
                initial = pageLst[0];
                count = pageLst[pageLst.Count - 1];
                if (count > pdfDoc.Pages.Count) count = pdfDoc.Pages.Count;
                total = count - initial + 1;

            }
            if (!MainInfo.isReg)
            {
                if (pdfDoc.Pages.Count >= 3)
                {
                    count = 3;
                    initial = 1;
                    total = 3;
                }
            }
            for (int i = initial, c = 1; i <= count; i++, c++)
            {
                if (mainInfo.isClose) break;
                try
                {
                    while (((ItemInfomation)lv.Tag).Status == StatusType.Pause)
                    {
                        Application.DoEvents();
                    }

                    pres.Slides.AddEmptySlide(pres.LayoutSlides[0]);
                    pres.Slides[c].Shapes.AddAutoShape(Aspose.Slides.ShapeType.Rectangle, 10, 20, System.Convert.ToSingle(mainInfo.txtWidth.Text)
                        , System.Convert.ToSingle(mainInfo.txtHeight.Text));
                    int scount = pres.Slides[c].Shapes.Count;
                    sy = scount - 1;
                    pres.Slides[c].Shapes[sy].FillFormat.FillType = Aspose.Slides.FillType.Picture;
                    pres.Slides[c].Shapes[sy].FillFormat.PictureFillFormat.PictureFillMode = Aspose.Slides.PictureFillMode.Stretch;
                    ms = new MemoryStream();
                    jpg_device.Process(pdfDoc.Pages[i], ms);
                    img_ex = pres.Images.AddImage(new Bitmap(ms));
                    pres.Slides[c].Shapes[sy].FillFormat.PictureFillFormat.Picture.Image = img_ex;
                }
                catch
                {
                    if (startRate == 50)
                    {
                        mainInfo.UpdateProcess(new TempClass(lv.Index, (c * 50 / total) + 50));
                    }
                    else
                    {
                        mainInfo.UpdateProcess(new TempClass(lv.Index, c * 100 / total));

                    } continue;
                }
                if (startRate == 50)
                {
                    mainInfo.UpdateProcess(new TempClass(lv.Index, (c * 50 / total) + 50));
                }
                else
                {
                    mainInfo.UpdateProcess(new TempClass(lv.Index, c * 100 / total));

                }


            }

            try
            {
                pres.Save(outPath, Aspose.Slides.Export.SaveFormat.Ppt);
            }
            catch { }


            UpdateLstState(lv, mainInfo);
        }


        private Aspose.Pdf.Document DocToPDF(Form dlg = null, int index = 0, ListViewItem lv = null)
        {
            if (word_doc == null) return null;
            int EndRate = 50;
            int initial = 1;
            int count = word_doc.PageCount;
            int total = count;

            if (targetFormat == FORMAT.DOC2PDF)
            {
                List<int> pageLst = GetPage(lv.SubItems[2].Text);
                if (pageLst != null && pageLst.Count > 0)
                {
                    initial = pageLst[0];
                    count = pageLst[pageLst.Count - 1];
                    if (count > word_doc.PageCount) count = word_doc.PageCount;
                    total = count - initial + 1;

                }
                outPath = outPath + ".pdf";
                EndRate = 100;
                ((ItemInfomation)lv.Tag).FileFullConvertPath = outPath;
            }
            Aspose.Words.Document new_word_doc;
            Aspose.Pdf.Document new_pdf_doc = new Aspose.Pdf.Document();
            MainInfo mainInfo = dlg as MainInfo;
            try
            {

                MemoryStream ms;
                new_pdf_doc.Pages.Delete();
                if (!MainInfo.isReg)
                {
                    if (word_doc.PageCount >= 3)
                    {
                        count = 3;
                        initial = 1;
                        total = 3;
                    }
                }
                for (int i = initial - 1, c = 1; i < count; i++, c++)
                {
                    try
                    {
                        while (((ItemInfomation)lv.Tag).Status == StatusType.Pause)
                        {
                            Application.DoEvents();
                        }
                        ms = new MemoryStream();
                        new_word_doc = new Aspose.Words.Document();
                        new_word_doc.ChildNodes.RemoveAt(0);
                        new_word_doc.AppendChild(new_word_doc.ImportNode(word_doc.ChildNodes[i], true));

                        new_word_doc.Save(ms, Aspose.Words.SaveFormat.Pdf);
                        new_pdf_doc.Pages.Add((new Aspose.Pdf.Document(ms)).Pages);
                        int cur = c * EndRate / total;
                        mainInfo.UpdateProcess(new TempClass(lv.Index, cur));
                    }
                    catch { mainInfo.UpdateProcess(new TempClass(lv.Index, c * EndRate / total)); continue; }

                }
                if (targetFormat == FORMAT.DOC2PDF)
                {
                    new_pdf_doc.Save(outPath);
                    UpdateLstState(lv, mainInfo);
                }


            }
            catch (Exception)
            {

                return null;
            }
            return new_pdf_doc;

        }

        private Aspose.Pdf.Document XlsToPDF(Form dlg = null, int index = 0, ListViewItem lv = null)
        {
            if (excel_doc == null) return null;
            int EndRate = 50;
            int initial = 1;
            int count = excel_doc.Worksheets.Count;
            int total = count;
            if (targetFormat == FORMAT.Excel2PDF)
            {
                List<int> pageLst = GetPage(lv.SubItems[2].Text);
                if (pageLst != null && pageLst.Count > 0)
                {
                    initial = pageLst[0];
                    count = pageLst[pageLst.Count - 1];
                    if (count > excel_doc.Worksheets.Count) count = excel_doc.Worksheets.Count;
                    total = count - initial + 1;

                }
                outPath = outPath + ".pdf";
                EndRate = 100;
                ((ItemInfomation)lv.Tag).FileFullConvertPath = outPath;
            }
            Aspose.Pdf.Document new_pdf_doc = null;
            try
            {
                Aspose.Cells.Workbook new_xls_doc = new Aspose.Cells.Workbook();
                MainInfo mainInfo = dlg as MainInfo;
                MemoryStream ms;

                new_xls_doc.Worksheets.Clear();

                new_pdf_doc = new Aspose.Pdf.Document();

                if (!MainInfo.isReg)
                {
                    if (excel_doc.Worksheets.Count >= 3)
                    {
                        count = 3;
                        initial = 1;
                        total = 3;
                    }
                }
                for (int i = initial - 1, c = 1; i < count; i++, c++)
                {
                    if (mainInfo.isClose) break;
                    try
                    {
                        while (((ItemInfomation)lv.Tag).Status == StatusType.Pause)
                        {
                            Application.DoEvents();
                        }
                        ms = new MemoryStream();
                        new_xls_doc.Worksheets.Add(i.ToString());
                        new_xls_doc.Worksheets[0].Copy(excel_doc.Worksheets[i]);
                        new_xls_doc.Save(ms, Aspose.Cells.SaveFormat.Pdf);
                        new_pdf_doc.Pages.Add(new Aspose.Pdf.Document(ms).Pages);
                        new_xls_doc.Worksheets.RemoveAt(0);
                        int cur = c * EndRate / total;
                        mainInfo.UpdateProcess(new TempClass(lv.Index, cur));
                    }
                    catch { mainInfo.UpdateProcess(new TempClass(lv.Index, c * EndRate / total)); continue; }

                }
                if (targetFormat == FORMAT.Excel2PDF)
                {
                    new_pdf_doc.Save(outPath);
                    UpdateLstState(lv, mainInfo);
                }

            }
            catch (Exception)
            {

                return null;
            }
            return new_pdf_doc;

        }

        private Aspose.Pdf.Document PptToPDF(Form dlg = null, int index = 0, ListViewItem lv = null)
        {
            if (ppt_doc == null) return null;
            int EndRate = 50;
            int initial = 1;
            int count = ppt_doc.Slides.Count;
            int total = count;
            if (targetFormat == FORMAT.PPT2PDF)
            {
                List<int> pageLst = GetPage(lv.SubItems[2].Text);
                if (pageLst != null && pageLst.Count > 0)
                {
                    initial = pageLst[0];
                    count = pageLst[pageLst.Count - 1];
                    if (count > ppt_doc.Slides.Count) count = ppt_doc.Slides.Count;
                    total = count - initial + 1;

                }
                outPath = outPath + ".pdf";
                EndRate = 100;
                ((ItemInfomation)lv.Tag).FileFullConvertPath = outPath;
            }
            Aspose.Slides.Presentation new_ppt_doc = new Aspose.Slides.Presentation();
            Aspose.Pdf.Document new_pdf_doc = new Aspose.Pdf.Document();
            MainInfo mainInfo = dlg as MainInfo;
            try
            {

                MemoryStream ms;
                new_ppt_doc.Slides.RemoveAt(0);


                if (!MainInfo.isReg)
                {
                    if (ppt_doc.Slides.Count >= 3)
                    {
                        count = 3;
                        initial = 1;
                        total = 3;
                    }
                }
                for (int i = initial - 1, c = 1; i < count; i++, c++)
                {
                    if (mainInfo.isClose) break;
                    try
                    {
                        while (((ItemInfomation)lv.Tag).Status == StatusType.Pause)
                        {
                            Application.DoEvents();
                        }
                        ms = new MemoryStream();
                        new_ppt_doc.Slides.AddClone(ppt_doc.Slides[i]);
                        new_ppt_doc.Save(ms, Aspose.Slides.Export.SaveFormat.Pdf);
                        new_ppt_doc.Slides.RemoveAt(0);
                        new_pdf_doc.Pages.Add(new Aspose.Pdf.Document(ms).Pages);

                        mainInfo.UpdateProcess(new TempClass(lv.Index, c * EndRate / total));
                    }
                    catch { mainInfo.UpdateProcess(new TempClass(lv.Index, c * EndRate / total)); continue; }


                }

                if (targetFormat == FORMAT.PPT2PDF)
                {
                    new_pdf_doc.Save(outPath);
                    UpdateLstState(lv, mainInfo);
                }


            }
            catch (Exception)
            {

                return null;
            }
            return new_pdf_doc;

        }

        public bool Can_work()
        {
            return file_can_work;
        }

        public void Close()
        {
            try
            {
                if (fileStream != null)
                    fileStream.Close();
            }
            catch
            {

            }
        }

        public string Get_err_msg()
        {
            return err_msg;
        }

    }
}
