﻿using System;
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
        Dictionary<CheckBox, string> characters { get; set; } = new Dictionary<CheckBox, string>(); //No duplicates
        Dictionary<CheckBox, string> symbols { get; set; } = new Dictionary<CheckBox, string>(); //No duplicates
        private Dictionary<CheckBox, string> fillCharDict (Dictionary<CheckBox, string> characters)
        {
            const string lower = "abcdefghijklmnopqrstuvwxyz";
            const string upper = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            const string numbers = "0123456789";

            characters.Add(UpperCheckbox, upper);
            characters.Add(LowerCheckbox, lower);
            characters.Add(NumbersCheckbox, numbers);

            return characters;
        }

        private Dictionary<CheckBox, string> fillSymbolsDict(Dictionary<CheckBox, string> symbols)
        {
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
            CopyClipboardButton.Image = Image.FromFile(@"..\..\Images\Clipboard32.ico");
            InfoPwnageButton.Image = Image.FromFile(@"..\..\Images\Info24.ico");
            characters = fillCharDict(characters);
            symbols = fillSymbolsDict(symbols);

        }

        private void SymbolsCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            if (SymbolsCheckbox.Checked == false)
            {
                foreach(KeyValuePair<CheckBox, string> pair in symbols)
                {
                    pair.Key.Enabled = false; //Although they are disabled, if they are checked you will do whatever in the ifs.
                }
            }

            if (SymbolsCheckbox.Checked == true)
            {
                foreach (KeyValuePair<CheckBox, string> pair in symbols)
                {
                    pair.Key.Enabled = true;
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

        private void GenPassButton_Click(object sender, EventArgs e)
        {
            //Clear previous content.
            PasswordTextBox.Clear();
            StringBuilder sb = new StringBuilder();

            //Create a string with the valid characters
            foreach (KeyValuePair<CheckBox, string> pair in characters)//Check if any char checkbox is activated.
            {
                if (pair.Key.Checked == true)
                {
                    sb.Append(pair.Value);
                }
            }

            if (SymbolsCheckbox.Checked == true)//Check symbols only if primary checkbox is enabled.
            {
                foreach (KeyValuePair<CheckBox, string> pair in symbols)//Check if any char checkbox is activated.
                {
                    if (pair.Key.Checked == true)
                    {
                        sb.Append(pair.Value);
                    }
                }
            }

            string validCharacters = sb.ToString(); //Got the characters to compose the password

            //Test if validCharacters is null (no chars were selected).
            if (String.IsNullOrEmpty(validCharacters)) //Maybe no chars were selected.
            {
                MessageBox.Show("Cannot generate passwords without characters. \n\nPlease select at least one of the available characters or symbols.");
            }
            else //We have valid characters to work with (at least one)
            {
                //Check if user wants unique and safe passwords.
                if (CheckPwnageCheckbox.Checked == true) //Checkbox pwn true
                {
                    int validCount = 0;

                    while (validCount != NPasswordsNUD.Value) //Real count of generated passwords
                    {
                        //Save gend pass temporarily
                        string genPass = GenerateSecurePassword((int)PassLengthNUD.Value, validCharacters);

                        //Check that genPass has all requested chars (CryptoProvider provides secure random, does not guarantee all chars of validCharacters are used).
                        HashSet<char> validChars = new HashSet<char>(validCharacters);
                        bool containsAll = validChars.IsSubsetOf(genPass);

                        if (containsAll) //If genPass has ALL characters requested 
                        {
                            if (CheckPwnage(genPass) == false) //No pwnage found for genPass
                            {
                                PasswordTextBox.Text += genPass + Environment.NewLine; //Add pass to textbox
                                validCount += 1; //A valid pass has been created, so we increment it
                            }
                        }
                    }
                }
                else //Checkbox pwn false
                {
                    for (int i = 0; i < NPasswordsNUD.Value; i++) //Generate requested pass
                    {
                        PasswordTextBox.Text += GenerateSecurePassword((int)PassLengthNUD.Value, validCharacters) + Environment.NewLine;
                    }
                }
                
            }

        }

        private bool CheckPwnage (string password)//Password pwned before? -> returns true, Password not pwned before? -> returns false
        {
            //Compute hash of given password
            SHA1 sha1 = new SHA1CryptoServiceProvider();
            byte[] passByte = Encoding.UTF8.GetBytes(password);
            byte[] passHash = sha1.ComputeHash(passByte);
            
            //Convert Hash into readable string
            StringBuilder sb = new StringBuilder();
            foreach (byte b in passHash)
            {
                sb.Append(b.ToString("x2"));
            }
            string hash = sb.ToString();

            //Manage API
            Process cmd = new Process();
            cmd.StartInfo.FileName = "cmd.exe";
            string instruction = "/C curl https://api.pwnedpasswords.com/range/"; // /C command1&command2
            cmd.StartInfo.Arguments = instruction + hash.Substring(0, 5); //First 5 letters of our computed hash sent to webpage.
            cmd.StartInfo.UseShellExecute = false;
            cmd.StartInfo.WindowStyle = ProcessWindowStyle.Normal; //Hidden for hidden window, normal for visible window.
            cmd.StartInfo.RedirectStandardOutput = true;
            cmd.Start();
            
            string standard_output;
            int iterations = 0;
            while (((standard_output = cmd.StandardOutput.ReadLine()) != null))
            {
                int position = standard_output.IndexOf(":"); //Nullreference
                string headhash = hash.Substring(0, 5).ToLower();
                string tailhash = standard_output.Substring(0, 35).ToLower();

                if (headhash + tailhash == hash)
                {
                    return true;
                }
                iterations += 1;
            }

            //PasswordTextBox.Text = cmd.StandardOutput.ReadToEnd();
            
            cmd.WaitForExit();
            
            return false;
        }

        private static string GenerateSecurePassword(int length, string validCharacters)
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

        private void button1_Click(object sender, EventArgs e)
        {
            Process cmd = new Process();
            cmd.StartInfo.FileName = "cmd.exe";
            string instruction = "/C curl https://api.pwnedpasswords.com/range/";
            cmd.StartInfo.Arguments = instruction + "FA224"; // /C command1&command2
            cmd.StartInfo.UseShellExecute = false;
            cmd.StartInfo.WindowStyle = ProcessWindowStyle.Normal;
            cmd.StartInfo.RedirectStandardOutput = true;
            cmd.Start();
            
            PasswordTextBox.Text = cmd.StandardOutput.ReadToEnd();
            cmd.WaitForExit();
        }

        private void InfoPwnageButton_Click(object sender, EventArgs e)
        {
            MessageBox.Show("If this option is checked, for every generated password we will check if it has been pwned by inserting it into the " +
                "webpage https://haveibeenpwned.com/Passwords. \nThis website will check in their database if this password has been cracked already. " +
                "\nFor more info about why should you write down your passwords in this webpage and how is the privacy of searched passwords being kept, " + 
                "visit https://www.troyhunt.com/ive-just-launched-pwned-passwords-version-2/#cloudflareprivacyandkanonymity." + "\n\nNote: Press Ctrl+C to copy " + 
                "the content of this dialog.");
        }
    }
}