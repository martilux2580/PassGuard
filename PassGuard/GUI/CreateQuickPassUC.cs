using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PassGuard.GUI
{
    public partial class CreateQuickPassUC : UserControl
    {
        public CreateQuickPassUC()
        {
            this.Anchor = AnchorStyles.None;
            //this.Dock = DockStyle.Fill;
            InitializeComponent();

        }
        private CheckBox[] symbolsArray { get; set; } = new CheckBox[34]; //{ get; set; }
        private CheckBox[] fillSymbolsArray(CheckBox[] SymbolsArray)
        {
            SymbolsArray[0] = CheckBoxExclamation;
            SymbolsArray[1] = CheckboxDollar;
            SymbolsArray[2] = CheckboxPercentage;
            SymbolsArray[3] = CheckboxAmpersand;
            SymbolsArray[4] = CheckboxSlash;

            SymbolsArray[5] = CheckboxBackSlash;
            SymbolsArray[6] = CheckboxOpenParenthesis;
            SymbolsArray[7] = CheckboxCloseParenthesis;
            SymbolsArray[8] = CheckboxBar;
            SymbolsArray[9] = CheckboxAt;

            SymbolsArray[10] = CheckboxHashTag;
            SymbolsArray[11] = CheckboxEuro;
            SymbolsArray[12] = CheckboxLessThan;
            SymbolsArray[13] = CheckboxHigherThan;
            SymbolsArray[14] = CheckboxOpenSquareBracket;
            
            SymbolsArray[15] = CheckboxCloseSquareBracket;
            SymbolsArray[16] = CheckboxOpenCurlyBracket;
            SymbolsArray[17] = CheckboxCloseCurlyBracket;
            SymbolsArray[18] = CheckboxAdd;
            SymbolsArray[19] = CheckboxSubstract;
            
            SymbolsArray[20] = CheckboxMultiplier;
            SymbolsArray[21] = CheckboxPeriod;
            SymbolsArray[22] = CheckboxColon;
            SymbolsArray[23] = CheckboxUnderscore;
            SymbolsArray[24] = CheckboxComma;

            SymbolsArray[25] = CheckboxSemicolon;
            SymbolsArray[26] = CheckboxLowerEne;
            SymbolsArray[27] = CheckboxHigherEne;
            SymbolsArray[28] = CheckboxOpenQuestion;
            SymbolsArray[29] = CheckboxCloseQuestion;

            SymbolsArray[30] = CheckboxEqual;
            SymbolsArray[31] = CheckboxLowerCaseTurkish;
            SymbolsArray[32] = CheckboxCapitalTurkish;
            SymbolsArray[33] = CheckboxOpenExclamation;

            return SymbolsArray;
        }

        private void CreateQuickPassUC_Load(object sender, EventArgs e)
        {
            CopyClipboardButton.Image = Image.FromFile(@"..\..\Images\Clipboard32.ico"); 
            symbolsArray = fillSymbolsArray(symbolsArray);
            
        }

        private void SymbolsCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            if (SymbolsCheckbox.Checked == false)
            {
                for (int i = 0; i < symbolsArray.Length; i++)
                {
                    symbolsArray[i].Enabled = false; //Although they are disabled, if they are checked you will do whatever in the ifs.
                }
            }

            if (SymbolsCheckbox.Checked == true)
            {
                for (int i = 0; i < symbolsArray.Length; i++)
                {
                    symbolsArray[i].Enabled = true;
                }
            }
        }

        private void CopyClipboardButton_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(PasswordTextBox.Text))
            {
                Clipboard.SetText(" ");
            }
            else
            {
                Clipboard.SetText(PasswordTextBox.Text);
            }
        }
    }
}
