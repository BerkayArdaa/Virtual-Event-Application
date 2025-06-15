namespace VirtualEvent_SE410
{
    partial class AddEventForm
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
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            label5 = new Label();
            label6 = new Label();
            descriptionBox = new RichTextBox();
            timePicker = new DateTimePicker();
            deadlinePicker = new DateTimePicker();
            titleText = new TextBox();
            categoryText = new TextBox();
            slotText = new TextBox();
            addNewEventButton = new Button();
            time2Picker = new DateTimePicker();
            deadline2Picker = new DateTimePicker();
            label7 = new Label();
            durationText = new TextBox();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Arial Rounded MT Bold", 16.2F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label1.Location = new Point(34, 39);
            label1.Margin = new Padding(2, 0, 2, 0);
            label1.Name = "label1";
            label1.Size = new Size(84, 32);
            label1.TabIndex = 0;
            label1.Text = "Title:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Arial Rounded MT Bold", 16.2F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label2.Location = new Point(34, 186);
            label2.Margin = new Padding(2, 0, 2, 0);
            label2.Name = "label2";
            label2.Size = new Size(150, 32);
            label2.TabIndex = 1;
            label2.Text = "Category:";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Arial Rounded MT Bold", 16.2F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label3.Location = new Point(34, 430);
            label3.Margin = new Padding(2, 0, 2, 0);
            label3.Name = "label3";
            label3.Size = new Size(322, 32);
            label3.TabIndex = 2;
            label3.Text = "Registration Deadline:";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Arial Rounded MT Bold", 16.2F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label4.Location = new Point(34, 113);
            label4.Margin = new Padding(2, 0, 2, 0);
            label4.Name = "label4";
            label4.Size = new Size(183, 32);
            label4.TabIndex = 3;
            label4.Text = "Description:";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Arial Rounded MT Bold", 16.2F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label5.Location = new Point(34, 362);
            label5.Margin = new Padding(2, 0, 2, 0);
            label5.Name = "label5";
            label5.Size = new Size(222, 32);
            label5.TabIndex = 4;
            label5.Text = "Date and Time:";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("Arial Rounded MT Bold", 16.2F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label6.Location = new Point(34, 245);
            label6.Margin = new Padding(2, 0, 2, 0);
            label6.Name = "label6";
            label6.Size = new Size(281, 32);
            label6.TabIndex = 5;
            label6.Text = "Available Capacity:";
            // 
            // descriptionBox
            // 
            descriptionBox.BackColor = Color.MintCream;
            descriptionBox.Location = new Point(361, 81);
            descriptionBox.Margin = new Padding(2, 2, 2, 2);
            descriptionBox.Name = "descriptionBox";
            descriptionBox.Size = new Size(346, 84);
            descriptionBox.TabIndex = 6;
            descriptionBox.Text = "";
            descriptionBox.TextChanged += descriptionBox_TextChanged;
            // 
            // timePicker
            // 
            timePicker.CustomFormat = "dd.MM.yyyy HH:mm";
            timePicker.Location = new Point(360, 367);
            timePicker.Margin = new Padding(2, 2, 2, 2);
            timePicker.Name = "timePicker";
            timePicker.Size = new Size(241, 27);
            timePicker.TabIndex = 7;
            // 
            // deadlinePicker
            // 
            deadlinePicker.CustomFormat = "dd.MM.yyyy HH:mm";
            deadlinePicker.Location = new Point(360, 430);
            deadlinePicker.Margin = new Padding(2, 2, 2, 2);
            deadlinePicker.Name = "deadlinePicker";
            deadlinePicker.Size = new Size(241, 27);
            deadlinePicker.TabIndex = 8;
            // 
            // titleText
            // 
            titleText.BackColor = Color.MintCream;
            titleText.Location = new Point(361, 39);
            titleText.Margin = new Padding(2, 2, 2, 2);
            titleText.Name = "titleText";
            titleText.Size = new Size(346, 27);
            titleText.TabIndex = 9;
            // 
            // categoryText
            // 
            categoryText.BackColor = Color.MintCream;
            categoryText.Location = new Point(361, 186);
            categoryText.Margin = new Padding(2, 2, 2, 2);
            categoryText.Name = "categoryText";
            categoryText.Size = new Size(346, 27);
            categoryText.TabIndex = 11;
            // 
            // slotText
            // 
            slotText.BackColor = Color.MintCream;
            slotText.Location = new Point(360, 245);
            slotText.Margin = new Padding(2, 2, 2, 2);
            slotText.Name = "slotText";
            slotText.Size = new Size(346, 27);
            slotText.TabIndex = 12;
            // 
            // addNewEventButton
            // 
            addNewEventButton.BackColor = Color.DarkCyan;
            addNewEventButton.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 162);
            addNewEventButton.ForeColor = SystemColors.ButtonHighlight;
            addNewEventButton.Location = new Point(743, 423);
            addNewEventButton.Margin = new Padding(2, 2, 2, 2);
            addNewEventButton.Name = "addNewEventButton";
            addNewEventButton.Size = new Size(179, 43);
            addNewEventButton.TabIndex = 13;
            addNewEventButton.Text = "Add New Event";
            addNewEventButton.UseVisualStyleBackColor = false;
            addNewEventButton.Click += addEventButton_Click;
            // 
            // time2Picker
            // 
            time2Picker.Format = DateTimePickerFormat.Time;
            time2Picker.Location = new Point(621, 362);
            time2Picker.Margin = new Padding(2, 2, 2, 2);
            time2Picker.Name = "time2Picker";
            time2Picker.ShowUpDown = true;
            time2Picker.Size = new Size(86, 27);
            time2Picker.TabIndex = 14;
            // 
            // deadline2Picker
            // 
            deadline2Picker.Format = DateTimePickerFormat.Time;
            deadline2Picker.Location = new Point(621, 430);
            deadline2Picker.Margin = new Padding(2, 2, 2, 2);
            deadline2Picker.Name = "deadline2Picker";
            deadline2Picker.ShowUpDown = true;
            deadline2Picker.Size = new Size(86, 27);
            deadline2Picker.TabIndex = 16;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Font = new Font("Arial Rounded MT Bold", 16.2F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label7.Location = new Point(34, 305);
            label7.Margin = new Padding(2, 0, 2, 0);
            label7.Name = "label7";
            label7.Size = new Size(276, 32);
            label7.TabIndex = 17;
            label7.Text = "Duration (Minutes):";
            // 
            // durationText
            // 
            durationText.BackColor = Color.MintCream;
            durationText.Location = new Point(360, 305);
            durationText.Margin = new Padding(2, 2, 2, 2);
            durationText.Name = "durationText";
            durationText.Size = new Size(346, 27);
            durationText.TabIndex = 18;
            // 
            // AddEventForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(933, 501);
            Controls.Add(durationText);
            Controls.Add(label7);
            Controls.Add(deadline2Picker);
            Controls.Add(time2Picker);
            Controls.Add(addNewEventButton);
            Controls.Add(slotText);
            Controls.Add(categoryText);
            Controls.Add(titleText);
            Controls.Add(deadlinePicker);
            Controls.Add(timePicker);
            Controls.Add(descriptionBox);
            Controls.Add(label6);
            Controls.Add(label5);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Margin = new Padding(2, 2, 2, 2);
            Name = "AddEventForm";
            Text = "AddEventForm";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private Label label6;
        private RichTextBox descriptionBox;
        private DateTimePicker timePicker;
        private DateTimePicker deadlinePicker;
        private TextBox titleText;
        private TextBox descriptionText;
        private TextBox categoryText;
        private TextBox slotText;
        private Button addNewEventButton;
        private DateTimePicker time2Picker;
        private DateTimePicker deadline2Picker;
        private Label label7;
        private TextBox durationText;
    }
}