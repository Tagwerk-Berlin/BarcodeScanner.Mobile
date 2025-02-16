﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Essentials;
using Plugin.Media;

namespace SampleApp.XF
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private async void Button1_Clicked(object sender, EventArgs e)
        {
            //Ask for permission first
            bool allowed = false;
            allowed = await BarcodeScanner.Mobile.XamarinForms.Methods.AskForRequiredPermission();
            if (allowed)
                Navigation.PushModalAsync(new NavigationPage(new Page1()));
            else DisplayAlert("Alert", "You have to provide Camera permission", "Ok");

        }

        private async void Button2_Clicked(object sender, EventArgs e)
        {
            //Ask for permission first
            bool allowed = false;
            allowed = await BarcodeScanner.Mobile.XamarinForms.Methods.AskForRequiredPermission();
            if (allowed)
                Navigation.PushModalAsync(new NavigationPage(new Page2()));
            else DisplayAlert("Alert", "You have to provide Camera permission", "Ok");
        }

        private async void Button3_Clicked(object sender, EventArgs e)
        {
            //Ask for permission first
            bool allowed = false;
            allowed = await BarcodeScanner.Mobile.XamarinForms.Methods.AskForRequiredPermission();
            if (allowed)
                Navigation.PushModalAsync(new NavigationPage(new Page3()));
            else DisplayAlert("Alert", "You have to provide Camera permission", "Ok");
        }

        private async void Button4_Clicked(object sender, EventArgs e)
        {
            var storageStatus = await Permissions.CheckStatusAsync<Permissions.StorageRead>();
            while (storageStatus != PermissionStatus.Granted)
            {
                storageStatus = await Permissions.RequestAsync<Permissions.StorageRead>();
            }
            if (storageStatus == PermissionStatus.Granted)
            {
                var file = await CrossMedia.Current.PickPhotoAsync();
                if (file == null)
                    return;
                Stream stream = file.GetStream();
                byte[] bytes = new byte[stream.Length];
                stream.Read(bytes, 0, bytes.Length);
                stream.Seek(0, SeekOrigin.Begin);
                List< BarcodeScanner.Mobile.Core.BarcodeResult > obj = await BarcodeScanner.Mobile.Core.Methods.ScanFromImage(bytes);
                if (obj.Count > 0)
                {
                    string result = string.Empty;
                    for (int i = 0; i < obj.Count; i++)
                    {
                        result += $"Type : {obj[i].BarcodeType}, Value : {obj[i].DisplayValue}{Environment.NewLine}";
                    }
                    Device.BeginInvokeOnMainThread(async () =>
                    {
                        await DisplayAlert("Result", result, "OK");
                    });
                }
                else
                {
                    Device.BeginInvokeOnMainThread(async () =>
                    {
                        await DisplayAlert("Result", "No barcode found!", "OK");
                    });
                }

            }
            else
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                    await DisplayAlert("Permissions Denied", "Unable to take photos.", "OK");
                });
            }

        }

        private async void Button5_Clicked(object sender, EventArgs e)
        {
            //Ask for permission first
            bool allowed = false;
            allowed = await BarcodeScanner.Mobile.XamarinForms.Methods.AskForRequiredPermission();
            if (allowed)
                Navigation.PushModalAsync(new NavigationPage(new Mvvm.MvvmDemo()));
            else DisplayAlert("Alert", "You have to provide Camera permission", "Ok");
        }


        private async void Button6_Clicked(object sender, EventArgs e)
        {
            //Ask for permission first
            bool allowed = false;
            allowed = await BarcodeScanner.Mobile.XamarinForms.Methods.AskForRequiredPermission();
            if (allowed)
                Navigation.PushModalAsync(new NavigationPage(new Page4()));
            else DisplayAlert("Alert", "You have to provide Camera permission", "Ok");
        }

        private async void Button7_Clicked(object sender, EventArgs e)
        {
            //Ask for permission first
            bool allowed = false;
            allowed = await BarcodeScanner.Mobile.XamarinForms.Methods.AskForRequiredPermission();
            if (allowed)
                Navigation.PushModalAsync(new NavigationPage(new ImageCapture.ImageCaptureDemo()));
            else DisplayAlert("Alert", "You have to provide Camera permission", "Ok");
        }
    }
}
