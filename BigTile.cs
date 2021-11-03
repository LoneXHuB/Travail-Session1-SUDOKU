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
        public Tile CurrentTile { get; private set;}
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
       #endregion

       #region PublicMethods
        public void Focus() => mainValue.Select();

        public void SetCurrent(Tile _tile)
        {
            CurrentTile = _tile;
            mainValue.text = _tile.Value.ToString();
            
            note.SetContent(CurrentTile.Note.Content);
            for(int i = 0; i < helpers.Length ; i++)
                helpers[i].SetContent(_tile.Subtiles[i].Content);
        }

        public void OnInputChanged(string _newVal)
        {
            var _newValue = Int32.Parse(_newVal);
            if(CurrentTile == null) return;
            CurrentTile.SetValue(_newValue);
            mainValue.DeactivateInputField();
            mainValue.ActivateInputField();
            mainValue.Select();
            GameManager.instance.CheckGameState();
        }
        public void SetHelper(int _i , int _content) => helpers[_i].SetContent(_content);
        public void NoteMode()
        {
            if(CurrentTile == null) return;
            CurrentTile.ShowNote();
            note.Show();
            note.input.Select();
            GetComponent<Tile>().HideValueText();
        }
        public int GetHelperValue(int _i) => helpers[_i].Content;
        public void ValueMode()
        {
            note.Hide();
            if(CurrentTile == null) return;
            CurrentTile.HideNote();
            mainValue.Select();
            GetComponent<Tile>().ShowValueText();
        }
       #endregion

       
       #region PrivateMethods
       #endregion
    }
   

}