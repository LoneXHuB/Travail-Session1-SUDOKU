using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using LoneX.UQTR.Sudoku;
using UnityEngine.UI;

namespace LoneX.UQTR.Sudoku
{
    public class BlocGrid : MonoBehaviour
    {
        
       #region Constants
        private const int ROWCOUNT = 3;
        private const int LINECOUNT = 3;
       #endregion

       #region PublicAttributes
       #endregion

       #region PrivateAttributes
        [SerializeField]
        private GameObject blocObject;
        private MPosition mPosition;
        public List<Bloc> Blocs {get; private set;}= new List<Bloc>();
       #endregion

       #region MonoCallbacks
        void Awake()
        {
            for(int j=0;j<LINECOUNT;j++)
            {
                for(int i=0;i<ROWCOUNT;i++)
                {
                    var _blocInstance = Instantiate(blocObject , Vector3.zero, Quaternion.identity);
                    _blocInstance.transform.SetParent(this.transform);
                    _blocInstance.transform.localScale = new Vector3(1f,1f,1f);

                    var _block = _blocInstance.GetComponent<Bloc>();
                    _block.SetMPosition(i,j);
                    Blocs.Add(_block);
                }
            }
        }
        #endregion

       #region PublicMethods
        public void MakeImmutable()
        {
            foreach(Bloc b in Blocs) b.MakeImmutable();
        }
        public void ClearGrid()
        {
            foreach(Bloc b in Blocs) b.ClearBloc();
        }
        public void ResetEvaluation()
        {
            foreach(Bloc b in Blocs) b.ResetEvaluation();
        }
        public bool AnalyseGrid()
        {
            ResetEvaluation();
            var _AnyIncorrect = Blocs.Where(c => ! c.AnalyseBloc()).Any();

            //line by line then row by row
            var _numIter = ROWCOUNT*ROWCOUNT;
            for(int i=0;i<=_numIter;i++)
            {
                var _tilesRow =  GetTileRow(i);
                _AnyIncorrect = Tile.CheckDoubles(_tilesRow);
            }

            _numIter = LINECOUNT*LINECOUNT;
            for(int i=0;i<=_numIter;i++)
            {
                var _tileLine =  GetTileLine(i);
                _AnyIncorrect = Tile.CheckDoubles(_tileLine);
            }

            return !_AnyIncorrect;
        }
       #endregion

       #region PrivateMethods
        private IEnumerable<Tile> GetTileLine(int i) => Blocs.Where(b => b.GetPosition().YLine == FindRowBloc(i)).SelectMany(b => b.GetLine(FindTileIndex(i)));
        private IEnumerable<Tile> GetTileRow(int i) => Blocs.Where(b => b.GetPosition().XRow == FindRowBloc(i)).SelectMany(b => b.GetRow(FindTileIndex(i)));
        private int FindRowBloc(int _row) => (int)Mathf.Floor(_row / 3f);
        private int FindTileIndex(int _tile) => _tile - (3 * FindRowBloc(_tile));
        // 0 1 2   3 4 5   6 7 8
        // 0 1 2   0 1 2   0 1 2
        //   0       1       2
       #endregion
    }
   

}