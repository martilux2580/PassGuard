using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PassGuard.VaultQueries
{
	/// <summary>
	/// Interface that defines the methods a class (that represents a DBMS like SQLite, H2O, Postgres...) must implement to be able to interact with a Vault.
	/// </summary>
	internal interface IQuery
	{
		public void CreateNewVault();
		public String[] GetPassword(string name);
		public List<String[]> GetAllData();
		public List<String> GetColumn(String column);
		public String[] GetDataGivenColumn(String column, String columnData);
		public void InsertData(string url, string name, string username, string password, string category, string notes, string important);
		public void DeletePassword(string name);
		public void DeleteAllData();
		public void UpdateData(string newUrl, string newName, string newUsername, string newPassword, string newCategory, string newNotes, string newImportant, string nameToBeEdited);
		public void UpdateImportance(string newImportant, string nameToBeEdited);
		public List<String[]> GetNameAndImportance();
	}
}
