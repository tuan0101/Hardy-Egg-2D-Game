using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyGroup : MonoBehaviour {
	public GameObject destroyPoint;

    // Use this for initialization
    void Start () {
		destroyPoint = GameObject.Find ("DestroyPoint");

    }
	
	// Update is called once per frame
	void Update () {
        if (transform.position.x < destroyPoint.transform.position.x)
        {
            gameObject.SetActive(false);
            if  (GetComponent<BoxCollider2D>() != null)
                this.GetComponent<BoxCollider2D>().enabled = true;
            BoxCollider2D[] box = GetComponentsInChildren<BoxCollider2D>();
            for (int i = 0; i < box.Length; i++)
                {
                    box[i].enabled = true;

                }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (GetComponent<BoxCollider2D>() != null)
            this.GetComponent<BoxCollider2D>().enabled = false;
        BoxCollider2D[] box = GetComponentsInChildren<BoxCollider2D>();
        for (int i = 0; i < box.Length; i++)
        {
            box[i].enabled = true;
        }
    }
}
