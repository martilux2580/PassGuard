
namespace PassGuard.GUI
{
    partial class CreateQuickPassUC
    {
        /// <summary> 
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de componentes

        /// <summary> 
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.GenPassButton = new System.Windows.Forms.Button();
            this.TitleLabel = new System.Windows.Forms.Label();
            this.CheckBoxesPanel = new System.Windows.Forms.Panel();
            this.InfoPwnageButton = new System.Windows.Forms.Button();
            this.CheckPwnageCheckbox = new System.Windows.Forms.CheckBox();
            this.button1 = new System.Windows.Forms.Button();
            this.CheckboxCloseQuestion = new System.Windows.Forms.CheckBox();
            this.CheckboxOpenQuestion = new System.Windows.Forms.CheckBox();
            this.PassLengthNUD = new System.Windows.Forms.NumericUpDown();
            this.PassLengthLabel = new System.Windows.Forms.Label();
            this.PassGenerateLabel = new System.Windows.Forms.Label();
            this.NPasswordsNUD = new System.Windows.Forms.NumericUpDown();
            this.CheckboxHigherEne = new System.Windows.Forms.CheckBox();
            this.CheckboxLowerEne = new System.Windows.Forms.CheckBox();
            this.CheckboxOpenExclamation = new System.Windows.Forms.CheckBox();
            this.CheckboxSemicolon = new System.Windows.Forms.CheckBox();
            this.CheckboxComma = new System.Windows.Forms.CheckBox();
            this.CheckboxUnderscore = new System.Windows.Forms.CheckBox();
            this.CheckboxColon = new System.Windows.Forms.CheckBox();
            this.CheckboxPeriod = new System.Windows.Forms.CheckBox();
            this.CheckboxLowerCaseTurkish = new System.Windows.Forms.CheckBox();
            this.CheckboxCapitalTurkish = new System.Windows.Forms.CheckBox();
            this.CheckboxMultiplier = new System.Windows.Forms.CheckBox();
            this.CheckboxSubstract = new System.Windows.Forms.CheckBox();
            this.CheckboxAdd = new System.Windows.Forms.CheckBox();
            this.CheckboxCloseCurlyBracket = new System.Windows.Forms.CheckBox();
            this.CheckboxOpenCurlyBracket = new System.Windows.Forms.CheckBox();
            this.CheckboxCloseSquareBracket = new System.Windows.Forms.CheckBox();
            this.CheckboxOpenSquareBracket = new System.Windows.Forms.CheckBox();
            this.CheckboxHigherThan = new System.Windows.Forms.CheckBox();
            this.CheckboxLessThan = new System.Windows.Forms.CheckBox();
            this.CheckboxEuro = new System.Windows.Forms.CheckBox();
            this.CheckboxHashTag = new System.Windows.Forms.CheckBox();
            this.CheckboxAt = new System.Windows.Forms.CheckBox();
            this.CheckboxBar = new System.Windows.Forms.CheckBox();
            this.CheckboxEqual = new System.Windows.Forms.CheckBox();
            this.CheckboxCloseParenthesis = new System.Windows.Forms.CheckBox();
            this.CheckboxOpenParenthesis = new System.Windows.Forms.CheckBox();
            this.CheckboxBackSlash = new System.Windows.Forms.CheckBox();
            this.CheckboxSlash = new System.Windows.Forms.CheckBox();
            this.CheckboxAmpersand = new System.Windows.Forms.CheckBox();
            this.CheckboxPercentage = new System.Windows.Forms.CheckBox();
            this.CheckboxDollar = new System.Windows.Forms.CheckBox();
            this.CheckBoxExclamation = new System.Windows.Forms.CheckBox();
            this.SymbolsCheckbox = new System.Windows.Forms.CheckBox();
            this.NumbersCheckbox = new System.Windows.Forms.CheckBox();
            this.LowerCheckbox = new System.Windows.Forms.CheckBox();
            this.UpperCheckbox = new System.Windows.Forms.CheckBox();
            this.PasswordTextBox = new System.Windows.Forms.TextBox();
            this.DisplayPassLabel = new System.Windows.Forms.Label();
            this.CopyClipboardButton = new System.Windows.Forms.Button();
            this.ClipboardToolTip = new System.Windows.Forms.ToolTip(this.components);
            this.panel1 = new System.Windows.Forms.Panel();
            this.PercentageLabel = new System.Windows.Forms.Label();
            this.SelectAllSymbolsButton = new System.Windows.Forms.Button();
            this.CheckBoxesPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PassLengthNUD)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.NPasswordsNUD)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // GenPassButton
            // 
            this.GenPassButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.GenPassButton.Location = new System.Drawing.Point(448, 298);
            this.GenPassButton.Name = "GenPassButton";
            this.GenPassButton.Size = new System.Drawing.Size(240, 33);
            this.GenPassButton.TabIndex = 0;
            this.GenPassButton.Text = "Generate Password(s)";
            this.GenPassButton.UseVisualStyleBackColor = true;
            this.GenPassButton.Click += new System.EventHandler(this.GenPassButton_Click);
            // 
            // TitleLabel
            // 
            this.TitleLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TitleLabel.Location = new System.Drawing.Point(19, 18);
            this.TitleLabel.Name = "TitleLabel";
            this.TitleLabel.Size = new System.Drawing.Size(486, 25);
            this.TitleLabel.TabIndex = 1;
            this.TitleLabel.Text = "Select the characteristics your password must have: ";
            // 
            // CheckBoxesPanel
            // 
            this.CheckBoxesPanel.AutoSize = true;
            this.CheckBoxesPanel.Controls.Add(this.SelectAllSymbolsButton);
            this.CheckBoxesPanel.Controls.Add(this.InfoPwnageButton);
            this.CheckBoxesPanel.Controls.Add(this.CheckPwnageCheckbox);
            this.CheckBoxesPanel.Controls.Add(this.button1);
            this.CheckBoxesPanel.Controls.Add(this.CheckboxCloseQuestion);
            this.CheckBoxesPanel.Controls.Add(this.CheckboxOpenQuestion);
            this.CheckBoxesPanel.Controls.Add(this.PassLengthNUD);
            this.CheckBoxesPanel.Controls.Add(this.PassLengthLabel);
            this.CheckBoxesPanel.Controls.Add(this.PassGenerateLabel);
            this.CheckBoxesPanel.Controls.Add(this.NPasswordsNUD);
            this.CheckBoxesPanel.Controls.Add(this.GenPassButton);
            this.CheckBoxesPanel.Controls.Add(this.CheckboxHigherEne);
            this.CheckBoxesPanel.Controls.Add(this.CheckboxLowerEne);
            this.CheckBoxesPanel.Controls.Add(this.CheckboxOpenExclamation);
            this.CheckBoxesPanel.Controls.Add(this.CheckboxSemicolon);
            this.CheckBoxesPanel.Controls.Add(this.CheckboxComma);
            this.CheckBoxesPanel.Controls.Add(this.CheckboxUnderscore);
            this.CheckBoxesPanel.Controls.Add(this.CheckboxColon);
            this.CheckBoxesPanel.Controls.Add(this.CheckboxPeriod);
            this.CheckBoxesPanel.Controls.Add(this.CheckboxLowerCaseTurkish);
            this.CheckBoxesPanel.Controls.Add(this.CheckboxCapitalTurkish);
            this.CheckBoxesPanel.Controls.Add(this.CheckboxMultiplier);
            this.CheckBoxesPanel.Controls.Add(this.CheckboxSubstract);
            this.CheckBoxesPanel.Controls.Add(this.CheckboxAdd);
            this.CheckBoxesPanel.Controls.Add(this.CheckboxCloseCurlyBracket);
            this.CheckBoxesPanel.Controls.Add(this.CheckboxOpenCurlyBracket);
            this.CheckBoxesPanel.Controls.Add(this.CheckboxCloseSquareBracket);
            this.CheckBoxesPanel.Controls.Add(this.CheckboxOpenSquareBracket);
            this.CheckBoxesPanel.Controls.Add(this.CheckboxHigherThan);
            this.CheckBoxesPanel.Controls.Add(this.CheckboxLessThan);
            this.CheckBoxesPanel.Controls.Add(this.CheckboxEuro);
            this.CheckBoxesPanel.Controls.Add(this.CheckboxHashTag);
            this.CheckBoxesPanel.Controls.Add(this.CheckboxAt);
            this.CheckBoxesPanel.Controls.Add(this.CheckboxBar);
            this.CheckBoxesPanel.Controls.Add(this.CheckboxEqual);
            this.CheckBoxesPanel.Controls.Add(this.CheckboxCloseParenthesis);
            this.CheckBoxesPanel.Controls.Add(this.CheckboxOpenParenthesis);
            this.CheckBoxesPanel.Controls.Add(this.CheckboxBackSlash);
            this.CheckBoxesPanel.Controls.Add(this.CheckboxSlash);
            this.CheckBoxesPanel.Controls.Add(this.CheckboxAmpersand);
            this.CheckBoxesPanel.Controls.Add(this.CheckboxPercentage);
            this.CheckBoxesPanel.Controls.Add(this.CheckboxDollar);
            this.CheckBoxesPanel.Controls.Add(this.CheckBoxExclamation);
            this.CheckBoxesPanel.Controls.Add(this.SymbolsCheckbox);
            this.CheckBoxesPanel.Controls.Add(this.NumbersCheckbox);
            this.CheckBoxesPanel.Controls.Add(this.LowerCheckbox);
            this.CheckBoxesPanel.Controls.Add(this.UpperCheckbox);
            this.CheckBoxesPanel.Location = new System.Drawing.Point(24, 46);
            this.CheckBoxesPanel.Name = "CheckBoxesPanel";
            this.CheckBoxesPanel.Size = new System.Drawing.Size(1105, 339);
            this.CheckBoxesPanel.TabIndex = 2;
            // 
            // InfoPwnageButton
            // 
            this.InfoPwnageButton.FlatAppearance.BorderSize = 0;
            this.InfoPwnageButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.InfoPwnageButton.Location = new System.Drawing.Point(865, 244);
            this.InfoPwnageButton.Name = "InfoPwnageButton";
            this.InfoPwnageButton.Size = new System.Drawing.Size(50, 39);
            this.InfoPwnageButton.TabIndex = 56;
            this.InfoPwnageButton.UseVisualStyleBackColor = true;
            this.InfoPwnageButton.Click += new System.EventHandler(this.InfoPwnageButton_Click);
            // 
            // CheckPwnageCheckbox
            // 
            this.CheckPwnageCheckbox.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CheckPwnageCheckbox.Location = new System.Drawing.Point(281, 247);
            this.CheckPwnageCheckbox.Name = "CheckPwnageCheckbox";
            this.CheckPwnageCheckbox.Size = new System.Drawing.Size(622, 39);
            this.CheckPwnageCheckbox.TabIndex = 55;
            this.CheckPwnageCheckbox.Text = "Only generate passwords that have not been pwned until today.";
            this.CheckPwnageCheckbox.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(745, 298);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(183, 33);
            this.button1.TabIndex = 54;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // CheckboxCloseQuestion
            // 
            this.CheckboxCloseQuestion.AutoSize = true;
            this.CheckboxCloseQuestion.Enabled = false;
            this.CheckboxCloseQuestion.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CheckboxCloseQuestion.Location = new System.Drawing.Point(1047, 45);
            this.CheckboxCloseQuestion.Name = "CheckboxCloseQuestion";
            this.CheckboxCloseQuestion.Size = new System.Drawing.Size(46, 30);
            this.CheckboxCloseQuestion.TabIndex = 53;
            this.CheckboxCloseQuestion.Text = "?";
            this.CheckboxCloseQuestion.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.CheckboxCloseQuestion.UseVisualStyleBackColor = true;
            // 
            // CheckboxOpenQuestion
            // 
            this.CheckboxOpenQuestion.AutoSize = true;
            this.CheckboxOpenQuestion.Enabled = false;
            this.CheckboxOpenQuestion.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CheckboxOpenQuestion.Location = new System.Drawing.Point(992, 45);
            this.CheckboxOpenQuestion.Name = "CheckboxOpenQuestion";
            this.CheckboxOpenQuestion.Size = new System.Drawing.Size(46, 30);
            this.CheckboxOpenQuestion.TabIndex = 52;
            this.CheckboxOpenQuestion.Text = "¿";
            this.CheckboxOpenQuestion.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.CheckboxOpenQuestion.UseVisualStyleBackColor = true;
            // 
            // PassLengthNUD
            // 
            this.PassLengthNUD.Location = new System.Drawing.Point(365, 206);
            this.PassLengthNUD.Maximum = new decimal(new int[] {
            256,
            0,
            0,
            0});
            this.PassLengthNUD.Minimum = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.PassLengthNUD.Name = "PassLengthNUD";
            this.PassLengthNUD.Size = new System.Drawing.Size(68, 22);
            this.PassLengthNUD.TabIndex = 51;
            this.PassLengthNUD.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            // 
            // PassLengthLabel
            // 
            this.PassLengthLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PassLengthLabel.Location = new System.Drawing.Point(10, 202);
            this.PassLengthLabel.Name = "PassLengthLabel";
            this.PassLengthLabel.Size = new System.Drawing.Size(382, 24);
            this.PassLengthLabel.TabIndex = 50;
            this.PassLengthLabel.Text = "My password(s) must have a length of: ";
            // 
            // PassGenerateLabel
            // 
            this.PassGenerateLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PassGenerateLabel.Location = new System.Drawing.Point(83, 143);
            this.PassGenerateLabel.Name = "PassGenerateLabel";
            this.PassGenerateLabel.Size = new System.Drawing.Size(245, 24);
            this.PassGenerateLabel.TabIndex = 49;
            this.PassGenerateLabel.Text = "password(s) to generate.";
            // 
            // NPasswordsNUD
            // 
            this.NPasswordsNUD.Location = new System.Drawing.Point(14, 146);
            this.NPasswordsNUD.Maximum = new decimal(new int[] {
            2048,
            0,
            0,
            0});
            this.NPasswordsNUD.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.NPasswordsNUD.Name = "NPasswordsNUD";
            this.NPasswordsNUD.Size = new System.Drawing.Size(68, 22);
            this.NPasswordsNUD.TabIndex = 48;
            this.NPasswordsNUD.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // CheckboxHigherEne
            // 
            this.CheckboxHigherEne.AutoSize = true;
            this.CheckboxHigherEne.Enabled = false;
            this.CheckboxHigherEne.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CheckboxHigherEne.Location = new System.Drawing.Point(888, 141);
            this.CheckboxHigherEne.Name = "CheckboxHigherEne";
            this.CheckboxHigherEne.Size = new System.Drawing.Size(50, 30);
            this.CheckboxHigherEne.TabIndex = 47;
            this.CheckboxHigherEne.Text = "Ñ";
            this.CheckboxHigherEne.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.CheckboxHigherEne.UseVisualStyleBackColor = true;
            // 
            // CheckboxLowerEne
            // 
            this.CheckboxLowerEne.AutoSize = true;
            this.CheckboxLowerEne.Enabled = false;
            this.CheckboxLowerEne.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CheckboxLowerEne.Location = new System.Drawing.Point(835, 141);
            this.CheckboxLowerEne.Name = "CheckboxLowerEne";
            this.CheckboxLowerEne.Size = new System.Drawing.Size(46, 30);
            this.CheckboxLowerEne.TabIndex = 46;
            this.CheckboxLowerEne.Text = "ñ";
            this.CheckboxLowerEne.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.CheckboxLowerEne.UseVisualStyleBackColor = true;
            // 
            // CheckboxOpenExclamation
            // 
            this.CheckboxOpenExclamation.AutoSize = true;
            this.CheckboxOpenExclamation.Enabled = false;
            this.CheckboxOpenExclamation.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CheckboxOpenExclamation.Location = new System.Drawing.Point(726, 189);
            this.CheckboxOpenExclamation.Name = "CheckboxOpenExclamation";
            this.CheckboxOpenExclamation.Size = new System.Drawing.Size(40, 30);
            this.CheckboxOpenExclamation.TabIndex = 45;
            this.CheckboxOpenExclamation.Text = "¡";
            this.CheckboxOpenExclamation.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.CheckboxOpenExclamation.UseVisualStyleBackColor = true;
            // 
            // CheckboxSemicolon
            // 
            this.CheckboxSemicolon.AutoSize = true;
            this.CheckboxSemicolon.Enabled = false;
            this.CheckboxSemicolon.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CheckboxSemicolon.Location = new System.Drawing.Point(781, 141);
            this.CheckboxSemicolon.Name = "CheckboxSemicolon";
            this.CheckboxSemicolon.Size = new System.Drawing.Size(40, 30);
            this.CheckboxSemicolon.TabIndex = 43;
            this.CheckboxSemicolon.Text = ";";
            this.CheckboxSemicolon.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.CheckboxSemicolon.UseVisualStyleBackColor = true;
            // 
            // CheckboxComma
            // 
            this.CheckboxComma.AutoSize = true;
            this.CheckboxComma.Enabled = false;
            this.CheckboxComma.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CheckboxComma.Location = new System.Drawing.Point(726, 141);
            this.CheckboxComma.Name = "CheckboxComma";
            this.CheckboxComma.Size = new System.Drawing.Size(40, 30);
            this.CheckboxComma.TabIndex = 42;
            this.CheckboxComma.Text = ",";
            this.CheckboxComma.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.CheckboxComma.UseVisualStyleBackColor = true;
            // 
            // CheckboxUnderscore
            // 
            this.CheckboxUnderscore.AutoSize = true;
            this.CheckboxUnderscore.Enabled = false;
            this.CheckboxUnderscore.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CheckboxUnderscore.Location = new System.Drawing.Point(668, 141);
            this.CheckboxUnderscore.Name = "CheckboxUnderscore";
            this.CheckboxUnderscore.Size = new System.Drawing.Size(46, 30);
            this.CheckboxUnderscore.TabIndex = 41;
            this.CheckboxUnderscore.Text = "_";
            this.CheckboxUnderscore.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.CheckboxUnderscore.UseVisualStyleBackColor = true;
            // 
            // CheckboxColon
            // 
            this.CheckboxColon.AutoSize = true;
            this.CheckboxColon.Enabled = false;
            this.CheckboxColon.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CheckboxColon.Location = new System.Drawing.Point(605, 141);
            this.CheckboxColon.Name = "CheckboxColon";
            this.CheckboxColon.Size = new System.Drawing.Size(40, 30);
            this.CheckboxColon.TabIndex = 40;
            this.CheckboxColon.Text = ":";
            this.CheckboxColon.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.CheckboxColon.UseVisualStyleBackColor = true;
            // 
            // CheckboxPeriod
            // 
            this.CheckboxPeriod.AutoSize = true;
            this.CheckboxPeriod.Enabled = false;
            this.CheckboxPeriod.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CheckboxPeriod.Location = new System.Drawing.Point(549, 141);
            this.CheckboxPeriod.Name = "CheckboxPeriod";
            this.CheckboxPeriod.Size = new System.Drawing.Size(40, 30);
            this.CheckboxPeriod.TabIndex = 39;
            this.CheckboxPeriod.Text = ".";
            this.CheckboxPeriod.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.CheckboxPeriod.UseVisualStyleBackColor = true;
            // 
            // CheckboxLowerCaseTurkish
            // 
            this.CheckboxLowerCaseTurkish.AutoSize = true;
            this.CheckboxLowerCaseTurkish.Enabled = false;
            this.CheckboxLowerCaseTurkish.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CheckboxLowerCaseTurkish.Location = new System.Drawing.Point(606, 188);
            this.CheckboxLowerCaseTurkish.Name = "CheckboxLowerCaseTurkish";
            this.CheckboxLowerCaseTurkish.Size = new System.Drawing.Size(45, 30);
            this.CheckboxLowerCaseTurkish.TabIndex = 37;
            this.CheckboxLowerCaseTurkish.Text = "ç";
            this.CheckboxLowerCaseTurkish.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.CheckboxLowerCaseTurkish.UseVisualStyleBackColor = true;
            // 
            // CheckboxCapitalTurkish
            // 
            this.CheckboxCapitalTurkish.AutoSize = true;
            this.CheckboxCapitalTurkish.Enabled = false;
            this.CheckboxCapitalTurkish.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CheckboxCapitalTurkish.Location = new System.Drawing.Point(668, 189);
            this.CheckboxCapitalTurkish.Name = "CheckboxCapitalTurkish";
            this.CheckboxCapitalTurkish.Size = new System.Drawing.Size(50, 30);
            this.CheckboxCapitalTurkish.TabIndex = 36;
            this.CheckboxCapitalTurkish.Text = "Ç";
            this.CheckboxCapitalTurkish.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.CheckboxCapitalTurkish.UseVisualStyleBackColor = true;
            // 
            // CheckboxMultiplier
            // 
            this.CheckboxMultiplier.AutoSize = true;
            this.CheckboxMultiplier.Enabled = false;
            this.CheckboxMultiplier.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CheckboxMultiplier.Location = new System.Drawing.Point(1047, 141);
            this.CheckboxMultiplier.Name = "CheckboxMultiplier";
            this.CheckboxMultiplier.Size = new System.Drawing.Size(43, 30);
            this.CheckboxMultiplier.TabIndex = 35;
            this.CheckboxMultiplier.Text = "*";
            this.CheckboxMultiplier.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.CheckboxMultiplier.UseVisualStyleBackColor = true;
            // 
            // CheckboxSubstract
            // 
            this.CheckboxSubstract.AutoSize = true;
            this.CheckboxSubstract.Enabled = false;
            this.CheckboxSubstract.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CheckboxSubstract.Location = new System.Drawing.Point(992, 141);
            this.CheckboxSubstract.Name = "CheckboxSubstract";
            this.CheckboxSubstract.Size = new System.Drawing.Size(41, 30);
            this.CheckboxSubstract.TabIndex = 34;
            this.CheckboxSubstract.Text = "-";
            this.CheckboxSubstract.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.CheckboxSubstract.UseVisualStyleBackColor = true;
            // 
            // CheckboxAdd
            // 
            this.CheckboxAdd.AutoSize = true;
            this.CheckboxAdd.Enabled = false;
            this.CheckboxAdd.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CheckboxAdd.Location = new System.Drawing.Point(945, 141);
            this.CheckboxAdd.Name = "CheckboxAdd";
            this.CheckboxAdd.Size = new System.Drawing.Size(47, 30);
            this.CheckboxAdd.TabIndex = 33;
            this.CheckboxAdd.Text = "+";
            this.CheckboxAdd.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.CheckboxAdd.UseVisualStyleBackColor = true;
            // 
            // CheckboxCloseCurlyBracket
            // 
            this.CheckboxCloseCurlyBracket.AutoSize = true;
            this.CheckboxCloseCurlyBracket.Enabled = false;
            this.CheckboxCloseCurlyBracket.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CheckboxCloseCurlyBracket.Location = new System.Drawing.Point(1047, 93);
            this.CheckboxCloseCurlyBracket.Name = "CheckboxCloseCurlyBracket";
            this.CheckboxCloseCurlyBracket.Size = new System.Drawing.Size(41, 30);
            this.CheckboxCloseCurlyBracket.TabIndex = 32;
            this.CheckboxCloseCurlyBracket.Text = "}";
            this.CheckboxCloseCurlyBracket.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.CheckboxCloseCurlyBracket.UseVisualStyleBackColor = true;
            // 
            // CheckboxOpenCurlyBracket
            // 
            this.CheckboxOpenCurlyBracket.AutoSize = true;
            this.CheckboxOpenCurlyBracket.Enabled = false;
            this.CheckboxOpenCurlyBracket.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CheckboxOpenCurlyBracket.Location = new System.Drawing.Point(992, 93);
            this.CheckboxOpenCurlyBracket.Name = "CheckboxOpenCurlyBracket";
            this.CheckboxOpenCurlyBracket.Size = new System.Drawing.Size(41, 30);
            this.CheckboxOpenCurlyBracket.TabIndex = 31;
            this.CheckboxOpenCurlyBracket.Text = "{";
            this.CheckboxOpenCurlyBracket.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.CheckboxOpenCurlyBracket.UseVisualStyleBackColor = true;
            // 
            // CheckboxCloseSquareBracket
            // 
            this.CheckboxCloseSquareBracket.AutoSize = true;
            this.CheckboxCloseSquareBracket.Enabled = false;
            this.CheckboxCloseSquareBracket.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CheckboxCloseSquareBracket.Location = new System.Drawing.Point(945, 93);
            this.CheckboxCloseSquareBracket.Name = "CheckboxCloseSquareBracket";
            this.CheckboxCloseSquareBracket.Size = new System.Drawing.Size(40, 30);
            this.CheckboxCloseSquareBracket.TabIndex = 30;
            this.CheckboxCloseSquareBracket.Text = "]";
            this.CheckboxCloseSquareBracket.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.CheckboxCloseSquareBracket.UseVisualStyleBackColor = true;
            // 
            // CheckboxOpenSquareBracket
            // 
            this.CheckboxOpenSquareBracket.AutoSize = true;
            this.CheckboxOpenSquareBracket.Enabled = false;
            this.CheckboxOpenSquareBracket.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CheckboxOpenSquareBracket.Location = new System.Drawing.Point(888, 93);
            this.CheckboxOpenSquareBracket.Name = "CheckboxOpenSquareBracket";
            this.CheckboxOpenSquareBracket.Size = new System.Drawing.Size(40, 30);
            this.CheckboxOpenSquareBracket.TabIndex = 29;
            this.CheckboxOpenSquareBracket.Text = "[";
            this.CheckboxOpenSquareBracket.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.CheckboxOpenSquareBracket.UseVisualStyleBackColor = true;
            // 
            // CheckboxHigherThan
            // 
            this.CheckboxHigherThan.AutoSize = true;
            this.CheckboxHigherThan.Enabled = false;
            this.CheckboxHigherThan.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CheckboxHigherThan.Location = new System.Drawing.Point(835, 93);
            this.CheckboxHigherThan.Name = "CheckboxHigherThan";
            this.CheckboxHigherThan.Size = new System.Drawing.Size(47, 30);
            this.CheckboxHigherThan.TabIndex = 25;
            this.CheckboxHigherThan.Text = ">";
            this.CheckboxHigherThan.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.CheckboxHigherThan.UseVisualStyleBackColor = true;
            // 
            // CheckboxLessThan
            // 
            this.CheckboxLessThan.AutoSize = true;
            this.CheckboxLessThan.Enabled = false;
            this.CheckboxLessThan.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CheckboxLessThan.Location = new System.Drawing.Point(781, 93);
            this.CheckboxLessThan.Name = "CheckboxLessThan";
            this.CheckboxLessThan.Size = new System.Drawing.Size(47, 30);
            this.CheckboxLessThan.TabIndex = 24;
            this.CheckboxLessThan.Text = "<";
            this.CheckboxLessThan.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.CheckboxLessThan.UseVisualStyleBackColor = true;
            // 
            // CheckboxEuro
            // 
            this.CheckboxEuro.AutoSize = true;
            this.CheckboxEuro.Enabled = false;
            this.CheckboxEuro.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CheckboxEuro.Location = new System.Drawing.Point(726, 93);
            this.CheckboxEuro.Name = "CheckboxEuro";
            this.CheckboxEuro.Size = new System.Drawing.Size(46, 30);
            this.CheckboxEuro.TabIndex = 22;
            this.CheckboxEuro.Text = "€";
            this.CheckboxEuro.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.CheckboxEuro.UseVisualStyleBackColor = true;
            // 
            // CheckboxHashTag
            // 
            this.CheckboxHashTag.AutoSize = true;
            this.CheckboxHashTag.Enabled = false;
            this.CheckboxHashTag.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CheckboxHashTag.Location = new System.Drawing.Point(668, 93);
            this.CheckboxHashTag.Name = "CheckboxHashTag";
            this.CheckboxHashTag.Size = new System.Drawing.Size(46, 30);
            this.CheckboxHashTag.TabIndex = 20;
            this.CheckboxHashTag.Text = "#";
            this.CheckboxHashTag.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.CheckboxHashTag.UseVisualStyleBackColor = true;
            // 
            // CheckboxAt
            // 
            this.CheckboxAt.AutoSize = true;
            this.CheckboxAt.Enabled = false;
            this.CheckboxAt.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CheckboxAt.Location = new System.Drawing.Point(605, 93);
            this.CheckboxAt.Name = "CheckboxAt";
            this.CheckboxAt.Size = new System.Drawing.Size(56, 30);
            this.CheckboxAt.TabIndex = 19;
            this.CheckboxAt.Text = "@";
            this.CheckboxAt.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.CheckboxAt.UseVisualStyleBackColor = true;
            // 
            // CheckboxBar
            // 
            this.CheckboxBar.AutoSize = true;
            this.CheckboxBar.Enabled = false;
            this.CheckboxBar.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CheckboxBar.Location = new System.Drawing.Point(549, 93);
            this.CheckboxBar.Name = "CheckboxBar";
            this.CheckboxBar.Size = new System.Drawing.Size(40, 30);
            this.CheckboxBar.TabIndex = 16;
            this.CheckboxBar.Text = "|";
            this.CheckboxBar.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.CheckboxBar.UseVisualStyleBackColor = true;
            // 
            // CheckboxEqual
            // 
            this.CheckboxEqual.AutoSize = true;
            this.CheckboxEqual.Enabled = false;
            this.CheckboxEqual.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CheckboxEqual.Location = new System.Drawing.Point(549, 188);
            this.CheckboxEqual.Name = "CheckboxEqual";
            this.CheckboxEqual.Size = new System.Drawing.Size(47, 30);
            this.CheckboxEqual.TabIndex = 15;
            this.CheckboxEqual.Text = "=";
            this.CheckboxEqual.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.CheckboxEqual.UseVisualStyleBackColor = true;
            // 
            // CheckboxCloseParenthesis
            // 
            this.CheckboxCloseParenthesis.AutoSize = true;
            this.CheckboxCloseParenthesis.Enabled = false;
            this.CheckboxCloseParenthesis.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CheckboxCloseParenthesis.Location = new System.Drawing.Point(945, 45);
            this.CheckboxCloseParenthesis.Name = "CheckboxCloseParenthesis";
            this.CheckboxCloseParenthesis.Size = new System.Drawing.Size(41, 30);
            this.CheckboxCloseParenthesis.TabIndex = 14;
            this.CheckboxCloseParenthesis.Text = ")";
            this.CheckboxCloseParenthesis.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.CheckboxCloseParenthesis.UseVisualStyleBackColor = true;
            // 
            // CheckboxOpenParenthesis
            // 
            this.CheckboxOpenParenthesis.AutoSize = true;
            this.CheckboxOpenParenthesis.Enabled = false;
            this.CheckboxOpenParenthesis.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CheckboxOpenParenthesis.Location = new System.Drawing.Point(888, 45);
            this.CheckboxOpenParenthesis.Name = "CheckboxOpenParenthesis";
            this.CheckboxOpenParenthesis.Size = new System.Drawing.Size(41, 30);
            this.CheckboxOpenParenthesis.TabIndex = 13;
            this.CheckboxOpenParenthesis.Text = "(";
            this.CheckboxOpenParenthesis.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.CheckboxOpenParenthesis.UseVisualStyleBackColor = true;
            // 
            // CheckboxBackSlash
            // 
            this.CheckboxBackSlash.AutoSize = true;
            this.CheckboxBackSlash.Enabled = false;
            this.CheckboxBackSlash.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CheckboxBackSlash.Location = new System.Drawing.Point(835, 45);
            this.CheckboxBackSlash.Name = "CheckboxBackSlash";
            this.CheckboxBackSlash.Size = new System.Drawing.Size(40, 30);
            this.CheckboxBackSlash.TabIndex = 12;
            this.CheckboxBackSlash.Text = "\\";
            this.CheckboxBackSlash.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.CheckboxBackSlash.UseVisualStyleBackColor = true;
            // 
            // CheckboxSlash
            // 
            this.CheckboxSlash.AutoSize = true;
            this.CheckboxSlash.Enabled = false;
            this.CheckboxSlash.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CheckboxSlash.Location = new System.Drawing.Point(781, 45);
            this.CheckboxSlash.Name = "CheckboxSlash";
            this.CheckboxSlash.Size = new System.Drawing.Size(40, 30);
            this.CheckboxSlash.TabIndex = 11;
            this.CheckboxSlash.Text = "/";
            this.CheckboxSlash.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.CheckboxSlash.UseVisualStyleBackColor = true;
            // 
            // CheckboxAmpersand
            // 
            this.CheckboxAmpersand.AutoSize = true;
            this.CheckboxAmpersand.Enabled = false;
            this.CheckboxAmpersand.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CheckboxAmpersand.Location = new System.Drawing.Point(726, 45);
            this.CheckboxAmpersand.Name = "CheckboxAmpersand";
            this.CheckboxAmpersand.Size = new System.Drawing.Size(49, 30);
            this.CheckboxAmpersand.TabIndex = 10;
            this.CheckboxAmpersand.Text = "&&";
            this.CheckboxAmpersand.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.CheckboxAmpersand.UseVisualStyleBackColor = true;
            // 
            // CheckboxPercentage
            // 
            this.CheckboxPercentage.AutoSize = true;
            this.CheckboxPercentage.Enabled = false;
            this.CheckboxPercentage.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CheckboxPercentage.Location = new System.Drawing.Point(668, 45);
            this.CheckboxPercentage.Name = "CheckboxPercentage";
            this.CheckboxPercentage.Size = new System.Drawing.Size(54, 30);
            this.CheckboxPercentage.TabIndex = 9;
            this.CheckboxPercentage.Text = "%";
            this.CheckboxPercentage.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.CheckboxPercentage.UseVisualStyleBackColor = true;
            // 
            // CheckboxDollar
            // 
            this.CheckboxDollar.AutoSize = true;
            this.CheckboxDollar.Enabled = false;
            this.CheckboxDollar.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CheckboxDollar.Location = new System.Drawing.Point(605, 45);
            this.CheckboxDollar.Name = "CheckboxDollar";
            this.CheckboxDollar.Size = new System.Drawing.Size(46, 30);
            this.CheckboxDollar.TabIndex = 8;
            this.CheckboxDollar.Text = "$";
            this.CheckboxDollar.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.CheckboxDollar.UseVisualStyleBackColor = true;
            // 
            // CheckBoxExclamation
            // 
            this.CheckBoxExclamation.AutoSize = true;
            this.CheckBoxExclamation.Enabled = false;
            this.CheckBoxExclamation.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CheckBoxExclamation.Location = new System.Drawing.Point(549, 45);
            this.CheckBoxExclamation.Name = "CheckBoxExclamation";
            this.CheckBoxExclamation.Size = new System.Drawing.Size(40, 30);
            this.CheckBoxExclamation.TabIndex = 5;
            this.CheckBoxExclamation.Text = "!";
            this.CheckBoxExclamation.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.CheckBoxExclamation.UseVisualStyleBackColor = true;
            // 
            // SymbolsCheckbox
            // 
            this.SymbolsCheckbox.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SymbolsCheckbox.Location = new System.Drawing.Point(528, 12);
            this.SymbolsCheckbox.Name = "SymbolsCheckbox";
            this.SymbolsCheckbox.Size = new System.Drawing.Size(431, 24);
            this.SymbolsCheckbox.TabIndex = 4;
            this.SymbolsCheckbox.Text = "Include Symbols: ";
            this.SymbolsCheckbox.UseVisualStyleBackColor = true;
            this.SymbolsCheckbox.CheckedChanged += new System.EventHandler(this.SymbolsCheckbox_CheckedChanged);
            // 
            // NumbersCheckbox
            // 
            this.NumbersCheckbox.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.NumbersCheckbox.Location = new System.Drawing.Point(14, 86);
            this.NumbersCheckbox.Name = "NumbersCheckbox";
            this.NumbersCheckbox.Size = new System.Drawing.Size(464, 24);
            this.NumbersCheckbox.TabIndex = 2;
            this.NumbersCheckbox.Text = "Include Numbers (e.g. 0123456789).";
            this.NumbersCheckbox.UseVisualStyleBackColor = true;
            // 
            // LowerCheckbox
            // 
            this.LowerCheckbox.Checked = true;
            this.LowerCheckbox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.LowerCheckbox.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LowerCheckbox.Location = new System.Drawing.Point(14, 49);
            this.LowerCheckbox.Name = "LowerCheckbox";
            this.LowerCheckbox.Size = new System.Drawing.Size(464, 24);
            this.LowerCheckbox.TabIndex = 1;
            this.LowerCheckbox.Text = "Include Lower Case Characters (e.g. abcdefgh).";
            this.LowerCheckbox.UseVisualStyleBackColor = true;
            // 
            // UpperCheckbox
            // 
            this.UpperCheckbox.Checked = true;
            this.UpperCheckbox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.UpperCheckbox.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.UpperCheckbox.Location = new System.Drawing.Point(14, 12);
            this.UpperCheckbox.Name = "UpperCheckbox";
            this.UpperCheckbox.Size = new System.Drawing.Size(488, 24);
            this.UpperCheckbox.TabIndex = 0;
            this.UpperCheckbox.Text = "Include Upper Case Characters (e.g. ABCDEFGH).";
            this.UpperCheckbox.UseVisualStyleBackColor = true;
            // 
            // PasswordTextBox
            // 
            this.PasswordTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PasswordTextBox.Location = new System.Drawing.Point(358, 403);
            this.PasswordTextBox.Multiline = true;
            this.PasswordTextBox.Name = "PasswordTextBox";
            this.PasswordTextBox.ReadOnly = true;
            this.PasswordTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.PasswordTextBox.Size = new System.Drawing.Size(686, 106);
            this.PasswordTextBox.TabIndex = 3;
            // 
            // DisplayPassLabel
            // 
            this.DisplayPassLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DisplayPassLabel.Location = new System.Drawing.Point(20, 403);
            this.DisplayPassLabel.Name = "DisplayPassLabel";
            this.DisplayPassLabel.Size = new System.Drawing.Size(332, 24);
            this.DisplayPassLabel.TabIndex = 4;
            this.DisplayPassLabel.Text = "You(r) password(s) will appear here: ";
            // 
            // CopyClipboardButton
            // 
            this.CopyClipboardButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.CopyClipboardButton.FlatAppearance.BorderSize = 0;
            this.CopyClipboardButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.CopyClipboardButton.Location = new System.Drawing.Point(1057, 417);
            this.CopyClipboardButton.Name = "CopyClipboardButton";
            this.CopyClipboardButton.Size = new System.Drawing.Size(64, 64);
            this.CopyClipboardButton.TabIndex = 5;
            this.ClipboardToolTip.SetToolTip(this.CopyClipboardButton, "Copy all passwords to clipboard.");
            this.CopyClipboardButton.UseVisualStyleBackColor = true;
            this.CopyClipboardButton.Click += new System.EventHandler(this.CopyClipboardButton_Click);
            // 
            // ClipboardToolTip
            // 
            this.ClipboardToolTip.AutomaticDelay = 300;
            this.ClipboardToolTip.AutoPopDelay = 5000;
            this.ClipboardToolTip.InitialDelay = 300;
            this.ClipboardToolTip.ReshowDelay = 60;
            // 
            // panel1
            // 
            this.panel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.panel1.Controls.Add(this.PercentageLabel);
            this.panel1.Controls.Add(this.CheckBoxesPanel);
            this.panel1.Controls.Add(this.CopyClipboardButton);
            this.panel1.Controls.Add(this.TitleLabel);
            this.panel1.Controls.Add(this.DisplayPassLabel);
            this.panel1.Controls.Add(this.PasswordTextBox);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1144, 526);
            this.panel1.TabIndex = 54;
            // 
            // PercentageLabel
            // 
            this.PercentageLabel.AutoSize = true;
            this.PercentageLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PercentageLabel.Location = new System.Drawing.Point(7, 452);
            this.PercentageLabel.Name = "PercentageLabel";
            this.PercentageLabel.Size = new System.Drawing.Size(0, 15);
            this.PercentageLabel.TabIndex = 6;
            this.PercentageLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // SelectAllSymbolsButton
            // 
            this.SelectAllSymbolsButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.SelectAllSymbolsButton.Enabled = false;
            this.SelectAllSymbolsButton.FlatAppearance.BorderSize = 0;
            this.SelectAllSymbolsButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.SelectAllSymbolsButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SelectAllSymbolsButton.Location = new System.Drawing.Point(837, 188);
            this.SelectAllSymbolsButton.Name = "SelectAllSymbolsButton";
            this.SelectAllSymbolsButton.Size = new System.Drawing.Size(218, 35);
            this.SelectAllSymbolsButton.TabIndex = 57;
            this.SelectAllSymbolsButton.Text = "Select All Symbols";
            this.SelectAllSymbolsButton.UseVisualStyleBackColor = true;
            this.SelectAllSymbolsButton.Click += new System.EventHandler(this.SelectAllSymbolsButton_Click);
            this.SelectAllSymbolsButton.MouseEnter += new System.EventHandler(this.SelectAllSymbolsButton_MouseEnter);
            this.SelectAllSymbolsButton.MouseLeave += new System.EventHandler(this.SelectAllSymbolsButton_MouseLeave);
            // 
            // CreateQuickPassUC
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.Controls.Add(this.panel1);
            this.Name = "CreateQuickPassUC";
            this.Size = new System.Drawing.Size(1144, 526);
            this.Load += new System.EventHandler(this.CreateQuickPassUC_Load);
            this.CheckBoxesPanel.ResumeLayout(false);
            this.CheckBoxesPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PassLengthNUD)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.NPasswordsNUD)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button GenPassButton;
        private System.Windows.Forms.Label TitleLabel;
        private System.Windows.Forms.Panel CheckBoxesPanel;
        private System.Windows.Forms.CheckBox NumbersCheckbox;
        private System.Windows.Forms.CheckBox LowerCheckbox;
        private System.Windows.Forms.CheckBox UpperCheckbox;
        private System.Windows.Forms.CheckBox SymbolsCheckbox;
        private System.Windows.Forms.CheckBox CheckboxDollar;
        private System.Windows.Forms.CheckBox CheckBoxExclamation;
        private System.Windows.Forms.CheckBox CheckboxBackSlash;
        private System.Windows.Forms.CheckBox CheckboxSlash;
        private System.Windows.Forms.CheckBox CheckboxAmpersand;
        private System.Windows.Forms.CheckBox CheckboxPercentage;
        private System.Windows.Forms.Label PassGenerateLabel;
        private System.Windows.Forms.NumericUpDown NPasswordsNUD;
        private System.Windows.Forms.CheckBox CheckboxHigherEne;
        private System.Windows.Forms.CheckBox CheckboxLowerEne;
        private System.Windows.Forms.CheckBox CheckboxOpenExclamation;
        private System.Windows.Forms.CheckBox CheckboxSemicolon;
        private System.Windows.Forms.CheckBox CheckboxComma;
        private System.Windows.Forms.CheckBox CheckboxUnderscore;
        private System.Windows.Forms.CheckBox CheckboxColon;
        private System.Windows.Forms.CheckBox CheckboxPeriod;
        private System.Windows.Forms.CheckBox CheckboxLowerCaseTurkish;
        private System.Windows.Forms.CheckBox CheckboxCapitalTurkish;
        private System.Windows.Forms.CheckBox CheckboxMultiplier;
        private System.Windows.Forms.CheckBox CheckboxSubstract;
        private System.Windows.Forms.CheckBox CheckboxAdd;
        private System.Windows.Forms.CheckBox CheckboxCloseCurlyBracket;
        private System.Windows.Forms.CheckBox CheckboxOpenCurlyBracket;
        private System.Windows.Forms.CheckBox CheckboxCloseSquareBracket;
        private System.Windows.Forms.CheckBox CheckboxOpenSquareBracket;
        private System.Windows.Forms.CheckBox CheckboxHigherThan;
        private System.Windows.Forms.CheckBox CheckboxLessThan;
        private System.Windows.Forms.CheckBox CheckboxEuro;
        private System.Windows.Forms.CheckBox CheckboxHashTag;
        private System.Windows.Forms.CheckBox CheckboxAt;
        private System.Windows.Forms.CheckBox CheckboxBar;
        private System.Windows.Forms.CheckBox CheckboxEqual;
        private System.Windows.Forms.CheckBox CheckboxCloseParenthesis;
        private System.Windows.Forms.CheckBox CheckboxOpenParenthesis;
        private System.Windows.Forms.NumericUpDown PassLengthNUD;
        private System.Windows.Forms.Label PassLengthLabel;
        private System.Windows.Forms.TextBox PasswordTextBox;
        private System.Windows.Forms.Label DisplayPassLabel;
        private System.Windows.Forms.Button CopyClipboardButton;
        private System.Windows.Forms.CheckBox CheckboxCloseQuestion;
        private System.Windows.Forms.CheckBox CheckboxOpenQuestion;
        private System.Windows.Forms.ToolTip ClipboardToolTip;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.CheckBox CheckPwnageCheckbox;
        private System.Windows.Forms.Button InfoPwnageButton;
        private System.Windows.Forms.Label PercentageLabel;
        private System.Windows.Forms.Button SelectAllSymbolsButton;
    }
}
