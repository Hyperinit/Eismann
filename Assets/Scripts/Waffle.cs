using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waffle : MonoBehaviour 
{
	public int m_NumberOfActiveScoops = 0;

	public GameObject[] Scoops;

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
		if (other.gameObject.CompareTag ("Ice")) 
		{
			Debug.Log ("Compare Tag: " + other.gameObject.CompareTag (other.tag).ToString ());
			Debug.Log ("Transform Tag is " + tag);

			IceofWaffle IceofTypeScript = other.gameObject.GetComponent<IceofWaffle> ();

			if (Scoops.Length < m_NumberOfActiveScoops) 
			{
				Scoops [m_NumberOfActiveScoops].GetComponent<IceCream>().SetType (IceofTypeScript.m_Type);


				Scoops [m_NumberOfActiveScoops].SetActive (true);

				m_NumberOfActiveScoops++;
			}

		} 
		else if (other.gameObject.CompareTag("IceVanilla"))
		{
			Debug.Log ("Compare Tag: " + other.gameObject.CompareTag (other.tag).ToString ());
			Debug.Log ("Transform Tag is " + tag);

			IceofWaffle IceofTypeScript = other.gameObject.GetComponent<IceofWaffle> ();

			if (Scoops.Length < m_NumberOfActiveScoops) 
			{
				Scoops [m_NumberOfActiveScoops].GetComponent<IceCream>().SetType (IceofTypeScript.m_TypeVanilla);


				Scoops [m_NumberOfActiveScoops].SetActive (true);

				m_NumberOfActiveScoops++;
			}
			
		}
		else if (other.gameObject.CompareTag("IceStrawberry"))
		{
			Debug.Log ("Compare Tag: " + other.gameObject.CompareTag (other.tag).ToString ());
			Debug.Log ("Transform Tag is " + tag);

			IceofWaffle IceofTypeScript = other.gameObject.GetComponent<IceofWaffle> ();

			if (Scoops.Length < m_NumberOfActiveScoops) 
			{
				Scoops [m_NumberOfActiveScoops].GetComponent<IceCream>().SetType (IceofTypeScript.m_TypeStrawberry);


				Scoops [m_NumberOfActiveScoops].SetActive (true);

				m_NumberOfActiveScoops++;
			}
			
		}
		else if (other.gameObject.CompareTag("IceChocolate"))
		{
			Debug.Log ("Compare Tag: " + other.gameObject.CompareTag (other.tag).ToString ());
			Debug.Log ("Transform Tag is " + tag);

			IceofWaffle IceofTypeScript = other.gameObject.GetComponent<IceofWaffle> ();

			if (Scoops.Length < m_NumberOfActiveScoops) 
			{
				Scoops [m_NumberOfActiveScoops].GetComponent<IceCream>().SetType (IceofTypeScript.m_TypeChocolate);


				Scoops [m_NumberOfActiveScoops].SetActive (true);

				m_NumberOfActiveScoops++;
			}
			
		}
	}
}
