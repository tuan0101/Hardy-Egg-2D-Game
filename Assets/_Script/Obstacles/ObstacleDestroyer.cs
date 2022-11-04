using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleDestroyer : MonoBehaviour {
	private GameObject DestroyPoint;
	// Use this for initialization
	void Start () {
		DestroyPoint = GameObject.Find ("DestroyPoint");
	}
	
	// Update is called once per frame
	void Update () {
		if (transform.position.x < DestroyPoint.transform.position.x)
			gameObject.SetActive (false);
	}
}
