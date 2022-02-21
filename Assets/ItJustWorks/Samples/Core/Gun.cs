using UnityEngine;

namespace ItJustWorks.Samples
{
	public class Gun : MonoBehaviour, IInitialiser
	{
		public bool Initialised { get; private set; }

		public float damage;
		public float range;
		public int ammo;
		public string gunName;
		public bool equipped;
		
		public void Initialise(params object[] _data)
		{
			if(Initialised)
				return;

			damage = (float) _data[0];
			range = (float) _data[1];
			ammo = (int) _data[2];
			gunName = (string) _data[3];
			equipped = (bool) _data[4];

			gameObject.name = gunName + " Gun";
			
			Initialised = true;
		}
	}
}