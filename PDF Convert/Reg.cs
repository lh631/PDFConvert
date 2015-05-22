using System;
using System.Collections.Generic;
using System.Text;
using System.Management;
using System.Security.Cryptography;
using System.Runtime.InteropServices;
using Microsoft.Win32;

namespace PDF_Convert
{
    partial class reg
    {

        private int get_cpu_code()
        {
            string cpu_id = "";
            string md5 = "";
            ManagementObjectCollection obj = new ManagementClass("Win32_Processor").GetInstances();
            foreach (ManagementObject mo in obj)
            {
                cpu_id = mo.Properties["ProcessorId"].Value.ToString();
                break;
            }

            if (cpu_id == string.Empty)
                return 0;
            if (cpu_id.Length == 16)
            {
                cpu_id = string.Format("{0}-{1}-00000000-00000000", cpu_id.Substring(0, 8), cpu_id.Substring(8, 8));
            }
            else if (cpu_id.Length == 32)
            {
                cpu_id = string.Format("{0}-{1}-{2}-{3}", cpu_id.Substring(0, 8), cpu_id.Substring(8, 8), cpu_id.Substring(16, 8),
                    cpu_id.Substring(24, 8));
            }

            md5 = get_md5(cpu_id);


            //计算----------------------------------------------------------------------

            string x = "", cpu_code = "";
            int y, z;
            for (int i = 1; i <= 27; i += 3)
            {
                x = md5.Substring(i + 2, 1);
                y = Encoding.ASCII.GetBytes(x)[0];
                z = y % encode[(i + 2) / 3 - 1];
                if (0 == z)
                    z = 8;
                cpu_code = cpu_code + z.ToString();
            }
            return int.Parse(cpu_code);
        }

        private string get_md5(string str, int bit = 32, bool lower_case = true)
        {
            byte[] md5_16;
            string md5 = "";
            MD5CryptoServiceProvider md5_csp = new MD5CryptoServiceProvider();

            md5_16 = md5_csp.ComputeHash(Encoding.ASCII.GetBytes(str));

            for (int i = 0; i < md5_16.Length; i++)
            {
                if (32 == bit)
                    md5 += System.Convert.ToString(md5_16[i], 16).PadLeft(2, '0');
                else
                    md5 += System.Convert.ToString(md5_16[i], 16);
            }

            return lower_case ? md5 : md5.ToUpper();

        }

        public string get_machine_code()
        {
            return get_cpu_code().ToString();
        }

        public string get_reg_code(string machine_code = "")
        {
            if (machine_code == string.Empty)
                machine_code = get_machine_code();

            long param = System.Convert.ToInt64(machine_code);
            string str = "";
            for (int i = 0; i < 100; ++i)
            {
                param = param * 2;
                str = param.ToString();
                if (str.Length <= 12)
                    param = System.Convert.ToInt64(str);
                else
                    param = System.Convert.ToInt64(str.Substring(0, 12));
            }
            return param.ToString();
        }

        public bool write_reg_code(string reg_code)
        {
            return ini.write_ini("RegCode", reg_code);
        }

        public bool Is_Reg()
        {
            return (ini.read_ini("RegCode") == get_reg_code());
        }
    }

    partial class reg
    {
        readonly int[] encode = new int[] { 8, 7, 8, 6, 5, 7, 9, 9, 8 };
        ini_config ini;
        registry register;
        public reg()
        {
            ini = new ini_config("config.ini");
            register = new registry("XJPDF Convert");
        }
    }

    partial class ini_config
    {
        public bool write_ini(string node_name, string str, string section_name = "App")
        {
            return WritePrivateProfileString(section_name, node_name, str, get_app_dic() + ini_name);
        }

        public string read_ini(string node_name, string section_name = "App")
        {
            StringBuilder str_buffer = new StringBuilder(100);
            GetPrivateProfileString(section_name, node_name, "", str_buffer, 100, get_app_dic() + ini_name);
            return str_buffer.ToString();
        }

        private string get_app_dic()
        {
            string app_full_path = System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName;
            return app_full_path.Substring(0, app_full_path.LastIndexOf("\\") + 1);
        }
    }

    partial class ini_config
    {
        string ini_name;

        public ini_config(string file_name)
        {
            ini_name = file_name;
        }

        [DllImport("kernel32")]
        private static extern bool WritePrivateProfileString(string section, string key, string val, string filePath);

        [DllImport("kernel32")]
        private static extern bool GetPrivateProfileString(string section, string key, string defVal, System.Text.StringBuilder retVal, int size, string filePath);
    }


    partial class registry
    {
        public int get_reg_int(string item)
        {
            try
            {
                RegistryKey dest = Registry.CurrentUser.OpenSubKey("software\\" + reg_item, false);
                int res = System.Convert.ToInt32(dest.GetValue(item));
                dest.Close();
                return res;
            }
            catch
            {
                return READ_ERROR;
            }
        }

        public byte[] get_reg_byte(string item)
        {
            try
            {
                RegistryKey dest = Registry.CurrentUser.OpenSubKey("software\\" + reg_item, false);
                byte[] res = (byte[])dest.GetValue(item);
                dest.Close();
                return res;
            }
            catch
            {
                return null;
            }
        }


        public string get_reg_string(string item)
        {
            try
            {
                RegistryKey dest = Registry.CurrentUser.OpenSubKey("software\\" + reg_item, false);
                string res = dest.GetValue(item).ToString();
                dest.Close();
                return res;
            }
            catch
            {
                return "";
            }
        }

        public bool set_reg_int(string item, int data)
        {
            try
            {
                if (!is_reg_exist(reg_item))
                {
                    create_reg_item(reg_item);
                }
                RegistryKey dest = Registry.CurrentUser.OpenSubKey("software\\" + reg_item, true);
                dest.SetValue(item, data);
                dest.Close();
                return true;
            }
            catch
            {
                return false;
            }

        }

        public bool set_reg_byte(string item, ref byte[] data)
        {
            try
            {
                if (!is_reg_exist(reg_item))
                {
                    create_reg_item(reg_item);
                }
                RegistryKey dest = Registry.CurrentUser.OpenSubKey("software\\" + reg_item, true);
                dest.SetValue(item, data);
                dest.Close();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool set_reg_string(string item, string data)
        {
            try
            {
                if (!is_reg_exist(reg_item))
                {
                    create_reg_item(reg_item);
                }
                RegistryKey dest = Registry.CurrentUser.OpenSubKey("software" + reg_item, true);
                dest.SetValue(item, data);
                dest.Close();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool is_reg_exist(string item)
        {
            try
            {
                RegistryKey dest = Registry.CurrentUser.OpenSubKey("software", false);
                string[] sub_names = dest.GetSubKeyNames();

                foreach (string sub in sub_names)
                {
                    if (item == sub)
                        goto YES;
                }
                dest.Close();
                return false;
            YES:
                dest.Close();
                return true;
            }
            catch
            {
                return false;
            }
        }

        private bool create_reg_item(string item)
        {
            try
            {
                RegistryKey dest = Registry.CurrentUser.OpenSubKey("software", true);
                dest.CreateSubKey(item);
                dest.Close();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }

    partial class registry
    {
        string reg_item;
        public readonly int READ_ERROR = -9999;
        public registry(string item)
        {
            reg_item = item;
        }
    }

}
