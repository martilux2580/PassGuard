using Microsoft.Office.Interop.Word;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Word = Microsoft.Office.Interop.Word;

namespace PassGuard.Core
{
    class Utils
    {
        internal string ComputeSHA1(string password)
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

        //Obtain all the hashes that start with headHash from an API of pwned passwords.
        internal async Task<string> getHashes(string headHash)
        {
            string instruction = "https://api.pwnedpasswords.com/range/";
            string url = instruction + headHash.ToUpper();
            HttpClient client = new HttpClient();
            string response = await client.GetStringAsync(url);

            return response;
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

        internal String DecryptText(byte[] key, String src)
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

        internal String Convert64(String src)
        {
            var plainTextBytes = Encoding.Default.GetBytes(src);
            return Convert.ToBase64String(plainTextBytes);
        }

        internal String Base64ToString(String src)
        {
            var base64EncodedBytes = Convert.FromBase64String(src);
            return Encoding.Default.GetString(base64EncodedBytes);
        }

        internal String JoinBase64(String src1, String src2)
        {
            var array1 = Convert.FromBase64String(src1);
            var array2 = Convert.FromBase64String(src2);
            var comb = CombineByteArray(array1, array2);
            var data = Encoding.Default.GetString(comb);
            return data;
        }

        internal byte[] CombineByteArray(byte[] first, byte[] second)
        {
            return first.Concat(second).ToArray();
        }


        internal void CreateDOCX(List<String[]> results, String Vault, String Email, String sk)
        {
            object docxLocation = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\TempVaultTable.docx";
            using (var tempDocxFile = File.Create(docxLocation.ToString()))
            {
                tempDocxFile.Close();
                tempDocxFile.Dispose();
                object oMissing = Missing.Value;
                object oEndOfDoc = "\\endofdoc";
                Word.Application app = new Word.Application(); //objword
                Word.Document docx = app.Documents.Open(ref docxLocation);
                docx.Activate();
                docx.PageSetup.TopMargin = (float)5;
                docx.PageSetup.BottomMargin = (float)5;
                docx.PageSetup.LeftMargin = (float)5;
                docx.PageSetup.RightMargin = (float)5;
                

                var p2 = docx.Paragraphs.Add(System.Reflection.Missing.Value);
                p2.Range.Font.Name = "Calibri";
                p2.Range.Font.Size = 15;
                p2.Range.Text = "PassGuard: Vault Content";
                p2.Range.Font.Bold = 1;
                p2.Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;
                p2.Range.ParagraphFormat.SpaceAfter = 0;
                p2.Range.InsertParagraphAfter();
                p2.Range.Bold = 0;

                var p3 = docx.Paragraphs.Add(System.Reflection.Missing.Value);
                p3.Range.Font.Name = "Arial";
                p3.Range.Font.Size = 12;
                p3.Range.Text = "\vDate: " + CultureInfo.CurrentCulture.TextInfo.ToTitleCase(DateTime.Now.ToLongDateString()) + "\v\tVault Name: " + Vault + "\v\tPassGuard Saved Email: " + Email + "\v\tPassGuard Saved Security Key (SK): " + sk;
                p3.Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft;
                p3.Range.ParagraphFormat.SpaceAfter = 0;
                p3.Range.InsertParagraphAfter();


                var p4 = docx.Paragraphs.Add(System.Reflection.Missing.Value);
                p4.Range.Font.Name = "Arial";
                p4.Range.Font.Size = 8;
                p4.Range.Text = "\vNote: Saved Email and SK may not correspond to the Vault. Those values are the ones PassGuard had stored on the day the backup was done.\v";
                p4.Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft;
                p4.Range.InsertParagraphAfter();


                Word.Range wrdRng = docx.Bookmarks.get_Item(ref oEndOfDoc).Range;
                Table objTable = docx.Tables.Add(Range: wrdRng, NumRows: results.Count+1, NumColumns: 6);
                objTable.Borders.InsideLineStyle = Word.WdLineStyle.wdLineStyleSingle;
                objTable.Borders.OutsideLineStyle = Word.WdLineStyle.wdLineStyleSingle;
                objTable.Rows.First.Range.Bold = 1;


                objTable.Cell(1, 1).Range.Font.Underline = WdUnderline.wdUnderlineSingle;
                objTable.Cell(1, 2).Range.Font.Underline = WdUnderline.wdUnderlineSingle;
                objTable.Cell(1, 3).Range.Font.Underline = WdUnderline.wdUnderlineSingle;
                objTable.Cell(1, 4).Range.Font.Underline = WdUnderline.wdUnderlineSingle;
                objTable.Cell(1, 5).Range.Font.Underline = WdUnderline.wdUnderlineSingle;
                objTable.Cell(1, 6).Range.Font.Underline = WdUnderline.wdUnderlineSingle;
                objTable.Cell(1, 1).Range.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphCenter;
                objTable.Cell(1, 2).Range.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphCenter;
                objTable.Cell(1, 3).Range.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphCenter;
                objTable.Cell(1, 4).Range.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphCenter;
                objTable.Cell(1, 5).Range.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphCenter;
                objTable.Cell(1, 6).Range.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphCenter;


                objTable.Cell(1, 1).Range.Text = "URL";
                objTable.Cell(1, 2).Range.Text = "Name";
                objTable.Cell(1, 3).Range.Text = "Site Username";
                objTable.Cell(1, 4).Range.Text = "Site Password";
                objTable.Cell(1, 5).Range.Text = "Category";
                objTable.Cell(1, 6).Range.Text = "Notes";

                
                for (int i = 2; i < results.Count+2; i++)
                {
                    objTable.Cell(i, 2).Range.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphCenter;
                    objTable.Cell(i, 3).Range.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphCenter;
                    objTable.Cell(i, 4).Range.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphCenter;
                    objTable.Cell(i, 5).Range.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphCenter;

                    objTable.Cell(i, 1).Range.Text = results[i - 2][0];
                    objTable.Cell(i, 2).Range.Text = results[i - 2][1];
                    objTable.Cell(i, 3).Range.Text = results[i - 2][2];
                    objTable.Cell(i, 4).Range.Text = results[i - 2][3];
                    objTable.Cell(i, 5).Range.Text = results[i - 2][4];
                    objTable.Cell(i, 6).Range.Text = results[i - 2][5];

                    objTable.Cell(i, 1).Range.Paragraphs.LineSpacing = (float)10;
                    objTable.Cell(i, 2).Range.Paragraphs.LineSpacing = (float)10;
                    objTable.Cell(i, 3).Range.Paragraphs.LineSpacing = (float)10;
                    objTable.Cell(i, 4).Range.Paragraphs.LineSpacing = (float)10;
                    objTable.Cell(i, 5).Range.Paragraphs.LineSpacing = (float)10;
                    objTable.Cell(i, 6).Range.Paragraphs.LineSpacing = (float)10;

                    objTable.Cell(i, 1).Range.Font.Size = 9;
                    objTable.Cell(i, 2).Range.Font.Size = 9;
                    objTable.Cell(i, 3).Range.Font.Size = 9;
                    objTable.Cell(i, 4).Range.Font.Size = 9;
                    objTable.Cell(i, 5).Range.Font.Size = 9;
                    objTable.Cell(i, 6).Range.Font.Size = 9;
                }

                var pdfLocation = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\VaultTable.pdf";
                docx.SaveAs2(pdfLocation, Word.WdSaveFormat.wdFormatPDF, oMissing, oMissing, oMissing, oMissing, oMissing, oMissing, oMissing, oMissing, oMissing, oMissing);
                docx.Close(WdSaveOptions.wdDoNotSaveChanges, WdOriginalFormat.wdOriginalDocumentFormat, false);
                app.Quit(WdSaveOptions.wdDoNotSaveChanges);
                MessageBox.Show("done");
            }
            
        }


        internal void convertDOCXtoPDF()
        {

            object misValue = System.Reflection.Missing.Value;
            String PATH_APP_PDF = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\Table.pdf";//@"c:\..\MY_WORD_DOCUMENT.pdf";

            var WORD = new Word.Application();

            Word.Document doc = WORD.Documents.Open(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\Table.docx");
            doc.Activate();

            doc.SaveAs2(@PATH_APP_PDF, Word.WdSaveFormat.wdFormatPDF, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue);

            doc.Close();
            WORD.Quit();


            releaseObject(doc);
            releaseObject(WORD);

        }

        internal void releaseObject(object obj)
        {
            try
            {
                System.Runtime.InteropServices.Marshal.ReleaseComObject(obj);
                obj = null;
            }
            catch (Exception ex)
            {
                //TODO
            }
            finally
            {
                GC.Collect();
            }
        }

    }
}
