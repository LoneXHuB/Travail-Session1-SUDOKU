using System;

namespace LoneX.UQTR.Sudoku
{
    public class GenericCommand<T> : ICommand
    {
        #region PublicAttributes
        private bool isExecuted;
        #endregion

        #region PrivateAttributes
        private Action<object[],T[]> execute;
        private T[] newParams;
        private T[] oldParams;
        private object[] sources;
        #endregion

        #region Constructors
        public GenericCommand(object[] _sources, Action<object[],T[]> _execute, T[] _from ,T[] _to)
        {
            execute = _execute;
            newParams = _to;
            oldParams = _from;
            sources = _sources;
        }
        #endregion

        #region PublicMethods
        public void Execute()
        {
            if(execute != null)
            {
                execute.Invoke(sources, newParams);
                isExecuted = true;
            }
        }
        public void Undo()
        {
            if(execute != null)
                execute.Invoke(sources, oldParams);
        }
        public bool IsExecuted() => isExecuted;
        #endregion

        #region PrivateMethods
        #endregion
       
    }


}