using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceofWaffle : MonoBehaviour 
{
	// IceCream Type
	public enum IceType
	{
		Vanilla = 1,
		Chocolate =2,
		Strawberry = 0,
	};

	public IceType m_Type = IceType.Vanilla;
}
