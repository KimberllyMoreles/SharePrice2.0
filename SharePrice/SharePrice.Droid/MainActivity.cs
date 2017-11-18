using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using DryIoc;
using Prism.DryIoc;
using Plugin.Media;
using Android.Net;
using Android.Content;

namespace SharePrice.Droid
{
    [Activity(Label = "SharePrice", Icon = "@drawable/icon", MainLauncher = false, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            TabLayoutResource = Resource.Layout.tabs;
            ToolbarResource = Resource.Layout.toolbar;

            base.OnCreate(bundle);

            //await CrossMedia.Current.Initialize();
            global::Xamarin.Forms.Forms.Init(this, bundle);

            OxyPlot.Xamarin.Forms.Platform.Android.PlotViewRenderer.Init();
            Microsoft.WindowsAzure.MobileServices.CurrentPlatform.Init();

            LoadApplication(new App(new AndroidInitializer()));
        }
    }



    public class AndroidInitializer : IPlatformInitializer
    {
        public void RegisterTypes(IContainer container)
        {

        }
    }
}

