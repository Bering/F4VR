using UnityEngine;
using UnityEngine.Events;


[RequireComponent(typeof(F4VR_Controller))]
[RequireComponent(typeof(SteamVR_TrackedObject))]
public class F4VR_ControllerEvents : MonoBehaviour
{
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

	public float triggerDeadZone = 0.1f;
	public float padAxisHorizontal;
	public float padAxisVertical;
	public float triggerAxis;

	[System.Serializable] public class VR_ControllerEvents_EventClass : UnityEvent<F4VR_Controller> {}
	public VR_ControllerEvents_EventClass TriggerPulled;
	public VR_ControllerEvents_EventClass TriggerUnPulled;
	public VR_ControllerEvents_EventClass TriggerClicked;
	public VR_ControllerEvents_EventClass TriggerUnClicked;
	public VR_ControllerEvents_EventClass Gripped;
	public VR_ControllerEvents_EventClass UnGripped;
	public VR_ControllerEvents_EventClass MenuButtonClicked;
	public VR_ControllerEvents_EventClass MenuButtonUnClicked;
	public VR_ControllerEvents_EventClass PadClicked;
	public VR_ControllerEvents_EventClass PadUnClicked;
	public VR_ControllerEvents_EventClass PadClickedNorth;
	public VR_ControllerEvents_EventClass PadUnClickedNorth;
	public VR_ControllerEvents_EventClass PadClickedEast;
	public VR_ControllerEvents_EventClass PadUnClickedEast;
	public VR_ControllerEvents_EventClass PadClickedSouth;
	public VR_ControllerEvents_EventClass PadUnClickedSouth;
	public VR_ControllerEvents_EventClass PadClickedWest;
	public VR_ControllerEvents_EventClass PadUnClickedWest;
	public VR_ControllerEvents_EventClass PadTouched;
	public VR_ControllerEvents_EventClass PadUnTouched;

	private SteamVR_Controller.Device controller_SteamVR;
	private F4VR_Controller controller;
	private bool currentlyPressed;
	private Vector2 v2;


	void Start()
	{
		// TODO: Is it true that index can change at runtime?
		SteamVR_TrackedObject.EIndex controllerIndex = GetComponent<SteamVR_TrackedObject> ().index;
		if (controllerIndex == 0)
		{
			Debug.LogError ("F4VR_ControllerEvents doesn't watch for events on the HMD... Use F4VR_HmdEvents instead.");
			Destroy (this);
			return;
		}

		controller_SteamVR = SteamVR_Controller.Input((int)controllerIndex);

		controller = GetComponent<F4VR_Controller> ();
	}


	void Update()
	{
		v2 = controller_SteamVR.GetAxis(Valve.VR.EVRButtonId.k_EButton_Axis0);
		padAxisHorizontal = v2.x;
		padAxisVertical = v2.y;
		triggerAxis = controller_SteamVR.GetAxis(Valve.VR.EVRButtonId.k_EButton_Axis1).x;

		currentlyPressed = triggerAxis > triggerDeadZone;
		if (currentlyPressed && !isTriggerPulled) {
			isTriggerPulled = true;
			TriggerPulled.Invoke (controller);
		}
		else if(!currentlyPressed && isTriggerPulled) {
			isTriggerPulled = false;
			TriggerUnPulled.Invoke (controller);
		}

		currentlyPressed = (triggerAxis == 1f);
		if (currentlyPressed && !isTriggerClicked) {
			isTriggerClicked = true;
			TriggerClicked.Invoke (controller);
		}
		else if(!currentlyPressed && isTriggerClicked) {
			isTriggerClicked = false;
			TriggerUnClicked.Invoke (controller);
		}

		currentlyPressed = controller_SteamVR.GetPress (SteamVR_Controller.ButtonMask.Grip);
		if (currentlyPressed && !isGripped) {
			isGripped = true;
			Gripped.Invoke (controller);
		}
		else if(!currentlyPressed && isGripped) {
			isGripped = false;
			UnGripped.Invoke (controller);
		}

		currentlyPressed = controller_SteamVR.GetPress (SteamVR_Controller.ButtonMask.ApplicationMenu);
		if (currentlyPressed && !isMenuPressed) {
			isMenuPressed = true;
			MenuButtonClicked.Invoke (controller);
		}
		else if(!currentlyPressed && isMenuPressed) {
			isMenuPressed = false;
			MenuButtonUnClicked.Invoke (controller);
		}

		currentlyPressed = controller_SteamVR.GetPress (SteamVR_Controller.ButtonMask.Touchpad);
		if (currentlyPressed && !isPadClicked) {
			isPadClicked = true;
			PadClicked.Invoke (controller);
		}
		else if(!currentlyPressed && isPadClicked) {
			isPadClicked = false;
			PadUnClicked.Invoke (controller);
		}

		if (isPadClicked && (padAxisVertical > 0.3f) && !isPadClickedNorth) {
			isPadClickedNorth = true;
			PadClickedNorth.Invoke (controller);
		} else if (!isPadClicked || !(padAxisVertical > 0.3f) && isPadClickedNorth) {
			isPadClickedNorth = false;
			PadUnClickedNorth.Invoke (controller);
		}

		if (isPadClicked && (padAxisHorizontal > 0.3f) && !isPadClickedEast) {
			isPadClickedEast = true;
			PadClickedEast.Invoke (controller);
		} else if (!isPadClicked || !(padAxisHorizontal > 0.3f) && isPadClickedEast) {
			isPadClickedEast = false;
			PadUnClickedEast.Invoke (controller);
		}

		if (isPadClicked && (padAxisVertical < -0.3f) && !isPadClickedSouth) {
			isPadClickedSouth = true;
			PadClickedSouth.Invoke (controller);
		} else if (!isPadClicked || !(padAxisVertical < -0.3f) && isPadClickedSouth) {
			isPadClickedSouth = false;
			PadUnClickedSouth.Invoke (controller);
		}

		if (isPadClicked && (padAxisHorizontal < -0.3f) && !isPadClickedWest) {
			isPadClickedWest = true;
			PadClickedWest.Invoke (controller);
		} else if (!isPadClicked || !(padAxisHorizontal < -0.3f) && isPadClickedWest) {
			isPadClickedWest = false;
			PadUnClickedWest.Invoke (controller);
		}

		currentlyPressed = controller_SteamVR.GetTouch (SteamVR_Controller.ButtonMask.Touchpad);
		if (currentlyPressed && !isPadTouched) {
			isPadTouched= true;
			PadTouched.Invoke (controller);
		}
		else if(!currentlyPressed && isPadTouched) {
			isPadTouched= false;
			PadUnTouched.Invoke (controller);
		}

	}


}

