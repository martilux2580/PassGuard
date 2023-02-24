using iText.StyledXmlParser.Jsoup.Nodes;
using PassGuard.Crypto;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SQLite;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using static iText.IO.Image.Jpeg2000ImageData;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace PassGuard.VaultQueries
{
	internal class Query : IQuery
	{
		private readonly String FullDecVaultPath; //Decrypted path

		internal Query(String decVaultPath)
		{
			FullDecVaultPath= decVaultPath;

		}

		private void Execute(string query, List<String> parameters) //Execute queries that dont return rows.
		{
			using (TransactionScope tran = new()) //Just in case, atomic procedure....
			{
				SQLiteConnection m_dbConnection = new("Data Source = " + FullDecVaultPath);
				m_dbConnection.Open(); //If first time, this models file as a vault, also opens a connection to it.

				//Create space for data.
				SQLiteCommand command = new(query, m_dbConnection); //Associate request with connection to vault.

				if (parameters != null && parameters.Count > 0)
				{
					command.Prepare();
					for(int i = 0; i< parameters.Count; i++)
					{
						command.Parameters.Add("@var"+i.ToString(), DbType.String).Value = parameters[i];
					}
					
				}

				command.ExecuteNonQuery(); //Execute request.
				command.Dispose(); //Delete object so it is no longer using the file.

				//Indicates that creating the SQLiteDatabase went succesfully, so the database can be committed.
				tran.Complete(); //Close and commit transaction.
				tran.Dispose(); //Dispose transaction so it is no longer using the file.

				m_dbConnection.Close(); //Close connection to vault.
				m_dbConnection.Dispose();
				m_dbConnection = null;

			}
		}
			
		private List<String[]> Retrieve(string query, List<String> parameters) //Retrieve rows from a query.
		{
			List<String[]> result = new();
			using (TransactionScope tran = new()) //Just in case, atomic procedure....
			using (SQLiteConnection m_dbConnection = new("Data Source = " + FullDecVaultPath))
			{
				using (SQLiteCommand commandExec = new(query, m_dbConnection)) //Associate request with connection to vault.)
				{
					m_dbConnection.Open(); //If first time, this models file as a vault, also opens a connection to it.

					if (parameters != null && parameters.Count > 0)
					{
						commandExec.Prepare();
						for (int i = 0; i < parameters.Count; i++)
						{
							commandExec.Parameters.Add("@var" + i.ToString(), DbType.String).Value = parameters[i];
						}

					}

					commandExec.ExecuteNonQuery(); //Execute request.
					using (SQLiteDataReader reader = commandExec.ExecuteReader())//Object Reader.
					{
						if (reader.Read())
						{
							result.Add(new string[6] { reader.GetString(0), reader.GetString(1), reader.GetString(2), reader.GetString(3), reader.GetString(4), reader.GetString(5) });
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

			return result;
		}

		public void CreateNewVault()
		{
			string query = "CREATE TABLE Vault (Url TEXT, Name TEXT PRIMARY KEY NOT NULL UNIQUE, Username TEXT NOT NULL, SitePassword TEXT NOT NULL, Category TEXT, Notes TEXT);";
			
			Execute(query, parameters: null);
		}

		public String[] GetPassword(string name) //Get all data given a Name
		{
			string query = "SELECT * FROM Vault WHERE Name = @var0;";

			var partialResult = Retrieve(query, new List<string> { name });
			if (partialResult.Count > 0)
			{
				return partialResult[0];
			}
			else
			{ 
				return null; 
			}

		}
		public List<String[]> GetAllData()
		{
			string query = "SELECT * FROM Vault;";

			return Retrieve(query, null);
		}

		public List<String> GetColumn(String column)
		{
			string query = "SELECT @var0 FROM Vault;";

			List<String[]> partialResult = Retrieve(query, new List<string> { column });
			List<String> result = new();

			for(int i = 0; i<partialResult.Count; i++)
			{
				result.Append(partialResult[i][0]);
			}

			return result;
		}

		public String[] GetDataGivenColumn(String column, String columnData)
		{
			string query = "SELECT * FROM Vault WHERE @var0 = @var1;";

			var partialResult = Retrieve(query, new List<string> { column, columnData });
			if (partialResult.Count > 0)
			{
				return partialResult[0];
			}
			else
			{
				return null;
			}
		}

		public void InsertData(string url, string name, string username, string password, string category, string notes)
		{
			string query = "INSERT INTO Vault (Url, Name, Username, SitePassword, Category, Notes) values (@var0, @var1, @var2, @var3, @var4, @var5);";

			Execute(query, parameters: new List<string> { url, name, username, password, category, notes });
		}

		public void DeletePassword(string name)
		{
			string query = "DELETE FROM Vault WHERE Name = @var0;"; ;

			Execute(query, parameters: new List<string> { name });
		}

		public void DeleteAllData()
		{
			string query = "DELETE FROM Vault";

			Execute(query, parameters: null);
		}

		public void UpdateData(string newUrl, string newName, string newUsername, string newPassword, string newCategory, string newNotes, string nameToBeEdited)
		{
			string query = "UPDATE Vault SET Url = @var0, Name = @var1, Username = @var2, SitePassword = @var3, Category = @var4, Notes = @var5 WHERE Name = var6;";

			Execute(query, parameters: new List<string> { newUrl,newName, newUsername, newPassword, newCategory, newNotes, nameToBeEdited });
		}
	}
}
