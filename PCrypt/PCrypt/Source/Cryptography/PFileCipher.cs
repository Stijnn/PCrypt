using System.Security.Cryptography;
using System.Text;

namespace PCrypt.Source.Cryptography
{
    using PCrypt.Source.Filesystem;
    using PCrypt.Source.Reporter;
    using System;
    using System.IO;
    using System.Windows;

    class PFileCipher
    {
        private readonly byte[] iv = Encoding.ASCII.GetBytes(@"bAe69cC91f5CxV3a");

        public PFileCipher()
        {
            
        }

        public void EncryptFile(string fpath, string password)
        {
            try
            {
                if (!File.Exists(fpath))
                    throw new Exception("This file does not exist");

                using (MemoryStream ms = new MemoryStream())
                {
                    using (ICryptoTransform icrp = CreateCipher(CryptMode.ENCRYPT,
                                                                Encoding.ASCII.GetBytes(PKeyGenerator.GenerateKey(password))))
                    {
                        using (CryptoStream cs = new CryptoStream(ms, icrp, CryptoStreamMode.Write))
                        {
                            using (FileStream fs = new FileStream(fpath, FileMode.Open, FileAccess.Read))
                            {
                                byte[] buffer = new byte[2048];
                                int bytesRead;
                                while ((bytesRead = fs.Read(buffer, 0, buffer.Length)) > 0)
                                {
                                    cs.Write(buffer, 0, bytesRead);
                                }
                                fs.Close();
                            }
                            cs.FlushFinalBlock();
                            cs.Clear();
                            cs.Close();
                        }
                    }

                    File.Delete(fpath);
                    File.WriteAllBytes(Path.GetDirectoryName(fpath) + "\\" + Path.GetFileName(fpath) + ".pcrypted", ms.ToArray());
                    ms.Close();
                }
            }
            catch (System.Exception ex)
            {
                SReporter.SetStatus("FAILED");
                MessageBox.Show(ex.Message);
            }
            finally
            {
                GC.Collect();
            }
        }

        public void DecryptFile(string fpath, string password)
        {
            try
            {
                if (!File.Exists(fpath))
                    throw new Exception("This file does not exist");

                using (MemoryStream ms = new MemoryStream())
                {
                    using (ICryptoTransform icrp = CreateCipher(CryptMode.DECRYPT,
                                                                Encoding.ASCII.GetBytes(PKeyGenerator.GenerateKey(password))))
                    {
                        using (CryptoStream cs = new CryptoStream(ms, icrp, CryptoStreamMode.Write))
                        {
                            using (FileStream fs = new FileStream(fpath, FileMode.Open, FileAccess.Read))
                            {
                                byte[] buffer = new byte[2048];
                                int bytesRead;
                                while ((bytesRead = fs.Read(buffer, 0, buffer.Length)) > 0)
                                {
                                    cs.Write(buffer, 0, bytesRead);
                                }
                                fs.Close();
                            }
                            cs.FlushFinalBlock();
                            cs.Clear();
                            cs.Close();
                        }
                    }

                    File.Delete(fpath);
                    File.WriteAllBytes(Path.GetDirectoryName(fpath) + "\\" + Path.GetFileNameWithoutExtension(fpath), ms.ToArray());
                    ms.Close();
                }
            }
            catch (System.Exception ex)
            {
                SReporter.SetStatus("FAILED");
                MessageBox.Show(ex.Message);
            }
            finally
            {
                GC.Collect();
            }
        }

        private ICryptoTransform CreateCipher(CryptMode mode, byte[] key)
        {
            AesCryptoServiceProvider provider = new AesCryptoServiceProvider()
            {
                BlockSize = 128,
                KeySize = 256,
                Key = key,
                IV = iv,
                Padding = PaddingMode.PKCS7,
                Mode = CipherMode.ECB,
            };

            ICryptoTransform icrp = mode == CryptMode.ENCRYPT ? provider.CreateEncryptor(provider.Key, provider.IV) : mode == CryptMode.DECRYPT ? provider.CreateDecryptor(provider.Key, provider.IV) : null;
            return icrp;
        }

        private enum CryptMode
        {
            ENCRYPT,
            DECRYPT
        }
    }
}
