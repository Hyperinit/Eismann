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
}
