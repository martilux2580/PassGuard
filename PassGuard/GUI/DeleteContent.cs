using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using System.Windows.Forms;

namespace PassGuard.GUI
{
    public partial class DeleteContent : Form
    {
        private String name { get; set; }
        private List<String> namesInDB;
        private readonly byte[] Key;
        private readonly String decPath;
        private bool deletedSuccess;
        private bool deletedAllSuccess;
        private String nameToBeDeleted;
        private readonly Dictionary<String, String> map; //No duplicate keys, (enc, dec)

        public bool getDeletedSuccess()
        {
            return deletedSuccess;
        }
        public bool getDeletedAllSuccess()
        {
            return deletedAllSuccess;
        }
        public String getNameToBeDeleted()
        {
            return nameToBeDeleted;
        }

        public DeleteContent(List<String> names, byte[] key, String decpath)
        {
            InitializeComponent();
            Core.Utils utils = new Core.Utils();
            decPath = decpath;
            Key = key;

            namesInDB = names;
            map = new Dictionary<string, string>();
            foreach (String enc in namesInDB)
            {
                map.Add(enc, utils.DecryptText(key: Key, src: enc));
            }

            NameCombobox.Items.Add("");
            foreach (String name in namesInDB)
            {
                NameCombobox.Items.Add(utils.DecryptText(key: Key, src: name));
            }
            deletedSuccess = false;
            deletedAllSuccess = false;

            this.Icon = new Icon(@"..\..\Images\LogoIcon64123.ico"); //Loads Icon from Image folder.
        }

        private void EnableDeleteAllCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            if(EnableDeleteAllCheckbox.Checked == true)
            {
                //Null textboxes
                URLTextbox.Text = null;
                NameTextbox.Text = null;
                UsernameTextbox.Text = null;
                PasswordTextbox.Text = null;
                CategoryTextbox.Text = null;
                NotesTextbox.Text = null;

                TitleLabel.Text = "If button is clicked, all Vault contents will be deleted.";
                NameCombobox.Enabled = false;
                NameCombobox.ResetText();

                //Enable elements
                DeleteAllButton.Enabled = true;
                DeleteButton.Enabled = false;
            }
            else if (EnableDeleteAllCheckbox.Checked == false)
            {
                TitleLabel.Text = "Select the name of the password you want to delete: ";

                //Enable elements
                DeleteAllButton.Enabled = false;
                DeleteButton.Enabled = true;
                NameCombobox.Enabled = true;
            }
        }

        private void DeleteButton_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(NameCombobox.Text) || String.IsNullOrWhiteSpace(NameCombobox.Text))
            {
                MessageBox.Show(text: "An element must be selected in order to be deleted.", caption: "No element selected", icon: MessageBoxIcon.Warning, buttons: MessageBoxButtons.OK);
            }
            else
            {
                DialogResult dialog = MessageBox.Show(text: "Are you sure you want to delete the element with name '" + NameTextbox.Text + "' from your Vault?\n\nNote: This action cannot be undone.", caption: "Confirm the deletion of the element", icon: MessageBoxIcon.Question, buttons: MessageBoxButtons.YesNo);
                if (dialog == DialogResult.Yes)
                {
                    nameToBeDeleted = map.FirstOrDefault(x => x.Value == NameCombobox.Text).Key;
                    deletedSuccess = true;
                    this.Close();

                }
            
            }
        }

        private void DeleteAllButton_Click(object sender, EventArgs e)
        {
            DialogResult dialog = MessageBox.Show(text: "Are you sure you want to delete all the elements in your Vault? After this action, your Vault will be completely empty. \n\nNote: This action cannot be undone.", caption: "Confirm the deletion of all the elements", icon: MessageBoxIcon.Question, buttons: MessageBoxButtons.YesNo);
            if (dialog == DialogResult.Yes)
            {
                deletedAllSuccess = true;
                this.Close();

            }
        }

        private void NameCombobox_SelectedIndexChanged(object sender, EventArgs e)
        {
            Core.Utils utils = new Core.Utils();

            if ((!String.IsNullOrWhiteSpace(NameCombobox.Text)) && (!String.IsNullOrEmpty(NameCombobox.Text)))
            {
                List<String[]> fullResults = new List<String[]>();
                using (TransactionScope tran = new TransactionScope()) //Just in case, atomic procedure....
                using (SQLiteConnection m_dbConnection = new SQLiteConnection("Data Source = " + decPath))
                {
                    var keyToSearch = map.FirstOrDefault(x => x.Value == NameCombobox.Text).Key;
                    var sql = "SELECT * FROM Vault WHERE Name = '" + keyToSearch + "';";
                    using (SQLiteCommand commandExec = new SQLiteCommand(sql, m_dbConnection)) //Associate request with connection to vault.)
                    {
                        m_dbConnection.Open(); //If first time, this models file as a vault, also opens a connection to it.
                        commandExec.ExecuteNonQuery(); //Execute request.

                        using (SQLiteDataReader reader = commandExec.ExecuteReader())//Object Reader.
                        {
                            if (reader.Read())
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
                }

                URLTextbox.Text = utils.DecryptText(key: Key, src: fullResults[0][0]);
                NameTextbox.Text = utils.DecryptText(key: Key, src: fullResults[0][1]);
                UsernameTextbox.Text = utils.DecryptText(key: Key, src: fullResults[0][2]);
                PasswordTextbox.Text = utils.DecryptText(key: Key, src: fullResults[0][3]);
                CategoryTextbox.Text = utils.DecryptText(key: Key, src: fullResults[0][4]);
                NotesTextbox.Text = utils.DecryptText(key: Key, src: fullResults[0][5]);

            }
            else
            {
                //Null textboxes
                URLTextbox.Text = null;
                NameTextbox.Text = null;
                UsernameTextbox.Text = null;
                PasswordTextbox.Text = null;
                CategoryTextbox.Text = null;
                NotesTextbox.Text = null;

            }
        }
    }
}
