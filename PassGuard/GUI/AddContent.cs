using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PassGuard.GUI
{
    public partial class AddContent : Form
    {
        private String url {get; set;}
        private String name { get; set; }
        private String username { get; set; }
        private String password { get; set; }
        private String category { get; set; }
        private String notes { get; set; }
        private List<String> namesInDB;
        private bool addedSuccess;
        private readonly byte[] Key;

        public bool getAddedSuccess()
        {
            return addedSuccess;
        }

        public String getUrl()
        {
            return url;
        }

        public String getName()
        {
            return name;
        }

        public String getUsername()
        {
            return username;
        }

        public String getPassword()
        {
            return password;
        }

        public String getCategory()
        {
            return category;
        }

        public String getNotes()
        {
            return notes;
        }

        public AddContent(List<String> names, byte[] key)
        {
            InitializeComponent();

            Core.Utils utils = new Core.Utils();

            Key = key;
            namesInDB = names;
            for (int i = 0; i < namesInDB.Count; i++)
            {
                namesInDB[i] = utils.DecryptText(key: Key, src: namesInDB[i]);
            }
            addedSuccess = false;

            this.Icon = new Icon(@"..\..\Images\LogoIcon64123.ico"); //Loads Icon from Image folder.
        }

        private void AddButton_MouseEnter(object sender, EventArgs e)
        {
            AddButton.Font = new Font("Microsoft Sans Serif", 11, FontStyle.Underline); //Underline the text when mouse is in the button
        }

        private void AddButton_MouseLeave(object sender, EventArgs e)
        {
            AddButton.Font = new Font("Microsoft Sans Serif", 11, FontStyle.Regular); //Underline the text when mouse is in the button
        }

        private void AddButton_Click(object sender, EventArgs e)
        {
            Core.Utils utils = new Core.Utils();
            String errorMessages = "";

            if (String.IsNullOrWhiteSpace(NameTextbox.Text) || String.IsNullOrWhiteSpace(UsernameTextbox.Text) || String.IsNullOrWhiteSpace(PasswordTextbox.Text))
            {
                errorMessages += "Non-optional fields (Name, Username, Password) cannot be left blank.";
            }
  
            if (namesInDB.Contains(NameTextbox.Text))
            {
                errorMessages += "There is already a saved password with that name in the vault.";
            }

            if (!String.IsNullOrEmpty(errorMessages)) //If any error...
            {
                MessageBox.Show(text: "The following errors have been found:\n\n" + errorMessages, caption: "Warning(s)", icon: MessageBoxIcon.Warning, buttons: MessageBoxButtons.OK);
            }
            else //No error in params, create vault.
            {
                url = utils.EncryptText(key: Key, src: URLTextbox.Text);
                name = utils.EncryptText(key: Key, src: NameTextbox.Text);
                username = utils.EncryptText(key: Key, src: UsernameTextbox.Text);
                password = utils.EncryptText(key: Key, src: PasswordTextbox.Text);
                category = utils.EncryptText(key: Key, src: CategoryTextbox.Text);
                notes = utils.EncryptText(key: Key, src: NotesTextbox.Text);

                addedSuccess = true;

                this.Close();

                
            }

         }


    }
}
