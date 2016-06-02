using UnityEngine;
using System.Collections;

public class F4VR_PlayArea : MonoBehaviour
{
	static protected Vector3[] playArea = new Vector3[4];


	static public Vector3[] GetBounds()
	{
		playArea = new Vector3[4];
		Valve.VR.HmdQuad_t steamVRPlayArea = new Valve.VR.HmdQuad_t ();
		SteamVR_PlayArea.GetBounds (SteamVR_PlayArea.Size.Calibrated, ref steamVRPlayArea);

		playArea [0].x = steamVRPlayArea.vCorners0.v0;
		playArea [0].y = steamVRPlayArea.vCorners0.v1;
		playArea [0].z = steamVRPlayArea.vCorners0.v2;
		playArea [1].x = steamVRPlayArea.vCorners1.v0;
		playArea [1].y = steamVRPlayArea.vCorners1.v1;
		playArea [1].z = steamVRPlayArea.vCorners1.v2;
		playArea [2].x = steamVRPlayArea.vCorners2.v0;
		playArea [2].y = steamVRPlayArea.vCorners2.v1;
		playArea [2].z = steamVRPlayArea.vCorners2.v2;
		playArea [3].x = steamVRPlayArea.vCorners3.v0;
		playArea [3].y = steamVRPlayArea.vCorners3.v1;
		playArea [3].z = steamVRPlayArea.vCorners3.v2;

		return playArea;
	}

}

