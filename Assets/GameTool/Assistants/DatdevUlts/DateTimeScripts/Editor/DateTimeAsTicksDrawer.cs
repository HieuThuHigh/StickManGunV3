using System;
using System.Globalization;
using UnityEditor;
using UnityEngine;

namespace DatdevUlts.DateTimeScripts.Editor
{
    [CustomPropertyDrawer(typeof(DateTimeAsTicks))]
    public class DateTimeAsTicksDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginProperty(position, label, property);
            var pos = EditorGUI.PrefixLabel(position, label);

            var att = (DateTimeAsTicks)attribute;

            string show;
            show = new DateTime(property.longValue).ToString(CultureInfo.InvariantCulture);
            
            var value = EditorGUI.TextField(pos, show);

            try
            {
                property.longValue = DateTime.Parse(value, CultureInfo.InvariantCulture).Ticks;
            }
            catch (Exception)
            {
                // ignored
            }

            EditorGUI.EndProperty();
        }
    }
}