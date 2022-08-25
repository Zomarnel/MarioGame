using WPFUI.Models;
using System.Collections.Generic;
using System.Windows.Media.Imaging;
using System;
using System.Windows;
using System.Linq;

namespace WPFUI.Services
{
    public static class SpritesFactory
    {
        private static readonly List<Sprite> _sprites = new List<Sprite>();

        private static readonly BitmapImage _littleMarioSpritesheet; 
        static SpritesFactory()
        {

            _littleMarioSpritesheet = new BitmapImage(new Uri("/Images/Spritesheets/Mario/Little.png", UriKind.Relative));

            _littleMarioSpritesheet.BaseUri = Application.Current.StartupUri;

            //Standing
            AddNewSprite(0, new CroppedBitmap(_littleMarioSpritesheet, new Int32Rect(0, 0, 12, 15)));

            //Running animations
            AddNewSprite(1, new CroppedBitmap(_littleMarioSpritesheet, new Int32Rect(13, 0, 12, 15)));
            AddNewSprite(2, new CroppedBitmap(_littleMarioSpritesheet, new Int32Rect(26, 0, 15, 16)));

            //Jumping animation
            AddNewSprite(3, new CroppedBitmap(_littleMarioSpritesheet, new Int32Rect(42, 0, 16, 16)));

            //Braking animation
            AddNewSprite(4, new CroppedBitmap(_littleMarioSpritesheet, new Int32Rect(59, 0, 14, 16)));

            //Flag animation
            AddNewSprite(5, new CroppedBitmap(_littleMarioSpritesheet, new Int32Rect(74, 0, 13, 15)));
            AddNewSprite(6, new CroppedBitmap(_littleMarioSpritesheet, new Int32Rect(88, 0, 13, 16)));

            //Death sprite
            AddNewSprite(7, new CroppedBitmap(_littleMarioSpritesheet, new Int32Rect(102, 0, 16, 16)));

            //Victory animation
            AddNewSprite(8, new CroppedBitmap(_littleMarioSpritesheet, new Int32Rect(119, 0, 14, 16)));
            AddNewSprite(9, new CroppedBitmap(_littleMarioSpritesheet, new Int32Rect(134, 0, 16, 16)));

            //???
            AddNewSprite(10, new CroppedBitmap(_littleMarioSpritesheet, new Int32Rect(151, 0, 16, 16)));
            AddNewSprite(11, new CroppedBitmap(_littleMarioSpritesheet, new Int32Rect(168, 0, 14, 16)));
            AddNewSprite(11, new CroppedBitmap(_littleMarioSpritesheet, new Int32Rect(183, 0, 14, 16)));

        }
        public static CroppedBitmap GetSprite(int id)
        {
            CroppedBitmap? source = _sprites.FirstOrDefault(s => s.ID == id)?.ImageSource;

            if (source == null)
            {
                throw new ArgumentOutOfRangeException("No spite had the given ID, dumbass");
            }

            return source;
            
        }
        private static void AddNewSprite(int id, CroppedBitmap source)
        {
            _sprites.Add(new Sprite(id, source));
        }
    }
}
