using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceofWaffle : MonoBehaviour 
{
	// IceCream Type
	public enum IceType
	{
		Vanilla = 0,
		Chocolate =1,
		Strawberry = 2,
	};

	public IceType m_Type = IceType.Vanilla;
}
