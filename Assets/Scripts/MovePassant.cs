using UnityEngine;
using System.Collections;

public class MovePassant : MonoBehaviour {

    private Rigidbody rb;
    public float speed;
    public bool disappear;
    public float disappearProbability;

    private GameController gameController;

    void Start()
    {
        GameObject gameControllerObject = GameObject.FindWithTag("GameController");
        gameController = gameControllerObject.GetComponent<GameController>();
        rb = GetComponent<Rigidbody>();
        rb.velocity = transform.right * -speed;
    }

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

    void disappearDecision()
    {
        if(disappear)
        {
            if(disappearProbability>=Random.Range(0.0f,1.0f))
            {
                gameController.ReduceCounterPassants();
                Destroy(gameObject, 0);
            }
        }
    }
}
