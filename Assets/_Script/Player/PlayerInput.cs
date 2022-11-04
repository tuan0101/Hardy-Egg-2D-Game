using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public GameObject stickControl;
    public GameObject fixedBtton;
    public GameObject jumpButton;


    public void DisableJump()
    {
        jumpButton.SetActive(false);
    }
}
