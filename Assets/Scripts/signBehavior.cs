using UnityEngine;
using System.Collections;

public class signBehavior : MonoBehaviour {

    public float lifetime; //time till selfdestruction
	// Use this for initialization
	void Start () {
        Destroy(gameObject, lifetime);
    }
	
	// Update is called once per frame
	void Update () {
        transform.LookAt(Camera.main.transform.position, -Vector3.up); //position it in a good visible way
    }
}
