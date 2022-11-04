using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Score : MonoBehaviour
{
    int scoreValue = 1;    
    public ScoreKeeper scoreKeeper;
    public Rigidbody2D rd;

    // Start is called before the first frame update
    void Start()
    {
        //scoreKeeper = GameObject.Find("Score").GetComponent<ScoreKeeper>();
        rd = GetComponent<Rigidbody2D>();

    }

    private void OnTriggerEnter2D(Collider2D collider)
    {       
        if (collider.tag == "ScoreCollider")
        {
            collider.GetComponent<BoxCollider2D>().enabled = false;
            scoreKeeper.Score(scoreValue);
        }

    }
}
