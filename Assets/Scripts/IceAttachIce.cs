using UnityEngine;
using System.Collections;

public class IceAttachIce : MonoBehaviour
{

    private GameController gameController;

    void Start()
    {


        GameObject gameControllerObject = GameObject.FindWithTag("GameController");
        gameController = gameControllerObject.GetComponent<GameController>();
    }


    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Ice"))
        {
            transform.SetParent(other.transform);
            //transform.parent = other.transform;
            gameController.AddScore(1);

        }
    }
}
