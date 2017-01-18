using UnityEngine;
using System.Collections;

public class CustomerMovement : MonoBehaviour {
    //DIESES SCRIPT IST NICHT GETESTET
    //Es fehlt insbesondere die Naviagation und das hochhalten des Schildes, sowie der Input des Schildes.

    //navigation
    Transform waitingAreaTransform;
    Transform disengageWall;
    UnityEngine.AI.NavMeshAgent nav;

	//animator
	Animator anim;

    //gamemechanics
    bool served;
    bool allowDisappear;
    bool walking;

    //speechbubble
    public GameObject speechbubble;
    GameObject speechbubbleClone;
    float speechbubbleTimer;
    bool speechbubbleTimerActive;
    SpeechbubbleBehavior SpeechbubbleBehaviorScript;


    // Use this for initialization
    void Start () {
        //serveMe();
        speechbubbleTimer = 2f;
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
        if (walking) //läuft bis TriggerEvent
        {
            nav.SetDestination(waitingAreaTransform.position);
        }
        else if (!served) //walking wurde durch trigger auf false gestellt. Bei served=false wartet der Kunde an der Theke. Bei true verlässt er das Spiel
        {

		}
        else
        {
            walking = true;
        }

        if(speechbubbleTimerActive) //wird mit serveMe() gestartet. Nach speechbubbleTimer wird die Speechbubble erzeugt 
        {
            speechbubbleTimer -= Time.deltaTime;
            if(speechbubbleTimer<=0)
            {
                spawnSpeechbubble();
                speechbubbleTimerActive = false;
            }
        }

    }

    public void setServed()
    {
        served = true;
        allowDisappear = true;
        anim.SetInteger("state", 0);
        if (Random.value>0.5f)
        {
            waitingAreaTransform = GameObject.FindGameObjectWithTag("wallLeft").transform;
        }
        else
        {
            waitingAreaTransform = GameObject.FindGameObjectWithTag("wallRight").transform;
        }
        SpeechbubbleBehaviorScript.destroySpeechbubble();
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log(other + " Customer");
        if (other.gameObject.CompareTag("waitingArea")) //Kunde erreicht die Theke und wartet, bis er bedient wird.
        {
            walking = false;
            Debug.Log("anim.SetInteger (state, 1)");
            anim.SetInteger ("state", 1);
            Debug.Log("serveMe() wird aufgerufen");
            serveMe();
        }
        if ((other.gameObject.CompareTag("wallLeft")|| other.gameObject.CompareTag("wallRight")) && allowDisappear) //Kunde darf das Spiel verlassen und verschwindet, sobald er mit einer der Spielgrenzen kollidiert
        {
            Destroy(gameObject, 0);
        }
    }

    void serveMe() //wird aufgerufen, wenn Kunde die Theke erreicht.
    {
        Debug.Log("serveMe() wurde aufgerufen");
        speechbubbleTimerActive = true;//starte Timer, damit die Speechbubble erzeugt und angezeigt wird.
    }

    void spawnSpeechbubble()
    {
        Vector3 offset = new Vector3(-0.79f, 1.52f, 0f);//SpawnPosition der Speechbubble in relation zu Customer
        speechbubbleClone = (GameObject)Instantiate(speechbubble, transform.position + offset, transform.rotation);
        SpeechbubbleBehaviorScript = (SpeechbubbleBehavior)speechbubbleClone.GetComponent(typeof(SpeechbubbleBehavior));
    }
}
