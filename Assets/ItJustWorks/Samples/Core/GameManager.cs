using System.Collections.Generic;

using UnityEngine;

namespace ItJustWorks.Samples
{
	public class GameManager : Singleton<GameManager>
	{
		public int enemyCount;

		public List<Gun> guns = new List<Gun>();
		public List<Enemy> enemies = new List<Enemy>();

		public void Start()
		{
			foreach(Enemy enemy in enemies)
			{
				enemy.Initialise(Random.Range(1f, 5f), Random.Range(1f, 5f), "Bob");
			}

			foreach(Gun gun in guns)
			{
				gun.Initialise(Random.Range(1f, 5f), Random.Range(1f, 5f), Random.Range(1, 5), "Bob", Random.Range(0, 2) == 0);
			}
		}
	}
}