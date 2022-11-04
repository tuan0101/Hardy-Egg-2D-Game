using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost : MonoBehaviour {

    [SerializeField] private float ghostDelay;
    [SerializeField] private GameObject ghost;
    [SerializeField] private bool makeGhost = false;
    [SerializeField] private EggMovement player;

    private float ghostDelaySeconds;

    // Use this for initialization
    void Start () {
        ghostDelaySeconds = ghostDelay;
        //player = GameObject.Find("PlayerController").GetComponent<EggMovement>();
    }

    // Update is called once per frame
    void Update () {
        if(player.Ghost)
        {
            if (ghostDelaySeconds > 0)
            {
                ghostDelaySeconds -= Time.deltaTime;
            }
            else
            {
                GameObject currentGhost = Instantiate(ghost, transform.position, transform.rotation);
                SpriteRenderer currentSprite = GetComponent<SpriteRenderer>();

                //SpriteRenderer[] sprites = GetComponentsInChildren<SpriteRenderer>();
                
                currentGhost.transform.localScale = this.transform.localScale;
                Vector3 flip = gameObject.transform.localScale;
                if (player.FlipIsTrue)
                {
                    flip.x *= -1;
                    currentGhost.transform.localScale = flip;
                }
                    
                //currentGhost1.GetComponent<SpriteRenderer>().sprite = sprites[1].sprite;
                currentGhost.GetComponent<SpriteRenderer>().sprite = currentSprite.sprite;
                currentGhost.GetComponent<SpriteRenderer>().sortingOrder = currentSprite.sortingOrder;
                ghostDelaySeconds = ghostDelay;
                //Destroy(currentGhost1, 0.2f);
                Destroy(currentGhost, 0.2f);
            }
        }

	}
}
