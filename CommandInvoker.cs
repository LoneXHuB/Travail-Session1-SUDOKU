using System;
using System.Collections.Generic;

namespace LoneX.UQTR.Sudoku
{
    sealed class CommandInvoker
    {
        #region Private Attributes
        private static CommandInvoker instance;

        private CommandInvoker() { }

        private Stack<ICommand> executedCommands = new Stack<ICommand>();

        private Stack<ICommand> undoneCommands = new Stack<ICommand>();      

        #endregion 

        #region Public Attributes
        public static CommandInvoker Instance
        { 
            get 
            {
                if (instance == null)
                    instance = new CommandInvoker();

                return instance;
            }
        }
        #endregion 

        #region Public Methods
        public void Execute(ICommand _cpmmand)
        {
            executedCommands.Push(_cpmmand);
            if(_cpmmand.IsExecuted()) FlushRedo();
            _cpmmand.Execute();
        }
        public void Redo()
        {
            if (undoneCommands.Count == 0)
                GameManager.instance.ShowPopup("woops!" , "you cant redo what has never been undone ... /n #wisedom");
            else
            {
                var _redoneCommand = undoneCommands.Pop();
                _redoneCommand.Execute();
                executedCommands.Push(_redoneCommand);
            }
        }
        public void Undo()
        {
            if (executedCommands.Count == 0)
                GameManager.instance.ShowPopup("yips!" , "you cant undo what has never been done ... /n #knowledge");
            else
            {
                var _undoneCommand = executedCommands.Pop();
                _undoneCommand.Undo();
                undoneCommands.Push(_undoneCommand);
            }
        }   
        #endregion

        #region private methods
        private void FlushRedo() => undoneCommands = new Stack<ICommand>();
        #endregion
    }
}