using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

	PlayerController player;

    Vector3 lastPlayerPos;

    float distanceToMove;
    //float moveDistance;

	void Start () {
		player = FindObjectOfType<PlayerController> ();
        lastPlayerPos = player.transform.position;
        //lastCamPos = transform.position;
    }

    // Update is called once per frame
    void Update() {      
        distanceToMove = player.transform.position.x - lastPlayerPos.x;
         transform.position = new Vector3(transform.position.x + distanceToMove,
                 transform.position.y, transform.position.z);

        lastPlayerPos = player.transform.position;

    }
}
