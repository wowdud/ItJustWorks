using ItJustWorks.CrossPlatform;

using UnityEngine;

namespace ItJustWorks.Samples.CrossPlatform
{
	public class PlayerMove : MonoBehaviour
	{
		public float speed;

		private void Update()
		{
			Vector2 axis = CrossPlatformInput.GetMovementAxis() * Time.deltaTime * speed;
			transform.position += transform.up * axis.y + transform.right * axis.x;
		}
	}
}