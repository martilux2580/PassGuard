using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using System.Windows.Forms;

namespace PassGuard.GUI
{
    //UC Component to obtain the credentials to login to a selected Vault.
    public partial class LoadVaultUC : UserControl
    {
        private bool settings;

        public LoadVaultUC(bool setts)
        {
            InitializeComponent();
            settings = setts;
            try
            {
                SelectVaultPathButton.Image = Image.FromFile(@".\Images\FolderIcon.ico"); //Loads Image for the Settings Icon
                if (setts) { LoadVaultButton.Text = "Export Vault as PDF"; }
            }
            catch (Exception)
            {
                MessageBox.Show(text: "PassGuard could not load some images.", caption: "Images not found", icon: MessageBoxIcon.Error, buttons: MessageBoxButtons.OK);
            }

        }

        private void LoadSavedSKButton_Click(object sender, EventArgs e)
        {
            try
            {
                SecurityKeyTextbox.Text = ConfigurationManager.AppSettings["SecurityKey"]; //Get data in the config file for future executions.
            }
            catch (Exception)
            {
                MessageBox.Show(text: "PassGuard could not access config file, this feature can´t be set up.", caption: "App Config File not found", icon: MessageBoxIcon.Error, buttons: MessageBoxButtons.OK);
            }
        }

        private void LoadVaultButton_Click(object sender, EventArgs e)
        {
            Core.Utils utils = new Core.Utils();
            String errorMessages = ""; //Store all error messages...

            //If any field is blank.
            if ((String.IsNullOrWhiteSpace(VaultEmailTextbox.Text)) || (String.IsNullOrWhiteSpace(VaultPassTextbox.Text)) || (String.IsNullOrWhiteSpace(SecurityKeyTextbox.Text)) || (String.IsNullOrWhiteSpace(VaultPathTextbox.Text)))
            {
                errorMessages += "    - There cannot be fields left in blank.\n";
            }
            //Validate the format of email.
            try
            {
                var test = new System.Net.Mail.MailAddress(VaultEmailTextbox.Text);
            }
            catch (Exception)
            {
                errorMessages += "    - Invalid Email Format.\n";
            }

            bool validPass = utils.Check(VaultPassTextbox.Text, "Lower") && utils.Check(VaultPassTextbox.Text, "Upper") && utils.Check(VaultPassTextbox.Text, "Number") && utils.Check(VaultPassTextbox.Text, "Symbol") && (VaultPassTextbox.Text.Length >= 12);
            if (!validPass) //Valid password
            {
                errorMessages += "    - The password must have upper and lower case letters, numbers, symbols and must have a minimum length of 12 characters.\n";
            }

            if (!String.IsNullOrEmpty(errorMessages)) //If any error...
            {
                MessageBox.Show(text: "The following errors have been found:\n\n" + errorMessages, caption: "Warning(s)", icon: MessageBoxIcon.Warning, buttons: MessageBoxButtons.OK);
            }
            else //No error in params, create vault.
            {
                //Deal with paths for files.
                String pathforEncryptedVault = VaultPathTextbox.Text;
                String[] saveEncryptedVaultPath = pathforEncryptedVault.Split('\\');
                saveEncryptedVaultPath[0] = saveEncryptedVaultPath[0] + "\\"; //Path of the encrypted vault in a list.
                String[] lastValue = saveEncryptedVaultPath[saveEncryptedVaultPath.Length - 1].Split('.'); //[filename, filextension]
                lastValue[lastValue.Length - 1] = "db3"; //FileExtension

                var encVault = Path.Combine(saveEncryptedVaultPath); //Set path for encrypted vault.
                var decVault = (Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\" + (lastValue[0] + "." + lastValue[1])); //Set path for decrypted vault.

                if (settings) //Loading vault from settings button, so what we want is to export this loaded vault as PDF.
                {
                    try
                    {
                        //Calculate keys to decrypt vault.
                        var vKey = utils.GetVaultKey(password: (VaultEmailTextbox.Text + VaultPassTextbox.Text), Convert.FromBase64String(SecurityKeyTextbox.Text));
                        var keyVStr = utils.Base64ToString(Convert.ToBase64String(vKey));
                        var skStr = utils.Base64ToString(SecurityKeyTextbox.Text);
                        var cKey = utils.GetVaultKey(password: (keyVStr + (VaultEmailTextbox.Text + VaultPassTextbox.Text)), salt: Encoding.Default.GetBytes(skStr + keyVStr));

                        //Obtain all its decrypted elements.
                        utils.Decrypt(key: vKey, src: encVault, dst: decVault);

                        List<String[]> fullResults = new List<String[]>();
                        using (TransactionScope tran = new TransactionScope()) //Just in case, atomic procedure....
                        using (SQLiteConnection m_dbConnection = new SQLiteConnection("Data Source = " + decVault))
                        using (SQLiteCommand commandExec = new SQLiteCommand("SELECT * FROM Vault;", m_dbConnection)) //Associate request with connection to vault.)
                        {
                            m_dbConnection.Open(); //If first time, this models file as a vault, also opens a connection to it.
                            commandExec.ExecuteNonQuery(); //Execute request.

                            using (SQLiteDataReader reader = commandExec.ExecuteReader())//Object Reader.
                            {
                                while (reader.Read()) //Reads each row.
                                {
                                    fullResults.Add(new string[6] { reader.GetString(0), reader.GetString(1), reader.GetString(2), reader.GetString(3), reader.GetString(4), reader.GetString(5) });
                                }
                            }

                            commandExec.Dispose(); //Delete object so it is no longer using the file.

                            //Indicates that creating the SQLiteDatabase went succesfully, so the database can be committed.
                            tran.Complete(); //Close and commit transaction.
                            tran.Dispose(); //Dispose transaction so it is no longer using the file.

                            m_dbConnection.Close(); //Close connection to vault.
                            m_dbConnection.Dispose();

                        }

                        //Decrypt data.
                        foreach (String[] row in fullResults)
                        {
                            row[0] = utils.DecryptText(key: cKey, src: row[0]);
                            row[1] = utils.DecryptText(key: cKey, src: row[1]);
                            row[2] = utils.DecryptText(key: cKey, src: row[2]);
                            row[3] = utils.DecryptText(key: cKey, src: row[3]);
                            row[4] = utils.DecryptText(key: cKey, src: row[4]);
                            row[5] = utils.DecryptText(key: cKey, src: row[5]);
                        }

                        utils.CreatePDF(fullResults, lastValue[0], ConfigurationManager.AppSettings.Get("Email"), ConfigurationManager.AppSettings.Get("SecurityKey"));

                        utils.Encrypt(vKey, (Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\" + (lastValue[0] + "." + lastValue[1])), Path.Combine(saveEncryptedVaultPath));
                        File.Delete(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\" + (lastValue[0] + "." + lastValue[1]));

                        VaultEmailTextbox.Text = "";
                        VaultPassTextbox.Text = "";
                        VaultPathTextbox.Text = "";
                        SecurityKeyTextbox.Text = "";

                    }
                    catch (Exception ex)
                    {
                        if ((ex is FormatException) || (ex is System.Security.Cryptography.CryptographicException))
                        {
                            MessageBox.Show(text: "PassGuard could not unlock your Vault. Check the input credentials.", caption: "Not correct credentials.", icon: MessageBoxIcon.Error, buttons: MessageBoxButtons.OK);
                        }
                        else
                        {
                            MessageBox.Show(text: "PassGuard could not unlock or open the Vault. Check the inserted credentials, verify the file is not corrupted and try again later.", caption: "Error", icon: MessageBoxIcon.Error, buttons: MessageBoxButtons.OK);
                        }
                    }
                    finally
                    {
                        if (File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\" + (lastValue[0] + ".db3")))
                        {
                            File.Delete(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\" + (lastValue[0] + ".db3"));
                        }
                    }
                }
                else //What we want to do is to is to show the table with the data.
                {
                    try
                    {
                        //Calculate key to decrypt vault
                        var key = utils.GetVaultKey(password: (VaultEmailTextbox.Text + VaultPassTextbox.Text), Convert.FromBase64String(SecurityKeyTextbox.Text));

                        //Show all the contents of the vault (UserControl).
                        GUI.VaultContentUC vc = new GUI.VaultContentUC(Path.Combine(saveEncryptedVaultPath), VaultEmailTextbox.Text, VaultPassTextbox.Text, key, SecurityKeyTextbox.Text); //Put the main panel visible.
                        var ContentPanel = this.Parent;
                        this.Parent.Controls.Clear(); //this.Parent.Name = ContentPanel
                        ContentPanel.Controls.Add(vc);
                        vc.Visible = true;
                    }
                    catch (Exception ex)
                    {
                        if ((ex is FormatException) || (ex is System.Security.Cryptography.CryptographicException))
                        {
                            MessageBox.Show(text: "PassGuard could not unlock your Vault. Check the input credentials.", caption: "Not correct credentials.", icon: MessageBoxIcon.Error, buttons: MessageBoxButtons.OK);
                        }
                        else
                        {
                            MessageBox.Show(text: "PassGuard could not unlock or open the Vault. Check the inserted credentials, verify the file is not corrupted and try again later.", caption: "Error", icon: MessageBoxIcon.Error, buttons: MessageBoxButtons.OK);
                        }
                    }
                    finally
                    {
                        if (File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\" + (lastValue[0] + ".db3")))
                        {
                            File.Delete(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\" + (lastValue[0] + ".db3"));
                        }
                    }
                }

            }
        }

        private void SelectVaultPathButton_Click(object sender, EventArgs e)
        {
            //Select and Save filepath and extension.
            string filepath = "";
            string ext = ""; //File extension
            bool cancelPathSearch = false;
            while (ext != ".encrypted" && !cancelPathSearch)
            {
                OpenFileDialog ofd = new OpenFileDialog(); //File selector
                ofd.Filter = "PassGuard Vaults|*.encrypted"; //Type of file we are looking for...
                
                var result = ofd.ShowDialog();
                filepath = ofd.FileName;
                ext = Path.GetExtension(filepath).ToLower();

                if (result == DialogResult.Cancel)
                {
                    cancelPathSearch = true; //Stop loop, as user hit cancel button.
                }
                else if (ext!=".encrypted") //If user specified file with diff extension...
                {
                    MessageBox.Show(text: "Selected file must have .encrypted extension.", caption: "Wrong File", buttons: MessageBoxButtons.OK, icon: MessageBoxIcon.Error);
                }
            }
            VaultPathTextbox.Text = filepath;
        }

        private void LoadVaultButton_MouseEnter(object sender, EventArgs e)
        {
            LoadVaultButton.Font = new Font("Microsoft Sans Serif", 11, FontStyle.Underline); //Underline the text when mouse is in the button
        }

        private void LoadVaultButton_MouseLeave(object sender, EventArgs e)
        {
            LoadVaultButton.Font = new Font("Microsoft Sans Serif", 11, FontStyle.Regular); //Underline the text when mouse is in the button
        }

        private void LoadSavedSKButton_MouseEnter(object sender, EventArgs e)
        {
            LoadSavedSKButton.Font = new Font("Microsoft Sans Serif", 9, FontStyle.Underline); //Underline the text when mouse is in the button
        }

        private void LoadSavedSKButton_MouseLeave(object sender, EventArgs e)
        {
            LoadSavedSKButton.Font = new Font("Microsoft Sans Serif", 9, FontStyle.Regular); //Underline the text when mouse is in the button
        }

        private void LoadSavedEmailButton_Click(object sender, EventArgs e)
        {
            VaultEmailTextbox.Text = ConfigurationManager.AppSettings["Email"]; //Modify data in the config file for future executions.
        }

        private void LoadSavedEmailButton_MouseEnter(object sender, EventArgs e)
        {
            LoadSavedEmailButton.Font = new Font("Microsoft Sans Serif", 9, FontStyle.Underline); //Underline the text when mouse is in the button
        }

        private void LoadSavedEmailButton_MouseLeave(object sender, EventArgs e)
        {
            LoadSavedEmailButton.Font = new Font("Microsoft Sans Serif", 9, FontStyle.Regular); //Underline the text when mouse is in the button
        }

        private void SaveEmailButton_Click(object sender, EventArgs e)
        {
            try
            {
                Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                config.AppSettings.Settings["Email"].Value = VaultEmailTextbox.Text; //Modify data in the config file for future executions.
                config.Save(ConfigurationSaveMode.Modified, true);
                ConfigurationManager.RefreshSection("appSettings"); //If not, changes wont be visible for the rest of the program.
            }
            catch (Exception)
            {
                MessageBox.Show(text: "PassGuard could not access config file, this feature can´t be set up.", caption: "App Config File not found", icon: MessageBoxIcon.Error, buttons: MessageBoxButtons.OK);
            }
        }

        private void SaveSKButton_Click(object sender, EventArgs e)
        {
            try
            {
                Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                config.AppSettings.Settings["SecurityKey"].Value = SecurityKeyTextbox.Text; //Modify data in the config file for future executions.
                config.Save(ConfigurationSaveMode.Modified, true);
                ConfigurationManager.RefreshSection("appSettings"); //If not, changes wont be visible for the rest of the program.
            }
            catch (Exception)
            {
                MessageBox.Show(text: "PassGuard could not access config file, this feature can´t be set up.", caption: "App Config File not found", icon: MessageBoxIcon.Error, buttons: MessageBoxButtons.OK);
            }
        }
    }
}
