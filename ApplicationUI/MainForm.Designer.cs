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
            this.loadToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.quitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.showLibraryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.AvailableModels_label = new System.Windows.Forms.Label();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.StatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.CameraCoords_label = new System.Windows.Forms.ToolStripStatusLabel();
            this.Import_openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.LeftPanel = new System.Windows.Forms.Panel();
            this.Help_btn = new System.Windows.Forms.Button();
            this.Mode_tabControl = new System.Windows.Forms.TabControl();
            this.Cameras_tab = new System.Windows.Forms.TabPage();
            this.cameraRotation_panel = new System.Windows.Forms.Panel();
            this.Y = new System.Windows.Forms.Label();
            this.Pitch_vScrollBar = new System.Windows.Forms.VScrollBar();
            this.PitchAngle_label = new System.Windows.Forms.Label();
            this.Yaw_hScrollBar = new System.Windows.Forms.HScrollBar();
            this.label10 = new System.Windows.Forms.Label();
            this.YawAngle_label = new System.Windows.Forms.Label();
            this.Cameras_listBox = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.CameraProperties_panel = new System.Windows.Forms.TableLayoutPanel();
            this.Position_label = new System.Windows.Forms.Label();
            this.Resolution_textBox = new System.Windows.Forms.TextBox();
            this.FOVy_textBox = new System.Windows.Forms.TextBox();
            this.AspectRatio_textBox = new System.Windows.Forms.TextBox();
            this.Update_btn = new System.Windows.Forms.Button();
            this.Direction_textBox = new System.Windows.Forms.TextBox();
            this.Position_textBox = new System.Windows.Forms.TextBox();
            this.Direction_label = new System.Windows.Forms.Label();
            this.AspectRatio_label = new System.Windows.Forms.Label();
            this.FOVy_label = new System.Windows.Forms.Label();
            this.Resolution_label = new System.Windows.Forms.Label();
            this.Delete_btn = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.Rotation_textBox = new System.Windows.Forms.TextBox();
            this.Models_tab = new System.Windows.Forms.TabPage();
            this.deleteModel_btn = new System.Windows.Forms.Button();
            this.ModelApply_btn = new System.Windows.Forms.Button();
            this.ModelFileName_label = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.addedModels_listBox = new System.Windows.Forms.ListBox();
            this.ModelRotate_textBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.ModelScale_textBox = new System.Windows.Forms.TextBox();
            this.Scale_label = new System.Windows.Forms.Label();
            this.ModelPosition_textBox = new System.Windows.Forms.TextBox();
            this.modelPos_label = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.AvailableModels_combo = new System.Windows.Forms.ComboBox();
            this.Lights_tab = new System.Windows.Forms.TabPage();
            this.label6 = new System.Windows.Forms.Label();
            this.LightType_combo = new System.Windows.Forms.ComboBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.label7 = new System.Windows.Forms.Label();
            this.UpdateLight_btn = new System.Windows.Forms.Button();
            this.DeleteLight_btn = new System.Windows.Forms.Button();
            this.Angle_label = new System.Windows.Forms.Label();
            this.Angle_textBox = new System.Windows.Forms.TextBox();
            this.LightDirection_label = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.LightDirection_textBox = new System.Windows.Forms.TextBox();
            this.ColorLight_btn = new System.Windows.Forms.Button();
            this.LightName_label = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.LightPosition_textBox = new System.Windows.Forms.TextBox();
            this.Lights_listBox = new System.Windows.Forms.ListBox();
            this.preview_label = new System.Windows.Forms.Label();
            this.CameraView_pictureBox = new System.Windows.Forms.PictureBox();
            this.Log_textBox = new System.Windows.Forms.TextBox();
            this.LightColor_dialog = new System.Windows.Forms.ColorDialog();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.LoadScene_openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.MainWindow.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.LeftPanel.SuspendLayout();
            this.Mode_tabControl.SuspendLayout();
            this.Cameras_tab.SuspendLayout();
            this.cameraRotation_panel.SuspendLayout();
            this.CameraProperties_panel.SuspendLayout();
            this.Models_tab.SuspendLayout();
            this.Lights_tab.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
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
            this.MainWindow.Size = new System.Drawing.Size(1280, 720);
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
            this.menuStrip1.Size = new System.Drawing.Size(1280, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.startToolStripMenuItem,
            this.loadToolStripMenuItem,
            this.saveToolStripMenuItem,
            this.quitToolStripMenuItem});
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(80, 20);
            this.toolStripMenuItem1.Text = "Application";
            // 
            // startToolStripMenuItem
            // 
            this.startToolStripMenuItem.Name = "startToolStripMenuItem";
            this.startToolStripMenuItem.Size = new System.Drawing.Size(100, 22);
            this.startToolStripMenuItem.Text = "Start";
            this.startToolStripMenuItem.Click += new System.EventHandler(this.startToolStripMenuItem_Click);
            // 
            // loadToolStripMenuItem
            // 
            this.loadToolStripMenuItem.Name = "loadToolStripMenuItem";
            this.loadToolStripMenuItem.Size = new System.Drawing.Size(100, 22);
            this.loadToolStripMenuItem.Text = "Load";
            this.loadToolStripMenuItem.Click += new System.EventHandler(this.loadToolStripMenuItem_Click);
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(100, 22);
            this.saveToolStripMenuItem.Text = "Save";
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.saveToolStripMenuItem_Click);
            // 
            // quitToolStripMenuItem
            // 
            this.quitToolStripMenuItem.Name = "quitToolStripMenuItem";
            this.quitToolStripMenuItem.Size = new System.Drawing.Size(100, 22);
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
            this.statusStrip1.Size = new System.Drawing.Size(1634, 22);
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
            // Import_openFileDialog1
            // 
            this.Import_openFileDialog1.FileName = "openFileDialog1";
            this.Import_openFileDialog1.Filter = "\"OGRE mesh|*.mesh";
            // 
            // LeftPanel
            // 
            this.LeftPanel.Controls.Add(this.Help_btn);
            this.LeftPanel.Controls.Add(this.Mode_tabControl);
            this.LeftPanel.Controls.Add(this.preview_label);
            this.LeftPanel.Controls.Add(this.CameraView_pictureBox);
            this.LeftPanel.Dock = System.Windows.Forms.DockStyle.Right;
            this.LeftPanel.Location = new System.Drawing.Point(1285, 0);
            this.LeftPanel.Name = "LeftPanel";
            this.LeftPanel.Size = new System.Drawing.Size(349, 839);
            this.LeftPanel.TabIndex = 6;
            this.LeftPanel.Leave += new System.EventHandler(this.DeactivateMainWindow);
            this.LeftPanel.MouseEnter += new System.EventHandler(this.ActivateMainWindow);
            // 
            // Help_btn
            // 
            this.Help_btn.Location = new System.Drawing.Point(309, 3);
            this.Help_btn.Name = "Help_btn";
            this.Help_btn.Size = new System.Drawing.Size(37, 31);
            this.Help_btn.TabIndex = 15;
            this.Help_btn.Text = "?";
            this.Help_btn.UseVisualStyleBackColor = true;
            // 
            // Mode_tabControl
            // 
            this.Mode_tabControl.Controls.Add(this.Cameras_tab);
            this.Mode_tabControl.Controls.Add(this.Models_tab);
            this.Mode_tabControl.Controls.Add(this.Lights_tab);
            this.Mode_tabControl.Location = new System.Drawing.Point(3, 12);
            this.Mode_tabControl.Name = "Mode_tabControl";
            this.Mode_tabControl.SelectedIndex = 0;
            this.Mode_tabControl.Size = new System.Drawing.Size(343, 546);
            this.Mode_tabControl.TabIndex = 14;
            this.Mode_tabControl.SelectedIndexChanged += new System.EventHandler(this.Mode_tabControl_SelectedIndexChanged);
            this.Mode_tabControl.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Mode_tabControl_KeyDown);
            this.Mode_tabControl.MouseEnter += new System.EventHandler(this.ActivateMainWindow);
            this.Mode_tabControl.MouseLeave += new System.EventHandler(this.DeactivateMainWindow);
            // 
            // Cameras_tab
            // 
            this.Cameras_tab.Controls.Add(this.cameraRotation_panel);
            this.Cameras_tab.Controls.Add(this.Cameras_listBox);
            this.Cameras_tab.Controls.Add(this.label1);
            this.Cameras_tab.Controls.Add(this.CameraProperties_panel);
            this.Cameras_tab.Location = new System.Drawing.Point(4, 25);
            this.Cameras_tab.Name = "Cameras_tab";
            this.Cameras_tab.Padding = new System.Windows.Forms.Padding(3);
            this.Cameras_tab.Size = new System.Drawing.Size(335, 517);
            this.Cameras_tab.TabIndex = 0;
            this.Cameras_tab.Text = "Cameras";
            this.Cameras_tab.UseVisualStyleBackColor = true;
            // 
            // cameraRotation_panel
            // 
            this.cameraRotation_panel.Controls.Add(this.Y);
            this.cameraRotation_panel.Controls.Add(this.Pitch_vScrollBar);
            this.cameraRotation_panel.Controls.Add(this.PitchAngle_label);
            this.cameraRotation_panel.Controls.Add(this.Yaw_hScrollBar);
            this.cameraRotation_panel.Controls.Add(this.label10);
            this.cameraRotation_panel.Controls.Add(this.YawAngle_label);
            this.cameraRotation_panel.Location = new System.Drawing.Point(6, 161);
            this.cameraRotation_panel.Name = "cameraRotation_panel";
            this.cameraRotation_panel.Size = new System.Drawing.Size(318, 110);
            this.cameraRotation_panel.TabIndex = 24;
            // 
            // Y
            // 
            this.Y.AutoSize = true;
            this.Y.Location = new System.Drawing.Point(5, 14);
            this.Y.Name = "Y";
            this.Y.Size = new System.Drawing.Size(103, 17);
            this.Y.TabIndex = 20;
            this.Y.Text = "H. angle (deg):";
            // 
            // Pitch_vScrollBar
            // 
            this.Pitch_vScrollBar.Location = new System.Drawing.Point(293, 14);
            this.Pitch_vScrollBar.Maximum = 90;
            this.Pitch_vScrollBar.Minimum = -90;
            this.Pitch_vScrollBar.Name = "Pitch_vScrollBar";
            this.Pitch_vScrollBar.Size = new System.Drawing.Size(17, 80);
            this.Pitch_vScrollBar.TabIndex = 19;
            this.Pitch_vScrollBar.ValueChanged += new System.EventHandler(this.Pitch_vScrollBar_ValueChanged);
            // 
            // PitchAngle_label
            // 
            this.PitchAngle_label.AutoSize = true;
            this.PitchAngle_label.Location = new System.Drawing.Point(265, 14);
            this.PitchAngle_label.Name = "PitchAngle_label";
            this.PitchAngle_label.Size = new System.Drawing.Size(16, 17);
            this.PitchAngle_label.TabIndex = 23;
            this.PitchAngle_label.Text = "0";
            // 
            // Yaw_hScrollBar
            // 
            this.Yaw_hScrollBar.Location = new System.Drawing.Point(8, 50);
            this.Yaw_hScrollBar.Maximum = 90;
            this.Yaw_hScrollBar.Minimum = -90;
            this.Yaw_hScrollBar.Name = "Yaw_hScrollBar";
            this.Yaw_hScrollBar.Size = new System.Drawing.Size(120, 17);
            this.Yaw_hScrollBar.TabIndex = 18;
            this.Yaw_hScrollBar.ValueChanged += new System.EventHandler(this.Yaw_hScrollBar_ValueChanged);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(157, 14);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(102, 17);
            this.label10.TabIndex = 21;
            this.label10.Text = "V. angle (deg):";
            // 
            // YawAngle_label
            // 
            this.YawAngle_label.AutoSize = true;
            this.YawAngle_label.Location = new System.Drawing.Point(112, 14);
            this.YawAngle_label.Name = "YawAngle_label";
            this.YawAngle_label.Size = new System.Drawing.Size(16, 17);
            this.YawAngle_label.TabIndex = 22;
            this.YawAngle_label.Text = "0";
            // 
            // Cameras_listBox
            // 
            this.Cameras_listBox.FormattingEnabled = true;
            this.Cameras_listBox.ItemHeight = 16;
            this.Cameras_listBox.Location = new System.Drawing.Point(6, 23);
            this.Cameras_listBox.Name = "Cameras_listBox";
            this.Cameras_listBox.ScrollAlwaysVisible = true;
            this.Cameras_listBox.Size = new System.Drawing.Size(318, 132);
            this.Cameras_listBox.TabIndex = 17;
            this.Cameras_listBox.SelectedIndexChanged += new System.EventHandler(this.Cameras_listBox_SelectedIndexChanged);
            this.Cameras_listBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Cameras_listBox_KeyDown);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 3);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(109, 17);
            this.label1.TabIndex = 13;
            this.label1.Text = "Added Cameras";
            // 
            // CameraProperties_panel
            // 
            this.CameraProperties_panel.ColumnCount = 2;
            this.CameraProperties_panel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30.42575F));
            this.CameraProperties_panel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 69.57425F));
            this.CameraProperties_panel.Controls.Add(this.Position_label, 0, 1);
            this.CameraProperties_panel.Controls.Add(this.Resolution_textBox, 1, 5);
            this.CameraProperties_panel.Controls.Add(this.FOVy_textBox, 1, 4);
            this.CameraProperties_panel.Controls.Add(this.AspectRatio_textBox, 1, 3);
            this.CameraProperties_panel.Controls.Add(this.Update_btn, 0, 9);
            this.CameraProperties_panel.Controls.Add(this.Direction_textBox, 1, 2);
            this.CameraProperties_panel.Controls.Add(this.Position_textBox, 1, 1);
            this.CameraProperties_panel.Controls.Add(this.Direction_label, 0, 2);
            this.CameraProperties_panel.Controls.Add(this.AspectRatio_label, 0, 3);
            this.CameraProperties_panel.Controls.Add(this.FOVy_label, 0, 4);
            this.CameraProperties_panel.Controls.Add(this.Resolution_label, 0, 5);
            this.CameraProperties_panel.Controls.Add(this.Delete_btn, 1, 9);
            this.CameraProperties_panel.Controls.Add(this.label8, 0, 8);
            this.CameraProperties_panel.Controls.Add(this.Rotation_textBox, 1, 8);
            this.CameraProperties_panel.Location = new System.Drawing.Point(3, 277);
            this.CameraProperties_panel.Name = "CameraProperties_panel";
            this.CameraProperties_panel.RowCount = 10;
            this.CameraProperties_panel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.CameraProperties_panel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.CameraProperties_panel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.CameraProperties_panel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.CameraProperties_panel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.CameraProperties_panel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.CameraProperties_panel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.CameraProperties_panel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.CameraProperties_panel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 31F));
            this.CameraProperties_panel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 17F));
            this.CameraProperties_panel.Size = new System.Drawing.Size(326, 234);
            this.CameraProperties_panel.TabIndex = 9;
            // 
            // Position_label
            // 
            this.Position_label.AutoSize = true;
            this.Position_label.Dock = System.Windows.Forms.DockStyle.Left;
            this.Position_label.Location = new System.Drawing.Point(5, 5);
            this.Position_label.Margin = new System.Windows.Forms.Padding(5, 5, 0, 0);
            this.Position_label.Name = "Position_label";
            this.Position_label.Size = new System.Drawing.Size(58, 28);
            this.Position_label.TabIndex = 1;
            this.Position_label.Text = "Position";
            // 
            // Resolution_textBox
            // 
            this.Resolution_textBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Resolution_textBox.Location = new System.Drawing.Point(99, 142);
            this.Resolution_textBox.Margin = new System.Windows.Forms.Padding(0, 10, 5, 0);
            this.Resolution_textBox.Name = "Resolution_textBox";
            this.Resolution_textBox.Size = new System.Drawing.Size(222, 23);
            this.Resolution_textBox.TabIndex = 12;
            // 
            // FOVy_textBox
            // 
            this.FOVy_textBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.FOVy_textBox.Location = new System.Drawing.Point(99, 109);
            this.FOVy_textBox.Margin = new System.Windows.Forms.Padding(0, 10, 5, 0);
            this.FOVy_textBox.Name = "FOVy_textBox";
            this.FOVy_textBox.Size = new System.Drawing.Size(222, 23);
            this.FOVy_textBox.TabIndex = 11;
            // 
            // AspectRatio_textBox
            // 
            this.AspectRatio_textBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.AspectRatio_textBox.Location = new System.Drawing.Point(99, 76);
            this.AspectRatio_textBox.Margin = new System.Windows.Forms.Padding(0, 10, 5, 0);
            this.AspectRatio_textBox.Name = "AspectRatio_textBox";
            this.AspectRatio_textBox.Size = new System.Drawing.Size(222, 23);
            this.AspectRatio_textBox.TabIndex = 10;
            // 
            // Update_btn
            // 
            this.Update_btn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.Update_btn.Location = new System.Drawing.Point(3, 199);
            this.Update_btn.Name = "Update_btn";
            this.Update_btn.Size = new System.Drawing.Size(77, 32);
            this.Update_btn.TabIndex = 14;
            this.Update_btn.Text = "Update";
            this.Update_btn.UseVisualStyleBackColor = true;
            this.Update_btn.Click += new System.EventHandler(this.Update_btn_Click);
            // 
            // Direction_textBox
            // 
            this.Direction_textBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Direction_textBox.Location = new System.Drawing.Point(99, 43);
            this.Direction_textBox.Margin = new System.Windows.Forms.Padding(0, 10, 5, 0);
            this.Direction_textBox.Name = "Direction_textBox";
            this.Direction_textBox.Size = new System.Drawing.Size(222, 23);
            this.Direction_textBox.TabIndex = 9;
            // 
            // Position_textBox
            // 
            this.Position_textBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Position_textBox.Location = new System.Drawing.Point(99, 10);
            this.Position_textBox.Margin = new System.Windows.Forms.Padding(0, 10, 5, 0);
            this.Position_textBox.Name = "Position_textBox";
            this.Position_textBox.Size = new System.Drawing.Size(222, 23);
            this.Position_textBox.TabIndex = 8;
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
            this.AspectRatio_label.Size = new System.Drawing.Size(88, 23);
            this.AspectRatio_label.TabIndex = 3;
            this.AspectRatio_label.Text = "Aspect Ratio";
            // 
            // FOVy_label
            // 
            this.FOVy_label.AutoSize = true;
            this.FOVy_label.Dock = System.Windows.Forms.DockStyle.Left;
            this.FOVy_label.Location = new System.Drawing.Point(5, 109);
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
            this.Resolution_label.Location = new System.Drawing.Point(5, 142);
            this.Resolution_label.Margin = new System.Windows.Forms.Padding(5, 10, 0, 0);
            this.Resolution_label.Name = "Resolution_label";
            this.Resolution_label.Size = new System.Drawing.Size(75, 23);
            this.Resolution_label.TabIndex = 5;
            this.Resolution_label.Text = "Resolution";
            // 
            // Delete_btn
            // 
            this.Delete_btn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.Delete_btn.Location = new System.Drawing.Point(248, 199);
            this.Delete_btn.Name = "Delete_btn";
            this.Delete_btn.Size = new System.Drawing.Size(75, 32);
            this.Delete_btn.TabIndex = 15;
            this.Delete_btn.Text = "Delete";
            this.Delete_btn.UseVisualStyleBackColor = true;
            this.Delete_btn.Click += new System.EventHandler(this.Delete_btn_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(5, 175);
            this.label8.Margin = new System.Windows.Forms.Padding(5, 10, 0, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(61, 17);
            this.label8.TabIndex = 16;
            this.label8.Text = "Rotation";
            // 
            // Rotation_textBox
            // 
            this.Rotation_textBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Rotation_textBox.Location = new System.Drawing.Point(99, 175);
            this.Rotation_textBox.Margin = new System.Windows.Forms.Padding(0, 10, 5, 0);
            this.Rotation_textBox.Name = "Rotation_textBox";
            this.Rotation_textBox.Size = new System.Drawing.Size(222, 23);
            this.Rotation_textBox.TabIndex = 17;
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
            this.Models_tab.Size = new System.Drawing.Size(335, 517);
            this.Models_tab.TabIndex = 1;
            this.Models_tab.Text = "Models";
            this.Models_tab.UseVisualStyleBackColor = true;
            // 
            // deleteModel_btn
            // 
            this.deleteModel_btn.Location = new System.Drawing.Point(254, 370);
            this.deleteModel_btn.Name = "deleteModel_btn";
            this.deleteModel_btn.Size = new System.Drawing.Size(75, 28);
            this.deleteModel_btn.TabIndex = 20;
            this.deleteModel_btn.Text = "Delete";
            this.deleteModel_btn.UseVisualStyleBackColor = true;
            this.deleteModel_btn.Click += new System.EventHandler(this.deleteModel_btn_Click);
            // 
            // ModelApply_btn
            // 
            this.ModelApply_btn.Location = new System.Drawing.Point(254, 443);
            this.ModelApply_btn.Name = "ModelApply_btn";
            this.ModelApply_btn.Size = new System.Drawing.Size(75, 52);
            this.ModelApply_btn.TabIndex = 19;
            this.ModelApply_btn.Text = "Update";
            this.ModelApply_btn.UseVisualStyleBackColor = true;
            this.ModelApply_btn.Click += new System.EventHandler(this.ModelApply_btn_Click);
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
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(7, 394);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(80, 17);
            this.label4.TabIndex = 17;
            this.label4.Text = "Model File: ";
            // 
            // addedModels_listBox
            // 
            this.addedModels_listBox.FormattingEnabled = true;
            this.addedModels_listBox.ItemHeight = 16;
            this.addedModels_listBox.Location = new System.Drawing.Point(10, 88);
            this.addedModels_listBox.Name = "addedModels_listBox";
            this.addedModels_listBox.ScrollAlwaysVisible = true;
            this.addedModels_listBox.Size = new System.Drawing.Size(319, 276);
            this.addedModels_listBox.TabIndex = 16;
            this.addedModels_listBox.SelectedIndexChanged += new System.EventHandler(this.addedModels_listBox_SelectedIndexChanged);
            // 
            // ModelRotate_textBox
            // 
            this.ModelRotate_textBox.Location = new System.Drawing.Point(81, 472);
            this.ModelRotate_textBox.Name = "ModelRotate_textBox";
            this.ModelRotate_textBox.Size = new System.Drawing.Size(167, 23);
            this.ModelRotate_textBox.TabIndex = 15;
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
            // ModelScale_textBox
            // 
            this.ModelScale_textBox.Location = new System.Drawing.Point(81, 443);
            this.ModelScale_textBox.Name = "ModelScale_textBox";
            this.ModelScale_textBox.Size = new System.Drawing.Size(167, 23);
            this.ModelScale_textBox.TabIndex = 13;
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
            // ModelPosition_textBox
            // 
            this.ModelPosition_textBox.Location = new System.Drawing.Point(81, 414);
            this.ModelPosition_textBox.Name = "ModelPosition_textBox";
            this.ModelPosition_textBox.Size = new System.Drawing.Size(248, 23);
            this.ModelPosition_textBox.TabIndex = 11;
            this.ModelPosition_textBox.Text = "77";
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
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(7, 68);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(162, 17);
            this.label2.TabIndex = 8;
            this.label2.Text = "Currently added models:";
            // 
            // AvailableModels_combo
            // 
            this.AvailableModels_combo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.AvailableModels_combo.FormattingEnabled = true;
            this.AvailableModels_combo.Location = new System.Drawing.Point(6, 23);
            this.AvailableModels_combo.Name = "AvailableModels_combo";
            this.AvailableModels_combo.Size = new System.Drawing.Size(323, 24);
            this.AvailableModels_combo.TabIndex = 7;
            // 
            // Lights_tab
            // 
            this.Lights_tab.Controls.Add(this.label6);
            this.Lights_tab.Controls.Add(this.LightType_combo);
            this.Lights_tab.Controls.Add(this.tableLayoutPanel1);
            this.Lights_tab.Controls.Add(this.Lights_listBox);
            this.Lights_tab.Location = new System.Drawing.Point(4, 25);
            this.Lights_tab.Name = "Lights_tab";
            this.Lights_tab.Size = new System.Drawing.Size(335, 517);
            this.Lights_tab.TabIndex = 2;
            this.Lights_tab.Text = "Lights";
            this.Lights_tab.UseVisualStyleBackColor = true;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(6, 10);
            this.label6.Margin = new System.Windows.Forms.Padding(10, 10, 0, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(283, 17);
            this.label6.TabIndex = 0;
            this.label6.Text = "Choose type of Light and click on the model";
            // 
            // LightType_combo
            // 
            this.LightType_combo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.LightType_combo.FormattingEnabled = true;
            this.LightType_combo.Location = new System.Drawing.Point(6, 37);
            this.LightType_combo.Margin = new System.Windows.Forms.Padding(10, 10, 0, 0);
            this.LightType_combo.MaxDropDownItems = 2;
            this.LightType_combo.Name = "LightType_combo";
            this.LightType_combo.Size = new System.Drawing.Size(130, 24);
            this.LightType_combo.TabIndex = 6;
            this.LightType_combo.SelectedIndexChanged += new System.EventHandler(this.LightType_combo_SelectedIndexChanged);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.label7, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.UpdateLight_btn, 0, 5);
            this.tableLayoutPanel1.Controls.Add(this.DeleteLight_btn, 1, 5);
            this.tableLayoutPanel1.Controls.Add(this.Angle_label, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this.Angle_textBox, 1, 4);
            this.tableLayoutPanel1.Controls.Add(this.LightDirection_label, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.label9, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.LightDirection_textBox, 1, 3);
            this.tableLayoutPanel1.Controls.Add(this.ColorLight_btn, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.LightName_label, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.label5, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.LightPosition_textBox, 1, 1);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(6, 283);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 6;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 45.45454F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 54.54546F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 41F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 38F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 42F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 31F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(324, 231);
            this.tableLayoutPanel1.TabIndex = 2;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(10, 45);
            this.label7.Margin = new System.Windows.Forms.Padding(10, 10, 0, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(58, 17);
            this.label7.TabIndex = 1;
            this.label7.Text = "Position";
            // 
            // UpdateLight_btn
            // 
            this.UpdateLight_btn.Location = new System.Drawing.Point(3, 202);
            this.UpdateLight_btn.Name = "UpdateLight_btn";
            this.UpdateLight_btn.Size = new System.Drawing.Size(75, 25);
            this.UpdateLight_btn.TabIndex = 4;
            this.UpdateLight_btn.Text = "Update";
            this.UpdateLight_btn.UseVisualStyleBackColor = true;
            this.UpdateLight_btn.Click += new System.EventHandler(this.UpdateLight_btn_Click);
            // 
            // DeleteLight_btn
            // 
            this.DeleteLight_btn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.DeleteLight_btn.Location = new System.Drawing.Point(245, 202);
            this.DeleteLight_btn.Name = "DeleteLight_btn";
            this.DeleteLight_btn.Size = new System.Drawing.Size(76, 25);
            this.DeleteLight_btn.TabIndex = 5;
            this.DeleteLight_btn.Text = "Delete";
            this.DeleteLight_btn.UseVisualStyleBackColor = true;
            this.DeleteLight_btn.Click += new System.EventHandler(this.DeleteLight_btn_Click);
            // 
            // Angle_label
            // 
            this.Angle_label.AutoSize = true;
            this.Angle_label.Location = new System.Drawing.Point(10, 167);
            this.Angle_label.Margin = new System.Windows.Forms.Padding(10, 10, 0, 0);
            this.Angle_label.Name = "Angle_label";
            this.Angle_label.Size = new System.Drawing.Size(120, 17);
            this.Angle_label.TabIndex = 10;
            this.Angle_label.Text = "Inner;Outer Angle";
            // 
            // Angle_textBox
            // 
            this.Angle_textBox.Location = new System.Drawing.Point(172, 167);
            this.Angle_textBox.Margin = new System.Windows.Forms.Padding(10, 10, 0, 0);
            this.Angle_textBox.Name = "Angle_textBox";
            this.Angle_textBox.Size = new System.Drawing.Size(139, 23);
            this.Angle_textBox.TabIndex = 11;
            // 
            // LightDirection_label
            // 
            this.LightDirection_label.AutoSize = true;
            this.LightDirection_label.Location = new System.Drawing.Point(10, 129);
            this.LightDirection_label.Margin = new System.Windows.Forms.Padding(10, 10, 0, 0);
            this.LightDirection_label.Name = "LightDirection_label";
            this.LightDirection_label.Size = new System.Drawing.Size(64, 17);
            this.LightDirection_label.TabIndex = 2;
            this.LightDirection_label.Text = "Direction";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(10, 88);
            this.label9.Margin = new System.Windows.Forms.Padding(10, 10, 0, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(41, 17);
            this.label9.TabIndex = 3;
            this.label9.Text = "Color";
            // 
            // LightDirection_textBox
            // 
            this.LightDirection_textBox.Location = new System.Drawing.Point(172, 129);
            this.LightDirection_textBox.Margin = new System.Windows.Forms.Padding(10, 10, 0, 0);
            this.LightDirection_textBox.Name = "LightDirection_textBox";
            this.LightDirection_textBox.Size = new System.Drawing.Size(139, 23);
            this.LightDirection_textBox.TabIndex = 8;
            // 
            // ColorLight_btn
            // 
            this.ColorLight_btn.BackColor = System.Drawing.Color.White;
            this.ColorLight_btn.Location = new System.Drawing.Point(172, 88);
            this.ColorLight_btn.Margin = new System.Windows.Forms.Padding(10, 10, 0, 0);
            this.ColorLight_btn.Name = "ColorLight_btn";
            this.ColorLight_btn.Size = new System.Drawing.Size(139, 24);
            this.ColorLight_btn.TabIndex = 9;
            this.ColorLight_btn.UseVisualStyleBackColor = false;
            this.ColorLight_btn.Click += new System.EventHandler(this.ColorLight_btn_Click);
            // 
            // LightName_label
            // 
            this.LightName_label.AutoSize = true;
            this.LightName_label.Location = new System.Drawing.Point(172, 10);
            this.LightName_label.Margin = new System.Windows.Forms.Padding(10, 10, 0, 0);
            this.LightName_label.Name = "LightName_label";
            this.LightName_label.Size = new System.Drawing.Size(31, 17);
            this.LightName_label.TabIndex = 3;
            this.LightName_label.Text = "N/A";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(10, 10);
            this.label5.Margin = new System.Windows.Forms.Padding(10, 10, 0, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(76, 17);
            this.label5.TabIndex = 12;
            this.label5.Text = "LightName";
            // 
            // LightPosition_textBox
            // 
            this.LightPosition_textBox.Location = new System.Drawing.Point(172, 45);
            this.LightPosition_textBox.Margin = new System.Windows.Forms.Padding(10, 10, 0, 0);
            this.LightPosition_textBox.Name = "LightPosition_textBox";
            this.LightPosition_textBox.Size = new System.Drawing.Size(139, 23);
            this.LightPosition_textBox.TabIndex = 7;
            // 
            // Lights_listBox
            // 
            this.Lights_listBox.FormattingEnabled = true;
            this.Lights_listBox.ItemHeight = 16;
            this.Lights_listBox.Location = new System.Drawing.Point(6, 65);
            this.Lights_listBox.Name = "Lights_listBox";
            this.Lights_listBox.ScrollAlwaysVisible = true;
            this.Lights_listBox.Size = new System.Drawing.Size(324, 212);
            this.Lights_listBox.TabIndex = 0;
            this.Lights_listBox.SelectedIndexChanged += new System.EventHandler(this.Lights_listBox_SelectedIndexChanged);
            // 
            // preview_label
            // 
            this.preview_label.AutoSize = true;
            this.preview_label.Location = new System.Drawing.Point(3, 561);
            this.preview_label.Name = "preview_label";
            this.preview_label.Size = new System.Drawing.Size(88, 17);
            this.preview_label.TabIndex = 11;
            this.preview_label.Text = "Preview: N/A";
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
            this.CameraView_pictureBox.DoubleClick += new System.EventHandler(this.CameraView_pictureBox_DoubleClick);
            // 
            // Log_textBox
            // 
            this.Log_textBox.Location = new System.Drawing.Point(0, 727);
            this.Log_textBox.Multiline = true;
            this.Log_textBox.Name = "Log_textBox";
            this.Log_textBox.ReadOnly = true;
            this.Log_textBox.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.Log_textBox.Size = new System.Drawing.Size(1277, 109);
            this.Log_textBox.TabIndex = 7;
            // 
            // saveFileDialog1
            // 
            this.saveFileDialog1.DefaultExt = "scene";
            // 
            // LoadScene_openFileDialog
            // 
            this.LoadScene_openFileDialog.Filter = "\"Scene File|*scene";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1634, 861);
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
            this.cameraRotation_panel.ResumeLayout(false);
            this.cameraRotation_panel.PerformLayout();
            this.CameraProperties_panel.ResumeLayout(false);
            this.CameraProperties_panel.PerformLayout();
            this.Models_tab.ResumeLayout(false);
            this.Models_tab.PerformLayout();
            this.Lights_tab.ResumeLayout(false);
            this.Lights_tab.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.CameraView_pictureBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }



        #endregion

        private System.Windows.Forms.Panel MainWindow;
        private Label AvailableModels_label;
        private StatusStrip statusStrip1;
        private ToolStripStatusLabel toolStripStatusLabel1;
        private OpenFileDialog Import_openFileDialog1;
        private Panel LeftPanel;
        private ComboBox AvailableModels_combo;
        private ToolStripStatusLabel StatusLabel;
        private TextBox Log_textBox;
        private ToolStripStatusLabel CameraCoords_label;
        private TableLayoutPanel CameraProperties_panel;
        private Label Position_label;
        private Label AspectRatio_label;
        private Label FOVy_label;
        private Label Resolution_label;
        private TextBox Resolution_textBox;
        private TextBox FOVy_textBox;
        private TextBox AspectRatio_textBox;
        private TextBox Position_textBox;
        private MenuStrip menuStrip1;
        private ToolStripMenuItem toolStripMenuItem1;
        private ToolStripMenuItem startToolStripMenuItem;
        private ToolStripMenuItem quitToolStripMenuItem;
        private PictureBox CameraView_pictureBox;
        private Label label1;
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
        private TableLayoutPanel tableLayoutPanel1;
        private Label label6;
        private Label label7;
        private Label LightDirection_label;
        private Label label9;
        private ListBox Lights_listBox;
        private Button UpdateLight_btn;
        private Button DeleteLight_btn;
        private ComboBox LightType_combo;
        private TextBox LightDirection_textBox;
        private TextBox LightPosition_textBox;
        private Button ColorLight_btn;
        private ColorDialog LightColor_dialog;
        private Label Angle_label;
        private TextBox Angle_textBox;
        private Label LightName_label;
        private Label label5;
        private ListBox Cameras_listBox;
        private Button Help_btn;
        private Button Update_btn;
        private TextBox Direction_textBox;
        private Label Direction_label;
        private Button Delete_btn;
        private Label label8;
        private TextBox Rotation_textBox;
        private Label PitchAngle_label;
        private Label YawAngle_label;
        private Label label10;
        private Label Y;
        private VScrollBar Pitch_vScrollBar;
        private HScrollBar Yaw_hScrollBar;
        private Panel cameraRotation_panel;
        private ToolStripMenuItem loadToolStripMenuItem;
        private ToolStripMenuItem saveToolStripMenuItem;
        private SaveFileDialog saveFileDialog1;
        private OpenFileDialog LoadScene_openFileDialog;

    }
}

