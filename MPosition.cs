using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LoneX.UQTR.Sudoku
{
    public class MPosition 
    {
       #region PublicAttributes
        public int XRow { get; private set; }
        public int YLine { get; private set; }
       #endregion

       #region Constructors
        public MPosition(int _xRow , int _yLine)
        {
            XRow = _xRow;
            YLine = _yLine;
        }
       #endregion

       #region Overrides
        public override bool Equals(object obj)
        {
            if(obj == null || !(obj is Tile))
                return false;

            return ((MPosition) obj).XRow == this.XRow && ((MPosition) obj).YLine == this.YLine;
        }

        public override int GetHashCode()
        {
                return XRow + YLine;
        }
       #endregion
    }
   

}