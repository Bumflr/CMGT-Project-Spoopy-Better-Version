using UnityEditor;

[CustomEditor(typeof(SC_LoadNewArea))]
public class SC_LoadNewAreaEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        SC_LoadNewArea myBehaviour = target as SC_LoadNewArea;

        myBehaviour.selected = EditorGUILayout.Popup("Destination Scene", myBehaviour.selected, myBehaviour.scenes);

    }
}