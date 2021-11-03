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
                var _helperChanged = BigTile.instance.CurrentTile.SetHelperValue( id , _newValue);
                if(!_helperChanged)
                    GetComponent<TMP_InputField>().text = BigTile.instance.GetHelperValue(id).ToString();
            }
        }
       #endregion

       
       #region PrivateMethods
       #endregion
    }

}