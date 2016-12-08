using UnityEngine;
using System.Collections;

public class IceAttachIce : MonoBehaviour
{

    void Start()
    {
        
    }


    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Ice"))
        {
            transform.SetParent(other.transform);

        }
    }
}
