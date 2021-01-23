using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generator : MonoBehaviour {

	public Transform GeneratorPoint;
    public Transform BackPoint;
    public ObjectPooler theObjectPool;
	float platformWidth = 15f;
	// Use this for initialization
	void Start () {
		GameObject newBG2 = theObjectPool.getPooledObject ();
		newBG2.transform.position = new Vector2 (-platformWidth, 0);

		GameObject newBG3 = theObjectPool.getPooledObject ();
		newBG3.transform.position = new Vector2 (0f, 0);
	}

	// Update is called once per frame
	void Update () {
        generateForward();
        generateBackward();
    }

    void generateForward()
    {
        if (transform.position.x < GeneratorPoint.position.x)
        {
            print(GeneratorPoint.position.x);
            transform.position = new Vector3(transform.position.x + platformWidth, transform.position.y,
                transform.position.z);
            GameObject newBG = theObjectPool.getPooledObject();
            newBG.transform.position = transform.position;
        }
    }

    void generateBackward()
    {
        if ((transform.position.x - BackPoint.position.x) >= 35) {
            
            GameObject newBG = theObjectPool.getPooledObject();
            newBG.transform.position = new Vector3(transform.position.x - platformWidth * 3f, transform.position.y,
                transform.position.z);
            transform.position = new Vector3(transform.position.x - platformWidth, transform.position.y,
               transform.position.z);
        }
    }
}

