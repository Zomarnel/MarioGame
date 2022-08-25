using System.Windows.Media.Imaging;

namespace WPFUI.Models
{
    public class Sprite
    {
        public int ID { get; init; }
        public CroppedBitmap ImageSource { get; init; }

        public Sprite(int id, CroppedBitmap source)
        {
            ID = id;
            ImageSource = source;
        }
    }
}
