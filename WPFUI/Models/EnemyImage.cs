using Models;
using System.Windows.Controls;

namespace WPFUI.Models
{
    public class EnemyImage
    {
        public int EntityID { get; set; }

        public Image FileImage { get; set; }

        public EnemyImage(int entityID, Image fileImage)
        {
            EntityID = entityID;
            FileImage = fileImage;
        }
    }
}
