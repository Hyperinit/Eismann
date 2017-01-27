using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceCream : MonoBehaviour {


	public enum IceType
	{
		Vanilla,
		Chocolate,
		Strawberry,
	};

	public IceType m_Type = IceType.Vanilla;

	public Material[] m_MaterialType;

	public void SetType (IceType _newType)
	{
		GetComponent<Render> ().sharedMaterial = m_MaterialType [(int)_newType];
		m_Type = _newType;
	}

	private float m_Time=0.0f; //TESTING

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		m_Time += Time.deltaTime;

		if (m_Time < 1.0f) {
			SetType (IceType.Vanilla);
		} else if (m_Time < 2.0f) {
			SetType (IceType.Chocolate);
		} else if (m_Time < 3.0f) {
			SetType (IceType.Strawberry);
		} else if (m_Time > 4.0f) {
			m_Time = 0.0f;
		}
		
	}
}
