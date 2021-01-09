using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonManager : MonoBehaviour {
    public Toggle Slider;
    static public bool isSlider = true; // can be used in PlayerController.cs

    public void activeSlider()
    {
        if (Slider.isOn)
        {
            isSlider = true;
        }
        else
        {
            isSlider = false;
        }
        Debug.Log(isSlider, gameObject);
    }
}
