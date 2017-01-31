using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waffle : MonoBehaviour {

	public GameObject[] Scoops;
	public void PrintNrandName(){
		for (int i = 0; i < Scoops.Length; i++) {
			Debug.Log ("Ice Number"+i+"Ice Name"+Scoops[i].name);
		}
		
	}
	// Use this for initialization
	void Start () {
		
		Scoops = GameObject.FindGameObjectsWithTag("IceofWaffle");
		PrintNrandName ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
