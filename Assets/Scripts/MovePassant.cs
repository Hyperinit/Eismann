using UnityEngine;
using System.Collections;

public class MovePassant : MonoBehaviour {

    private Rigidbody rb;
    public float speed; //speed the passant is moving
    public bool disappear; //is the passant allowed to disappear or does he have to wander forever?
    public float disappearProbability; //probabilty to disappear after colliding with an exit point
    private int lane; //describes on which lanes passant wander

    private GameController gameController; //GameController

    void Start()
    {
        GameObject gameControllerObject = GameObject.FindWithTag("GameController");
        gameController = gameControllerObject.GetComponent<GameController>();
        rb = GetComponent<Rigidbody>();
        rb.velocity = transform.right * -speed;
    }

    //if his lane is not parrallel to the grid. Rotate the passant
    public void AddRotation(float extraRotation)
    {
        transform.RotateAround(transform.position, transform.up, extraRotation);
    }

    //called by GameController to add lane description
    public void AddLaneDescription(int nLane)
    {
        lane = nLane;
    }

    //if passant hit an wall. passant will disappear or turn 180°
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("wallLeft"))
        {
            disappearDecision();
            rb.velocity = transform.right * speed;
            transform.RotateAround(transform.position, transform.up, 180f);//umdrehen der Blickrichtung
        }
        if(other.gameObject.CompareTag("wallRight"))
        {
            disappearDecision();
            rb.velocity = transform.right * speed;
            transform.RotateAround(transform.position, transform.up, 180f);//umdrehen der Blickrichtung
        }
    }

    //decides if passant will disappear
    void disappearDecision()
    {
        if(disappear)
        {
            if(disappearProbability>=Random.Range(0.0f,1.0f))
            {
                gameController.ReduceCounterPassants(lane);
                Destroy(gameObject, 0);
            }
        }
    }
}
