using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waffle : MonoBehaviour 
{
	//controlling variable
	public int m_NumberOfActiveScoops = 0; // number of active scoops.

	//array for holding the scoop objects.
	public GameObject[] Scoops; // Number of scoops of the waffle. can be set from the insepctor by giving the number to the size.

	// controlling reference.
	private IceofWaffle IceCreamType; // Reference to the IceofWaffle script.

	// variables for Ice Order
	private int[] iceOrder = new int[] { -1,-1,-1,-1,-1}; // when the waffle is not full, the remaining will be set as -1.
														  // e.g. when Strawberry type has int value = 0, then we have a int 0 in the array.
	private int IceOrderNr = 0; // the number of current order.
	private int MaxIceOrderNr = 5; // this number corresponds to the number of invisisble scoops on the waffle.

	// Reading IceOrder
	public int[] IceOrder {
		
		get
		{
			return iceOrder;
		}
	}

	// check the number of active scoops.
	public int CheckNumberofVisibleScoops()
	{
		return m_NumberOfActiveScoops;
	}

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	// our waffle has 5 inactive (invisisble) scoops,
	// when the ice scoop is held and brought to have the contact to the waffle by a controller,
	// the following things happen;
	// 1. the waffle which contacts the waffle will be destroyed.
	// 2. an inactive scoop of the waffle will be set as active, meaning, we can see a scoop on the waffle.
	void OnTriggerEnter(Collider other)
	{
		// when the ice scoop object is entering the box collider of the waffle,
		// the tag of the ice scoop object is compared to see if it is the ice or not. we only want to destroy the ice scoop object, not other objects.
		// when the condition mentioned above is true, and the number of active scoops on the waffle is smaller than the maximum ice order number, then the following things happen:
		if (other.gameObject.CompareTag("Ice") && m_NumberOfActiveScoops < MaxIceOrderNr)
		{
			
			IceCreamType = other.gameObject.GetComponent<IceofWaffle>(); // assign the IceCreamType of the current waffle to the Type of contacted Scoop.

	        //when the length of the array of the scoop objects is greater than the number of active scoops.
            if (Scoops.Length > m_NumberOfActiveScoops) 
			{
                Scoops[m_NumberOfActiveScoops].GetComponent<IceofWaffle>().m_Type = IceCreamType.m_Type; // assigning the ice type to the active scoop.
                iceOrder[IceOrderNr]=(int)IceCreamType.m_Type; // converting the IceCreamType to an integer and store it into an array for further comparison.
	
			
				Scoops [m_NumberOfActiveScoops].SetActive (true); // set the inactive scoop on the waffle to be active (visible)
				Destroy (other.gameObject); // Destroy the contacted scoop

				m_NumberOfActiveScoops++; //increment the number of active scoops.

				// if the number of ice order is smaller than the maximum ice order number
				if (IceOrderNr < MaxIceOrderNr) {
					IceOrderNr++; // increment ice order number
				} else {
					Debug.Log ("The Waffle is full.");
				}
			}
			
		}
	}
}
