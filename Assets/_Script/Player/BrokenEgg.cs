using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrokenEgg : MonoBehaviour {

    [SerializeField] private float speed = 45f;
    private Rigidbody2D[] myChildren;
    void Start()
    {
        myChildren = GetComponentsInChildren<Rigidbody2D>();
        for (int i = 0; i < myChildren.Length; i++)
        {
            myChildren[i].AddForce(new Vector2(speed, 0));
        }
    }

    // Update is called once per frame
    void Update () {
		
	}
}
