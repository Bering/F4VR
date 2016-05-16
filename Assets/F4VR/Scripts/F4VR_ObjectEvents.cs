using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(F4VR_Object))]
public class F4VR_ObjectEvents : MonoBehaviour {

	[System.Serializable] public class VR_ObjectEvents_EventClass : UnityEvent<F4VR_Controller> {}
	public VR_ObjectEvents_EventClass TriggerClicked;
	public VR_ObjectEvents_EventClass TriggerUnclicked;
	public VR_ObjectEvents_EventClass Gripped;
	public VR_ObjectEvents_EventClass Ungripped;
	public VR_ObjectEvents_EventClass MenuButtonClicked;
	public VR_ObjectEvents_EventClass MenuButtonUnclicked;
	public VR_ObjectEvents_EventClass PadClicked;
	public VR_ObjectEvents_EventClass PadUnclicked;
	public VR_ObjectEvents_EventClass PadTouched;
	public VR_ObjectEvents_EventClass PadUntouched;


	void Start()
	{
		F4VR_Object o = GetComponent<F4VR_Object> ();
		o.TouchedEvent.AddListener (ListenToControllerEvents);
		o.UntouchedEvent.AddListener (StopListeningToControllerEvents);
	}


	void Destroy()
	{
		F4VR_Object o = GetComponent<F4VR_Object> ();
		o.TouchedEvent.RemoveListener (ListenToControllerEvents);
		o.UntouchedEvent.RemoveListener (StopListeningToControllerEvents);
	}


	void ListenToControllerEvents(F4VR_Controller controller)
	{
		F4VR_ControllerEvents e = controller.GetComponent<F4VR_ControllerEvents> ();
		e.TriggerClicked.AddListener (TriggerClicked.Invoke);
		e.TriggerUnclicked.AddListener (TriggerUnclicked.Invoke);
		e.Gripped.AddListener (Gripped.Invoke);
		e.Ungripped.AddListener (Ungripped.Invoke);
		e.MenuButtonClicked.AddListener (MenuButtonClicked.Invoke);
		e.MenuButtonUnclicked.AddListener (MenuButtonUnclicked.Invoke);
		e.PadClicked.AddListener (PadClicked.Invoke);
		e.PadUnclicked.AddListener (PadUnclicked.Invoke);
		e.PadTouched.AddListener (PadTouched.Invoke);
		e.PadUntouched.AddListener (PadUntouched.Invoke);
	}


	void StopListeningToControllerEvents(F4VR_Controller controller)
	{
		F4VR_ControllerEvents e = controller.GetComponent<F4VR_ControllerEvents> ();
		e.TriggerClicked.RemoveListener (TriggerClicked.Invoke);
		e.TriggerUnclicked.RemoveListener (TriggerUnclicked.Invoke);
		e.Gripped.RemoveListener (Gripped.Invoke);
		e.Ungripped.RemoveListener (Ungripped.Invoke);
		e.MenuButtonClicked.RemoveListener (MenuButtonClicked.Invoke);
		e.MenuButtonUnclicked.RemoveListener (MenuButtonUnclicked.Invoke);
		e.PadClicked.RemoveListener (PadClicked.Invoke);
		e.PadUnclicked.RemoveListener (PadUnclicked.Invoke);
		e.PadTouched.RemoveListener (PadTouched.Invoke);
		e.PadUntouched.RemoveListener (PadUntouched.Invoke);
	}

}
