using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.Versioning;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PassGuard.GUI
{
	//UserControl for the Create Quick Password Menu
	public partial class CreateQuickPassUC : UserControl
	{
		public CreateQuickPassUC()
		{
			this.Anchor = AnchorStyles.None;
			InitializeComponent();

			SetAcceptButton();
		}

		//Dictionaries containing the relation between a checkbox and the char/text it represents.
		private Dictionary<CheckBox, string> characters { get; set; } = new Dictionary<CheckBox, string>(); //No duplicates
		private Dictionary<CheckBox, string> symbols { get; set; } = new Dictionary<CheckBox, string>(); //No duplicates

		private void SetAcceptButton()
		{
			// Get the parent form of the user control
			Form parentForm = this.FindForm();

			// Set the button1 as the AcceptButton for the parent form
			if (parentForm != null)
			{
				parentForm.AcceptButton = GenPassButton;
			}
		}

		//Fill the dictionary with corresponding pair CheckBox(characters)-String
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

		//Fill the dictionary with corresponding pair CheckBox(symbols)-String
		private Dictionary<CheckBox, string> fillSymbolsDict(Dictionary<CheckBox, string> symbols)
		{
			// !$%&/\()|@#€<>[]{}+-*.:_,;ñÑ¿?=çÇ¡
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
			//Load Dictionaries with their corresponding values.
			characters = fillCharDict(characters);
			symbols = fillSymbolsDict(symbols);
			//Set tooltip texts
			SymbolsTooltip.SetToolTip(SymbolsCheckbox, "If this option and at least one or more symbols are checked, only one or more \nof these symbols (at least one, it is unlikely that all symbols will be displayed) \nwill be displayed in the password.");
			ClipboardToolTip.SetToolTip(CheckPwnageCheckbox, "Recommended as it implies more secure passwords, however it consumes \nmuch more time when number of passwords requested grows.");
		
		}

		private void SymbolsCheckbox_CheckedChanged(object sender, EventArgs e)
		{
			if (SymbolsCheckbox.Checked == false)//If it has been deactivated...
			{
				foreach(KeyValuePair<CheckBox, string> pair in symbols)//Disable all checkboxes and uncheck them.
				{
					pair.Key.Enabled = false; //Although they are disabled, if they are checked you could do whatever in the ifs of later functions.
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

		//Set all the checkboxes enable property as true or false whether check is true or false.
		private void SetEnabled(bool check)
		{
			if(check)
			{
				CheckBoxesPanel.Enabled = true;
			}
			else
			{
				CheckBoxesPanel.Enabled = false;
			}
		}

		private async void GenPassButton_Click(object sender, EventArgs e)
		{
			try
			{
				SetEnabled(false); //Unset every checkbox, so that meanwhile passwords are being generated there arent any errors for clicking other buttons/checkboxes.

				//Clear previous content + Add Progress in label and textbox.
				PercentageLabel.Text = "0% complete, 0/" + NPasswordsNUD.Value.ToString() + " passwords generated.";
				PercentageLabel.Visible = true;
				PercentageLabel.Refresh();
				PasswordTextBox.Clear();

				//Create a string with the valid characters
				StringBuilder sb = new();

				foreach (KeyValuePair<CheckBox, string> pair in characters)//Check if any char checkbox is activated.
				{
					if (pair.Key.Checked == true)
					{
						sb.Append(pair.Value); //Add characters related to that checkbox.
					}
				}

				if (SymbolsCheckbox.Checked == true)//Check symbols only if primary checkbox is enabled.
				{

					foreach (KeyValuePair<CheckBox, string> pair in symbols)//Check if any char checkbox is activated.
					{
						if (pair.Key.Checked == true)
						{
							sb.Append(pair.Value); //Add characters related to that checkbox.
						}
					}
				}

				string validCharacters = sb.ToString(); //Got the characters into a string to compose the password

				//Test if validCharacters is null (no chars were selected).
				if (String.IsNullOrEmpty(validCharacters)) //Maybe no chars were selected.
				{
					MessageBox.Show(text: "Cannot generate passwords without characters. \n\nPlease select at least one of the available characters or symbols.", caption: "Needed characters to generate passwords!");
				}
				else //We have valid characters to work with (at least one) and the password length is OK.
				{
					//Check if user wants unique and safe passwords.
					if (CheckPwnageCheckbox.Checked == true) //Checkbox pwn true
					{
						if (LowerCheckbox.Checked == true || UpperCheckbox.Checked == true || NumbersCheckbox.Checked == true) //So that it actually can find no pwned passwords.
						{
							int validCount = 0; //Many random passwords will be generated but only the ones that contain all the characters requested will be valid.
							bool missingChar;
							while (validCount != NPasswordsNUD.Value) //Until Real count of valid generated passwords does match nPasswords
							{
								//Save generated pass temporarily
								string genPass = Utils.CryptoUtils.GenerateSecurePassword((int)PassLengthNUD.Value, validCharacters);

								//Check that genPass has all requested chars (CryptoProvider provides secure random, does not guarantee all chars of validCharacters are used).
								missingChar = false; //If chars are missing, we won´t enter the block of code of generating not pwned passwords.
													 //Check (if checkbox is checked) if genPass contains the characters selected.
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
								//Same check but with symbols, in case the corresponding checkboxes are checked and there are not already missing characters
								if (SymbolsCheckbox.Checked == true)
								{
									if (!missingChar)
									{
										foreach (KeyValuePair<CheckBox, string> pair in symbols)//Check if any char checkbox is activated.
										{
											if (pair.Key.Checked == true)
											{
												if (genPass.Contains(pair.Value))//If genPass contains one of the symbols checked, we exit.
												{
													missingChar = false;
													break;
												}
												else
												{
													missingChar = true; //If it does not contain a checked symbol, we exit and generate another password.
												}
											}
										}
									}
								}
								if (!missingChar) //If genPass has ALL characters requested 
								{
									bool check = await Pwned.Pwned.CheckPwnage(genPass);
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
							}
						}
						else //Any of lower, upper or numbers checkboxes were checked, we cannot go on...
						{
							PercentageLabel.Visible = false;
							MessageBox.Show(text: "When generating not pwned passwords, at least lower or upper case letters or numbers checkboxes must be checked.", caption: "Notes about conditions of generating not pwned passwords");
						}

					}
					else //Checkbox pwn false
					{
						int validCount = 0; //Many random passwords will be generated but only the ones that contain all the characters requested will be valid.
						bool missingChar; //Will be true if a requested char is missing
						while (validCount != NPasswordsNUD.Value) //Until Real count of valid generated passwords does match nPasswords requested
						{

							string genPass = Utils.CryptoUtils.GenerateSecurePassword((int)PassLengthNUD.Value, validCharacters); //Generate a secure password
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
						}
					}
				}
			}
			catch (Exception)
			{
				MessageBox.Show(text: "PassGuard could not generate the requested passwords, please try again later", caption: "Error", icon: MessageBoxIcon.Error, buttons: MessageBoxButtons.OK);
			}
			finally
			{
				SetEnabled(true); //Re-enable checkboxes
			}
		}

		private void InfoPwnageButton_Click(object sender, EventArgs e)
		{
			GUI.InfoPwnageForm info = new()
			{
				BackColor = this.BackColor
			};
			info.ShowDialog();
		}

		[SupportedOSPlatform("windows")]
		private void SelectAllSymbolsButton_MouseEnter(object sender, EventArgs e)
		{
			SelectAllSymbolsButton.Font = new Font("Microsoft Sans Serif", 10, FontStyle.Underline);//If mouse over button, underline text
		}

		[SupportedOSPlatform("windows")]
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

		private void CheckPwnageCheckbox_CheckedChanged(object sender, EventArgs e)
		{
			if(CheckPwnageCheckbox.Checked == true)
			{
				PassLengthNUD.Minimum = 16; //Set minimum (longer) length when Checkbox is checked.
				PassLengthNUD.Value = PassLengthNUD.Minimum;
			}

			else if (CheckPwnageCheckbox.Checked == false)
			{
				PassLengthNUD.Minimum = 5; //Set minimum (shorter) length when Checkbox is checked.
				PassLengthNUD.Value = PassLengthNUD.Minimum;
			}

		}

		private void NoteSymbolsButton_Click(object sender, EventArgs e)
		{
			MessageBox.Show(text: "If one or more symbols are checked, only one or more of these symbols (at least one, it is unlikely that all symbols will be displayed) will be displayed in the password.", caption: "Information about symbols in passwords");
		}

		[SupportedOSPlatform("windows")]
		private void NoteSymbolsButton_MouseEnter(object sender, EventArgs e)
		{
			NoteSymbolsButton.Font = new Font("Microsoft Sans Serif", 9, FontStyle.Underline);//If mouse over button, underline text
		}

		[SupportedOSPlatform("windows")]
		private void NoteSymbolsButton_MouseLeave(object sender, EventArgs e)
		{
			NoteSymbolsButton.Font = new Font("Microsoft Sans Serif", 9, FontStyle.Regular); //If mouse leaves button, regular text
		}

		[SupportedOSPlatform("windows")]
		private void GenPassButton_MouseEnter(object sender, EventArgs e)
		{
			GenPassButton.Font = new Font("Microsoft Sans Serif", 11, FontStyle.Underline);//If mouse over button, underline text
		}

		[SupportedOSPlatform("windows")]
		private void GenPassButton_MouseLeave(object sender, EventArgs e)
		{
			GenPassButton.Font = new Font("Microsoft Sans Serif", 11, FontStyle.Regular); //If mouse leaves button, regular text
		}

		private void CreateQuickPassUC_BackColorChanged(object sender, EventArgs e)
		{
			if (this.BackColor == Color.FromArgb(230, 230, 230))
			{
				NPasswordsNUD.BackColor = SystemColors.Window;
				PassLengthNUD.BackColor = SystemColors.Window;
				PasswordTextBox.BackColor = SystemColors.Window;

			}
			else
			{
				NPasswordsNUD.BackColor = Color.FromArgb(128, 130, 129);
				PassLengthNUD.BackColor = Color.FromArgb(128, 130, 129);
				PasswordTextBox.BackColor = Color.FromArgb(128, 130, 129);

			}
		}
	}
}

