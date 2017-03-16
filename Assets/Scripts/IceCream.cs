using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceCream : MonoBehaviour {

	// Material Variables
	public Material[] m_MaterialType; // creating an array holding the materials for the ice type.
									  // e.g. element0=strawberry, element1= vanilla...
    
	// Use this for initialization
	void Awake () {
 
    }

    // Update is called once per frame
    void Update ()
    {
		// to prevent the problem, that when the type of ice is assigned by other scripts (e.g. Waffle.cs)
		// and the change doesn't work because inspector always overwrites the inital setting to the change.
		// we need to put sharedMaterial here. Moreover, sharedMaterial updates in each frame.

        int Set = (int)gameObject.GetComponent<IceofWaffle>().m_Type; // converting the type to an integer value and stored in Set.
        GetComponent<Renderer>().sharedMaterial = m_MaterialType[Set]; // assigning the converted value to the correseponding array poistion in the material array.
																       // e.g. m_MaterialType[0] is strawberry, then when we have a converted value 0, then we have strawberry material.
    }
}
