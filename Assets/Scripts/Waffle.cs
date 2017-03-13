﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waffle : MonoBehaviour 
{
	public int m_NumberOfActiveScoops = 0;

	public GameObject[] Scoops;
	private IceofWaffle IceCreamType;
	private int[] iceOrder = new int[] { -1,-1,-1,-1};
	private int IceOrderNr = 0;
	private int MaxIceOrderNr = 4;


	public int[] IceOrder {
		
		get
		{
			return iceOrder;
		}
	}

	public void PrintNrandName()
	{
		for (int i = 0; i < Scoops.Length; i++) {
			Debug.Log ("Ice Number"+i+"Ice Name"+Scoops[i].name);
		}
		
	}

	/*public void CheckIceType(GameObject[] Scoops)
	{
		if (Scoops[i].SetActive (true)) 
		{
			Scoops[i];
		}
		
	}*/

	public int CheckNumberofInvisibleScoops()
	{
		return m_NumberOfActiveScoops;
	}


	/*
	public void SetScoopVisible()
	{
			if (Scoops[i].SetActive (false)) 
			{
				Scoops[i].SetActive (true);
			}
	}*/

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.CompareTag("Ice")&&!(m_NumberOfActiveScoops==MaxIceOrderNr))
		{
			Debug.Log ("Compare Tag: " + other.gameObject.CompareTag (other.tag).ToString ());
			Debug.Log ("Transform Tag is " + tag);

			//IceofWaffle IceofTypeScript = other.gameObject.GetComponent<IceofWaffle> ();

			IceCreamType = other.gameObject.GetComponent<IceofWaffle>(); // assign the IceCreamType of the current waffle to the Type of contacted Scoop.

            Debug.Log("++++++ set Current Ice Type " + IceCreamType.m_Type.ToString());
            if (Scoops.Length > m_NumberOfActiveScoops) 
			{
                Scoops[m_NumberOfActiveScoops].GetComponent<IceofWaffle>().m_Type = IceCreamType.m_Type;

                //Scoops [m_NumberOfActiveScoops].GetComponent<IceCream>().SetType (IceofTypeScript.m_Type);

                //Scoops [m_NumberOfActiveScoops].GetComponent<IceCream>().SetType(IceCreamType.m_Type); // assign the IceCreamType to specific Scoops.
                iceOrder[IceOrderNr]=(int)IceCreamType.m_Type; // converting the IceCreamType to an integer and store it into an array for further comparison.

                Debug.Log ("Number of Scoops "+ Scoops.Length.ToString()); 
				Debug.Log ("Number of Active Scoops "+ m_NumberOfActiveScoops.ToString());
				Debug.Log ("Current Index of NumberOfActiveScoops "+Scoops[m_NumberOfActiveScoops].ToString());
				Debug.Log ("++++++Current Ice Type "+Scoops[m_NumberOfActiveScoops].GetComponent<IceofWaffle>().m_Type.ToString());

				Scoops [m_NumberOfActiveScoops].SetActive (true);
				Destroy (other.gameObject); // Destroy the contacted scoop


				m_NumberOfActiveScoops++;


				if (IceOrderNr < MaxIceOrderNr) {
					IceOrderNr++;
				} else {
					Debug.Log ("The Waffle is full.");
				}
			}
			
		}
	}
}
