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
        private enum DBColumns
        {
            NULLVALUESS,
            Url,
            Name,
            Username,
            SitePassword,
            Category,
            Notes
        }
        private List<DataRowUC> DataRowUCList = new List<DataRowUC>();
        private String encryptedVaultPath;
        private readonly String vaultEmail;
        private readonly String vaultPass;
        private readonly byte[] vKey;
        private readonly byte[] cKey;

        public VaultContentUC(String path, String email, String pass, byte[] key, String SK)
        {
            InitializeComponent();
            Core.Utils utils = new Core.Utils();

            encryptedVaultPath = path;
            vaultEmail = email;
            vaultPass = pass;
            vKey = key;

            var keyVStr = utils.Base64ToString(Convert.ToBase64String(vKey));
            var skStr = utils.Base64ToString(SK);
            cKey = utils.getVaultKey(password: (keyVStr + (vaultEmail + vaultPass)), salt: Encoding.Default.GetBytes(skStr + keyVStr));

            LoadContent(Order.Normal, DBColumns.NULLVALUESS);
            SetCMS();
            

        }

        private void SetCMS()
        {
            var titleURL = new ToolStripLabel("ORDER BY URL");
            titleURL.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            titleURL.TextAlign = ContentAlignment.MiddleCenter;
            titleURL.ForeColor = Color.FromArgb(109, 109, 109);
            URLCMS.Items.Insert(0, titleURL);

            var titleName = new ToolStripLabel("ORDER BY NAME");
            titleName.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            titleName.TextAlign = ContentAlignment.MiddleCenter;
            titleName.ForeColor = Color.FromArgb(109, 109, 109);
            NameCMS.Items.Insert(0, titleName);

            var titleUsername = new ToolStripLabel("ORDER BY USERNAME");
            titleUsername.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            titleUsername.TextAlign = ContentAlignment.MiddleCenter;
            titleUsername.ForeColor = Color.FromArgb(109, 109, 109);
            UsernameCMS.Width += 20;
            UsernameCMS.Items.Insert(0, titleUsername);

            var titleCategory = new ToolStripLabel("ORDER BY CATEGORY");
            titleCategory.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            titleCategory.TextAlign = ContentAlignment.MiddleCenter;
            titleCategory.ForeColor = Color.FromArgb(109, 109, 109);
            CategoryCMS.Items.Insert(0, titleCategory);

            var titleNotes = new ToolStripLabel("ORDER BY NOTES");
            titleNotes.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            titleNotes.TextAlign = ContentAlignment.MiddleCenter;
            titleNotes.ForeColor = Color.FromArgb(109, 109, 109);
            NotesCMS.Items.Insert(0, titleNotes);
        }

        private void LoadContent(Order order, DBColumns col)
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

            if ((order != Order.Normal) && (col!= DBColumns.NULLVALUESS))
            {
                List<String> fullResults = new List<String>();
                using (TransactionScope tran = new TransactionScope()) //Just in case, atomic procedure....
                using (SQLiteConnection m_dbConnection = new SQLiteConnection("Data Source = " + decVault))
                using (SQLiteCommand commandExec = new SQLiteCommand("SELECT " + col.ToString() + " FROM Vault;", m_dbConnection)) //Associate request with connection to vault.)
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
                        var sql = "SELECT * FROM Vault WHERE " + col.ToString() + " = @col1;";
                        using (SQLiteCommand commandExec = new SQLiteCommand(sql, m_dbConnection)) //Associate request with connection to vault.)
                        {
                            commandExec.Prepare();
                            commandExec.Parameters.Add("@col1", DbType.String).Value = keyToSearch;
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
            try
            {
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

                if (add.getAddedSuccess()) //Exited add dialog from the add button, so we have valid data to insert. We didnt exit through AltF4 or X button.
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
                        var sql = "INSERT INTO Vault (Url, Name, Username, SitePassword, Category, Notes) values (@url1, @name2, @username3, @sitepassword4, @category5, @notes6);";
                        using (SQLiteCommand commandExec = new SQLiteCommand(sql, m_dbConnection)) //Associate request with connection to vault.)
                        {
                            m_dbConnection.Open(); //If first time, this models file as a vault, also opens a connection to it.

                            commandExec.Prepare();
                            commandExec.Parameters.Add("@url1", DbType.String).Value = newUrl;
                            commandExec.Parameters.Add("@name2", DbType.String).Value = newName;
                            commandExec.Parameters.Add("@username3", DbType.String).Value = newUsername;
                            commandExec.Parameters.Add("@sitepassword4", DbType.String).Value = newPassword;
                            commandExec.Parameters.Add("@category5", DbType.String).Value = newCategory;
                            commandExec.Parameters.Add("@notes6", DbType.String).Value = newNotes;

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
            
                if (add.getAddedSuccess())
                {
                    if (ConfigurationManager.AppSettings.Get("AutoBackupState") == "true")
                    {
                        if (String.Equals(a:Path.GetFullPath(ConfigurationManager.AppSettings.Get("PathVaultForAutoBackup")), b: Path.GetFullPath(encryptedVaultPath)))
                        {
                            if (1 == Int32.Parse(ConfigurationManager.AppSettings.Get("FrequencyAutoBackup")))
                            {
                                if (utils.CreateBackup(srcPath: ConfigurationManager.AppSettings.Get("PathVaultForAutoBackup"), dstPath: ConfigurationManager.AppSettings.Get("dstBackupPathForSave")))
                                {
                                    MessageBox.Show(text: "AutoBackup was created successfully.", caption: "Success", buttons: MessageBoxButtons.OK, icon: MessageBoxIcon.Information);
                                }
                                else
                                {
                                    MessageBox.Show(text: "AutoBackup could not make a backup of the specified Vault, please try again later.", caption: "Error", buttons: MessageBoxButtons.OK, icon: MessageBoxIcon.Error);
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                if (ex is ConfigurationErrorsException) 
                {
                    MessageBox.Show(text: "PassGuard could not load configuration file, AutoBackup could not run.", caption: "App Config File not found", icon: MessageBoxIcon.Error, buttons: MessageBoxButtons.OK);
                }
                else if(ex is FormatException)
                {
                    MessageBox.Show(text: "PassGuard could not decrypt your Vault.", caption: "Error", icon: MessageBoxIcon.Error, buttons: MessageBoxButtons.OK);
                }
                else
                {
                    MessageBox.Show(text: "PassGuard could not fulfill this operation.", caption: "Error", icon: MessageBoxIcon.Error, buttons: MessageBoxButtons.OK);
                }
            }
            finally
            {
                if (File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\" + (vaultpath[0] + ".db3")))
                {
                    File.Delete(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\" + (vaultpath[0] + ".db3"));
                }
            }

        }

        private void DeleteButton_Click(object sender, EventArgs e)
        {
            Core.Utils utils = new Core.Utils();

            String[] lastvalue = encryptedVaultPath.Split('\\');
            var vaultpath = lastvalue[lastvalue.Length - 1].Split('.');
            var decVault = (Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\" + (vaultpath[0] + ".db3"));
            try
            {
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
                        var sql = "DELETE FROM Vault WHERE Name = @name1;";
                        using (SQLiteCommand commandExec = new SQLiteCommand(sql, m_dbConnection)) //Associate request with connection to vault.)
                        {
                            m_dbConnection.Open(); //If first time, this models file as a vault, also opens a connection to it.

                            commandExec.Prepare();
                            commandExec.Parameters.Add("@name1", DbType.String).Value = del.getNameToBeDeleted();

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

                if (del.getDeletedSuccess() || del.getDeletedAllSuccess())
                {
                    if (ConfigurationManager.AppSettings.Get("AutoBackupState") == "true")
                    {
                        if (String.Equals(a: Path.GetFullPath(ConfigurationManager.AppSettings.Get("PathVaultForAutoBackup")), b: Path.GetFullPath(encryptedVaultPath)))
                        {
                            if (1 == Int32.Parse(ConfigurationManager.AppSettings.Get("FrequencyAutoBackup")))
                            {
                                if (utils.CreateBackup(srcPath: ConfigurationManager.AppSettings.Get("PathVaultForAutoBackup"), dstPath: ConfigurationManager.AppSettings.Get("dstBackupPathForSave")))
                                {
                                    MessageBox.Show(text: "AutoBackup was created successfully.", caption: "Success", buttons: MessageBoxButtons.OK, icon: MessageBoxIcon.Information);
                                }
                                else
                                {
                                    MessageBox.Show(text: "AutoBackup could not make a backup of the specified Vault, please try again later.", caption: "Error", buttons: MessageBoxButtons.OK, icon: MessageBoxIcon.Error);
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                if (ex is ConfigurationErrorsException)
                {
                    MessageBox.Show(text: "PassGuard could not load configuration file, AutoBackup could not run.", caption: "App Config File not found", icon: MessageBoxIcon.Error, buttons: MessageBoxButtons.OK);
                }
                else if (ex is FormatException)
                {
                    MessageBox.Show(text: "PassGuard could not decrypt your Vault.", caption: "Error", icon: MessageBoxIcon.Error, buttons: MessageBoxButtons.OK);
                }
                else
                {
                    MessageBox.Show(text: "PassGuard could not fulfill this operation.", caption: "Error", icon: MessageBoxIcon.Error, buttons: MessageBoxButtons.OK);
                }
            }
            finally
            {
                if (File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\" + (vaultpath[0] + ".db3")))
                {
                    File.Delete(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\" + (vaultpath[0] + ".db3"));
                }
            }

        }

        private void EditButton_Click(object sender, EventArgs e)
        {
            Core.Utils utils = new Core.Utils();
            String[] lastvalue = encryptedVaultPath.Split('\\');
            var vaultpath = lastvalue[lastvalue.Length - 1].Split('.');
            var decVault = (Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\" + (vaultpath[0] + ".db3"));

            try
            {
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
                        var sql = "UPDATE Vault SET Url = @url1, Name = @name2, Username = @username3, SitePassword = @sitepassword4, Category = @category5, Notes = @notes6 WHERE Name = @nameedit7;";
                        using (SQLiteCommand commandExec = new SQLiteCommand(sql, m_dbConnection)) //Associate request with connection to vault.)
                        {
                            m_dbConnection.Open(); //If first time, this models file as a vault, also opens a connection to it.

                            commandExec.Prepare();
                            commandExec.Parameters.Add("@url1", DbType.String).Value = newUrl;
                            commandExec.Parameters.Add("@name2", DbType.String).Value = newName;
                            commandExec.Parameters.Add("@username3", DbType.String).Value = newUsername;
                            commandExec.Parameters.Add("@sitepassword4", DbType.String).Value = newPassword;
                            commandExec.Parameters.Add("@category5", DbType.String).Value = newCategory;
                            commandExec.Parameters.Add("@notes6", DbType.String).Value = newNotes;
                            commandExec.Parameters.Add("@nameedit7", DbType.String).Value = edit.getHashofName(name: edit.getNameToBeEdited());

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

                if (edit.getEditedSuccess())
                {
                    if (ConfigurationManager.AppSettings.Get("AutoBackupState") == "true")
                    {
                        if (String.Equals(a: Path.GetFullPath(ConfigurationManager.AppSettings.Get("PathVaultForAutoBackup")), b: Path.GetFullPath(encryptedVaultPath)))
                        {
                            if (1 == Int32.Parse(ConfigurationManager.AppSettings.Get("FrequencyAutoBackup")))
                            {
                                if (utils.CreateBackup(srcPath: ConfigurationManager.AppSettings.Get("PathVaultForAutoBackup"), dstPath: ConfigurationManager.AppSettings.Get("dstBackupPathForSave")))
                                {
                                    MessageBox.Show(text: "AutoBackup was created successfully.", caption: "Success", buttons: MessageBoxButtons.OK, icon: MessageBoxIcon.Information);
                                }
                                else
                                {
                                    MessageBox.Show(text: "AutoBackup could not make a backup of the specified Vault, please try again later.", caption: "Error", buttons: MessageBoxButtons.OK, icon: MessageBoxIcon.Error);
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                if (ex is ConfigurationErrorsException)
                {
                    MessageBox.Show(text: "PassGuard could not load configuration file, AutoBackup could not run.", caption: "App Config File not found", icon: MessageBoxIcon.Error, buttons: MessageBoxButtons.OK);
                }
                else if (ex is FormatException)
                {
                    MessageBox.Show(text: "PassGuard could not decrypt your Vault.", caption: "Error", icon: MessageBoxIcon.Error, buttons: MessageBoxButtons.OK);
                }
                else
                {
                    MessageBox.Show(text: "PassGuard could not fulfill this operation.", caption: "Error", icon: MessageBoxIcon.Error, buttons: MessageBoxButtons.OK);
                }
            }
            finally
            {
                if (File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\" + (vaultpath[0] + ".db3")))
                {
                    File.Delete(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\" + (vaultpath[0] + ".db3"));
                }
            }

        }

        private void HelpButton_Click(object sender, EventArgs e)
        {
            var data = "In this panel the contents of the Vault you have logged in will be shown. This panel is composed of a table with six columns (buttons) that show the saved data of each of your passwords, a space where all the passwords in your Vault will be displayed, and four buttons at the bottom where you can Add, Edit, Delete or get Help on how PassGuard works."
                + "\nIf you click in any of the column buttons, you can select to order your passwords by that column you´ve clicked in Normal Order (just as they where added), Ascending Order (from A-Z) or Descending Order (from Z-A). Initially everything is ordered in Normal Order. The Vault cannot be ordered by two or more columns simultaneosly."
                + "\nRows composed of buttons with your password data will be shown in the table. In case of the password itself, a text composed of '***************' will be displayed. If any of those buttons is clicked, the whole text data will be copied to your clipboard. All data is stored, but because of space issues it may not be shown completely, this could be the case of the Notes column."
                + "\nFour buttons at the bottom of the form will be displayed: "
                + "\n\tAdd: Adds a password to your Vault. Name, Username and Password cannot be blank, and the new Name cannot be already registered in your Vault. Be aware that textboxes support a limited amount of characters, in case of the Notes column you should be OK if you save brief notes."
                + "\n\tEdit: Edits a password from your Vault. In the drop-down list you can select the Name of the password you want to edit. Any field of the password can be modified, but the new Name cannot be already registered in your Vault (you can check all the registered names in the drop-down list)."
                + "\n\tDelete: Deletes a password from your Vault, or delete all the passwords from your Vault: In the drop-down list you can select the Name of the password you want to delete, then click on the button 'Delete Selected Element' and the data of that selected password will be deleted."
                + "\n\t\tIf the checkbox is Enabled, you can click in the button 'Delete All Elements' and all the elements of your Vault will be deleted."
                + "\n\t\tAny deleted element cannot be recovered. Before deleting an element or all the elements, a pop-up window asking for confirmation of the action will be shown."
                + "\n\tExport as PDF: Exports all the data from the current Vault into PDF format. This PDF will be stored in your My Documents folder in Windows OS. This action could take some minutes depending on the amount of data to export."
                + "\n\tHelp: Displays a pop-up window with information about how to manage your PassGuard Vault."
                + "\n\nHave a good day :)";
            MessageBox.Show(text: data, caption: "Information about operations in a Vault.", icon: MessageBoxIcon.Information, buttons: MessageBoxButtons.OK);
        }

        private void URLNormalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            String[] lastvalue = encryptedVaultPath.Split('\\');
            var vaultpath = lastvalue[lastvalue.Length - 1].Split('.');
            try
            {
                LoadContent(Order.Normal, DBColumns.Url);

                URLNormalToolStripMenuItem.Checked = true;
                URLAscendingToolStripMenuItem.Checked = false;
                URLDescendingToolStripMenuItem.Checked = false;
            }
            catch (Exception ex)
            {
                if (ex is ConfigurationErrorsException)
                {
                    MessageBox.Show(text: "PassGuard could not load configuration file, AutoBackup could not run.", caption: "App Config File not found", icon: MessageBoxIcon.Error, buttons: MessageBoxButtons.OK);
                }
                else if (ex is FormatException)
                {
                    MessageBox.Show(text: "PassGuard could not decrypt your Vault.", caption: "Error", icon: MessageBoxIcon.Error, buttons: MessageBoxButtons.OK);
                }
                else
                {
                    MessageBox.Show(text: "PassGuard could not fulfill this operation.", caption: "Error", icon: MessageBoxIcon.Error, buttons: MessageBoxButtons.OK);
                }
            }
            finally
            {
                if (File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\" + (vaultpath[0] + ".db3")))
                {
                    File.Delete(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\" + (vaultpath[0] + ".db3"));
                }
            }
        }

        private void URLAscendingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            String[] lastvalue = encryptedVaultPath.Split('\\');
            var vaultpath = lastvalue[lastvalue.Length - 1].Split('.');
            try
            {
                LoadContent(Order.Asc, DBColumns.Url);

                URLNormalToolStripMenuItem.Checked = true;
                URLAscendingToolStripMenuItem.Checked = false;
                URLDescendingToolStripMenuItem.Checked = false;
            }
            catch (Exception ex)
            {
                if (ex is ConfigurationErrorsException)
                {
                    MessageBox.Show(text: "PassGuard could not load configuration file, AutoBackup could not run.", caption: "App Config File not found", icon: MessageBoxIcon.Error, buttons: MessageBoxButtons.OK);
                }
                else if (ex is FormatException)
                {
                    MessageBox.Show(text: "PassGuard could not decrypt your Vault.", caption: "Error", icon: MessageBoxIcon.Error, buttons: MessageBoxButtons.OK);
                }
                else
                {
                    MessageBox.Show(text: "PassGuard could not fulfill this operation.", caption: "Error", icon: MessageBoxIcon.Error, buttons: MessageBoxButtons.OK);
                }
            }
            finally
            {
                if (File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\" + (vaultpath[0] + ".db3")))
                {
                    File.Delete(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\" + (vaultpath[0] + ".db3"));
                }
            }
        }

        private void URLDescendingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            String[] lastvalue = encryptedVaultPath.Split('\\');
            var vaultpath = lastvalue[lastvalue.Length - 1].Split('.');
            try
            {
                LoadContent(Order.Desc, DBColumns.Url);

                URLNormalToolStripMenuItem.Checked = true;
                URLAscendingToolStripMenuItem.Checked = false;
                URLDescendingToolStripMenuItem.Checked = false;
            }
            catch (Exception ex)
            {
                if (ex is ConfigurationErrorsException)
                {
                    MessageBox.Show(text: "PassGuard could not load configuration file, AutoBackup could not run.", caption: "App Config File not found", icon: MessageBoxIcon.Error, buttons: MessageBoxButtons.OK);
                }
                else if (ex is FormatException)
                {
                    MessageBox.Show(text: "PassGuard could not decrypt your Vault.", caption: "Error", icon: MessageBoxIcon.Error, buttons: MessageBoxButtons.OK);
                }
                else
                {
                    MessageBox.Show(text: "PassGuard could not fulfill this operation.", caption: "Error", icon: MessageBoxIcon.Error, buttons: MessageBoxButtons.OK);
                }
            }
            finally
            {
                if (File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\" + (vaultpath[0] + ".db3")))
                {
                    File.Delete(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\" + (vaultpath[0] + ".db3"));
                }
            }
        }

        private void NameNormalCMS_Click(object sender, EventArgs e)
        {
            String[] lastvalue = encryptedVaultPath.Split('\\');
            var vaultpath = lastvalue[lastvalue.Length - 1].Split('.');
            try
            {
                LoadContent(Order.Normal, DBColumns.Name);

                URLNormalToolStripMenuItem.Checked = true;
                URLAscendingToolStripMenuItem.Checked = false;
                URLDescendingToolStripMenuItem.Checked = false;
            }
            catch (Exception ex)
            {
                if (ex is ConfigurationErrorsException)
                {
                    MessageBox.Show(text: "PassGuard could not load configuration file, AutoBackup could not run.", caption: "App Config File not found", icon: MessageBoxIcon.Error, buttons: MessageBoxButtons.OK);
                }
                else if (ex is FormatException)
                {
                    MessageBox.Show(text: "PassGuard could not decrypt your Vault.", caption: "Error", icon: MessageBoxIcon.Error, buttons: MessageBoxButtons.OK);
                }
                else
                {
                    MessageBox.Show(text: "PassGuard could not fulfill this operation.", caption: "Error", icon: MessageBoxIcon.Error, buttons: MessageBoxButtons.OK);
                }
            }
            finally
            {
                if (File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\" + (vaultpath[0] + ".db3")))
                {
                    File.Delete(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\" + (vaultpath[0] + ".db3"));
                }
            }
        }

        private void NameAscendingCMS_Click(object sender, EventArgs e)
        {
            String[] lastvalue = encryptedVaultPath.Split('\\');
            var vaultpath = lastvalue[lastvalue.Length - 1].Split('.');
            try
            {
                LoadContent(Order.Asc, DBColumns.Name);

                URLNormalToolStripMenuItem.Checked = true;
                URLAscendingToolStripMenuItem.Checked = false;
                URLDescendingToolStripMenuItem.Checked = false;
            }
            catch (Exception ex)
            {
                if (ex is ConfigurationErrorsException)
                {
                    MessageBox.Show(text: "PassGuard could not load configuration file, AutoBackup could not run.", caption: "App Config File not found", icon: MessageBoxIcon.Error, buttons: MessageBoxButtons.OK);
                }
                else if (ex is FormatException)
                {
                    MessageBox.Show(text: "PassGuard could not decrypt your Vault.", caption: "Error", icon: MessageBoxIcon.Error, buttons: MessageBoxButtons.OK);
                }
                else
                {
                    MessageBox.Show(text: "PassGuard could not fulfill this operation.", caption: "Error", icon: MessageBoxIcon.Error, buttons: MessageBoxButtons.OK);
                }
            }
            finally
            {
                if (File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\" + (vaultpath[0] + ".db3")))
                {
                    File.Delete(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\" + (vaultpath[0] + ".db3"));
                }
            }

        }

        private void NameDescendingCMS_Click(object sender, EventArgs e)
        {
            String[] lastvalue = encryptedVaultPath.Split('\\');
            var vaultpath = lastvalue[lastvalue.Length - 1].Split('.');
            try
            {
                LoadContent(Order.Desc, DBColumns.Name);

                URLNormalToolStripMenuItem.Checked = true;
                URLAscendingToolStripMenuItem.Checked = false;
                URLDescendingToolStripMenuItem.Checked = false;
            }
            catch (Exception ex)
            {
                if (ex is ConfigurationErrorsException)
                {
                    MessageBox.Show(text: "PassGuard could not load configuration file, AutoBackup could not run.", caption: "App Config File not found", icon: MessageBoxIcon.Error, buttons: MessageBoxButtons.OK);
                }
                else if (ex is FormatException)
                {
                    MessageBox.Show(text: "PassGuard could not decrypt your Vault.", caption: "Error", icon: MessageBoxIcon.Error, buttons: MessageBoxButtons.OK);
                }
                else
                {
                    MessageBox.Show(text: "PassGuard could not fulfill this operation.", caption: "Error", icon: MessageBoxIcon.Error, buttons: MessageBoxButtons.OK);
                }
            }
            finally
            {
                if (File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\" + (vaultpath[0] + ".db3")))
                {
                    File.Delete(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\" + (vaultpath[0] + ".db3"));
                }
            }

        }

        private void UsernameNormalCMS_Click(object sender, EventArgs e)
        {
            String[] lastvalue = encryptedVaultPath.Split('\\');
            var vaultpath = lastvalue[lastvalue.Length - 1].Split('.');
            try
            {
                LoadContent(Order.Normal, DBColumns.Username);

                URLNormalToolStripMenuItem.Checked = true;
                URLAscendingToolStripMenuItem.Checked = false;
                URLDescendingToolStripMenuItem.Checked = false;
            }
            catch (Exception ex)
            {
                if (ex is ConfigurationErrorsException)
                {
                    MessageBox.Show(text: "PassGuard could not load configuration file, AutoBackup could not run.", caption: "App Config File not found", icon: MessageBoxIcon.Error, buttons: MessageBoxButtons.OK);
                }
                else if (ex is FormatException)
                {
                    MessageBox.Show(text: "PassGuard could not decrypt your Vault.", caption: "Error", icon: MessageBoxIcon.Error, buttons: MessageBoxButtons.OK);
                }
                else
                {
                    MessageBox.Show(text: "PassGuard could not fulfill this operation.", caption: "Error", icon: MessageBoxIcon.Error, buttons: MessageBoxButtons.OK);
                }
            }
            finally
            {
                if (File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\" + (vaultpath[0] + ".db3")))
                {
                    File.Delete(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\" + (vaultpath[0] + ".db3"));
                }
            }
        }

        private void UsernameAscendingCMS_Click(object sender, EventArgs e)
        {
            String[] lastvalue = encryptedVaultPath.Split('\\');
            var vaultpath = lastvalue[lastvalue.Length - 1].Split('.');
            try
            {
                LoadContent(Order.Asc, DBColumns.Username);

                URLNormalToolStripMenuItem.Checked = true;
                URLAscendingToolStripMenuItem.Checked = false;
                URLDescendingToolStripMenuItem.Checked = false;
            }
            catch (Exception ex)
            {
                if (ex is ConfigurationErrorsException)
                {
                    MessageBox.Show(text: "PassGuard could not load configuration file, AutoBackup could not run.", caption: "App Config File not found", icon: MessageBoxIcon.Error, buttons: MessageBoxButtons.OK);
                }
                else if (ex is FormatException)
                {
                    MessageBox.Show(text: "PassGuard could not decrypt your Vault.", caption: "Error", icon: MessageBoxIcon.Error, buttons: MessageBoxButtons.OK);
                }
                else
                {
                    MessageBox.Show(text: "PassGuard could not fulfill this operation.", caption: "Error", icon: MessageBoxIcon.Error, buttons: MessageBoxButtons.OK);
                }
            }
            finally
            {
                if (File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\" + (vaultpath[0] + ".db3")))
                {
                    File.Delete(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\" + (vaultpath[0] + ".db3"));
                }
            }
        }

        private void UsernameDescendingCMS_Click(object sender, EventArgs e)
        {
            String[] lastvalue = encryptedVaultPath.Split('\\');
            var vaultpath = lastvalue[lastvalue.Length - 1].Split('.');
            try
            {
                LoadContent(Order.Desc, DBColumns.Username);

                URLNormalToolStripMenuItem.Checked = true;
                URLAscendingToolStripMenuItem.Checked = false;
                URLDescendingToolStripMenuItem.Checked = false;
            }
            catch (Exception ex)
            {
                if (ex is ConfigurationErrorsException)
                {
                    MessageBox.Show(text: "PassGuard could not load configuration file, AutoBackup could not run.", caption: "App Config File not found", icon: MessageBoxIcon.Error, buttons: MessageBoxButtons.OK);
                }
                else if (ex is FormatException)
                {
                    MessageBox.Show(text: "PassGuard could not decrypt your Vault.", caption: "Error", icon: MessageBoxIcon.Error, buttons: MessageBoxButtons.OK);
                }
                else
                {
                    MessageBox.Show(text: "PassGuard could not fulfill this operation.", caption: "Error", icon: MessageBoxIcon.Error, buttons: MessageBoxButtons.OK);
                }
            }
            finally
            {
                if (File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\" + (vaultpath[0] + ".db3")))
                {
                    File.Delete(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\" + (vaultpath[0] + ".db3"));
                }
            }
        }

        private void CategoryNormalCMS_Click(object sender, EventArgs e)
        {
            String[] lastvalue = encryptedVaultPath.Split('\\');
            var vaultpath = lastvalue[lastvalue.Length - 1].Split('.');
            try
            {
                LoadContent(Order.Normal, DBColumns.Category);

                URLNormalToolStripMenuItem.Checked = true;
                URLAscendingToolStripMenuItem.Checked = false;
                URLDescendingToolStripMenuItem.Checked = false;
            }
            catch (Exception ex)
            {
                if (ex is ConfigurationErrorsException)
                {
                    MessageBox.Show(text: "PassGuard could not load configuration file, AutoBackup could not run.", caption: "App Config File not found", icon: MessageBoxIcon.Error, buttons: MessageBoxButtons.OK);
                }
                else if (ex is FormatException)
                {
                    MessageBox.Show(text: "PassGuard could not decrypt your Vault.", caption: "Error", icon: MessageBoxIcon.Error, buttons: MessageBoxButtons.OK);
                }
                else
                {
                    MessageBox.Show(text: "PassGuard could not fulfill this operation.", caption: "Error", icon: MessageBoxIcon.Error, buttons: MessageBoxButtons.OK);
                }
            }
            finally
            {
                if (File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\" + (vaultpath[0] + ".db3")))
                {
                    File.Delete(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\" + (vaultpath[0] + ".db3"));
                }
            }
        }

        private void CategoryAscendingCMS_Click(object sender, EventArgs e)
        {
            String[] lastvalue = encryptedVaultPath.Split('\\');
            var vaultpath = lastvalue[lastvalue.Length - 1].Split('.');
            try
            {
                LoadContent(Order.Asc, DBColumns.Category);

                URLNormalToolStripMenuItem.Checked = true;
                URLAscendingToolStripMenuItem.Checked = false;
                URLDescendingToolStripMenuItem.Checked = false;
            }
            catch (Exception ex)
            {
                if (ex is ConfigurationErrorsException)
                {
                    MessageBox.Show(text: "PassGuard could not load configuration file, AutoBackup could not run.", caption: "App Config File not found", icon: MessageBoxIcon.Error, buttons: MessageBoxButtons.OK);
                }
                else if (ex is FormatException)
                {
                    MessageBox.Show(text: "PassGuard could not decrypt your Vault.", caption: "Error", icon: MessageBoxIcon.Error, buttons: MessageBoxButtons.OK);
                }
                else
                {
                    MessageBox.Show(text: "PassGuard could not fulfill this operation.", caption: "Error", icon: MessageBoxIcon.Error, buttons: MessageBoxButtons.OK);
                }
            }
            finally
            {
                if (File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\" + (vaultpath[0] + ".db3")))
                {
                    File.Delete(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\" + (vaultpath[0] + ".db3"));
                }
            }

        }

        private void CategoryDescendingCMS_Click(object sender, EventArgs e)
        {
            String[] lastvalue = encryptedVaultPath.Split('\\');
            var vaultpath = lastvalue[lastvalue.Length - 1].Split('.');
            try
            {
                LoadContent(Order.Desc, DBColumns.Category);

                URLNormalToolStripMenuItem.Checked = true;
                URLAscendingToolStripMenuItem.Checked = false;
                URLDescendingToolStripMenuItem.Checked = false;
            }
            catch (Exception ex)
            {
                if (ex is ConfigurationErrorsException)
                {
                    MessageBox.Show(text: "PassGuard could not load configuration file, AutoBackup could not run.", caption: "App Config File not found", icon: MessageBoxIcon.Error, buttons: MessageBoxButtons.OK);
                }
                else if (ex is FormatException)
                {
                    MessageBox.Show(text: "PassGuard could not decrypt your Vault.", caption: "Error", icon: MessageBoxIcon.Error, buttons: MessageBoxButtons.OK);
                }
                else
                {
                    MessageBox.Show(text: "PassGuard could not fulfill this operation.", caption: "Error", icon: MessageBoxIcon.Error, buttons: MessageBoxButtons.OK);
                }
            }
            finally
            {
                if (File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\" + (vaultpath[0] + ".db3")))
                {
                    File.Delete(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\" + (vaultpath[0] + ".db3"));
                }
            }

        }

        private void NotesNormalCMS_Click(object sender, EventArgs e)
        {
            String[] lastvalue = encryptedVaultPath.Split('\\');
            var vaultpath = lastvalue[lastvalue.Length - 1].Split('.');
            try
            {
                LoadContent(Order.Normal, DBColumns.Notes);

                URLNormalToolStripMenuItem.Checked = true;
                URLAscendingToolStripMenuItem.Checked = false;
                URLDescendingToolStripMenuItem.Checked = false;
            }
            catch (Exception ex)
            {
                if (ex is ConfigurationErrorsException)
                {
                    MessageBox.Show(text: "PassGuard could not load configuration file, AutoBackup could not run.", caption: "App Config File not found", icon: MessageBoxIcon.Error, buttons: MessageBoxButtons.OK);
                }
                else if (ex is FormatException)
                {
                    MessageBox.Show(text: "PassGuard could not decrypt your Vault.", caption: "Error", icon: MessageBoxIcon.Error, buttons: MessageBoxButtons.OK);
                }
                else
                {
                    MessageBox.Show(text: "PassGuard could not fulfill this operation.", caption: "Error", icon: MessageBoxIcon.Error, buttons: MessageBoxButtons.OK);
                }
            }
            finally
            {
                if (File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\" + (vaultpath[0] + ".db3")))
                {
                    File.Delete(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\" + (vaultpath[0] + ".db3"));
                }
            }

        }

        private void NotesAscendingCMS_Click(object sender, EventArgs e)
        {
            String[] lastvalue = encryptedVaultPath.Split('\\');
            var vaultpath = lastvalue[lastvalue.Length - 1].Split('.');
            try
            {
                LoadContent(Order.Asc, DBColumns.Notes);

                URLNormalToolStripMenuItem.Checked = true;
                URLAscendingToolStripMenuItem.Checked = false;
                URLDescendingToolStripMenuItem.Checked = false;
            }
            catch (Exception ex)
            {
                if (ex is ConfigurationErrorsException)
                {
                    MessageBox.Show(text: "PassGuard could not load configuration file, AutoBackup could not run.", caption: "App Config File not found", icon: MessageBoxIcon.Error, buttons: MessageBoxButtons.OK);
                }
                else if (ex is FormatException)
                {
                    MessageBox.Show(text: "PassGuard could not decrypt your Vault.", caption: "Error", icon: MessageBoxIcon.Error, buttons: MessageBoxButtons.OK);
                }
                else
                {
                    MessageBox.Show(text: "PassGuard could not fulfill this operation.", caption: "Error", icon: MessageBoxIcon.Error, buttons: MessageBoxButtons.OK);
                }
            }
            finally
            {
                if (File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\" + (vaultpath[0] + ".db3")))
                {
                    File.Delete(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\" + (vaultpath[0] + ".db3"));
                }
            }
        }

        private void NotesDescendingCMS_Click(object sender, EventArgs e)
        {
            String[] lastvalue = encryptedVaultPath.Split('\\');
            var vaultpath = lastvalue[lastvalue.Length - 1].Split('.');
            try
            {
                LoadContent(Order.Desc, DBColumns.Notes);

                URLNormalToolStripMenuItem.Checked = true;
                URLAscendingToolStripMenuItem.Checked = false;
                URLDescendingToolStripMenuItem.Checked = false;
            }
            catch (Exception ex)
            {
                if (ex is ConfigurationErrorsException)
                {
                    MessageBox.Show(text: "PassGuard could not load configuration file, AutoBackup could not run.", caption: "App Config File not found", icon: MessageBoxIcon.Error, buttons: MessageBoxButtons.OK);
                }
                else if (ex is FormatException)
                {
                    MessageBox.Show(text: "PassGuard could not decrypt your Vault.", caption: "Error", icon: MessageBoxIcon.Error, buttons: MessageBoxButtons.OK);
                }
                else
                {
                    MessageBox.Show(text: "PassGuard could not fulfill this operation.", caption: "Error", icon: MessageBoxIcon.Error, buttons: MessageBoxButtons.OK);
                }
            }
            finally
            {
                if (File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\" + (vaultpath[0] + ".db3")))
                {
                    File.Delete(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\" + (vaultpath[0] + ".db3"));
                }
            }

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

            try
            {
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

                utils.CreatePDF(fullResults, lastValue[0], ConfigurationManager.AppSettings.Get("Email"), ConfigurationManager.AppSettings.Get("SecurityKey"));

                utils.Encrypt(vKey, (Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\" + (lastValue[0] + "." + lastValue[1])), Path.Combine(saveEncryptedVaultPath));
                File.Delete(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\" + (lastValue[0] + "." + lastValue[1]));
            }
            catch (Exception ex)
            {
                if (ex is ConfigurationErrorsException)
                {
                    MessageBox.Show(text: "PassGuard could not load configuration file, AutoBackup could not run.", caption: "App Config File not found", icon: MessageBoxIcon.Error, buttons: MessageBoxButtons.OK);
                }
                else if (ex is FormatException)
                {
                    MessageBox.Show(text: "PassGuard could not decrypt your Vault.", caption: "Error", icon: MessageBoxIcon.Error, buttons: MessageBoxButtons.OK);
                }
                else if (ex is iText.IO.IOException || ex is iText.Kernel.PdfException || ex is iText.Kernel.LicenseVersionException || ex is iText.Signatures.VerificationException)
                {
                    MessageBox.Show(text: "PassGuard could not create the PDF.", caption: "Error while creating the PDF", icon: MessageBoxIcon.Error, buttons: MessageBoxButtons.OK);
                }
                else
                {
                    MessageBox.Show(text: "PassGuard could not fulfill this operation.", caption: "Error", icon: MessageBoxIcon.Error, buttons: MessageBoxButtons.OK);
                }
            }
            finally
            {
                if (File.Exists(decVault))
                {
                    File.Delete(decVault);
                }
            }
            
        }
    }
}
