using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Score : MonoBehaviour
{
    int scoreValue = 1;
    [SerializeField] private ScoreKeeper scoreKeeper;

    private void OnTriggerEnter2D(Collider2D collider)
    {       
        if (collider.tag == "ScoreCollider")
        {
            collider.GetComponent<BoxCollider2D>().enabled = false;
            scoreKeeper.Score(scoreValue);
        }

    }
}
