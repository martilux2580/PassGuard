using iText.IO.Font;
using iText.IO.Image;
using iText.Kernel.Colors;
using iText.Kernel.Font;
using iText.Kernel.Geom;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Borders;
using iText.Layout.Element;
using iText.Layout.Properties;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;


namespace PassGuard.Core
{
    class Utils
    {
        internal string ComputeSHA1(string password)
        {
            if (String.IsNullOrEmpty(password))
            {
                return null;
            }
            else
            {
                SHA1 sha1 = new SHA1CryptoServiceProvider();
                byte[] passByte = Encoding.UTF8.GetBytes(password);//Encode Pass
                byte[] passHash = sha1.ComputeHash(passByte);//Compute SHA1

                //Convert Hash into readable string
                StringBuilder sb = new StringBuilder();
                foreach (byte b in passHash)
                {
                    sb.Append(b.ToString("x2"));
                }
                string hash = sb.ToString();//Compose bytes to string

                return hash;
            }
            
        }

        //Obtain all the hashes that start with headHash from an API of pwned passwords.
        internal async Task<string> getHashes(string headHash)
        {
            string instruction = "https://api.pwnedpasswords.com/range/";
            string url = instruction + headHash.ToUpper();
            HttpClient client = new HttpClient();
            try
            {
                string response = await client.GetStringAsync(url);
                return response;
            }
            catch (HttpRequestException)
            {
                return "";
            }
            
        }

        internal async Task<bool> CheckPwnage(string password)//Password pwned before? -> returns true, Password not pwned before? -> returns false
        {
            string hash = ComputeSHA1(password); //Compute SHA1
            string headhash = hash.Substring(0, 5); //Compute first part of hash in order to check hashes.
            string PwnedHashes = await getHashes(headhash);

            StringReader reader = new StringReader(PwnedHashes);
            string line, tailHash;
            while ((line = reader.ReadLine()) != null)//Read all pwned hashes
            {
                tailHash = line.Substring(0, 35);
                if (headhash.ToUpper() + tailHash.ToUpper() == hash.ToUpper()) //If match, pass has been pwned before.
                {
                    return true;
                }
            }
            return false;
        }

        //https://stackoverflow.com/questions/32932679/using-rngcryptoserviceprovider-to-generate-random-string
        //https://stackoverflow.com/questions/32932679/using-rngcryptoserviceprovider-to-generate-random-string/32932789#32932789
        internal string GenerateSecurePassword(int length, string validCharacters) //StackOverflow xD
        {
            StringBuilder res = new StringBuilder();
            using (RNGCryptoServiceProvider cryptoProvider = new RNGCryptoServiceProvider())
            {
                while (res.Length != length)
                {
                    byte[] oneByte = new byte[1];
                    cryptoProvider.GetBytes(oneByte);
                    char character = (char)oneByte[0];
                    if (validCharacters.Contains(character))
                    {
                        res.Append(character);
                    }
                }
            }

            return res.ToString();
        }

        //Function to check if a string has Lower, Upper, Number or Symbol characters, based on those modes.
        internal bool Check(String str, String mode)
        {
            switch (mode)
            {
                case "Lower":
                    foreach (char c in str)
                    {
                        if (char.IsLower(c)) { return true; }
                    }
                    return false;
                case "Upper":
                    foreach (char c in str)
                    {
                        if (char.IsUpper(c)) { return true; }
                    }
                    return false;
                case "Number":
                    foreach (char c in str)
                    {
                        if (char.IsDigit(c)) { return true; }
                    }
                    return false;
                case "Symbol":
                    String validSymbols = "!@#~$%&¬€/()=?¿+*[]{}ñÑ-_.:,;<>¡ªº";
                    foreach (char c in str)
                    {
                        if (validSymbols.Contains(c)) { return true; }
                    }
                    return false;
                default:
                    return false;
            }
        }

        //Function to encrypt a src file into a dst file given a key with AES.
        internal void Encrypt(byte[] key, String src, String dst)
        {
            // Encrypt the source file and write it to the destination file.
            using (var sourceStream = File.OpenRead(src))
            using (var destinationStream = File.Create(dst))
            using (var provider = new AesCryptoServiceProvider())
            {
                if (key != null)
                {
                    provider.Key = key;
                }

                using (var cryptoTransform = provider.CreateEncryptor())
                using (var cryptoStream = new CryptoStream(destinationStream, cryptoTransform, CryptoStreamMode.Write))
                {
                    destinationStream.Write(provider.IV, 0, provider.IV.Length);
                    sourceStream.CopyTo(cryptoStream);
                }
            }
        }

        //Function to derive a 256bit key (with PBKDF2) given a password and a salt.
        internal byte[] getVaultKey(String password, byte[] salt)
        {
            Rfc2898DeriveBytes d1 = new Rfc2898DeriveBytes(password, salt, iterations: 100100);
            return d1.GetBytes(32); //256bit key.

        }

        //Function to decrypt a src file into a dst file given a key with AES.
        internal void Decrypt(byte[] key, String src, String dst)
        {
            // Decrypt the source file and write it to the destination file.
            using (var sourceStream = File.OpenRead(src))
            using (var destinationStream = File.Create(dst))
            using (var provider = new AesCryptoServiceProvider())
            {
                var IV = new byte[provider.IV.Length];
                sourceStream.Read(IV, 0, IV.Length);
                using (var cryptoTransform = provider.CreateDecryptor(key, IV))
                using (var cryptoStream = new CryptoStream(sourceStream, cryptoTransform, CryptoStreamMode.Read))
                {
                    cryptoStream.CopyTo(destinationStream);
                }
            }
        }

        internal String EncryptText(byte[] key, String src)
        {
            try
            {
                byte[] encrypted;
                byte[] IV;

                using (var provider = new AesCryptoServiceProvider())
                {
                    provider.Key = key;

                    provider.GenerateIV();
                    IV = provider.IV;

                    provider.Mode = CipherMode.CBC;

                    // Create the streams used for encryption. 
                    using (var encryptor = provider.CreateEncryptor(provider.Key, provider.IV))
                    using (var msEncrypt = new MemoryStream())
                    {
                        using (var csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                        using (var swEncrypt = new StreamWriter(csEncrypt))
                        {
                            //Write all data to the stream.
                            swEncrypt.Write(src);
                        }
                        encrypted = msEncrypt.ToArray();
                    }

                }

                var combinedIvCt = new byte[IV.Length + encrypted.Length];
                Array.Copy(IV, 0, combinedIvCt, 0, IV.Length);
                Array.Copy(encrypted, 0, combinedIvCt, IV.Length, encrypted.Length);

                // Return the encrypted bytes from the memory stream. 
                return Convert.ToBase64String(combinedIvCt);
            }
            catch (Exception)
            {
                return "void";
            }
        }

        internal String DecryptText(byte[] key, String src)
        {
            try
            {
                // Declare the string used to hold 
                // the decrypted text. 
                string plaintext = null;
                byte[] cipherTextCombined = Convert.FromBase64String(src);

                // Create an Aes object 
                // with the specified key and IV. 
                using (var provider = new AesCryptoServiceProvider())
                {
                    provider.Key = key;

                    byte[] IV = new byte[provider.BlockSize / 8];
                    byte[] cipherText = new byte[cipherTextCombined.Length - IV.Length];

                    Array.Copy(cipherTextCombined, IV, IV.Length);
                    Array.Copy(cipherTextCombined, IV.Length, cipherText, 0, cipherText.Length);

                    provider.IV = IV;

                    provider.Mode = CipherMode.CBC;

                    // Create a decrytor to perform the stream transform.
                    ICryptoTransform decryptor = provider.CreateDecryptor(provider.Key, provider.IV);

                    // Create the streams used for decryption. 
                    using (var msDecrypt = new MemoryStream(cipherText))
                    using (var csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                    using (var srDecrypt = new StreamReader(csDecrypt))
                    {

                        // Read the decrypted bytes from the decrypting stream
                        // and place them in a string.
                        plaintext = srDecrypt.ReadToEnd();
                    }
                }

                return plaintext;
            }
            catch (Exception)
            {
                return src;
            }

        }

        internal String Base64ToString(String src)
        {
            var base64EncodedBytes = Convert.FromBase64String(src);
            return Encoding.Default.GetString(base64EncodedBytes);
        }

        internal void CreatePDF(List<String[]> results, String Vault, String Email, String sk)
        {
            var pdfLocation = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\VaultTable-" + DateTime.Now.ToString("yyyyMMdd-HHmmss") + ".pdf";
            if (!File.Exists(pdfLocation))
            {
                var file = File.Create(pdfLocation);
                file.Close();

                PdfWriter writer = new PdfWriter(pdfLocation);
                PdfDocument pdf = new PdfDocument(writer);
                Document doc = new Document(pdf);
                doc.SetMargins(8, 8, 8, 8);

                var title = new Paragraph("PassGuard: Vault Content").SetBold().SetFontSize(15);
                title.SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER);
                doc.Add(title);

                var intro = new Paragraph("Date: " + DateTime.Now.ToString("D", new CultureInfo("en-US")) + ":").SetFontSize(12); //CultureInfo.CurrentCulture.TextInfo.ToTitleCase(DateTime.Now.ToLongDateString())
                intro.SetTextAlignment(iText.Layout.Properties.TextAlignment.LEFT);
                intro.SetFixedLeading(14);
                intro.SetMarginBottom(0f);
                doc.Add(intro);

                var intro2 = new Paragraph("Vault Name: " + Vault + "\nVault Filename: " + Vault + ".encrypted" + "\nPassGuard Saved Email: " + Email + "\nPassGuard Saved Security Key (SK): " + sk).SetFontSize(12);
                intro2.SetPaddingLeft(40f);
                intro2.SetFixedLeading(14);
                intro2.SetTextAlignment(iText.Layout.Properties.TextAlignment.LEFT);
                doc.Add(intro2);

                var note = new Paragraph("Note: Saved Email and SK may not correspond to the Vault. Those values are the ones PassGuard had stored the day the backup was done.").SetFontSize(8);
                note.SetMarginTop(0f);
                note.SetTextAlignment(iText.Layout.Properties.TextAlignment.LEFT);
                note.SetMarginBottom(16f);
                doc.Add(note);

                Table content = new Table(numColumns: 6).UseAllAvailableWidth();
                content.SetWidth(UnitValue.CreatePercentValue(100));
                content.SetFixedLayout();
                content.SetBorderCollapse(iText.Layout.Properties.BorderCollapsePropertyValue.SEPARATE);
                content.SetBorderBottom(null);
                content.SetMarginBottom(0f);
                content.SetPaddingBottom(0f);

                content.AddCell("URL").SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER).SetBold().SetBorderTop(new SolidBorder(ColorConstants.BLACK, 0.5f)).SetBorderLeft(new SolidBorder(ColorConstants.BLACK, 0.5f)).SetBorderRight(new SolidBorder(ColorConstants.BLACK, 0.5f));
                content.AddCell("Name").SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER).SetBold().SetBorderTop(new SolidBorder(ColorConstants.BLACK, 0.5f)).SetBorderLeft(new SolidBorder(ColorConstants.BLACK, 0.5f)).SetBorderRight(new SolidBorder(ColorConstants.BLACK, 0.5f));
                content.AddCell("Site Username").SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER).SetBold().SetBorderTop(new SolidBorder(ColorConstants.BLACK, 0.5f)).SetBorderLeft(new SolidBorder(ColorConstants.BLACK, 0.5f)).SetBorderRight(new SolidBorder(ColorConstants.BLACK, 0.5f));
                content.AddCell("Site Password").SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER).SetBold().SetBorderTop(new SolidBorder(ColorConstants.BLACK, 0.5f)).SetBorderLeft(new SolidBorder(ColorConstants.BLACK, 0.5f)).SetBorderRight(new SolidBorder(ColorConstants.BLACK, 0.5f));
                content.AddCell("Category").SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER).SetBold().SetBorderTop(new SolidBorder(ColorConstants.BLACK, 0.5f)).SetBorderLeft(new SolidBorder(ColorConstants.BLACK, 0.5f)).SetBorderRight(new SolidBorder(ColorConstants.BLACK, 0.5f));
                content.AddCell("Notes").SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER).SetBold().SetBorderTop(new SolidBorder(ColorConstants.BLACK, 0.5f)).SetBorderLeft(new SolidBorder(ColorConstants.BLACK, 0.5f)).SetBorderRight(new SolidBorder(ColorConstants.BLACK, 0.5f));

                doc.Add(content);

                Table content2 = new Table(numColumns: 6).UseAllAvailableWidth();
                content2.SetWidth(UnitValue.CreatePercentValue(100));
                content2.SetFixedLayout();
                content2.SetMarginBottom(0.1f);

                for (int i = 0; i < results.Count; i++)
                {
                    for (int j = 0; j < 6; j++)
                    {
                        content2.AddCell(results[i][j]).SetFontSize(9);
                    }
                }

                doc.Add(content2);

                doc.Close();

                MessageBox.Show(text: "PDF with the content of the Vault was generated successfully in your Documents Folder :)", caption: "Success", buttons: MessageBoxButtons.OK, icon: MessageBoxIcon.Information);
            }
            else { MessageBox.Show(text: "There is already a file with the name of the PDF. Please try again later.", caption: "File with same name at path", buttons: MessageBoxButtons.OK, icon: MessageBoxIcon.Error); }
        }

        internal bool CreateBackup(String srcPath, String dstPath)
        {
            var tempSplit = srcPath.Split('\\');
            var fileName = tempSplit[tempSplit.Length - 1].Split('.');
            var nameOfBackup = "Backup" + char.ToUpper(fileName[0][0]) + fileName[0].Substring(1) + DateTime.Now.ToString("-yyyyMMdd-HHmmss") + "." + fileName[1];

            if (!File.Exists(dstPath + "\\" + nameOfBackup))
            {
                try
                {
                    File.Copy(sourceFileName: srcPath, destFileName: dstPath + "\\" + nameOfBackup);
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }

            }
            else
            {
                return false;
            }
        }

        //Infinite method
        internal void AutoBackupTime()
        {
            Core.Utils utils = new Core.Utils();

            while (true)
            {
                var mode = ConfigurationManager.AppSettings.Get("FrequencyAutoBackup");
                var pathVault = ConfigurationManager.AppSettings.Get("PathVaultForAutoBackup");
                var dstPath = ConfigurationManager.AppSettings.Get("dstBackupPathForSave");
                var lastDate = ConfigurationManager.AppSettings.Get("LastDateAutoBackup");
                var active = ConfigurationManager.AppSettings.Get("AutoBackupState");
                if (active == "true")
                {
                    switch (Int32.Parse(mode))
                    {
                        case 3:
                            if (DateTime.Now.Subtract(DateTime.Parse(lastDate)).TotalSeconds >= 15)
                            {
                                if (File.Exists(pathVault) && Directory.Exists(dstPath))
                                {
                                    if (utils.CreateBackup(pathVault, dstPath: dstPath))
                                    {
                                        Configuration config = ConfigurationManager.OpenExeConfiguration(Application.ExecutablePath);
                                        config.AppSettings.Settings["LastDateAutoBackup"].Value = DateTime.Now.ToString(); //Modify data in the config file for future executions.
                                        config.Save(ConfigurationSaveMode.Modified, true);
                                        ConfigurationManager.RefreshSection("appSettings");
                                        MessageBox.Show(text: "AutoBackup was created successfully.", caption: "Success", buttons: MessageBoxButtons.OK, icon: MessageBoxIcon.Information);
                                    }
                                    else
                                    {
                                        MessageBox.Show(text: "AutoBackup could not make a backup of the specified Vault, please try again later. \nThis message will be shown every 30 seconds until the issue is solved or AutoBackup is deactivated.", caption: "Error", buttons: MessageBoxButtons.OK, icon: MessageBoxIcon.Error);
                                        Thread.Sleep(30000);

                                    }
                                }
                                else
                                {
                                    MessageBox.Show(text: "AutoBackup could not make a backup of the specified Vault. Please review AutoBackup config and check all the paths and files exist. \nThis message will be shown everytime AutoBackup tries to make a backup until the issue is solved or AutoBackup is deactivated.", caption: "Error", buttons: MessageBoxButtons.OK, icon: MessageBoxIcon.Error);
                                    Thread.Sleep(60000);
                                }
                            }
                            break;
                        case 4:
                            if (DateTime.Now.Subtract(DateTime.Parse(lastDate)).TotalSeconds >= 30)
                            {
                                if (File.Exists(pathVault) && Directory.Exists(dstPath))
                                {
                                    if (utils.CreateBackup(pathVault, dstPath: dstPath))
                                    {
                                        Configuration config = ConfigurationManager.OpenExeConfiguration(Application.ExecutablePath);
                                        config.AppSettings.Settings["LastDateAutoBackup"].Value = DateTime.Now.ToString(); //Modify data in the config file for future executions.
                                        config.Save(ConfigurationSaveMode.Modified, true);
                                        ConfigurationManager.RefreshSection("appSettings");
                                        MessageBox.Show(text: "AutoBackup was created successfully.", caption: "Success", buttons: MessageBoxButtons.OK, icon: MessageBoxIcon.Information);
                                    }
                                    else
                                    {
                                        Configuration config = ConfigurationManager.OpenExeConfiguration(Application.ExecutablePath);
                                        config.AppSettings.Settings["LastDateAutoBackup"].Value = DateTime.Now.ToString(); //Modify data in the config file for future executions.
                                        config.Save(ConfigurationSaveMode.Modified, true);
                                        ConfigurationManager.RefreshSection("appSettings");
                                        MessageBox.Show(text: "AutoBackup could not make a backup of the specified Vault, please try again later. \nThis message will be shown every 30 seconds until the issue is solved or AutoBackup is deactivated.", caption: "Error", buttons: MessageBoxButtons.OK, icon: MessageBoxIcon.Error);
                                        Thread.Sleep(30000);

                                    }
                                }
                                else
                                {
                                    MessageBox.Show(text: "AutoBackup could not make a backup of the specified Vault. Please review AutoBackup config and check all the paths and files exist. \nThis message will be shown everytime AutoBackup tries to make a backup until the issue is solved or AutoBackup is deactivated.", caption: "Error", buttons: MessageBoxButtons.OK, icon: MessageBoxIcon.Error);
                                    Thread.Sleep(60000);
                                }
                            }
                            break;
                        case 5:
                            if (DateTime.Now.Subtract(DateTime.Parse(lastDate)).TotalSeconds >= 45)
                            {
                                if (File.Exists(pathVault) && Directory.Exists(dstPath))
                                {
                                    if (utils.CreateBackup(pathVault, dstPath: dstPath))
                                    {
                                        Configuration config = ConfigurationManager.OpenExeConfiguration(Application.ExecutablePath);
                                        config.AppSettings.Settings["LastDateAutoBackup"].Value = DateTime.Now.ToString(); //Modify data in the config file for future executions.
                                        config.Save(ConfigurationSaveMode.Modified, true);
                                        ConfigurationManager.RefreshSection("appSettings");
                                        MessageBox.Show(text: "AutoBackup was created successfully.", caption: "Success", buttons: MessageBoxButtons.OK, icon: MessageBoxIcon.Information);
                                    }
                                    else
                                    {
                                        MessageBox.Show(text: "AutoBackup could not make a backup of the specified Vault, please try again later. \nThis message will be shown every 30 seconds until the issue is solved or AutoBackup is deactivated.", caption: "Error", buttons: MessageBoxButtons.OK, icon: MessageBoxIcon.Error);
                                        Thread.Sleep(30000);

                                    }
                                }
                                else
                                {
                                    MessageBox.Show(text: "AutoBackup could not make a backup of the specified Vault. Please review AutoBackup config and check all the paths and files exist. \nThis message will be shown everytime AutoBackup tries to make a backup until the issue is solved or AutoBackup is deactivated.", caption: "Error", buttons: MessageBoxButtons.OK, icon: MessageBoxIcon.Error);
                                    Thread.Sleep(60000);
                                }
                            }
                            break;
                        default:
                            break;
                    }
                }
                else if (active == "false")
                {
                    break;
                }
            }



        }

    }
}
