using System;
using System.Collections.Generic;
using System.ComponentModel;
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

namespace UI.Scenes {
    public partial class ContentScene : UserControl, INotifyPropertyChanged {

        public ContentScene() {
            InitializeComponent();
        }
        // CRUD events
        private void ButtonAddHotel_Click(object sender, System.Windows.RoutedEventArgs e) {

        }

        private void ButtonUpdateHotel_Click(object sender, System.Windows.RoutedEventArgs e) {

        }

        private void ButtonRemoveHotel_Click(object sender, System.Windows.RoutedEventArgs e) {

        }

        private void TextBoxFloorsNumber_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e) {
            string inputSymbol = e.Text.ToString();

        }

        private void DataGridRow_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e) {

        }

        private void ButtonClearFields_Click(object sender, System.Windows.RoutedEventArgs e) { }

        private void ContentScene_Loaded(object sender, System.Windows.RoutedEventArgs e) {

        }

        //

        protected virtual void OnPropertyChanged() {
            
        }

        protected virtual bool OnPropertyChanged<T>(ref T backingField, T value) {
            if(EqualityComparer<T>.Default.Equals(backingField, value))
                return false;
            backingField = value;
            OnPropertyChanged();
            return true;
        }
        public event PropertyChangedEventHandler PropertyChanged;
    }
}