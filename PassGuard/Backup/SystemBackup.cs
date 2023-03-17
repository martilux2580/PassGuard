using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PassGuard.Backup
{
	internal static class SystemBackup
	{
		//Create a backup located in srcPath in dstPath
		internal static bool CreateBackup(String srcPath, String dstPath)
		{
			var tempSplit = srcPath.Split('\\');
			var fileName = tempSplit[tempSplit.Length - 1].Split('.');
			var nameOfBackup = "Backup" + char.ToUpper(fileName[0][0]) + fileName[0].Substring(1) + DateTime.Now.ToString("-yyyyMMdd-HHmmss") + "." + fileName[1];

			if (!File.Exists(dstPath + "\\" + nameOfBackup))
			{
				try
				{
					File.Copy(sourceFileName: srcPath, destFileName: dstPath + "\\" + nameOfBackup);
					return true;
				}
				catch (Exception)
				{
					return false;
				}
			}
			else
			{
				return false;
			}
		}

		//Infinite method for checking AutoBackup if its range is every day/week/month
		//If enough time have passed since lastDate of backup, create a backup. Keep checking everytime if it is time to create a backup.
		internal static void AutoBackupTime()
		{
			while (true)
			{
				var mode = ConfigurationManager.AppSettings["FrequencyAutoBackup"];
				var pathVault = ConfigurationManager.AppSettings["PathVaultForAutoBackup"];
				var dstPath = ConfigurationManager.AppSettings["dstBackupPathForSave"];
				var lastDate = ConfigurationManager.AppSettings["LastDateAutoBackup"];
				var active = ConfigurationManager.AppSettings["AutoBackupState"];
				if (active == "true")
				{
					switch (Int32.Parse(mode))
					{
						case 3:
							if (DateTime.Now.Subtract(DateTime.Parse(lastDate)).TotalDays >= 1) //Difference between last backup and now is +1day
							{
								if (File.Exists(pathVault) && Directory.Exists(dstPath))
								{
									if (CreateBackup(pathVault, dstPath: dstPath))
									{
										Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
										config.AppSettings.Settings["LastDateAutoBackup"].Value = DateTime.Now.ToString(); //Modify data in the config file for future executions.
										config.Save(ConfigurationSaveMode.Modified, true);
										ConfigurationManager.RefreshSection("appSettings");
										MessageBox.Show(text: "AutoBackup was created successfully.", caption: "Success", buttons: MessageBoxButtons.OK, icon: MessageBoxIcon.Information);
									}
									else
									{
										MessageBox.Show(text: "AutoBackup could not make a backup of the specified Vault, please try again later. \nThis message will be shown every 30 seconds until the issue is solved or AutoBackup is deactivated.", caption: "Error", buttons: MessageBoxButtons.OK, icon: MessageBoxIcon.Error);
										Thread.Sleep(30000);
									}
								}
								else
								{
									MessageBox.Show(text: "AutoBackup could not make a backup of the specified Vault. Please review AutoBackup config and check all the paths and files exist. \nThis message will be shown everytime AutoBackup tries to make a backup until the issue is solved or AutoBackup is deactivated.", caption: "Error", buttons: MessageBoxButtons.OK, icon: MessageBoxIcon.Error);
									Thread.Sleep(60000);
								}
							}
							break;
						case 4:
							if (DateTime.Now.Subtract(DateTime.Parse(lastDate)).TotalDays >= 7) //Difference between last backup and now is +1week
							{
								if (File.Exists(pathVault) && Directory.Exists(dstPath))
								{
									if (CreateBackup(pathVault, dstPath: dstPath))
									{
										Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
										config.AppSettings.Settings["LastDateAutoBackup"].Value = DateTime.Now.ToString(); //Modify data in the config file for future executions.
										config.Save(ConfigurationSaveMode.Modified, true);
										ConfigurationManager.RefreshSection("appSettings");
										MessageBox.Show(text: "AutoBackup was created successfully.", caption: "Success", buttons: MessageBoxButtons.OK, icon: MessageBoxIcon.Information);
									}
									else
									{
										Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
										config.AppSettings.Settings["LastDateAutoBackup"].Value = DateTime.Now.ToString(); //Modify data in the config file for future executions.
										config.Save(ConfigurationSaveMode.Modified, true);
										ConfigurationManager.RefreshSection("appSettings");
										MessageBox.Show(text: "AutoBackup could not make a backup of the specified Vault, please try again later. \nThis message will be shown every 30 seconds until the issue is solved or AutoBackup is deactivated.", caption: "Error", buttons: MessageBoxButtons.OK, icon: MessageBoxIcon.Error);
										Thread.Sleep(30000);
									}
								}
								else
								{
									MessageBox.Show(text: "AutoBackup could not make a backup of the specified Vault. Please review AutoBackup config and check all the paths and files exist. \nThis message will be shown everytime AutoBackup tries to make a backup until the issue is solved or AutoBackup is deactivated.", caption: "Error", buttons: MessageBoxButtons.OK, icon: MessageBoxIcon.Error);
									Thread.Sleep(60000);
								}
							}
							break;
						case 5:
							if (DateTime.Now.Subtract(DateTime.Parse(lastDate)).TotalDays >= 30) //Difference between last backup and now is +1month
							{
								if (File.Exists(pathVault) && Directory.Exists(dstPath))
								{
									if (CreateBackup(pathVault, dstPath: dstPath))
									{
										Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
										config.AppSettings.Settings["LastDateAutoBackup"].Value = DateTime.Now.ToString(); //Modify data in the config file for future executions.
										config.Save(ConfigurationSaveMode.Modified, true);
										ConfigurationManager.RefreshSection("appSettings");
										MessageBox.Show(text: "AutoBackup was created successfully.", caption: "Success", buttons: MessageBoxButtons.OK, icon: MessageBoxIcon.Information);
									}
									else
									{
										MessageBox.Show(text: "AutoBackup could not make a backup of the specified Vault, please try again later. \nThis message will be shown every 30 seconds until the issue is solved or AutoBackup is deactivated.", caption: "Error", buttons: MessageBoxButtons.OK, icon: MessageBoxIcon.Error);
										Thread.Sleep(30000);
									}
								}
								else
								{
									MessageBox.Show(text: "AutoBackup could not make a backup of the specified Vault. Please review AutoBackup config and check all the paths and files exist. \nThis message will be shown everytime AutoBackup tries to make a backup until the issue is solved or AutoBackup is deactivated.", caption: "Error", buttons: MessageBoxButtons.OK, icon: MessageBoxIcon.Error);
									Thread.Sleep(60000);
								}
							}
							break;
						default:
							break;
					}
				}
				else if (active == "false") //Signal to stop this method in the Task
				{
					break;
				}
			}
		}
	}
}
