using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

namespace LoneX.UQTR.Sudoku
{

    public class BigTile : MonoBehaviour
    {

       #region PublicAttributes
        public static BigTile instance;
       #endregion

       #region PrivateAttributes
        [SerializeField]
        private TMP_InputField mainValue;
        [SerializeField]
        private SubTile[] helpers;
        [SerializeField]
        private Note note;
       #endregion

       #region MonoCallbacks
        public void Awake()
        {
            if(instance == null)
                instance = this;
            else
                Destroy(this.gameObject);
        }

        public void OnEnable()
        {
            TileSelector.selectionChanged+=OnTileSelected;
        }
        public void OnDisable()
        {
            TileSelector.selectionChanged-=OnTileSelected;
        }
       #endregion

       #region PublicMethods
        public void Focus() => mainValue.Select();

        public void OnTileSelected(object _source, EventArgs _args)
        {
            if(TileSelector.SelectedTiles.Count != 1) 
            {
                Focus();
                return;
            }

            var _tile = TileSelector.SelectedTiles[0];
            var _selectedCount = TileSelector.SelectedTiles.Count;

            var _newText = _selectedCount == 1 ? _tile.Value.ToString() : "0";
            mainValue.SetTextWithoutNotify(_newText);
            var _noteContent = _selectedCount == 1 ? _tile.Note.Content : 0;
            note.SetContent(_noteContent);
            for(int i = 0; i < helpers.Length ; i++)
            { 
                var _subTileContent = _selectedCount == 1 ? _tile.Subtiles[i].Content : 0;
                helpers[i].SetContent(_subTileContent);
            }
            if(_selectedCount != 1) ValueMode();
            Focus();
        }
        public void OnInputChanged(string _newVal)
        {
            var _newValue = Int32.Parse(_newVal);
            var _selectedTiles = TileSelector.SelectedTiles;
            if(_selectedTiles.Count == 0) return;
            var _from = new int[_selectedTiles.Count];
            var _to = new int[_selectedTiles.Count];

            for(int i = 0; i < _selectedTiles.Count ; i++) 
            {
                _from[i] = _selectedTiles[i].Value;
                _to[i] = _newValue;
            }

            var _commande = new GenericCommand<int>(_selectedTiles.ToArray(),UpdateTargetTiles, _from, _to);

            CommandInvoker.Instance.Execute(_commande);
            mainValue.DeactivateInputField();
            mainValue.ActivateInputField();
            mainValue.Select();
            GameManager.instance.CheckGameState();
        }
        public void UpdateTargetTiles(object[] _sources, int[] values)
        {
            for(int i = 0; i < _sources.Length ; i++) ((Tile) _sources[i]).SetValue(values[i]);
        }
        public void SetHelper(int _i , int _content) => helpers[_i].SetContent(_content);
        public void NoteMode()
        {
            if(TileSelector.SelectedTiles.Count == 0) return;
            foreach(Tile t in TileSelector.SelectedTiles) t.ShowNote();
            note.Show();
            note.input.Select();
            GetComponent<Tile>().HideValueText();
        }
        public int GetHelperValue(int _i) => helpers[_i].Content;
        public void ValueMode()
        {
            note.Hide();
            if(TileSelector.SelectedTiles.Count == 0) return;
            foreach(Tile t in TileSelector.SelectedTiles) t.HideNote();
            mainValue.Select();
            GetComponent<Tile>().ShowValueText();
        }
       #endregion

       
       #region PrivateMethods
       #endregion
    }
   

}