using System.Collections.Generic;

using UnityEngine;
using UnityEngine.InputSystem;

using TouchPhase = UnityEngine.TouchPhase;

namespace ItJustWorks.CrossPlatform
{
	public class SwipeInputHandler : InputHandler
	{
		public class Swipe
		{
			public readonly Queue<Vector2> positions = new Queue<Vector2>();
			public readonly Vector2 startPos;
			public readonly int fingerId;

			public Swipe(Vector2 _startPos, int _fingerId)
			{
				startPos = _startPos;
				fingerId = _fingerId;
			}
		}
		
		public Swipe CurrentSwipe { get; private set; }
		
		[SerializeField] private InputActionReference touchAction;
		[SerializeField] private InputActionReference mouseTouchAction;
		[SerializeField] private InputActionReference mousePosAction;
		
		protected override void OnInitialise(object[] _data)
		{
			touchAction.action.Enable();
			mouseTouchAction.action.Enable();
			mousePosAction.action.Enable();
		}
		
		public override void Process()
		{
			switch(Input.currentControlScheme)
			{
				case "Touch": ProcessTouchControls(); break;
				case "Keyboard&Mouse": ProcessMouseControls(); break;
			}
		}

		private void ProcessTouchControls()
		{
			Touch touch = touchAction.action.ReadValue<Touch>();
			if(CurrentSwipe == null && touch.phase == TouchPhase.Began)
				CurrentSwipe = new Swipe(touch.position, touch.fingerId);

			if(CurrentSwipe != null)
			{
				if(touch.phase == TouchPhase.Moved)
				{
					CurrentSwipe.positions.Enqueue(touch.position);

					if(CurrentSwipe.positions.Count > 20)
						CurrentSwipe.positions.Dequeue();
				}
				else if(touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled)
				{
					CurrentSwipe = null;
				}
			}
		}

		private void ProcessMouseControls()
		{
			bool mouseDown = Mathf.Approximately(mouseTouchAction.action.ReadValue<float>(), 1f);
			// ReSharper disable once ConvertIfStatementToSwitchExpression
			if(mouseDown && CurrentSwipe == null)
				CurrentSwipe = new Swipe(mousePosAction.action.ReadValue<Vector2>(), 0);
			else if(!mouseDown && CurrentSwipe != null)
				CurrentSwipe = null;

			if(CurrentSwipe != null)
			{
				CurrentSwipe.positions.Enqueue(mousePosAction.action.ReadValue<Vector2>());
				if(CurrentSwipe.positions.Count > 20)
					CurrentSwipe.positions.Dequeue();
			}
		}
	}
}