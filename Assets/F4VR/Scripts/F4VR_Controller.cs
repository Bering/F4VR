using UnityEngine;


public class F4VR_Controller : MonoBehaviour {


	public bool isTrigger;


	void Start()
	{
		SetupRigidbody ();
		SetupCollider ();
	}


	protected void SetupRigidbody()
	{
		Rigidbody rb = GetComponent<Rigidbody> ();
		if (rb == null) {
			rb = gameObject.AddComponent<Rigidbody> ();
		}

		rb.useGravity = false;
		rb.isKinematic = true;
	}


	protected void SetupCollider()
	{
		CapsuleCollider col = GetComponent<CapsuleCollider> ();
		if (col == null) {
			col = gameObject.AddComponent<CapsuleCollider> ();
		}

		col.isTrigger = isTrigger;
		col.center = new Vector3 (0, -0.04f, 0);
		col.radius = 0.05f;
		col.height = 0.01f;
		col.direction = 2; // Z-Axis
	}


}
