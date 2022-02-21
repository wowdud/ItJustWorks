using System;

using UnityEngine;
using UnityEngine.InputSystem;

namespace ItJustWorks.CrossPlatform
{
	public abstract class InputHandler : MonoBehaviour, IInitialiser
	{
		public bool Initialised { get; private set; }
		
		protected PlayerInput Input { get; private set; }
		
		public void Initialise(params object[] _data)
		{
			if(Initialised)
				return;

			Input = (PlayerInput) _data[0];
			
			// Take the passed data and remove the first element
			object[] data = new object[_data.Length - 1];
			Array.Copy(_data, 1, data, 0, data.Length);
			OnInitialise(data);
			
			Initialised = true;
		}

		public abstract void Process();

		protected abstract void OnInitialise(object[] _data);
	}
}