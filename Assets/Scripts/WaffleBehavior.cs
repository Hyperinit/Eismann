using UnityEngine;
using System.Collections;

public class WaffleBehavior : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    public void DestroyWaffle()
    {
        Destroy(gameObject,0);
    }
}
