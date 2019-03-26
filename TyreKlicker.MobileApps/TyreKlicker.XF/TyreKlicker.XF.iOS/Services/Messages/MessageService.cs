using Foundation;
using System.Drawing;
using TyreKlicker.XF.Core.Services.Messages;
using UIKit;

namespace TyreKlicker.iOS.Services.Messages
{
    public class MessageService : IMessage
    {
        private const double LONG_DELAY = 3.5;
        private const double SHORT_DELAY = 0.75;

        public void LongAlert(string message, Color color)
        {
            ShowAlert(message, LONG_DELAY);
        }

        public void ShortAlert(string message, Color color)
        {
            ShowAlert(message, SHORT_DELAY);
        }

        private void ShowAlert(string message, double seconds)
        {
            var alert = UIAlertController.Create(null, message, UIAlertControllerStyle.Alert);

            var alertDelay = NSTimer.CreateScheduledTimer(seconds, obj =>
            {
                DismissMessage(alert, obj);
            });

            UIApplication.SharedApplication.KeyWindow.RootViewController.PresentViewController(alert, true, null);
        }

        private void DismissMessage(UIAlertController alert, NSTimer alertDelay)
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