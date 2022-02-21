using UnityEngine;

namespace ItJustWorks.Samples
{
	public class Enemy : MonoBehaviour, IInitialiser
	{
		public bool Initialised { get; private set; }

		public float health;
		public float speed;
		
		public void Initialise(params object[] _data)
		{
			if(Initialised)
				return;

			health = (float) _data[0];
			speed = (float) _data[1];
			gameObject.name = (string) _data[2];

			Initialised = true;
		}
		
		private void Start()
		{
			GameManager.Instance.enemyCount++;
		}
	}
}