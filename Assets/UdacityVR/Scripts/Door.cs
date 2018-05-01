using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour 
{
    public bool locked = true;
    public bool open = false;

    public AudioClip[] audioClips;

    public GameObject waypoint;

    private AudioSource audioSource;
    // Create a boolean value called "locked" that can be checked in OnDoorClicked() 
    // Create a boolean value called "opening" that can be checked in Update() 

    private void Start()
    {
        waypoint.SetActive(false);
        audioSource = GetComponent<AudioSource>();
    }

    void Update() {
        if (open)
        {
            GameController.instance.CheckFinish();
        }
    }

    public void OnDoorClicked() {
        if (!locked)
        {
            open = true;
            GetComponent<Animator>().SetBool("opened", open);

            GetComponent<BoxCollider>().enabled = false;

            waypoint.SetActive(true);
            audioSource.clip = audioClips[0];
            audioSource.Play();
        }
        else
        {
            audioSource.clip = audioClips[1];
            audioSource.Play();
        }

        // Play a sound to indicate the door is locked


    }

    public void Unlock()
    {
        locked = false;
    }
}
