using System;

using UnityEngine;

namespace ItJustWorks
{
	// The '<GameManager>' is updating the 'SINGLETON_TYPE' to be GameManager
	// public class GameManager : Singleton<GameManager> {}
	
	// Generic types are just a placeholder for a future class type that we don't know about yet.
	public class Singleton<SINGLETON_TYPE> : MonoBehaviour 
		where SINGLETON_TYPE : Singleton<SINGLETON_TYPE>
	{
		public static SINGLETON_TYPE Instance
		{
			get
			{
				// If the internal instance isn't set, attempt to find it in the scene.
				if(instance == null)
				{
					instance = FindObjectOfType<SINGLETON_TYPE>();
					
					// Has an instance still not been found? If it hasn't, throw an exception
					// detailing what singleton caused the error
					if(instance == null)
					{
						// The 'typeof(SINGLETON_TYPE).Name' shows the exact class name of the inheriting type
						// This line will also give us a stacktrace, showing where the call to the Instance was before it existed
						throw new NullReferenceException($"No objects of type: {typeof(SINGLETON_TYPE).Name} was found.");
					}
				}

				return instance;
			}
		}
		
		private static SINGLETON_TYPE instance;

		/// <summary> Has the singleton been generate yet? </summary>
		public static bool IsValid() => instance != null;

		/// <summary> Force the singleton instance to not be destroyed on scene load. </summary>
		public static void SetPersistant() => DontDestroyOnLoad(instance.gameObject);
	}
}