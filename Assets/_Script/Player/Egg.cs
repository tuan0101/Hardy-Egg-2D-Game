using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

[RequireComponent(typeof(PlayerAudio), typeof(PlayerInput), typeof(EggMovement))]
public class Egg : MonoBehaviour
{
    [SerializeField] private PlayerAudio playerAudio;
    [SerializeField] private PlayerInput playerInput;
    [SerializeField] private EggMovement eggMovement;

    [SerializeField] private bool isDead = false;
    [SerializeField] private bool isGrounded = true;
    private bool gameHasEnded = false;

    private void Start()
    {
        playerAudio = GetComponent<PlayerAudio>();
        playerInput = GetComponent<PlayerInput>();
        eggMovement = GetComponent<EggMovement>();

    }

    void Update()
    {
        if (isDead != true)
        {
            EggMove();
            if (CrossPlatformInputManager.GetButtonDown("Jump") || Input.GetKey(KeyCode.Space))
            {
                EggJump();
            }
        }
    }

    void EggJump()
    {
        if (isGrounded)
        {
            isGrounded = false;
            eggMovement.Jump();
            playerAudio.PlayJumSound();

            //if the EGG is still on the air
            // still need a look again ????
            if (isGrounded == false)
            {
                eggMovement.EggPresenter.PlayJumpEffect();
            }
        }
    }
   
    void EggMove()
    {
        //controls
        if (ButtonManager.isSlider)
        {
            playerInput.StickControl.SetActive(true);
            playerInput.FixedBtton.SetActive(false);
        }
        else
        {
            playerInput.FixedBtton.SetActive(true);
            playerInput.StickControl.SetActive(false);
        }

        eggMovement.MoveEgg();

        if (!isGrounded)
        {
            eggMovement.EggPresenter.PlayRunEffect();
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

            playerAudio.PlayHitSound();
            if (gameHasEnded != true)
            {
                isDead = true;
                gameHasEnded = true;
                eggMovement.BreakEgg();
                playerInput.DisableJump();
            }

            playerAudio.PlayGameOver();
            eggMovement.EggPresenter.UIGameOver();
        }
    }
}
