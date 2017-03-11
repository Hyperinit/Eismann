using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//On Trigger Enter will "reset" the waffle

public class TrashcanBehavior : MonoBehaviour {

    private GameController gameController;

    void Start()
    {
        GameObject gameControllerObject = GameObject.FindWithTag("GameController");
        gameController = gameControllerObject.GetComponent<GameController>();
    }
    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("waffle"))
        {
            gameController.TrashcanReset();
        }
    }
}
