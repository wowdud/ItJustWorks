﻿using UnityEngine;
using UnityEngine.InputSystem;

namespace ItJustWorks.CrossPlatform
{
	public class CrossPlatformInput : Singleton<CrossPlatformInput>
	{
		[RuntimeInitializeOnLoadMethod]
		public static void OnRuntimeInitialise()
		{
			if(!IsValid() && FindObjectOfType<CrossPlatformInput>() == null)
			{
				GameObject newInput = Instantiate(Resources.Load<CrossPlatformInput>("Cross-Platform Input")).gameObject;
				newInput.name = newInput.name.Replace("(Clone)", "");
				DontDestroyOnLoad(newInput);
			}
		}

		public static Vector2 GetMovementAxis() => Instance.joystick != null ? Instance.joystick.Axis : Vector2.zero;

		[SerializeField] private PlayerInput input;
		[SerializeField] private MobileJoystickHandler joystick;

		private void Start()
		{
			joystick.Initialise(input);
		}
	}
}