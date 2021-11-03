using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

namespace LoneX.UQTR.Sudoku
{
    public class GameManager : MonoBehaviour
    {
       #region PublicAttributes
        public static GameManager instance;
        public IPersistance persistance;     
        public Canvas canvas;
        public GameObject popup;
        public GameObject saveMenu;
        public GameObject loadMenu;
       #endregion

       #region PrivateAttributes
        [SerializeField]
        private BlocGrid grid; 
       #endregion

       #region MonoCallbacks
       //new GameManager(new BinaryPersistance()); 
       // var gm = new GameManager( new FilePersistance());
        // gm.LoadGame(fileName);
        public GameManager(IPersistance _persistance)
        {
            persistance = _persistance;
        }
        public void Awake()
        {
            persistance = new BinaryPersistance();
            if(instance == null)
            {
                instance = this;
            }
            else
                Destroy(this.gameObject);
        }
       #endregion

       #region PublicMethods
        public void ShowPopup(string _title , string _text)
        {
            var _pop = Instantiate(popup , canvas.transform);
            _pop.GetComponent<PopupMenu>().SetPopup(_title , _text);
        }
        public void ShowSaveMenu()
        {
            var _sav = Instantiate(saveMenu , canvas.transform);
            _sav.GetComponent<PopupMenu>().SetPopup("SAVE GAME" , "Please enter file name ...");
        }
        public void ShowLoadMenu()
        {
            var _load = Instantiate(loadMenu , canvas.transform);
            _load.GetComponent<PopupMenu>().SetPopup("LOAD GAME" , "You may select a game to load");
        }
        public void LoadDefault()
        {
            instance.OnLoad("default");
            grid.MakeImmutable();
        }
        public void CheckGameState()
        {
            var _isGameOver = grid.AnalyseGrid();

            if(_isGameOver)
                PlayerWon();
        }

        public void PlayerWon()
        {
            throw new Exception("Implement player won you idiot");
        }
        public void ExitGame()
        {
            Application.Quit();
        }

        public void OnSave(string _fileName)
        {
            persistance.SaveGrid(grid , _fileName);
            GameManager.instance.ShowPopup("Game Saved" , "Your game has been saved succesfully !");
        }

        public void OnLoad(string _fileName)
        {
            var _data = persistance.LoadGrid(_fileName);
            var i = 0;
            foreach(Bloc b in grid.Blocs)
            foreach(Tile t in b.Tiles)
            {
                var _s0 = _data.subtiles[i + i*3];
                var _s1 = _data.subtiles[i + 1 + i*3];
                var _s2 = _data.subtiles[i + 2 + i*3];
                var _s3 = _data.subtiles[i + 3 + i*3];
                
                var _v = _data.values[i];
                t.SetSubtiles(_s0, _s1, _s2, _s3);
                if(_data.noteMode[i])
                {
                    t.SetNote(_data.notes[i]);
                    t.ShowNote();
                }
                else
                    t.SetValue(_v);
                i++;    
            }
        }
        //0     1     2     3    4    5    6 7 8 9
        //0123  4567 891011 
       #endregion

       
       #region PrivateMethods
       #endregion
    }
   

}