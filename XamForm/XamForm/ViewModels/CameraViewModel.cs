using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace XamForm.ViewModels
{
    public class CameraViewModel : BaseViewModel
    {
        public Command OnUploadPhoto { get; }
        public Command OnUploadVideo { get; }
        public CameraViewModel()
        {
            Title = "拍照-拍视频";
            OnUploadPhoto = new Command(OnUploadPhotoRun);
            OnUploadVideo = new Command(OnUploadVideoRun);
        }
        private const string Action_TakePhoto = "拍摄照片";
        private const string Action_PickPhoto = "本地相册";
        private const string Action_TakeVideo = "拍摄视频";
        private const string Action_PickVideo = "本地视频";
        private async void OnUploadPhotoRun(object sender)
        {
           
        }
        private async void OnUploadVideoRun(object obj)
        {
        }
    }
}
