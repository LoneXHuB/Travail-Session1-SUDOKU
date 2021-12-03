using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

namespace LoneX.UQTR.Sudoku
{
    public class TileSelector : MonoBehaviour
    {
       #region PublicAttributes
        public static List<Tile> SelectedTiles {get; private set;} = new List<Tile>();
        public static TileSelector instance;
        public static event EventHandler selectionChanged;
       #endregion

       #region PrivateAttributes
        private static bool multipleSelection = false;
       #endregion

       #region MonoCallbacks
        public void Awake()
        {
            if(instance == null)
                instance = this;
            else
                Destroy(this);
        }
        public void OnEnable()
        {
            Tile.TileSelected+=OnTileSelected;
        }
        public void OnDisable()
        {
            Tile.TileSelected-=OnTileSelected;
        }

        public void Update()
        {
            if(Input.GetKeyDown(KeyCode.LeftControl)) multipleSelection = true;
            if(Input.GetKeyUp(KeyCode.LeftControl)) multipleSelection = false;
        }
       #endregion

       #region PublicMethods
        public void OnTileSelected(object _sender , EventArgs _args)
        {
            var _selectedTile = (Tile) _sender;
            if(multipleSelection)
            {
                SelectedTiles.Add(_selectedTile);
                foreach(Tile t in SelectedTiles) t.Select();
            }
            else
            {
                foreach(Tile t in SelectedTiles) t.Deselect();
                SelectedTiles = new List<Tile>();
                SelectedTiles.Add(_selectedTile);
            }
            selectionChanged(this, EventArgs.Empty);
        }
       #endregion

       #region PrivateMethods
       #endregion
    }
   

}