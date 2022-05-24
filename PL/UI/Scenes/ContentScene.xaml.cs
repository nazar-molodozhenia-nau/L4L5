using System.Windows.Controls;

using API_Controllers;
using API_Models;

namespace UI.Scenes {
    public partial class ContentScene : UserControl {

        // IController
        private readonly IController<StorageModel> _storageController;
        private readonly IController<FolderModel> _folderController;
        private readonly IController<FileModel> _fileController;

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

            // Models
            Storage = new StorageModel();
            Folder = new FolderModel();
            File = new FileModel();

            // DB
            DataContext = this;
        }

        private void ContentScene_Loaded(object sender, System.Windows.RoutedEventArgs e) { }

    }
}