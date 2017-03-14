using UnityEngine;
using System.Collections;

public class DroppointBehavior : MonoBehaviour {

    private GameController gameController;
    public AudioSource orderCorrect;
    public AudioSource orderWrong;


    void Start()
    {
        GameObject gameControllerObject = GameObject.FindWithTag("GameController");
        gameController = gameControllerObject.GetComponent<GameController>();
    }
    // Update is called once per frame
    void Update () {
	
	}

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
