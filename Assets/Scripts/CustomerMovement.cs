using UnityEngine;
using System.Collections;

public class CustomerMovement : MonoBehaviour {
    //DIESES SCRIPT IST NICHT GETESTET
    //Es fehlt insbesondere die Naviagation und das hochhalten des Schildes, sowie der Input des Schildes.

    //navigation
    Transform waitingAreaTransform;
    Transform disengageWall;
    NavMeshAgent nav;

    //gamemechanics
    bool served;
    bool allowDisappear;
    bool walking;

    //sign
    public GameObject sign;


    // Use this for initialization
    void Start () {
        serveMe();

    }

    void Awake()
    {
        waitingAreaTransform = GameObject.FindGameObjectWithTag("waitingArea").transform;
        nav = GetComponent<NavMeshAgent>();
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
    }

    public void setServed()
    {
        served = true;
        allowDisappear = true;
        if(Random.value>0.5f)
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
        if (other.gameObject.CompareTag("waitingArea")&&(!served)) //Kunde erreicht die Theke und wartet, bis er bedient wird.
        {
            walking = false;
            serveMe();
            
        }
        if ((other.gameObject.CompareTag("wallLeft")|| other.gameObject.CompareTag("wallRight")) && allowDisappear) //Kunde darf das Spiel verlassen und verschwindet, sobald er mit einer der Spielgrenzen kollidiert
        {
            Destroy(gameObject, 0);
        }
    }

    void serveMe() //wird aufgerufen, wenn Kunde die Theke erreicht.
    {
        //halte das Schild hoch
        Vector3 offset=new Vector3(0f,0.2f,0f);
        Instantiate(sign, transform.position+offset, transform.rotation);
    }
}
