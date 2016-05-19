# F4VR

(Yet Another) Framework for VR


Features:
---------

* Uses the SteamVR plugin for Unity
* Uses (lots of) UnityEvents, for more flexibility and friendliness towards "script-challenged" devs
* Works on the HTC Vive. I'd like to make it work with other devices too (at least the Rift and the PlayStation VR) but I don't own any other VR devices and I'm broke now, thanks to my beloved Vive :-)


Instructions:
-------------

###Setup the controllers:

1. Create a new project.
1. Download and Import SteamVR from the Asset Store.
1. Remove the MainCamera object from the hierarchy.
1. Add a [CameraRig] prefab from SteamVR.
1. Download and Import F4VR from the [release folder on GitHub](F4VR/releases).
1. Add the **F4VR_Controller** and **F4VR_ControllerEvents** scripts to both Left and Right controllers of [CameraRig].

UnityEvents are invoked on F4VR_ControllerEvents components when buttons are pressed on the controllers. This is useful for the menu button for example, or for using the touch pad as a Play/Stop button like they do in Fantastic Contraptions.


###Touch objects:

1. Add a cube to the scene.
2. Add the **F4VR_Object** script to the cube.

UnityEvents are invoked on F4VR_Object components when they are touched by the controllers.


###Interact with objects:

1. Add the **F4VR_ObjectEvents** component to the cube.

UnityEvents are invoked on F4VR_ObjectEvents components when controller buttons are pressed **while a F4VR_Controller is touching it**. When the trigger is pulled while touching the object, for example. This is very modular: your controllers don't need to have code about that object on them, nor know about them. The class used to open a door can simply be put on the door itself, etc.


###Grab objects:

1. Add a **F4VR_Grabbable** component to the cube.
1. Set the F4VR_ObjectEvents.Gripped event to invoke the F4VR_Grabbable.Grab() function.
1. Set the F4VR_ObjectEvents.Ungripped event to invoke the F4VR_Grabbable.Release() function.

This will allow players to intuitively pick up and release the cube when squeezing and releasing the grip buttons.

If you set the F4VR_Grabbable.isThrowable boolean to false, momentum is not transfered when the object is let go, so it will just drop straight down towards the floor. It's some kind of "child proofing" feature, to discourage kids (and adults ;-)) from throwing stuff, which helps protect your controllers and walls :-)


TODO:
-----

* More example scenes
* Fix the buggy transfer of momentum when F4VR_Grabbable objects are released
* Add event for when the player directly looks at an object (with optional crosshair?)
* Add a custom inspector to allow folding groups of events
* Add event for when the player does common gestures:
  * Eating
  * Drinking
  * Looking at watch
  * Reaching behind back
  * Military salute
  * Hand waving
  * more
* Add generic scripts for interacting with common objects:
  * Buttons
  * Switches
  * Doors
  * Drawers
  * Simple pistol
* Automatically adding the CapsuleCollider and Rigidbody to the controllers should be optional. How about doing like the SteamVR camera: have Expand and Collapse buttons add the CapsuleCollider and Rigidbody in the editor.
