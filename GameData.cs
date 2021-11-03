using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using System.Linq;

namespace LoneX.UQTR.Sudoku
{
    [System.Serializable]
    public class GameData
    {
       #region PublicAttributes
        public int[] values;
        public int[] subtiles;
        public bool[] noteMode;
        public int[] notes;
       #endregion

       #region Constructors
        public GameData(BlocGrid _grid)
        {
            values = new int[9*9];
            subtiles = new int[0];
            noteMode = new bool[9*9];
            notes = new int[9*9];
            var _subt = new List<int>();
            var _i = 0;
            
            foreach(Bloc b in _grid.Blocs)
            foreach(Tile t in b.Tiles) 
            {
                values[_i] = t.Value;
                subtiles = subtiles.Concat(t.ExtractSubtiles()).ToArray();
                noteMode[_i] = t.NoteMode;
                notes[_i] = t.Note.Content;
                _i++;
            }
        }
       #endregion
    }
   

}