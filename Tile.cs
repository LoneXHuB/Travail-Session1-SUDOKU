using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using TMPro;
using System;

namespace LoneX.UQTR.Sudoku
{
    public class Tile : MonoBehaviour
    {
       #region Constants
        private const int ROWCOUNT = 2;
        private const int LINECOUNT = 2;

       #endregion

       #region PublicAttributes
        public static event EventHandler TileSelected;
        public Note Note{get; private set;}
        public bool NoteMode{ get; private set;}
        public int Value 
        {
            get => tileValue;
            private set
            { 
                if(! isBigTile)
                {
                    UpdateTile(value);
                }
            }
        }
        public bool IsCorrect
        { 
            get=> IsCorrect ; 
            set 
            {
                if(value == false) 
                    MarkFalse();
                else
                    UnmarkFalse();
            } 
        } 
        public bool IsImmutable{get; private set;}
        public List<SubTile> Subtiles {get; private set;} = new List<SubTile>();
       #endregion

       #region PrivateAttributes
        private MaterialAnimator animator;
        private int tileValue = 0;
        [SerializeField]
        private bool isBigTile = false;
        [SerializeField]
        private TMP_Text valueText;
        [SerializeField]
        private GameObject subTileObject;
        private MPosition mPosition = new MPosition(-1,-1);
       #endregion

       #region MonoCallbacks
        void Start()
        {
            animator = GetComponent<MaterialAnimator>();
            Note = GetComponentInChildren<Note>();
        }
        void Awake()
        {
            if(isBigTile) return;

            var _numIter = ROWCOUNT*LINECOUNT;
            for(int i=0;i< _numIter;i++)
            {
                var _subTileObjectInstance = Instantiate(subTileObject , Vector3.zero , Quaternion.identity);
                _subTileObjectInstance.transform.SetParent(this.transform);
                var _subtile = _subTileObjectInstance.GetComponent<SubTile>();
                Subtiles.Add(_subtile);
            }
        }
       #endregion

       #region PrivateMethods
        private void UpdateTile(int value)
        {
            valueText.text = value == 0 ? "":value.ToString();
            tileValue = value;
        }
       #endregion

       #region PublicMethods
        public void MakeImmutable()
        {
            if(Value == 0) return;
            IsImmutable = true;
            GetComponent<MaterialAnimator>().Dimmer();
        }
        public void SetSubtiles(int _s0, int _s1, int _s2, int _s3)
        {
            Subtiles.ElementAt(0).SetContent(_s0);
            Subtiles.ElementAt(1).SetContent(_s1);
            Subtiles.ElementAt(2).SetContent(_s2);
            Subtiles.ElementAt(3).SetContent(_s3);
        }
        public void MarkFalse()
        {
            animator.OnWrong();
        }
        public void UnmarkFalse()
        {
            animator.OnWrongEnd();
        }
        public void SetNote(int _value)
        {
            Note.SetContent(_value);
        }
        public void ShowNote()
        {
            Note.Show();
            Value = 0;
            HideValueText();
            NoteMode = true;
        }
        public void HideNote()
        {
            Note.Hide();
            ShowValueText();
            NoteMode = false;
        }
        public void ClearTile()
        {
            UnmarkFalse();
            if(IsImmutable) return;
            foreach(SubTile _subTile in Subtiles) _subTile.SetContent(0);
            Value = 0;
            HideNote();
        }
        public bool SetHelperValue(int _index , int _value)
        {
            foreach(SubTile _s in Subtiles)
            if(Subtiles[_index].Content != _s.Content)
            if(_s.Content == _value) 
            return false;

            Debug.Log("setting helpers for TIle : " + this.mPosition.XRow + "," + mPosition.YLine);
            Subtiles[_index].SetContent(_value);
            return true;
        }
        public void SetMPosition(int _x , int _y) => mPosition = new MPosition(_x,_y);
        public void SetValue(int _value)
        {
            if(!IsImmutable) Value = _value;
        }
        public int[] ExtractSubtiles() => Subtiles.Select(c => c.Content).ToArray();
        public void OnClick()
        {
            if(IsImmutable) return;
            animator.OnSelect();
            TileSelected(this,EventArgs.Empty);
        }
        public void Deselect() => animator.OnUnselected();
        public void Select() => animator.OnSelect();
        public void BaseColorUpdate(Color _color) => animator.BaseColorUpdate(_color);
        public Color GetCurrentBaseColor() => animator.GetCurrentBaseColor();
        public MPosition GetPosition() => new MPosition (mPosition.XRow , mPosition.YLine);
        public void HideValueText() => valueText.gameObject.SetActive(false);
        public void ShowValueText() => valueText.gameObject.SetActive(true);
       #endregion

       
       #region overrides
        public override bool Equals(object obj)
        {
            if(obj == null || !(obj is Tile))
                return false;
            return ((Tile) obj).Value == this.Value && ((Tile) obj).mPosition == this.mPosition;
        }
        
        public override int GetHashCode() => Value + mPosition.GetHashCode();
       #endregion

       #region Static methods
        public static bool CheckDoubles(IEnumerable<Tile> _tiles)
        {
            var _doubles = _tiles.Where(c => c.Value != 0).GroupBy(x => x.Value).Where(g => g.Count() > 1).SelectMany(x => x);
            foreach(Tile _t in _doubles) _t.IsCorrect = false;  
            return ! _doubles.Any();
            //AssertEquals(new StringToTiles(" 1 1 2 3 4 5 6 8 9 8 B1 B2 B3 B5")) 
        }
       #endregion
    }
   

}