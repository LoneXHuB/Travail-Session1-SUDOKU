using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

namespace LoneX.UQTR.Sudoku
{

    public class SubTile : MonoBehaviour
    {
        
       #region PublicAttributes
       public int Content
        { 
           get => content;
            private set
            { 
                var _inputField = GetComponentInParent<TMP_InputField>();
                if(_inputField != null)
                    _inputField.text = value == 0 ? "":value.ToString();
                else
                    valueText.text = value == 0 ? "":value.ToString();
                content = value;
            } 
        }
       #endregion

       #region PrivateAttributes
        [SerializeField]
        private int id;
        private int content = 0;
        [SerializeField]
        private TMP_Text valueText;
       #endregion

       #region MonoCallbacks
       void start()
       {
           this.gameObject.SetActive(false);
       }
       #endregion

       #region PublicMethods
        public void SetContent(int _content)
        {
            Content = _content;
        }
        public void OnInputChanged(string _newVal)
        {
            if(Int32.TryParse(_newVal , out int _newValue))
            {
                var _selectedTiles = TileSelector.SelectedTiles;
                var _from = new int[_selectedTiles.Count + 1];
                var _to = new int[_selectedTiles.Count + 1];
                _from[_selectedTiles.Count] = id;
                _to[_selectedTiles.Count] = id;
                for(int i = 0; i < _selectedTiles.Count ; i++)
                {
                    _from[i] = _selectedTiles[i].Subtiles[id].content;
                    _to[i] = _newValue;
                }
                var _commande = new GenericCommand<int>(_selectedTiles.ToArray(), SetHelperValue, _from , _to);
                CommandInvoker.Instance.Execute(_commande);
                GetComponent<TMP_InputField>().text = _newValue.ToString();
            }
        }
       #endregion

       #region PrivateMethods
        private void SetHelperValue(object[] _sources, int[] _values)
        {
            var _id = _values[_sources.Length];
            for(int i = 0; i < _sources.Length ; i++)
            {
                ((Tile) _sources[i]).SetHelperValue(_id , _values[i]);
                Debug.Log("Tile: "+(Tile)_sources[i]+" undoing Id : " + _id + "value : " + _values[i]);
            }
        }
       #endregion
    }

}