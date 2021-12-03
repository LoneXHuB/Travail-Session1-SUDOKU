using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace LoneX.UQTR.Sudoku
{
    public class MaterialAnimator : MonoBehaviour
    {
       #region PublicAttributes
       #endregion

       #region PrivateAttributes
        [SerializeField]
        private Vector3 endScale;
        private Material material;
        private bool isDisabled;

        [SerializeField , ColorUsage(true, true)]
        private Color beginColor , hoverColor ,disabledColor , selectedColor , wrongColor , rightColor , correctColor;

        [SerializeField]
        private AnimationCurve blinkCurve;
       #endregion

       #region MonoCallbacks
        void Start()
        {
            var _image = GetComponent<Image>();
            material = Instantiate(_image.material);
            material.CopyPropertiesFromMaterial(_image.material);
            _image.material = material;
            beginColor = material.color;
        }
        void Awake()
        {
            OnInitialized();
        }
       #endregion

       #region PublicMethods
        public void OnInitialized()
        {
            LeanTween.value(gameObject , 0.9f, 1.1f, .2f)
            .setOnUpdate((value)=>{ 
                material.SetFloat("_Intensity" , value);
            }).setLoopPingPong();
        }
        public void BaseColorUpdate(Color color)
        {
            correctColor = color;
            beginColor = color;
            Debug.Log("BaseColorUpdate Called");
            ChangeScaleAndColor(correctColor , endScale);
        }
        public Color GetCurrentBaseColor() => correctColor;
        public void OnWrong()
        {
            ChangeScaleAndColor(wrongColor , endScale);
        }
        public void OnWrongEnd()
        {
            ChangeScaleAndColor(correctColor , Vector3.one);
        }
        public void Dimmer()
        {
            material.color = disabledColor;
            isDisabled = true;
        }
        public void OnSelect()
        {
            ChangeScaleAndColor(selectedColor , endScale);
        }
        public void OnUnselected()
        {
            var _current = GetComponent<Image>().materialForRendering.color;
            GameManager.instance.CheckGameState();
            if(!_current.Equals(wrongColor))
                ChangeScaleAndColor(beginColor , Vector3.one);
        }
        public void OnHover()
        {
            var _current = GetComponent<Image>().materialForRendering.color;
            if(_current != selectedColor)
                ChangeScaleAndColor(hoverColor , endScale);
        }

        public void EndHover()
        {
            var _current = GetComponent<Image>().materialForRendering.color;
            if(_current != selectedColor)
                ChangeScaleAndColor(beginColor , Vector3.one);
        }

        public void ChangeScaleAndColor( Color colorB, Vector3 scaleB)
        {
            if(isDisabled) return;
            var _current = GetComponent<Image>().materialForRendering.color;
            if(_current.Equals(wrongColor) && colorB != correctColor) return;
            
            LeanTween.scale(gameObject, scaleB , .75f);
            LeanTween.value(gameObject, .1f, 1f, .25f)
            .setOnUpdate( (value)=>{ 
                material.color = Color.Lerp(_current , colorB , value);
            } ).setEaseInSine();
        }
        public void BrightBlink()
        {
            LeanTween.value(gameObject , 1f , 5f , .15f).setOnUpdate((value)=>{ material.SetFloat("_Intensity",value);});
        }
       #endregion

       
       #region PrivateMethods
       #endregion
    }
   

}