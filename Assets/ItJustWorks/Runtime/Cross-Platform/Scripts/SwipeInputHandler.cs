using UnityEngine;
using UnityEngine.InputSystem;

using TouchPhase = UnityEngine.TouchPhase;

namespace ItJustWorks.CrossPlatform
{
	public class SwipeInputHandler : InputHandler
	{
		[SerializeField] private InputActionReference touchAction;
		[SerializeField] private InputActionReference mouseTouchAction;
		
		protected override void OnInitialise(object[] _data)
		{
			touchAction.action.Enable();
			mouseTouchAction.action.Enable();
		}
		
		public override void Process()
		{
			Touch touch = touchAction.action.ReadValue<Touch>();
			
			if(touch.phase == TouchPhase.Began)
				Debug.Log(touch.position);
		}
	}
}