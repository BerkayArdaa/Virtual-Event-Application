namespace VirtualEvent_SE410
{
    partial class Form2
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            label1 = new Label();
            addEventButton = new Button();
            manageButton = new Button();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Arial Rounded MT Bold", 19.8000011F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label1.Location = new Point(215, 30);
            label1.Margin = new Padding(2, 0, 2, 0);
            label1.Name = "label1";
            label1.Size = new Size(199, 39);
            label1.TabIndex = 0;
            label1.Text = "Dashboard";
            // 
            // addEventButton
            // 
            addEventButton.BackColor = Color.DarkCyan;
            addEventButton.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 162);
            addEventButton.ForeColor = SystemColors.ButtonHighlight;
            addEventButton.Location = new Point(215, 114);
            addEventButton.Margin = new Padding(2);
            addEventButton.Name = "addEventButton";
            addEventButton.Size = new Size(180, 46);
            addEventButton.TabIndex = 1;
            addEventButton.Text = "Create Event";
            addEventButton.UseVisualStyleBackColor = false;
            addEventButton.Click += addEventButton_Click;
            // 
            // manageButton
            // 
            manageButton.BackColor = Color.DarkCyan;
            manageButton.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 162);
            manageButton.ForeColor = SystemColors.ButtonFace;
            manageButton.Location = new Point(215, 178);
            manageButton.Margin = new Padding(2);
            manageButton.Name = "manageButton";
            manageButton.Size = new Size(180, 49);
            manageButton.TabIndex = 2;
            manageButton.Text = "Manage Events";
            manageButton.UseVisualStyleBackColor = false;
            manageButton.Click += manageButton_Click;
            // 
            // Form2
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(640, 360);
            Controls.Add(manageButton);
            Controls.Add(addEventButton);
            Controls.Add(label1);
            Margin = new Padding(2);
            Name = "Form2";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Dashboard";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Button addEventButton;
        private Button manageButton;
    }
}