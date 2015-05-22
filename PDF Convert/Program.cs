using System;
using System.Collections.Generic;
using System.Globalization;
using System.Resources;
using System.Threading;
using System.Windows.Forms;

namespace PDF_Convert
{
    static class Program
    {


        static ResourceManager rm = new ResourceManager(typeof(MainInfo));
        static string language;
        static ini_config ini = new ini_config("config.ini");
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            language = ini.read_ini("language");
            if (string.IsNullOrEmpty(language))
                language = System.Globalization.CultureInfo.InstalledUICulture.Name;


            Thread.CurrentThread.CurrentUICulture = new CultureInfo(language);
            License();
            RunForm();
        }


        public static void License()
        {
            string licensePath = System.AppDomain.CurrentDomain.SetupInformation.ApplicationBase + "\\Aspose.Total.lic";
            Aspose.Pdf.License pdf_license = new Aspose.Pdf.License();
            pdf_license.SetLicense(licensePath);

            Aspose.Words.License word_license = new Aspose.Words.License();
            word_license.SetLicense(licensePath);

            Aspose.Cells.License excel_license = new Aspose.Cells.License();
            excel_license.SetLicense(licensePath);

            Aspose.Slides.License ppt_license = new Aspose.Slides.License();
            ppt_license.SetLicense(licensePath);


        }

        static void RunForm()
        {
            bool mutex_succ;
            Mutex mutex = new Mutex(false, "XJ_PDF_CONVERT2", out mutex_succ);
            if (!mutex_succ)
            {
                //MessageBox.Show("本程序已经在运行了", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                MessageBox.Show(rm.GetString("msg12"), rm.GetString("Tips"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            string form_type = new ini_config("config.ini").read_ini("formtype");

            Application.Run(new MainInfo());

            try
            {
                mutex.ReleaseMutex();
            }
            catch { }
        }
    }
}
