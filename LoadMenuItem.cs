using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace LoneX.UQTR.Sudoku
{
    public class LoadMenuItem : MonoBehaviour
    {
        
       #region PublicAttributes
        public string Name {get; private set;}
        public Text textField;
       #endregion

       #region PrivateAttributes
       #endregion

       #region MonoCallbacks
       #endregion

       #region PublicMethods
        public void SetItemName(string _name)
        {
            Name = _name;
            textField.text = _name;
        }

        public void OnClick()
        {
            LoadMenu.selectedItem = this;
        }
       #endregion

       
       #region PrivateMethods
       #endregion
    }
   

}