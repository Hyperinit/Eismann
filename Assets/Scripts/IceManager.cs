using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceManager : MonoBehaviour {

	public Rigidbody IceCream;
	public Transform IceCreamPos;
	public int IceCreamNrOnTray = 1;
	public int MaxIceCreamNrOnTray=4;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		if (IceCreamNrOnTray<MaxIceCreamNrOnTray) {
			Rigidbody IceCreamInstance;
			IceCreamInstance = Instantiate (IceCream,IceCreamPos.position,IceCreamPos.rotation);
			IceCreamNrOnTray++;

		}
		
	}
}
