using System;
using BodyPoseEstimation.Core;

namespace BodyPoseEstimation.MVVM.ViewModel
{
    class HomeViewModel : ObservableObject
    {
        public HomeViewModel()
        {
            
        }

        public static HomeViewModel Instance
        {
            get;
        } = new HomeViewModel();
    }
}