using UnityEngine;
using System.Collections;

//interface between waffle.cs and GameController.cs
public class WaffleBehavior : MonoBehaviour {
    int[] iceballs; //contains ice flavors as int

	private Waffle myWaffle;


	// Use this for initialization
	void Start () {
        
        transform.RotateAround(transform.position, transform.up, 180f);
        transform.Rotate(-90, 0, 0);
        iceballs = new int[] { 0,0,0,0,0};
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    //destroy the waffle
    public void DestroyWaffle()
    {
        Destroy(gameObject,0);
    }

    //called by GameController to recieve which components are in the waffle
    public int[] PullIceballs()
    {
		myWaffle = GetComponent<Waffle> ();
		iceballs = myWaffle.IceOrder;
        return iceballs;
    }
}
