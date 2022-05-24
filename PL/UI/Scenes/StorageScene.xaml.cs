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
using System.Text.RegularExpressions;

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
            foreach(var category in Enum.GetValues(typeof(TypeFile))) { ComboBoxStorageType.Items.Add(category.ToString()); }

            // DB
            DataContext = this;

            // Start Methods
            UpdateDataGrid();
        }

        public int? SelectedId { get; set; }
        public int? SelectedIdNewTable { get; set; }
        public int? SaveParentId { get; set; }
        public string SaveParentPath { get; set; }

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
            foreach(FileModel file in ListFile) { if(file.IdParent == SaveParentId) { ListAll.Add(file); } }
            foreach(FolderModel folder in ListFolder) { if(folder.IdParent == SaveParentId) { ListAll.Add(folder); } }
            StorageAllDataGrid.ItemsSource = ListAll;
        }

        // Clear

        private void ClearFields() { ComboBoxStorageSpecificType.SelectedIndex = 0; TextBoxOwner.Clear(); }
        private void ClearFields_OpenStorage() { ComboBoxStorageType.SelectedIndex = 0; TextBoxFolderName.Clear(); TextBoxFileName.Clear(); }

        // Button

        // Storage

        private void AddStorageButton(object sender, System.Windows.RoutedEventArgs e) {
            if(ComboBoxStorageSpecificType.SelectedItem == null || TextBoxOwner.Text.Trim().Length == 0) { return; }
            _storageController.Add(Storage);
            UpdateDataGrid(); ClearFields();
        }

        List<int> ControllerId;
        int Depth { get; set; } = 0;

        private void OpenStorageButton(object sender, RoutedEventArgs e) {
            if(ComboBoxStorageSpecificType.SelectedItem == null || TextBoxOwner.Text.Trim().Length == 0) { return; }
            if(SelectedId != null) {
                Storage.Id = (int)SelectedId; _storageController.Update(Storage);
                mainGrid.Visibility = Visibility.Collapsed; OpenStorageGrid.Visibility = Visibility;
                // Set SaveParentId and/or SaveTapId && Depth
                SaveParentId = Storage.Id;
                Depth = 1;
                ControllerId = new List<int>();
                //SaveParentPath = "";
                ControllerId.Add((int)SaveParentId);
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
            foreach(SpecificType specificType in SearchSpecificType.GetEnumValues<SpecificType>()) {
                if(specificType == temp.SpecificType) { ComboBoxStorageSpecificType.SelectedIndex = (int)specificType; }
            }
            TextBoxOwner.Text = temp.Owner;
        }

        // Folder

        int CounterFolder { get; set; }

        private void AddFolderButton(object sender, System.Windows.RoutedEventArgs e) {
            if(TextBoxFolderName.Text.Trim().Length == 0) { return; }
            Folder.IdParent = (int)SaveParentId;
            Folder.Name = TextBoxFolderName.Text;
            //Folder.Path = SaveParentPath + "/" + Folder.Name;
            _folderController.Add(Folder);
            UpdateDataGrid_OpenStorage(); ClearFields_OpenStorage();
        }

        private void OpenFolderButton(object sender, RoutedEventArgs e) {
            if(TextBoxFolderName.Text.Trim().Length == 0) { return; }
            if(SelectedIdNewTable != null) {
                // Id is Not Set
                //Folder.Id = (int)SelectedIdNewTable + 1000; _folderController.Update(Folder);
                // Set SaveParentId and/or SaveTapId && Depth
                SaveParentId = Folder.Id;
                Depth++;
                //SaveParentPath = Folder.Path;
                ControllerId.Add((int)SaveParentId);
                ButtonBack_MovementInStorage.Visibility = Visibility;
                UpdateDataGrid_OpenStorage();
            }
        }

        private void DeleteFolderButton(object sender, System.Windows.RoutedEventArgs e) {
            if(TextBoxFolderName.Text.Trim().Length == 0) { return; }
            if(SelectedIdNewTable != null) { _folderController.Remove((int)SelectedIdNewTable); }
            UpdateDataGrid_OpenStorage(); ClearFields_OpenStorage();
        }

        // File

        int CounterFile { get; set; } = 2000;

        private void AddFileButton(object sender, System.Windows.RoutedEventArgs e) {
            if(ComboBoxStorageType.SelectedItem == null || TextBoxFileName.Text.Trim().Length == 0) { return; }
            File.IdParent = (int)SaveParentId;
            File.Name = TextBoxFileName.Text;
            //if(SaveParentPath == "") { File.Path = File.Name; } 
            //else { File.Path = SaveParentPath + "/" + File.Name; }
            File.Type = File.Name + "." + Convert.ToString(ComboBoxStorageType.SelectedItem);
            _fileController.Add(File);
            UpdateDataGrid_OpenStorage(); ClearFields_OpenStorage();
        }

        private void DeleteFileButton(object sender, System.Windows.RoutedEventArgs e) {
            if(ComboBoxStorageType.SelectedItem == null || TextBoxFileName.Text.Trim().Length == 0) { return; }
            if(SelectedIdNewTable != null) { _fileController.Remove((int)SelectedIdNewTable); }
            UpdateDataGrid_OpenStorage(); ClearFields_OpenStorage();
        }

        // Back in Main

        private void BackButton__MovementInStorage(object sender, System.Windows.RoutedEventArgs e) {
            SaveParentId = ControllerId[Depth - 2];
            Depth--;
            if(Depth == 1) { ButtonBack_MovementInStorage.Visibility = Visibility.Hidden; }
            UpdateDataGrid_OpenStorage(); ClearFields_OpenStorage();
        }

        private void BackButton_MainMenu(object sender, System.Windows.RoutedEventArgs e) { Depth = 0; OpenStorageGrid.Visibility = Visibility.Collapsed; mainGrid.Visibility = Visibility; }

        // OpenStorage MouseDoubleClick

        private void DataGridRow_MouseDoubleClick_OpenStorage(object sender, System.Windows.Input.MouseButtonEventArgs e) {

            if(StorageAllDataGrid.SelectedItem.GetType() == typeof(API_Models.FileModel)) {
                FileModel tempFile = (FileModel)StorageAllDataGrid.SelectedItem;
                TextBoxFileName.Text = tempFile.Name;

                Regex firstRegex = new Regex("\\..+");
                Regex secondRegex = new Regex("[^\\.]+");
                MatchCollection firstMatches = firstRegex.Matches(tempFile.Type);
                string bufferType = Convert.ToString(firstMatches[0]);
                MatchCollection secondMatches = secondRegex.Matches(bufferType);
                bufferType = Convert.ToString(secondMatches[0]);

                foreach(TypeFile type in SearchTypeFile.GetEnumValues<TypeFile>()) {
                    if(Convert.ToString(type) == bufferType) { ComboBoxStorageType.SelectedIndex = (int)type; }
                }
            }

            if(StorageAllDataGrid.SelectedItem.GetType() == typeof(API_Models.FolderModel)) {
                FolderModel tempFolder = (FolderModel)StorageAllDataGrid.SelectedItem;
                TextBoxFolderName.Text = tempFolder.Name;
            }

        }

    }
}