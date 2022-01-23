using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Diagnostics;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.IO;
using System.Net.Http;
using System.Reflection;

namespace PassGuard.GUI
{
    //UserControl for the Create Quick Password Menu
    public partial class CreateQuickPassUC : UserControl
    {
        public CreateQuickPassUC()
        {
            this.Anchor = AnchorStyles.None;
            //this.Dock = DockStyle.Fill;
            InitializeComponent();

        }

        
        private Dictionary<CheckBox, string> characters { get; set; } = new Dictionary<CheckBox, string>(); //No duplicates
        private Dictionary<CheckBox, string> symbols { get; set; } = new Dictionary<CheckBox, string>(); //No duplicates

        private Dictionary<CheckBox, string> fillCharDict (Dictionary<CheckBox, string> characters)
        {   //Fill the dictionary with corresponding pair CheckBox(characters)-String
            const string lower = "abcdefghijklmnopqrstuvwxyz";
            const string upper = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            const string numbers = "0123456789";

            characters.Add(UpperCheckbox, upper);
            characters.Add(LowerCheckbox, lower);
            characters.Add(NumbersCheckbox, numbers);

            return characters;
        }

        private Dictionary<CheckBox, string> fillSymbolsDict(Dictionary<CheckBox, string> symbols)
        {   //Fill the dictionary with corresponding pair CheckBox(symbols)-String
            symbols.Add(CheckBoxExclamation, "!");
            symbols.Add(CheckboxDollar, "$");
            symbols.Add(CheckboxPercentage, "%");
            symbols.Add(CheckboxAmpersand, "&");
            symbols.Add(CheckboxSlash, "/");

            symbols.Add(CheckboxBackSlash, "\\");
            symbols.Add(CheckboxOpenParenthesis, "(");
            symbols.Add(CheckboxCloseParenthesis, ")");
            symbols.Add(CheckboxBar, "|");
            symbols.Add(CheckboxAt, "@");

            symbols.Add(CheckboxHashTag, "#");
            symbols.Add(CheckboxEuro, "€");
            symbols.Add(CheckboxLessThan, "<");
            symbols.Add(CheckboxHigherThan, ">");
            symbols.Add(CheckboxOpenSquareBracket, "[");

            symbols.Add(CheckboxCloseSquareBracket, "]");
            symbols.Add(CheckboxOpenCurlyBracket, "{");
            symbols.Add(CheckboxCloseCurlyBracket, "}");
            symbols.Add(CheckboxAdd, "+");
            symbols.Add(CheckboxSubstract, "-");

            symbols.Add(CheckboxMultiplier, "*");
            symbols.Add(CheckboxPeriod, ".");
            symbols.Add(CheckboxColon, ":");
            symbols.Add(CheckboxUnderscore, "_");
            symbols.Add(CheckboxComma, ",");

            symbols.Add(CheckboxSemicolon, ";");
            symbols.Add(CheckboxLowerEne, "ñ");
            symbols.Add(CheckboxHigherEne, "Ñ");
            symbols.Add(CheckboxOpenQuestion, "¿");
            symbols.Add(CheckboxCloseQuestion, "?");

            symbols.Add(CheckboxEqual, "=");
            symbols.Add(CheckboxLowerCaseTurkish, "ç");
            symbols.Add(CheckboxCapitalTurkish, "Ç");
            symbols.Add(CheckboxOpenExclamation, "¡");

            return symbols;
        }

        private void CreateQuickPassUC_Load(object sender, EventArgs e)
        {
            //Load Images
            CopyClipboardButton.Image = Image.FromFile(@"..\..\Images\Clipboard32.ico");//Load Clipboard Icon
            InfoPwnageButton.Image = Image.FromFile(@"..\..\Images\Info243.ico");//Load Info Icon
            //Load Dictionaries with their corresponding values.
            characters = fillCharDict(characters);
            symbols = fillSymbolsDict(symbols);
            SymbolsTooltip.SetToolTip(SymbolsCheckbox, "If this option and at least one or more symbols are checked, only one or more \nof these symbols (at least one, it is unlikely that all symbols will be displayed) \nwill be displayed in the password.");
            ClipboardToolTip.SetToolTip(CheckPwnageCheckbox, "Recommended as it implies more secure passwords, however it consumes \nmuch more time when number of passwords requested grows.");
        
        
        }

        private void SymbolsCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            if (SymbolsCheckbox.Checked == false)//If it has been deactivated...
            {
                foreach(KeyValuePair<CheckBox, string> pair in symbols)//Disable all checkboxes and uncheck them.
                {
                    pair.Key.Enabled = false; //Although they are disabled, if they are checked you will do whatever in the ifs.
                    pair.Key.Checked = false;
                }
                SelectAllSymbolsButton.Enabled = false;
                SelectAllSymbolsButton.Text = "Select All Symbols";
            }

            if (SymbolsCheckbox.Checked == true)//If it has been activated...
            {
                foreach (KeyValuePair<CheckBox, string> pair in symbols)//Enable all symbol checkboxes.
                {
                    pair.Key.Enabled = true;
                }
                SelectAllSymbolsButton.Enabled = true;
            }

        }

        private void CopyClipboardButton_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(PasswordTextBox.Text))//If nothing in Textbox, we will copy a whitespace as "" cannot be copied.
            {
                Clipboard.SetText(" ");
            }
            else//If it has text in it, copy it.
            {
                Clipboard.SetText(PasswordTextBox.Text);
            }
        }

        private async void GenPassButton_Click(object sender, EventArgs e)
        {
            //Clear previous content + Add Progress
            PercentageLabel.Text = "0% complete, 0/" + NPasswordsNUD.Value.ToString() +" passwords generated.";
            PercentageLabel.Refresh();
            PasswordTextBox.Clear();

            //Create a string with the valid characters
            StringBuilder sb = new StringBuilder();

            int countLeftCharacters = 0;  //We will use this to later check if we have to include some of the leftCheckBoxes characters (letters + numbers).
            foreach (KeyValuePair<CheckBox, string> pair in characters)//Check if any char checkbox is activated.
            {
                if (pair.Key.Checked == true)
                {
                    sb.Append(pair.Value);
                    countLeftCharacters++;
                }
            }

            int countRightCharacters = 0;  //We will use this to later check if we have to include some of the rightCheckBoxes characters (symbols).
            if (SymbolsCheckbox.Checked == true)//Check symbols only if primary checkbox is enabled.
            {
                
                foreach (KeyValuePair<CheckBox, string> pair in symbols)//Check if any char checkbox is activated.
                {
                    if (pair.Key.Checked == true)
                    {
                        sb.Append(pair.Value);
                        countRightCharacters++;
                    }
                }
            }

            string validCharacters = sb.ToString(); //Got the characters into a string to compose the password

            //Test if validCharacters is null (no chars were selected).
            if (String.IsNullOrEmpty(validCharacters)) //Maybe no chars were selected.
            {
                MessageBox.Show("Cannot generate passwords without characters. \n\nPlease select at least one of the available characters or symbols.");
            }
            else //We have valid characters to work with (at least one) and the password length is OK.
            {
                //Check if user wants unique and safe passwords.
                if (CheckPwnageCheckbox.Checked == true) //Checkbox pwn true
                {
                    int validCount = 0; //Many random passwords will be generated but only the ones that contain all the characters requested will be valid.
                    int totalCount = 0;
                    bool missingChar;
                    while (validCount != NPasswordsNUD.Value) //Until Real count of valid generated passwords does match nPasswords
                    {
                        if (totalCount >= ((512 * 2) + 1)) //Iterations overflown
                        {
                            string message = "Many iterations of the process have been completed and not all passwords with the required conditions have been generated. Do you want to interrupt the password generation or continue with more iterations until possibly completing the password generation? \nClick Yes to continue with some more iterations. \nClick No to interrupt the current password generation.";
                            MessageBoxButtons buttons = MessageBoxButtons.YesNo;
                            string title = "Important";
                            DialogResult result = MessageBox.Show(message, title, buttons, MessageBoxIcon.Warning);

                            if (result == DialogResult.Yes)
                            {
                                //Continue Exec
                                totalCount = 0;
                            }
                            else if (result == DialogResult.No)
                            {
                                //Interrupt
                                break;
                            }
                        }

                        //Save generated pass temporarily
                        string genPass = GenerateSecurePassword((int)PassLengthNUD.Value, validCharacters);

                        //Check that genPass has all requested chars (CryptoProvider provides secure random, does not guarantee all chars of validCharacters are used).
                        missingChar = false; //If chars are missing, we won´t enter the block of code of generating not pwned passwords.

                        foreach (KeyValuePair<CheckBox, string> pair in characters)//Check if any char checkbox is activated.
                        {
                            if (pair.Key.Checked == true)
                            {
                                for (int i = 0; i < genPass.Length; i++)
                                {
                                    if (pair.Value.Contains(genPass[i]))//If it contains the character, it is not missing and we exit the foreach
                                    {
                                        missingChar = false;
                                        break;
                                    }
                                    else//If it does not contain it, it is technically missing. We will check rest of chars in genPass.
                                    {
                                        missingChar = true;
                                    }
                                }
                                if (missingChar) //If you exit previous for loop (which means character is checked) and missingChar is true, it is missing it definitively.
                                {
                                    break;
                                }
                            }
                        }
                        if (SymbolsCheckbox.Checked == true)
                        {
                            if (!missingChar)
                            {
                                foreach (KeyValuePair<CheckBox, string> pair in symbols)//Check if any char checkbox is activated.
                                {
                                    if (pair.Key.Checked == true)
                                    {
                                        if (genPass.Contains(pair.Value))
                                        {
                                            missingChar = false;
                                            break;
                                        }
                                        else
                                        {
                                            missingChar = true;
                                        }
                                    }
                                }
                            }
                        }
                        if (!missingChar) //If genPass has ALL characters requested 
                        {
                            bool check = await CheckPwnage(genPass);
                            if (check == false) //No pwnage found for genPass
                            {
                                validCount += 1; //A valid pass has been created, so we increment it
                                //Set Progress Label´s progress
                                decimal proportion = Decimal.Round((validCount / NPasswordsNUD.Value) * 100, 2);
                                PercentageLabel.Text = proportion.ToString() + "% complete, " + validCount.ToString() + "/" + NPasswordsNUD.Value.ToString() + " passwords generated.";
                                PasswordTextBox.Text += genPass + Environment.NewLine; //Add pass to textbox
                                PasswordTextBox.Refresh();
                                PercentageLabel.Refresh();
                            }
                        }
                        
                        totalCount++; //A cycle/iteration has been completed.
                    }
                    
                }
                else //Checkbox pwn false
                {
                    int validCount = 0; //Many random passwords will be generated but only the ones that contain all the characters requested will be valid.
                    bool missingChar; //Will be true if a requested char is missing
                    int totalCount = 0;
                    while (validCount != NPasswordsNUD.Value) //Until Real count of valid generated passwords does match nPasswords requested
                    {
                        if (totalCount >= ((512 * 2) + 1)) //Iterations overflown
                        {
                            string message = "Many iterations of the process have been completed and not all passwords with the required conditions have been generated. Do you want to interrupt the password generation or continue with more iterations until possibly completing the password generation? \nClick Yes to continue with some more iterations. \nClick No to interrupt the current password generation.";
                            MessageBoxButtons buttons = MessageBoxButtons.YesNo;
                            string title = "Important";
                            DialogResult result = MessageBox.Show(message, title, buttons, MessageBoxIcon.Warning);

                            if (result == DialogResult.Yes)
                            {
                                //Continue Exec
                                totalCount = 0;
                            }
                            else if (result == DialogResult.No)
                            {
                                //Interrupt
                                break;
                            }
                        }
                        
                        string genPass = GenerateSecurePassword((int)PassLengthNUD.Value, validCharacters); //Generate a secure password
                        missingChar = false;

                        //Equal Part as above
                        foreach (KeyValuePair<CheckBox, string> pair in characters)//Check if any char checkbox is activated.
                        {
                            if (pair.Key.Checked == true)
                            {
                                for (int i = 0; i < genPass.Length; i++)
                                {
                                    if (pair.Value.Contains(genPass[i]))//If it contains the character, it is not missing and we exit the foreach
                                    {
                                        missingChar = false;
                                        break;
                                    }
                                    else//If it does not contain it, it is technically missing. We will check rest of chars in genPass.
                                    {
                                        missingChar = true;
                                    }
                                }
                                if (missingChar) //If you exit previous for loop (which means character is checked) and missingChar is true, it is missing it definitively.
                                {
                                    break;
                                }
                            }
                        }


                        if (SymbolsCheckbox.Checked == true)
                        {
                            if (!missingChar)
                            {
                                foreach (KeyValuePair<CheckBox, string> pair in symbols)//Check if any char checkbox is activated.
                                {
                                    if (pair.Key.Checked == true)
                                    {
                                        if (genPass.Contains(pair.Value))
                                        {
                                            missingChar = false;
                                            break;
                                        }
                                        else
                                        {
                                            missingChar = true;
                                        }
                                    }
                                }
                            }
                        }
                        //End Equal Part as above
                        if (!missingChar)
                        {
                            PasswordTextBox.Text += genPass + Environment.NewLine;
                            validCount++;
                            decimal proportion = Decimal.Round(((validCount) / NPasswordsNUD.Value) * 100, 2);
                            PercentageLabel.Text = proportion.ToString() + "% complete, " + (validCount).ToString() + "/" + NPasswordsNUD.Value.ToString() + " passwords generated.";
                            PasswordTextBox.Refresh();
                            PercentageLabel.Refresh();
                        }
                        totalCount++;
                        
                    }
                }
            }
        }

        private string ComputeSHA1 (string password)
        {
            SHA1 sha1 = new SHA1CryptoServiceProvider();
            byte[] passByte = Encoding.UTF8.GetBytes(password);//Encode Pass
            byte[] passHash = sha1.ComputeHash(passByte);//Compute SHA1

            //Convert Hash into readable string
            StringBuilder sb = new StringBuilder();
            foreach (byte b in passHash)
            {
                sb.Append(b.ToString("x2"));
            }
            string hash = sb.ToString();

            return hash;
        }

        private async Task<string> getHashes(string headHash)
        {
            string instruction = "https://api.pwnedpasswords.com/range/";
            string url = instruction + headHash.ToUpper();
            HttpClient client = new HttpClient();
            string response = await client.GetStringAsync(url);

            return response;
        }

        private async Task<bool> CheckPwnage (string password)//Password pwned before? -> returns true, Password not pwned before? -> returns false
        {
            string hash = ComputeSHA1(password); //Compute SHA1
            string headhash = hash.Substring(0, 5); //Compute first part of hash in order to check hashes.
            string PwnedHashes = await getHashes(headhash);

            StringReader reader = new StringReader(PwnedHashes);
            string line, tailHash;
            while ((line = reader.ReadLine()) != null)//Read all pwned hashes
            {
                tailHash = line.Substring(0, 35);
                if (headhash.ToUpper() + tailHash.ToUpper() == hash.ToUpper()) //If match, pass has been pwned before.
                {
                    return true;
                }
            }
            return false;
        }

        private static string GenerateSecurePassword(int length, string validCharacters) //StackOverflow xD
        {
            StringBuilder res = new StringBuilder();
            using (RNGCryptoServiceProvider cryptoProvider = new RNGCryptoServiceProvider())
            {
                while (res.Length != length)
                {
                    byte[] oneByte = new byte[1];
                    cryptoProvider.GetBytes(oneByte);
                    char character = (char)oneByte[0];
                    if (validCharacters.Contains(character))
                    {
                        res.Append(character);
                    }
                }
            }

            return res.ToString();
        }


        private void InfoPwnageButton_Click(object sender, EventArgs e)
        {
            MessageBox.Show("If this option is checked, for every generated password we will check if it has been pwned by inserting it into the " +
                "webpage https://haveibeenpwned.com/Passwords. \nThis website will check in their database if this password has been cracked already. " +
                "\nFor more info about why should you write down your passwords in this webpage and how is the privacy of searched passwords being kept, " + 
                "visit https://www.troyhunt.com/ive-just-launched-pwned-passwords-version-2/#cloudflareprivacyandkanonymity." + "\n\nNote: Press Ctrl+C to copy " + 
                "the whole content of this dialog.");
        }

        private void SelectAllSymbolsButton_MouseEnter(object sender, EventArgs e)
        {
            SelectAllSymbolsButton.Font = new Font("Microsoft Sans Serif", 10, FontStyle.Underline);//If mouse over button, underline text
        }

        private void SelectAllSymbolsButton_MouseLeave(object sender, EventArgs e)
        {
            SelectAllSymbolsButton.Font = new Font("Microsoft Sans Serif", 10, FontStyle.Regular); //If mouse leaves button, regular text
        }

        private void SelectAllSymbolsButton_Click(object sender, EventArgs e)
        {
            if (SymbolsCheckbox.Checked == true)
            {
                if (SelectAllSymbolsButton.Text == "Select All Symbols") //If true check all
                {
                    foreach (CheckBox box in symbols.Keys)
                    {
                        box.Checked = true;
                    }
                    SelectAllSymbolsButton.Text = "Unselect All Symbols";
                    
                }
                else if (SelectAllSymbolsButton.Text == "Unselect All Symbols") //If not true, uncheck all.
                {
                    foreach (CheckBox box in symbols.Keys)
                    {
                        box.Checked = false;
                    }
                    SelectAllSymbolsButton.Text = "Select All Symbols";
                    SymbolsCheckbox.Checked = false;
                }
            }
        }
    }
}
