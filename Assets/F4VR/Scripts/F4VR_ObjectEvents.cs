using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(F4VR_Object))]
public class F4VR_ObjectEvents : MonoBehaviour {

	public bool isTriggerPulled = false;
	public bool isTriggerClicked = false;
	public bool isGripped = false;
	public bool isMenuPressed = false;
	public bool isPadClicked = false;
	public bool isPadClickedNorth = false;
	public bool isPadClickedEast = false;
	public bool isPadClickedSouth = false;
	public bool isPadClickedWest = false;
	public bool isPadTouched = false;

	[System.Serializable] public class VR_ObjectEvents_EventClass : UnityEvent<F4VR_Controller> {}
	public VR_ObjectEvents_EventClass TriggerPulled;
	public VR_ObjectEvents_EventClass TriggerUnPulled;
	public VR_ObjectEvents_EventClass TriggerClicked;
	public VR_ObjectEvents_EventClass TriggerUnClicked;
	public VR_ObjectEvents_EventClass Gripped;
	public VR_ObjectEvents_EventClass UnGripped;
	public VR_ObjectEvents_EventClass MenuButtonClicked;
	public VR_ObjectEvents_EventClass MenuButtonUnClicked;
	public VR_ObjectEvents_EventClass PadClicked;
	public VR_ObjectEvents_EventClass PadUnClicked;
	public VR_ObjectEvents_EventClass PadClickedNorth;
	public VR_ObjectEvents_EventClass PadUnClickedNorth;
	public VR_ObjectEvents_EventClass PadClickedEast;
	public VR_ObjectEvents_EventClass PadUnClickedEast;
	public VR_ObjectEvents_EventClass PadClickedSouth;
	public VR_ObjectEvents_EventClass PadUnClickedSouth;
	public VR_ObjectEvents_EventClass PadClickedWest;
	public VR_ObjectEvents_EventClass PadUnClickedWest;
	public VR_ObjectEvents_EventClass PadTouched;
	public VR_ObjectEvents_EventClass PadUnTouched;


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

		isTriggerPulled = e.isTriggerPulled;
		isTriggerClicked = e.isTriggerClicked;
		isGripped = e.isGripped;
		isMenuPressed = e.isMenuPressed;
		isPadClicked = e.isPadClicked;
		isPadClickedNorth = e.isPadClickedNorth;
		isPadClickedEast = e.isPadClickedEast;
		isPadClickedSouth = e.isPadClickedSouth;
		isPadClickedWest = e.isPadClickedWest;
		isPadTouched = e.isPadTouched;

		e.TriggerPulled.AddListener (InvokeTriggerPulled);
		e.TriggerUnPulled.AddListener (InvokeTriggerUnPulled);
		e.TriggerClicked.AddListener (InvokeTriggerClicked);
		e.TriggerUnClicked.AddListener (InvokeTriggerUnClicked);
		e.Gripped.AddListener (InvokeGripped);
		e.UnGripped.AddListener (InvokeUnGripped);
		e.MenuButtonClicked.AddListener (InvokeMenuButtonClicked);
		e.MenuButtonUnClicked.AddListener (InvokeMenuButtonUnClicked);
		e.PadClicked.AddListener (InvokePadClicked);
		e.PadUnClicked.AddListener (InvokePadUnClicked);
		e.PadClickedNorth.AddListener (InvokePadClickedNorth);
		e.PadUnClickedNorth.AddListener (InvokePadUnClickedNorth);
		e.PadClickedEast.AddListener (InvokePadClickedEast);
		e.PadUnClickedEast.AddListener (InvokePadUnClickedEast);
		e.PadClickedSouth.AddListener (InvokePadClickedSouth);
		e.PadUnClickedSouth.AddListener (InvokePadUnClickedSouth);
		e.PadClickedWest.AddListener (InvokePadClickedWest);
		e.PadUnClickedWest.AddListener (InvokePadUnClickedWest);
		e.PadTouched.AddListener (InvokePadTouched);
		e.PadUnTouched.AddListener (InvokePadUnTouched);
	}

	void StopListeningToControllerEvents(F4VR_Controller controller)
	{
		F4VR_ControllerEvents e = controller.GetComponent<F4VR_ControllerEvents> ();

		isTriggerPulled = false;
		isTriggerClicked = false;
		isGripped = false;
		isMenuPressed = false;
		isPadClicked = false;
		isPadClickedNorth = false;
		isPadClickedEast = false;
		isPadClickedSouth = false;
		isPadClickedWest = false;
		isPadTouched = false;

		e.TriggerPulled.RemoveListener (InvokeTriggerPulled);
		e.TriggerUnPulled.RemoveListener (InvokeTriggerUnPulled);
		e.TriggerClicked.RemoveListener (InvokeTriggerClicked);
		e.TriggerUnClicked.RemoveListener (InvokeTriggerUnClicked);
		e.Gripped.RemoveListener (InvokeGripped);
		e.UnGripped.RemoveListener (InvokeUnGripped);
		e.MenuButtonClicked.RemoveListener (InvokeMenuButtonClicked);
		e.MenuButtonUnClicked.RemoveListener (InvokeMenuButtonUnClicked);
		e.PadClicked.RemoveListener (InvokePadClicked);
		e.PadUnClicked.RemoveListener (InvokePadUnClicked);
		e.PadClickedNorth.RemoveListener (InvokePadClickedNorth);
		e.PadUnClickedNorth.RemoveListener (InvokePadUnClickedNorth);
		e.PadClickedEast.RemoveListener (InvokePadClickedEast);
		e.PadUnClickedEast.RemoveListener (InvokePadUnClickedEast);
		e.PadClickedSouth.RemoveListener (InvokePadClickedSouth);
		e.PadUnClickedSouth.RemoveListener (InvokePadUnClickedSouth);
		e.PadClickedWest.RemoveListener (InvokePadClickedWest);
		e.PadUnClickedWest.RemoveListener (InvokePadUnClickedWest);
		e.PadTouched.RemoveListener (InvokePadTouched);
		e.PadUnTouched.RemoveListener (InvokePadUnTouched);
	}


	void InvokeTriggerPulled(F4VR_Controller controller)
	{
		isTriggerPulled = true;
		TriggerPulled.Invoke (controller);
	}

	void InvokeTriggerUnPulled(F4VR_Controller controller)
	{
		isTriggerPulled = false;
		TriggerUnPulled.Invoke (controller);
	}


	void InvokeTriggerClicked(F4VR_Controller controller)
	{
		isTriggerClicked = true;
		TriggerClicked.Invoke (controller);
	}

	void InvokeTriggerUnClicked(F4VR_Controller controller)
	{
		isTriggerClicked = false;
		TriggerUnClicked.Invoke (controller);
	}


	void InvokeGripped(F4VR_Controller controller)
	{
		isGripped = true;
		Gripped.Invoke (controller);
	}

	void InvokeUnGripped(F4VR_Controller controller)
	{
		isGripped = false;
		UnGripped.Invoke (controller);
	}


	void InvokeMenuButtonClicked(F4VR_Controller controller)
	{
		isMenuPressed = true;
		MenuButtonClicked.Invoke (controller);
	}

	void InvokeMenuButtonUnClicked(F4VR_Controller controller)
	{
		isMenuPressed = false;
		MenuButtonUnClicked.Invoke (controller);
	}


	void InvokePadClicked(F4VR_Controller controller)
	{
		isPadClicked = true;
		PadClicked.Invoke (controller);
	}

	void InvokePadUnClicked(F4VR_Controller controller)
	{
		isPadClicked = false;
		PadUnClicked.Invoke (controller);
	}


	void InvokePadClickedNorth(F4VR_Controller controller)
	{
		isPadClickedNorth = true;
		PadClickedNorth.Invoke (controller);
	}

	void InvokePadUnClickedNorth(F4VR_Controller controller)
	{
		isPadClickedNorth = false;
		PadUnClickedNorth.Invoke (controller);
	}


	void InvokePadClickedEast(F4VR_Controller controller)
	{
		isPadClickedEast = true;
		PadClickedEast.Invoke (controller);
	}

	void InvokePadUnClickedEast(F4VR_Controller controller)
	{
		isPadClickedEast = false;
		PadUnClickedEast.Invoke (controller);
	}


	void InvokePadClickedSouth(F4VR_Controller controller)
	{
		isPadClickedSouth = true;
		PadClickedSouth.Invoke (controller);
	}

	void InvokePadUnClickedSouth(F4VR_Controller controller)
	{
		isPadClickedSouth = false;
		PadUnClickedSouth.Invoke (controller);
	}

	void InvokePadClickedWest(F4VR_Controller controller)
	{
		isPadClickedWest = true;
		PadClickedWest.Invoke (controller);
	}
	void InvokePadUnClickedWest(F4VR_Controller controller)
	{
		isPadClickedWest = false;
		PadUnClickedWest.Invoke (controller);
	}


	void InvokePadTouched(F4VR_Controller controller)
	{
		isPadTouched = true;
		PadTouched.Invoke (controller);
	}

	void InvokePadUnTouched(F4VR_Controller controller)
	{
		isPadTouched = false;
		PadUnTouched.Invoke (controller);
	}

}
