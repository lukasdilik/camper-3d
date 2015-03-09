using System;
using System.Windows.Forms;
using ApplicationLogic;

namespace ApplicationUI
{
    public partial class LibraryForm : Form
    {
        private readonly AppController mAppController;
        public LibraryForm(AppController appController)
        {
            mAppController = appController;
            InitializeComponent();
            if (Models_listBox.Items.Count > 0)
            {
                Models_listBox.SelectedIndex = 0;
            }
        }

        private void LibraryForm_Activated(object sender, System.EventArgs e)
        {
            RefreshModelList();
        }

        private void AddNew_button_Click(object sender, System.EventArgs e)
        {
            DialogResult result = folderBrowserDialog1.ShowDialog();
            if (result == DialogResult.OK)
            {
                try
                {
                    mAppController.ModelLibrary.ImportModel(folderBrowserDialog1.SelectedPath);
                    MessageBox.Show("Model imported successfully");
                }
                catch (Exception exception)
                {
                    MessageBox.Show(exception.ToString());
                }
            }
            RefreshModelList();
        }

        private void RefreshModelList()
        {
            Models_listBox.Items.Clear();
            mAppController.ModelLibrary.GetAvailableModelsName().ForEach(x => Models_listBox.Items.Add(x));
        }

        private void Remove_button_Click(object sender, EventArgs e)
        {
            if (Models_listBox.SelectedIndex > -1)
            {
                mAppController.ModelLibrary.RemoveModel((string) Models_listBox.SelectedItem);
            }
            RefreshModelList();
        }

        private void Models_listBox_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            SelectModelData((string) Models_listBox.SelectedItem);
        }

        private void SelectModelData(string modelName)
        {
            var modelData = mAppController.ModelLibrary.GetModel(modelName);
            if (modelData != null)
            {

                MeshNameValue_label.Text = modelData.Name;

                Materials_comboBox.Items.Clear();
                modelData.Materials.ForEach(x => Materials_comboBox.Items.Add(x));
                if (Materials_comboBox.Items.Count > 0)
                {
                    Materials_comboBox.SelectedIndex = 0;
                }

                Textures_comboBox.Items.Clear();
                modelData.Textures.ForEach(x => Textures_comboBox.Items.Add(x));
                if (Textures_comboBox.Items.Count > 0)
                {
                    Textures_comboBox.SelectedIndex = 0;
                }
            }
        }

        private void LibraryForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Hide();
            e.Cancel = true; 
        }

        private void LibraryForm_Shown(object sender, System.EventArgs e)
        {
            if (Models_listBox.Items.Count > 0)
            {
                Models_listBox.SelectedIndex = 0;
            }
        }

        private void LibraryForm_Load(object sender, System.EventArgs e)
        {
            if (Models_listBox.Items.Count > 0)
            {
                Models_listBox.SelectedIndex = 0;
            }
        }
    }
}
