using UnityEngine;
using UnityEngine.Events;


public class F4VR_Object : MonoBehaviour {

	[System.Serializable] public class VR_Object_EventClass : UnityEvent<F4VR_Controller> {}

	public VR_Object_EventClass TouchedEvent;
	public VR_Object_EventClass UntouchedEvent;

	public bool showDebugMessages;


	void Start()
	{
		// TODO: Eh... are collider events bubbling up or not?
		if (showDebugMessages && GetComponents<Collider> ().Length == 0) {
			Debug.Log ("F4VR_Object on " + gameObject.name + " needs at least one collider.");
		}
	}


	// TODO: make sure it also works with triggers
	void OnTriggerEnter(Collider other)
	{
		SendTouchedEvent (other.GetComponent<F4VR_Controller> ());
	}


	void OnCollisionEnter(Collision col)
	{
		SendTouchedEvent (col.gameObject.GetComponent<F4VR_Controller> ());
	}


	void OnTriggerExit(Collider other)
	{
		SendUntouchedEvent (other.GetComponent<F4VR_Controller> ());
	}


	void OnCollisionExit(Collision col)
	{
		SendUntouchedEvent (col.gameObject.GetComponent<F4VR_Controller> ());
	}


	void SendTouchedEvent(F4VR_Controller controller)
	{
		if (controller == null) {
			if (showDebugMessages == true) {
				Debug.Log (gameObject.name + " is being touched by something that is not a VR_Controller");
			}
			return;
		}

		if (showDebugMessages == true) {
			Debug.Log (gameObject.name + " is being touched by " + controller.gameObject.name + ".");
		}
		TouchedEvent.Invoke (controller);
	}


	void SendUntouchedEvent(F4VR_Controller controller)
	{
		if (controller == null) {
			if (showDebugMessages == true) {
				Debug.Log (gameObject.name + " is not touched anymore by something that is not a VR_Controller.");
			}
			return;
		}

		if (showDebugMessages == true) {
			Debug.Log (gameObject.name + " is not touched by " + controller.gameObject.name + " anymore.");
		}
		UntouchedEvent.Invoke (controller);
	}
}
