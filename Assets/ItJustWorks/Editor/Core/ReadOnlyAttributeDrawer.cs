using UnityEditor;

using UnityEngine;

namespace ItJustWorks
{
	[CustomPropertyDrawer(typeof(ReadOnlyAttribute))]
	public class ReadOnlyAttributeDrawer : PropertyDrawer
	{
		public override void OnGUI(Rect _position, SerializedProperty _property, GUIContent _label)
		{
			EditorGUI.BeginProperty(_position, _label, _property);
			{
				// Disable the GUI, making this readonly, as it still renders, just becomes not-editable
				GUI.enabled = false;
				{
					// Render the property as it normally would.
					EditorGUI.PropertyField(_position, _property, _label);
				}
				// Re-enable the GUI to make everything work after this property
				GUI.enabled = true;
			}
			EditorGUI.EndProperty();
		}
	}
}