using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace LoneX.UQTR.Sudoku
{
    public class LoadMenu : MonoBehaviour
    {
       #region PublicAttributes
        public static LoadMenuItem selectedItem;
        public IPersistance persistance;
        public GameObject itemGameObject;
        public GameObject viewPort;
       #endregion

       #region MonoCallbacks
        void Awake()
        {
            persistance = new BinaryPersistance();
            Init();
        }
       #endregion

       #region PublicMethods
        public void LoadGame()
        {
            if(selectedItem == null)
                GameManager.instance.ShowPopup("Woopsie" , "Maybe select an Item first");
            else
               {
                    GameManager.instance.OnLoad(selectedItem.Name);
                    GetComponent<PopupMenu>().Hide();
               }
        }
        public void Init()
        {
            var _fileNames = persistance.GetSavedGames();
            foreach(string _fileName in _fileNames)
            {
                var _item = Instantiate(itemGameObject , viewPort.transform);
                _item.GetComponent<LoadMenuItem>().SetItemName(_fileName);
            }
        }
       #endregion

       
       #region PrivateMethods
       #endregion
    }
   

}