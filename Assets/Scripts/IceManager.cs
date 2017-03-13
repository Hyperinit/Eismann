using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NewtonVR;

public class IceManager : MonoBehaviour {

	private NVRPhysicalController myController;

	public Rigidbody IceCreamVanilla;
	public Transform IceCreamVanillaPos1;
	public Transform IceCreamVanillaPos2;
	public Transform IceCreamVanillaPos3;

	public int IceCreamNrOnTray = 1;
	public int MaxIceCreamNrOnTray=4;

	// Use this for initialization
	void Start () {
		myController = GetComponent<NVRPhysicalController> ();
	}

	void OnTriggerEnter(Collider other){

		if (other.gameObject.GetType () == myController.GetType ()) {
			Rigidbody IceCreamInstance1;
			//Rigidbody IceCreamInstance2;
			//Rigidbody IceCreamInstance3;

			IceCreamInstance1 = Instantiate (IceCreamVanilla,myController.transform.position,myController.transform.rotation);
			//IceCreamInstance2 = Instantiate (IceCream,IceCreamVanillaPos2.position,IceCreamVanillaPos2.rotation);
			//IceCreamInstance3 = Instantiate (IceCream,IceCreamVanillaPos3.position,IceCreamVanillaPos3.rotation);
		}
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
