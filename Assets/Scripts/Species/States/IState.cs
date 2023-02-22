namespace Species.States
{
    public interface IState
    {
        void Tick();
        void OnEnter();
        void OnExit();
    }
}