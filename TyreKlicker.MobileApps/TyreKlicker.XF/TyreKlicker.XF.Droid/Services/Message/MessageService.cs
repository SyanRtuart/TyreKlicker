using Android.App;
using Android.Graphics;
using Android.Widget;
using TyreKlicker.XF.Core.Services.Messages;

namespace TyreKlicker.XF.Droid.Services.Message
{
    internal class MessageService : IMessage
    {
        public void LongAlert(string message, System.Drawing.Color color)
        {
            var toast = Toast.MakeText(Application.Context, message, ToastLength.Long);
            var colorMatrix = new ColorMatrixColorFilter(new float[]
            {
                0,0,0,0,color.R,
                0,0,0,0,color.G,
                0,0,0,0,color.B,
                0,0,0,1,0
            });
            toast.View.Background.SetColorFilter(colorMatrix);
            toast.Show();
        }

        public void ShortAlert(string message, System.Drawing.Color color)
        {
            var toast = Toast.MakeText(Application.Context, message, ToastLength.Short);
            var colorMatrix = new ColorMatrixColorFilter(new float[]
            {
                0,0,0,0,color.R,
                0,0,0,0,color.G,
                0,0,0,0,color.B,
                0,0,0,1,0
            });
            toast.View.Background.SetColorFilter(colorMatrix);
            toast.Show();
        }
    }
}