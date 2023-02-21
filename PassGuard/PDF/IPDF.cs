using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PassGuard.PDF
{
    internal interface IPDF
    {
        public void CreatePDF(List<String[]> results, String Vault, String Email, String sk);
        public void CreateOutlinePDF();
    }
}
