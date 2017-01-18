using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeechbubbleBehavior : MonoBehaviour {

    private GameController gameController;
    public GameObject erdbeere;
    public GameObject schoko;
    public GameObject vanille;
    private int[] order;
    private Vector3[] orderPositions;
    private GameObject[] iceBalls;


    // Use this for initialization
    void Start () {
        GameObject gameControllerObject = GameObject.FindWithTag("GameController");
        gameController = gameControllerObject.GetComponent<GameController>();
        order=gameController.getOrder();
        iceBalls = new GameObject[order.GetLength(0)];

        orderPositions = new Vector3[] { new Vector3( -0.2f,0.1f,-0.05f), new Vector3( 0f,0.1f,-0.05f), new Vector3( 0.2f, 0.1f, -0.05f ),
                                            new Vector3( -0.1f,-0.05f,-0.05f), new Vector3( 0.1f,-0.05f,-0.05f)};
        placeOrderInBubble();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void placeOrderInBubble()
    {
        for(int i=0;i<order.GetLength(0);i++)
        {
            if(order[i] == 0)
            {
                Instantiate(erdbeere, transform.position + orderPositions[i], transform.rotation).transform.parent = transform;
            }
            else if(order[i] == 1)
            {
                Instantiate(schoko, transform.position + orderPositions[i], transform.rotation).transform.parent = transform;
            }
            else if (order[i] == 2)
            {
                Instantiate(vanille, transform.position + orderPositions[i], transform.rotation).transform.parent = transform;
            }
        }
    }

    public void destroySpeechbubble()
    {
        Destroy(gameObject, 0);
        Debug.Log("Speechbubble wird zerstört");
    }
}
