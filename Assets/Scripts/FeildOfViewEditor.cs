//using UnityEngine;
//using System.Collections;
//using UnityEditor;

//[CustomEditor (typeof (EvidenceInView))]
//public class FieldOfViewEditor : Editor
//{
//    private void OnSceneGUI()
//    {
//        EvidenceInView fov = (EvidenceInView)target;
//        Handles.color = Color.white;
//        Handles.DrawWireArc(fov.transform.position, Vector3.up, Vector3.forward, 360, fov.viewRadius);

//        Vector3 viewingAngleA = fov.DirFromAngle(-fov.viewAngle / 2, false);
//        Vector3 viewingAngleB = fov.DirFromAngle(fov.viewAngle / 2, false);

//        Handles.DrawLine(fov.transform.position, fov.transform.position + viewingAngleA * fov.viewRadius);
//        Handles.DrawLine(fov.transform.position, fov.transform.position + viewingAngleB * fov.viewRadius);

//        Handles.color = Color.red;
//        foreach(Transform visibleEvidence in fov.visibleEvidence)
//        {
//            Handles.DrawLine(fov.transform.position, visibleEvidence.position);
//        }
//    }
//}
