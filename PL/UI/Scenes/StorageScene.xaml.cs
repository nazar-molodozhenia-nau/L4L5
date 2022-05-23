using System;
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

using API_Controllers;
using API_Models;

namespace UI.Scenes {
    public partial class StorageScene : UserControl {

        private readonly OpenStorageScene _openStorageScene;

        private readonly IController<StorageModel> _storageController;

        public StorageModel Storage { get; set; }

        // WeControlScenes

        public ContentControl WeControlScenes { get; set; }

        public StorageScene(IController<StorageModel> storageController, OpenStorageScene openStorageScene) {

            InitializeComponent();

            _storageController = storageController;

            _openStorageScene = openStorageScene;

            Storage = new StorageModel();

            foreach(var category in Enum.GetValues(typeof(SpecificType))) { ComboBoxStorageSpecificType.Items.Add(category.ToString()); }

            DataContext = this;

            UpdateDataGrid();
        }

        public int? SelectedId { get; set; }

        private void StorageScene_Loaded(object sender, System.Windows.RoutedEventArgs e) { }

        // Grid

        private void UpdateDataGrid() { StorageDataGrid.ItemsSource = _storageController.GetAll(); }

        // Clear

        private void ClearFields() {
            ComboBoxStorageSpecificType.SelectedIndex = 0;
            TextBoxOwner.Clear();
        }

        // Button

        private void AddStorageButton(object sender, System.Windows.RoutedEventArgs e) {
            if(ComboBoxStorageSpecificType.SelectedItem == null || TextBoxOwner.Text.Trim().Length == 0) { return; }
            _storageController.Add(Storage); UpdateDataGrid(); ClearFields();
        }

        private void OpenStorageButton(object sender, RoutedEventArgs e) {
            if(ComboBoxStorageSpecificType.SelectedItem == null || TextBoxOwner.Text.Trim().Length == 0) { return; }
            List<StorageModel> List = _storageController.GetAll();
            bool flag = false;
            foreach(StorageModel o in List) { if(o.Owner == TextBoxOwner.Text && o.SpecificType == (SpecificType)ComboBoxStorageSpecificType.SelectedItem) { flag = true; } }
            if(flag == true) { _openStorageScene.Information(_storageController, TextBoxOwner.Text, (SpecificType)ComboBoxStorageSpecificType.SelectedItem);  WeControlScenes.Content = _openStorageScene; }
        }

        private void DeleteStorageButton(object sender, System.Windows.RoutedEventArgs e) {
            if(ComboBoxStorageSpecificType.SelectedItem == null || TextBoxOwner.Text.Trim().Length == 0) { return; }
            if(SelectedId != null) { _storageController.Remove((int)SelectedId); ; UpdateDataGrid(); ClearFields(); } else { ClearFields(); }
        }

        private void DataGridRow_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e) {
            StorageModel temp = (StorageModel)StorageDataGrid.SelectedItem;
            foreach(SpecificType specificType in Search.GetEnumValues<SpecificType>()) {
                if(specificType == temp.SpecificType) {
                    ComboBoxStorageSpecificType.SelectedIndex = (int)specificType;
                }
            }
            TextBoxOwner.Text = temp.Owner;
        }

    }
}