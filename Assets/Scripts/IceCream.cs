using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceCream : MonoBehaviour {

	public Material[] m_MaterialType;
   

	public void SetType (IceofWaffle.IceType _newType)
	{
		GetComponent<Renderer> ().sharedMaterial = m_MaterialType [(int)_newType];
	}

	public void SelfDestruction(){
		Destroy (gameObject);
	}

	// Testing variables and methods.
	private float m_Time=0.0f; //TESTING

	
	private void TestingColor(){
		
		m_Time += Time.deltaTime;

		if (m_Time < 1.0f) {
			SetType (IceofWaffle.IceType.Vanilla);
			Debug.Log("Now shift to vanilla");
		} else if (m_Time < 2.0f) {
			SetType (IceofWaffle.IceType.Chocolate);
			Debug.Log("Now shift to chocolate");
		} else if (m_Time < 3.0f) {
			SetType (IceofWaffle.IceType.Strawberry);
			Debug.Log("Now shift to strawberry");
		} else if (m_Time > 3.0f) {
			m_Time = 0.0f;
		}
	}
	

	// Use this for initialization
	void Start () {

		IceofWaffle IceofWaffleScript = GetComponent<IceofWaffle> ();
        Debug.Log("before setting Ice Material "+IceofWaffleScript.m_Type.ToString());
		SetType (IceofWaffleScript.m_Type);
        Debug.Log("after setting Ice Material " + IceofWaffleScript.m_Type.ToString());
    }
	
	// Update is called once per frame
	void Update () {

		//TestingColor ();
		
	}
}
