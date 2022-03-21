using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

namespace ItJustWorks.CrossPlatform
{
	public class MobileJoystickHandler : InputHandler, IDragHandler, IEndDragHandler, IPointerDownHandler, IPointerUpHandler
	{
		/// <summary> The position of the Joystick. </summary>
		public Vector2 Axis { get; private set; }

		[SerializeField] private RectTransform handle;
		[SerializeField] private RectTransform background;
		[SerializeField, Range(0, 1)] private float deadzone = .25f;
		[SerializeField] private InputActionReference movementAction;
		[SerializeField] private bool testTouchInput;

		// The position the handle was originally in.
		private Vector3 initialPos;

        protected override void OnInitialise(object[] _data)
        {
            initialPos = handle.position;

            // Check if the user is using mobile controls or PC controls.
            if (Input.currentControlScheme == "Keyboard&Mouse" && !testTouchInput)
            {
                //if they are, enable joystick controls
                handle.gameObject.SetActive(false);
                background.gameObject.SetActive(false);
            }
            if (Input.currentControlScheme == "Touch" || testTouchInput)
            {
                //if they are, enable joystick controls
                handle.gameObject.SetActive(true);
                background.gameObject.SetActive(true);
            }
        }

		public override void Process() 
		{
            if(Input.currentControlScheme == "Keyboard&Mouse" && !testTouchInput)
            {
                Axis = movementAction.action.ReadValue<Vector2>(); 
            }
		}
		
		public void OnDrag(PointerEventData _eventData)
		{
			// Calculate the half size difference between the background and handle rects
			Vector2 difference = new Vector2
			{
				x = (background.rect.size.x - handle.rect.size.x) * .5f,
				y = (background.rect.size.y - handle.rect.size.y) * .5f
			};

			// Calculate the axis of the input based on the event data and the relative
			// position to the background's center
			Axis = Vector2.ClampMagnitude(new Vector2
			{
				x = (_eventData.position.x - background.position.x) / difference.x,
				y = (_eventData.position.y - background.position.y) / difference.y
			}, 1);

			// Apply the axis position to the handles position
			handle.position = new Vector3
			{
				x = (Axis.x * difference.x) + background.position.x,
				y = (Axis.y * difference.y) + background.position.y
			};

			// Apply the deadzone effect after the handle has been placed to prevent
			// the handle from visually being stuck in the deadzone
			Axis = (Axis.magnitude < deadzone) ? Vector2.zero : Axis;
		}

		public void OnEndDrag(PointerEventData _eventData)
		{
			// We have let go so reset the axis and position of the handle
			Axis = Vector2.zero;
			handle.position = initialPos;
		}

		public void OnPointerDown(PointerEventData _eventData) => OnDrag(_eventData);
		public void OnPointerUp(PointerEventData _eventData) => OnEndDrag(_eventData);
	}
}