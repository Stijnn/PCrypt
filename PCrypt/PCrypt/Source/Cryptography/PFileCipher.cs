using System.Security.Cryptography;
using System.Text;

namespace PCrypt.Source.Cryptography
{
    using PCrypt.Source.Filesystem;
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
                byte[] buffer = FileHandler.ReadBytes(fpath);
                string basestr = FileHandler.BufferToBase64(buffer);
                byte[] fcontent = Encoding.ASCII.GetBytes(basestr);

                using (ICryptoTransform icrp = CreateCipher(CryptMode.ENCRYPT, 
                                                            Encoding.ASCII.GetBytes(PShaGenerator.GenerateKey(password))))
                {
                    byte[] encrypted = icrp.TransformFinalBlock(fcontent, 0, fcontent.Length);
                    FileHandler.CreatePCryptFile(fpath, encrypted);
                    FileHandler.DeleteFile(fpath);
                }
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void DecryptFile(string fpath, string password)
        {
            try
            {
                byte[] encrypted;
                string ext = string.Empty;

                using (StreamReader reader = new StreamReader(fpath))
                {
                    encrypted = FileHandler.Base64ToBuffer(reader.ReadLine());
                    ext = reader.ReadLine();
                }

                if (encrypted == null || string.IsNullOrWhiteSpace(ext))
                    throw new System.Exception("Invalid File");

                using (ICryptoTransform icrp = CreateCipher(CryptMode.DECRYPT, 
                                                            Encoding.ASCII.GetBytes(PShaGenerator.GenerateKey(password))))
                {
                    byte[] decrypted = icrp.TransformFinalBlock(encrypted, 0, encrypted.Length);
                    string basestr = Encoding.ASCII.GetString(decrypted);
                    byte[] bytes = FileHandler.Base64ToBuffer(basestr);

                    FileHandler.CreateFile(Path.GetFileNameWithoutExtension(fpath) + ext, bytes);
                    FileHandler.DeleteFile(fpath);
                }
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
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
