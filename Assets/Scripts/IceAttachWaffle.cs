using UnityEngine;
using System.Collections;

public class IceAttachWaffle : MonoBehaviour {

	//private GameController gameController;


	void Start () {

	}


	void OnTriggerEnter(Collider other)
	{
		Debug.Log("Transform Tag is " + tag);

		NewtonVR.NVRInteractableItem ItemScript = other.GetComponent<NewtonVR.NVRInteractableItem>();

		if (ItemScript != null)
		{
			ItemScript.ForceDetach(); // release the ice from hands

			ItemScript.StopAllCoroutines();

			ItemScript.enabled = false; // stop NVRInteractableItem script

			Destroy(other.gameObject.GetComponent<Rigidbody>()); // Destroy the rigidbody of the ice

			other.gameObject.GetComponent<SphereCollider>().enabled = false; // does not matter
		}

		if (other.gameObject.CompareTag("Ice"))
		{
			Debug.Log("Compare Tag: " + other.gameObject.CompareTag(other.tag).ToString());

			Debug.Log("Transform Tag is " + tag);
			other.gameObject.transform.SetParent(transform);

			//transform.parent = other.transform;
			//gameController.AddScore(1);

		} else if (other.gameObject.CompareTag("Ice"))
		{
			Debug.Log("Compare Tag : ICE " + other.gameObject.CompareTag(other.tag).ToString());

			Debug.Log("Transform Tag is from Ice " + tag);
		}
	} 

	/* private void OnCollisionEnter(Collision collision)
    {

    

     if (collision.gameObject.CompareTag("Ice"))
        {
            Debug.Log("Compare Tag: " + collision.gameObject.CompareTag(collision.gameObject.tag).ToString());

            Debug.Log("Transform Tag is " + tag);
            transform.SetParent(collision.transform);

            //transform.parent = other.transform;
            //gameController.AddScore(1);

        } /*else if (other.gameObject.CompareTag("Ice"))
             {
            Debug.Log("Compare Tag : ICE " + other.gameObject.CompareTag(other.tag).ToString());

            Debug.Log("Transform Tag is from Ice " + tag);
        }
    }*/
}
