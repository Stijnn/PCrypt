using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace PCrypt.Source.Filesystem
{
    class FileHandler
    {
        public static void WriteToFile(string fpath, bool append, string line)
        {
            if (!File.Exists(fpath))
                return;

            using (StreamWriter writer = new StreamWriter(fpath, append))
            {
                writer.WriteLine(line);
            }
        }

        public static void WriteToFile(string fpath, bool append, List<string> lines)
        {
            if (!File.Exists(fpath))
                return;

            for (int i = 0; i < lines.Count; i++)
            {
                WriteToFile(fpath, append, lines[i]);
            }
        }
        
        public static void CreateFile(string fname, string fpath, bool overwrite = false)
        {
            if (!File.Exists(fpath + fname) || overwrite == true)
            {
                File.Create(fpath + fname);
            }
        }

        public static void CreateFile(string dpath, byte[] buffer)
        {
            File.WriteAllBytes(dpath, buffer);
        }

        public static void DeleteFile(string fpath)
        {
            if (File.Exists(fpath))
            {
                File.Delete(fpath);
            }
        }

        public static bool IsFileBiggerThanGB(string fpath, int amountOfGb)
        {
            FileInfo info = new FileInfo(fpath);
            int gb = (int)info.Length / 1024 / 1024 / 1024;

            if (gb > amountOfGb)
                return true;
            else
                return false;
        }

        public static byte[] ReadBytes(string fpath)
        {
            return File.ReadAllBytes(fpath);
        }

        public static string BufferToBase64(byte[] buffer)
        {
            return Convert.ToBase64String(buffer);
        }

        public static byte[] Base64ToBuffer(string basestr)
        {
            return Convert.FromBase64String(basestr);
        }

        public static void OpenFolderWin32(string fpath)
        {
            if (Directory.Exists(Path.GetDirectoryName(fpath)))
            {
                Process.Start(Path.GetDirectoryName(fpath));
            }
        }
    }
}
