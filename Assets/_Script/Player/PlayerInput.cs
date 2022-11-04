using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public GameObject StickControl { get; set; }
    public GameObject FixedBtton { get; set; }
    [SerializeField] private GameObject jumpButton;

    public void DisableJump()
    {
        jumpButton.SetActive(false);
    }
}
