using UnityEngine;
using System.Collections;

public class WaffleBehavior : MonoBehaviour {

	// Use this for initialization
	void Start () {
        transform.RotateAround(transform.position, transform.up, 180f);//rotate y-axis 180 at spawn
    }
	
	// Update is called once per frame
	void Update () {
	
	}
    public void DestroyWaffle()
    {
        Destroy(gameObject,0);
    }
}
