using System.Collections;
using System.Diagnostics.CodeAnalysis;

using UnityEngine;

namespace ItJustWorks.AI
{
	/// <summary> A generic spawner that uses a BoxCollider style editor to spawn enemies in an area defined by the developer. </summary>
	[SuppressMessage("ReSharper", "AutoPropertyCanBeMadeGetOnly.Global")]
	public class SpawnBounds : MonoBehaviour
	{
		/// <summary> Whether or not an enemy can spawn. This is used inside the while loop of <see cref="SpawnLoop"/>. </summary>
		public bool CanSpawn { get; set; } = true;

		/// <summary> The size of the spawner. This is used as the area that enemies can spawn in. </summary>
		[Tooltip("The size of the spawner. This is used as the area that enemies can spawn in.")]
		public Vector3 size = Vector3.one;

		/// <summary> The center point of the spawner. This can also be seen as an offset from the position of the spawner GameObject. </summary>
		[Tooltip("The center point of the spawner. This can also be seen as an offset from the position of the spawner GameObject.")]
		public Vector3 center;

		/// <summary> Whether or not we should use the y position of the spawner instead of a random y inside the bounds. </summary>
		[SerializeField, Tooltip("Whether or not we should use the y position of the spawner instead of a random y inside the bounds.")]
		private bool floorYPosition;

		/// <summary> A range for how long between spawns. Each time a spawn occurs a new value in this range is chosen. </summary>
		[SerializeField, Tooltip("A range for how long between spawns. Each time a spawn occurs a new value in this range is chosen.")]
		private Vector2 spawnRateBounds = new Vector2(0, 1);

		/// <summary> Whether or not this spawner is capable of spawning a boss. </summary>
		[SerializeField, Tooltip("Whether or not this spawner is capable of spawning a boss.")]
		private bool shouldSpawnBoss;

		/// <summary> The chance of a boss spawning from this spawner. This value is only relevant if <see cref="shouldSpawnBoss"/> is true. </summary>
		[SerializeField, Range(0, 100), Tooltip("The chance of a boss spawning from this spawner. This value is only relevant if shouldSpawnBoss is true.")]
		private int bossSpawnChance = 1;

		/// <summary> The prefab of the boss object that gets spawned. This value is only relevant if <see cref="shouldSpawnBoss"/> is true. </summary>
		[SerializeField, Tooltip("The prefab of the boss object that gets spawned. This value is only relevant if shouldSpawnBoss is true.")]
		private GameObject bossPrefab;

		/// <summary> The prefab of the enemy that this spawner will spawn. </summary>
		[SerializeField, Tooltip("The prefab of the enemy that this spawner will spawn.")]
		private GameObject enemyPrefab;

		/// <summary> The function that actually spawns an enemy on this spawner. It uses most of the variables to run it's calculations. </summary>
		public void Spawn()
		{
			// Choose a prefab based on whether or not we can spawn a boss and the chance to spawn a boss has reached.
			GameObject toSpawn = shouldSpawnBoss && Random.Range(0, 101) < bossSpawnChance ? bossPrefab : enemyPrefab;
			
			// Get the spawn point for the prefab using the size and then offsetting it by the center. 
			Vector3 spawnPoint = new Vector3
			{
				x = Random.Range(-size.x * .5f, size.x * .5f),
				y = floorYPosition ? 0 : Random.Range(-size.x * .5f, size.x * .5f),
				z = Random.Range(-size.x * .5f, size.x * .5f),
			};
			spawnPoint += center;

			// Instantiate the object being spawned and set it's transform values
			GameObject spawned = Instantiate(toSpawn, transform);
			spawned.transform.localPosition = spawnPoint;
			spawned.transform.localRotation = Quaternion.identity;
		}

		/// <summary>
		/// The coroutine that runs the spawner, calling <see cref="Spawn"/> everytime the spawner loops.
		/// It yields using a randomly chosen value in <see cref="spawnRateBounds"/> range.
		/// </summary>
		private IEnumerator SpawnLoop()
		{
			// if we can spawn, loop the spawner waiting a random amount of time inside the spawnRateBounds value.
			while(CanSpawn)
			{
				yield return new WaitForSeconds(Random.Range(spawnRateBounds.x, spawnRateBounds.y));
				Spawn();
			}
		}

		#region UNITY_MESSAGES

		private void Awake() => StartCoroutine(SpawnLoop());

		#if UNITY_EDITOR
		private void OnDrawGizmos()
		{
			// Store the original Gizmos Matrix
			Matrix4x4 original = Gizmos.matrix;

			// Make the Gizmos use the current objects transform matrix
			Gizmos.matrix = transform.localToWorldMatrix;

			// Draw a green, mostly transparent cube
			Gizmos.color = new Color(0, 1, 0, .25f);
			Gizmos.DrawCube(center, size);

			// Reset the Gizmos matrix to the original one
			Gizmos.matrix = original;
		}
		#endif
		
		#endregion
	}
}