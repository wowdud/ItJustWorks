using UnityEditor;

using UnityEngine;

namespace ItJustWorks
{
	[CustomPropertyDrawer(typeof(SceneAttribute))]
	public class SceneAttributeDrawer : PropertyDrawer
	{
		public override void OnGUI(Rect _position, SerializedProperty _property, GUIContent _label)
		{
			EditorGUI.BeginProperty(_position, _label, _property);
			{
				// Load the scene currently set in the inspector
				SceneAsset oldScene = AssetDatabase.LoadAssetAtPath<SceneAsset>(SceneAttribute.ToAssetPath(_property.stringValue));

				// Begin checking if anything has changed in the inspector
				EditorGUI.BeginChangeCheck();

				// Draw the scene field as an object with the SceneAsset type
				SceneAsset newScene = EditorGUI.ObjectField(_position, _label, oldScene, typeof(SceneAsset), false) as SceneAsset;

				// Confirm if anything did change since we started
				if(EditorGUI.EndChangeCheck())
				{
					// Set the string value to the path of the new scene.
					string path = SceneAttribute.ToLoadableName(AssetDatabase.GetAssetPath(newScene));
					_property.stringValue = path;
				}
			}
			EditorGUI.EndProperty();
		}
	}
}