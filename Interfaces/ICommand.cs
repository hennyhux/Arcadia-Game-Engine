namespace GameSpace
{
    public interface ICommand
    {
        void Execute();
        void Unexecute();
    }
}
