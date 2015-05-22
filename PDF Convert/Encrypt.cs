using System;
using System.Collections.Generic;
using System.Text;
using System.Security.Cryptography;
using System.IO;

namespace PDF_Convert
{
    public static class Encrypt
    {
        public static string APPTITLE;

        static Encrypt()
        {
            string tmp = new ini_config("config.ini").read_ini("Data");
            string language = new ini_config("config.ini").read_ini("language");
            switch (language.ToLower())
            {
                case "zh-cn":
                    tmp = new ini_config("config.ini").read_ini("Data");
                    break;
                case "en":
                    tmp = new ini_config("config.ini").read_ini("EData");
                    break;
            }
            if (tmp == string.Empty)
            {
                APPTITLE = "PDF Converter";
                return;
            }

            APPTITLE = DecryptDES(tmp);
        }
        public static string Refresh()
        {
            string tmp = new ini_config("config.ini").read_ini("Data");
            string language = new ini_config("config.ini").read_ini("language");
            switch (language.ToLower())
            {
                case "zh-cn":
                    tmp = new ini_config("config.ini").read_ini("Data");
                    break;
                case "en":
                    tmp = new ini_config("config.ini").read_ini("EData");
                    break;
            }
            if (tmp == string.Empty)
            {
                APPTITLE = "PDF Converter";
                return APPTITLE;
            }

            return APPTITLE = DecryptDES(tmp);
        }

        public static string EncryptDES(string encryptString)
        {
            try
            {
                return System.Convert.ToBase64String(Encoding.Default.GetBytes(encryptString));
            }
            catch
            {
                return encryptString;
            }
        }

        public static string DecryptDES(string decryptString)
        {
            try
            {
                string tmp = Encoding.Default.GetString(System.Convert.FromBase64String(decryptString));
                if (tmp == string.Empty)
                    return "PDF Converter";
                return tmp;
            }
            catch
            {
                return "PDF Converter";
            }
        }
    }
}
