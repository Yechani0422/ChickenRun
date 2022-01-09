using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class FarmerAnimCtrl : MonoBehaviour
{
    public Animator animators;

    public void SwapVisibility(GameObject obj)
    {
        obj.SetActive(!obj.activeSelf);
    }


    public void SetInt(string parameter = "key,value")
    {
        char[] separator = { ',', ';' };
        string[] param = parameter.Split(separator);

        string name = param[0];
        int value = Convert.ToInt32(param[1]);

       // Debug.Log(name + " " + value);

       
        animators.SetInteger(name, value);
    }

    
}
