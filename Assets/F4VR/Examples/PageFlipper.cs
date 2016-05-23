using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;

public class PageFlipper : MonoBehaviour {

	[SerializeField] protected RectTransform[] bookPages;
	[SerializeField] protected float maxRotationSpeed;

	protected float headingsDotProduct;
	protected Quaternion toRotation;
	protected RectTransform currentPage;
	protected int indexCurrentPage;
	protected bool isFlipping;


	void Start()
	{
		MovePagesApart ();

		indexCurrentPage = bookPages.Length-1;
		currentPage = bookPages[indexCurrentPage];
	}


	void MovePagesApart ()
	{
		Vector3 pos;

		for (int i = 0; i < bookPages.Length; i++) {
			currentPage = bookPages [i];
			pos = currentPage.localPosition;
			pos.z = -bookPages.Length + (i / currentPage.localScale.z);
			currentPage.localPosition = pos;
		}
	}


	[ContextMenu("Flip to next page")]
	public void Next()
	{
		// Can't flip past the back cover
		if (indexCurrentPage == 0) {
			return;
		}

		currentPage.Rotate (0, -180, 0, Space.Self);
		currentPage.Translate (0, 0, bookPages.Length-indexCurrentPage, Space.Self);
		
		indexCurrentPage--;
		currentPage = bookPages[indexCurrentPage];
	}


	[ContextMenu("Flip back to previous page")]
	public void Back()
	{
		// Can't flip before the front cover
		if (indexCurrentPage == bookPages.Length - 1) {
			return;
		}

		currentPage.Translate (0, 0, -bookPages.Length-indexCurrentPage, Space.Self);
		currentPage.Rotate (0, 180, 0, Space.Self);

		indexCurrentPage++;
		currentPage = bookPages[indexCurrentPage];
	}


	void Update()
	{
		if (!isFlipping) {
			return;
		}

		currentPage.localRotation = Quaternion.RotateTowards(currentPage.rotation, toRotation, maxRotationSpeed * Time.deltaTime);

		headingsDotProduct = Quaternion.Dot (currentPage.rotation, toRotation);
		if (headingsDotProduct == 1f) {  // -1 = parallel but opposite, 1 = parallel and same direction
			isFlipping = false;
		}
	}

}
