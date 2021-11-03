using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace LoneX.UQTR.Sudoku
{
    public class SaveMenu : MonoBehaviour
    {
        
       #region PublicAttributes
        public TMP_InputField fileNameInput;
       #endregion

       #region PrivateAttributes
       #endregion

       #region MonoCallbacks
       #endregion

       #region PublicMethods
        public void OnSave()
        {
            var _fileName = fileNameInput.text;
            if(_fileName == null || _fileName == "")
                GameManager.instance.ShowPopup("Woopsie !" , "It might be a good idea to type a file name ...");
            else
            {
                GameManager.instance.OnSave(_fileName);
                GetComponent<PopupMenu>().Hide();
            }
        }
       #endregion

       
       #region PrivateMethods
       #endregion
    }
   

}