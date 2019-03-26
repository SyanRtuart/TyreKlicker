using System.Drawing;

namespace TyreKlicker.XF.Core.Services.Messages
{
    public interface IMessage
    {
        void LongAlert(string message, Color color);

        void ShortAlert(string message, Color color);
    }
}