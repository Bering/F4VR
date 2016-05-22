using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody))]
public class F4VR_Grabbable : MonoBehaviour {

	[System.Serializable] public class GrabbableEventClass : UnityEvent<F4VR_Controller> {}
	public GrabbableEventClass GrabbedEvent;
	public GrabbableEventClass ReleasedEvent;

	public bool debugGrabbing;
	public bool debugReleasing;

	public bool isThrowable = false; // Great for kids ;-)

	protected new Rigidbody rigidbody;
	protected FixedJoint joint = null;
	protected bool wasKinematic;
	public float breakForce = 1000f;
	public float breakTorque = 100f;

	// TODO: Make a dictionary of controller/joint to allow holding something with both hands (and to allow switching hands)


	void Start()
	{
		rigidbody = GetComponent<Rigidbody> ();
		if (rigidbody == null) {
			Debug.LogError ("VR_Grabbable needs a rigidbody...");
			Destroy (this);
		}
	}


	public void ToggleGrab(F4VR_Controller controller)
	{
		if (joint == null) {
			Grab (controller);
			return;
		}
		else {
			Release (controller);
			return;
		}
	}


	public void Grab(F4VR_Controller controller)
	{
		// TODO: Make a dictionary of controller/joint to allow holding something with both hands (and to allow switching hands)
		if (joint != null) {
			if (debugGrabbing) {
				Debug.Log (gameObject.name + " grabbed by " + controller.gameObject.name + " but it is already grabbed by " + joint.gameObject.name + ".");
			}
			return;
		}

		joint = gameObject.AddComponent<FixedJoint>();
		joint.connectedBody = controller.GetComponent<Rigidbody>();
		joint.breakForce = breakForce;
		joint.breakTorque = breakTorque;

		wasKinematic = rigidbody.isKinematic;
//		rigidbody.isKinematic = true;

		GrabbedEvent.Invoke (controller);

		if (debugGrabbing) {
			Debug.Log (gameObject.name + " grabbed by " + controller.gameObject.name);
		}
	}


	public void Release(F4VR_Controller controller)
	{
		if (joint == null) {
			if (debugReleasing) {
				Debug.Log (gameObject.name + " released by " + controller.gameObject.name + " but it was not grabbed.");
			}
			return;
		}

		DestroyImmediate (joint);
		joint = null;

		if (isThrowable) {
			SteamVR_Controller.Device device = SteamVR_Controller.Input ((int)(controller.GetComponent<SteamVR_TrackedObject> ().index));
			Transform origin = controller.transform.parent;

			rigidbody.velocity = origin.TransformVector(device.velocity);
			rigidbody.angularVelocity = origin.TransformVector(device.angularVelocity);
			rigidbody.maxAngularVelocity = rigidbody.angularVelocity.magnitude;
		}
		//rigidbody.isKinematic = wasKinematic;

		ReleasedEvent.Invoke (controller);

		if (debugReleasing) {
			Debug.Log (gameObject.name + " released by " + controller.gameObject.name);
		}
	}

}
