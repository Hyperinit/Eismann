using UnityEngine;
using System.Collections;

public class signBehavior : MonoBehaviour {
    //NICHT GETESTET

    public float lifetime;
	// Use this for initialization
	void Start () {
        Destroy(gameObject, lifetime);
    }
	
	// Update is called once per frame
	void Update () {
        transform.LookAt(Camera.main.transform.position, -Vector3.up);
    }
}
