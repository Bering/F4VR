using UnityEngine;
using UnityEngine.Events;


[RequireComponent(typeof(F4VR_Object))]
public class F4VR_TouchSwipe : MonoBehaviour
{
	Vector2 startPos;
	Vector2 endPos;

	[System.Serializable] public class F4VR_TouchSwipeEventClass : UnityEvent<F4VR_Controller> {}
	public F4VR_TouchSwipeEventClass SwipeUpEvent;
	public F4VR_TouchSwipeEventClass SwipeRightEvent;
	public F4VR_TouchSwipeEventClass SwipeDownEvent;
	public F4VR_TouchSwipeEventClass SwipeLeftEvent;

	// TODO: Again, I'll need a dictionary to allow more than one controller touching this
	F4VR_Controller touchedBy = null;

	public float minimumDistance = 0.6f;
	public bool showDebugMessages = false;


	void Start()
	{
		F4VR_Object o = GetComponent<F4VR_Object> ();
		o.TouchedEvent.AddListener (StartListening);
		o.UntouchedEvent.AddListener (StopListening);
	}


	void Destroy()
	{
		F4VR_Object o = GetComponent<F4VR_Object> ();
		o.TouchedEvent.RemoveListener (StartListening);
		o.UntouchedEvent.RemoveListener (StopListening);
	}


	void StartListening (F4VR_Controller controller)
	{
		F4VR_ControllerEvents e = controller.GetComponent<F4VR_ControllerEvents> ();
		e.PadTouched.AddListener(StartTouch);
		e.PadUnTouched.AddListener(EndTouch);
		touchedBy = controller;
	}


	void StopListening(F4VR_Controller controller)
	{
		touchedBy = null;
		F4VR_ControllerEvents e = controller.GetComponent<F4VR_ControllerEvents> ();
		e.PadTouched.RemoveListener(StartTouch);
		e.PadUnTouched.RemoveListener(EndTouch);
	}


	void StartTouch (F4VR_Controller controller)
	{
		startPos.x = controller.GetComponent<F4VR_ControllerEvents> ().padAxisHorizontal;
		startPos.y = controller.GetComponent<F4VR_ControllerEvents> ().padAxisVertical;
	}


	void Update()
	{
		if (touchedBy == null) {
			return;
		}

		endPos.x = touchedBy.GetComponent<F4VR_ControllerEvents> ().padAxisHorizontal;
		endPos.y = touchedBy.GetComponent<F4VR_ControllerEvents> ().padAxisVertical;
	}


	void EndTouch (F4VR_Controller controller)
	{

		if (showDebugMessages) {
			Debug.Log ("Touch started at " + startPos + " and ended at " + endPos);
		}

		if (Vector2.Distance (endPos, startPos) < minimumDistance) {
			if (showDebugMessages) {
				Debug.Log ("Not swiped long enough to be considered (" + Vector2.Distance (endPos, startPos) + " < " + minimumDistance);
			}
			return;
		}

		if (Mathf.Abs (endPos.y - startPos.y) < Mathf.Abs (endPos.x - startPos.x))  {
			if (endPos.x < startPos.x) {
				if (showDebugMessages) {
					Debug.Log("Swipe left detected.");
				}
				SwipeLeftEvent.Invoke(controller);
			}
			else {
				if (showDebugMessages) {
					Debug.Log ("Swipe right detected.");
				}
				SwipeRightEvent.Invoke(controller);
			}
		}
		else {
			if (endPos.y < startPos.y) {
				if (showDebugMessages) {
					Debug.Log("Swipe down detected.");
				}
				SwipeLeftEvent.Invoke(controller);
			}
			else {
				if (showDebugMessages) {
					Debug.Log ("Swipe up detected.");
				}
				SwipeRightEvent.Invoke(controller);
			}
		}

	}

}

