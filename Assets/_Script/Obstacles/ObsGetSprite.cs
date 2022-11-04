using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObsGetSprite : MonoBehaviour
{
    [SerializeField] private GameObject[] sprites;

    // Use this for initialization
    void Start()
    {
        int i = Random.Range(0, 4);
        this.GetComponent<SpriteRenderer>().sprite = sprites[i].GetComponent<SpriteRenderer>().sprite;
        this.transform.localScale = sprites[i].transform.localScale;
        //Get colider automatically from prefab objects.
        this.GetComponent<PolygonCollider2D>().points = sprites[i].GetComponent<PolygonCollider2D>().points;
        //this.GetComponent<PolygonCollider2D>().autoTiling = true;
    }
}
