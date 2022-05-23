using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using API_Controllers;
using API_Models;

namespace UI.Scenes {
    public partial class OpenStorageScene : UserControl {

        private IController<StorageModel> _storageController;
        private readonly IController<FolderModel> _folderController;
        private readonly IController<FileModel> _fileController;

        public StorageModel Storage { get; set; }
        public FolderModel Folder { get; set; }
        public FileModel File { get; set; }

        // WeControlScenes

        public ContentControl WeControlScenes { get; set; }

        // Information

        public string POwner {get; set;}
        public SpecificType PSpecificType {get; set;}

        public void Information(IController<StorageModel> storageController, string pOwner, SpecificType pSpecificType) {
            _storageController = storageController; POwner = pOwner; PSpecificType = pSpecificType;
        }

        public OpenStorageScene(IController<FolderModel> folderController, IController<FileModel> fileController) {

            InitializeComponent();

            _folderController = folderController;
            _fileController = fileController;

            Folder = new FolderModel();
            File = new FileModel();

            DataContext = this;

            //Shearch();
            //UpdateTextBlock();
            //UpdateDataGrid();
        }

        public int? SelectedId { get; set; }

        private void OpenStorageScene_Loaded(object sender, System.Windows.RoutedEventArgs e) { }

        // Grid

        private void Shearch() {
            List<StorageModel> List = _storageController.GetAll();
            foreach(StorageModel o in List) { if(o.Owner == POwner && o.SpecificType == PSpecificType) { Storage = o; break; } }
        }

        private void UpdateTextBlock() {
            string Owner = "Owner: " + Storage.Owner; TextBlockOwner.Text = Owner;

        }

        private void UpdateDataGrid() {
            ArrayList List = new ArrayList();

            //foreach(FileModel file in Storage.ListFiles) { List.Add(file); }
            if(_storageController == null) { TextBoxFolderName.Text = "noob"; }
                //TextBoxFolderName.Text = Storage.Owner;
            //if(Storage.ListFiles != null) { StorageAllDataGrid.ItemsSource = Storage.ListFiles; } 
        }

        // Clear

        private void ClearFields() { TextBoxFolderName.Text = null; TextBoxFileName.Text = null; }

        // Button

        private void AddFolderButton(object sender, System.Windows.RoutedEventArgs e) {
            if(TextBoxFolderName.Text.Trim().Length == 0) { return; }
            Storage.ListFolders.Add(Folder); UpdateDataGrid(); ClearFields();
        }

        private void OpenFolderButton(object sender, RoutedEventArgs e) {
            //if(TextBoxFolderName.Text.Trim().Length == 0) { return; }
            //WeControlScenes.Content = _openStorageScene;
        }

        private void DeleteFolderButton(object sender, System.Windows.RoutedEventArgs e) {
            if(TextBoxFolderName.Text.Trim().Length == 0) { return; }
            foreach(FolderModel folder in Storage.ListFolders) {
                if(folder.Name == TextBoxFolderName.Text) { Storage.ListFolders.Remove(folder); }
            }
            UpdateDataGrid(); ClearFields();
        }

        private void AddFileButton(object sender, System.Windows.RoutedEventArgs e) {
            if(TextBoxFileName.Text.Trim().Length == 0) { return; }
            Storage.ListFiles.Add(File); UpdateDataGrid(); ClearFields();
        }

        private void DeleteFileButton(object sender, System.Windows.RoutedEventArgs e) {
            if(TextBoxFileName.Text.Trim().Length == 0) { return; }
            foreach(FileModel file in Storage.ListFiles) {
                if(file.Name == TextBoxFileName.Text) { Storage.ListFiles.Remove(file); }
            }
            UpdateDataGrid(); ClearFields();
        }

        private void DataGridRow_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e) {
            
            if(StorageAllDataGrid.SelectedItem.GetType() == typeof(API_Models.FileModel)) {
                FileModel tempFile = (FileModel)StorageAllDataGrid.SelectedItem;
                TextBoxFileName.Text = tempFile.Name;
            }

            if(StorageAllDataGrid.SelectedItem.GetType() == typeof(API_Models.FolderModel)) {
                FolderModel tempFolder = (FolderModel)StorageAllDataGrid.SelectedItem;
                TextBoxFolderName.Text = tempFolder.Name;
            }

        }

    }
}