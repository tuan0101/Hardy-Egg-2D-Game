using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepeatingBG : MonoBehaviour {

	public Transform generatorPoint;
    public Transform BackPoint;

    float platformWidth = 15f;

	// Update is called once per frame
	void Update () {
		if (transform.position.x < generatorPoint.position.x) {	
			Repos ();
		}
        generateBackward();
    }

	void Repos() {
		Vector2 offset = new Vector2 (platformWidth*3f, 0);
		transform.position = (Vector2)transform.position + offset;
	}
    
    void generateBackward()
    {
       //if ((transform.position.x - BackPoint.position.x) >= 35)
       if(transform.position.x > BackPoint.position.x)
        {
            Vector2 offset = new Vector2(platformWidth * 3f, 0);
            transform.position = (Vector2)transform.position - offset;
        }
    } 

}
