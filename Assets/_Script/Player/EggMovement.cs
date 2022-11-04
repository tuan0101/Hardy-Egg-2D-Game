using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

[RequireComponent(typeof(EggPresenter))]
public class EggMovement : MonoBehaviour
{
    public EggPresenter EggPresenter { get; set; }

    [Header("Egg Attributes")]
    [SerializeField] private float moveX, moveY;
    [SerializeField] private float moveSpeed, jumpPower;
    [SerializeField] private bool facingRight = false;
    private Rigidbody2D rd;

    [Header("Auto AI")]
    [SerializeField] private float targetDistance;
    [SerializeField] private float firstPoint;
    [SerializeField] private float secondPoint;
    [SerializeField] private float getHeight;
    [SerializeField] private float brkEggSpeed;

    [Header("Ghost Effect")]
    [SerializeField] private bool autoPlay = false;
     public bool FlipIsTrue { get; set; }
    public bool Ghost { get; set; }

    [Header("Score")]
    private int scoreValue = 1;
    [SerializeField] private ScoreKeeper scoreKeeper;

    // Start is called before the first frame update
    void Start()
    {
        EggPresenter = GetComponent<EggPresenter>();

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
            EggPresenter.JumpEffect.Play();
            //jumpEmission.enabled = true;
        }

        //Direction
        if (moveX < 0f && facingRight == false)
        {
            FlipPlayer();
            FlipIsTrue = true;
        }
        else if (moveX > 0f && facingRight == true)
        {
            FlipPlayer();
            FlipIsTrue = false;
        }

        //auto move
        //moveX = 1;
        //Physics
        rd.velocity = new Vector2(moveX * moveSpeed, rd.velocity.y);
        if (moveX != 0)
        {
            Ghost = true;
            EggPresenter.SetRunEmission(true);
            //runEffect.Emit(30);
        }
        else
        {
            Ghost = false;
            EggPresenter.SetRunEmission(false);
        }

        //animation
        EggPresenter.MyAnim.SetFloat("Speed", Mathf.Abs(rd.velocity.x));
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
        EggPresenter.PlayBrokenEffect();
    }
}
