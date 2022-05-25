using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;

using API_Controllers;
using API_Models;

namespace UI.Scenes {
    public partial class ContentScene : UserControl, INotifyPropertyChanged {

        // IController
        private readonly IController<StorageModel> _storageController;
        private readonly IController<FolderModel> _folderController;
        private readonly IController<FileModel> _fileController;

        // Models
        public StorageModel Storage { get; set; }
        public FolderModel Folder { get; set; }
        public FileModel File { get; set; }

        public ContentScene(IController<StorageModel> storageController, IController<FolderModel> folderController, IController<FileModel> fileController) {
            
            // Initialize
            InitializeComponent();
            
            // IController
            _storageController = storageController;
            _folderController = folderController;
            _fileController = fileController;

            // Models
            Storage = new StorageModel();
            Folder = new FolderModel();
            File = new FileModel();

            // Enums
            foreach(var category in Enum.GetValues(typeof(TypeFile))) { ComboBoxStorageType.Items.Add(category.ToString()); }

            // DB
            DataContext = this;
        }

        public int? SelectedId_C { get; set; }
        public int? SelectedId_D { get; set; }
        public int? SaveParentId { get; set; }
        public string SaveParentPath { get; set; }

        private void ContentScene_Loaded(object sender, System.Windows.RoutedEventArgs e) { }

        // Grid

        private void UpdateDataGrid() {
            if(RadioButton_Disk_C.IsChecked == true) {
                ArrayList ListAll = new ArrayList();
                List<FolderModel> ListFolder = _folderController.GetAll();
                List<FileModel> ListFile = _fileController.GetAll();
                if(Depth_C == 1) {
                    foreach(FileModel file in ListFile) { if(file.IdStorageParent == SaveParentId && file.OrDirectStorageParent == true) { ListAll.Add(file); } }
                    foreach(FolderModel folder in ListFolder) { if(folder.IdStorageParent == SaveParentId && folder.OrDirectStorageParent == true) { ListAll.Add(folder); } }
                } else {
                    foreach(FileModel file in ListFile) { if(file.IdStorageParent == -3 && file.IdFolderParent == SaveParentId) { ListAll.Add(file); } }
                    foreach(FolderModel folder in ListFolder) { if(folder.IdStorageParent == -3 && folder.IdFolderParent == SaveParentId) { ListAll.Add(folder); } }
                }
                DataGrid_C.ItemsSource = ListAll;
            } else if(RadioButton_Disk_D.IsChecked == true) {
                ArrayList ListAll = new ArrayList();
                List<FolderModel> ListFolder = _folderController.GetAll();
                List<FileModel> ListFile = _fileController.GetAll();
                if(Depth_D == 1) {
                    foreach(FileModel file in ListFile) { if(file.IdStorageParent == SaveParentId && file.OrDirectStorageParent == true) { ListAll.Add(file); } }
                    foreach(FolderModel folder in ListFolder) { if(folder.IdStorageParent == SaveParentId && folder.OrDirectStorageParent == true) { ListAll.Add(folder); } }
                } else {
                    foreach(FileModel file in ListFile) { if(file.IdStorageParent == -5 && file.IdFolderParent == SaveParentId) { ListAll.Add(file); } }
                    foreach(FolderModel folder in ListFolder) { if(folder.IdStorageParent == -5 && folder.IdFolderParent == SaveParentId) { ListAll.Add(folder); } }
                }
                DataGrid_D.ItemsSource = ListAll;
            }
        }

        private void UpdateDataGrid_Shearch() {
            if(RadioButton_Disk_D.IsChecked == true && RadioButton_Disk_C.IsChecked == false) {
                ArrayList ListAll = new ArrayList();
                List<FolderModel> ListFolder = _folderController.GetAll();
                List<FileModel> ListFile = _fileController.GetAll();
                if(Depth_D == 1) {
                    foreach(FileModel file in ListFile) { if(file.Name.ToLower().Contains(SearchValue_D.ToLower()) && file.IdStorageParent == -5 && file.IdFolderParent == 0) { ListAll.Add(file); } }
                    foreach(FolderModel folder in ListFolder) { if(folder.Name.ToLower().Contains(SearchValue_D.ToLower()) && folder.IdStorageParent == -5 && folder.IdFolderParent == 0) { ListAll.Add(folder); } }
                } else {
                    foreach(FileModel file in ListFile) { if(file.Name.ToLower().Contains(SearchValue_D.ToLower()) && file.IdStorageParent == -5 && file.IdFolderParent == SaveParentId) { ListAll.Add(file); } }
                    foreach(FolderModel folder in ListFolder) { if(folder.Name.ToLower().Contains(SearchValue_D.ToLower()) && folder.IdStorageParent == -5 && folder.IdFolderParent == SaveParentId) { ListAll.Add(folder); } }
                }
                DataGrid_D.ItemsSource = ListAll;
            } else if(RadioButton_Disk_D.IsChecked == false && RadioButton_Disk_C.IsChecked == true) {
                ArrayList ListAll = new ArrayList();
                List<FolderModel> ListFolder = _folderController.GetAll();
                List<FileModel> ListFile = _fileController.GetAll();
                if(Depth_C == 1) {
                    foreach(FileModel file in ListFile) { if(file.Name.ToLower().Contains(SearchValue_C.ToLower()) && file.IdStorageParent == -3 && file.IdFolderParent == 0) { ListAll.Add(file); } }
                    foreach(FolderModel folder in ListFolder) { if(folder.Name.ToLower().Contains(SearchValue_C.ToLower()) && folder.IdStorageParent == -3 && folder.IdFolderParent == 0) { ListAll.Add(folder); } }
                } else {
                    foreach(FileModel file in ListFile) { if(file.Name.ToLower().Contains(SearchValue_C.ToLower()) && file.IdStorageParent == -3 && file.IdFolderParent == SaveParentId) { ListAll.Add(file); } }
                    foreach(FolderModel folder in ListFolder) { if(folder.Name.ToLower().Contains(SearchValue_C.ToLower()) && folder.IdStorageParent == -3 && folder.IdFolderParent == SaveParentId) { ListAll.Add(folder); } }
                }
                DataGrid_C.ItemsSource = ListAll;
            } 
        }

        // Clear

        private void ClearFields() { ComboBoxStorageType.SelectedIndex = 0; TextBoxFolderName.Clear(); TextBoxFileName.Clear(); }

        // Folder

        private void AddFolderButton(object sender, System.Windows.RoutedEventArgs e) {
            if(TextBoxFolderName.Text.Trim().Length == 0) { return; }
            if(Depth_C == 1 && RadioButton_Disk_C.IsChecked == true) {
                SaveParentId = -3;
                Folder.IdStorageParent = (int)SaveParentId;
                Folder.Path = Folder.Name;
                Folder.OrDirectStorageParent = true;
            } else if(Depth_D == 1 && RadioButton_Disk_D.IsChecked == true) { 
                SaveParentId = -5;
                Folder.IdStorageParent = (int)SaveParentId;
                Folder.Path = Folder.Name;
                Folder.OrDirectStorageParent = true;
            } else {
                if(RadioButton_Disk_C.IsChecked == true) { Folder.IdStorageParent = -3; } 
                else if(RadioButton_Disk_D.IsChecked == true) { Folder.IdStorageParent = -5; }
                Folder.IdFolderParent = (int)SaveParentId;
                Folder.Path = SaveParentPath + "/" + Folder.Name;
                Folder.OrDirectStorageParent = false;
            }
            Folder.Name = TextBoxFolderName.Text;
            _folderController.Add(Folder);
            UpdateDataGrid(); ClearFields();
        }

        private List<int> ControllerId_C;
        private List<int> ControllerId_D;
        int Depth_C { get; set; } = 1;
        int Depth_D { get; set; } = 1;

        private void OpenFolderButton(object sender, RoutedEventArgs e) {
            if(TextBoxFolderName.Text.Trim().Length == 0) { return; }
            if(SelectedId_C != null && RadioButton_Disk_C.IsChecked == true) {
                Folder.Id = (int)SelectedId_C; _folderController.Update(Folder);
                SaveParentId = Folder.Id;
                if(Depth_C == 1) { ControllerId_C = new List<int>(); }
                Depth_C++;
                ControllerId_C.Add(-3);
                ControllerId_C.Add((int)SaveParentId);
                var buffer = _folderController.GetById((int)SelectedId_C);
                SaveParentPath = buffer.Path;
                UpdateDataGrid();
                return;
            }
            if(SelectedId_D != null && RadioButton_Disk_D.IsChecked == true) {
                Folder.Id = (int)SelectedId_D; _folderController.Update(Folder);
                SaveParentId = Folder.Id;
                if(Depth_D == 1) { ControllerId_D = new List<int>(); }
                Depth_D++;
                ControllerId_D.Add(-5);
                ControllerId_D.Add((int)SaveParentId);
                var buffer = _folderController.GetById((int)SelectedId_D);
                SaveParentPath = buffer.Path;
                UpdateDataGrid();
                return;
            }
        }

        // The folder and file hierarchy will be broken if you delete the folder through search.
        private void DeleteFolderButton(object sender, System.Windows.RoutedEventArgs e) {
            if(TextBoxFolderName.Text.Trim().Length == 0) { return; }
            if(SelectedId_C != null && RadioButton_Disk_C.IsChecked == true) { _folderController.Remove((int)SelectedId_C); }
            if(SelectedId_D != null && RadioButton_Disk_D.IsChecked == true) { _folderController.Remove((int)SelectedId_D); }
            UpdateDataGrid(); ClearFields();
        }

        // File

        private void AddFileButton(object sender, System.Windows.RoutedEventArgs e) {
            if(ComboBoxStorageType.SelectedItem == null || TextBoxFileName.Text.Trim().Length == 0) { return; }
            File.Name = TextBoxFileName.Text;
            File.Type = File.Name + "." + Convert.ToString(ComboBoxStorageType.SelectedItem);
            if(Depth_C == 1 && RadioButton_Disk_C.IsChecked == true) {
                SaveParentId = -3;
                File.IdStorageParent = (int)SaveParentId;
                File.Path = Convert.ToString(File.Type);
                File.OrDirectStorageParent = true;
            } else if(Depth_D == 1 && RadioButton_Disk_D.IsChecked == true) {
                SaveParentId = -5;
                File.IdStorageParent = (int)SaveParentId;
                File.Path = Convert.ToString(File.Type);
                File.OrDirectStorageParent = true;
            } else {
                if(RadioButton_Disk_C.IsChecked == true) { File.IdStorageParent = -3; }
                else if(RadioButton_Disk_D.IsChecked == true) { File.IdStorageParent = -5; }
                File.IdFolderParent = (int)SaveParentId;
                File.Path = SaveParentPath + "/" + Convert.ToString(File.Type);
                File.OrDirectStorageParent = false;
            }
            _fileController.Add(File);
            UpdateDataGrid(); ClearFields();
        }

        private void DeleteFileButton(object sender, System.Windows.RoutedEventArgs e) {
            if(ComboBoxStorageType.SelectedItem == null || TextBoxFileName.Text.Trim().Length == 0) { return; }
            if(SelectedId_C != null && RadioButton_Disk_C.IsChecked == true) { _fileController.Remove((int)SelectedId_C); }
            if(SelectedId_D != null && RadioButton_Disk_D.IsChecked == true) { _fileController.Remove((int)SelectedId_D); }
            UpdateDataGrid(); ClearFields();
        }

        // MouseDoubleClick

        private void DataGridRow_MouseDoubleClick_C(object sender, System.Windows.Input.MouseButtonEventArgs e) {

            RadioButton_Disk_C.IsChecked = true; RadioButton_Disk_D.IsChecked = false; SaveParentId = -3;

            if(DataGrid_C.SelectedItem.GetType() == typeof(API_Models.FileModel)) {
                FileModel tempFile = (FileModel)DataGrid_C.SelectedItem;
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

            if(DataGrid_C.SelectedItem.GetType() == typeof(API_Models.FolderModel)) {
                FolderModel tempFolder = (FolderModel)DataGrid_C.SelectedItem;
                TextBoxFolderName.Text = tempFolder.Name;
            }
        }
        
        private void DataGridRow_MouseDoubleClick_D(object sender, System.Windows.Input.MouseButtonEventArgs e) {

            RadioButton_Disk_D.IsChecked = true; RadioButton_Disk_C.IsChecked = false; SaveParentId = -5;

            if(DataGrid_D.SelectedItem.GetType() == typeof(API_Models.FileModel)) {
                FileModel tempFile = (FileModel)DataGrid_D.SelectedItem;
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

            if(DataGrid_D.SelectedItem.GetType() == typeof(API_Models.FolderModel)) {
                FolderModel tempFolder = (FolderModel)DataGrid_D.SelectedItem;
                TextBoxFolderName.Text = tempFolder.Name;
            }
        }

        // Back in Main

        private void BackButton_MovementInDisk_C(object sender, System.Windows.RoutedEventArgs e) {
            RadioButton_Disk_C.IsChecked = true; RadioButton_Disk_D.IsChecked = false;
            if(ControllerId_C == null) { return; }
            SaveParentId = ControllerId_C[Depth_C - 2];
            Depth_C--;
            if(Depth_C == 1) { ControllerId_C = null; }
            UpdateDataGrid(); ClearFields();
        }

        private void BackButton_Disk_C(object sender, System.Windows.RoutedEventArgs e) {
            RadioButton_Disk_C.IsChecked = true; RadioButton_Disk_D.IsChecked = false;
            Depth_C = 1; ControllerId_C = null; SaveParentId = -3;
            UpdateDataGrid(); ClearFields();
        }

        private void BackButton_MovementInDisk_D(object sender, System.Windows.RoutedEventArgs e) {
            RadioButton_Disk_D.IsChecked = true; RadioButton_Disk_C.IsChecked = false;
            if(ControllerId_D == null) { return; }
            SaveParentId = ControllerId_D[Depth_D - 2];
            Depth_D--;
            if(Depth_D == 1) { ControllerId_D = null; }
            UpdateDataGrid(); ClearFields();
        }

        private void BackButton_Disk_D(object sender, System.Windows.RoutedEventArgs e) {
            RadioButton_Disk_D.IsChecked = true; RadioButton_Disk_C.IsChecked = false;
            Depth_D = 1; ControllerId_D = null; SaveParentId = -5;
            UpdateDataGrid(); ClearFields();
        }

        // Search

        public event PropertyChangedEventHandler PropertyChanged = null;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null) {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        // Search Disk C

        private string searchValue_C;

        public string SearchValue_C {
            get => searchValue_C;
            set { if(searchValue_C != value) { searchValue_C = value; RadioButton_Disk_C.IsChecked = true; RadioButton_Disk_D.IsChecked = false; OnPropertyChanged(); UpdateDataGrid_Shearch(); } }
        }

        // Search Disk D

        private string searchValue_D;

        public string SearchValue_D {
            get => searchValue_D;
            set { if(searchValue_D != value) { searchValue_D = value; RadioButton_Disk_D.IsChecked = true; RadioButton_Disk_C.IsChecked = false; OnPropertyChanged(); UpdateDataGrid_Shearch(); } }
        }

    }
}