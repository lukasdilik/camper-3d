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
            this.showLibraryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.AvailableModels_label = new System.Windows.Forms.Label();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.StatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.CameraCoords_label = new System.Windows.Forms.ToolStripStatusLabel();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.LeftPanel = new System.Windows.Forms.Panel();
            this.Mode_tabControl = new System.Windows.Forms.TabControl();
            this.Cameras_tab = new System.Windows.Forms.TabPage();
            this.SecurityCameras_comboBox = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.CameraProperties_panel = new System.Windows.Forms.TableLayoutPanel();
            this.Rotation_textBox = new System.Windows.Forms.TextBox();
            this.Resolution_textBox = new System.Windows.Forms.TextBox();
            this.FOVy_textBox = new System.Windows.Forms.TextBox();
            this.AspectRatio_textBox = new System.Windows.Forms.TextBox();
            this.Direction_textBox = new System.Windows.Forms.TextBox();
            this.Position_textBox = new System.Windows.Forms.TextBox();
            this.Position_label = new System.Windows.Forms.Label();
            this.Direction_label = new System.Windows.Forms.Label();
            this.AspectRatio_label = new System.Windows.Forms.Label();
            this.FOVy_label = new System.Windows.Forms.Label();
            this.Resolution_label = new System.Windows.Forms.Label();
            this.Rotation_label = new System.Windows.Forms.Label();
            this.Update_btn = new System.Windows.Forms.Button();
            this.Delete_btn = new System.Windows.Forms.Button();
            this.Models_tab = new System.Windows.Forms.TabPage();
            this.AvailableModels_combo = new System.Windows.Forms.ComboBox();
            this.Lights_tab = new System.Windows.Forms.TabPage();
            this.preview_label = new System.Windows.Forms.Label();
            this.CameraView_pictureBox = new System.Windows.Forms.PictureBox();
            this.Log_textBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.modelPos_label = new System.Windows.Forms.Label();
            this.ModelPosition_textBox = new System.Windows.Forms.TextBox();
            this.Scale_label = new System.Windows.Forms.Label();
            this.ModelScale_textBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.ModelRotate_textBox = new System.Windows.Forms.TextBox();
            this.addedModels_listBox = new System.Windows.Forms.ListBox();
            this.label4 = new System.Windows.Forms.Label();
            this.ModelFileName_label = new System.Windows.Forms.Label();
            this.ModelApply_btn = new System.Windows.Forms.Button();
            this.deleteModel_btn = new System.Windows.Forms.Button();
            this.MainWindow.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.LeftPanel.SuspendLayout();
            this.Mode_tabControl.SuspendLayout();
            this.Cameras_tab.SuspendLayout();
            this.CameraProperties_panel.SuspendLayout();
            this.Models_tab.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.CameraView_pictureBox)).BeginInit();
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
            this.startToolStripMenuItem.Size = new System.Drawing.Size(98, 22);
            this.startToolStripMenuItem.Text = "Start";
            this.startToolStripMenuItem.Click += new System.EventHandler(this.startToolStripMenuItem_Click);
            // 
            // quitToolStripMenuItem
            // 
            this.quitToolStripMenuItem.Name = "quitToolStripMenuItem";
            this.quitToolStripMenuItem.Size = new System.Drawing.Size(98, 22);
            this.quitToolStripMenuItem.Text = "Quit";
            this.quitToolStripMenuItem.Click += new System.EventHandler(this.quitToolStripMenuItem_Click);
            // 
            // showLibraryToolStripMenuItem
            // 
            this.showLibraryToolStripMenuItem.Name = "showLibraryToolStripMenuItem";
            this.showLibraryToolStripMenuItem.Size = new System.Drawing.Size(84, 20);
            this.showLibraryToolStripMenuItem.Text = "Show library";
            this.showLibraryToolStripMenuItem.Click += new System.EventHandler(this.showLibraryToolStripMenuItem_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(12, 20);
            // 
            // AvailableModels_label
            // 
            this.AvailableModels_label.AutoSize = true;
            this.AvailableModels_label.Location = new System.Drawing.Point(7, 3);
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
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            this.openFileDialog1.Filter = "\"OGRE mesh|*.mesh";
            // 
            // LeftPanel
            // 
            this.LeftPanel.Controls.Add(this.Mode_tabControl);
            this.LeftPanel.Controls.Add(this.preview_label);
            this.LeftPanel.Controls.Add(this.CameraView_pictureBox);
            this.LeftPanel.Dock = System.Windows.Forms.DockStyle.Right;
            this.LeftPanel.Location = new System.Drawing.Point(1239, 0);
            this.LeftPanel.Name = "LeftPanel";
            this.LeftPanel.Size = new System.Drawing.Size(345, 839);
            this.LeftPanel.TabIndex = 6;
            this.LeftPanel.Leave += new System.EventHandler(this.DeactivateMainWindow);
            this.LeftPanel.MouseEnter += new System.EventHandler(this.ActivateMainWindow);
            // 
            // Mode_tabControl
            // 
            this.Mode_tabControl.Controls.Add(this.Cameras_tab);
            this.Mode_tabControl.Controls.Add(this.Models_tab);
            this.Mode_tabControl.Controls.Add(this.Lights_tab);
            this.Mode_tabControl.Location = new System.Drawing.Point(3, 24);
            this.Mode_tabControl.Name = "Mode_tabControl";
            this.Mode_tabControl.SelectedIndex = 0;
            this.Mode_tabControl.Size = new System.Drawing.Size(320, 534);
            this.Mode_tabControl.TabIndex = 14;
            this.Mode_tabControl.SelectedIndexChanged += new System.EventHandler(this.Mode_tabControl_SelectedIndexChanged);
            this.Mode_tabControl.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Mode_tabControl_KeyDown);
            this.Mode_tabControl.MouseEnter += new System.EventHandler(this.ActivateMainWindow);
            this.Mode_tabControl.MouseLeave += new System.EventHandler(this.DeactivateMainWindow);
            // 
            // Cameras_tab
            // 
            this.Cameras_tab.Controls.Add(this.SecurityCameras_comboBox);
            this.Cameras_tab.Controls.Add(this.label1);
            this.Cameras_tab.Controls.Add(this.CameraProperties_panel);
            this.Cameras_tab.Location = new System.Drawing.Point(4, 25);
            this.Cameras_tab.Name = "Cameras_tab";
            this.Cameras_tab.Padding = new System.Windows.Forms.Padding(3);
            this.Cameras_tab.Size = new System.Drawing.Size(312, 505);
            this.Cameras_tab.TabIndex = 0;
            this.Cameras_tab.Text = "Cameras";
            this.Cameras_tab.UseVisualStyleBackColor = true;
            // 
            // SecurityCameras_comboBox
            // 
            this.SecurityCameras_comboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.SecurityCameras_comboBox.FormattingEnabled = true;
            this.SecurityCameras_comboBox.Location = new System.Drawing.Point(9, 21);
            this.SecurityCameras_comboBox.Name = "SecurityCameras_comboBox";
            this.SecurityCameras_comboBox.Size = new System.Drawing.Size(293, 24);
            this.SecurityCameras_comboBox.TabIndex = 12;
            this.SecurityCameras_comboBox.SelectedIndexChanged += new System.EventHandler(this.SecurityCameras_comboBox_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 3);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(118, 17);
            this.label1.TabIndex = 13;
            this.label1.Text = "Selected camera:";
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
            this.CameraProperties_panel.Controls.Add(this.Position_label, 0, 1);
            this.CameraProperties_panel.Controls.Add(this.Direction_label, 0, 2);
            this.CameraProperties_panel.Controls.Add(this.AspectRatio_label, 0, 3);
            this.CameraProperties_panel.Controls.Add(this.FOVy_label, 0, 4);
            this.CameraProperties_panel.Controls.Add(this.Resolution_label, 0, 5);
            this.CameraProperties_panel.Controls.Add(this.Rotation_label, 0, 6);
            this.CameraProperties_panel.Controls.Add(this.Update_btn, 0, 7);
            this.CameraProperties_panel.Controls.Add(this.Delete_btn, 1, 7);
            this.CameraProperties_panel.Location = new System.Drawing.Point(9, 51);
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
            this.CameraProperties_panel.Size = new System.Drawing.Size(293, 258);
            this.CameraProperties_panel.TabIndex = 9;
            // 
            // Rotation_textBox
            // 
            this.Rotation_textBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Rotation_textBox.Location = new System.Drawing.Point(87, 186);
            this.Rotation_textBox.Margin = new System.Windows.Forms.Padding(0, 10, 5, 0);
            this.Rotation_textBox.Name = "Rotation_textBox";
            this.Rotation_textBox.Size = new System.Drawing.Size(201, 23);
            this.Rotation_textBox.TabIndex = 13;
            // 
            // Resolution_textBox
            // 
            this.Resolution_textBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Resolution_textBox.Location = new System.Drawing.Point(87, 153);
            this.Resolution_textBox.Margin = new System.Windows.Forms.Padding(0, 10, 5, 0);
            this.Resolution_textBox.Name = "Resolution_textBox";
            this.Resolution_textBox.Size = new System.Drawing.Size(201, 23);
            this.Resolution_textBox.TabIndex = 12;
            // 
            // FOVy_textBox
            // 
            this.FOVy_textBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.FOVy_textBox.Location = new System.Drawing.Point(87, 120);
            this.FOVy_textBox.Margin = new System.Windows.Forms.Padding(0, 10, 5, 0);
            this.FOVy_textBox.Name = "FOVy_textBox";
            this.FOVy_textBox.Size = new System.Drawing.Size(201, 23);
            this.FOVy_textBox.TabIndex = 11;
            // 
            // AspectRatio_textBox
            // 
            this.AspectRatio_textBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.AspectRatio_textBox.Location = new System.Drawing.Point(87, 76);
            this.AspectRatio_textBox.Margin = new System.Windows.Forms.Padding(0, 10, 5, 0);
            this.AspectRatio_textBox.Name = "AspectRatio_textBox";
            this.AspectRatio_textBox.Size = new System.Drawing.Size(201, 23);
            this.AspectRatio_textBox.TabIndex = 10;
            // 
            // Direction_textBox
            // 
            this.Direction_textBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Direction_textBox.Location = new System.Drawing.Point(87, 43);
            this.Direction_textBox.Margin = new System.Windows.Forms.Padding(0, 10, 5, 0);
            this.Direction_textBox.Name = "Direction_textBox";
            this.Direction_textBox.Size = new System.Drawing.Size(201, 23);
            this.Direction_textBox.TabIndex = 9;
            // 
            // Position_textBox
            // 
            this.Position_textBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Position_textBox.Location = new System.Drawing.Point(87, 10);
            this.Position_textBox.Margin = new System.Windows.Forms.Padding(0, 10, 5, 0);
            this.Position_textBox.Name = "Position_textBox";
            this.Position_textBox.Size = new System.Drawing.Size(201, 23);
            this.Position_textBox.TabIndex = 8;
            // 
            // Position_label
            // 
            this.Position_label.AutoSize = true;
            this.Position_label.Dock = System.Windows.Forms.DockStyle.Left;
            this.Position_label.Location = new System.Drawing.Point(5, 10);
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
            this.Direction_label.Location = new System.Drawing.Point(5, 43);
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
            this.AspectRatio_label.Location = new System.Drawing.Point(5, 76);
            this.AspectRatio_label.Margin = new System.Windows.Forms.Padding(5, 10, 0, 0);
            this.AspectRatio_label.Name = "AspectRatio_label";
            this.AspectRatio_label.Size = new System.Drawing.Size(55, 34);
            this.AspectRatio_label.TabIndex = 3;
            this.AspectRatio_label.Text = "Aspect Ratio";
            // 
            // FOVy_label
            // 
            this.FOVy_label.AutoSize = true;
            this.FOVy_label.Dock = System.Windows.Forms.DockStyle.Left;
            this.FOVy_label.Location = new System.Drawing.Point(5, 120);
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
            this.Resolution_label.Location = new System.Drawing.Point(5, 153);
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
            this.Rotation_label.Location = new System.Drawing.Point(5, 186);
            this.Rotation_label.Margin = new System.Windows.Forms.Padding(5, 10, 0, 0);
            this.Rotation_label.Name = "Rotation_label";
            this.Rotation_label.Size = new System.Drawing.Size(61, 23);
            this.Rotation_label.TabIndex = 6;
            this.Rotation_label.Text = "Rotation";
            // 
            // Update_btn
            // 
            this.Update_btn.Dock = System.Windows.Forms.DockStyle.Left;
            this.Update_btn.Location = new System.Drawing.Point(3, 212);
            this.Update_btn.Name = "Update_btn";
            this.Update_btn.Size = new System.Drawing.Size(75, 43);
            this.Update_btn.TabIndex = 14;
            this.Update_btn.Text = "Apply";
            this.Update_btn.UseVisualStyleBackColor = true;
            this.Update_btn.Click += new System.EventHandler(this.Update_btn_Click);
            // 
            // Delete_btn
            // 
            this.Delete_btn.Dock = System.Windows.Forms.DockStyle.Right;
            this.Delete_btn.Location = new System.Drawing.Point(215, 212);
            this.Delete_btn.Name = "Delete_btn";
            this.Delete_btn.Size = new System.Drawing.Size(75, 43);
            this.Delete_btn.TabIndex = 15;
            this.Delete_btn.Text = "Delete";
            this.Delete_btn.UseVisualStyleBackColor = true;
            this.Delete_btn.Click += new System.EventHandler(this.Delete_btn_Click);
            // 
            // Models_tab
            // 
            this.Models_tab.Controls.Add(this.deleteModel_btn);
            this.Models_tab.Controls.Add(this.ModelApply_btn);
            this.Models_tab.Controls.Add(this.ModelFileName_label);
            this.Models_tab.Controls.Add(this.label4);
            this.Models_tab.Controls.Add(this.addedModels_listBox);
            this.Models_tab.Controls.Add(this.ModelRotate_textBox);
            this.Models_tab.Controls.Add(this.label3);
            this.Models_tab.Controls.Add(this.ModelScale_textBox);
            this.Models_tab.Controls.Add(this.Scale_label);
            this.Models_tab.Controls.Add(this.ModelPosition_textBox);
            this.Models_tab.Controls.Add(this.modelPos_label);
            this.Models_tab.Controls.Add(this.label2);
            this.Models_tab.Controls.Add(this.AvailableModels_label);
            this.Models_tab.Controls.Add(this.AvailableModels_combo);
            this.Models_tab.Location = new System.Drawing.Point(4, 25);
            this.Models_tab.Name = "Models_tab";
            this.Models_tab.Padding = new System.Windows.Forms.Padding(3);
            this.Models_tab.Size = new System.Drawing.Size(312, 505);
            this.Models_tab.TabIndex = 1;
            this.Models_tab.Text = "Models";
            this.Models_tab.UseVisualStyleBackColor = true;
            // 
            // AvailableModels_combo
            // 
            this.AvailableModels_combo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.AvailableModels_combo.FormattingEnabled = true;
            this.AvailableModels_combo.Location = new System.Drawing.Point(6, 23);
            this.AvailableModels_combo.Name = "AvailableModels_combo";
            this.AvailableModels_combo.Size = new System.Drawing.Size(300, 24);
            this.AvailableModels_combo.TabIndex = 7;
            // 
            // Lights_tab
            // 
            this.Lights_tab.Location = new System.Drawing.Point(4, 25);
            this.Lights_tab.Name = "Lights_tab";
            this.Lights_tab.Size = new System.Drawing.Size(312, 505);
            this.Lights_tab.TabIndex = 2;
            this.Lights_tab.Text = "Lights";
            this.Lights_tab.UseVisualStyleBackColor = true;
            // 
            // preview_label
            // 
            this.preview_label.AutoSize = true;
            this.preview_label.Location = new System.Drawing.Point(3, 561);
            this.preview_label.Name = "preview_label";
            this.preview_label.Size = new System.Drawing.Size(61, 17);
            this.preview_label.TabIndex = 11;
            this.preview_label.Text = "Preview:";
            // 
            // CameraView_pictureBox
            // 
            this.CameraView_pictureBox.BackColor = System.Drawing.Color.White;
            this.CameraView_pictureBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.CameraView_pictureBox.Location = new System.Drawing.Point(3, 581);
            this.CameraView_pictureBox.Name = "CameraView_pictureBox";
            this.CameraView_pictureBox.Size = new System.Drawing.Size(320, 240);
            this.CameraView_pictureBox.TabIndex = 10;
            this.CameraView_pictureBox.TabStop = false;
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
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(7, 68);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(162, 17);
            this.label2.TabIndex = 8;
            this.label2.Text = "Currently added models:";
            // 
            // modelPos_label
            // 
            this.modelPos_label.AutoSize = true;
            this.modelPos_label.Location = new System.Drawing.Point(6, 420);
            this.modelPos_label.Name = "modelPos_label";
            this.modelPos_label.Size = new System.Drawing.Size(58, 17);
            this.modelPos_label.TabIndex = 10;
            this.modelPos_label.Text = "Position";
            // 
            // ModelPosition_textBox
            // 
            this.ModelPosition_textBox.Location = new System.Drawing.Point(81, 414);
            this.ModelPosition_textBox.Name = "ModelPosition_textBox";
            this.ModelPosition_textBox.Size = new System.Drawing.Size(225, 23);
            this.ModelPosition_textBox.TabIndex = 11;
            // 
            // Scale_label
            // 
            this.Scale_label.AutoSize = true;
            this.Scale_label.Location = new System.Drawing.Point(7, 449);
            this.Scale_label.Name = "Scale_label";
            this.Scale_label.Size = new System.Drawing.Size(43, 17);
            this.Scale_label.TabIndex = 12;
            this.Scale_label.Text = "Scale";
            // 
            // ModelScale_textBox
            // 
            this.ModelScale_textBox.Location = new System.Drawing.Point(81, 443);
            this.ModelScale_textBox.Name = "ModelScale_textBox";
            this.ModelScale_textBox.Size = new System.Drawing.Size(144, 23);
            this.ModelScale_textBox.TabIndex = 13;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(7, 478);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(69, 17);
            this.label3.TabIndex = 14;
            this.label3.Text = "Rotate(Y)";
            // 
            // ModelRotate_textBox
            // 
            this.ModelRotate_textBox.Location = new System.Drawing.Point(81, 472);
            this.ModelRotate_textBox.Name = "ModelRotate_textBox";
            this.ModelRotate_textBox.Size = new System.Drawing.Size(144, 23);
            this.ModelRotate_textBox.TabIndex = 15;
            // 
            // addedModels_listBox
            // 
            this.addedModels_listBox.FormattingEnabled = true;
            this.addedModels_listBox.ItemHeight = 16;
            this.addedModels_listBox.Location = new System.Drawing.Point(10, 88);
            this.addedModels_listBox.Name = "addedModels_listBox";
            this.addedModels_listBox.ScrollAlwaysVisible = true;
            this.addedModels_listBox.Size = new System.Drawing.Size(296, 276);
            this.addedModels_listBox.TabIndex = 16;
            this.addedModels_listBox.SelectedIndexChanged += new System.EventHandler(this.addedModels_listBox_SelectedIndexChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(7, 394);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(80, 17);
            this.label4.TabIndex = 17;
            this.label4.Text = "Model File: ";
            // 
            // ModelFileName_label
            // 
            this.ModelFileName_label.AutoSize = true;
            this.ModelFileName_label.Location = new System.Drawing.Point(93, 394);
            this.ModelFileName_label.Name = "ModelFileName_label";
            this.ModelFileName_label.Size = new System.Drawing.Size(13, 17);
            this.ModelFileName_label.TabIndex = 18;
            this.ModelFileName_label.Text = "-";
            // 
            // ModelApply_btn
            // 
            this.ModelApply_btn.Location = new System.Drawing.Point(231, 443);
            this.ModelApply_btn.Name = "ModelApply_btn";
            this.ModelApply_btn.Size = new System.Drawing.Size(75, 52);
            this.ModelApply_btn.TabIndex = 19;
            this.ModelApply_btn.Text = "Apply";
            this.ModelApply_btn.UseVisualStyleBackColor = true;
            this.ModelApply_btn.Click += new System.EventHandler(this.ModelApply_btn_Click);
            // 
            // deleteModel_btn
            // 
            this.deleteModel_btn.Location = new System.Drawing.Point(231, 370);
            this.deleteModel_btn.Name = "deleteModel_btn";
            this.deleteModel_btn.Size = new System.Drawing.Size(75, 23);
            this.deleteModel_btn.TabIndex = 20;
            this.deleteModel_btn.Text = "Delete";
            this.deleteModel_btn.UseVisualStyleBackColor = true;
            this.deleteModel_btn.Click += new System.EventHandler(this.deleteModel_btn_Click);
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
            this.Mode_tabControl.ResumeLayout(false);
            this.Cameras_tab.ResumeLayout(false);
            this.Cameras_tab.PerformLayout();
            this.CameraProperties_panel.ResumeLayout(false);
            this.CameraProperties_panel.PerformLayout();
            this.Models_tab.ResumeLayout(false);
            this.Models_tab.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.CameraView_pictureBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }



        #endregion

        private System.Windows.Forms.Panel MainWindow;
        private Label AvailableModels_label;
        private StatusStrip statusStrip1;
        private ToolStripStatusLabel toolStripStatusLabel1;
        private OpenFileDialog openFileDialog1;
        private Panel LeftPanel;
        private ComboBox AvailableModels_combo;
        private ToolStripStatusLabel StatusLabel;
        private TextBox Log_textBox;
        private ToolStripStatusLabel CameraCoords_label;
        private TableLayoutPanel CameraProperties_panel;
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
        private TabControl Mode_tabControl;
        private TabPage Cameras_tab;
        private TabPage Models_tab;
        private TabPage Lights_tab;
        private Label label2;
        private TextBox ModelRotate_textBox;
        private Label label3;
        private TextBox ModelScale_textBox;
        private Label Scale_label;
        private TextBox ModelPosition_textBox;
        private Label modelPos_label;
        private ListBox addedModels_listBox;
        private Label ModelFileName_label;
        private Label label4;
        private Button ModelApply_btn;
        private Button deleteModel_btn;

    }
}

