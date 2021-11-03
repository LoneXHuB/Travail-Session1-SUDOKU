using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace LoneX.UQTR.Sudoku
{
    public class Bloc : MonoBehaviour
    {
      #region Constants
        private const int ROWCOUNT = 3;
        private const int LINECOUNT = 3;
       #endregion

       #region PublicAttributes
        public bool IsCorrect { get; set;} = false;
       #endregion

       #region PrivateAttributes
        [SerializeField]
        private GameObject tileObject;
        public MPosition mPosition {get; private set;} = new MPosition(-1,-1);
        public List<Tile> Tiles {get; private set;} = new List<Tile>();
       #endregion

       #region MonoCallbacks    
        void Awake()
        {
            for(int j=0;j<LINECOUNT;j++)
            {
                for(int i=0;i<ROWCOUNT;i++)
                {
                    var _tileInstance = Instantiate(tileObject , Vector3.zero , Quaternion.identity);
                    _tileInstance.transform.SetParent(this.transform);
                    var _tile = _tileInstance.GetComponent<Tile>();
                    _tile.SetMPosition(i, j);
                    Tiles.Add(_tile);
                }
            }
        }
       #endregion

       #region PublicMethods
        public void MakeImmutable()
        {
            foreach(Tile _tile in Tiles) _tile.MakeImmutable();
        }
        public void ClearBloc()
        { 
            foreach(Tile _tile in Tiles) _tile.ClearTile();
        }
        public void ResetEvaluation()
        {
            foreach(Tile _tile in Tiles)  _tile.IsCorrect = true;
        }
        public bool AnalyseBloc() => Tile.CheckDoubles(Tiles);

        public void SetMPosition(int _x , int _y) => mPosition = new MPosition(_x,_y);

        public MPosition GetPosition() => new MPosition (mPosition.XRow , mPosition.YLine);

        public IEnumerable<Tile> GetRow(int _row) => Tiles.Where(c => c.GetPosition().XRow == _row);
        
        public IEnumerable<Tile> GetLine(int _line) => Tiles.Where(c => c.GetPosition().YLine == _line);
       #endregion

       #region PrivateMethods
       #endregion
    }
   

}