namespace ApplicationUI
{
    partial class LibraryForm
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
            this.AddNew_button = new System.Windows.Forms.Button();
            this.Models_listBox = new System.Windows.Forms.ListBox();
            this.Remove_button = new System.Windows.Forms.Button();
            this.MeshName_label = new System.Windows.Forms.Label();
            this.MeshTextures_label = new System.Windows.Forms.Label();
            this.MeshMaterials_label = new System.Windows.Forms.Label();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.Materials_comboBox = new System.Windows.Forms.ComboBox();
            this.Textures_comboBox = new System.Windows.Forms.ComboBox();
            this.MeshNameValue_label = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // AddNew_button
            // 
            this.AddNew_button.Location = new System.Drawing.Point(12, 12);
            this.AddNew_button.Name = "AddNew_button";
            this.AddNew_button.Size = new System.Drawing.Size(130, 23);
            this.AddNew_button.TabIndex = 0;
            this.AddNew_button.Text = "Add model";
            this.AddNew_button.UseVisualStyleBackColor = true;
            this.AddNew_button.Click += new System.EventHandler(this.AddNew_button_Click);
            // 
            // Models_listBox
            // 
            this.Models_listBox.FormattingEnabled = true;
            this.Models_listBox.Location = new System.Drawing.Point(12, 176);
            this.Models_listBox.Name = "Models_listBox";
            this.Models_listBox.Size = new System.Drawing.Size(296, 225);
            this.Models_listBox.TabIndex = 1;
            this.Models_listBox.SelectedIndexChanged += new System.EventHandler(this.Models_listBox_SelectedIndexChanged);
            // 
            // Remove_button
            // 
            this.Remove_button.Location = new System.Drawing.Point(178, 12);
            this.Remove_button.Name = "Remove_button";
            this.Remove_button.Size = new System.Drawing.Size(130, 23);
            this.Remove_button.TabIndex = 2;
            this.Remove_button.Text = "Remove ";
            this.Remove_button.UseVisualStyleBackColor = true;
            this.Remove_button.Click += new System.EventHandler(this.Remove_button_Click);
            // 
            // MeshName_label
            // 
            this.MeshName_label.AutoSize = true;
            this.MeshName_label.Location = new System.Drawing.Point(13, 51);
            this.MeshName_label.Name = "MeshName_label";
            this.MeshName_label.Size = new System.Drawing.Size(64, 13);
            this.MeshName_label.TabIndex = 3;
            this.MeshName_label.Text = "MeshName:";
            // 
            // MeshTextures_label
            // 
            this.MeshTextures_label.AutoSize = true;
            this.MeshTextures_label.Location = new System.Drawing.Point(12, 135);
            this.MeshTextures_label.Name = "MeshTextures_label";
            this.MeshTextures_label.Size = new System.Drawing.Size(77, 13);
            this.MeshTextures_label.TabIndex = 4;
            this.MeshTextures_label.Text = "MeshTextures:";
            // 
            // MeshMaterials_label
            // 
            this.MeshMaterials_label.AutoSize = true;
            this.MeshMaterials_label.Location = new System.Drawing.Point(13, 92);
            this.MeshMaterials_label.Name = "MeshMaterials_label";
            this.MeshMaterials_label.Size = new System.Drawing.Size(78, 13);
            this.MeshMaterials_label.TabIndex = 5;
            this.MeshMaterials_label.Text = "MeshMaterials:";
            // 
            // folderBrowserDialog1
            // 
            this.folderBrowserDialog1.Description = "Please select folder where .mesh .material a and textures is stored";
            // 
            // Materials_comboBox
            // 
            this.Materials_comboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.Materials_comboBox.FormattingEnabled = true;
            this.Materials_comboBox.Location = new System.Drawing.Point(97, 89);
            this.Materials_comboBox.Name = "Materials_comboBox";
            this.Materials_comboBox.Size = new System.Drawing.Size(211, 21);
            this.Materials_comboBox.TabIndex = 6;
            // 
            // Textures_comboBox
            // 
            this.Textures_comboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.Textures_comboBox.FormattingEnabled = true;
            this.Textures_comboBox.Location = new System.Drawing.Point(97, 132);
            this.Textures_comboBox.Name = "Textures_comboBox";
            this.Textures_comboBox.Size = new System.Drawing.Size(211, 21);
            this.Textures_comboBox.TabIndex = 7;
            // 
            // MeshNameValue_label
            // 
            this.MeshNameValue_label.AutoSize = true;
            this.MeshNameValue_label.Location = new System.Drawing.Point(97, 51);
            this.MeshNameValue_label.Name = "MeshNameValue_label";
            this.MeshNameValue_label.Size = new System.Drawing.Size(10, 13);
            this.MeshNameValue_label.TabIndex = 8;
            this.MeshNameValue_label.Text = "-";
            // 
            // LibraryForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(320, 413);
            this.Controls.Add(this.MeshNameValue_label);
            this.Controls.Add(this.Textures_comboBox);
            this.Controls.Add(this.Materials_comboBox);
            this.Controls.Add(this.MeshMaterials_label);
            this.Controls.Add(this.MeshTextures_label);
            this.Controls.Add(this.MeshName_label);
            this.Controls.Add(this.Remove_button);
            this.Controls.Add(this.Models_listBox);
            this.Controls.Add(this.AddNew_button);
            this.Name = "LibraryForm";
            this.Text = "Models library";
            this.TopMost = true;
            this.Activated += new System.EventHandler(this.LibraryForm_Activated);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.LibraryForm_FormClosing);
            this.Load += new System.EventHandler(this.LibraryForm_Load);
            this.Shown += new System.EventHandler(this.LibraryForm_Shown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button AddNew_button;
        private System.Windows.Forms.ListBox Models_listBox;
        private System.Windows.Forms.Button Remove_button;
        private System.Windows.Forms.Label MeshName_label;
        private System.Windows.Forms.Label MeshTextures_label;
        private System.Windows.Forms.Label MeshMaterials_label;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.ComboBox Materials_comboBox;
        private System.Windows.Forms.ComboBox Textures_comboBox;
        private System.Windows.Forms.Label MeshNameValue_label;
    }
}