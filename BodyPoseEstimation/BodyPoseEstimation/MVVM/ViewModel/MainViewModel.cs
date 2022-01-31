using BodyPoseEstimation.Core;

namespace BodyPoseEstimation.MVVM.ViewModel
{
    class MainViewModel : ObservableObject
    {
        public RelayCommand HomeViewCommand { get; set; }
        public RelayCommand CloseCommand { get; set; }
        public RelayCommand AboutViewCommand { get; set; }
        public HomeViewModel HomeVM { get; set; }
        public HomeViewModel prevHomeVM { get; set; }
        public AboutViewModel AboutVM { get; set; }


        private object _currentView = null!;
        public object CurrentView
        {
            get { return _currentView; }
            set
            {
                _currentView = value;
                OnPropertyChanged();
            }
        }
        public MainViewModel()
        {
            HomeVM = HomeViewModel.Instance;
            AboutVM = new AboutViewModel();

            CurrentView = HomeVM;

            HomeViewCommand = new RelayCommand((o) =>
            {
                CurrentView = HomeVM;

            });

            AboutViewCommand = new RelayCommand((o) =>
            {
                CurrentView = AboutVM;
            });

            CloseCommand = new RelayCommand((o) =>
            {
                System.Windows.Application.Current.Shutdown();
            });
        }
    }
}