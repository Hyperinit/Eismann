using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NewtonVR;

public class IceManager : MonoBehaviour {

	private NVRHand Hand;
    private NVRPhysicalController Controller;
    private GameObject handObject;
    private GameObject controllerObject;

	public Rigidbody IceCreamVanilla;
	public Transform IceCreamVanillaPos1;
	public Transform IceCreamVanillaPos2;
	public Transform IceCreamVanillaPos3;

	public int IceCreamNrOnTray = 1;
	public int MaxIceCreamNrOnTray=4;

	// Use this for initialization
	void Start () {
		Hand = GetComponent<NVRHand> ();
        Controller = GetComponent<NVRPhysicalController>();

        handObject = Hand.gameObject;

        Debug.Log("what is the type of handobject? "+ handObject.GetType());
        //controllerObject = controller;


	}
    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        Debug.Log("hit by the controller" + hit.gameObject.ToString());
    }


    


	void OnTriggerEnter(Collider other){
        //Debug.Log("what is the type of the object entering the box collider ? "+ other.gameObject.GetType().ToString());
        //Debug.Log(Controller.GetType().ToString());
        
        //if (hit.GetType () == myHand.GetType ()) {
        //Rigidbody IceCreamInstance1;
        //Rigidbody IceCreamInstance2;
        //Rigidbody IceCreamInstance3;

        //IceCreamInstance1 = Instantiate (IceCreamVanilla,myHand.transform.position,myHand.transform.rotation);
        //IceCreamInstance2 = Instantiate (IceCream,IceCreamVanillaPos2.position,IceCreamVanillaPos2.rotation);
        //IceCreamInstance3 = Instantiate (IceCream,IceCreamVanillaPos3.position,IceCreamVanillaPos3.rotation);
        //}
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
