namespace LoneX.UQTR.Sudoku
{
    public interface ICommand
    {
        void Execute();
        void Undo();
        bool IsExecuted();
    }   
}