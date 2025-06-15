namespace VirtualEvent_SE410
{
    partial class ManageScreen
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
            components = new System.ComponentModel.Container();
            backToMenuButton = new Button();
            dataGridView1 = new DataGridView();
            deleteButton = new Button();
            filterButton = new Button();
            contextMenuStrip1 = new ContextMenuStrip(components);
            titleText = new TextBox();
            categoryText = new TextBox();
            label1 = new Label();
            label3 = new Label();
            eventStartDatePicker = new DateTimePicker();
            eventStart = new Label();
            durationText = new TextBox();
            label2 = new Label();
            label4 = new Label();
            slotNumText = new TextBox();
            deadlineDatePicker = new DateTimePicker();
            label5 = new Label();
            resetFilterButton = new Button();
            editButton = new Button();
            textBox1 = new TextBox();
            label6 = new Label();
            label7 = new Label();
            totalFinderButton = new Button();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            SuspendLayout();
            // 
            // backToMenuButton
            // 
            backToMenuButton.BackColor = Color.DarkCyan;
            backToMenuButton.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 162);
            backToMenuButton.ForeColor = SystemColors.ButtonHighlight;
            backToMenuButton.Location = new Point(29, 563);
            backToMenuButton.Margin = new Padding(2);
            backToMenuButton.Name = "backToMenuButton";
            backToMenuButton.Size = new Size(242, 36);
            backToMenuButton.TabIndex = 1;
            backToMenuButton.Text = "Back to The Main Menu";
            backToMenuButton.UseVisualStyleBackColor = false;
            backToMenuButton.Click += backToMenuButton_Click;
            // 
            // dataGridView1
            // 
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Location = new Point(29, 95);
            dataGridView1.Margin = new Padding(2);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.RowHeadersWidth = 62;
            dataGridView1.Size = new Size(1093, 454);
            dataGridView1.TabIndex = 2;
            // 
            // deleteButton
            // 
            deleteButton.BackColor = Color.DarkCyan;
            deleteButton.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 162);
            deleteButton.ForeColor = SystemColors.ButtonHighlight;
            deleteButton.Location = new Point(1014, 564);
            deleteButton.Margin = new Padding(2);
            deleteButton.Name = "deleteButton";
            deleteButton.Size = new Size(108, 36);
            deleteButton.TabIndex = 3;
            deleteButton.Text = "Delete";
            deleteButton.UseVisualStyleBackColor = false;
            deleteButton.Click += deleteButton_Click;
            // 
            // filterButton
            // 
            filterButton.BackColor = Color.DarkCyan;
            filterButton.Font = new Font("Arial Rounded MT Bold", 10.2F, FontStyle.Regular, GraphicsUnit.Point, 0);
            filterButton.ForeColor = SystemColors.ButtonFace;
            filterButton.Location = new Point(1211, 73);
            filterButton.Name = "filterButton";
            filterButton.Size = new Size(147, 34);
            filterButton.TabIndex = 4;
            filterButton.Text = "Filter";
            filterButton.UseVisualStyleBackColor = false;
            filterButton.Click += filterButton_Click;
            // 
            // contextMenuStrip1
            // 
            contextMenuStrip1.ImageScalingSize = new Size(20, 20);
            contextMenuStrip1.Name = "contextMenuStrip1";
            contextMenuStrip1.Size = new Size(61, 4);
            // 
            // titleText
            // 
            titleText.BackColor = Color.MintCream;
            titleText.Location = new Point(29, 43);
            titleText.Name = "titleText";
            titleText.Size = new Size(125, 27);
            titleText.TabIndex = 6;
            // 
            // categoryText
            // 
            categoryText.BackColor = Color.MintCream;
            categoryText.Location = new Point(182, 43);
            categoryText.Name = "categoryText";
            categoryText.Size = new Size(125, 27);
            categoryText.TabIndex = 7;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Arial Rounded MT Bold", 10.2F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label1.Location = new Point(29, 20);
            label1.Name = "label1";
            label1.Size = new Size(46, 20);
            label1.TabIndex = 8;
            label1.Text = "Title";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 162);
            label3.Location = new Point(182, 20);
            label3.Name = "label3";
            label3.Size = new Size(84, 23);
            label3.TabIndex = 9;
            label3.Text = "Category";
            label3.Click += label3_Click;
            // 
            // eventStartDatePicker
            // 
            eventStartDatePicker.Location = new Point(336, 43);
            eventStartDatePicker.Name = "eventStartDatePicker";
            eventStartDatePicker.ShowCheckBox = true;
            eventStartDatePicker.Size = new Size(250, 27);
            eventStartDatePicker.TabIndex = 10;
            // 
            // eventStart
            // 
            eventStart.AutoSize = true;
            eventStart.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 162);
            eventStart.Location = new Point(336, 18);
            eventStart.Name = "eventStart";
            eventStart.Size = new Size(142, 23);
            eventStart.TabIndex = 11;
            eventStart.Text = "Event Start Date";
            // 
            // durationText
            // 
            durationText.BackColor = Color.MintCream;
            durationText.Location = new Point(608, 43);
            durationText.Name = "durationText";
            durationText.Size = new Size(81, 27);
            durationText.TabIndex = 12;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 162);
            label2.Location = new Point(608, 19);
            label2.Name = "label2";
            label2.Size = new Size(81, 23);
            label2.TabIndex = 13;
            label2.Text = "Duration";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 162);
            label4.Location = new Point(750, 18);
            label4.Name = "label4";
            label4.Size = new Size(145, 23);
            label4.TabIndex = 14;
            label4.Text = "Participant Limit";
            // 
            // slotNumText
            // 
            slotNumText.BackColor = Color.MintCream;
            slotNumText.Location = new Point(750, 44);
            slotNumText.Name = "slotNumText";
            slotNumText.Size = new Size(145, 27);
            slotNumText.TabIndex = 15;
            // 
            // deadlineDatePicker
            // 
            deadlineDatePicker.Location = new Point(925, 45);
            deadlineDatePicker.Name = "deadlineDatePicker";
            deadlineDatePicker.ShowCheckBox = true;
            deadlineDatePicker.Size = new Size(250, 27);
            deadlineDatePicker.TabIndex = 16;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 162);
            label5.Location = new Point(925, 19);
            label5.Name = "label5";
            label5.Size = new Size(233, 23);
            label5.TabIndex = 17;
            label5.Text = "Event Registration Deadline";
            // 
            // resetFilterButton
            // 
            resetFilterButton.BackColor = Color.DarkCyan;
            resetFilterButton.Font = new Font("Arial Rounded MT Bold", 10.2F, FontStyle.Regular, GraphicsUnit.Point, 0);
            resetFilterButton.ForeColor = SystemColors.ButtonHighlight;
            resetFilterButton.Location = new Point(1211, 18);
            resetFilterButton.Name = "resetFilterButton";
            resetFilterButton.Size = new Size(147, 36);
            resetFilterButton.TabIndex = 18;
            resetFilterButton.Text = "Reset Filters";
            resetFilterButton.UseVisualStyleBackColor = false;
            resetFilterButton.Click += resetFilterButton_Click;
            // 
            // editButton
            // 
            editButton.BackColor = Color.DarkCyan;
            editButton.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 162);
            editButton.ForeColor = SystemColors.ButtonHighlight;
            editButton.Location = new Point(289, 564);
            editButton.Name = "editButton";
            editButton.Size = new Size(158, 35);
            editButton.TabIndex = 19;
            editButton.Text = "Edit Event";
            editButton.UseVisualStyleBackColor = false;
            editButton.Click += editButton_Click;
            // 
            // textBox1
            // 
            textBox1.Location = new Point(1126, 257);
            textBox1.Margin = new Padding(2, 2, 2, 2);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(166, 27);
            textBox1.TabIndex = 20;
            textBox1.TextChanged += textBox1_TextChanged;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("Arial Rounded MT Bold", 10.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label6.Location = new Point(1126, 187);
            label6.Margin = new Padding(2, 0, 2, 0);
            label6.Name = "label6";
            label6.Size = new Size(206, 21);
            label6.TabIndex = 21;
            label6.Text = "Total Capacity Search";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Font = new Font("Arial Rounded MT Bold", 10.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label7.Location = new Point(1126, 222);
            label7.Margin = new Padding(2, 0, 2, 0);
            label7.Name = "label7";
            label7.Size = new Size(250, 21);
            label7.TabIndex = 22;
            label7.Text = "Enter the Related Category";
            // 
            // totalFinderButton
            // 
            totalFinderButton.BackColor = Color.DarkCyan;
            totalFinderButton.Font = new Font("Arial Rounded MT Bold", 10.2F, FontStyle.Regular, GraphicsUnit.Point, 0);
            totalFinderButton.ForeColor = SystemColors.ButtonHighlight;
            totalFinderButton.Location = new Point(1126, 298);
            totalFinderButton.Margin = new Padding(2, 2, 2, 2);
            totalFinderButton.Name = "totalFinderButton";
            totalFinderButton.Size = new Size(117, 35);
            totalFinderButton.TabIndex = 23;
            totalFinderButton.Text = "Find Total";
            totalFinderButton.UseVisualStyleBackColor = false;
            totalFinderButton.Click += totalFinderButton_Click;
            // 
            // ManageScreen
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1384, 600);
            Controls.Add(totalFinderButton);
            Controls.Add(label7);
            Controls.Add(label6);
            Controls.Add(textBox1);
            Controls.Add(editButton);
            Controls.Add(resetFilterButton);
            Controls.Add(label5);
            Controls.Add(deadlineDatePicker);
            Controls.Add(slotNumText);
            Controls.Add(label4);
            Controls.Add(label2);
            Controls.Add(durationText);
            Controls.Add(eventStart);
            Controls.Add(eventStartDatePicker);
            Controls.Add(label3);
            Controls.Add(label1);
            Controls.Add(categoryText);
            Controls.Add(titleText);
            Controls.Add(filterButton);
            Controls.Add(deleteButton);
            Controls.Add(dataGridView1);
            Controls.Add(backToMenuButton);
            Margin = new Padding(2);
            Name = "ManageScreen";
            Text = "ManageScreen";
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Button backToMenuButton;
        private DataGridView dataGridView1;
        private Button deleteButton;
        private Button filterButton;
        private ContextMenuStrip contextMenuStrip1;
        private TextBox titleText;
        private TextBox categoryText;
        private Label label1;
        private Label label3;
        private DateTimePicker eventStartDatePicker;
        private Label eventStart;
        private TextBox durationText;
        private Label label2;
        private Label label4;
        private TextBox slotNumText;
        private DateTimePicker deadlineDatePicker;
        private Label label5;
        private Button resetFilterButton;
        private Button editButton;
        private TextBox textBox1;
        private Label label6;
        private Label label7;
        private Button totalFinderButton;
    }
}