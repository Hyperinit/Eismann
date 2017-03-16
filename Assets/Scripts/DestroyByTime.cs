using UnityEngine;
using System.Collections;

// if attached to an GamObject, it will self detroy after lifetime
public class DestroyByTime : MonoBehaviour {
    public float lifetime;

	void Start () {
        Destroy(gameObject,lifetime);
	}
	
}
