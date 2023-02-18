using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Versioning;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using System.Windows.Forms;

namespace PassGuard.GUI
{
    //ScrollBar is 61px

    //UC Component that shows the table with the content of your Vault and more components to manage the data of it.
    public partial class VaultContentUC : UserControl
    {
        private enum Order //Enum for the order of each column
        {
            Normal,
            Asc,
            Desc
        }
        private enum DBColumns //Enum for the valid names 
        {
            NULLVALUESS,
            Url,
            Name,
            Username,
            SitePassword,
            Category,
            Notes
        }
        private readonly List<DataRowUC> DataRowUCList = new(); //List of DataRows with the data of the passwords.
        private readonly String encryptedVaultPath;
        private readonly String vaultEmail;
        private readonly String vaultPass;
        private readonly byte[] vKey; //Vault
        private readonly byte[] cKey; //Content

        [SupportedOSPlatform("windows")]
        public VaultContentUC(String path, String email, String pass, byte[] key, String SK)
        {
            InitializeComponent();
            Core.Utils utils = new();

            encryptedVaultPath = path;
            vaultEmail = email;
            vaultPass = pass;
            vKey = key;

            //Calculate cKey
            var keyVStr = utils.Base64ToString(Convert.ToBase64String(vKey));
            var skStr = utils.Base64ToString(SK);
            cKey = utils.GetVaultKey(password: (keyVStr + (vaultEmail + vaultPass)), salt: Encoding.Default.GetBytes(skStr + keyVStr));

            //Load the content of the Vault without any column order, and set the CMS for the orders.
            LoadContent(Order.Normal, DBColumns.NULLVALUESS);
            SetCMS();
            

        }

        [SupportedOSPlatform("windows")]
        //Sets the contents for the CMS of each header button (except SitePassword)
        private void SetCMS()
        {
            var titleURL = new ToolStripLabel("ORDER BY URL")
            {
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                TextAlign = ContentAlignment.MiddleCenter,
                ForeColor = Color.FromArgb(109, 109, 109)
            };
            URLCMS.Items.Insert(0, titleURL);

            var titleName = new ToolStripLabel("ORDER BY NAME")
            {
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                TextAlign = ContentAlignment.MiddleCenter,
                ForeColor = Color.FromArgb(109, 109, 109)
            };
            NameCMS.Items.Insert(0, titleName);

            var titleUsername = new ToolStripLabel("ORDER BY USERNAME")
            {
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                TextAlign = ContentAlignment.MiddleCenter,
                ForeColor = Color.FromArgb(109, 109, 109)
            };
            UsernameCMS.Width += 20;
            UsernameCMS.Items.Insert(0, titleUsername);

            var titleCategory = new ToolStripLabel("ORDER BY CATEGORY")
            {
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                TextAlign = ContentAlignment.MiddleCenter,
                ForeColor = Color.FromArgb(109, 109, 109)
            };
            CategoryCMS.Items.Insert(0, titleCategory);

            var titleNotes = new ToolStripLabel("ORDER BY NOTES")
            {
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                TextAlign = ContentAlignment.MiddleCenter,
                ForeColor = Color.FromArgb(109, 109, 109)
            };
            NotesCMS.Items.Insert(0, titleNotes);
        }

        private void LoadContent(Order order, DBColumns col)
        {
            Core.Utils utils = new();

            //Set Path for encrypted Vault (for the Path.Combine())
            String[] saveEncryptedVaultPath = encryptedVaultPath.Split('\\');
            saveEncryptedVaultPath[0] = saveEncryptedVaultPath[0] + "\\";

            String[] lastValue = saveEncryptedVaultPath[saveEncryptedVaultPath.Length - 1].Split('.'); //[filename, filextension]
            lastValue[lastValue.Length - 1] = "db3"; //FileExtension
            var encVault = Path.Combine(saveEncryptedVaultPath); //Set path for encrypted vault.
            var decVault = (Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\" + (lastValue[0] + "." + lastValue[1])); //Set path for decrypted vault.

            utils.Decrypt(key: vKey, src: encVault, dst: decVault); //Decrypt Vault

            //Obtain all the contents of the vault.
            if ((order != Order.Normal) && (col!= DBColumns.NULLVALUESS)) //If order diff from normal, we have to order. If col diff from NULLVALUESS we have to order.
            {
                List<String> fullResults = new(); //Get all the values from the column the user wants to order
                using (TransactionScope tran = new()) //Just in case, atomic procedure....
                using (SQLiteConnection m_dbConnection = new("Data Source = " + decVault))
                using (SQLiteCommand commandExec = new("SELECT " + col.ToString() + " FROM Vault;", m_dbConnection)) //Associate request with connection to vault.
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
                Dictionary<String, String> map = new();
                foreach(String allColumnData in fullResults) //Map the values of the column the user wants to order with its decrypted text.
                {
                    map.Add(allColumnData, utils.DecryptText(key: cKey, src: allColumnData));
                }
                //Sort the values of that dictionary (decrypted values of the column) as the user wants. Nullvalues will go first in ascending order, last in descending order.
                var ColList = map.Values.ToList<String>();
                List<String> sortedList = new();
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
                DataRowUCList.Clear(); //Clear previous list so we can load data correctly, not duplicated
                using (TransactionScope tran = new()) //Just in case, atomic procedure....
                using (SQLiteConnection m_dbConnection = new("Data Source = " + decVault))
                {
                    m_dbConnection.Open(); //If first time, this models file as a vault, also opens a connection to it.
                    foreach (String column in sortedList) //ir eliminando los que vayamos sacando, ya que sino si hay repetidos sacará siempre el mismo...ERROR
                    {
                        var keyToSearch = map.FirstOrDefault(x => (x.Value == column)).Key; //Get the encrypted key of the row
                        var sql = "SELECT * FROM Vault WHERE " + col.ToString() + " = @col1;";
                        using (SQLiteCommand commandExec = new(sql, m_dbConnection)) //Associate request with connection to vault.)
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

                            DataRowUC data = new(orderedRow.ToList<String>(), cKey); //Create a new datarow with the data

                            DataRowUCList.Add(data); //Add it to the list.
                            map.Remove(keyToSearch); //Remove it from the map, so we keep retrieving data, not the same element everytime (FirstOrDefault)

                        }

                    }
                    //Indicates that creating the SQLiteDatabase went succesfully, so the database can be committed.
                    tran.Complete(); //Close and commit transaction.
                    tran.Dispose(); //Dispose transaction so it is no longer using the file.

                    m_dbConnection.Close(); //Close connection to vault.
                    m_dbConnection.Dispose();
                }

                ContentFlowLayoutPanel.Controls.Clear(); //Clear previous things
                foreach (DataRowUC row in DataRowUCList) 
                {
                    ContentFlowLayoutPanel.Controls.Add(row); //Add rows to table.
                }

            }
            else if (order == Order.Normal) //Order is normal, or it is first time loading content in the table...
            {
                List<String[]> fullResults = new();
                using (TransactionScope tran = new()) //Just in case, atomic procedure....
                using (SQLiteConnection m_dbConnection = new("Data Source = " + decVault))
                using (SQLiteCommand commandExec = new("SELECT * FROM Vault;", m_dbConnection)) //Associate request with connection to vault.)
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
                DataRowUCList.Clear(); //Clear previous content in the list and in the table.
                Random rnd = new();
                foreach (String[] row in fullResults)
                {
                    DataRowUC data = new(row.ToList<String>(), cKey); //Create row and add it to list.

                    DataRowUCList.Add(data);
                }

                foreach (DataRowUC row in DataRowUCList)
                {
                    ContentFlowLayoutPanel.Controls.Add(row); //Add datarows to table.
                }
            }

            utils.Encrypt(vKey, (Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\" + (lastValue[0] + "." + lastValue[1])), Path.Combine(saveEncryptedVaultPath)); //Encrypt Vault
            File.Delete(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\" + (lastValue[0] + "." + lastValue[1])); //Delete decrypted vault

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
            Core.Utils utils = new();

            String[] lastvalue = encryptedVaultPath.Split('\\');
            var vaultpath = lastvalue[lastvalue.Length - 1].Split('.'); //Path for decryption
            try
            {
                utils.Decrypt(vKey, encryptedVaultPath, (Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\" + (vaultpath[0] + ".db3")));

                List<String> names = new();
                using (TransactionScope tran = new()) //Just in case, atomic procedure....
                using (SQLiteConnection m_dbConnection = new("Data Source = " + (Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\" + (vaultpath[0] + ".db3"))))
                using (SQLiteCommand commandExec = new("SELECT Name FROM Vault;", m_dbConnection)) //Fetch names already in Vault
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

                GUI.AddContent add = new(names, cKey)
                {
                    BackColor = this.Parent.BackColor
                }; //Invoke Form and retrieve new data
                add.ShowDialog();

                if (add.addedSuccess) //Exited add dialog from the add button, so we have valid data to insert. We didnt exit through AltF4 or X button.
                {
                    String newUrl = add.url;
                    String newName = add.name;
                    String newUsername = add.username;
                    String newPassword = add.password;
                    String newCategory = add.category;
                    String newNotes = add.notes;

                    List<String[]> fullResults = new();
                    using (TransactionScope tran = new()) //Just in case, atomic procedure....
                    using (SQLiteConnection m_dbConnection = new("Data Source = " + (Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\" + (vaultpath[0] + ".db3"))))
                    {
                        var sql = "INSERT INTO Vault (Url, Name, Username, SitePassword, Category, Notes) values (@url1, @name2, @username3, @sitepassword4, @category5, @notes6);"; //Insert value
                        using (SQLiteCommand commandExec = new(sql, m_dbConnection)) //Associate request with connection to vault.)
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

                            using (SQLiteCommand commandRetrieveAll = new("SELECT * FROM Vault;", m_dbConnection)) //Get everything from Vault in order to display it.
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

                    ContentFlowLayoutPanel.Controls.Clear(); //Clear contents from table and list of datarows
                    DataRowUCList.Clear();
                    foreach (String[] row in fullResults)
                    {
                        DataRowUC data = new(row.ToList<String>(), cKey); //Add data rows to list

                        DataRowUCList.Add(data);
                    }

                    foreach (DataRowUC row in DataRowUCList)
                    {
                        ContentFlowLayoutPanel.Controls.Add(row); //Add new values to the table.
                    }
                }

                utils.Encrypt(vKey, (Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\" + (vaultpath[0] + ".db3")), encryptedVaultPath); //Encrypt changes
                File.Delete(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\" + (vaultpath[0] + ".db3")); //Delete decryption
            
                if (add.addedSuccess) //If autobackup is enabled after each change in the Vault, create backup
                {
                    if (ConfigurationManager.AppSettings["AutoBackupState"] == "true")
                    {
                        //Check the change in current vault is the vault autobackup has configured to be backed up.
                        if (String.Equals(a:Path.GetFullPath(ConfigurationManager.AppSettings["PathVaultForAutoBackup"]), b: Path.GetFullPath(encryptedVaultPath)))
                        {
                            if (1 == Int32.Parse(ConfigurationManager.AppSettings["FrequencyAutoBackup"]))
                            {
                                if (utils.CreateBackup(srcPath: ConfigurationManager.AppSettings["PathVaultForAutoBackup"], dstPath: ConfigurationManager.AppSettings["dstBackupPathForSave"]))
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
                if (File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\" + (vaultpath[0] + ".db3"))) //If error occurred and state is compromised, delete changes in Vault.
                {
                    File.Delete(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\" + (vaultpath[0] + ".db3"));
                }
            }

        }

        private void DeleteButton_Click(object sender, EventArgs e)
        {
            Core.Utils utils = new();

            String[] lastvalue = encryptedVaultPath.Split('\\');
            var vaultpath = lastvalue[lastvalue.Length - 1].Split('.');
            var decVault = (Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\" + (vaultpath[0] + ".db3")); //Path of decrypted Vault
            try
            {
                utils.Decrypt(vKey, encryptedVaultPath, decVault);

                List<String> names = new();
                using (TransactionScope tran = new()) //Just in case, atomic procedure....
                using (SQLiteConnection m_dbConnection = new("Data Source = " + (Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\" + (vaultpath[0] + ".db3"))))
                using (SQLiteCommand commandExec = new("SELECT Name FROM Vault;", m_dbConnection)) //Get names already in DB-
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

                GUI.DeleteContent del = new(names, cKey, decVault)
                {
                    BackColor = this.Parent.BackColor
                }; //Invoke delete form and get data for deletion of one row or all database.
                del.ShowDialog();

                if (del.deletedSuccess) //If valid data is for deleting one row.
                {
                    List<String[]> fullResults = new();
                    using (TransactionScope tran = new()) //Just in case, atomic procedure....
                    using (SQLiteConnection m_dbConnection = new("Data Source = " + (Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\" + (vaultpath[0] + ".db3"))))
                    {
                        var sql = "DELETE FROM Vault WHERE Name = @name1;"; //Delete the row
                        using (SQLiteCommand commandExec = new(sql, m_dbConnection)) //Associate request with connection to vault.)
                        {
                            m_dbConnection.Open(); //If first time, this models file as a vault, also opens a connection to it.

                            commandExec.Prepare();
                            commandExec.Parameters.Add("@name1", DbType.String).Value = del.nameToBeDeleted;

                            commandExec.ExecuteNonQuery(); //Execute request.

                            using (SQLiteCommand commandRetrieveAll = new("SELECT * FROM Vault;", m_dbConnection)) //Associate request with connection to vault.)
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

                    //Clear table and lists
                    ContentFlowLayoutPanel.Controls.Clear();
                    DataRowUCList.Clear();
                    //Add data to datarows
                    foreach (String[] row in fullResults)
                    {
                        DataRowUC data = new(row.ToList<String>(), cKey);

                        DataRowUCList.Add(data);
                    }
                    //Add datarows to table.
                    foreach (DataRowUC row in DataRowUCList)
                    {
                        ContentFlowLayoutPanel.Controls.Add(row);
                    }
                }
                else if (del.deletedAllSuccess) //If valid data is for deleting all contents in the Vault.
                {
                    List<String[]> fullResults = new();
                    using (TransactionScope tran = new()) //Just in case, atomic procedure....
                    using (SQLiteConnection m_dbConnection = new("Data Source = " + (Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\" + (vaultpath[0] + ".db3"))))
                    {
                        using (SQLiteCommand commandExec = new("DELETE FROM Vault", m_dbConnection)) //Delete everything from Vault.
                        {
                            m_dbConnection.Open(); //If first time, this models file as a vault, also opens a connection to it.
                            commandExec.ExecuteNonQuery(); //Execute request.

                            using (SQLiteCommand commandRetrieveAll = new("SELECT * FROM Vault;", m_dbConnection)) //Associate request with connection to vault.)
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
                    //Clear data from table and lists
                    ContentFlowLayoutPanel.Controls.Clear();
                    DataRowUCList.Clear();
                    //Add data to datarows
                    foreach (String[] row in fullResults)
                    {
                        DataRowUC data = new(row.ToList<String>(), cKey);

                        DataRowUCList.Add(data);
                    }
                    //Add datarows to table.
                    foreach (DataRowUC row in DataRowUCList)
                    {
                        ContentFlowLayoutPanel.Controls.Add(row);
                    }
                }

                utils.Encrypt(vKey, (Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\" + (vaultpath[0] + ".db3")), encryptedVaultPath); //Encrypt changes
                File.Delete(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\" + (vaultpath[0] + ".db3")); //Delete old data

                if (del.deletedSuccess || del.deletedAllSuccess) 
                {
                    if (ConfigurationManager.AppSettings["AutoBackupState"] == "true") //If autobackup was set for every change in the Vault
                    {
                        //If the Vault we are making changes is the same vault set to autobackup, and the mode is 1, do autobackup.
                        if (String.Equals(a: Path.GetFullPath(ConfigurationManager.AppSettings["PathVaultForAutoBackup"]), b: Path.GetFullPath(encryptedVaultPath)))
                        {
                            if (1 == Int32.Parse(ConfigurationManager.AppSettings["FrequencyAutoBackup"]))
                            {
                                if (utils.CreateBackup(srcPath: ConfigurationManager.AppSettings["PathVaultForAutoBackup"], dstPath: ConfigurationManager.AppSettings["dstBackupPathForSave"]))
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
                if (File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\" + (vaultpath[0] + ".db3"))) //Delete old files in case of errors
                {
                    File.Delete(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\" + (vaultpath[0] + ".db3"));
                }
            }

        }

        private void EditButton_Click(object sender, EventArgs e)
        {
            Core.Utils utils = new();
            String[] lastvalue = encryptedVaultPath.Split('\\');
            var vaultpath = lastvalue[lastvalue.Length - 1].Split('.');
            var decVault = (Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\" + (vaultpath[0] + ".db3")); //Decrypt vault path

            try
            {
                utils.Decrypt(vKey, encryptedVaultPath, decVault);

                List<String> names = new();
                using (TransactionScope tran = new()) //Just in case, atomic procedure....
                using (SQLiteConnection m_dbConnection = new("Data Source = " + (Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\" + (vaultpath[0] + ".db3"))))
                using (SQLiteCommand commandExec = new("SELECT Name FROM Vault;", m_dbConnection)) //Fetch all names already in Vault
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

                GUI.EditContent edit = new(names, cKey, decVault)
                {
                    BackColor = this.Parent.BackColor
                }; //Invoke edit form and retrieve data
                edit.ShowDialog();

                if (edit.editedSuccess) //Exited add dialog from the add button, so we have valid data to insert. We didnt exit through AltF4 or X button.
                {
                    String newUrl = edit.url;
                    String newName = edit.name;
                    String newUsername = edit.username;
                    String newPassword = edit.password;
                    String newCategory = edit.category;
                    String newNotes = edit.notes;

                    List<String[]> fullResults = new();
                    using (TransactionScope tran = new()) //Just in case, atomic procedure....
                    using (SQLiteConnection m_dbConnection = new("Data Source = " + (Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\" + (vaultpath[0] + ".db3"))))
                    {
                        var sql = "UPDATE Vault SET Url = @url1, Name = @name2, Username = @username3, SitePassword = @sitepassword4, Category = @category5, Notes = @notes6 WHERE Name = @nameedit7;"; //Update row
                        using (SQLiteCommand commandExec = new(sql, m_dbConnection)) //Associate request with connection to vault.)
                        {
                            m_dbConnection.Open(); //If first time, this models file as a vault, also opens a connection to it.

                            commandExec.Prepare();
                            commandExec.Parameters.Add("@url1", DbType.String).Value = newUrl;
                            commandExec.Parameters.Add("@name2", DbType.String).Value = newName;
                            commandExec.Parameters.Add("@username3", DbType.String).Value = newUsername;
                            commandExec.Parameters.Add("@sitepassword4", DbType.String).Value = newPassword;
                            commandExec.Parameters.Add("@category5", DbType.String).Value = newCategory;
                            commandExec.Parameters.Add("@notes6", DbType.String).Value = newNotes;
                            commandExec.Parameters.Add("@nameedit7", DbType.String).Value = edit.getHashofName(name: edit.nameToBeEdited);

                            commandExec.ExecuteNonQuery(); //Execute request.

                            using (SQLiteCommand commandRetrieveAll = new("SELECT * FROM Vault;", m_dbConnection)) //Associate request with connection to vault.)
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
                    //Clear table and lists
                    ContentFlowLayoutPanel.Controls.Clear();
                    DataRowUCList.Clear();
                    //Add data to datarows
                    foreach (String[] row in fullResults)
                    {
                        DataRowUC data = new(row.ToList<String>(), cKey);

                        DataRowUCList.Add(data);
                    }
                    //Add datarows to table.
                    foreach (DataRowUC row in DataRowUCList)
                    {
                        ContentFlowLayoutPanel.Controls.Add(row);
                    }
                }

                utils.Encrypt(vKey, (Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\" + (vaultpath[0] + ".db3")), encryptedVaultPath); //Encrypt changes
                File.Delete(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\" + (vaultpath[0] + ".db3")); //Delete old data

                if (edit.editedSuccess) 
                {
                    if (ConfigurationManager.AppSettings["AutoBackupState"] == "true") //If autobackup was set to every change in the Vault....
                    {
                        //If the Vault we are making changes is the same vault set to autobackup, and the mode is 1, do autobackup.
                        if (String.Equals(a: Path.GetFullPath(ConfigurationManager.AppSettings["PathVaultForAutoBackup"]), b: Path.GetFullPath(encryptedVaultPath)))
                        {
                            if (1 == Int32.Parse(ConfigurationManager.AppSettings["FrequencyAutoBackup"]))
                            {
                                if (utils.CreateBackup(srcPath: ConfigurationManager.AppSettings["PathVaultForAutoBackup"], dstPath: ConfigurationManager.AppSettings["dstBackupPathForSave"]))
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
                if (File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\" + (vaultpath[0] + ".db3"))) //Delete old data in case of errors
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
                //Load the ordered content depending on column and order, and set toolstrip check property.
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
                //Load the ordered content depending on column and order, and set toolstrip check property.
                LoadContent(Order.Asc, DBColumns.Url);

                URLNormalToolStripMenuItem.Checked = false;
                URLAscendingToolStripMenuItem.Checked = true;
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
                //Load the ordered content depending on column and order, and set toolstrip check property.
                LoadContent(Order.Desc, DBColumns.Url);

                URLNormalToolStripMenuItem.Checked = false;
                URLAscendingToolStripMenuItem.Checked = false;
                URLDescendingToolStripMenuItem.Checked = true;
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
                //Load the ordered content depending on column and order, and set toolstrip check property.
                LoadContent(Order.Normal, DBColumns.Name);

                NameNormalCMS.Checked = true;
                NameAscendingCMS.Checked = false;
                NameDescendingCMS.Checked = false;
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
                //Load the ordered content depending on column and order, and set toolstrip check property.
                LoadContent(Order.Asc, DBColumns.Name);

                NameNormalCMS.Checked = false;
                NameAscendingCMS.Checked = true;
                NameDescendingCMS.Checked = false;
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
                //Load the ordered content depending on column and order, and set toolstrip check property.
                LoadContent(Order.Desc, DBColumns.Name);

                NameNormalCMS.Checked = false;
                NameAscendingCMS.Checked = false;
                NameDescendingCMS.Checked = true;
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
                //Load the ordered content depending on column and order, and set toolstrip check property.
                LoadContent(Order.Normal, DBColumns.Username);

                UsernameNormalCMS.Checked = true;
                UsernameAscendingCMS.Checked = false;
                UsernameDescendingCMS.Checked = false;
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
                //Load the ordered content depending on column and order, and set toolstrip check property.
                LoadContent(Order.Asc, DBColumns.Username);

                UsernameNormalCMS.Checked = false;
                UsernameAscendingCMS.Checked = true;
                UsernameDescendingCMS.Checked = false;
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
                //Load the ordered content depending on column and order, and set toolstrip check property.
                LoadContent(Order.Desc, DBColumns.Username);

                UsernameNormalCMS.Checked = false;
                UsernameAscendingCMS.Checked = false;
                UsernameDescendingCMS.Checked = true;
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
                //Load the ordered content depending on column and order, and set toolstrip check property.
                LoadContent(Order.Normal, DBColumns.Category);

                CategoryNormalCMS.Checked = true;
                CategoryAscendingCMS.Checked = false;
                CategoryDescendingCMS.Checked = false;
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
                //Load the ordered content depending on column and order, and set toolstrip check property.
                LoadContent(Order.Asc, DBColumns.Category);

                CategoryNormalCMS.Checked = false;
                CategoryAscendingCMS.Checked = true;
                CategoryDescendingCMS.Checked = false;
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
                //Load the ordered content depending on column and order, and set toolstrip check property.
                LoadContent(Order.Desc, DBColumns.Category);

                CategoryNormalCMS.Checked = false;
                CategoryAscendingCMS.Checked = false;
                CategoryDescendingCMS.Checked = true;
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
                //Load the ordered content depending on column and order, and set toolstrip check property.
                LoadContent(Order.Normal, DBColumns.Notes);

                NotesNormalCMS.Checked = true;
                NotesAscendingCMS.Checked = false;
                NotesDescendingCMS.Checked = false;
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
                //Load the ordered content depending on column and order, and set toolstrip check property.
                LoadContent(Order.Asc, DBColumns.Notes);

                NotesNormalCMS.Checked = false;
                NotesAscendingCMS.Checked = true;
                NotesDescendingCMS.Checked = false;
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

                NotesNormalCMS.Checked = false;
                NotesAscendingCMS.Checked = false;
                NotesDescendingCMS.Checked = true;
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
            Core.Utils utils = new();

            String[] saveEncryptedVaultPath = encryptedVaultPath.Split('\\');
            saveEncryptedVaultPath[0] = saveEncryptedVaultPath[0] + "\\";

            String[] lastValue = saveEncryptedVaultPath[saveEncryptedVaultPath.Length - 1].Split('.');
            lastValue[lastValue.Length - 1] = "db3";

            var encVault = Path.Combine(saveEncryptedVaultPath);
            var decVault = (Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\" + (lastValue[0] + "." + lastValue[1]));

            try
            {
                utils.Decrypt(key: vKey, src: encVault, dst: decVault);

                List<String[]> fullResults = new();
                using (TransactionScope tran = new()) //Just in case, atomic procedure....
                using (SQLiteConnection m_dbConnection = new("Data Source = " + decVault))
                using (SQLiteCommand commandExec = new("SELECT * FROM Vault;", m_dbConnection)) //Get everything in order to write it in PDF.
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

                utils.CreatePDF(fullResults, lastValue[0], ConfigurationManager.AppSettings["Email"], ConfigurationManager.AppSettings["SecurityKey"]);

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
                else if (ex is iText.IO.Exceptions.IOException || ex is iText.Kernel.Exceptions.PdfException || ex is iText.Signatures.VerificationException)
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
                if (File.Exists(decVault)) //Delete old files in case of error.
                {
                    File.Delete(decVault);
                }
            }
            
        }

    }
}
