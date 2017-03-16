using UnityEngine;
using System.Collections;

public class CustomerMovement : MonoBehaviour {

    //navigation
    Transform waitingAreaTransform; //a place near the counter to order ice cream
    Transform disengageWall; //a place to head to, if you want to leave
    UnityEngine.AI.NavMeshAgent nav; //the NavMeshAgent

	//animator
	Animator anim; //the animator

    //gamemechanics
    bool served; //true the customer is served
    bool allowDisappear; //if true the customer can disappear
    bool walking; //is the customer walking?

    //speechbubble
    public GameObject speechbubble; //speechbubble prototype
    GameObject speechbubbleClone; //the acutally speechbubble
    float speechbubbleTimer; //after reaching the counter, wait this amount of time till showing your speechbubble
    bool speechbubbleTimerActive; //have i already shown my speechbubble?
    SpeechbubbleBehavior SpeechbubbleBehaviorScript; //save the script for easier access later

    //gameController;
    private GameController gameController; //Game Controller

    //audio
    public AudioSource hi; //Hi sound
    public AudioSource nom; //Nom sound
    public AudioSource oh; //Oh sound

    // Use this for initialization
    void Start () {
        GameObject gameControllerObject = GameObject.FindWithTag("GameController");
        gameController = gameControllerObject.GetComponent<GameController>();
        //serveMe();
        speechbubbleTimer = 1.5f;
        speechbubbleTimerActive = false;
    }

    void Awake()
    {
		anim = GetComponent <Animator> ();
        waitingAreaTransform = GameObject.FindGameObjectWithTag("waitingArea").transform;
        nav = GetComponent<UnityEngine.AI.NavMeshAgent>();
        served = false;
        allowDisappear = false;
        walking = true;
    }

    // Update is called once per frame
    void Update () {
        if (walking) //runs till trigger event
        {
            nav.SetDestination(waitingAreaTransform.position);
        }
        else if (!served)
        {

		}
        else
        {
            walking = true;
        }

        if(speechbubbleTimerActive) //set true with serveMe(). After speechbubbleTimer hits 0 the speechbubble gets generated 
        {
            speechbubbleTimer -= Time.deltaTime;
            if(speechbubbleTimer<=0)
            {
                hi.Play();
                spawnSpeechbubble();
                speechbubbleTimerActive = false;
                gameController.StartPointDecay();
            }
        }

    }

    //called by GameController. If called the customer is served and will try to wander off and disappar
    public void setServed()
    {
        served = true;
        allowDisappear = true;
        StartCoroutine(servedCoroutine());
        try { SpeechbubbleBehaviorScript.destroySpeechbubble(); }
        catch { }
        gameController.StopPointDecay();
    }

    //plays the served animation and determines exit route
    IEnumerator servedCoroutine()
    {
        anim.SetInteger("state", 2);
        nom.Play();
        yield return new WaitForSeconds(2.0f);
        anim.SetInteger("state", 0);
        if (Random.value > 0.5f)
        {
            waitingAreaTransform = GameObject.FindGameObjectWithTag("wallLeft").transform;
        }
        else
        {
            waitingAreaTransform = GameObject.FindGameObjectWithTag("wallRight").transform;
        }
    }


    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("waitingArea")) //customer reaches the counter and waits until he is served.
        {
            walking = false;
            anim.SetInteger ("state", 1);
            serveMe();
        }
        if ((other.gameObject.CompareTag("wallLeft")|| other.gameObject.CompareTag("wallRight")) && allowDisappear) //customer may leave the game and disappear as soon as he collides with one of the game limits
        {
            Destroy(gameObject, 0);
        }
    }

    //is called when the customer reaches the counter.
    void serveMe()
    {
        speechbubbleTimerActive = true;
    }

    //spawns a speechbubble
    void spawnSpeechbubble()
    {
        Vector3 offset = new Vector3(-0.79f, 1.52f, 0f);//SpawnPosition in relation to customer
        speechbubbleClone = (GameObject)Instantiate(speechbubble, transform.position + offset, transform.rotation);
        SpeechbubbleBehaviorScript = (SpeechbubbleBehavior)speechbubbleClone.GetComponent(typeof(SpeechbubbleBehavior));

    }

    //plays the oh sound
    public void makeOhSound()
    {
        oh.Play();
    }
}
