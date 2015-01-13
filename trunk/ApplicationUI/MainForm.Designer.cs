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
            this.label1 = new System.Windows.Forms.Label();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.StatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.CameraCoords_label = new System.Windows.Forms.ToolStripStatusLabel();
            this.AddFile_btn = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.panel1 = new System.Windows.Forms.Panel();
            this.AvailableModels_combo = new System.Windows.Forms.ComboBox();
            this.Start_btn = new System.Windows.Forms.Button();
            this.Log_textBox = new System.Windows.Forms.TextBox();
            this.Camera_listBox = new System.Windows.Forms.ListBox();
            this.statusStrip1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // MainWindow
            // 
            this.MainWindow.BackColor = System.Drawing.Color.White;
            this.MainWindow.Location = new System.Drawing.Point(0, 0);
            this.MainWindow.Margin = new System.Windows.Forms.Padding(4);
            this.MainWindow.Name = "MainWindow";
            this.MainWindow.Size = new System.Drawing.Size(1233, 742);
            this.MainWindow.TabIndex = 0;
            this.MainWindow.MouseClick += new System.Windows.Forms.MouseEventHandler(this.MainWindow_MouseClick);
            this.MainWindow.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.MainWindow_MouseDoubleClick);
            this.MainWindow.MouseDown += new System.Windows.Forms.MouseEventHandler(this.MainWindow_MouseDown);
            this.MainWindow.MouseLeave += new System.EventHandler(this.MainWindow_MouseLeave);
            this.MainWindow.MouseMove += new System.Windows.Forms.MouseEventHandler(this.MainWindow_MouseMove);
            this.MainWindow.MouseUp += new System.Windows.Forms.MouseEventHandler(this.MainWindow_MouseUp);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 10);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(118, 17);
            this.label1.TabIndex = 1;
            this.label1.Text = "Available models:";
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
            // panel1
            // 
            this.panel1.Controls.Add(this.Camera_listBox);
            this.panel1.Controls.Add(this.AvailableModels_combo);
            this.panel1.Controls.Add(this.Start_btn);
            this.panel1.Controls.Add(this.AddFile_btn);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel1.Location = new System.Drawing.Point(1239, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(345, 839);
            this.panel1.TabIndex = 6;
            // 
            // AvailableModels_combo
            // 
            this.AvailableModels_combo.FormattingEnabled = true;
            this.AvailableModels_combo.Location = new System.Drawing.Point(16, 42);
            this.AvailableModels_combo.Name = "AvailableModels_combo";
            this.AvailableModels_combo.Size = new System.Drawing.Size(248, 24);
            this.AvailableModels_combo.TabIndex = 7;
            this.AvailableModels_combo.SelectedIndexChanged += new System.EventHandler(this.AvailableModels_combo_SelectedIndexChanged);
            // 
            // Start_btn
            // 
            this.Start_btn.AutoSize = true;
            this.Start_btn.Location = new System.Drawing.Point(16, 81);
            this.Start_btn.Name = "Start_btn";
            this.Start_btn.Size = new System.Drawing.Size(131, 27);
            this.Start_btn.TabIndex = 6;
            this.Start_btn.Text = "Start";
            this.Start_btn.UseVisualStyleBackColor = true;
            this.Start_btn.Click += new System.EventHandler(this.Start_btn_Click);
            // 
            // Log_textBox
            // 
            this.Log_textBox.Location = new System.Drawing.Point(0, 749);
            this.Log_textBox.Multiline = true;
            this.Log_textBox.Name = "Log_textBox";
            this.Log_textBox.Size = new System.Drawing.Size(1233, 87);
            this.Log_textBox.TabIndex = 7;
            // 
            // Camera_listBox
            // 
            this.Camera_listBox.FormattingEnabled = true;
            this.Camera_listBox.ItemHeight = 16;
            this.Camera_listBox.Location = new System.Drawing.Point(16, 640);
            this.Camera_listBox.Name = "Camera_listBox";
            this.Camera_listBox.Size = new System.Drawing.Size(317, 196);
            this.Camera_listBox.TabIndex = 8;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1584, 861);
            this.Controls.Add(this.Log_textBox);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.MainWindow);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MinimumSize = new System.Drawing.Size(1024, 768);
            this.Name = "MainForm";
            this.Text = "CAMPER";
            this.Disposed += new System.EventHandler(this.MainForm_Disposed);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }



        #endregion

        private System.Windows.Forms.Panel MainWindow;
        private Label label1;
        private StatusStrip statusStrip1;
        private ToolStripStatusLabel toolStripStatusLabel1;
        private Button AddFile_btn;
        private OpenFileDialog openFileDialog1;
        private Panel panel1;
        private Button Start_btn;
        private ComboBox AvailableModels_combo;
        private ToolStripStatusLabel StatusLabel;
        private TextBox Log_textBox;
        private ToolStripStatusLabel CameraCoords_label;
        private ListBox Camera_listBox;

    }
}

