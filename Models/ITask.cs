
namespace Models
{
    public interface ITask
    {
        public WorldEntity WorldEntity { get; set; }
        public bool IsFulfilled { get; set; }
        void Execute();
        
    }
}
