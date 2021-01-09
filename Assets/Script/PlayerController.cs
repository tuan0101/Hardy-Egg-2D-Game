using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityStandardAssets.CrossPlatformInput;
public class PlayerController : MonoBehaviour
{
    //static bool isSlider;
    public float moveSpeed, jumpPower;
    public float targetDistance;
    public float firstPoint;
    public float secondPoint;
    public float getHeight;
    public float brkEggSpeed;
    public bool isGrounded = true;
    public bool autoPlay = false;

    public ScoreKeeper scoreBoard;
    public Transform brokenEgg;
    public Animator myAni;
    public ParticleSystem runEffect;
    public ParticleSystem jumpEffect;
    public GameObject gameOverUI;
    public bool ghost;
    public bool flipIsTrue;
    public GameObject jumpButton;

    //Button Type pick
    public GameObject stickControl;
    public GameObject fixedBtton;

    ParticleSystem.EmissionModule jumpEmission;
    public ParticleSystem.EmissionModule runEmission;

    float moveX, moveY;
    bool gameHasEnded = false;
    bool facingRight = false;
    bool isDead = false;
    Animator UIAnim;
    Animator UIScreen;
    Rigidbody2D rd;
    int scoreValue = 1;
    //testing
    GameObject[] myPlayer;

    AudioSource audioSource;
    public AudioClip hit_sound;
    public AudioClip jump_sound;
    public AudioClip[] gameOver;
    LineRenderer lineRdr;
    int lineCount = 0;
    //Vector2 lastPos = Vector2.one * float.MaxValue;


    // Use this for initialization
    void Start()
    {
        rd = GetComponent<Rigidbody2D>();
        myAni = GetComponentInChildren<Animator>();
        UIAnim = GameObject.Find("GameOverUI").GetComponent<Animator>();
        UIScreen = GameObject.Find("ScreenShader").GetComponent<Animator>();
        lineRdr = GetComponent<LineRenderer>();
        scoreBoard = GameObject.Find("Score").GetComponent<ScoreKeeper>();
        audioSource = GetComponent<AudioSource>();
        audioSource.PlayOneShot(gameOver[2]);
    }

    private void Awake()
    {
        runEmission = runEffect.emission;// .GetComponent<ParticleSystem>().emission;
    }

    // Update is called once per frame
    void Update()
    {
        if (isDead != true)
        {
            if (!isGrounded)
            {
                runEffect.Play();
                runEmission = runEffect.emission;
            }
            PlayerMove();
            Jump();
        }
        //trigger running animation
        myAni.SetFloat("Speed", Mathf.Abs(rd.velocity.x));
        if (autoPlay) { auto(); }

        if (linePoints == null) linePoints = new List<Vector2>();
        linePoints.Add(transform.position);
        DrawLine();
    }




    void PlayerMove()
    {
        
        //controls
        if (ButtonManager.isSlider)
        {
            stickControl.SetActive(true);
            fixedBtton.SetActive(false);
        }
        else
        {
            fixedBtton.SetActive(true);
            stickControl.SetActive(false);
        }
        
        //moveX = Input.GetAxis("Horizontal");
        moveX = CrossPlatformInputManager.GetAxis("Horizontal");
        moveY = rd.velocity.y;

        //activate jump particle system
        if (moveY < -9 && moveY > -10)
        {
            jumpEffect.Play();
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
        if (moveX !=0)
        {
            ghost = true;
            runEmission.enabled = true;
            //runEffect.Emit(30);
        }
        else
        {
            ghost = false;
            runEmission.enabled = false;
        }
    }

    void Jump()
    {
        //if (Input.GetKey(KeyCode.Space))
        //if (Input.GetButtonDown("Fire1"))
        if (CrossPlatformInputManager.GetButtonDown("Jump")|| Input.GetKey(KeyCode.Space))
        {
            if (isGrounded)
            {
                isGrounded = false;
                rd.velocity = new Vector2(rd.velocity.x, jumpPower);
                //runEffect.Play();
                //runEmission.enabled = true;
                audioSource.PlayOneShot(jump_sound);
                
                if (isGrounded == false)
                {
                    jumpEffect.Play();
                    jumpEmission = jumpEffect.emission;
                    runEffect.Stop();
                    //runEmission.enabled = false;
                    jumpEmission.enabled = true;
                }
            }
        }
    }

    void FlipPlayer()
    {
        facingRight = !facingRight;
        Vector3 flip = gameObject.transform.localScale;
        flip.x *= -1;
        transform.localScale = flip;
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag == "ScoreCollider")
        {
            collider.GetComponent<BoxCollider2D>().enabled = false;
            scoreBoard.Score(scoreValue);
        }
        
    }
    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.tag == "Ground")
        {
            isGrounded = true;
            //runEmission.enabled = true;
        }

        if (other.collider.tag == "Obstacle")
        {

            audioSource.PlayOneShot(hit_sound);
            if (gameHasEnded != true)
            {
                isDead = true;
                gameHasEnded = true;
                BreakIt();
                jumpButton.SetActive(false);
                //Invoke("Restart", 3f);
            }
            int i = Random.Range(0, 2);
            audioSource.PlayOneShot(gameOver[i]);
            //myAni.SetTrigger("die");
            UIAnim.SetTrigger("isGameOver");
            UIScreen.SetTrigger("isShader");
        }
    }
    void Restart()
    {
        //SceneManager.LoadScene("Lose");
    }

    // calculated from a linear function of height-distance
    float firstDistance = 2.3f;
    float secondDistance = 1.7f;
    float offset0 = -1.25f;
    float offset1 = 2f;
    float offset2 = 2.3f;
    void auto()
    {
        // Drawing a debug line from the player to the obstacle
        RaycastHit2D theHit;
        Vector2 startPos = (Vector2)transform.position + new Vector2(1.2f, 0.5f);  // move the starting point up
        theHit = Physics2D.Raycast(startPos, new Vector2(1, 0.01f));
        Vector3 forward = transform.TransformDirection(Vector3.right) * 10;
        Debug.DrawRay(transform.position, forward, Color.red);

        if (theHit.collider)
            Debug.Log("Hit: " + theHit.collider.name);

        if (theHit.collider.tag == "Obstacle")
        {
            targetDistance = theHit.point.x - transform.position.x;
            getHeight = theHit.collider.transform.position.y;
            // can be jumped at either the first point or the second point
            firstPoint = firstDistance + (offset0 - getHeight) / offset1 * offset2;
            secondPoint = secondDistance - (offset0 - getHeight) / offset1;
            Debug.DrawLine(startPos, theHit.point, Color.red);
        }
        solution1();
    }

    

    void solution2()
    {
        if (targetDistance < (secondPoint))
        {
            if (isGrounded)
            {
                audioSource.PlayOneShot(jump_sound);
                isGrounded = false;            
                rd.velocity = new Vector2(rd.velocity.x, jumpPower);
            }
        }
    }
    void solution1()
    {
        if (targetDistance < firstPoint & targetDistance > (firstPoint - 0.3f))
        {
            //Jump();
            if (isGrounded)
            {
                isGrounded = false;
                audioSource.PlayOneShot(jump_sound);
                rd.velocity = new Vector2(rd.velocity.x, jumpPower);
            }
        }
        else solution2();
    }
    List<Vector2> linePoints = new List<Vector2>();

    void DrawLine()
    {

        lineRdr.positionCount = linePoints.Count;

        for (int i = lineCount; i < linePoints.Count; i++)
        {
            lineRdr.SetPosition(i, linePoints[i]);
        }
        lineCount = linePoints.Count;
    }

    public void BreakIt()
    {
        //stop the egg from falling down
        rd.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY;
        this.GetComponent<CapsuleCollider2D>().enabled = false;
        //remove smoke after death
        //runEmission.enabled = false;
        myPlayer = GameObject.FindGameObjectsWithTag("Player");
        for (int i = 0; i < myPlayer.Length; i++)
        {
            Destroy(myPlayer[i]);
        }
        Instantiate(brokenEgg, transform.position, transform.rotation);
        brokenEgg.localScale = transform.localScale;
    }
}
