using System.Security.AccessControl;
using System.Windows.Forms;

namespace ApplicationUI
{
    partial class MainForm : Form
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
            this.MainWindow = new System.Windows.Forms.Panel();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.startToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.quitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.AvailableModels_label = new System.Windows.Forms.Label();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.StatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.CameraCoords_label = new System.Windows.Forms.ToolStripStatusLabel();
            this.AddFile_btn = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.LeftPanel = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.SecurityCameras_comboBox = new System.Windows.Forms.ComboBox();
            this.preview_label = new System.Windows.Forms.Label();
            this.CameraView_pictureBox = new System.Windows.Forms.PictureBox();
            this.CameraProperties_panel = new System.Windows.Forms.TableLayoutPanel();
            this.Rotation_textBox = new System.Windows.Forms.TextBox();
            this.Resolution_textBox = new System.Windows.Forms.TextBox();
            this.FOVy_textBox = new System.Windows.Forms.TextBox();
            this.AspectRatio_textBox = new System.Windows.Forms.TextBox();
            this.Direction_textBox = new System.Windows.Forms.TextBox();
            this.Position_textBox = new System.Windows.Forms.TextBox();
            this.Name_label = new System.Windows.Forms.Label();
            this.Position_label = new System.Windows.Forms.Label();
            this.Direction_label = new System.Windows.Forms.Label();
            this.AspectRatio_label = new System.Windows.Forms.Label();
            this.FOVy_label = new System.Windows.Forms.Label();
            this.Resolution_label = new System.Windows.Forms.Label();
            this.Rotation_label = new System.Windows.Forms.Label();
            this.Name_textBox = new System.Windows.Forms.TextBox();
            this.Update_btn = new System.Windows.Forms.Button();
            this.Delete_btn = new System.Windows.Forms.Button();
            this.AvailableModels_combo = new System.Windows.Forms.ComboBox();
            this.Log_textBox = new System.Windows.Forms.TextBox();
            this.showLibraryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.MainWindow.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.LeftPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.CameraView_pictureBox)).BeginInit();
            this.CameraProperties_panel.SuspendLayout();
            this.SuspendLayout();
            // 
            // MainWindow
            // 
            this.MainWindow.BackColor = System.Drawing.Color.White;
            this.MainWindow.Controls.Add(this.menuStrip1);
            this.MainWindow.Location = new System.Drawing.Point(0, 0);
            this.MainWindow.Margin = new System.Windows.Forms.Padding(4);
            this.MainWindow.Name = "MainWindow";
            this.MainWindow.Size = new System.Drawing.Size(1233, 742);
            this.MainWindow.TabIndex = 0;
            this.MainWindow.MouseClick += new System.Windows.Forms.MouseEventHandler(this.MainWindow_MouseClick);
            this.MainWindow.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.MainWindow_MouseDoubleClick);
            this.MainWindow.MouseDown += new System.Windows.Forms.MouseEventHandler(this.MainWindow_MouseDown);
            this.MainWindow.MouseEnter += new System.EventHandler(this.MainWindow_MouseEnter);
            this.MainWindow.MouseLeave += new System.EventHandler(this.MainWindow_MouseLeave);
            this.MainWindow.MouseMove += new System.Windows.Forms.MouseEventHandler(this.MainWindow_MouseMove);
            this.MainWindow.MouseUp += new System.Windows.Forms.MouseEventHandler(this.MainWindow_MouseUp);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1,
            this.showLibraryToolStripMenuItem,
            this.toolStripMenuItem2});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1233, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.startToolStripMenuItem,
            this.quitToolStripMenuItem});
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(80, 20);
            this.toolStripMenuItem1.Text = "Application";
            // 
            // startToolStripMenuItem
            // 
            this.startToolStripMenuItem.Name = "startToolStripMenuItem";
            this.startToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.startToolStripMenuItem.Text = "Start";
            this.startToolStripMenuItem.Click += new System.EventHandler(this.startToolStripMenuItem_Click);
            // 
            // quitToolStripMenuItem
            // 
            this.quitToolStripMenuItem.Name = "quitToolStripMenuItem";
            this.quitToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.quitToolStripMenuItem.Text = "Quit";
            this.quitToolStripMenuItem.Click += new System.EventHandler(this.quitToolStripMenuItem_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(12, 20);
            // 
            // AvailableModels_label
            // 
            this.AvailableModels_label.AutoSize = true;
            this.AvailableModels_label.Location = new System.Drawing.Point(13, 10);
            this.AvailableModels_label.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.AvailableModels_label.Name = "AvailableModels_label";
            this.AvailableModels_label.Size = new System.Drawing.Size(118, 17);
            this.AvailableModels_label.TabIndex = 1;
            this.AvailableModels_label.Text = "Available models:";
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1,
            this.StatusLabel,
            this.CameraCoords_label});
            this.statusStrip1.Location = new System.Drawing.Point(0, 839);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Padding = new System.Windows.Forms.Padding(1, 0, 19, 0);
            this.statusStrip1.Size = new System.Drawing.Size(1584, 22);
            this.statusStrip1.TabIndex = 2;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(0, 17);
            // 
            // StatusLabel
            // 
            this.StatusLabel.Name = "StatusLabel";
            this.StatusLabel.Size = new System.Drawing.Size(64, 17);
            this.StatusLabel.Text = "Status: Idle";
            // 
            // CameraCoords_label
            // 
            this.CameraCoords_label.Name = "CameraCoords_label";
            this.CameraCoords_label.Size = new System.Drawing.Size(131, 17);
            this.CameraCoords_label.Text = "Camera [N/A;N/A;N/A]";
            // 
            // AddFile_btn
            // 
            this.AddFile_btn.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.AddFile_btn.Location = new System.Drawing.Point(271, 42);
            this.AddFile_btn.Margin = new System.Windows.Forms.Padding(4);
            this.AddFile_btn.Name = "AddFile_btn";
            this.AddFile_btn.Size = new System.Drawing.Size(63, 24);
            this.AddFile_btn.TabIndex = 4;
            this.AddFile_btn.Text = "Add";
            this.AddFile_btn.UseVisualStyleBackColor = true;
            this.AddFile_btn.Click += new System.EventHandler(this.AddFile_btn_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            this.openFileDialog1.Filter = "\"OGRE mesh|*.mesh";
            // 
            // LeftPanel
            // 
            this.LeftPanel.Controls.Add(this.label1);
            this.LeftPanel.Controls.Add(this.SecurityCameras_comboBox);
            this.LeftPanel.Controls.Add(this.preview_label);
            this.LeftPanel.Controls.Add(this.CameraView_pictureBox);
            this.LeftPanel.Controls.Add(this.CameraProperties_panel);
            this.LeftPanel.Controls.Add(this.AvailableModels_combo);
            this.LeftPanel.Controls.Add(this.AddFile_btn);
            this.LeftPanel.Controls.Add(this.AvailableModels_label);
            this.LeftPanel.Dock = System.Windows.Forms.DockStyle.Right;
            this.LeftPanel.Location = new System.Drawing.Point(1239, 0);
            this.LeftPanel.Name = "LeftPanel";
            this.LeftPanel.Size = new System.Drawing.Size(345, 839);
            this.LeftPanel.TabIndex = 6;
            this.LeftPanel.MouseEnter += new System.EventHandler(this.LeftPanel_MouseEnter);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(16, 236);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(118, 17);
            this.label1.TabIndex = 13;
            this.label1.Text = "Selected camera:";
            // 
            // SecurityCameras_comboBox
            // 
            this.SecurityCameras_comboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.SecurityCameras_comboBox.FormattingEnabled = true;
            this.SecurityCameras_comboBox.Location = new System.Drawing.Point(16, 256);
            this.SecurityCameras_comboBox.Name = "SecurityCameras_comboBox";
            this.SecurityCameras_comboBox.Size = new System.Drawing.Size(314, 24);
            this.SecurityCameras_comboBox.TabIndex = 12;
            this.SecurityCameras_comboBox.SelectedIndexChanged += new System.EventHandler(this.SecurityCameras_comboBox_SelectedIndexChanged);
            // 
            // preview_label
            // 
            this.preview_label.AutoSize = true;
            this.preview_label.Location = new System.Drawing.Point(18, 561);
            this.preview_label.Name = "preview_label";
            this.preview_label.Size = new System.Drawing.Size(61, 17);
            this.preview_label.TabIndex = 11;
            this.preview_label.Text = "Preview:";
            // 
            // CameraView_pictureBox
            // 
            this.CameraView_pictureBox.BackColor = System.Drawing.Color.White;
            this.CameraView_pictureBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.CameraView_pictureBox.Location = new System.Drawing.Point(16, 581);
            this.CameraView_pictureBox.Name = "CameraView_pictureBox";
            this.CameraView_pictureBox.Size = new System.Drawing.Size(320, 240);
            this.CameraView_pictureBox.TabIndex = 10;
            this.CameraView_pictureBox.TabStop = false;
            // 
            // CameraProperties_panel
            // 
            this.CameraProperties_panel.ColumnCount = 2;
            this.CameraProperties_panel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.CameraProperties_panel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 70F));
            this.CameraProperties_panel.Controls.Add(this.Rotation_textBox, 1, 6);
            this.CameraProperties_panel.Controls.Add(this.Resolution_textBox, 1, 5);
            this.CameraProperties_panel.Controls.Add(this.FOVy_textBox, 1, 4);
            this.CameraProperties_panel.Controls.Add(this.AspectRatio_textBox, 1, 3);
            this.CameraProperties_panel.Controls.Add(this.Direction_textBox, 1, 2);
            this.CameraProperties_panel.Controls.Add(this.Position_textBox, 1, 1);
            this.CameraProperties_panel.Controls.Add(this.Name_label, 0, 0);
            this.CameraProperties_panel.Controls.Add(this.Position_label, 0, 1);
            this.CameraProperties_panel.Controls.Add(this.Direction_label, 0, 2);
            this.CameraProperties_panel.Controls.Add(this.AspectRatio_label, 0, 3);
            this.CameraProperties_panel.Controls.Add(this.FOVy_label, 0, 4);
            this.CameraProperties_panel.Controls.Add(this.Resolution_label, 0, 5);
            this.CameraProperties_panel.Controls.Add(this.Rotation_label, 0, 6);
            this.CameraProperties_panel.Controls.Add(this.Name_textBox, 1, 0);
            this.CameraProperties_panel.Controls.Add(this.Update_btn, 0, 7);
            this.CameraProperties_panel.Controls.Add(this.Delete_btn, 1, 7);
            this.CameraProperties_panel.Location = new System.Drawing.Point(16, 286);
            this.CameraProperties_panel.Name = "CameraProperties_panel";
            this.CameraProperties_panel.RowCount = 8;
            this.CameraProperties_panel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.CameraProperties_panel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.CameraProperties_panel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.CameraProperties_panel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.CameraProperties_panel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.CameraProperties_panel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.CameraProperties_panel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.CameraProperties_panel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.CameraProperties_panel.Size = new System.Drawing.Size(317, 270);
            this.CameraProperties_panel.TabIndex = 9;
            // 
            // Rotation_textBox
            // 
            this.Rotation_textBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Rotation_textBox.Location = new System.Drawing.Point(95, 208);
            this.Rotation_textBox.Margin = new System.Windows.Forms.Padding(0, 10, 5, 0);
            this.Rotation_textBox.Name = "Rotation_textBox";
            this.Rotation_textBox.Size = new System.Drawing.Size(217, 23);
            this.Rotation_textBox.TabIndex = 13;
            // 
            // Resolution_textBox
            // 
            this.Resolution_textBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Resolution_textBox.Location = new System.Drawing.Point(95, 175);
            this.Resolution_textBox.Margin = new System.Windows.Forms.Padding(0, 10, 5, 0);
            this.Resolution_textBox.Name = "Resolution_textBox";
            this.Resolution_textBox.Size = new System.Drawing.Size(217, 23);
            this.Resolution_textBox.TabIndex = 12;
            // 
            // FOVy_textBox
            // 
            this.FOVy_textBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.FOVy_textBox.Location = new System.Drawing.Point(95, 142);
            this.FOVy_textBox.Margin = new System.Windows.Forms.Padding(0, 10, 5, 0);
            this.FOVy_textBox.Name = "FOVy_textBox";
            this.FOVy_textBox.Size = new System.Drawing.Size(217, 23);
            this.FOVy_textBox.TabIndex = 11;
            // 
            // AspectRatio_textBox
            // 
            this.AspectRatio_textBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.AspectRatio_textBox.Location = new System.Drawing.Point(95, 109);
            this.AspectRatio_textBox.Margin = new System.Windows.Forms.Padding(0, 10, 5, 0);
            this.AspectRatio_textBox.Name = "AspectRatio_textBox";
            this.AspectRatio_textBox.Size = new System.Drawing.Size(217, 23);
            this.AspectRatio_textBox.TabIndex = 10;
            // 
            // Direction_textBox
            // 
            this.Direction_textBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Direction_textBox.Location = new System.Drawing.Point(95, 76);
            this.Direction_textBox.Margin = new System.Windows.Forms.Padding(0, 10, 5, 0);
            this.Direction_textBox.Name = "Direction_textBox";
            this.Direction_textBox.Size = new System.Drawing.Size(217, 23);
            this.Direction_textBox.TabIndex = 9;
            // 
            // Position_textBox
            // 
            this.Position_textBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Position_textBox.Location = new System.Drawing.Point(95, 43);
            this.Position_textBox.Margin = new System.Windows.Forms.Padding(0, 10, 5, 0);
            this.Position_textBox.Name = "Position_textBox";
            this.Position_textBox.Size = new System.Drawing.Size(217, 23);
            this.Position_textBox.TabIndex = 8;
            // 
            // Name_label
            // 
            this.Name_label.AutoSize = true;
            this.Name_label.Dock = System.Windows.Forms.DockStyle.Left;
            this.Name_label.Location = new System.Drawing.Point(5, 10);
            this.Name_label.Margin = new System.Windows.Forms.Padding(5, 10, 0, 0);
            this.Name_label.Name = "Name_label";
            this.Name_label.Size = new System.Drawing.Size(45, 23);
            this.Name_label.TabIndex = 0;
            this.Name_label.Text = "Name";
            // 
            // Position_label
            // 
            this.Position_label.AutoSize = true;
            this.Position_label.Dock = System.Windows.Forms.DockStyle.Left;
            this.Position_label.Location = new System.Drawing.Point(5, 43);
            this.Position_label.Margin = new System.Windows.Forms.Padding(5, 10, 0, 0);
            this.Position_label.Name = "Position_label";
            this.Position_label.Size = new System.Drawing.Size(58, 23);
            this.Position_label.TabIndex = 1;
            this.Position_label.Text = "Position";
            // 
            // Direction_label
            // 
            this.Direction_label.AutoSize = true;
            this.Direction_label.Dock = System.Windows.Forms.DockStyle.Left;
            this.Direction_label.Location = new System.Drawing.Point(5, 76);
            this.Direction_label.Margin = new System.Windows.Forms.Padding(5, 10, 0, 0);
            this.Direction_label.Name = "Direction_label";
            this.Direction_label.Size = new System.Drawing.Size(64, 23);
            this.Direction_label.TabIndex = 2;
            this.Direction_label.Text = "Direction";
            // 
            // AspectRatio_label
            // 
            this.AspectRatio_label.AutoSize = true;
            this.AspectRatio_label.Dock = System.Windows.Forms.DockStyle.Left;
            this.AspectRatio_label.Location = new System.Drawing.Point(5, 109);
            this.AspectRatio_label.Margin = new System.Windows.Forms.Padding(5, 10, 0, 0);
            this.AspectRatio_label.Name = "AspectRatio_label";
            this.AspectRatio_label.Size = new System.Drawing.Size(88, 23);
            this.AspectRatio_label.TabIndex = 3;
            this.AspectRatio_label.Text = "Aspect Ratio";
            // 
            // FOVy_label
            // 
            this.FOVy_label.AutoSize = true;
            this.FOVy_label.Dock = System.Windows.Forms.DockStyle.Left;
            this.FOVy_label.Location = new System.Drawing.Point(5, 142);
            this.FOVy_label.Margin = new System.Windows.Forms.Padding(5, 10, 0, 0);
            this.FOVy_label.Name = "FOVy_label";
            this.FOVy_label.Size = new System.Drawing.Size(43, 23);
            this.FOVy_label.TabIndex = 4;
            this.FOVy_label.Text = "FOVy";
            // 
            // Resolution_label
            // 
            this.Resolution_label.AutoSize = true;
            this.Resolution_label.Dock = System.Windows.Forms.DockStyle.Left;
            this.Resolution_label.Location = new System.Drawing.Point(5, 175);
            this.Resolution_label.Margin = new System.Windows.Forms.Padding(5, 10, 0, 0);
            this.Resolution_label.Name = "Resolution_label";
            this.Resolution_label.Size = new System.Drawing.Size(75, 23);
            this.Resolution_label.TabIndex = 5;
            this.Resolution_label.Text = "Resolution";
            // 
            // Rotation_label
            // 
            this.Rotation_label.AutoSize = true;
            this.Rotation_label.Dock = System.Windows.Forms.DockStyle.Left;
            this.Rotation_label.Location = new System.Drawing.Point(5, 208);
            this.Rotation_label.Margin = new System.Windows.Forms.Padding(5, 10, 0, 0);
            this.Rotation_label.Name = "Rotation_label";
            this.Rotation_label.Size = new System.Drawing.Size(61, 23);
            this.Rotation_label.TabIndex = 6;
            this.Rotation_label.Text = "Rotation";
            // 
            // Name_textBox
            // 
            this.Name_textBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Name_textBox.Location = new System.Drawing.Point(95, 10);
            this.Name_textBox.Margin = new System.Windows.Forms.Padding(0, 10, 5, 0);
            this.Name_textBox.Name = "Name_textBox";
            this.Name_textBox.Size = new System.Drawing.Size(217, 23);
            this.Name_textBox.TabIndex = 7;
            // 
            // Update_btn
            // 
            this.Update_btn.Dock = System.Windows.Forms.DockStyle.Left;
            this.Update_btn.Location = new System.Drawing.Point(3, 234);
            this.Update_btn.Name = "Update_btn";
            this.Update_btn.Size = new System.Drawing.Size(75, 37);
            this.Update_btn.TabIndex = 14;
            this.Update_btn.Text = "Apply";
            this.Update_btn.UseVisualStyleBackColor = true;
            this.Update_btn.Click += new System.EventHandler(this.Update_btn_Click);
            // 
            // Delete_btn
            // 
            this.Delete_btn.Dock = System.Windows.Forms.DockStyle.Right;
            this.Delete_btn.Location = new System.Drawing.Point(239, 234);
            this.Delete_btn.Name = "Delete_btn";
            this.Delete_btn.Size = new System.Drawing.Size(75, 37);
            this.Delete_btn.TabIndex = 15;
            this.Delete_btn.Text = "Delete";
            this.Delete_btn.UseVisualStyleBackColor = true;
            this.Delete_btn.Click += new System.EventHandler(this.Delete_btn_Click);
            // 
            // AvailableModels_combo
            // 
            this.AvailableModels_combo.FormattingEnabled = true;
            this.AvailableModels_combo.Location = new System.Drawing.Point(16, 42);
            this.AvailableModels_combo.Name = "AvailableModels_combo";
            this.AvailableModels_combo.Size = new System.Drawing.Size(248, 24);
            this.AvailableModels_combo.TabIndex = 7;
            // 
            // Log_textBox
            // 
            this.Log_textBox.Location = new System.Drawing.Point(0, 749);
            this.Log_textBox.Multiline = true;
            this.Log_textBox.Name = "Log_textBox";
            this.Log_textBox.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.Log_textBox.Size = new System.Drawing.Size(1233, 87);
            this.Log_textBox.TabIndex = 7;
            // 
            // showLibraryToolStripMenuItem
            // 
            this.showLibraryToolStripMenuItem.Name = "showLibraryToolStripMenuItem";
            this.showLibraryToolStripMenuItem.Size = new System.Drawing.Size(84, 20);
            this.showLibraryToolStripMenuItem.Text = "Show library";
            this.showLibraryToolStripMenuItem.Click += new System.EventHandler(this.showLibraryToolStripMenuItem_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1584, 861);
            this.Controls.Add(this.Log_textBox);
            this.Controls.Add(this.LeftPanel);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.MainWindow);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.KeyPreview = true;
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MinimumSize = new System.Drawing.Size(1024, 768);
            this.Name = "MainForm";
            this.Text = "CAMPER";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Disposed += new System.EventHandler(this.MainForm_Disposed);
            this.MainWindow.ResumeLayout(false);
            this.MainWindow.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.LeftPanel.ResumeLayout(false);
            this.LeftPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.CameraView_pictureBox)).EndInit();
            this.CameraProperties_panel.ResumeLayout(false);
            this.CameraProperties_panel.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }



        #endregion

        private System.Windows.Forms.Panel MainWindow;
        private Label AvailableModels_label;
        private StatusStrip statusStrip1;
        private ToolStripStatusLabel toolStripStatusLabel1;
        private Button AddFile_btn;
        private OpenFileDialog openFileDialog1;
        private Panel LeftPanel;
        private ComboBox AvailableModels_combo;
        private ToolStripStatusLabel StatusLabel;
        private TextBox Log_textBox;
        private ToolStripStatusLabel CameraCoords_label;
        private TableLayoutPanel CameraProperties_panel;
        private Label Name_label;
        private Label Position_label;
        private Label Direction_label;
        private Label AspectRatio_label;
        private Label FOVy_label;
        private Label Resolution_label;
        private Label Rotation_label;
        private TextBox Rotation_textBox;
        private TextBox Resolution_textBox;
        private TextBox FOVy_textBox;
        private TextBox AspectRatio_textBox;
        private TextBox Direction_textBox;
        private TextBox Position_textBox;
        private TextBox Name_textBox;
        private Button Update_btn;
        private MenuStrip menuStrip1;
        private ToolStripMenuItem toolStripMenuItem1;
        private ToolStripMenuItem startToolStripMenuItem;
        private ToolStripMenuItem quitToolStripMenuItem;
        private Button Delete_btn;
        private PictureBox CameraView_pictureBox;
        private Label label1;
        private ComboBox SecurityCameras_comboBox;
        private Label preview_label;
        private ToolStripMenuItem toolStripMenuItem2;
        private ToolStripMenuItem showLibraryToolStripMenuItem;

    }
}

