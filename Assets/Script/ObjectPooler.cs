using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour {

	public GameObject[] objects;
	public GameObject objectToInstantiate;
	public int poolAmount;

	// Use this for initialization
	void Start () {
		objects = new GameObject[poolAmount];
		for (int i = 0; i < poolAmount; i++) {
			objects [i] = Instantiate (objectToInstantiate) as GameObject;
			objects [i].transform.parent = gameObject.transform;
			objects [i].SetActive (false);
		}
	}
	
	public GameObject getPooledObject(){
		for (int i = 0; i < poolAmount; i++) {
			if (objects [i].activeInHierarchy == false) {
				objects [i].SetActive (true);
				return objects[i];
			}
		}
		return null;
			
	}
	
}
