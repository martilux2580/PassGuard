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
		private DBColumns actualColumn;
		private Order actualOrder;
		private bool isSearched;

		[SupportedOSPlatform("windows")]
		public VaultContentUC(String path, String email, String pass, byte[] key, String SK)
		{
			InitializeComponent();
			IKDF kdf = new PBKDF2Function();

			encryptedVaultPath = path;
			vaultEmail = email;
			vaultPass = pass;
			vKey = key;
			actualColumn = DBColumns.NULLVALUESS;
			actualOrder = Order.Normal;
			isSearched = false;

			//Calculate cKey
			var keyVStr = Utils.StringUtils.Base64ToString(Convert.ToBase64String(vKey));
			var skStr = Utils.StringUtils.Base64ToString(SK);
			cKey = kdf.GetVaultKey(password: (keyVStr + (vaultEmail + vaultPass)), salt: Encoding.Default.GetBytes(skStr + keyVStr), bytes: 32);

			//Load the content of the Vault without any column order, and set the CMS for the orders.
			LoadContent(actualOrder, actualColumn);
			SetCMS();

		}

		public void TrimComponents()
		{
			SearchTextbox.Text = SearchTextbox.Text.Trim();
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

		private void ResetToNormalOrdering(bool actualSelects)
		{
			if (actualSelects)
			{
				actualOrder = Order.Normal;
				actualColumn = DBColumns.NULLVALUESS;
			}

			URLNormalCMS.Checked = true;
			URLAscendingCMS.Checked = false;
			URLDescendingCMS.Checked = false;

			NameNormalCMS.Checked = true;
			NameAscendingCMS.Checked = false;
			NameDescendingCMS.Checked = false;

			UsernameNormalCMS.Checked = true;
			UsernameAscendingCMS.Checked = false;
			UsernameDescendingCMS.Checked = false;

			CategoryNormalCMS.Checked = true;
			CategoryAscendingCMS.Checked = false;
			CategoryDescendingCMS.Checked = false;

			NotesNormalCMS.Checked = true;
			NotesAscendingCMS.Checked = false;
			NotesDescendingCMS.Checked = false;

			ImportantNormalCMS.Checked = true;
			ImportantAscendingCMS.Checked = false;
			ImportantDescendingCMS.Checked = false;
		}

		private void UncheckOrdering()
		{
			URLNormalCMS.Checked = false;
			URLAscendingCMS.Checked = false;
			URLDescendingCMS.Checked = false;

			NameNormalCMS.Checked = false;
			NameAscendingCMS.Checked = false;
			NameDescendingCMS.Checked = false;

			UsernameNormalCMS.Checked = false;
			UsernameAscendingCMS.Checked = false;
			UsernameDescendingCMS.Checked = false;

			CategoryNormalCMS.Checked = false;
			CategoryAscendingCMS.Checked = false;
			CategoryDescendingCMS.Checked = false;

			NotesNormalCMS.Checked = false;
			NotesAscendingCMS.Checked = false;
			NotesDescendingCMS.Checked = false;

			ImportantNormalCMS.Checked = false;
			ImportantAscendingCMS.Checked = false;
			ImportantDescendingCMS.Checked = false;
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
				List<String> categories = query.GetColumn(DBColumns.Category.ToString());

				GUI.AddContent add = new(names, cKey, categories)
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

					//Encrypt the decrypted vault with the new changes (Encrypted vault now has old data), so that then LoadCOntent decrypts it and loads updated data.
					crypt.Encrypt(vKey, (Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\" + (vaultpath[0] + ".db3")), encryptedVaultPath); //Encrypt changes
					File.Delete(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\" + (vaultpath[0] + ".db3")); //Delete old data

					if(isSearched)
					{
						SearchButton.PerformClick();
					}
					else
					{
						VaultContentDGV.Rows.Clear(); //Clear previous content in the list and in the table.
						LoadContent(actualOrder, actualColumn);
					}
					
				}
				
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

				}
				else if (del.deletedAllSuccess) //If valid data is for deleting all contents in the Vault.
				{
					query.DeleteAllData();
				}

				//Encrypt the decrypted vault with the new changes (Encrypted vault now has old data), so that then LoadCOntent decrypts it and loads updated data.
				crypt.Encrypt(vKey, (Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\" + (vaultpath[0] + ".db3")), encryptedVaultPath); //Encrypt changes
				File.Delete(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\" + (vaultpath[0] + ".db3")); //Delete old data

				if (isSearched)
				{
					SearchButton.PerformClick();
				}
				else
				{
					VaultContentDGV.Rows.Clear(); //Clear previous content in the list and in the table.
					LoadContent(actualOrder, actualColumn);
				}

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
				List<String> categories = query.GetColumn(DBColumns.Category.ToString());

				GUI.EditContent edit = new(names, cKey, decVault, categories)
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
					//Encrypt the decrypted vault with the new changes (Encrypted vault now has old data), so that then LoadCOntent decrypts it and loads updated data.
					crypt.Encrypt(vKey, (Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\" + (vaultpath[0] + ".db3")), encryptedVaultPath); //Encrypt changes
					File.Delete(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\" + (vaultpath[0] + ".db3")); //Delete old data

					if (isSearched)
					{
						SearchButton.PerformClick();
					}
					else
					{
						VaultContentDGV.Rows.Clear(); //Clear previous content in the list and in the table.
						LoadContent(actualOrder, actualColumn);
					}
				}

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
			GUI.HelpVaultForm help = new();
			help.ShowDialog();
		}

		private void URLNormalCMS_Click(object sender, EventArgs e)
		{
			String[] lastvalue = encryptedVaultPath.Split('\\');
			var vaultpath = lastvalue[lastvalue.Length - 1].Split('.');
			try
			{
				actualOrder = Order.Normal;
				actualColumn = DBColumns.Url;

				VaultContentDGV.Rows.Clear(); //Clear previous content in the list and in the table.
				if (isSearched)
				{
					SearchButton.PerformClick();
				}
				else
				{
					//Load the ordered content depending on column and order, and set toolstrip check property.
					LoadContent(Order.Normal, DBColumns.Url);
				}

				ResetToNormalOrdering(false);
				
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
				ResetToNormalOrdering(true);
			}
			finally
			{
				if (File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\" + (vaultpath[0] + ".db3")))
				{
					File.Delete(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\" + (vaultpath[0] + ".db3"));
				}
				
			}
		}

		private void URLAscendingCMS_Click(object sender, EventArgs e)
		{
			String[] lastvalue = encryptedVaultPath.Split('\\');
			var vaultpath = lastvalue[lastvalue.Length - 1].Split('.');
			try
			{
				actualOrder = Order.Asc;
				actualColumn = DBColumns.Url;

				VaultContentDGV.Rows.Clear(); //Clear previous content in the list and in the table.
				if (isSearched)
				{
					SearchButton.PerformClick();
				}
				else
				{
					//Load the ordered content depending on column and order, and set toolstrip check property.
					LoadContent(Order.Asc, DBColumns.Url);
				}

				UncheckOrdering();
				URLAscendingCMS.Checked = true;

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
				ResetToNormalOrdering(true);
			}
			finally
			{
				if (File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\" + (vaultpath[0] + ".db3")))
				{
					File.Delete(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\" + (vaultpath[0] + ".db3"));
				}
				
			}
		}

		private void URLDescendingCMS_Click(object sender, EventArgs e)
		{
			String[] lastvalue = encryptedVaultPath.Split('\\');
			var vaultpath = lastvalue[lastvalue.Length - 1].Split('.');
			try
			{
				actualOrder = Order.Desc;
				actualColumn = DBColumns.Url;

				VaultContentDGV.Rows.Clear(); //Clear previous content in the list and in the table.
				if (isSearched)
				{
					SearchButton.PerformClick();
				}
				else
				{
					//Load the ordered content depending on column and order, and set toolstrip check property.
					LoadContent(Order.Desc, DBColumns.Url);
				}

				UncheckOrdering();
				URLDescendingCMS.Checked = true;
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
				ResetToNormalOrdering(true);
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
				actualOrder = Order.Normal;
				actualColumn = DBColumns.Name;

				VaultContentDGV.Rows.Clear(); //Clear previous content in the list and in the table.
				if (isSearched)
				{
					SearchButton.PerformClick();
				}
				else
				{
					//Load the ordered content depending on column and order, and set toolstrip check property.
					LoadContent(Order.Normal, DBColumns.Name);
				}

				ResetToNormalOrdering(false);

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
				ResetToNormalOrdering(true);
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
				actualOrder = Order.Asc;
				actualColumn = DBColumns.Name;

				VaultContentDGV.Rows.Clear(); //Clear previous content in the list and in the table.
				if (isSearched)
				{
					SearchButton.PerformClick();
				}
				else
				{
					//Load the ordered content depending on column and order, and set toolstrip check property.
					LoadContent(Order.Asc, DBColumns.Name);
				}

				UncheckOrdering();
				NameAscendingCMS.Checked = true;

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
				ResetToNormalOrdering(true);
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
				actualOrder = Order.Desc;
				actualColumn = DBColumns.Name;

				VaultContentDGV.Rows.Clear(); //Clear previous content in the list and in the table.
				if (isSearched)
				{
					SearchButton.PerformClick();
				}
				else
				{
					//Load the ordered content depending on column and order, and set toolstrip check property.
					LoadContent(Order.Desc, DBColumns.Name);
				}

				UncheckOrdering();
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
				ResetToNormalOrdering(true);
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
				actualOrder = Order.Normal;
				actualColumn = DBColumns.Username;

				VaultContentDGV.Rows.Clear(); //Clear previous content in the list and in the table.
				if (isSearched)
				{
					SearchButton.PerformClick();
				}
				else
				{
					//Load the ordered content depending on column and order, and set toolstrip check property.
					LoadContent(Order.Normal, DBColumns.Username);
				}

				ResetToNormalOrdering(false);

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
				ResetToNormalOrdering(true);
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
				actualOrder = Order.Asc;
				actualColumn = DBColumns.Username;

				VaultContentDGV.Rows.Clear(); //Clear previous content in the list and in the table.
				if (isSearched)
				{
					SearchButton.PerformClick();
				}
				else
				{
					//Load the ordered content depending on column and order, and set toolstrip check property.
					LoadContent(Order.Asc, DBColumns.Username);
				}

				UncheckOrdering();
				UsernameAscendingCMS.Checked = true;

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
				ResetToNormalOrdering(true);
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
				actualOrder = Order.Desc;
				actualColumn = DBColumns.Username;

				VaultContentDGV.Rows.Clear(); //Clear previous content in the list and in the table.
				if (isSearched)
				{
					SearchButton.PerformClick();
				}
				else
				{
					//Load the ordered content depending on column and order, and set toolstrip check property.
					LoadContent(Order.Desc, DBColumns.Username);
				}

				UncheckOrdering();
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
				ResetToNormalOrdering(true);
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
				actualOrder = Order.Normal;
				actualColumn = DBColumns.Category;

				VaultContentDGV.Rows.Clear(); //Clear previous content in the list and in the table.
				if (isSearched)
				{
					SearchButton.PerformClick();
				}
				else
				{
					//Load the ordered content depending on column and order, and set toolstrip check property.
					LoadContent(Order.Normal, DBColumns.Category);
				}

				ResetToNormalOrdering(false);

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
				ResetToNormalOrdering(true);
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
				actualOrder = Order.Asc;
				actualColumn = DBColumns.Category;

				VaultContentDGV.Rows.Clear(); //Clear previous content in the list and in the table.
				if (isSearched)
				{
					SearchButton.PerformClick();
				}
				else
				{
					//Load the ordered content depending on column and order, and set toolstrip check property.
					LoadContent(Order.Asc, DBColumns.Category);
				}

				UncheckOrdering();
				CategoryAscendingCMS.Checked = true;

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
				ResetToNormalOrdering(true);
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
				actualOrder = Order.Desc;
				actualColumn = DBColumns.Category;

				VaultContentDGV.Rows.Clear(); //Clear previous content in the list and in the table.
				if (isSearched)
				{
					SearchButton.PerformClick();
				}
				else
				{
					//Load the ordered content depending on column and order, and set toolstrip check property.
					LoadContent(Order.Desc, DBColumns.Category);
				}

				UncheckOrdering();
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
				ResetToNormalOrdering(true);
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
				actualOrder = Order.Normal;
				actualColumn = DBColumns.Notes;

				VaultContentDGV.Rows.Clear(); //Clear previous content in the list and in the table.
				if (isSearched)
				{
					SearchButton.PerformClick();
				}
				else
				{
					//Load the ordered content depending on column and order, and set toolstrip check property.
					LoadContent(Order.Normal, DBColumns.Notes);
				}

				ResetToNormalOrdering(false);

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
				ResetToNormalOrdering(true);
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
				actualOrder = Order.Asc;
				actualColumn = DBColumns.Notes;

				VaultContentDGV.Rows.Clear(); //Clear previous content in the list and in the table.
				if (isSearched)
				{
					SearchButton.PerformClick();
				}
				else
				{
					//Load the ordered content depending on column and order, and set toolstrip check property.
					LoadContent(Order.Asc, DBColumns.Notes);
				}

				UncheckOrdering();
				NotesAscendingCMS.Checked = true;

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
				ResetToNormalOrdering(true);
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
				actualOrder = Order.Desc;
				actualColumn = DBColumns.Notes;

				VaultContentDGV.Rows.Clear(); //Clear previous content in the list and in the table.
				if (isSearched)
				{
					SearchButton.PerformClick();
				}
				else
				{
					//Load the ordered content depending on column and order, and set toolstrip check property.
					LoadContent(Order.Desc, DBColumns.Notes);
				}

				UncheckOrdering();
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
				ResetToNormalOrdering(true);
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
				actualOrder = Order.Normal;
				actualColumn = DBColumns.Important;

				VaultContentDGV.Rows.Clear(); //Clear previous content in the list and in the table.
				if (isSearched)
				{
					SearchButton.PerformClick();
				}
				else
				{
					//Load the ordered content depending on column and order, and set toolstrip check property.
					LoadContent(Order.Normal, DBColumns.Important);
				}

				ResetToNormalOrdering(false);

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
				ResetToNormalOrdering(true);
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
				actualOrder = Order.Asc;
				actualColumn = DBColumns.Important;

				VaultContentDGV.Rows.Clear(); //Clear previous content in the list and in the table.
				if (isSearched)
				{
					SearchButton.PerformClick();
				}
				else
				{
					//Load the ordered content depending on column and order, and set toolstrip check property.
					LoadContent(Order.Asc, DBColumns.Important);
				}

				UncheckOrdering();
				ImportantAscendingCMS.Checked = true;

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
				ResetToNormalOrdering(true);
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
				actualOrder = Order.Desc;
				actualColumn = DBColumns.Important;

				VaultContentDGV.Rows.Clear(); //Clear previous content in the list and in the table.
				if (isSearched)
				{
					SearchButton.PerformClick();
				}
				else
				{
					//Load the ordered content depending on column and order, and set toolstrip check property.
					LoadContent(Order.Desc, DBColumns.Important);
				}

				UncheckOrdering();
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
				ResetToNormalOrdering(true);
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

		private void UncheckAllMenuItems(ContextMenuStrip contextMenuStrip)
		{
			foreach (ToolStripItem item in contextMenuStrip.Items)
			{
				if (item is ToolStripMenuItem toolStripMenuItem)
				{
					toolStripMenuItem.Checked = false;
					//CMS dont have submenus, if they had we would need another method.
				}
			}
		}

		private void VaultContentDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
		{
			if (e.RowIndex >= 0) //Check that cell clicked is a row and not a column header....
			{
				String[] lastvalue = encryptedVaultPath.Split('\\');
				var vaultpath = lastvalue[lastvalue.Length - 1].Split('.');
				var decVault = (Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\" + (vaultpath[0] + ".db3")); //Path of decrypted Vault

				DataGridViewCell clickedCell = VaultContentDGV.Rows[e.RowIndex].Cells[e.ColumnIndex];
				switch (VaultContentDGV.Columns[e.ColumnIndex].Name)
				{
					case "DeleteRowColumn":
						var decNameToDelete = VaultContentDGV.Rows[e.RowIndex].Cells[1].Value.ToString();
						DialogResult dialog = MessageBox.Show(text: "Are you sure you want to delete the password with name '" + decNameToDelete + "' from your Vault? \n\nNote: " +
							"This action cannot be undone.", caption: "Delete password from Vault", buttons: MessageBoxButtons.YesNo, icon: MessageBoxIcon.Question);
						if (dialog == DialogResult.Yes)
						{
							try
							{
								crypt.Decrypt(vKey, encryptedVaultPath, decVault);
								query = new Query(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\" + (vaultpath[0] + ".db3"));

								var encNameToDelete = "";
								foreach (String namesInDb in query.GetColumn(DBColumns.Name.ToString())) //Decrypt names in db.
								{
									var temp = crypt.DecryptText(key: cKey, src: namesInDb);
									if (temp == decNameToDelete)
									{
										encNameToDelete = namesInDb;
										break;
									}
								}

								query.DeletePassword(encNameToDelete);

								//Encrypt the decrypted vault with the new changes (Encrypted vault now has old data), so that then LoadCOntent decrypts it and loads updated data.
								crypt.Encrypt(vKey, (Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\" + (vaultpath[0] + ".db3")), encryptedVaultPath); //Encrypt changes
								File.Delete(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\" + (vaultpath[0] + ".db3")); //Delete old data

								VaultContentDGV.Rows.Clear(); //Clear previous content in the list and in the table.
								LoadContent(actualOrder, actualColumn);
							}
							catch(Exception ex)
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

						break;
					case "PasswordColumn":
						//Get the Name(PK) of that row, to retrieve the data....
						var decNameOfPassword = VaultContentDGV.Rows[e.RowIndex].Cells[1].Value.ToString();

						try
						{
							crypt.Decrypt(vKey, encryptedVaultPath, decVault);
							query = new Query(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\" + (vaultpath[0] + ".db3"));

							var encNameToCopy = "";
							foreach (String namesInDb in query.GetColumn(DBColumns.Name.ToString())) //Decrypt names in db.
							{
								var temp = crypt.DecryptText(key: cKey, src: namesInDb);
								if (temp == decNameOfPassword)
								{
									encNameToCopy = namesInDb;
									break;
								}
							}
							Clipboard.SetText(crypt.DecryptText(key: cKey, src: query.GetPassword(name: encNameToCopy)[3]));
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
						break;
					case "URLColumn":
					case "NameColumn":
					case "SiteUsernameColumn":
					case "CategoryColumn":
					case "NotesColumn":
					case "ImportantColumn":
						// Copy the button text to clipboard
						Clipboard.SetText(clickedCell.Value.ToString());
						break;
					default:
						break;
				}
			}
		}

		private void SearchButton_Click(object sender, EventArgs e)
		{
			TrimComponents();

			//Reset content, so that you dont search content on the previously searched ocntent....
			VaultContentDGV.Rows.Clear();
			LoadContent(actualOrder, actualColumn);

			List<DataGridViewRow> matchingRows = new List<DataGridViewRow>();

			foreach (DataGridViewRow row in VaultContentDGV.Rows)
			{
				if (row.Cells["NameColumn"].Value != null)
				{
					string name = row.Cells["NameColumn"].Value.ToString();

					if (name.Contains(SearchTextbox.Text, StringComparison.OrdinalIgnoreCase))
					{
						matchingRows.Add(row);
					}
				}
			}

			// Display the matching rows in the DataGridView
			VaultContentDGV.Rows.Clear();
			VaultContentDGV.Rows.AddRange(matchingRows.ToArray());

			ResetButton.Enabled = true;
			isSearched = true;
		}

		[SupportedOSPlatform("windows")]
		private void SearchButton_MouseEnter(object sender, EventArgs e)
		{
			SearchButton.Font = new Font("Microsoft Sans Serif", 11, FontStyle.Underline); //Underline the text when mouse is in the button
		}

		[SupportedOSPlatform("windows")]
		private void SearchButton_MouseLeave(object sender, EventArgs e)
		{
			SearchButton.Font = new Font("Microsoft Sans Serif", 11, FontStyle.Regular); 
		}

		private void ResetButton_Click(object sender, EventArgs e)
		{
			VaultContentDGV.Rows.Clear();
			SearchTextbox.Text = string.Empty;
			LoadContent(actualOrder, actualColumn);

			ResetButton.Enabled = false;
			isSearched = false;

		}

		[SupportedOSPlatform("windows")]
		private void ResetButton_MouseEnter(object sender, EventArgs e)
		{
			ResetButton.Font = new Font("Microsoft Sans Serif", 11, FontStyle.Underline); //Underline the text when mouse is in the button
		}

		[SupportedOSPlatform("windows")]
		private void ResetButton_MouseLeave(object sender, EventArgs e)
		{
			ResetButton.Font = new Font("Microsoft Sans Serif", 11, FontStyle.Regular); 
		}

		private void SearchTextbox_TextChanged(object sender, EventArgs e)
		{
			if (!String.IsNullOrWhiteSpace(SearchTextbox.Text))
			{
				SearchButton.Enabled = true;
			}
			else { SearchButton.Enabled = false; }
		}

	}
}
