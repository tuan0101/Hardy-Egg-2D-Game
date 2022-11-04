using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EggPresenter : MonoBehaviour
{
    [Header("Egg View")]
    [SerializeField] private Transform brokenEgg;
    [SerializeField] private bool ghost;
    public Animator MyAnim { get; set; }

    [Header("Particle System")]
    [SerializeField] private ParticleSystem runEffect;
    [SerializeField] private ParticleSystem jumpEffect;
    [SerializeField] private ParticleSystem.EmissionModule jumpEmission;
    [SerializeField] private ParticleSystem.EmissionModule runEmission;
    

    [Header("UI")]
    [SerializeField] private GameObject gameOverUI;
    GameObject[] myPlayer;
    Animator UIAnim;     
    Animator UIScreen;
    
    public ParticleSystem JumpEffect { get { return jumpEffect; }  set {jumpEffect = value; } }


    private void Awake()
    {
        runEmission = runEffect.emission;// .GetComponent<ParticleSystem>().emission;
    }

    private void Start()
    {
        MyAnim = GetComponentInChildren<Animator>();
        UIAnim = GameObject.Find("GameOverUI").GetComponent<Animator>();
        UIScreen = GameObject.Find("ScreenShader").GetComponent<Animator>();
    }

    // Start is called before the first frame update
    public void PlayJumpEffect()
    {
        jumpEffect.Play();
        jumpEmission = jumpEffect.emission;
        runEffect.Stop();
        //runEmission.enabled = false;
        jumpEmission.enabled = true;
    }

    public void PlayRunEffect()
    {
        runEffect.Play();
        runEmission = runEffect.emission;
    }

    public void PlayBrokenEffect()
    {
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

    public void UIGameOver()
    {
        UIAnim.SetTrigger("isGameOver");
        UIScreen.SetTrigger("isShader");
    }

    public void SetRunEmission(bool value)
    {
        runEmission.enabled = value;
    }
}
