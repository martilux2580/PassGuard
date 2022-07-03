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
    public partial class EditContent : Form
    {
        private String url { get; set; }
        private String name { get; set; }
        private String username { get; set; }
        private String password { get; set; }
        private String category { get; set; }
        private String notes { get; set; }

        private List<String> namesInDB;
        private readonly byte[] Key;
        private readonly String decPath;
        private bool editedSuccess;
        private String nameToBeEdited;
        private readonly Dictionary<String, String> map; //No duplicate keys, (enc, dec)

        internal String getHashofName(String name)
        {
            return map.FirstOrDefault(x => x.Value == NameCombobox.Text).Key;
        }
        
        public bool getEditedSuccess()
        {
            return editedSuccess;
        }
        internal String getNameToBeEdited()
        {
            return nameToBeEdited;
        }

        public String getUrl()
        {
            return url;
        }

        public String getName()
        {
            return name;
        }

        public String getUsername()
        {
            return username;
        }

        public String getPassword()
        {
            return password;
        }

        public String getCategory()
        {
            return category;
        }

        public String getNotes()
        {
            return notes;
        }

        public EditContent(List<String> names, byte[] key, String decpath)
        {
            InitializeComponent();
            Core.Utils utils = new Core.Utils();
            decPath = decpath;
            Key = key;

            namesInDB = names;
            map = new Dictionary<string, string>();
            foreach(String enc in namesInDB)
            {
                map.Add(enc, utils.DecryptText(key: Key, src: enc));
            }

            NameCombobox.Items.Add("");
            foreach (String name in namesInDB)
            {
                NameCombobox.Items.Add(utils.DecryptText(key: Key, src: name));
            }
            editedSuccess = false;

            try
            {
                this.Icon = new Icon(@".\Images\LogoIcon64123.ico"); //Loads Icon from Image folder.
            }
            catch (Exception)
            {
                MessageBox.Show(text: "PassGuard could not load some images.", caption: "Images not found", icon: MessageBoxIcon.Error, buttons: MessageBoxButtons.OK);
            }
        }

        private void EditButton_Click(object sender, EventArgs e)
        {
            Core.Utils utils = new Core.Utils();
            String errorMessages = "";

            if (String.IsNullOrWhiteSpace(NameTextbox.Text) || String.IsNullOrWhiteSpace(UsernameTextbox.Text) || String.IsNullOrWhiteSpace(PasswordTextbox.Text))
            {
                errorMessages += "Non-optional fields (Name, Username, Password) cannot be left blank.";
            }

            if ((map.Values.ToList<String>().Contains(NameTextbox.Text)) && (NameTextbox.Text != NameCombobox.Text))
            {
                errorMessages += "There is already a saved password with that name in the vault.";
            }

            if (!String.IsNullOrEmpty(errorMessages)) //If any error...
            {
                MessageBox.Show(text: "The following errors have been found:\n\n" + errorMessages, caption: "Warning(s)", icon: MessageBoxIcon.Warning, buttons: MessageBoxButtons.OK);
            }
            else //No error in params, create vault.
            {
                nameToBeEdited = map.FirstOrDefault(x => x.Value == NameCombobox.Text).Key;

                url = utils.EncryptText(key: Key, src: URLTextbox.Text);
                name = utils.EncryptText(key: Key, src: NameTextbox.Text);
                username = utils.EncryptText(key: Key, src: UsernameTextbox.Text);
                password = utils.EncryptText(key: Key, src: PasswordTextbox.Text);
                category = utils.EncryptText(key: Key, src: CategoryTextbox.Text);
                notes = utils.EncryptText(key: Key, src: NotesTextbox.Text);

                editedSuccess = true;

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
                    var sql = "SELECT * FROM Vault WHERE Name = @name1;";
                    using (SQLiteCommand commandExec = new SQLiteCommand(sql, m_dbConnection)) //Associate request with connection to vault.)
                    {
                        m_dbConnection.Open(); //If first time, this models file as a vault, also opens a connection to it.
                        commandExec.Prepare();
                        commandExec.Parameters.Add("@name1", DbType.String).Value = keyToSearch;
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

                //Enable elements
                URLLabel.Enabled = true;
                URLTextbox.Enabled = true;
                NameLabel.Enabled = true;
                NameTextbox.Enabled = true;
                UsernameLabel.Enabled = true;
                UsernameTextbox.Enabled = true;
                PassLabel.Enabled = true;
                PasswordTextbox.Enabled = true;
                CategoryLabel.Enabled = true;
                CategoryTextbox.Enabled = true;
                NotesLabel.Enabled = true;
                NotesTextbox.Enabled = true;
                EditButton.Enabled = true;

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

                //Unable elements
                URLLabel.Enabled = false;
                URLTextbox.Enabled = false;
                NameLabel.Enabled = false;
                NameTextbox.Enabled = false;
                UsernameLabel.Enabled = false;
                UsernameTextbox.Enabled = false;
                PassLabel.Enabled = false;
                PasswordTextbox.Enabled = false;
                CategoryLabel.Enabled = false;
                CategoryTextbox.Enabled = false;
                NotesLabel.Enabled = false;
                NotesTextbox.Enabled = false;
                EditButton.Enabled = false;

            }
        }
    }
}
