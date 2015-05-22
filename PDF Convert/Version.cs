using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Windows.Forms;

namespace PDF_Convert
{
    public static class Version
    {
        static ini_config ini = new ini_config("config.ini");
        public static string version = string.IsNullOrEmpty(ini.read_ini("Version")) ? Application.ProductVersion : ini.read_ini("Version");
        private static string cells_ver = "7.7.1.0";
        private static string pdf_ver = "8.8.0.0";
        private static string slides_ver = "8.2.0.0";
        private static string words_ver = "13.12.0.0";
        public static Dictionary<string, string> dic_ver = new Dictionary<string, string>();

        static Version()
        {
            dic_ver.Add("exe_main", version);
            dic_ver.Add("dll_cells", cells_ver);
            dic_ver.Add("dll_pdf", pdf_ver);
            dic_ver.Add("dll_slides", slides_ver);
            dic_ver.Add("dll_words", words_ver);

        }

        /// <summary>
        /// POST请求在线统计
        /// </summary>
        /// <param name="url">请求的链接地址,如：http://statistical.jsocr.com/Default.aspx </param>
        /// <param name="softName">软件名称</param>
        /// <param name="version">版本号</param>
        /// <param name="encoding">机器码</param>
        public static void Post(string url, string softName, string softType, string version, string encoding, string target, string mehodObject)
        {
            try
            {
                WebClient w = new WebClient();

                System.Collections.Specialized.NameValueCollection VarPost = new System.Collections.Specialized.NameValueCollection();
                VarPost.Add("softName", softName.Trim());
                VarPost.Add("softType", softType.Trim());
                VarPost.Add("version", version.Trim());
                VarPost.Add("encoding", encoding.Trim());
                VarPost.Add("target", target.Trim());
                VarPost.Add("mehodObject", mehodObject.Trim());
                VarPost.Add("recordState", "使用");
                VarPost.Add("packageName", "无");
                VarPost.Add("packageVersion", "无");

                byte[] byRemoteInfo = w.UploadValues(url, "POST", VarPost);

            }
            catch { }


        }

        public static string GetParamName(string param)
        {
            string paramName = string.Empty;
            string tmp = new ini_config("config.ini").read_ini(param);
            if (param == "Data")
            {
                paramName = DecryptDES(tmp);
                return paramName;
            }
            else
            {
                paramName = tmp;
            }

            return paramName;
        }

        public static string DecryptDES(string decryptString)
        {
            try
            {
                string tmp = Encoding.Default.GetString(System.Convert.FromBase64String(decryptString));
                if (tmp == string.Empty)
                    return "迅捷PDF转换器";
                return tmp;
            }
            catch
            {
                return "迅捷PDF转换器";
            }
        }
    }
}
