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

using API_Controllers;
using API_Models;

namespace UI.Scenes {
    public partial class ContentScene : UserControl {

        // IController
        private IController<StorageModel> _storageController { get; set; }
        private IController<FolderModel> _folderController { get; set; }
        private IController<FileModel> _fileController { get; set; }

        // Models
        public StorageModel Storage { get; set; }
        public FolderModel Folder { get; set; }
        public FileModel File { get; set; }

        public ContentScene(IController<StorageModel> storageController, IController<FolderModel> folderController, IController<FileModel> fileController) {

            InitializeComponent();
            
            // IController
            _storageController = storageController;
            _folderController = folderController;
            _fileController = fileController;
        }

        private void ContentScene_Loaded(object sender, System.Windows.RoutedEventArgs e) { }

    }
}