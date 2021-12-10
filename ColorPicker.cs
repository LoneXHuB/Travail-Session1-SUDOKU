using System;
using System.Collections.Generic;
using UnityEngine;

namespace LoneX.UQTR.Sudoku
{
    public class ColorPicker : MonoBehaviour
    {
       #region PublicAttributes
        public static ColorPicker instance;
       #endregion

       #region PrivateAttribute
        [SerializeField]
        private FlexibleColorPicker fcp;
        private bool isActivated;
       #endregion

       #region MonoCallbacks
        public void Awake()
        {
            if(instance == null)
                instance = this;
            else
                Destroy(this);
        }
        public void Update()
        {
            if(isActivated)
            foreach(Tile tile in TileSelector.SelectedTiles) tile.BaseColorUpdate(fcp.color);
        }
        public void OnEnable()
        {
            Tile.TileSelected+=OnTileSelected;
        }
        public void OnDisable()
        {
            Tile.TileSelected-=OnTileSelected;
        }
       #endregion

       #region PublicMethodss
        public void ShowColorPicker()
        {
            LeanTween.scale(fcp.gameObject , 2f * Vector3.one , .5f).setEaseInOutBack();
            isActivated = true;
        }
        public void HideColorPicker()
        {
            LeanTween.scale(fcp.gameObject , Vector3.zero , .5f).setEaseInOutBack();
            isActivated = false;
        }
       #endregion

       
       #region PrivateMethods
        private void OnTileSelected(object _source,EventArgs _args)
        {
            if(!isActivated) return;
            var _selectedTiles = TileSelector.SelectedTiles;
            var _from = new Color[_selectedTiles.Count];
            var _to = new Color[_selectedTiles.Count];
            for(int i = 0; i < _selectedTiles.Count ; i++) 
            {
                _from[i] = _selectedTiles[i].GetCurrentBaseColor();
                _to[i] = fcp.color;
            }
            //CODE pour pouvoir UNDO une commande j'ai 
            var _commande = new GenericCommand<Color>(_selectedTiles.ToArray(), UpdateColor, _from, _to);
            CommandInvoker.Instance.Execute(_commande);
        }

        private void UpdateColor(object[] _sources,Color[] _colors)
        {
            for(int i = 0; i < _sources.Length ; i++) ((Tile) _sources[i]).BaseColorUpdate(_colors[i]);
        }

       #endregion
    }
   

}