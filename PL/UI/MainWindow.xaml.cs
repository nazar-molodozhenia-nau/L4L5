using System.Windows;
using System.Windows.Input;

using UI.Scenes;

namespace UI {
    public partial class MainWindow : Window {

        private readonly ContentScene _contentScene;
        private readonly StorageScene _storageScene;

        public MainWindow(ContentScene contentScene, StorageScene storageScene) {
            InitializeComponent();
            _contentScene = contentScene;
            _storageScene = storageScene;
            _storageScene.WeControlScenes = WeControlScenes;
            WeControlScenes.Content = contentScene;
        }

        // MainGrid_Header

        private void ButtonMinimizeWindow(object sender, RoutedEventArgs e) => WindowState = WindowState.Minimized;

        private void ButtonMaximizeWindow(object sender, RoutedEventArgs e) {
            if(WindowState == WindowState.Normal) { WindowState = WindowState.Maximized; } else { WindowState = WindowState.Normal; }
        }

        private void ButtonCloseWindow(object sender, RoutedEventArgs e) => App.Current.Shutdown();

        private void MainGrid_Header_MouseDown(object sender, MouseButtonEventArgs e) { if(e.LeftButton == MouseButtonState.Pressed) { DragMove(); } }

        // Scenes

        private void ContentSceneButton(object sender, RoutedEventArgs e) { WeControlScenes.Content = _contentScene; }

        private void StorageSceneButton(object sender, RoutedEventArgs e) { WeControlScenes.Content = _storageScene; }

    }
}