using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceofWaffle : MonoBehaviour 
{
	// IceCream Type
	public enum IceType
	{
		Vanilla,
		Chocolate,
		Strawberry,
	};

	public IceType m_Type = IceType.Vanilla;
}
