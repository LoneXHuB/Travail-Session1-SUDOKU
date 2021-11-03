using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LoneX.UQTR.Sudoku
{
    public interface IPersistance 
    {
       #region PublicMethods
        void SaveGrid(BlocGrid _grid , string _fileName);
        GameData LoadGrid(string _fileName);
        IEnumerable<string> GetSavedGames();
       #endregion
    }
   

}