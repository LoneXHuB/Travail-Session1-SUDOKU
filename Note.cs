using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using System.Linq;
using UnityEngine.UI;
namespace LoneX.UQTR.Sudoku
{
    public class Note : MonoBehaviour
    {
        
       #region PublicAttributes
        public TMP_InputField input;
        public TMP_Text text;
        public int Content {get; private set;}
        public bool isBigTile;
       #endregion

       #region PrivateAttributes
       #endregion

       #region MonoCallbacks
       #endregion

       #region PublicMethods
        public void SetContent(int _value)
        {
            Content = _value;
            text.text = _value.ToString();
        }
        public void Show() 
        {
            if(isBigTile) input.gameObject.SetActive(true);
            else text.gameObject.SetActive(true);
            var _img = GetComponent<Image>();
            LeanTween.value(gameObject , 0f , 1f , .5f)
            .setOnUpdate((value)=>{
                _img.fillAmount = value;
            }).setEaseInOutExpo().setOnComplete(()=>{
            GetComponent<MaterialAnimator>().BrightBlink();});
        }
        public void Hide()
        {
            if(isBigTile) input.gameObject.SetActive(false);
            else text.gameObject.SetActive(false);
            GetComponent<Image>().fillAmount = 0;
        }
        public void OnValueChanged(string _newVal)
        {
            var _noDoubles = _newVal.ToList().Distinct().Count() == _newVal.Length;
            var _selectedTiles = TileSelector.SelectedTiles;
            if(!_noDoubles) 
            {
                if(_selectedTiles.Count == 1)
                input.SetTextWithoutNotify(_selectedTiles[0].Note.Content.ToString());
                return;
            }

            if(Int32.TryParse(_newVal , out int _newValue))
            {
                var _from = new int[_selectedTiles.Count];
                var _to = new int[_selectedTiles.Count];

                for(int i = 0; i < _selectedTiles.Count ; i++) 
                {
                    _from[i] = _selectedTiles[i].Note.Content;
                    _to[i] = _newValue;
                }
                var _commande = new GenericCommand<int>(_selectedTiles.ToArray(), SetNoteValue, _from , _to);
                CommandInvoker.Instance.Execute(_commande);
            }
        }
       #endregion

       
       #region PrivateMethods
        private void SetNoteValue(object[] _sources, int[] values)
        {
            for(int i = 0; i < _sources.Length ; i++) ((Tile)_sources[i]).SetNote(values[i]);
        }
       #endregion
    }
   

}