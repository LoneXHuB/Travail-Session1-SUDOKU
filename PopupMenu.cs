using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace LoneX.UQTR.Sudoku
{
    public class PopupMenu : MonoBehaviour
    {
        
       #region PublicAttributes
        public TMP_Text title;
        public TMP_Text text;
       #endregion

       #region PrivateAttributes
       #endregion

       #region MonoCallbacks
        void Awake()
        {
           Show();
        }

        void Update()
        {
            if(Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.Escape))
                Hide();
        }
       #endregion

       #region PublicMethods
        public void SetPopup(string _title , string _text)
        {
            title.text = _title;
            text.text = _text;
        }

        public void Show()
        {
            gameObject.transform.localScale = Vector3.zero;
            LeanTween.scale(gameObject , Vector3.one , .5f).setEaseInOutBack();
        }

        public void Hide()
        {
            LeanTween.scale(gameObject , Vector3.zero , .5f).setEaseInOutBack().setOnComplete(()=>{Destroy(gameObject);});
        }
       #endregion

       
       #region PrivateMethods
       #endregion
    }
   

}