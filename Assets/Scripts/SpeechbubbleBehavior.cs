using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeechbubbleBehavior : MonoBehaviour {

    private GameController gameController; //GameController
    public GameObject erdbeere; // strawberry sprite
    public GameObject vanille; // vanilla sprite
    public GameObject schoko; // chocolate sprite
    public GameObject waldmeister; // waldmeister sprite
    public GameObject zitrone; // zitrone sprite
    private int[] order; // the order to be displayed
    private Vector3[] orderPositions; // positions where the sprites should be displayed
    private GameObject[] iceBalls; // this array contains all generated ice sprites
    public AudioSource popSound; // pop sound
    public AudioSource newOrderSound; // new order sound

    float timeTillDestroy; //after this time the speechbubble + ice sprites will be destroyed

    float fadePerSec = 0.5f;//Fadeing out Speed
    bool fadeOutbool; //if true speechbubble + ice sprites will fade out

    // Use this for initialization
    void Start () {
        fadeOutbool = false;
        GameObject gameControllerObject = GameObject.FindWithTag("GameController");
        gameController = gameControllerObject.GetComponent<GameController>();
        order=gameController.getOrder();
        timeTillDestroy = gameController.SpeechbubbleDestroyTime();
        StartCoroutine(TimeTillDestroy());
        iceBalls = new GameObject[order.GetLength(0)];

        orderPositions = new Vector3[] { new Vector3( -0.2f,0.1f,-0.1f), new Vector3( 0f,0.1f,-0.1f), new Vector3( 0.2f, 0.1f, -0.1f ),
                                            new Vector3( -0.1f,-0.05f,-0.1f), new Vector3( 0.1f,-0.05f,-0.1f)};
        placeOrderInBubble();
        newOrderSound.Play();

    }
	
	// Update is called once per frame
	void Update () {
        if (fadeOutbool)//TODO let this happen in a coroutine
            fadeOut();
    }

    //places the order as ice sprites in the speechbubble. 
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
                iceBalls[i] = Instantiate(vanille, transform.position + orderPositions[i], transform.rotation);
                iceBalls[i].transform.parent = transform;
            }
            else if (order[i] == 2)
            {
                iceBalls[i] = Instantiate(schoko, transform.position + orderPositions[i], transform.rotation);
                iceBalls[i].transform.parent = transform;
            }
            else if (order[i] == 3)
            {
                iceBalls[i] = Instantiate(waldmeister, transform.position + orderPositions[i], transform.rotation);
                iceBalls[i].transform.parent = transform;
            }
            else if (order[i] == 4)
            {
                iceBalls[i] = Instantiate(zitrone, transform.position + orderPositions[i], transform.rotation);
                iceBalls[i].transform.parent = transform;
            }
        }
    }

    //will make the object and all iceballs slowly disappear IT WONT DESTROY THEM just working with the color
    void fadeOut() 
    {
        var material = GetComponent<Renderer>().material;
        var color = material.color;
        material.color = new Color(color.r, color.g, color.b, color.a - (fadePerSec * Time.deltaTime));
        for (int i = 0;i < iceBalls.GetLength(0);i++)
        {
            if (order[i] >= 0)
            {
                material = iceBalls[i].GetComponent<Renderer>().material;
                color = material.color;
                material.color = new Color(color.r, color.g, color.b, color.a - (fadePerSec * Time.deltaTime));
            }
        }
        
    }

    //cals destroy function destroySpeechbubble() after timeTillDestroy
    IEnumerator TimeTillDestroy()
    {
        yield return new WaitForSeconds(timeTillDestroy);
        destroySpeechbubble();
    }

    //destroy speechbubble and it childs. plays a sound while doing this
    public void destroySpeechbubble()
    {
        popSound.Play();
        fadeOutbool = true;  //enable fadeOut() in Update()
        Destroy(gameObject, 5); //destroy this gameObject and it childs after time
    }
}
