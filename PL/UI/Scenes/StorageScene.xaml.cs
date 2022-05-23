using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
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

using System.Data.Entity;

using API_Controllers;
using API_Models;

namespace UI.Scenes {
    public partial class StorageScene : UserControl {

        // Scenes


        // IController
        private readonly IController<StorageModel> _storageController;
        private readonly IController<FolderModel> _folderController;
        private readonly IController<FileModel> _fileController;

        // Models
        public StorageModel Storage { get; set; }
        public FolderModel Folder { get; set; }
        public FileModel File { get; set; }

        // WeControlScenes
        public ContentControl WeControlScenes { get; set; }

        // Constructor
        public StorageScene(IController<StorageModel> storageController, IController<FolderModel> folderController, IController<FileModel> fileController) {

            // Initialize
            InitializeComponent();

            // Scenes


            // IController
            _storageController = storageController;
            _folderController = folderController;
            _fileController = fileController;

            // Models
            Storage = new StorageModel();
            Folder = new FolderModel();
            File = new FileModel();

            // Enums
            foreach(var category in Enum.GetValues(typeof(SpecificType))) { ComboBoxStorageSpecificType.Items.Add(category.ToString()); }

            // DB
            DataContext = this;

            // Start Methods
            UpdateDataGrid();
        }

        public int? SelectedId { get; set; }
        public int? SelectedIdNewTable { get; set; }

        private void StorageScene_Loaded(object sender, System.Windows.RoutedEventArgs e) { }

        // Text

        private void UpdateTextBlock() {
            string Owner = "Owner: " + Storage.Owner;
            TextBlockOwner.Text = Owner;
            TextBlockSpecificType.Text = Convert.ToString(Storage.SpecificType);
        }

        // Grid

        private void UpdateDataGrid() { StorageDataGrid.ItemsSource = _storageController.GetAll(); }
        private void UpdateDataGrid_OpenStorage() {
            ArrayList ListAll = new ArrayList();
            List<FolderModel> ListFolder = _folderController.GetAll();
            List<FileModel> ListFile = _fileController.GetAll();
            foreach(FileModel file in ListFile) { if(file.IdParent == (int)SelectedId) { ListAll.Add(file); } }
            foreach(FolderModel folder in ListFolder) { if(folder.IdParent == (int)SelectedId) { ListAll.Add(folder); } }
            StorageAllDataGrid.ItemsSource = ListAll;
        }

        // Clear

        private void ClearFields() { ComboBoxStorageSpecificType.SelectedIndex = 0; TextBoxOwner.Clear(); }
        private void ClearFields_OpenStorage() { TextBoxFolderName.Clear(); TextBoxFileName.Clear(); }

        // Button

        // Storage

        private void AddStorageButton(object sender, System.Windows.RoutedEventArgs e) {
            if(ComboBoxStorageSpecificType.SelectedItem == null || TextBoxOwner.Text.Trim().Length == 0) { return; }
            _storageController.Add(Storage);
            UpdateDataGrid(); ClearFields();
        }

        private void OpenStorageButton(object sender, RoutedEventArgs e) {
            if(ComboBoxStorageSpecificType.SelectedItem == null || TextBoxOwner.Text.Trim().Length == 0) { return; }
            if(SelectedId != null) {
                Storage.Id = (int)SelectedId; _storageController.Update(Storage);
                mainGrid.Visibility = Visibility.Collapsed; OpenStorageGrid.Visibility = Visibility;
                UpdateTextBlock(); UpdateDataGrid_OpenStorage();
            }
        }

        private void DeleteStorageButton(object sender, System.Windows.RoutedEventArgs e) {
            if(ComboBoxStorageSpecificType.SelectedItem == null || TextBoxOwner.Text.Trim().Length == 0) { return; }
            if(SelectedId != null) { _storageController.Remove((int)SelectedId); }
            UpdateDataGrid(); ClearFields();
        }

        // Storage MouseDoubleClick

        private void DataGridRow_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e) {
            StorageModel temp = (StorageModel)StorageDataGrid.SelectedItem;
            foreach(SpecificType specificType in Search.GetEnumValues<SpecificType>()) {
                if(specificType == temp.SpecificType) { ComboBoxStorageSpecificType.SelectedIndex = (int)specificType; }
            }
            TextBoxOwner.Text = temp.Owner;
        }

        // Folder

        private void AddFolderButton(object sender, System.Windows.RoutedEventArgs e) {
            if(TextBoxFolderName.Text.Trim().Length == 0) { return; }
            Folder.IdParent = Storage.Id;
            Folder.Name = TextBoxFolderName.Text;
            _folderController.Add(Folder);
            UpdateDataGrid_OpenStorage(); ClearFields_OpenStorage();
        }

        private void OpenFolderButton(object sender, RoutedEventArgs e) {
            if(TextBoxFolderName.Text.Trim().Length == 0) { return; }
            //WeControlScenes.Content = _openStorageScene;
        }

        private void DeleteFolderButton(object sender, System.Windows.RoutedEventArgs e) {
            if(TextBoxFolderName.Text.Trim().Length == 0) { return; }
            if(SelectedId != null) { _folderController.Remove((int)SelectedId); }
            UpdateDataGrid_OpenStorage(); ClearFields_OpenStorage();
        }

        // File

        private void AddFileButton(object sender, System.Windows.RoutedEventArgs e) {
            if(TextBoxFileName.Text.Trim().Length == 0) { return; }
            File.IdParent = Storage.Id;
            File.Name = TextBoxFileName.Text;
            _fileController.Add(File);
            UpdateDataGrid_OpenStorage(); ClearFields_OpenStorage();
        }

        private void DeleteFileButton(object sender, System.Windows.RoutedEventArgs e) {
            if(TextBoxFileName.Text.Trim().Length == 0) { return; }
            if(SelectedId != null) { _fileController.Remove((int)SelectedId); }
            UpdateDataGrid_OpenStorage(); ClearFields_OpenStorage();
        }

        // Back in Main

        private void BackButton(object sender, System.Windows.RoutedEventArgs e) { OpenStorageGrid.Visibility = Visibility.Collapsed; mainGrid.Visibility = Visibility; }

        // OpenStorage MouseDoubleClick

        private void DataGridRow_MouseDoubleClick_OpenStorage(object sender, System.Windows.Input.MouseButtonEventArgs e) {

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