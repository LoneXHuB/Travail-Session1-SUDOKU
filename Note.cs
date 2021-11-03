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
            if(!_noDoubles) 
            {
                var _trimmed = BigTile.instance.CurrentTile.Note.Content.ToString();
                input.SetTextWithoutNotify(_trimmed);
                return;
            }

            if(Int32.TryParse(_newVal , out int _newValue))
            {
                BigTile.instance.CurrentTile.SetNote(_newValue);  
            }
        }
       #endregion

       
       #region PrivateMethods
       #endregion
    }
   

}