using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneControll : MonoBehaviour {
    AudioSource audioSource;
    public AudioClip click;
	public void loadScene(string name){
        StartCoroutine(load(name));
	}

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    } 
    //get to wait 0.3s to hear sound click or touch
    IEnumerator load(string name)
    {
        yield return new WaitForSeconds(0.3f);
        SceneManager.LoadScene(name);
        // Use this for initialization
    }

    public void OnClick()
    {
        //AudioSource.PlayClipAtPoint(click, transform.position, 10f);
        audioSource.PlayOneShot(click);
    }

    public void exit()
    {
        Application.Quit();
    }
}
