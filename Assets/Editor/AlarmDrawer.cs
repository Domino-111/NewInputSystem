using UnityEngine;
using UnityEditor;

// This tage tells Unity which class to use this drawer for
[CustomPropertyDrawer(typeof(Alarm))]
public class AlarmDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        // Prepare Unity to make manual changes to a property
        EditorGUI.BeginProperty(position, label, property);

        var timeRemaining = property.FindPropertyRelative("timeRemaining");
        var timeMax = property.FindPropertyRelative("timeMax");
        var looping = property.FindPropertyRelative("looping");

        EditorGUIUtility.labelWidth = 25;

        // Start a horizontal group of controls
        EditorGUILayout.BeginHorizontal();

        // Draw the name of the variable (e.g. Alarm Test)
        EditorGUILayout.LabelField(label);

        timeRemaining.floatValue = EditorGUILayout.FloatField(timeRemaining.floatValue);
        EditorGUILayout.LabelField("of", GUILayout.Width(15));
        timeMax.floatValue = EditorGUILayout.FloatField(timeMax.floatValue);

        EditorGUILayout.EndHorizontal();

        looping.boolValue = EditorGUILayout.ToggleLeft("Looping", looping.boolValue);

        EditorGUILayout.BeginHorizontal();

        // Cast the '.boxedValue' to the desired data type 'Alarm'
        Alarm alarm = property.boxedValue as Alarm;
        if (alarm.IsPlaying)
        {
            if (GUILayout.Button("Pause"))
            {
                alarm.Pause();
            }
        }

        else
        {
            if (GUILayout.Button("Play"))
            {
                alarm.Play();
            }
        }

        if (GUILayout.Button("Stop"))
        {
            alarm.Stop();
        }

        if (GUILayout.Button("Reset"))
        {
            alarm.Reset();
        }

        property.boxedValue = alarm;

        EditorGUILayout.EndHorizontal();

        // '.PropertyField' is a generic field which will behave/draw like the default field for this property
        EditorGUILayout.PropertyField(property.FindPropertyRelative("onComplete"));

        // Tell Unity we're done making manual changes
        EditorGUI.EndProperty();
    }
}
