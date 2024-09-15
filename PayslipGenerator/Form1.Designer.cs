namespace PayslipGenerator
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
            this.label2 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtDaysIn = new System.Windows.Forms.TextBox();
            this.cmbEmp = new System.Windows.Forms.ComboBox();
            this.button1 = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.txtdailyRate = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtAbsents = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.txtOthers = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtAdjustments = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.txtWorkingDays = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(73, 158);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(126, 20);
            this.label2.TabIndex = 1;
            this.label2.Text = "Employee Name :";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(73, 192);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(88, 20);
            this.label4.TabIndex = 3;
            this.label4.Text = "Daily Rate : ";
            // 
            // txtDaysIn
            // 
            this.txtDaysIn.Location = new System.Drawing.Point(291, 255);
            this.txtDaysIn.Name = "txtDaysIn";
            this.txtDaysIn.Size = new System.Drawing.Size(214, 27);
            this.txtDaysIn.TabIndex = 4;
            // 
            // cmbEmp
            // 
            this.cmbEmp.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbEmp.FormattingEnabled = true;
            this.cmbEmp.Location = new System.Drawing.Point(291, 155);
            this.cmbEmp.Name = "cmbEmp";
            this.cmbEmp.Size = new System.Drawing.Size(214, 28);
            this.cmbEmp.TabIndex = 1;
            this.cmbEmp.SelectedIndexChanged += new System.EventHandler(this.cmbEmp_SelectedIndexChanged);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(210, 407);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(169, 29);
            this.button1.TabIndex = 8;
            this.button1.Text = "Generate";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(12, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(187, 143);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 10;
            this.pictureBox1.TabStop = false;
            // 
            // txtdailyRate
            // 
            this.txtdailyRate.Location = new System.Drawing.Point(291, 189);
            this.txtdailyRate.Name = "txtdailyRate";
            this.txtdailyRate.Size = new System.Drawing.Size(214, 27);
            this.txtdailyRate.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(73, 258);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(107, 20);
            this.label1.TabIndex = 11;
            this.label1.Text = "Days Worked : ";
            // 
            // txtAbsents
            // 
            this.txtAbsents.Location = new System.Drawing.Point(291, 288);
            this.txtAbsents.Name = "txtAbsents";
            this.txtAbsents.Size = new System.Drawing.Size(214, 27);
            this.txtAbsents.TabIndex = 5;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(73, 291);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(78, 20);
            this.label9.TabIndex = 13;
            this.label9.Text = "Absences :";
            // 
            // txtOthers
            // 
            this.txtOthers.Location = new System.Drawing.Point(291, 354);
            this.txtOthers.Name = "txtOthers";
            this.txtOthers.Size = new System.Drawing.Size(214, 27);
            this.txtOthers.TabIndex = 7;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(73, 357);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(132, 20);
            this.label10.TabIndex = 15;
            this.label10.Text = "Other Deductions :";
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label3.ForeColor = System.Drawing.Color.Black;
            this.label3.Location = new System.Drawing.Point(199, 46);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(316, 51);
            this.label3.TabIndex = 17;
            this.label3.Text = "PAYSLIP GENERATOR";
            // 
            // txtAdjustments
            // 
            this.txtAdjustments.Location = new System.Drawing.Point(291, 321);
            this.txtAdjustments.Name = "txtAdjustments";
            this.txtAdjustments.Size = new System.Drawing.Size(214, 27);
            this.txtAdjustments.TabIndex = 6;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(73, 324);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(170, 20);
            this.label11.TabIndex = 18;
            this.label11.Text = "Adjustments (Holidays) :";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(73, 225);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(141, 20);
            this.label12.TabIndex = 21;
            this.label12.Text = "Total Days of Work :";
            // 
            // txtWorkingDays
            // 
            this.txtWorkingDays.Location = new System.Drawing.Point(291, 222);
            this.txtWorkingDays.Name = "txtWorkingDays";
            this.txtWorkingDays.Size = new System.Drawing.Size(214, 27);
            this.txtWorkingDays.TabIndex = 3;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.SpringGreen;
            this.ClientSize = new System.Drawing.Size(578, 469);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.txtWorkingDays);
            this.Controls.Add(this.txtAdjustments);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtOthers);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.txtAbsents);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.txtdailyRate);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.cmbEmp);
            this.Controls.Add(this.txtDaysIn);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label2);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private Label label2;
        private Label label4;
        private TextBox txtDaysIn;
        private TextBox textBox4;
        private TextBox textBox5;
        private ComboBox cmbEmp;
        private Label label5;
        private Label label6;
        private Label label7;
        private Label label8;
        private TextBox textBox6;
        private Button button1;
        private PrintPreviewControl printPreviewControl1;
        private PictureBox pictureBox1;
        private TextBox txtdailyRate;
        private Label label1;
        private TextBox txtAbsents;
        private Label label9;
        private TextBox txtOthers;
        private Label label10;
        private Label label3;
        private TextBox txtAdjustments;
        private Label label11;
        private Label label12;
        private TextBox txtWorkingDays;
    }
}