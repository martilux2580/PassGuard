using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PassGuard.VaultQueries
{
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
		public void UpdateData(string newUrl, string newName, string newUsername, string newPassword, string newCategory, string newNotes, string nameToBeEdited);
	}
}
