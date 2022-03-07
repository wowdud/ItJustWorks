using System;

using UnityEngine;

namespace ItJustWorks
{
	[AttributeUsage(AttributeTargets.Field)]
	public class SceneAttribute : PropertyAttribute
	{
		private const string PATH_PREFIX = "Assets/";
		private const string FILE_EXTENSION = ".unity";

		// Assets/MainMenu.unity
		/// <summary> Takes a local folder path and converts it into an asset path. </summary>
		/// <param name="_path"> The local path of the scene file. </param>
		public static string ToAssetPath(string _path) => $"{PATH_PREFIX}{_path}{FILE_EXTENSION}";

		/// <summary> Converts an asset filepath to a SceneManager friendly one for scene loading. </summary>
		/// <param name="_assetPath"> The asset path to be converted into a scene path. </param>
		public static string ToLoadableName(string _assetPath)
		{
			// Test if the path contains 'PATH_PREFIX' data, if so, remove it.
			if(_assetPath.StartsWith(PATH_PREFIX))
				_assetPath = _assetPath.Substring(PATH_PREFIX.Length);

			// Test if the path contains 'FILE_EXTENSION' data, if so, remove it.
			if(_assetPath.EndsWith(FILE_EXTENSION))
				// ReSharper disable once StringLastIndexOfIsCultureSpecific.1
				_assetPath = _assetPath.Substring(0, _assetPath.LastIndexOf(FILE_EXTENSION));

			// Return the newly edited path
			return _assetPath;
		}
	}
}