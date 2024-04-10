using AbinLibs;

namespace FontAwesomeExtractor
{
	partial class MainForm
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
			this.lblIcon = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.btnExtract = new System.Windows.Forms.Button();
			this.pictureBox1 = new System.Windows.Forms.PictureBox();
			this.label4 = new System.Windows.Forms.Label();
			this.numCornerRadius = new System.Windows.Forms.NumericUpDown();
			this.btnForeColor = new System.Windows.Forms.Button();
			this.cmbFontName = new System.Windows.Forms.ComboBox();
			this.btnBackgroundColor = new System.Windows.Forms.Button();
			this.btnSave = new System.Windows.Forms.Button();
			this.label5 = new System.Windows.Forms.Label();
			this.panel1 = new System.Windows.Forms.Panel();
			this.ddlFontSize = new System.Windows.Forms.ComboBox();
			this.label6 = new System.Windows.Forms.Label();
			this.lnkFontAwesome = new System.Windows.Forms.LinkLabel();
			this.chkBackTransparent = new System.Windows.Forms.CheckBox();
			this.chkForeTransparent = new System.Windows.Forms.CheckBox();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numCornerRadius)).BeginInit();
			this.panel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// lblIcon
			// 
			this.lblIcon.BackColor = System.Drawing.SystemColors.Control;
			this.lblIcon.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.lblIcon.Location = new System.Drawing.Point(3, 4);
			this.lblIcon.Name = "lblIcon";
			this.lblIcon.Size = new System.Drawing.Size(128, 128);
			this.lblIcon.TabIndex = 0;
			this.lblIcon.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.lblIcon.Visible = false;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(15, 20);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(60, 13);
			this.label1.TabIndex = 0;
			this.label1.Text = "Font name:";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(15, 54);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(57, 13);
			this.label2.TabIndex = 2;
			this.label2.Text = "Fore color:";
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(15, 88);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(61, 13);
			this.label3.TabIndex = 4;
			this.label3.Text = "Back color:";
			// 
			// btnExtract
			// 
			this.btnExtract.Image = global::FontAwesomeExtractor.Properties.Resources.Extract;
			this.btnExtract.Location = new System.Drawing.Point(33, 193);
			this.btnExtract.Name = "btnExtract";
			this.btnExtract.Size = new System.Drawing.Size(90, 26);
			this.btnExtract.TabIndex = 10;
			this.btnExtract.Text = " Extract";
			this.btnExtract.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
			this.btnExtract.UseVisualStyleBackColor = true;
			this.btnExtract.Click += new System.EventHandler(this.btnExtract_Click);
			// 
			// pictureBox1
			// 
			this.pictureBox1.Location = new System.Drawing.Point(3, 4);
			this.pictureBox1.Name = "pictureBox1";
			this.pictureBox1.Size = new System.Drawing.Size(256, 256);
			this.pictureBox1.TabIndex = 7;
			this.pictureBox1.TabStop = false;
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(15, 156);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(72, 13);
			this.label4.TabIndex = 8;
			this.label4.Text = "Corner radius:";
			// 
			// numCornerRadius
			// 
			this.numCornerRadius.Location = new System.Drawing.Point(94, 152);
			this.numCornerRadius.Maximum = new decimal(new int[] {
            32,
            0,
            0,
            0});
			this.numCornerRadius.Name = "numCornerRadius";
			this.numCornerRadius.Size = new System.Drawing.Size(153, 20);
			this.numCornerRadius.TabIndex = 9;
			this.numCornerRadius.ValueChanged += new System.EventHandler(this.numCornerRadius_ValueChanged);
			// 
			// btnForeColor
			// 
			this.btnForeColor.Location = new System.Drawing.Point(94, 50);
			this.btnForeColor.Name = "btnForeColor";
			this.btnForeColor.Size = new System.Drawing.Size(53, 21);
			this.btnForeColor.TabIndex = 3;
			this.btnForeColor.UseVisualStyleBackColor = true;
			this.btnForeColor.Click += new System.EventHandler(this.btnForeColor_Click);
			// 
			// cmbFontName
			// 
			this.cmbFontName.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
			this.cmbFontName.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
			this.cmbFontName.FormattingEnabled = true;
			this.cmbFontName.Location = new System.Drawing.Point(94, 16);
			this.cmbFontName.Name = "cmbFontName";
			this.cmbFontName.Size = new System.Drawing.Size(153, 21);
			this.cmbFontName.TabIndex = 1;
			// 
			// btnBackgroundColor
			// 
			this.btnBackgroundColor.Location = new System.Drawing.Point(94, 84);
			this.btnBackgroundColor.Name = "btnBackgroundColor";
			this.btnBackgroundColor.Size = new System.Drawing.Size(53, 21);
			this.btnBackgroundColor.TabIndex = 5;
			this.btnBackgroundColor.UseVisualStyleBackColor = true;
			this.btnBackgroundColor.Click += new System.EventHandler(this.btnBackgroundColor_Click);
			// 
			// btnSave
			// 
			this.btnSave.Image = global::FontAwesomeExtractor.Properties.Resources.Save;
			this.btnSave.Location = new System.Drawing.Point(142, 193);
			this.btnSave.Name = "btnSave";
			this.btnSave.Size = new System.Drawing.Size(90, 26);
			this.btnSave.TabIndex = 11;
			this.btnSave.Text = " Save As";
			this.btnSave.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
			this.btnSave.UseVisualStyleBackColor = true;
			this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point(15, 122);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(60, 13);
			this.label5.TabIndex = 6;
			this.label5.Text = "Image size:";
			// 
			// panel1
			// 
			this.panel1.BackColor = System.Drawing.SystemColors.Control;
			this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.panel1.Controls.Add(this.lblIcon);
			this.panel1.Controls.Add(this.pictureBox1);
			this.panel1.Location = new System.Drawing.Point(269, 16);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(265, 266);
			this.panel1.TabIndex = 14;
			// 
			// ddlFontSize
			// 
			this.ddlFontSize.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.ddlFontSize.FormattingEnabled = true;
			this.ddlFontSize.Location = new System.Drawing.Point(94, 118);
			this.ddlFontSize.Name = "ddlFontSize";
			this.ddlFontSize.Size = new System.Drawing.Size(153, 21);
			this.ddlFontSize.TabIndex = 7;
			this.ddlFontSize.SelectedIndexChanged += new System.EventHandler(this.ddlFontSize_SelectedIndexChanged);
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.Location = new System.Drawing.Point(78, 269);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(55, 13);
			this.label6.TabIndex = 12;
			this.label6.Text = "Based on:";
			// 
			// lnkFontAwesome
			// 
			this.lnkFontAwesome.AutoSize = true;
			this.lnkFontAwesome.Location = new System.Drawing.Point(139, 269);
			this.lnkFontAwesome.Name = "lnkFontAwesome";
			this.lnkFontAwesome.Size = new System.Drawing.Size(107, 13);
			this.lnkFontAwesome.TabIndex = 13;
			this.lnkFontAwesome.TabStop = true;
			this.lnkFontAwesome.Text = " Font Awesome 4.7.0";
			this.lnkFontAwesome.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkFontAwesome_LinkClicked);
			// 
			// chkBackTransparent
			// 
			this.chkBackTransparent.AutoSize = true;
			this.chkBackTransparent.Location = new System.Drawing.Point(163, 86);
			this.chkBackTransparent.Name = "chkBackTransparent";
			this.chkBackTransparent.Size = new System.Drawing.Size(83, 17);
			this.chkBackTransparent.TabIndex = 15;
			this.chkBackTransparent.Text = "Transparent";
			this.chkBackTransparent.UseVisualStyleBackColor = true;
			this.chkBackTransparent.CheckedChanged += new System.EventHandler(this.chkBackTransparent_CheckedChanged);
			// 
			// chkForeTransparent
			// 
			this.chkForeTransparent.AutoSize = true;
			this.chkForeTransparent.Location = new System.Drawing.Point(163, 53);
			this.chkForeTransparent.Name = "chkForeTransparent";
			this.chkForeTransparent.Size = new System.Drawing.Size(83, 17);
			this.chkForeTransparent.TabIndex = 15;
			this.chkForeTransparent.Text = "Transparent";
			this.chkForeTransparent.UseVisualStyleBackColor = true;
			this.chkForeTransparent.CheckedChanged += new System.EventHandler(this.chkForeTransparent_CheckedChanged);
			// 
			// MainForm
			// 
			this.AcceptButton = this.btnExtract;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(547, 305);
			this.Controls.Add(this.chkForeTransparent);
			this.Controls.Add(this.chkBackTransparent);
			this.Controls.Add(this.lnkFontAwesome);
			this.Controls.Add(this.label6);
			this.Controls.Add(this.ddlFontSize);
			this.Controls.Add(this.panel1);
			this.Controls.Add(this.btnSave);
			this.Controls.Add(this.cmbFontName);
			this.Controls.Add(this.btnBackgroundColor);
			this.Controls.Add(this.btnForeColor);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.numCornerRadius);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.btnExtract);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.Name = "MainForm";
			this.Text = "Font Awesome Extractor";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
			this.Load += new System.EventHandler(this.MainForm_Load);
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.numCornerRadius)).EndInit();
			this.panel1.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion
		private System.Windows.Forms.Label lblIcon;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Button btnExtract;
		private System.Windows.Forms.PictureBox pictureBox1;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.NumericUpDown numCornerRadius;
		private System.Windows.Forms.Button btnForeColor;
		private System.Windows.Forms.ComboBox cmbFontName;
		private System.Windows.Forms.Button btnBackgroundColor;
		private System.Windows.Forms.Button btnSave;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.ComboBox ddlFontSize;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.LinkLabel lnkFontAwesome;
		private System.Windows.Forms.CheckBox chkBackTransparent;
		private System.Windows.Forms.CheckBox chkForeTransparent;
	}
}

