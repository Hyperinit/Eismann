using UnityEngine;
using System.Collections;

public class DroppointBehavior : MonoBehaviour {

    private GameController gameController; //GameController
    public AudioSource orderCorrect; //if order is correct, play this sound
    public AudioSource orderWrong; //if order is wrong, play this sound


    void Start()
    {
        GameObject gameControllerObject = GameObject.FindWithTag("GameController");
        gameController = gameControllerObject.GetComponent<GameController>();
    }
    // Update is called once per frame
    void Update () {
	
	}

    //if waffle enters the droppoint collider, it checks if the waffle is correct an plays the appropriate sound
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("waffle"))
        {
            if(gameController.IceIsInDelivery())
            {
                orderCorrect.Play();
            }
            else
            {
                orderWrong.Play();
            }
        }
    }

}
