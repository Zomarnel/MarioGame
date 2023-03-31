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

        private static readonly BitmapImage _blocksSpritesheet;

        private static readonly BitmapImage _mushroomSpritesheet;
        static SpritesFactory()
        {

            _littleMarioSpritesheet = new BitmapImage(new Uri("/Images/Spritesheets/Mario/Little.png", UriKind.Relative));

            _littleMarioSpritesheet.BaseUri = Application.Current.StartupUri;

            _blocksSpritesheet = new BitmapImage(new Uri("/Images/Blocks/Blocks.png", UriKind.Relative));

            _blocksSpritesheet.BaseUri = Application.Current.StartupUri;

            _mushroomSpritesheet = new BitmapImage(new Uri("/Images/Mobs/Mushroom.png", UriKind.Relative));

            _mushroomSpritesheet.BaseUri = Application.Current.StartupUri;

            //Standing
            AddNewSprite(1, new CroppedBitmap(_littleMarioSpritesheet, new Int32Rect(0, 0, 12, 15)));

            AddNewSprite(-1, new CroppedBitmap(_littleMarioSpritesheet, new Int32Rect(0, 18, 12, 15)));

            //Running animations
            AddNewSprite(2, new CroppedBitmap(_littleMarioSpritesheet, new Int32Rect(13, 0, 12, 15)));
            AddNewSprite(3, new CroppedBitmap(_littleMarioSpritesheet, new Int32Rect(26, 0, 15, 16)));

            AddNewSprite(-2, new CroppedBitmap(_littleMarioSpritesheet, new Int32Rect(13, 18, 12, 15)));
            AddNewSprite(-3, new CroppedBitmap(_littleMarioSpritesheet, new Int32Rect(26, 17, 15, 16)));

            //Jumping animation
            AddNewSprite(4, new CroppedBitmap(_littleMarioSpritesheet, new Int32Rect(42, 0, 16, 16)));

            AddNewSprite(-4, new CroppedBitmap(_littleMarioSpritesheet, new Int32Rect(42, 16, 16, 16)));

            //Braking animation
            AddNewSprite(5, new CroppedBitmap(_littleMarioSpritesheet, new Int32Rect(59, 0, 14, 16)));

            AddNewSprite(-5, new CroppedBitmap(_littleMarioSpritesheet, new Int32Rect(59, 16, 14, 16)));

            //Flag animation
            AddNewSprite(6, new CroppedBitmap(_littleMarioSpritesheet, new Int32Rect(74, 0, 13, 15)));
            AddNewSprite(7, new CroppedBitmap(_littleMarioSpritesheet, new Int32Rect(88, 0, 13, 16)));

            //Death sprite
            AddNewSprite(8, new CroppedBitmap(_littleMarioSpritesheet, new Int32Rect(102, 0, 16, 16)));

            //Victory animation
            AddNewSprite(9, new CroppedBitmap(_littleMarioSpritesheet, new Int32Rect(119, 0, 14, 16)));
            AddNewSprite(10, new CroppedBitmap(_littleMarioSpritesheet, new Int32Rect(134, 0, 16, 16)));

            //???
            AddNewSprite(11, new CroppedBitmap(_littleMarioSpritesheet, new Int32Rect(151, 0, 16, 16)));
            AddNewSprite(12, new CroppedBitmap(_littleMarioSpritesheet, new Int32Rect(168, 0, 14, 16)));
            AddNewSprite(13, new CroppedBitmap(_littleMarioSpritesheet, new Int32Rect(183, 0, 14, 16)));

            //Blocks

            AddNewSprite(30, new CroppedBitmap(_blocksSpritesheet, new Int32Rect(0, 0, 32, 32)));
            AddNewSprite(31, new CroppedBitmap(_blocksSpritesheet, new Int32Rect(32, 0, 32, 32)));
            AddNewSprite(32, new CroppedBitmap(_blocksSpritesheet, new Int32Rect(64, 0, 32, 32)));
            AddNewSprite(33, new CroppedBitmap(_blocksSpritesheet, new Int32Rect(96, 0, 32, 32)));
            AddNewSprite(34, new CroppedBitmap(_blocksSpritesheet, new Int32Rect(128, 0, 32, 32)));

            // Mobs

            //Mushroom
            AddNewSprite(50, new CroppedBitmap(_mushroomSpritesheet, new Int32Rect(0, 0, 16, 16)));
            AddNewSprite(51, new CroppedBitmap(_mushroomSpritesheet, new Int32Rect(16, 0, 16, 16)));
            AddNewSprite(52, new CroppedBitmap(_mushroomSpritesheet, new Int32Rect(32, 0, 16, 16)));

        }
        public static CroppedBitmap GetSprite(int id)
        {
            CroppedBitmap? source = _sprites.FirstOrDefault(s => s.ID == id)?.ImageSource;

            if (source == null)
            {
                throw new ArgumentOutOfRangeException($"No sprite had the given ID {id}, dumbass");
            }

            return source;
            
        }
        public static CroppedBitmap GetSpriteByString(string id)
        {
            CroppedBitmap? source;

            switch (id)
            {
                case "LuckyBlock":
                    return _sprites.FirstOrDefault(s => s.ID == 30).ImageSource;

                case "LuckyBlockGlow":
                    return _sprites.FirstOrDefault(s => s.ID == 31).ImageSource;

                case "LuckyBlockGlowGlow":
                    return _sprites.FirstOrDefault(s => s.ID == 32).ImageSource;

                case "Blank":
                    return _sprites.FirstOrDefault(s => s.ID == 33).ImageSource;

                case "Brick":
                    return _sprites.FirstOrDefault(s => s.ID == 34).ImageSource;
                default:
                    throw new ArgumentOutOfRangeException($"Invalid ID, {id}");
            }
        }
        private static void AddNewSprite(int id, CroppedBitmap source)
        {
            _sprites.Add(new Sprite(id, source));
        }
    }
}
