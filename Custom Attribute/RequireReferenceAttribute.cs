using UnityEditor;
using UnityEngine;

namespace Custom_Attribute
{
    /// <summary>
    /// Show warning message on inspector bellow selected property to set reference of that property. 
    /// </summary>
    public class RequireReferenceAttribute : PropertyAttribute
    {
        public string message;

        public RequireReferenceAttribute(string message = "Field can't be left null.")
        {
            this.message = message;
        }
    }

#if UNITY_EDITOR

    [CustomPropertyDrawer(typeof(RequireReferenceAttribute), true)]
    public class RequireReferenceAttributeDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            var requireReferenceAttribute = this.attribute as RequireReferenceAttribute;
            EditorGUI.PropertyField(position, property, label, true);
            if (property.objectReferenceValue == null)
            {
                if (requireReferenceAttribute != null)
                {
                    EditorGUILayout.Separator();
                    EditorGUILayout.HelpBox(requireReferenceAttribute.message, MessageType.Warning);
                    EditorGUILayout.Separator();
                }
                    
            }
        }
    }

#endif
}