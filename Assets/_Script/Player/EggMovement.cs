using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

[RequireComponent(typeof(EggPresenter))]
public class EggMovement : MonoBehaviour
{
    public EggPresenter eggPresenter;

    [Header("Egg Attributes")]
    public float moveX, moveY;
    public float moveSpeed, jumpPower;
    public bool facingRight = false;
    Rigidbody2D rd;

    [Header("Auto AI")]
    public float targetDistance;
    public float firstPoint;
    public float secondPoint;
    public float getHeight;
    public float brkEggSpeed;

    [Header("Ghost Effect")]
    public bool autoPlay = false;
    public bool flipIsTrue;
    public bool ghost;

    [Header("Score")]
    int scoreValue = 1;
    public ScoreKeeper scoreKeeper;

    // Start is called before the first frame update
    void Start()
    {
        eggPresenter = GetComponent<EggPresenter>();

        rd = GetComponent<Rigidbody2D>();
    }

    public void MoveEgg()
    {
        //moveX = Input.GetAxis("Horizontal");
        moveX = CrossPlatformInputManager.GetAxis("Horizontal");
        moveY = rd.velocity.y;

        //activate jump particle system
        if (moveY < -9 && moveY > -10)
        {
            eggPresenter.jumpEffect.Play();
            //jumpEmission.enabled = true;
        }

        //Direction
        if (moveX < 0f && facingRight == false)
        {
            FlipPlayer();
            flipIsTrue = true;
        }
        else if (moveX > 0f && facingRight == true)
        {
            FlipPlayer();
            flipIsTrue = false;
        }

        //auto move
        //moveX = 1;
        //Physics
        rd.velocity = new Vector2(moveX * moveSpeed, rd.velocity.y);
        if (moveX != 0)
        {
            ghost = true;
            eggPresenter.runEmission.enabled = true;
            //runEffect.Emit(30);
        }
        else
        {
            ghost = false;
            eggPresenter.runEmission.enabled = false;
        }

        //animation
        eggPresenter.myAni.SetFloat("Speed", Mathf.Abs(rd.velocity.x));
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag == "ScoreCollider")
        {
            collider.GetComponent<BoxCollider2D>().enabled = false;
            scoreKeeper.Score(scoreValue);
        }

    }

    public void Jump()
    {
            rd.velocity = new Vector2(rd.velocity.x, jumpPower);
    }

    public void FlipPlayer()
    {
        facingRight = !facingRight;
        Vector3 flip = gameObject.transform.localScale;
        flip.x *= -1;
        transform.localScale = flip;
    }

    public void BreakEgg()
    {
        //stop the egg from falling down
        rd.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY;
        this.GetComponent<CapsuleCollider2D>().enabled = false;
        eggPresenter.PlayBrokenEffect();
    }
}
