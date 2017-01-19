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

    //Fadeing out Speed
    float fadePerSec = 0.5f;
    bool fadeOutbool;

    // Use this for initialization
    void Start () {
        fadeOutbool = false;
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
        if (fadeOutbool)
            fadeOut();
    }

    void placeOrderInBubble() //switch could be used here
    {
        for(int i=0;i<order.GetLength(0);i++)
        {
            if(order[i] == 0)
            {
                iceBalls[i] = Instantiate(erdbeere, transform.position + orderPositions[i], transform.rotation); //this could be a one liner, but this way it is way easier to implement a fading out method
                iceBalls[i].transform.parent = transform;
            }
            else if(order[i] == 1)
            {
                iceBalls[i] = Instantiate(schoko, transform.position + orderPositions[i], transform.rotation);
                iceBalls[i].transform.parent = transform;
            }
            else if (order[i] == 2)
            {
                iceBalls[i] = Instantiate(vanille, transform.position + orderPositions[i], transform.rotation);
                iceBalls[i].transform.parent = transform;
            }
        }
    }

    void fadeOut() //will make the object and all iceballs slowly disappear IT WONT DESTROY THEM just working with the color
    {
        var material = GetComponent<Renderer>().material;
        var color = material.color;
        material.color = new Color(color.r, color.g, color.b, color.a - (fadePerSec * Time.deltaTime));
        for (int i = 0;i < iceBalls.GetLength(0);i++)
        {
            material = iceBalls[i].GetComponent<Renderer>().material;
            color = material.color;
            material.color = new Color(color.r, color.g, color.b, color.a - (fadePerSec * Time.deltaTime));
        }
        
    }

    public void destroySpeechbubble() //bye bye Speechbubbles we will miss you :'(
    {
        fadeOutbool = true;  //enable fadeOut() in Update()
        Destroy(gameObject, 5); //destroy this gameObject and it childs after time
        Debug.Log("Speechbubble wird zerstört");
    }
}
