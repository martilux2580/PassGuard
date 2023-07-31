using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PassGuard.PDF
{
	/// <summary>
	/// Interface that sets the methods for creating PDFs for any PDFCreator that implements it.
	/// </summary>
	internal interface IPDF
	{
		public void CreatePDF(List<String[]> results, String Vault, String Email, String sk);
		public void CreateOutlinePDF();
	}
}
