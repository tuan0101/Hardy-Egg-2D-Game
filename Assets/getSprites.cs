using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class getSprites : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        //SpriteRenderer renderer = transform.Find("Upper Body 1").gameObject.GetComponent<SpriteRenderer>();
        SpriteRenderer[] sprites = GetComponentsInChildren<SpriteRenderer>();

        //currentGhost1.transform.localScale = this.transform.localScale;
        //currentGhost2.GetComponent<SpriteRenderer>().sprite = sprites;
        this.GetComponent<SpriteRenderer>().sprite = sprites[2].sprite;
    }
}
