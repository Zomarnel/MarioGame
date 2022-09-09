using System.Windows.Media;
using System.Windows.Controls;
using System.Collections.Generic;
using Models;
using WPFUI.Models;
using System.Linq;
using Services;
using System;

namespace WPFUI.Services
{
    public class DrawingService
    {
        private Image _playerSprite { get; set; }
        private List<BlockImage> _blocksCache { get; set; } = new List<BlockImage>();
        public void DrawPlayer(Canvas canvas, int id, double xCoordinate, double yCoordinate)
        {
            if (_playerSprite == null)
            {
                InitializePlayerSprite(id, canvas);
            }
            else
            {
                _playerSprite.Source = SpritesFactory.GetSprite(id);
            }

            Canvas.SetLeft(_playerSprite, xCoordinate);
            Canvas.SetBottom(_playerSprite, yCoordinate);
        }
        public void DrawBlocks(Canvas canvas, List<Block> blocks)
        {
            if (blocks.Count == 0)
            {
                return;
            }

            foreach (Block block in blocks)
            {

                if (_blocksCache.Any(bi => bi.BlockID == block))
                {
                    BlockImage bi = _blocksCache.First(bi => bi.BlockID == block);

                    _blocksCache.Remove(bi);

                    canvas.Children.Remove(bi.FileImage);
                } 

                Image image = new Image()
                {
                    Width = block.Width,
                    Height = block.Height,
                    Source = SpritesFactory.GetSpriteByString(block.FileName)
                };

                block.NeedsToBeUpdated = false;

                block.HasBeenDrawn = true;

                _blocksCache.Add(new BlockImage(block, image));

                canvas.Children.Add(image);

                Canvas.SetLeft(image, block.XCoordinate - Math.Abs(MapService.MapXCoordinate));
                Canvas.SetBottom(image, block.YCoordinate);

                Canvas.SetZIndex(image, 99);
            }
        }
        public void DisposeBlocks(Canvas canvas, List<Block> blocks)
        {
            if (blocks.Count == 0)
            {
                return;
            }

            List<BlockImage> imagesToRemove = _blocksCache.Where(bi => blocks.Any(b => b == bi.BlockID)).ToList();

            imagesToRemove.ForEach(i => canvas.Children.Remove(i.FileImage));

            imagesToRemove.ForEach(i => _blocksCache.Remove(i));
        }
        public void UpdateCurrentBlocks(double mapXCoordinate)
        {
            _blocksCache.ForEach(bi => Canvas.SetLeft(bi.FileImage, bi.BlockID.XCoordinate - mapXCoordinate));
        }
        private void InitializePlayerSprite(int id, Canvas canvas)
        {
            _playerSprite = new Image()
            {
                Width = 32,
                Height = 32,
                Source = SpritesFactory.GetSprite(id)
            };

            RenderOptions.SetBitmapScalingMode(_playerSprite, BitmapScalingMode.NearestNeighbor);

            canvas.Children.Add(_playerSprite);

            Canvas.SetZIndex(_playerSprite, 99);
        }
    }
}
