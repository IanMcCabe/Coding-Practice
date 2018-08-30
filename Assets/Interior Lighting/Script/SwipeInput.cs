using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SwipeInput : MonoBehaviour
{
	public bool detectSwipeOnlyAfterRelease = false;
	public float SWIPE_THRESHOLD = 20f;


	public UnityEvent SwipeUpEvent;
	public UnityEvent SwipeDownEvent;
	public UnityEvent SwipeLeftEvent;
	public UnityEvent SwipeRightEvent;

	private Vector2 fingerDown;
	private Vector2 fingerUp;

	private float verticalDelta;
	private float horizontalDelta;

	// Update is called once per frame
	void Update()
	{
		foreach (Touch touch in Input.touches)
		{
			if (touch.phase == TouchPhase.Began)
			{
				fingerUp = touch.position;
				fingerDown = touch.position;
			}

			//Detects Swipe while finger is still moving
			if (touch.phase == TouchPhase.Moved)
			{
				if (!detectSwipeOnlyAfterRelease)
				{
					fingerDown = touch.position;
					CheckSwipe(fingerDown, fingerUp);
				}
			}

			//Detects swipe after finger is released
			if (touch.phase == TouchPhase.Ended)
			{
				fingerDown = touch.position;
				CheckSwipe(fingerDown, fingerUp);
			}
		}
	}

	protected void CheckSwipe(Vector2 fingerDown, Vector2 fingerUp)
	{
		// get the touch deltas.
		verticalDelta = GetVerticalDelta();
		horizontalDelta = GetHorizontalDelta();

		//Check if Vertical swipe
		if (verticalDelta > SWIPE_THRESHOLD && verticalDelta > horizontalDelta)
		{
			//Debug.Log("Vertical");
			if (fingerDown.y - fingerUp.y > 0)//up swipe
			{
				OnSwipeUp();
			}
			else if (fingerDown.y - fingerUp.y < 0)//Down swipe
			{
				OnSwipeDown();
			}
			fingerUp = fingerDown;
		}

		//Check if Horizontal swipe
		else if (horizontalDelta > SWIPE_THRESHOLD && horizontalDelta > verticalDelta)
		{
			//Debug.Log("Horizontal");
			if (fingerDown.x - fingerUp.x > 0)//Right swipe
			{
				OnSwipeRight();
			}
			else if (fingerDown.x - fingerUp.x < 0)//Left swipe
			{
				OnSwipeLeft();
			}
			fingerUp = fingerDown;
		}

		//No Movement at-all
		else
		{
			//Debug.Log("No Swipe!");
		}
	}

	float GetVerticalDelta()
	{
		return Mathf.Abs(fingerDown.y - fingerUp.y);
	}

	float GetHorizontalDelta()
	{
		return Mathf.Abs(fingerDown.x - fingerUp.x);
	}
	
	void OnSwipeUp()
	{
		Debug.Log("Swipe UP");
		SwipeUpEvent.Invoke();
	}

	void OnSwipeDown()
	{
		Debug.Log("Swipe Down");
		SwipeDownEvent.Invoke();
	}

	void OnSwipeLeft()
	{
		Debug.Log("Swipe Left");
		SwipeLeftEvent.Invoke();
	}

	void OnSwipeRight()
	{
		Debug.Log("Swipe Right");
		SwipeRightEvent.Invoke();
	}
}
