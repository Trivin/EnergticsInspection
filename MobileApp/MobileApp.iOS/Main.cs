using System;
using System.Collections.Generic;
using System.Linq;

using Foundation;
using MobileApp.Models;
using UIKit;
using Xamarin.Forms;
using static MobileApp.iOS.Application;

[assembly: Xamarin.Forms.Dependency(typeof(MessageIOS))]
namespace MobileApp.iOS
{
    public class Application
    {
        // This is the main entry point of the application.
        static void Main(string[] args)
        {
            // if you want to use a different Application Delegate class from "AppDelegate"
            // you can specify it here.

            // If you are using Android you must pass through the activity
            UIApplication.Main(args, null, "AppDelegate");
        }

        public class MessageIOS : IMessage
        {
            const double LONG_DELAY = 3.5;
            const double SHORT_DELAY = 0.75;

            public void LongAlert(string message)
            {
                ShowAlert(message, LONG_DELAY);
            }

            public void ShortAlert(string message)
            {
                ShowAlert(message, SHORT_DELAY);
            }

            void ShowAlert(string message, double seconds)
            {
                var alert = UIAlertController.Create(null, message, UIAlertControllerStyle.Alert);

                var alertDelay = NSTimer.CreateScheduledTimer(seconds, obj =>
                {
                    DismissMessage(alert, obj);
                });

                UIApplication.SharedApplication.KeyWindow.RootViewController.PresentViewController(alert, true, null);
            }

            void DismissMessage(UIAlertController alert, NSTimer alertDelay)
            {
                if (alert != null)
                {
                    alert.DismissViewController(true, null);
                }

                if (alertDelay != null)
                {
                    alertDelay.Dispose();
                }
            }
        }
    }
}
