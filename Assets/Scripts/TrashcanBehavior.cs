using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//On Trigger Enter will "reset" the waffle

public class TrashcanBehavior : MonoBehaviour {

    private GameController gameController; //GameController
    private AudioSource destroySoundWaffle; //destroy sound

    void Start()
    {
        GameObject gameControllerObject = GameObject.FindWithTag("GameController");
        gameController = gameControllerObject.GetComponent<GameController>();
        destroySoundWaffle = GetComponent<AudioSource>();
    }
    // Update is called once per frame
    void Update()
    {

    }

    //if a waffle enters the trigger collider, destroy it, play a sound and tell the GameController what happened
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("waffle"))
        {
            destroySoundWaffle.Play();
            gameController.TrashcanReset();
        }
    }
}
