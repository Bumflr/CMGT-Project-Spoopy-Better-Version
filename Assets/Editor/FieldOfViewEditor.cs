using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(FlashLightFOVCheck))]
public class FieldOfViewEditor : Editor
{
    private void OnSceneGUI()
    {
        FlashLightFOVCheck fov = (FlashLightFOVCheck)target;
        Handles.color = Color.white;
        Handles.DrawWireArc(fov.transform.parent.position, Vector3.up, Vector3.forward, 360, fov.radius);

        Vector3 viewAngle01 = DirectionFromAngle(fov.transform.parent.eulerAngles.y, -fov.angle / 2);
        Vector3 viewAngle02 = DirectionFromAngle(fov.transform.parent.eulerAngles.y, fov.angle / 2);

        Handles.color = Color.yellow;
        Handles.DrawLine(fov.transform.parent.position, fov.transform.parent.position + viewAngle01 * fov.radius);
        Handles.DrawLine(fov.transform.parent.position, fov.transform.parent.position + viewAngle02 * fov.radius);

        if (fov.canSeeEnemy)
        {
            Handles.color = Color.green;
            Handles.DrawLine(fov.transform.parent.position, fov.playerRef.transform.position);
        }
    }

    private Vector3 DirectionFromAngle(float eulerY, float angleInDegrees)
    {
        angleInDegrees += eulerY;

        return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0, Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
    }
}