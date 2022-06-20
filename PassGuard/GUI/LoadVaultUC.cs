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
    public partial class LoadVaultUC : UserControl
    {
        public LoadVaultUC()
        {
            InitializeComponent();
            SelectVaultPathButton.Image = Image.FromFile(@"..\..\Images\FolderIcon.ico"); //Loads Image for the Settings Icon

        }

        private void LoadSavedSKButton_Click(object sender, EventArgs e)
        {
            SecurityKeyTextbox.Text = ConfigurationManager.AppSettings.Get("SecurityKey"); //Modify data in the config file for future executions.
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
            catch (Exception ex)
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
                saveEncryptedVaultPath[0] = saveEncryptedVaultPath[0] + "\\";

                //Calculate key to decrypt vault
                var key = utils.getVaultKey(password: (VaultEmailTextbox.Text + VaultPassTextbox.Text), Convert.FromBase64String(SecurityKeyTextbox.Text));

                //Show all the contents of the vault (UserControl).
                GUI.VaultContentUC vc = new GUI.VaultContentUC(Path.Combine(saveEncryptedVaultPath), VaultEmailTextbox.Text, VaultPassTextbox.Text, key); //Put the main panel visible.
                var ContentPanel = this.Parent;
                this.Parent.Controls.Clear(); //this.Parent.Name; //contentpanel
                ContentPanel.Controls.Add(vc);
                vc.Visible = true;
                

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
                OpenFileDialog ofd = new OpenFileDialog();
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
            VaultEmailTextbox.Text = ConfigurationManager.AppSettings.Get("Email"); //Modify data in the config file for future executions.
        }

        private void LoadSavedEmailButton_MouseEnter(object sender, EventArgs e)
        {
            LoadSavedEmailButton.Font = new Font("Microsoft Sans Serif", 9, FontStyle.Underline); //Underline the text when mouse is in the button
        }

        private void LoadSavedEmailButton_MouseLeave(object sender, EventArgs e)
        {
            LoadSavedEmailButton.Font = new Font("Microsoft Sans Serif", 9, FontStyle.Regular); //Underline the text when mouse is in the button
        }
    }
}
