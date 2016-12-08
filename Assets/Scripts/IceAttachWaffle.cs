using UnityEngine;
using System.Collections;

public class IceAttachWaffle : MonoBehaviour {

    //private GameController gameController;
	private gameObject waffle;

	void Start () {

	}
	

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Ice"))
        {
            Debug.Log("Compare Tag: " + other.gameObject.CompareTag(other.tag).ToString());

            Debug.Log("Transform Tag is " + tag);
            waffle.transform.SetParent(other.transform);

            //transform.parent = other.transform;
            //gameController.AddScore(1);

        } /*else if (other.gameObject.CompareTag("Ice"))
             {
            Debug.Log("Compare Tag : ICE " + other.gameObject.CompareTag(other.tag).ToString());

            Debug.Log("Transform Tag is from Ice " + tag);
        }*/
    }
}
