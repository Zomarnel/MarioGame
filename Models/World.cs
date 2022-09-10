using Core;

namespace Models
{
    public class World
    {
        public List<Block> Blocks { get; set; }
        public List<Enemy> Enemies { get; set; }
        public double WorldXCoordinate { get; set; } = 0;
        private bool IsUpdatingLuckyBlocks { get; set; } = false;
        public void Update()
        {
            CreateNewGlowingTask();
        }
        public List<Block> ReturnDisposableBlocks()
        {
            List<Block> blocksToReturn = new List<Block>();

            List<Block> blocksToRemove = new List<Block>();

            foreach (Block b in Blocks)
            {
                if (b.XCoordinate < WorldXCoordinate && b.XCoordinate + b.Width < WorldXCoordinate)
                {
                    blocksToRemove.Add(b);
                }

                if (b.XCoordinate < WorldXCoordinate && b.XCoordinate + b.Width < WorldXCoordinate && b.HasBeenDrawn)
                {
                    blocksToReturn.Add(b);
                }
            }

            blocksToRemove.ForEach(b => Blocks.Remove(b));

            return blocksToReturn;
        }
        public List<Block> ReturnUpdatedBlocks()
        {
            List<Block> blocksToReturn = new List<Block>();

            foreach (Block b in Blocks)
            {

                if (b.XCoordinate > WorldXCoordinate && b.XCoordinate < WorldXCoordinate + GameInfo.SCREEN_WIDTH && b.NeedsToBeUpdated)
                {
                    blocksToReturn.Add(b);
                }
                else if (b.XCoordinate + b.Width > WorldXCoordinate && b.XCoordinate + b.Width < WorldXCoordinate + GameInfo.SCREEN_WIDTH && b.NeedsToBeUpdated)
                {
                    blocksToReturn.Add(b);
                }
            }

            return blocksToReturn;
        }
        private void CreateNewGlowingTask()
        {
            if (!IsUpdatingLuckyBlocks)
            {
                IsUpdatingLuckyBlocks = true;
                UpdateGlowingBlocks();
            }
        }
        private async void UpdateGlowingBlocks()
        {
            await Task.Delay(250);

            List<Block> blocks = new List<Block>();

            if (Blocks.Any(b => b.FileName == "LuckyBlock"))
            {
                blocks = Blocks.Where(b => b.FileName == "LuckyBlock").ToList();

                blocks.ForEach(b => b.FileName = "LuckyBlockGlow");

                blocks.ForEach(b => b.NeedsToBeUpdated = true);
            }
            else if (Blocks.Any(b => b.FileName == "LuckyBlockGlow"))
            {
                blocks = Blocks.Where(b => b.FileName == "LuckyBlockGlow").ToList();

                blocks.ForEach(b => b.FileName = "LuckyBlockGlowGlow");

                blocks.ForEach(b => b.NeedsToBeUpdated = true);
            }
            else
            {
                blocks = Blocks.Where(b => b.FileName == "LuckyBlockGlowGlow").ToList();

                blocks.ForEach(b => b.FileName = "LuckyBlock");

                blocks.ForEach(b => b.NeedsToBeUpdated = true);
            }

            IsUpdatingLuckyBlocks = false;
        }
    }
}
