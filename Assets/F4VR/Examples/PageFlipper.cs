using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using System;

public class PageFlipper : MonoBehaviour {

	[SerializeField] protected Transform[] bookSheets;
	[SerializeField] protected float maxRotationSpeed;
	[SerializeField] protected float closeAngle = 0f;
	[SerializeField] protected float openAngle = 179.999f;

	protected Transform currentSheet;
	protected int indexCurrentSheet;

	protected bool isFlipping;
	protected Vector3 hinge;

	[SerializeField] protected float maxDelta;
	[SerializeField] protected float toAngle;


	void Start()
	{
		PlaceSheets ();

		indexCurrentSheet = bookSheets.Length;
		currentSheet = bookSheets[bookSheets.Length-1];
	}


	void PlaceSheets ()
	{
		Vector3 pos;

		for (int i = 0; i < bookSheets.Length; i++) {
			currentSheet = bookSheets [i];
			pos = currentSheet.localPosition;
			pos.z = -i * currentSheet.localScale.z;
			currentSheet.localPosition = pos;
		}
	}


	[ContextMenu("Flip to next page")]
	public void Next()
	{
		if (isFlipping) {
			return;
		}

		// Can't flip the back cover
		if (indexCurrentSheet <= 0) {
			return;
		}

		maxDelta = maxRotationSpeed;
		toAngle = openAngle;

		isFlipping = true;
	}


	[ContextMenu("Flip back to previous page")]
	public void Back()
	{
		if (isFlipping) {
			return;
		}

		// Can't flip before the front cover
		if (indexCurrentSheet >= bookSheets.Length -1) {
			return;
		}

		indexCurrentSheet++;
		currentSheet = bookSheets [indexCurrentSheet];

		maxDelta = maxRotationSpeed;
		toAngle = closeAngle;

		isFlipping = true;
	}


	void Update()
	{
		if (!isFlipping) {
			return;
		}

		currentSheet.localRotation = Quaternion.RotateTowards(currentSheet.localRotation, Quaternion.Euler (0, toAngle, 0), maxDelta);

		if (Mathf.Approximately (currentSheet.localRotation.eulerAngles.y, toAngle)) {
			if (toAngle == openAngle) {
				indexCurrentSheet--;
				currentSheet = bookSheets [indexCurrentSheet];
			}
			isFlipping = false;
		}
	}

}
