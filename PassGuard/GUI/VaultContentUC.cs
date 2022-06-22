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
    public partial class VaultContentUC : UserControl
    {
        private enum Order
        {
            Normal,
            Asc,
            Desc
        }
        private List<DataRowUC> DataRowUCList = new List<DataRowUC>();
        private String encryptedVaultPath;
        private readonly String vaultEmail;
        private readonly String vaultPass;
        private readonly byte[] vKey;
        private readonly byte[] cKey;

        public VaultContentUC(String path, String email, String pass, byte[] key)
        {
            InitializeComponent();
            Core.Utils utils = new Core.Utils();

            encryptedVaultPath = path;
            vaultEmail = email;
            vaultPass = pass;
            vKey = key;

            var keyVStr = utils.Base64ToString(Convert.ToBase64String(vKey));
            var skStr = utils.Base64ToString(ConfigurationManager.AppSettings.Get("SecurityKey"));
            cKey = utils.getVaultKey(password: (keyVStr + (vaultEmail + vaultPass)), salt: Encoding.Default.GetBytes(skStr + keyVStr));

            LoadContent(Order.Normal, null);
            SetCMS();
            

        }

        private void SetCMS()
        {
            var titleURL = new ToolStripLabel("FILTER BY URL");
            titleURL.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            titleURL.TextAlign = ContentAlignment.MiddleCenter;
            titleURL.ForeColor = Color.FromArgb(109, 109, 109);
            URLCMS.Items.Insert(0, titleURL);

            var titleName = new ToolStripLabel("FILTER BY NAME");
            titleName.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            titleName.TextAlign = ContentAlignment.MiddleCenter;
            titleName.ForeColor = Color.FromArgb(109, 109, 109);
            NameCMS.Items.Insert(0, titleName);

            var titleUsername = new ToolStripLabel("FILTER BY USERNAME");
            titleUsername.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            titleUsername.TextAlign = ContentAlignment.MiddleCenter;
            titleUsername.ForeColor = Color.FromArgb(109, 109, 109);
            UsernameCMS.Width += 20;
            UsernameCMS.Items.Insert(0, titleUsername);

            var titleCategory = new ToolStripLabel("FILTER BY CATEGORY");
            titleCategory.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            titleCategory.TextAlign = ContentAlignment.MiddleCenter;
            titleCategory.ForeColor = Color.FromArgb(109, 109, 109);
            CategoryCMS.Items.Insert(0, titleCategory);

            var titleNotes = new ToolStripLabel("FILTER BY NOTES");
            titleNotes.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            titleNotes.TextAlign = ContentAlignment.MiddleCenter;
            titleNotes.ForeColor = Color.FromArgb(109, 109, 109);
            NotesCMS.Items.Insert(0, titleNotes);
        }

        private void LoadContent(Order order, String col)
        {
            Core.Utils utils = new Core.Utils();

            String[] saveEncryptedVaultPath = encryptedVaultPath.Split('\\');
            saveEncryptedVaultPath[0] = saveEncryptedVaultPath[0] + "\\";

            String[] lastValue = saveEncryptedVaultPath[saveEncryptedVaultPath.Length - 1].Split('.');
            lastValue[lastValue.Length - 1] = "db3";
            var encVault = Path.Combine(saveEncryptedVaultPath);
            var decVault = (Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\" + (lastValue[0] + "." + lastValue[1]));

            utils.Decrypt(key: vKey, src: encVault, dst: decVault);

            //Obtain all the contents of the vault.

            if ((order != Order.Normal) && (col!= null))
            {
                List<String> fullResults = new List<String>();
                using (TransactionScope tran = new TransactionScope()) //Just in case, atomic procedure....
                using (SQLiteConnection m_dbConnection = new SQLiteConnection("Data Source = " + decVault))
                using (SQLiteCommand commandExec = new SQLiteCommand("SELECT " + col + " FROM Vault;", m_dbConnection)) //Associate request with connection to vault.)
                {
                    m_dbConnection.Open(); //If first time, this models file as a vault, also opens a connection to it.
                    commandExec.ExecuteNonQuery(); //Execute request.

                    using (SQLiteDataReader reader = commandExec.ExecuteReader())//Object Reader.
                    {
                        while (reader.Read()) //Reads each row.
                        {
                            fullResults.Add(reader.GetString(0));
                        }
                    }

                    commandExec.Dispose(); //Delete object so it is no longer using the file.

                    //Indicates that creating the SQLiteDatabase went succesfully, so the database can be committed.
                    tran.Complete(); //Close and commit transaction.
                    tran.Dispose(); //Dispose transaction so it is no longer using the file.

                    m_dbConnection.Close(); //Close connection to vault.
                    m_dbConnection.Dispose();


                }
                Dictionary<String, String> map = new Dictionary<string, string>();
                foreach(String allColumnData in fullResults) 
                {
                    map.Add(allColumnData, utils.DecryptText(key: cKey, src: allColumnData));
                }
                var ColList = map.Values.ToList<String>();
                List<String> sortedList = new List<string>();
                if(order == Order.Asc) 
                { 
                    sortedList = ColList.OrderBy(p => (!String.IsNullOrEmpty(p) || !String.IsNullOrWhiteSpace(p))).ThenBy(p => p).ToList<String>(); 
                }
                else if(order == Order.Desc)
                {
                    sortedList = ColList.OrderBy(p => (!String.IsNullOrEmpty(p) || !String.IsNullOrWhiteSpace(p))).ThenBy(p => p).ToList<String>();
                    sortedList.Reverse();
                }

                String[] orderedRow = new String[6];
                DataRowUCList.Clear();
                using (TransactionScope tran = new TransactionScope()) //Just in case, atomic procedure....
                using (SQLiteConnection m_dbConnection = new SQLiteConnection("Data Source = " + decVault))
                {
                    m_dbConnection.Open(); //If first time, this models file as a vault, also opens a connection to it.
                    foreach (String column in sortedList) //ir eliminando los que vayamos sacando, ya que sino si hay repetidos sacará siempre el mismo...ERROR
                    {
                        var keyToSearch = map.FirstOrDefault(x => (x.Value == column)).Key;
                        using (SQLiteCommand commandExec = new SQLiteCommand("SELECT * FROM Vault WHERE " + col + " = '" + keyToSearch + "';", m_dbConnection)) //Associate request with connection to vault.)
                        {
                            commandExec.ExecuteNonQuery(); //Execute request.

                            using (SQLiteDataReader reader = commandExec.ExecuteReader())//Object Reader.
                            {
                                if (reader.Read())
                                {
                                    orderedRow[0] = reader.GetString(0);
                                    orderedRow[1] = reader.GetString(1);
                                    orderedRow[2] = reader.GetString(2);
                                    orderedRow[3] = reader.GetString(3);
                                    orderedRow[4] = reader.GetString(4);
                                    orderedRow[5] = reader.GetString(5);
                                }
                            }

                            commandExec.Dispose(); //Delete object so it is no longer using the file.

                            DataRowUC data = new DataRowUC(orderedRow.ToList<String>(), cKey);

                            DataRowUCList.Add(data);
                            map.Remove(keyToSearch);

                        }

                    }
                    //Indicates that creating the SQLiteDatabase went succesfully, so the database can be committed.
                    tran.Complete(); //Close and commit transaction.
                    tran.Dispose(); //Dispose transaction so it is no longer using the file.

                    m_dbConnection.Close(); //Close connection to vault.
                    m_dbConnection.Dispose();
                }

                ContentFlowLayoutPanel.Controls.Clear();
                foreach (DataRowUC row in DataRowUCList)
                {
                    ContentFlowLayoutPanel.Controls.Add(row);
                }

            }
            else if (order == Order.Normal)
            {
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

                ContentFlowLayoutPanel.Controls.Clear();
                DataRowUCList.Clear();
                Random rnd = new Random();
                foreach (String[] row in fullResults)
                {
                    DataRowUC data = new DataRowUC(row.ToList<String>(), cKey);

                    DataRowUCList.Add(data);
                }

                foreach (DataRowUC row in DataRowUCList)
                {
                    ContentFlowLayoutPanel.Controls.Add(row);
                }
            }

            utils.Encrypt(vKey, (Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\" + (lastValue[0] + "." + lastValue[1])), Path.Combine(saveEncryptedVaultPath));
            File.Delete(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\" + (lastValue[0] + "." + lastValue[1]));

        }

        private void URLButton_Click(object sender, EventArgs e)
        {
            URLCMS.Show(URLButton, new Point(URLButton.Width - URLButton.Width, URLButton.Height)); //Sets where to display the ContextMenuStrip...
        }

        private void NameButton_Click(object sender, EventArgs e)
        {
            NameCMS.Show(NameButton, new Point(NameButton.Width - NameButton.Width, NameButton.Height)); //Sets where to display the ContextMenuStrip...
        }

        private void UsernameButton_Click(object sender, EventArgs e)
        {
            UsernameCMS.Show(UsernameButton, new Point(UsernameButton.Width - UsernameButton.Width, UsernameButton.Height)); //Sets where to display the ContextMenuStrip...
        }

        private void PassButton_Click(object sender, EventArgs e)
        {

        }

        private void CategoryButton_Click(object sender, EventArgs e)
        {
            CategoryCMS.Show(CategoryButton, new Point(CategoryButton.Width - CategoryCMS.Width, CategoryButton.Height)); //Sets where to display the ContextMenuStrip...
        }

        private void NotesButton_Click(object sender, EventArgs e)
        {
            NotesCMS.Show(NotesButton, new Point(NotesButton.Width - NotesCMS.Width, NotesButton.Height)); //Sets where to display the ContextMenuStrip...
        }

        private void AddButton_Click(object sender, EventArgs e)
        {
            Core.Utils utils = new Core.Utils();

            String[] lastvalue = encryptedVaultPath.Split('\\');
            var vaultpath = lastvalue[lastvalue.Length - 1].Split('.');
            utils.Decrypt(vKey, encryptedVaultPath, (Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\" + (vaultpath[0] + ".db3")));

            List<String> names = new List<String>();
            using (TransactionScope tran = new TransactionScope()) //Just in case, atomic procedure....
            using (SQLiteConnection m_dbConnection = new SQLiteConnection("Data Source = " + (Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\" + (vaultpath[0] + ".db3"))))
            using (SQLiteCommand commandExec = new SQLiteCommand("SELECT Name FROM Vault;", m_dbConnection)) //Associate request with connection to vault.)
            {
                m_dbConnection.Open(); //If first time, this models file as a vault, also opens a connection to it.
                commandExec.ExecuteNonQuery(); //Execute request.

                using (SQLiteDataReader reader = commandExec.ExecuteReader())//Object Reader.
                {
                    while (reader.Read()) //Reads each row.
                    {
                        names.Add(reader.GetString(0));
                    }
                }

                commandExec.Dispose(); //Delete object so it is no longer using the file.

                //Indicates that creating the SQLiteDatabase went succesfully, so the database can be committed.
                tran.Complete(); //Close and commit transaction.
                tran.Dispose(); //Dispose transaction so it is no longer using the file.

                m_dbConnection.Close(); //Close connection to vault.
                m_dbConnection.Dispose();


            }

            GUI.AddContent add = new GUI.AddContent(names, cKey);
            add.BackColor = this.Parent.BackColor;
            add.ShowDialog();

            if(add.getAddedSuccess()) //Exited add dialog from the add button, so we have valid data to insert. We didnt exit through AltF4 or X button.
            {
                String newUrl = add.getUrl(); 
                String newName = add.getName();
                String newUsername = add.getUsername();
                String newPassword = add.getPassword();
                String newCategory = add.getCategory();
                String newNotes = add.getNotes();

                List<String[]> fullResults = new List<String[]>();
                using (TransactionScope tran = new TransactionScope()) //Just in case, atomic procedure....
                using (SQLiteConnection m_dbConnection = new SQLiteConnection("Data Source = " + (Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\" + (vaultpath[0] + ".db3"))))
                {
                    var sql = "INSERT INTO Vault (Url, Name, Username, SitePassword, Category, Notes) values ('" + newUrl + "', '" + newName + "', '" + newUsername + "', '" + newPassword + "', '" + newCategory + "', '" + newNotes + "');";
                    using (SQLiteCommand commandExec = new SQLiteCommand(sql, m_dbConnection)) //Associate request with connection to vault.)
                    {
                        m_dbConnection.Open(); //If first time, this models file as a vault, also opens a connection to it.
                        commandExec.ExecuteNonQuery(); //Execute request.

                        using (SQLiteCommand commandRetrieveAll = new SQLiteCommand("SELECT * FROM Vault;", m_dbConnection)) //Associate request with connection to vault.)
                        {
                            commandRetrieveAll.ExecuteNonQuery(); //Execute request.

                            using (SQLiteDataReader reader = commandRetrieveAll.ExecuteReader())//Object Reader.
                            {
                                while (reader.Read()) //Reads each row.
                                {
                                    fullResults.Add(new string[6] { reader.GetString(0), reader.GetString(1), reader.GetString(2), reader.GetString(3), reader.GetString(4), reader.GetString(5) });
                                }
                            }

                            commandRetrieveAll.Dispose();
                        }


                        commandExec.Dispose(); //Delete object so it is no longer using the file.

                        //Indicates that creating the SQLiteDatabase went succesfully, so the database can be committed.
                        tran.Complete(); //Close and commit transaction.
                        tran.Dispose(); //Dispose transaction so it is no longer using the file.

                        m_dbConnection.Close(); //Close connection to vault.
                        m_dbConnection.Dispose();

                    }
                }

                ContentFlowLayoutPanel.Controls.Clear();
                DataRowUCList.Clear();
                foreach (String[] row in fullResults)
                {
                    DataRowUC data = new DataRowUC(row.ToList<String>(), cKey);

                    DataRowUCList.Add(data);
                }

                foreach (DataRowUC row in DataRowUCList)
                {
                    ContentFlowLayoutPanel.Controls.Add(row);
                }
            }

            utils.Encrypt(vKey, (Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\" + (vaultpath[0] + ".db3")), encryptedVaultPath);
            File.Delete(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\" + (vaultpath[0] + ".db3"));

        }

        private void DeleteButton_Click(object sender, EventArgs e)
        {
            Core.Utils utils = new Core.Utils();

            String[] lastvalue = encryptedVaultPath.Split('\\');
            var vaultpath = lastvalue[lastvalue.Length - 1].Split('.');
            var decVault = (Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\" + (vaultpath[0] + ".db3"));
            utils.Decrypt(vKey, encryptedVaultPath, decVault);

            List<String> names = new List<String>();
            using (TransactionScope tran = new TransactionScope()) //Just in case, atomic procedure....
            using (SQLiteConnection m_dbConnection = new SQLiteConnection("Data Source = " + (Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\" + (vaultpath[0] + ".db3"))))
            using (SQLiteCommand commandExec = new SQLiteCommand("SELECT Name FROM Vault;", m_dbConnection)) //Associate request with connection to vault.)
            {
                m_dbConnection.Open(); //If first time, this models file as a vault, also opens a connection to it.
                commandExec.ExecuteNonQuery(); //Execute request.

                using (SQLiteDataReader reader = commandExec.ExecuteReader())//Object Reader.
                {
                    while (reader.Read()) //Reads each row.
                    {
                        names.Add(reader.GetString(0));
                    }
                }

                commandExec.Dispose(); //Delete object so it is no longer using the file.

                //Indicates that creating the SQLiteDatabase went succesfully, so the database can be committed.
                tran.Complete(); //Close and commit transaction.
                tran.Dispose(); //Dispose transaction so it is no longer using the file.

                m_dbConnection.Close(); //Close connection to vault.
                m_dbConnection.Dispose();


            }

            GUI.DeleteContent del = new GUI.DeleteContent(names, cKey, decVault); //names, cKey, decVault
            del.BackColor = this.Parent.BackColor;
            del.ShowDialog();

            if (del.getDeletedSuccess())
            {
                List<String[]> fullResults = new List<String[]>();
                using (TransactionScope tran = new TransactionScope()) //Just in case, atomic procedure....
                using (SQLiteConnection m_dbConnection = new SQLiteConnection("Data Source = " + (Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\" + (vaultpath[0] + ".db3"))))
                {
                    var sql = "DELETE FROM Vault WHERE Name = '" + del.getNameToBeDeleted() + "';";
                    using (SQLiteCommand commandExec = new SQLiteCommand(sql, m_dbConnection)) //Associate request with connection to vault.)
                    {
                        m_dbConnection.Open(); //If first time, this models file as a vault, also opens a connection to it.
                        commandExec.ExecuteNonQuery(); //Execute request.

                        using (SQLiteCommand commandRetrieveAll = new SQLiteCommand("SELECT * FROM Vault;", m_dbConnection)) //Associate request with connection to vault.)
                        {
                            commandRetrieveAll.ExecuteNonQuery(); //Execute request.

                            using (SQLiteDataReader reader = commandRetrieveAll.ExecuteReader())//Object Reader.
                            {
                                while (reader.Read()) //Reads each row.
                                {
                                    fullResults.Add(new string[6] { reader.GetString(0), reader.GetString(1), reader.GetString(2), reader.GetString(3), reader.GetString(4), reader.GetString(5) });
                                }
                            }

                            commandRetrieveAll.Dispose();
                        }


                        commandExec.Dispose(); //Delete object so it is no longer using the file.

                        //Indicates that creating the SQLiteDatabase went succesfully, so the database can be committed.
                        tran.Complete(); //Close and commit transaction.
                        tran.Dispose(); //Dispose transaction so it is no longer using the file.

                        m_dbConnection.Close(); //Close connection to vault.
                        m_dbConnection.Dispose();

                    }
                }

                ContentFlowLayoutPanel.Controls.Clear();
                DataRowUCList.Clear();
                foreach (String[] row in fullResults)
                {
                    DataRowUC data = new DataRowUC(row.ToList<String>(), cKey);

                    DataRowUCList.Add(data);
                }

                foreach (DataRowUC row in DataRowUCList)
                {
                    ContentFlowLayoutPanel.Controls.Add(row);
                }
            }
            else if (del.getDeletedAllSuccess())
            {
                List<String[]> fullResults = new List<String[]>();
                using (TransactionScope tran = new TransactionScope()) //Just in case, atomic procedure....
                using (SQLiteConnection m_dbConnection = new SQLiteConnection("Data Source = " + (Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\" + (vaultpath[0] + ".db3"))))
                {
                    using (SQLiteCommand commandExec = new SQLiteCommand("DELETE FROM Vault", m_dbConnection)) //Associate request with connection to vault.)
                    {
                        m_dbConnection.Open(); //If first time, this models file as a vault, also opens a connection to it.
                        commandExec.ExecuteNonQuery(); //Execute request.

                        using (SQLiteCommand commandRetrieveAll = new SQLiteCommand("SELECT * FROM Vault;", m_dbConnection)) //Associate request with connection to vault.)
                        {
                            commandRetrieveAll.ExecuteNonQuery(); //Execute request.

                            using (SQLiteDataReader reader = commandRetrieveAll.ExecuteReader())//Object Reader.
                            {
                                while (reader.Read()) //Reads each row.
                                {
                                    fullResults.Add(new string[6] { reader.GetString(0), reader.GetString(1), reader.GetString(2), reader.GetString(3), reader.GetString(4), reader.GetString(5) });
                                }
                            }

                            commandRetrieveAll.Dispose();
                        }


                        commandExec.Dispose(); //Delete object so it is no longer using the file.

                        //Indicates that creating the SQLiteDatabase went succesfully, so the database can be committed.
                        tran.Complete(); //Close and commit transaction.
                        tran.Dispose(); //Dispose transaction so it is no longer using the file.

                        m_dbConnection.Close(); //Close connection to vault.
                        m_dbConnection.Dispose();

                    }
                }

                ContentFlowLayoutPanel.Controls.Clear();
                DataRowUCList.Clear();
                foreach (String[] row in fullResults)
                {
                    DataRowUC data = new DataRowUC(row.ToList<String>(), cKey);

                    DataRowUCList.Add(data);
                }

                foreach (DataRowUC row in DataRowUCList)
                {
                    ContentFlowLayoutPanel.Controls.Add(row);
                }
            }

            utils.Encrypt(vKey, (Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\" + (vaultpath[0] + ".db3")), encryptedVaultPath);
            File.Delete(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\" + (vaultpath[0] + ".db3"));

        }

        private void EditButton_Click(object sender, EventArgs e)
        {
            Core.Utils utils = new Core.Utils();

            String[] lastvalue = encryptedVaultPath.Split('\\');
            var vaultpath = lastvalue[lastvalue.Length - 1].Split('.');
            var decVault = (Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\" + (vaultpath[0] + ".db3"));
            utils.Decrypt(vKey, encryptedVaultPath, decVault);

            List<String> names = new List<String>();
            using (TransactionScope tran = new TransactionScope()) //Just in case, atomic procedure....
            using (SQLiteConnection m_dbConnection = new SQLiteConnection("Data Source = " + (Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\" + (vaultpath[0] + ".db3"))))
            using (SQLiteCommand commandExec = new SQLiteCommand("SELECT Name FROM Vault;", m_dbConnection)) //Associate request with connection to vault.)
            {
                m_dbConnection.Open(); //If first time, this models file as a vault, also opens a connection to it.
                commandExec.ExecuteNonQuery(); //Execute request.

                using (SQLiteDataReader reader = commandExec.ExecuteReader())//Object Reader.
                {
                    while (reader.Read()) //Reads each row.
                    {
                        names.Add(reader.GetString(0));
                    }
                }

                commandExec.Dispose(); //Delete object so it is no longer using the file.

                //Indicates that creating the SQLiteDatabase went succesfully, so the database can be committed.
                tran.Complete(); //Close and commit transaction.
                tran.Dispose(); //Dispose transaction so it is no longer using the file.

                m_dbConnection.Close(); //Close connection to vault.
                m_dbConnection.Dispose();


            }

            GUI.EditContent edit = new GUI.EditContent(names, cKey, decVault);
            edit.BackColor = this.Parent.BackColor;
            edit.ShowDialog();

            if (edit.getEditedSuccess()) //Exited add dialog from the add button, so we have valid data to insert. We didnt exit through AltF4 or X button.
            {
                String newUrl = edit.getUrl();
                String newName = edit.getName();
                String newUsername = edit.getUsername();
                String newPassword = edit.getPassword();
                String newCategory = edit.getCategory();
                String newNotes = edit.getNotes();

                List<String[]> fullResults = new List<String[]>();
                using (TransactionScope tran = new TransactionScope()) //Just in case, atomic procedure....
                using (SQLiteConnection m_dbConnection = new SQLiteConnection("Data Source = " + (Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\" + (vaultpath[0] + ".db3"))))
                {
                    var sql = "UPDATE Vault SET Url = '" + newUrl + "', Name = '" + newName + "', Username = '" + newUsername + "', SitePassword = '" + newPassword + "', Category = '" + newCategory + "', Notes = '" + newNotes + "' WHERE Name = '" + edit.getHashofName(name: edit.getNameToBeEdited()) + "';";
                    using (SQLiteCommand commandExec = new SQLiteCommand(sql, m_dbConnection)) //Associate request with connection to vault.)
                    {
                        m_dbConnection.Open(); //If first time, this models file as a vault, also opens a connection to it.
                        commandExec.ExecuteNonQuery(); //Execute request.

                        using (SQLiteCommand commandRetrieveAll = new SQLiteCommand("SELECT * FROM Vault;", m_dbConnection)) //Associate request with connection to vault.)
                        {
                            commandRetrieveAll.ExecuteNonQuery(); //Execute request.

                            using (SQLiteDataReader reader = commandRetrieveAll.ExecuteReader())//Object Reader.
                            {
                                while (reader.Read()) //Reads each row.
                                {
                                    fullResults.Add(new string[6] { reader.GetString(0), reader.GetString(1), reader.GetString(2), reader.GetString(3), reader.GetString(4), reader.GetString(5) });
                                }
                            }

                            commandRetrieveAll.Dispose();
                        }


                        commandExec.Dispose(); //Delete object so it is no longer using the file.

                        //Indicates that creating the SQLiteDatabase went succesfully, so the database can be committed.
                        tran.Complete(); //Close and commit transaction.
                        tran.Dispose(); //Dispose transaction so it is no longer using the file.

                        m_dbConnection.Close(); //Close connection to vault.
                        m_dbConnection.Dispose();

                    }
                }

                ContentFlowLayoutPanel.Controls.Clear();
                DataRowUCList.Clear();
                foreach (String[] row in fullResults)
                {
                    DataRowUC data = new DataRowUC(row.ToList<String>(), cKey);

                    DataRowUCList.Add(data);
                }

                foreach (DataRowUC row in DataRowUCList)
                {
                    ContentFlowLayoutPanel.Controls.Add(row);
                }
            }

            utils.Encrypt(vKey, (Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\" + (vaultpath[0] + ".db3")), encryptedVaultPath);
            File.Delete(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\" + (vaultpath[0] + ".db3"));


        }

        private void HelpButton_Click(object sender, EventArgs e)
        {

        }

        private void URLNormalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LoadContent(Order.Normal, "Url");

            URLNormalToolStripMenuItem.Checked = true;
            URLAscendingToolStripMenuItem.Checked = false;
            URLDescendingToolStripMenuItem.Checked = false;
        }

        private void URLAscendingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LoadContent(Order.Asc, "Url");

            URLNormalToolStripMenuItem.Checked = false;
            URLAscendingToolStripMenuItem.Checked = true;
            URLDescendingToolStripMenuItem.Checked = false;
        }

        private void URLDescendingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LoadContent(Order.Desc, "Url");

            URLNormalToolStripMenuItem.Checked = false;
            URLAscendingToolStripMenuItem.Checked = false;
            URLDescendingToolStripMenuItem.Checked = true;
        }

        private void NameNormalCMS_Click(object sender, EventArgs e)
        {
            LoadContent(Order.Normal, "Name");

            NameNormalCMS.Checked = true;
            NameAscendingCMS.Checked = false;
            NameDescendingCMS.Checked = false;
        }

        private void NameAscendingCMS_Click(object sender, EventArgs e)
        {
            LoadContent(Order.Asc, "Name");

            NameNormalCMS.Checked = false;
            NameAscendingCMS.Checked = true;
            NameDescendingCMS.Checked = false;
        }

        private void NameDescendingCMS_Click(object sender, EventArgs e)
        {
            LoadContent(Order.Desc, "Name");

            NameNormalCMS.Checked = false;
            NameAscendingCMS.Checked = false;
            NameDescendingCMS.Checked = true;
        }

        private void UsernameNormalCMS_Click(object sender, EventArgs e)
        {
            LoadContent(Order.Normal, "Username");

            UsernameNormalCMS.Checked = true;
            UsernameAscendingCMS.Checked = false;
            UsernameDescendingCMS.Checked = false;
        }

        private void UsernameAscendingCMS_Click(object sender, EventArgs e)
        {
            LoadContent(Order.Asc, "Username");

            UsernameNormalCMS.Checked = false;
            UsernameAscendingCMS.Checked = true;
            UsernameDescendingCMS.Checked = false;
        }

        private void UsernameDescendingCMS_Click(object sender, EventArgs e)
        {
            LoadContent(Order.Desc, "Username");

            UsernameNormalCMS.Checked = false;
            UsernameAscendingCMS.Checked = false;
            UsernameDescendingCMS.Checked = true;
        }

        private void CategoryNormalCMS_Click(object sender, EventArgs e)
        {
            LoadContent(Order.Normal, "Category");

            CategoryNormalCMS.Checked = true;
            CategoryAscendingCMS.Checked = false;
            CategoryDescendingCMS.Checked = false;
        }

        private void CategoryAscendingCMS_Click(object sender, EventArgs e)
        {
            LoadContent(Order.Asc, "Category");

            CategoryNormalCMS.Checked = false;
            CategoryAscendingCMS.Checked = true;
            CategoryDescendingCMS.Checked = false;
        }

        private void CategoryDescendingCMS_Click(object sender, EventArgs e)
        {
            LoadContent(Order.Desc, "Category");

            CategoryNormalCMS.Checked = false;
            CategoryAscendingCMS.Checked = false;
            CategoryDescendingCMS.Checked = true;
        }

        private void NotesNormalCMS_Click(object sender, EventArgs e)
        {
            LoadContent(Order.Normal, "Notes");

            NotesNormalCMS.Checked = true;
            NotesAscendingCMS.Checked = false;
            NotesDescendingCMS.Checked = false;
        }

        private void NotesAscendingCMS_Click(object sender, EventArgs e)
        {
            LoadContent(Order.Asc, "Notes");

            NotesNormalCMS.Checked = false;
            NotesAscendingCMS.Checked = true;
            NotesDescendingCMS.Checked = false;
        }

        private void NotesDescendingCMS_Click(object sender, EventArgs e)
        {
            LoadContent(Order.Desc, "Notes");

            NotesNormalCMS.Checked = false;
            NotesAscendingCMS.Checked = false;
            NotesDescendingCMS.Checked = true;
        }

        private void ExportAsPdfButton_Click(object sender, EventArgs e)
        {
            Core.Utils utils = new Core.Utils();

            String[] saveEncryptedVaultPath = encryptedVaultPath.Split('\\');
            saveEncryptedVaultPath[0] = saveEncryptedVaultPath[0] + "\\";

            String[] lastValue = saveEncryptedVaultPath[saveEncryptedVaultPath.Length - 1].Split('.');
            lastValue[lastValue.Length - 1] = "db3";
            var encVault = Path.Combine(saveEncryptedVaultPath);
            var decVault = (Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\" + (lastValue[0] + "." + lastValue[1]));

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

            foreach (String[] row in fullResults)
            {
                row[0] = utils.DecryptText(key: cKey, src: row[0]);
                row[1] = utils.DecryptText(key: cKey, src: row[1]);
                row[2] = utils.DecryptText(key: cKey, src: row[2]);
                row[3] = utils.DecryptText(key: cKey, src: row[3]);
                row[4] = utils.DecryptText(key: cKey, src: row[4]);
                row[5] = utils.DecryptText(key: cKey, src: row[5]);
            }

            utils.CreateDOCX(fullResults, lastValue[0], ConfigurationManager.AppSettings.Get("Email"), ConfigurationManager.AppSettings.Get("SecurityKey"));

            utils.Encrypt(vKey, (Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\" + (lastValue[0] + "." + lastValue[1])), Path.Combine(saveEncryptedVaultPath));
            File.Delete(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\" + (lastValue[0] + "." + lastValue[1]));







            /*
            var xd = DateTime.Now.ToString();
            utils.convertDOCtoPDF(); //18 pags full text y 1 imagen grande en 4 segs
            var xd1 = DateTime.Now.ToString();
            MessageBox.Show("inicio: " + xd + "\nfinal: " + xd1);*/
        }
    }
}
