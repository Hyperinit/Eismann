using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NewtonVR;

public class IceManager : MonoBehaviour {

	public NVRHand LeftHand; // use insepctor to link the NVRHand left or right.
    public NVRHand RightHand;
    public NVRPhysicalController Controller;
    private GameObject handObject;
    private GameObject controllerObject;

	public Rigidbody IceCream;

    private bool canIScoop;

    public Transform IceCreamVanillaPos1;
	public Transform IceCreamVanillaPos2;
	public Transform IceCreamVanillaPos3;

	public int IceCreamNrOnTray = 1;
	public int MaxIceCreamNrOnTray=4;

	// Use this for initialization
	void Start () {
        //Hand = GetComponent<NVRHand> (); // not needing this one,  because no HVRHand script is attached to the current object. enable this will result overwritten feld of Hand.
        //Controller = GetComponent<NVRPhysicalController>();
        canIScoop = true;
       // handObject = Hand.gameObject;

        //Debug.Log("what is the type of handobject? "+ handObject.GetType());
        //controllerObject = controller;


    }
    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
       // Debug.Log("hit by the controller" + hit.gameObject.ToString());
    }


    


	/*void OnTriggerEnter(Collider other){
		// don't forget to set the boxcollider to "is trigger"
        Debug.Log("BoxCollider hit by a controller  "+ other.gameObject.GetType().ToString());
        Debug.Log("controller position "+ other.gameObject.transform.position.ToString());
        Debug.Log("controller rotation"+ other.gameObject.transform.rotation.ToString());


        //Debug.Log("what is the type of the object entering the box collider ? "+ other.gameObject.GetType().ToString());
        //Debug.Log(Controller.GetType().ToString());
        
        //if (hit.GetType () == myHand.GetType ()) {
        Rigidbody IceCreamInstance1;
        //Rigidbody IceCreamInstance2;
        //Rigidbody IceCreamInstance3;

		IceCreamInstance1 = Instantiate (IceCreamVanilla,other.gameObject.transform.position,other.gameObject.transform.rotation);
        //IceCreamInstance2 = Instantiate (IceCream,IceCreamVanillaPos2.position,IceCreamVanillaPos2.rotation);
        //IceCreamInstance3 = Instantiate (IceCream,IceCreamVanillaPos3.position,IceCreamVanillaPos3.rotation);
        //}
    }*/

	void OnTriggerStay(Collider other){
        // don't forget to set the boxcollider to "is trigger"



        //Debug.Log("what is the type of the object entering the box collider ? "+ other.gameObject.GetType().ToString());
        //Debug.Log(Controller.GetType().ToString());

        //if (hit.GetType () == myHand.GetType ()) {
       
       

       if ((LeftHand.UseButtonDown==true  || RightHand.UseButtonDown==true) && canIScoop)
		{
			NVRHand hand=GetComponent<NVRHand>();

			if (LeftHand.UseButtonDown == true) 
			{
				hand = LeftHand;
			} 
			else if (RightHand.UseButtonDown == true) 
			{
				hand = RightHand;
			} 
			else 
			{
				Debug.Log ("hand is not assgined.");
			}
				
            Debug.Log("UseButton " + LeftHand.UseButton.ToString());
            Debug.Log("BoxCollider hit by a controller  " + other.gameObject.GetType().ToString());
            Debug.Log("controller position " + other.gameObject.transform.position.ToString());
            Debug.Log("controller rotation" + other.gameObject.transform.rotation.ToString());

            Rigidbody IceCreamInstance1;
		//Rigidbody IceCreamInstance2;
		//Rigidbody IceCreamInstance3;

		IceCreamInstance1 = Instantiate (IceCream,other.gameObject.transform.position,other.gameObject.transform.rotation);
            //IceCreamInstance2 = Instantiate (IceCream,IceCreamVanillaPos2.position,IceCreamVanillaPos2.rotation);
            //IceCreamInstance3 = Instantiate (IceCream,IceCreamVanillaPos3.position,IceCreamVanillaPos3.rotation);
            //}
			IceCreamInstance1.transform.SetParent(hand.transform);

            canIScoop = false;
            StartCoroutine(ScoopSoon());
		}
	}

    IEnumerator ScoopSoon()
    {
        yield return new WaitForSeconds(0.5f);
        canIScoop = true;
    }
	
	// Update is called once per frame
	void Update () {

		/*if (IceCreamNrOnTray<MaxIceCreamNrOnTray) {
			Rigidbody IceCreamInstance1;
			Rigidbody IceCreamInstance2;
			Rigidbody IceCreamInstance3;

			IceCreamInstance1 = Instantiate (IceCream,IceCreamVanillaPos1.position,IceCreamVanillaPos1.rotation);
			IceCreamInstance2 = Instantiate (IceCream,IceCreamVanillaPos2.position,IceCreamVanillaPos2.rotation);
			IceCreamInstance3 = Instantiate (IceCream,IceCreamVanillaPos3.position,IceCreamVanillaPos3.rotation);

			IceCreamNrOnTray++;

		}*/
		
	}
}
