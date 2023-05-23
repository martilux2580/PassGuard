using iText.Kernel.Colors.Gradients;
using iText.Layout.Element;
using iText.StyledXmlParser.Jsoup.Nodes;
using Microsoft.AspNetCore.DataProtection.KeyManagement;
using PassGuard.Crypto;
using PassGuard.PDF;
using PassGuard.VaultQueries;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.Versioning;
using System.Security.Policy;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Transactions;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

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
			Notes,
			Important
		}
		private readonly String encryptedVaultPath;
		private readonly String vaultEmail;
		private readonly String vaultPass;
		private readonly byte[] vKey; //Vault
		private readonly byte[] cKey; //Content
		private ICrypt crypt = new AESAlgorithm();
		private IQuery query;

		[SupportedOSPlatform("windows")]
		public VaultContentUC(String path, String email, String pass, byte[] key, String SK)
		{
			InitializeComponent();
			IKDF kdf = new PBKDF2Function();

			encryptedVaultPath = path;
			vaultEmail = email;
			vaultPass = pass;
			vKey = key;

			//Calculate cKey
			var keyVStr = Utils.StringUtils.Base64ToString(Convert.ToBase64String(vKey));
			var skStr = Utils.StringUtils.Base64ToString(SK);
			cKey = kdf.GetVaultKey(password: (keyVStr + (vaultEmail + vaultPass)), salt: Encoding.Default.GetBytes(skStr + keyVStr), bytes: 32);

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

			var titleImportant = new ToolStripLabel("ORDER BY IMPORTANT")
			{
				Font = new Font("Segoe UI", 10, FontStyle.Bold),
				TextAlign = ContentAlignment.MiddleCenter,
				ForeColor = Color.FromArgb(109, 109, 109)
			};
			ImportantCMS.Width += 15;
			ImportantCMS.Items.Insert(0, titleImportant);
		}

		private void LoadContent(Order order, DBColumns col)
		{
			//Set Path for encrypted Vault (for the Path.Combine())
			String[] saveEncryptedVaultPath = encryptedVaultPath.Split('\\');
			saveEncryptedVaultPath[0] = saveEncryptedVaultPath[0] + "\\";

			String[] lastValue = saveEncryptedVaultPath[saveEncryptedVaultPath.Length - 1].Split('.'); //[filename, filextension]
			lastValue[lastValue.Length - 1] = "db3"; //FileExtension
			var encVault = Path.Combine(saveEncryptedVaultPath); //Set path for encrypted vault.
			var decVault = (Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\" + (lastValue[0] + "." + lastValue[1])); //Set path for decrypted vault.

			crypt.Decrypt(key: vKey, src: encVault, dst: decVault); //Decrypt Vault

			query = new Query(decVault);

			if ((order != Order.Normal) && (col!= DBColumns.NULLVALUESS)) //If order diff from normal, we have to order. If col diff from NULLVALUESS we have to order.
			{
				if (col == DBColumns.Important) //We will order by the Importance and then by Name.
				{
					//Obtain all the contents of the vault.
					List<String[]> nameAndImportance = new();
					nameAndImportance = query.GetNameAndImportance(); //List with name and importance (later in this if) decrypted.

					Dictionary<String, String> map = new();
					foreach (String[] nameImpPair in nameAndImportance) 
					{
						//Map the values of the names with its decrypted text.
						map.Add(nameImpPair[0], crypt.DecryptText(key: cKey, src: nameImpPair[0]));

						//Decrypt names and importance for later order them.
						nameImpPair[0] = crypt.DecryptText(key: cKey, src: nameImpPair[0]);
						nameImpPair[1] = crypt.DecryptText(key: cKey, src: nameImpPair[1]);
					}

					//Order the struct by importance, and then by name ascending or descending (Decrypted step).
					if(order == Order.Asc)
					{
						nameAndImportance = nameAndImportance.OrderByDescending(x => x[1]).ThenBy(x => x[0]).ToList();
					}
					else if (order == Order.Desc)
					{
						nameAndImportance = nameAndImportance.OrderByDescending(x => x[1]).ThenByDescending(x => x[0]).ToList();
					}

					List<String[]> fullResults = new();
					foreach (String[] columnPair in nameAndImportance) //ir eliminando los que vayamos sacando, ya que sino si hay repetidos sacará siempre el mismo...ERROR
					{
						var keyToSearch = map.FirstOrDefault(x => (x.Value == columnPair[0])).Key; //Get the encrypted key of the row

						fullResults.Add(query.GetDataGivenColumn(DBColumns.Name.ToString(), keyToSearch)); //We look here for Name, although col = Important.

						map.Remove(keyToSearch); //Remove it from the map, so we keep retrieving data, not the same element everytime (FirstOrDefault)

					}

					VaultContentDGV.Rows.Clear(); //Clear previous content in the list and in the table.
					foreach (String[] row in fullResults)
					{
						var tempImportant = crypt.DecryptText(key: cKey, src: row[6]); //Not to decrypt two times same string....
						VaultContentDGV.Rows.Add(new String[]
						{
							crypt.DecryptText(key: cKey, src: row[0]),
							crypt.DecryptText(key: cKey, src: row[1]),
							crypt.DecryptText(key: cKey, src: row[2]),
							String.Concat(Enumerable.Repeat("*", 15)), //Hide the password
							crypt.DecryptText(key: cKey, src: row[4]),
							crypt.DecryptText(key: cKey, src: row[5]),
							tempImportant == "1" ? "Important" : "Not Important" //If decrypts to "1" it is important, else is not.
						});
					}
				}
				else //Other column than Important, we can order them easily.
				{
					List<String> columnData = query.GetColumn(col.ToString());
					List<String[]> fullResults = new();

					Dictionary<String, String> map = new();
					foreach (String allColumnData in columnData) //Map the values of the column the user wants to order with its decrypted text.
					{
						map.Add(allColumnData, crypt.DecryptText(key: cKey, src: allColumnData));
					}
					//Sort the values of that dictionary (decrypted values of the column) as the user wants. Nullvalues will go first in ascending order, last in descending order.
					var ColList = map.Values.ToList<String>();
					List<String> sortedList = new();
					sortedList = ColList.OrderBy(p => (!String.IsNullOrEmpty(p) || !String.IsNullOrWhiteSpace(p))).ThenBy(p => p).ToList<String>();
					if (order == Order.Desc) { sortedList.Reverse(); }

					foreach (String column in sortedList) //ir eliminando los que vayamos sacando, ya que sino si hay repetidos sacará siempre el mismo...ERROR
					{
						var keyToSearch = map.FirstOrDefault(x => (x.Value == column)).Key; //Get the encrypted key of the row
						
						fullResults.Add(query.GetDataGivenColumn(col.ToString(), keyToSearch));

						map.Remove(keyToSearch); //Remove it from the map, so we keep retrieving data, not the same element everytime (FirstOrDefault)

					}

					VaultContentDGV.Rows.Clear();
					foreach (String[] row in fullResults)
					{
						var tempImportant = crypt.DecryptText(key: cKey, src: row[6]); //Not to decrypt two times same string....
						VaultContentDGV.Rows.Add(new String[]
						{
							crypt.DecryptText(key: cKey, src: row[0]),
							crypt.DecryptText(key: cKey, src: row[1]),
							crypt.DecryptText(key: cKey, src: row[2]),
							String.Concat(Enumerable.Repeat("*", 15)), //Hide the password
							crypt.DecryptText(key: cKey, src: row[4]),
							crypt.DecryptText(key: cKey, src: row[5]),
							tempImportant == "1" ? "Important" : "Not Important" //If decrypts to "1" it is important, else is not.
						});
					}

				}
			}
			else if (order == Order.Normal) //Order is normal, or it is first time loading content in the table...
			{
				List<String[]> fullResults = query.GetAllData();

				VaultContentDGV.Rows.Clear(); //Clear previous content in the list and in the table.
				foreach (String[] row in fullResults)
				{
					var tempImportant = crypt.DecryptText(key: cKey, src: row[6]); //Not to decrypt two times same string....
					VaultContentDGV.Rows.Add(new String[]
					{
							crypt.DecryptText(key: cKey, src: row[0]),
							crypt.DecryptText(key: cKey, src: row[1]),
							crypt.DecryptText(key: cKey, src: row[2]),
							String.Concat(Enumerable.Repeat("*", 15)), //Hide the password
							crypt.DecryptText(key: cKey, src: row[4]),
							crypt.DecryptText(key: cKey, src: row[5]),
							tempImportant == "1" ? "Important" : "Not Important" //If decrypts to "1" it is important, else is not.
					});
				}
			}

			crypt.Encrypt(vKey, (Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\" + (lastValue[0] + "." + lastValue[1])), Path.Combine(saveEncryptedVaultPath)); //Encrypt Vault
			File.Delete(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\" + (lastValue[0] + "." + lastValue[1])); //Delete decrypted vault

		}

		private void AddButton_Click(object sender, EventArgs e)
		{
			String[] lastvalue = encryptedVaultPath.Split('\\');
			var vaultpath = lastvalue[lastvalue.Length - 1].Split('.'); //Path for decryption
			try
			{
				crypt.Decrypt(vKey, encryptedVaultPath, (Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\" + (vaultpath[0] + ".db3")));

				query = new Query(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\" + (vaultpath[0] + ".db3"));
				List<String> names = query.GetColumn(DBColumns.Name.ToString());

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
					String newImportant = add.important;

					query.InsertData(newUrl, newName, newUsername, newPassword, newCategory, newNotes, newImportant);

					List<String[]> fullResults = query.GetAllData();

					VaultContentDGV.Rows.Clear(); //Clear previous content in the list and in the table.
					foreach (String[] row in fullResults)
					{
						var tempImportant = crypt.DecryptText(key: cKey, src: row[6]); //Not to decrypt two times same string....
						VaultContentDGV.Rows.Add(new String[]
						{
							crypt.DecryptText(key: cKey, src: row[0]),
							crypt.DecryptText(key: cKey, src: row[1]),
							crypt.DecryptText(key: cKey, src: row[2]),
							String.Concat(Enumerable.Repeat("*", 15)), //Hide the password
							crypt.DecryptText(key: cKey, src: row[4]),
							crypt.DecryptText(key: cKey, src: row[5]),
							tempImportant == "1" ? "Important" : "Not Important" //If decrypts to "1" it is important, else is not.
						});
					}
				}

				crypt.Encrypt(vKey, (Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\" + (vaultpath[0] + ".db3")), encryptedVaultPath); //Encrypt changes
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
								if (Backup.SystemBackup.CreateBackup(srcPath: ConfigurationManager.AppSettings["PathVaultForAutoBackup"], dstPath: ConfigurationManager.AppSettings["dstBackupPathForSave"]))
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
					Thread.Sleep(1000);
					File.Delete(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\" + (vaultpath[0] + ".db3"));
				}
			}

		}

		private void DeleteButton_Click(object sender, EventArgs e)
		{
			String[] lastvalue = encryptedVaultPath.Split('\\');
			var vaultpath = lastvalue[lastvalue.Length - 1].Split('.');
			var decVault = (Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\" + (vaultpath[0] + ".db3")); //Path of decrypted Vault
			try
			{
				crypt.Decrypt(vKey, encryptedVaultPath, decVault);

				query = new Query(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\" + (vaultpath[0] + ".db3"));
				List<String> names = query.GetColumn(DBColumns.Name.ToString());

				GUI.DeleteContent del = new(names, cKey, decVault)
				{
					BackColor = this.Parent.BackColor
				}; //Invoke delete form and get data for deletion of one row or all database.
				del.ShowDialog();

				if (del.deletedSuccess) //If valid data is for deleting one row.
				{
					query.DeletePassword(del.nameToBeDeleted);
					List<String[]> fullResults = query.GetAllData();

					VaultContentDGV.Rows.Clear(); //Clear previous content in the list and in the table.
					foreach (String[] row in fullResults)
					{
						var tempImportant = crypt.DecryptText(key: cKey, src: row[6]); //Not to decrypt two times same string....
						VaultContentDGV.Rows.Add(new String[]
						{
							crypt.DecryptText(key: cKey, src: row[0]),
							crypt.DecryptText(key: cKey, src: row[1]),
							crypt.DecryptText(key: cKey, src: row[2]),
							String.Concat(Enumerable.Repeat("*", 15)), //Hide the password
							crypt.DecryptText(key: cKey, src: row[4]),
							crypt.DecryptText(key: cKey, src: row[5]),
							tempImportant == "1" ? "Important" : "Not Important" //If decrypts to "1" it is important, else is not.
						});
					}
				}
				else if (del.deletedAllSuccess) //If valid data is for deleting all contents in the Vault.
				{
					query.DeleteAllData();
					List<String[]> fullResults = query.GetAllData();

					VaultContentDGV.Rows.Clear(); //Clear previous content in the list and in the table.
					foreach (String[] row in fullResults)
					{
						var tempImportant = crypt.DecryptText(key: cKey, src: row[6]); //Not to decrypt two times same string....
						VaultContentDGV.Rows.Add(new String[]
						{
							crypt.DecryptText(key: cKey, src: row[0]),
							crypt.DecryptText(key: cKey, src: row[1]),
							crypt.DecryptText(key: cKey, src: row[2]),
							String.Concat(Enumerable.Repeat("*", 15)), //Hide the password
							crypt.DecryptText(key: cKey, src: row[4]),
							crypt.DecryptText(key: cKey, src: row[5]),
							tempImportant == "1" ? "Important" : "Not Important" //If decrypts to "1" it is important, else is not.
						});
					}
				}

				crypt.Encrypt(vKey, (Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\" + (vaultpath[0] + ".db3")), encryptedVaultPath); //Encrypt changes
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
								if (Backup.SystemBackup.CreateBackup(srcPath: ConfigurationManager.AppSettings["PathVaultForAutoBackup"], dstPath: ConfigurationManager.AppSettings["dstBackupPathForSave"]))
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
			String[] lastvalue = encryptedVaultPath.Split('\\');
			var vaultpath = lastvalue[lastvalue.Length - 1].Split('.');
			var decVault = (Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\" + (vaultpath[0] + ".db3")); //Decrypt vault path

			try
			{
				crypt.Decrypt(vKey, encryptedVaultPath, decVault);
				query = new Query(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\" + (vaultpath[0] + ".db3"));

				List<String> names = query.GetColumn(DBColumns.Name.ToString());
				
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
					String newImportant = edit.important;

					query.UpdateData(newUrl, newName, newUsername, newPassword, newCategory, newNotes, newImportant, edit.getHashofName(name: edit.nameToBeEdited));
					List<String[]> fullResults = query.GetAllData();

					VaultContentDGV.Rows.Clear(); //Clear previous content in the list and in the table.
					foreach (String[] row in fullResults)
					{
						var tempImportant = crypt.DecryptText(key: cKey, src: row[6]); //Not to decrypt two times same string....
						VaultContentDGV.Rows.Add(new String[]
						{
							crypt.DecryptText(key: cKey, src: row[0]),
							crypt.DecryptText(key: cKey, src: row[1]),
							crypt.DecryptText(key: cKey, src: row[2]),
							String.Concat(Enumerable.Repeat("*", 15)), //Hide the password
							crypt.DecryptText(key: cKey, src: row[4]),
							crypt.DecryptText(key: cKey, src: row[5]),
							tempImportant == "1" ? "Important" : "Not Important" //If decrypts to "1" it is important, else is not.
						});
					}
				}

				crypt.Encrypt(vKey, (Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\" + (vaultpath[0] + ".db3")), encryptedVaultPath); //Encrypt changes
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
								if (Backup.SystemBackup.CreateBackup(srcPath: ConfigurationManager.AppSettings["PathVaultForAutoBackup"], dstPath: ConfigurationManager.AppSettings["dstBackupPathForSave"]))
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
			var data = "In this panel the contents of the Vault you have logged in will be shown. This panel is composed of a table with seven columns (buttons) that show the saved data of each of your passwords, a space where all the passwords in your Vault will be displayed, and four buttons at the bottom where you can Add, Edit, Delete or get Help on how PassGuard works."
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

		private void ImportantNormalCMS_Click(object sender, EventArgs e)
		{
			String[] lastvalue = encryptedVaultPath.Split('\\');
			var vaultpath = lastvalue[lastvalue.Length - 1].Split('.');
			try
			{
				LoadContent(Order.Normal, DBColumns.Important);

				ImportantNormalCMS.Checked = true;
				ImportantAscendingCMS.Checked = false;
				ImportantDescendingCMS.Checked = false;
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

		private void ImportantAscendingCMS_Click(object sender, EventArgs e)
		{
			String[] lastvalue = encryptedVaultPath.Split('\\');
			var vaultpath = lastvalue[lastvalue.Length - 1].Split('.');
			try
			{
				LoadContent(Order.Asc, DBColumns.Important);

				ImportantNormalCMS.Checked = false;
				ImportantAscendingCMS.Checked = true;
				ImportantDescendingCMS.Checked = false;
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

		private void ImportantDescendingCMS_Click(object sender, EventArgs e)
		{
			String[] lastvalue = encryptedVaultPath.Split('\\');
			var vaultpath = lastvalue[lastvalue.Length - 1].Split('.');
			try
			{
				LoadContent(Order.Desc, DBColumns.Important);

				ImportantNormalCMS.Checked = false;
				ImportantAscendingCMS.Checked = false;
				ImportantDescendingCMS.Checked = true;
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
			IPDF pdf = new PDFCreator();

			String[] saveEncryptedVaultPath = encryptedVaultPath.Split('\\');
			saveEncryptedVaultPath[0] = saveEncryptedVaultPath[0] + "\\";

			String[] lastValue = saveEncryptedVaultPath[saveEncryptedVaultPath.Length - 1].Split('.');
			lastValue[lastValue.Length - 1] = "db3";

			var encVault = Path.Combine(saveEncryptedVaultPath);
			var decVault = (Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\" + (lastValue[0] + "." + lastValue[1]));

			try
			{
				crypt.Decrypt(key: vKey, src: encVault, dst: decVault);
				query = new Query(decVault);

				List<String[]> fullResults = query.GetAllData();
				
				//Decrypt data.
				foreach (String[] row in fullResults)
				{
					row[0] = crypt.DecryptText(key: cKey, src: row[0]);
					row[1] = crypt.DecryptText(key: cKey, src: row[1]);
					row[2] = crypt.DecryptText(key: cKey, src: row[2]);
					row[3] = crypt.DecryptText(key: cKey, src: row[3]);
					row[4] = crypt.DecryptText(key: cKey, src: row[4]);
					row[5] = crypt.DecryptText(key: cKey, src: row[5]);
					row[6] = crypt.DecryptText(key: cKey, src: row[6]);
				}

				pdf.CreatePDF(fullResults, lastValue[0], ConfigurationManager.AppSettings["Email"], ConfigurationManager.AppSettings["SecurityKey"]);

				crypt.Encrypt(vKey, (Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\" + (lastValue[0] + "." + lastValue[1])), Path.Combine(saveEncryptedVaultPath)); 
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

		private void VaultContentUC_BackColorChanged(object sender, EventArgs e)
		{
			// Set the back color of the DataGridView
			VaultContentDGV.BackgroundColor = this.BackColor;

			// Set the back color of the column headers
			VaultContentDGV.ColumnHeadersDefaultCellStyle.BackColor = this.BackColor;

			// Set the back color of the rows
			VaultContentDGV.RowsDefaultCellStyle.BackColor = this.BackColor;

			// Set the back color of the selection
			VaultContentDGV.DefaultCellStyle.SelectionBackColor = this.BackColor;

		}

		[SupportedOSPlatform("windows")]
		private void AddButton_MouseEnter(object sender, EventArgs e)
		{
			AddButton.Font = new Font("Microsoft Sans Serif", 11, FontStyle.Underline); //Underline the text when mouse is in the button
		}

		[SupportedOSPlatform("windows")]
		private void AddButton_MouseLeave(object sender, EventArgs e)
		{
			AddButton.Font = new Font("Microsoft Sans Serif", 11, FontStyle.Regular); //Dont underline the text when mouse leaves
		}

		[SupportedOSPlatform("windows")]
		private void EditButton_MouseEnter(object sender, EventArgs e)
		{
			EditButton.Font = new Font("Microsoft Sans Serif", 11, FontStyle.Underline); //Underline the text when mouse is in the button
		}

		[SupportedOSPlatform("windows")]
		private void EditButton_MouseLeave(object sender, EventArgs e)
		{
			EditButton.Font = new Font("Microsoft Sans Serif", 11, FontStyle.Regular); //Dont underline the text when mouse leaves
		}

		[SupportedOSPlatform("windows")]
		private void DeleteButton_MouseEnter(object sender, EventArgs e)
		{
			DeleteButton.Font = new Font("Microsoft Sans Serif", 11, FontStyle.Underline); //Underline the text when mouse is in the button
		}

		[SupportedOSPlatform("windows")]
		private void DeleteButton_MouseLeave(object sender, EventArgs e)
		{
			DeleteButton.Font = new Font("Microsoft Sans Serif", 11, FontStyle.Regular); //Dont underline the text when mouse leaves
		}

		[SupportedOSPlatform("windows")]
		private void ExportAsPdfButton_MouseEnter(object sender, EventArgs e)
		{
			ExportAsPdfButton.Font = new Font("Microsoft Sans Serif", 11, FontStyle.Underline); //Underline the text when mouse is in the button
		}

		[SupportedOSPlatform("windows")]
		private void ExportAsPdfButton_MouseLeave(object sender, EventArgs e)
		{
			ExportAsPdfButton.Font = new Font("Microsoft Sans Serif", 11, FontStyle.Regular); //Dont underline the text when mouse leaves
		}

		[SupportedOSPlatform("windows")]
		private void HelpButton_MouseEnter(object sender, EventArgs e)
		{
			HelpButton.Font = new Font("Microsoft Sans Serif", 11, FontStyle.Underline); //Underline the text when mouse is in the button
		}

		[SupportedOSPlatform("windows")]
		private void HelpButton_MouseLeave(object sender, EventArgs e)
		{
			HelpButton.Font = new Font("Microsoft Sans Serif", 11, FontStyle.Regular); //Dont underline the text when mouse leaves
		}

		private void VaultContentDGV_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
		{
			if (e.Button == MouseButtons.Left)
			{
				DataGridViewColumn clickedColumn = VaultContentDGV.Columns[e.ColumnIndex];
				string columnName = clickedColumn.HeaderText;

				// Show the context menu strip at the mouse location 
				switch (columnName)
				{
					case "URL":
						URLCMS.Show(Cursor.Position);
						break;
					case "Name":
						NameCMS.Show(Cursor.Position);
						break;
					case "Site Username":
						UsernameCMS.Show(Cursor.Position);
						break;
					case "Category":
						CategoryCMS.Show(Cursor.Position);
						break;
					case "Notes":
						NotesCMS.Show(Cursor.Position);
						break;
					case "Important":
						ImportantCMS.Show(Cursor.Position);
						break;
					default:
						break;
				}

			}
		}
	}
}
