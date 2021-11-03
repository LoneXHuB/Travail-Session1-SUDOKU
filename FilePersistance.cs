using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
namespace LoneX.UQTR.Sudoku
{
    public abstract class FilePersistance : IPersistance
    {
        
       #region PublicAttributes
        public const string FILE_EXTENTION = ".sudoku";
       #endregion

       #region PublicMethods
        public virtual void SaveGrid(BlocGrid _grid , string _fileName)
        {
            throw new NotImplementedException("implement methode in FilePersistance.cs");
        }

        public virtual GameData LoadGrid(string _fileName)
        {
            throw new NotImplementedException("implement methode in FilePersistance.cs");
        }

        public virtual IEnumerable<string> GetSavedGames()
        {
            throw new NotImplementedException("implement methode in FilePersistance.cs");
        }
       #endregion
    }
   

}