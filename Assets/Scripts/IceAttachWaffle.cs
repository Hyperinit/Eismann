using UnityEngine;
using System.Collections;

public class IceAttachWaffle : MonoBehaviour {

    private GameController gameController;

	void Start () {


        GameObject gameControllerObject = GameObject.FindWithTag("GameController");
        gameController = gameControllerObject.GetComponent<GameController>();
	}
	

    void OnTriggerEnter(Collider other)
    {
        //if (other.gameObject.CompareTag("waffle"))
        {
            transform.SetParent(other.transform);
            //transform.parent = other.transform;
            gameController.AddScore(1);

        }
    }
}
