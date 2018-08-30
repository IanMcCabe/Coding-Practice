using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.Events;

[Serializable]
public class KeyCodeInput
{
	public KeyCode keycode;
	public KeyTypeEnum keyTypeEnum;
	public UnityEvent unityEvent;

	public enum KeyTypeEnum
	{
		keyUp,
		keyDown,
		key,
	}

	public void CheckInput()
	{
		if (keyTypeEnum == KeyTypeEnum.key)
		{
			if (Input.GetKey(keycode))
			{
				unityEvent.Invoke();
			}
		}
		else if (keyTypeEnum == KeyTypeEnum.keyDown)
		{
			if (Input.GetKeyDown(keycode))
			{
				unityEvent.Invoke();
			}
		}
		else if (keyTypeEnum == KeyTypeEnum.keyUp)
		{
			if (Input.GetKeyUp(keycode))
			{
				unityEvent.Invoke();
			}
		}
	}
}

public class KeyInput : MonoBehaviour
{
	public KeyCodeInput[] keyCodeInputs;
	
	void Update ()
	{
		for (int i = 0; i < keyCodeInputs.Length; i++)
		{
			keyCodeInputs[i].CheckInput();
		}
	}
}
