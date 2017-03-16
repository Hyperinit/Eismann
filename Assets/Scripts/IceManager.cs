using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NewtonVR;

public class IceManager : MonoBehaviour {

	// NewtonVR Variables
	public NVRHand LeftHand; // using the left controller through newton vr.
    public NVRHand RightHand; // using the right controller through newton vr.
 	
	// Prefab Object
	public Rigidbody IceCream;

	// Coroutine Variable
    private bool canIScoop;

	// Use this for initialization
	void Start () {

        canIScoop = true; // Ice scoop is allowed to be generated.

    }
 
    
	// When a controller comes into a boxcollider of the object, which has the "IceManager.cs" script, and when the 
	// trigger button is pressed down, a scoop ice will be created.

	void OnTriggerStay(Collider other){
   
		// check the trigger button of which controller is pressed down, also if it is allowed to generated a scoop of ice
		//the canIScoop variable is used for preventing creating multiple scoops at one time.
       if ((LeftHand.UseButtonDown==true  || RightHand.UseButtonDown==true) && canIScoop)
		{
			// since we can only know which controller will be the right and left hand after initialization, 
			// we need a reference to point to either one of the controller to make sure that the following things
			// will happen when either of the trigger button of one of the controller is pressed.

			NVRHand hand=GetComponent<NVRHand>();

			if (LeftHand.UseButtonDown == true) 
			{
				hand = LeftHand; // when the left controller enters the box collider and the trigger button is pressed down.
			} 
			else if (RightHand.UseButtonDown == true) 
			{
				hand = RightHand; // when the right controller enters the box collider and the trigger button is pressed down.
			} 
			else 
			{
				Debug.Log ("hand is not assgined.");  // nothing happens.
			}
	
            Rigidbody IceCreamInstance1; // the clone object of the scoop of ice.
		
			// creating the clone object of the scoop of ice at the position where a controller is in the box collider
			// and the trigger button is pressed down.
			IceCreamInstance1 = Instantiate (IceCream,other.gameObject.transform.position,other.gameObject.transform.rotation);
          
			// after the ice is created, it is automatically attached to the controller, which creates it.
			IceCreamInstance1.transform.SetParent(hand.transform);

			// when a scoop of ice is created, set the variable for coroutine to be false,
			// meaning, it is not allowed to create another scoop of ice at this time.
            canIScoop = false;

			// Start Coroutine here.
            StartCoroutine(ScoopSoon());

		}
	}

	// make the 0.5 second delay. 
    IEnumerator ScoopSoon()
    {
        yield return new WaitForSeconds(0.5f);
        canIScoop = true;
    }
	
	// Update is called once per frame
	void Update () {

	
	}
}
