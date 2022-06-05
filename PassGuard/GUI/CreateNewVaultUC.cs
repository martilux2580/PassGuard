using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SQLite;
using System.Transactions;
using System.Security.Cryptography;
using System.IO;
using System.Configuration;

namespace PassGuard.GUI
{
    public partial class CreateNewVaultUC : UserControl
    {
        public CreateNewVaultUC()
        {
            InitializeComponent();
            SelectVaultPathButton.Image = Image.FromFile(@"..\..\Images\FolderIcon.ico"); //Loads Image for the Settings Icon
            VaultPathTextbox.Text = Environment.GetFolderPath(Environment.SpecialFolder.Desktop); //Set default text to Desktop folder.
        }

        private void CreateNewVaultButton_MouseEnter(object sender, EventArgs e)
        {
            CreateNewVaultButton.Font = new Font("Microsoft Sans Serif", 11, FontStyle.Underline); //Underline the text when mouse is in the button
        }

        private void CreateNewVaultButton_MouseLeave(object sender, EventArgs e)
        {
            CreateNewVaultButton.Font = new Font("Microsoft Sans Serif", 11, FontStyle.Regular); //Regularise the text when mouse is not in the button
        }

        private void CreateNewVaultButton_Click(object sender, EventArgs e)
        {
            String errorMessages = ""; //All the error messages due to input, later print it to user in just one messagebox.

            //If any field is blank.
            if ((String.IsNullOrWhiteSpace(VaultEmailTextbox.Text)) || (String.IsNullOrWhiteSpace(VaultNameTextbox.Text)) || (String.IsNullOrWhiteSpace(VaultPassTextbox.Text)) || (String.IsNullOrWhiteSpace(ConfirmPassVaultTextbox.Text)) || (String.IsNullOrWhiteSpace(VaultPathTextbox.Text)))
            {
                errorMessages += "    - There cannot be fields left in blank.\n";
            }

            //Validate the format of email.
            try
            {
                var test = new System.Net.Mail.MailAddress(VaultEmailTextbox.Text);
            }
            catch (Exception ex)
            {
                errorMessages += "    - Invalid Email Format.\n";
            }

            bool validName = Check(VaultNameTextbox.Text, "Lower") || Check(VaultNameTextbox.Text, "Upper") || Check(VaultNameTextbox.Text, "Number"); //Name not composed of symbols.
            if (!validName) //Validate name of vault.
            {
                errorMessages += "    - The new vault´s name should be composed of letters or numbers.\n";
            }

            bool validPass = Check(VaultPassTextbox.Text, "Lower") && Check(VaultPassTextbox.Text, "Upper") && Check(VaultPassTextbox.Text, "Number") && Check(VaultPassTextbox.Text, "Symbol") && (VaultPassTextbox.Text.Length >= 12);
            if (!validPass) //Valid password
            {
                errorMessages += "    - The password must have upper and lower case letters, numbers, symbols and must have a minimum length of 12 characters.\n";
            }
            
            if (!String.Equals(VaultPassTextbox.Text, ConfirmPassVaultTextbox.Text)) //Valid Confirmation of Password.
            {
                errorMessages += "    - Confirmation Password does not match actual password.\n";
            }

            //Deal with path.
            List<String> checkEncryptedPath = VaultPathTextbox.Text.Split('\\').ToList();
            checkEncryptedPath[0] = checkEncryptedPath[0] + "\\";
            checkEncryptedPath.Add(VaultNameTextbox.Text + ".encrypted");
            if (File.Exists(Path.Combine(checkEncryptedPath.ToArray()))) //There is not already a vault in that location.
            {
                errorMessages += "    - In the specified path there is already created a Password Vault with the same file name.\n";
            }

            if (!String.IsNullOrEmpty(errorMessages)) //If any error...
            {
                MessageBox.Show(text: "The following errors have been found:\n\n" + errorMessages, caption: "Warning(s)");
            }
            else //No error in params, create vault.
            {
                using (TransactionScope tran = new TransactionScope()) //Just in case, atomic procedure....
                {
                    String path = VaultPathTextbox.Text + "\\" + VaultNameTextbox.Text + ".db3"; //Path for the vault.
                    SQLiteConnection.CreateFile(path); //Create 0-byte file that will be modeled when it is opened, if it already exists then it is substituted.
                    
                    SQLiteConnection m_dbConnection = new SQLiteConnection("Data Source = " + path);
                    m_dbConnection.Open(); //If first time, this models file as a vault, also opens a connection to it.
                    
                    //Create space for data.
                    string query = "CREATE TABLE Vault (Url TEXT, Name TEXT PRIMARY KEY NOT NULL UNIQUE, Username TEXT NOT NULL, SitePassword TEXT NOT NULL, Category TEXT, Notes TEXT);";
                    SQLiteCommand command = new SQLiteCommand(query, m_dbConnection); //Associate request with connection to vault.
                    command.ExecuteNonQuery(); //Execute request.
                    command.Dispose(); //Delete object so it is no longer using the file.

                    //CUIDADO QUE NINGUNO DE LOS CAMPOS EN LOS QUE SE INTRODUZCAN DATOS TENGAN COMILLAS O COSAS RARAS, QUE LUEGO ESO AL GUARDARLO DA PROBLEMA....
                    /*sql = "INSERT INTO Vault (Url, Name, Username, SitePassword, Notes) values ('xd.com', 'xd', 'xDD', 'elpepasos68', 'cat1', 'notastanlargascomoquiera');";
                    command = new SQLiteCommand(sql, m_dbConnection); //Associate request with connection to vault.
                    command.ExecuteNonQuery(); //Execute request.
                    command.Dispose(); //Delete object so it is no longer using the file.
                    */


                    //Indicates that creating the SQLiteDatabase went succesfully, so the database can be committed.
                    tran.Complete(); //Close and commit transaction.
                    tran.Dispose(); //Dispose transaction so it is no longer using the file.

                    m_dbConnection.Close(); //Close connection to vault.
                    m_dbConnection.Dispose();
                    m_dbConnection = null;

                }

                //Vault Encryption
                //Deal with paths for files.
                String pathforVault = VaultPathTextbox.Text + "\\" + VaultNameTextbox.Text + ".db3";
                List<String> saveVaultPath = pathforVault.Split('\\').ToList();
                saveVaultPath[0] = saveVaultPath[0] + "\\";

                List<String> saveEncryptedVaultPath = VaultPathTextbox.Text.Split('\\').ToList();
                saveEncryptedVaultPath[0] = saveEncryptedVaultPath[0] + "\\";
                saveEncryptedVaultPath.Add(VaultNameTextbox.Text + ".encrypted");

                //Encrypt New Vault
                //Generate random salt.
                Random rnd = new Random();
                byte[] salt = new byte[16];
                rnd.NextBytes(salt);
                string rndsalt = Convert.ToBase64String(salt);
                //Encrypt and delete previous file.
                Encrypt(key: getVaultKey(password: (VaultEmailTextbox.Text+VaultPassTextbox.Text), salt: Convert.FromBase64String(rndsalt)), Path.Combine(saveVaultPath.ToArray()), Path.Combine(saveEncryptedVaultPath.ToArray()));
                File.Delete(Path.Combine(saveVaultPath.ToArray()));

                //Save salt
                Configuration config = ConfigurationManager.OpenExeConfiguration(Application.ExecutablePath);
                config.AppSettings.Settings["SecurityKey"].Value = rndsalt; //Modify data in the config file for future executions.
                config.Save(ConfigurationSaveMode.Modified);

                //Inform user
                var data = "\tEmail: " + VaultEmailTextbox.Text + "\n\tVault Password: " + VaultPassTextbox.Text + "\n\tSecurity Key: " + rndsalt;
                var message = "Congrats! Your new Password Vault has been created successfully!\nThe information you must store and remember in order to load and access to your Password Vault is the following: \n\n" 
                    + data + "\n\nNotes: \n\tWithout any of those three values, your Password Vault and its content will be inacessible. \n\tBy clicking OK, those three values will be copied to the clipboard, please save them carefully." 
                    + "\n\tSecurity Key will be remembered by PassGuard. However, if another Password Vault is created its Security Key will be remembered by PassGuard and the previous key will be deleted, " 
                    + "so make sure you keep save and remember the email, password and Security Key of each Password Vault you create.";
                DialogResult dialog = MessageBox.Show(text: message, caption: "Success!", buttons: MessageBoxButtons.OK, icon: MessageBoxIcon.Information);
                if(dialog == DialogResult.OK)
                {
                    Clipboard.SetText(data.Replace("\t", ""));
                }

                //Leave everything as if the user wanted to create a new password vault...
                VaultNameTextbox.Text = VaultPassTextbox.Text = ConfirmPassVaultTextbox.Text = "";

            }

        }

        //Function to encrypt a src file into a dst file given a key with AES.
        private void Encrypt(byte[] key, String src, String dst)
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
        private byte[] getVaultKey(String password, byte[] salt)
        {
            Rfc2898DeriveBytes d1 = new Rfc2898DeriveBytes(password, salt, iterations: 100100);
            return d1.GetBytes(32); //256bit key.

        }

        //Function to check if a string has Lower, Upper, Number or Symbol characters, based on those modes.
        private bool Check(String str, String mode)
        {
            switch (mode)
            {
                case "Lower":
                    foreach(char c in str)
                    {
                        if(char.IsLower(c)) { return true; }
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

        private void SelectVaultPathButton_Click(object sender, EventArgs e)
        {
            String path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            FolderBrowserDialog fbd = new FolderBrowserDialog();

            // Show the FolderBrowserDialog.
            DialogResult result = fbd.ShowDialog();
            if (result == DialogResult.OK)
            {
                path = fbd.SelectedPath;
                VaultPathTextbox.Text = path;
            }
        }

    }

 }

