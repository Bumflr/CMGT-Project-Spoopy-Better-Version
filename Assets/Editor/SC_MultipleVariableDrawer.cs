using UnityEngine;
using UnityEditor;

[CustomPropertyDrawer(typeof(MultipleFloatsAttribute))]
public class SC_MultipleVariableDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position,
                              SerializedProperty property,
                              GUIContent label)
    {

        position.height = EditorGUIUtility.singleLineHeight;
        var att = (MultipleFloatsAttribute)attribute;
        var type = property.propertyType;



        var ctrlRect = EditorGUI.PrefixLabel(position, label);

        Rect[] r = SplitRect(ctrlRect, 4);

        EditorGUI.BeginChangeCheck();

        for (int x = 0; x < property.arraySize; x++)
        {
            SerializedProperty serializedProperty = property.GetArrayElementAtIndex(x);
            property.floatValue = Mathf.Max(0, property.floatValue); // Edit this element's value, in this case limit the float's value to a positive value.
        }


        // Line setup
        var line2 = position;
        line2.y += EditorGUIUtility.singleLineHeight;


        float first = property.floatValue;
        float second = property.floatValue;
        float third = property.floatValue;
        float fourth = property.floatValue;
        float fifth = property.floatValue;

        first = EditorGUI.FloatField(r[0], first);
        second = EditorGUI.FloatField(r[1], second);
        third = EditorGUI.FloatField(r[2], third);
        fourth = EditorGUI.FloatField(r[3], fourth);



        // And finally update the variables
        if (EditorGUI.EndChangeCheck())
        {
            property.floatValue = first;
        }
    }

    public static Rect[] SplitRect(Rect a, int n)
    {
        Rect[] r = new Rect[n];
        for (int i = 0; i < n; ++i)
            r[i] = new Rect(a.x + a.width / n * i, a.y, a.width / n, a.height);
        return r;
    }
}
