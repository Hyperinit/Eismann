using UnityEngine;
using System.Collections;

public class WaffleBehavior : MonoBehaviour {
    int[] order;
    int[] iceballs;

	private Waffle myWaffle;


	// Use this for initialization
	void Start () {
        
        transform.RotateAround(transform.position, transform.up, 180f);//rotate y-axis 180 at spawn
        transform.Rotate(-90, 0, 0);
        iceballs = new int[] { 0,0,0,0,0}; // z.b 0 = white, 1= red...
    }
	
	// Update is called once per frame
	void Update () {
	
	}
    public void DestroyWaffle()
    {
        Destroy(gameObject,0);
    }

    public void GiveOrder(int[] nOrder)
    {
        order = nOrder;
    }

    public int[] PullIceballs()
    {
		myWaffle = GetComponent<Waffle> ();
		iceballs = myWaffle.IceOrder;
        return iceballs;
    }
}
