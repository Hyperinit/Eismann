using UnityEngine;
using System.Collections;

public class MovePassant : MonoBehaviour {

    private Rigidbody rb;
    public float speed;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.velocity = transform.right * -speed;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("wallLeft"))
        {
            rb.velocity = transform.right * speed;
        }
        if(other.gameObject.CompareTag("wallRight"))
        {
            rb.velocity = transform.right * -speed;
        }
    }
}
