using ItJustWorks.CrossPlatform;

using UnityEngine;

namespace ItJustWorks.Samples.CrossPlatform
{
	public class PlayerMove : MonoBehaviour
	{
		private void Update()
		{
			Vector2 axis = CrossPlatformInput.GetMovementAxis() * Time.deltaTime;
			transform.position += transform.up * axis.y + transform.right * axis.x;
		}
	}
}