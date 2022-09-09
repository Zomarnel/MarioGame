using Models;
using System.Windows.Controls;

namespace WPFUI.Models
{
    public class BlockImage
    {
        public Block BlockID { get; set; }

        public Image FileImage { get; set; }
        public BlockImage(Block blockID, Image fileImage)
        {
            BlockID = blockID;
            FileImage = fileImage;
        }
    }
}
