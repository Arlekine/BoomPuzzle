
namespace Model.SlowMotionSystem
{
    public interface ISlowable
    {
        float CurrentNormalizedSpeed { get; }
        void SetNormalizedSpeed(float speed);
    }
}