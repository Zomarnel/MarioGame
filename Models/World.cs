
using Core;

namespace Models
{
    public class World
    {
        public List<Block> Blocks { private get; set; }
        public List<Enemy> Enemies { private get; set; }
        public double WorldXCoordinate { get; set; } = 0;
        public void Update()
        {
            Blocks.ForEach(b => b.OnUpdate?.Invoke(b, new EventArgs()));
        }

        public List<Block> ReturnBlocks()
        {
            return Blocks;
        }

        public List<Block> ReturnBlocksInChunk()
        {
            List<Block> blocksToReturn = new List<Block>(); 

            foreach (Block b in Blocks)
            {

                if (b.XCoordinate > WorldXCoordinate && b.XCoordinate < WorldXCoordinate + GameInfo.SCREEN_WIDTH && b.NeedsToBeUpdated)
                {
                    blocksToReturn.Add(b);
                }
                else if (b.XCoordinate + b.Width > WorldXCoordinate && b.XCoordinate + b.Width < WorldXCoordinate + GameInfo.SCREEN_WIDTH
                         && b.NeedsToBeUpdated)
                {
                    blocksToReturn.Add(b);
                }
            }

            return blocksToReturn;
        }
        public List<Block> ReturnDisposableBlocks()
        {
            List<Block> blocksToReturn = new List<Block>();

            foreach (Block b in Blocks)
            {
                if (b.XCoordinate < WorldXCoordinate && b.XCoordinate + b.Width < WorldXCoordinate && b.HasBeenDrawn)
                {
                    blocksToReturn.Add(b);
                }
                else if (b.XCoordinate > WorldXCoordinate + GameInfo.SCREEN_WIDTH && b.XCoordinate + b.Width > WorldXCoordinate + GameInfo.SCREEN_WIDTH && b.HasBeenDrawn)
                {
                    blocksToReturn.Add(b);
                }
            }

            blocksToReturn.ForEach(b => Blocks.Remove(b));

            return blocksToReturn;
        }
    }
}
