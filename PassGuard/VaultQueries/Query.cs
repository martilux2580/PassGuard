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
						command.Parameters.Add("@param"+i.ToString(), DbType.String).Value = parameters[i];
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
			
		private List<object[]> Retrieve(string query, List<String> parameters) //Retrieve rows from a query.
		{
			List<object[]> result = new();
			using (TransactionScope tran = new()) //Just in case, atomic procedure....
			using (SQLiteConnection m_dbConnection = new("Data Source = " + FullDecVaultPath))
			{
				m_dbConnection.Open(); //If first time, this models file as a vault, also opens a connection to it.
				SQLiteCommand commandExec = new(query, m_dbConnection); //Associate request with connection to vault.)
				
				if (parameters != null && parameters.Count > 0)
				{
					commandExec.Prepare();
					for (int i = 0; i < parameters.Count; i++)
					{
						commandExec.Parameters.Add("@param" + i.ToString(), DbType.String).Value = parameters[i];
					}

				}

				commandExec.ExecuteNonQuery();
				using (SQLiteDataReader reader = commandExec.ExecuteReader())//Object Reader.
				{
					while (reader.Read())
					{
						var values = new object[reader.FieldCount];
						reader.GetValues(values);
						result.Add(values);
					}
				}

				commandExec.ExecuteNonQuery(); //Execute request.
				commandExec.Dispose(); //Delete object so it is no longer using the file.

				//Indicates that creating the SQLiteDatabase went succesfully, so the database can be committed.
				tran.Complete(); //Close and commit transaction.
				tran.Dispose(); //Dispose transaction so it is no longer using the file.

				m_dbConnection.Close(); //Close connection to vault.
				m_dbConnection.Dispose();

			}
			return result;
		}

		public void CreateNewVault()
		{
			string query = "CREATE TABLE Vault (Url TEXT, Name TEXT PRIMARY KEY NOT NULL UNIQUE, Username TEXT NOT NULL, SitePassword TEXT NOT NULL, Category TEXT, Notes TEXT, Important TEXT);";
			
			Execute(query, parameters: null);
		}

		public String[] GetPassword(string name) //Get all data given a Name
		{
			string query = "SELECT * FROM Vault WHERE Name = @param0;";

			var partialResult = Retrieve(query, new List<string> { name });
			if (partialResult.Count > 0)
			{
				return Array.ConvertAll(partialResult[0], x => x.ToString());
			}
			else
			{ 
				return null; 
			}

		}
		public List<String[]> GetAllData()
		{
			string query = "SELECT * FROM Vault;";

			var partialResult = Retrieve(query, null);
			var result = new List<String[]>();
			foreach(var item in partialResult)
			{
				result.Add(Array.ConvertAll(item, x => x.ToString()));
			}

			return result;
		}

		public List<String> GetColumn(String column)
		{
			string query = $"SELECT {column} FROM Vault;"; //Prevent SQLINjections, control where is this method being called (as column)...

			var partialResult = Retrieve(query, new List<string> {column});
			List<String> result = new();

			for(int i = 0; i<partialResult.Count; i++)
			{
				result.Add(partialResult[i][0].ToString());
			}

			return result;
		}

		public String[] GetDataGivenColumn(String column, String columnData)
		{
			string query = $"SELECT * FROM Vault WHERE {column} = @param0;";

			var partialResult = Retrieve(query, new List<string> { columnData });
			if (partialResult.Count > 0)
			{
				return Array.ConvertAll(partialResult[0], x => x.ToString());
			}
			else
			{
				return null;
			}
		}

		public void InsertData(string url, string name, string username, string password, string category, string notes, string important)
		{
			string query = "INSERT INTO Vault (Url, Name, Username, SitePassword, Category, Notes, Important) values (@param0, @param1, @param2, @param3, @param4, @param5, @param6);";

			Execute(query, parameters: new List<string> { url, name, username, password, category, notes, important });
		}

		public void DeletePassword(string name)
		{
			string query = "DELETE FROM Vault WHERE Name = @param0;"; ;

			Execute(query, parameters: new List<string> { name });
		}

		public void DeleteAllData()
		{
			string query = "DELETE FROM Vault";

			Execute(query, parameters: null);
		}

		public void UpdateData(string newUrl, string newName, string newUsername, string newPassword, string newCategory, string newNotes, string newImportant, string nameToBeEdited)
		{
			string query = "UPDATE Vault SET Url = @param0, Name = @param1, Username = @param2, SitePassword = @param3, Category = @param4, Notes = @param5, Important = @param6 WHERE Name = @param7;";

			Execute(query, parameters: new List<string> { newUrl,newName, newUsername, newPassword, newCategory, newNotes, newImportant, nameToBeEdited });
		}

		public List<String[]> GetNameAndImportance()
		{
			string query = "SELECT Name, Important FROM Vault;";

			var partialResult = Retrieve(query, null);
			var result = new List<String[]>();
			foreach (var item in partialResult)
			{
				result.Add(Array.ConvertAll(item, x => x.ToString()));
			}

			return result;
		}
	}
}
