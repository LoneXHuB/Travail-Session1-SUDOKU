using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LoneX.UQTR.Sudoku
{
    public class Launcher : MonoBehaviour
    {
       #region PublicMethods
        public void OnClick()
        {
            GameManager.instance.LoadDefault();
            LeanTween.scale(gameObject, Vector3.zero , 1f).setEaseInExpo().setOnComplete(()=> {Destroy(gameObject);});
        }
       #endregion
    }
   

}