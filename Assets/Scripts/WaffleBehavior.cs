using UnityEngine;
using System.Collections;

public class WaffleBehavior : MonoBehaviour {
    int[] order;
    int[] iceballs;

	// Use this for initialization
	void Start () {
        transform.RotateAround(transform.position, transform.up, 180f);//rotate y-axis 180 at spawn
        iceballs = new int[] { 0,0,0,0,0};
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

    public int[] pullIceballs()
    {
        return iceballs;
    }
}
