namespace VirtualEvent_SE410
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            button1 = new Button();
            userMailText = new TextBox();
            passwordText = new TextBox();
            passwordL = new Label();
            userNameL = new Label();
            label1 = new Label();
            SuspendLayout();
            // 
            // button1
            // 
            button1.BackColor = Color.DarkCyan;
            button1.ForeColor = SystemColors.ButtonHighlight;
            button1.Location = new Point(311, 289);
            button1.Margin = new Padding(2);
            button1.Name = "button1";
            button1.Size = new Size(106, 38);
            button1.TabIndex = 0;
            button1.Text = "Login";
            button1.UseVisualStyleBackColor = false;
            button1.Click += button1_Click;
            // 
            // userMailText
            // 
            userMailText.BackColor = Color.MintCream;
            userMailText.Location = new Point(224, 151);
            userMailText.Margin = new Padding(2);
            userMailText.Name = "userMailText";
            userMailText.Size = new Size(278, 25);
            userMailText.TabIndex = 1;
            // 
            // passwordText
            // 
            passwordText.BackColor = Color.MintCream;
            passwordText.Location = new Point(224, 232);
            passwordText.Margin = new Padding(2);
            passwordText.Name = "passwordText";
            passwordText.Size = new Size(278, 25);
            passwordText.TabIndex = 2;
            passwordText.TextChanged += passwordText_TextChanged;
            // 
            // passwordL
            // 
            passwordL.AutoSize = true;
            passwordL.Font = new Font("Segoe UI", 10.8F, FontStyle.Bold, GraphicsUnit.Point, 162);
            passwordL.Location = new Point(316, 205);
            passwordL.Margin = new Padding(2, 0, 2, 0);
            passwordL.Name = "passwordL";
            passwordL.Size = new Size(92, 25);
            passwordL.TabIndex = 3;
            passwordL.Text = "Password";
            // 
            // userNameL
            // 
            userNameL.AutoSize = true;
            userNameL.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 162);
            userNameL.Location = new Point(334, 126);
            userNameL.Margin = new Padding(2, 0, 2, 0);
            userNameL.Name = "userNameL";
            userNameL.Size = new Size(54, 23);
            userNameL.TabIndex = 4;
            userNameL.Text = "Email";
            userNameL.Click += userNameL_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.BackColor = Color.White;
            label1.Font = new Font("Arial Rounded MT Bold", 18F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label1.ForeColor = SystemColors.ActiveCaptionText;
            label1.Location = new Point(101, 52);
            label1.Margin = new Padding(2, 0, 2, 0);
            label1.Name = "label1";
            label1.Size = new Size(564, 34);
            label1.TabIndex = 5;
            label1.Text = "Virtual Event Management System V2";
            label1.TextAlign = ContentAlignment.MiddleCenter;
            label1.Click += label1_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(9F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(731, 437);
            Controls.Add(label1);
            Controls.Add(userNameL);
            Controls.Add(passwordL);
            Controls.Add(passwordText);
            Controls.Add(userMailText);
            Controls.Add(button1);
            Cursor = Cursors.Hand;
            Font = new Font("Arial Rounded MT Bold", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            Margin = new Padding(2);
            Name = "Form1";
            Text = "Login";
            Load += Login_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button button1;
        private TextBox userMailText;
        private TextBox passwordText;
        private Label passwordL;
        private Label userNameL;
        private Label label1;
    }
}
