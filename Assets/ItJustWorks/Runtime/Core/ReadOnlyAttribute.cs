using System;

using UnityEngine;

namespace ItJustWorks
{
	// AttributeTargets.Field means this attribute can only ever be on a variable
	[AttributeUsage(AttributeTargets.Field)]
	public class ReadOnlyAttribute : PropertyAttribute { }
}