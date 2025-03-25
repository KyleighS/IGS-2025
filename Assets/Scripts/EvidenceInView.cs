using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine.Rendering.Universal;

public class EvidenceInView : MonoBehaviour
{
    public float viewRadius;
    [Range(0,360)]
    public float viewAngle;

    public LayerMask evidenceMask;
    public LayerMask obstacleMask;

    [HideInInspector]
    public List<Transform> visibleEvidence = new List<Transform>();

    public float meshResolution;
    public bool evidenceOnScreen;
    public Camera mainCam;

    private void Start()
    {
        StartCoroutine("FindEvidenceWithDelay", 1f);
    }
    private void Update()
    {
        //DrawFOV();
    }

    IEnumerator FindEvidenceWithDelay(float delay)
    {
        while (true)
        {
            yield return new WaitForSeconds(delay);
            FindVisibleEvidence();
        }
    }

    void FindVisibleEvidence()
    {
        //Debug.Log("list was cleared");
        visibleEvidence.Clear();
        Collider[] evidenceInViewRadius = Physics.OverlapSphere(transform.position, viewRadius, evidenceMask);

        for(int i = 0; i < evidenceInViewRadius.Length; i++)
        {
            Transform evidence = evidenceInViewRadius[i].transform;
            Vector3 dirToEvidence = (evidence.position - transform.position).normalized;
       
            if(Vector3.Angle(mainCam.transform.forward, dirToEvidence) < viewAngle / 2f)
            {
                float distToEvidence = Vector3.Distance(transform.position, evidence.position);
                Debug.DrawLine(transform.position, evidence.position, Color.red, 0.5f);


                if (!Physics.Raycast(transform.position, dirToEvidence, distToEvidence, obstacleMask))
                {
                    visibleEvidence.Add(evidence);
                    //Debug.Log("Current layer: " + evidenceInViewRadius.layer);
                    evidenceOnScreen = true;
                }
            }
        }
    }

    void DrawFOV()
    {
        int stepCount = Mathf.RoundToInt(viewAngle * meshResolution);
        float stepAngleSize = viewAngle / stepCount;
        List<Vector3> viewPoints = new List<Vector3>();

        for (int i = 0; i <= stepCount; i++)
        {
            float angle = transform.eulerAngles.y - viewAngle / 2 + stepAngleSize * i;
            ViewCastInfo newViewCast = ViewCast(angle);
            viewPoints.Add(newViewCast.point);
        }

        int vertexCount = viewPoints.Count + 1;
        Vector3[] verticies = new Vector3[vertexCount];
        int[] triagnles = new int[(vertexCount - 2) * 3];

        verticies[0] = Vector3.zero;

        for (int i = 0; i < vertexCount - 1; i++)
        {
            if (i < vertexCount - 2)
            {
                verticies[i + 1] = viewPoints[i];
                triagnles[i * 3] = 0;
                triagnles[i * 3 * 1] = i + 1;
                //triagnles[i * 3 * 2] = i + 2;
            }
        }

    }

    ViewCastInfo ViewCast(float globalAngle)
    {
        Vector3 dir = DirFromAngle(globalAngle, true);  
        RaycastHit hit;

        if(Physics.Raycast (transform.position, dir, out hit, viewAngle, obstacleMask))
        {
            return new ViewCastInfo(true, hit.point, hit.distance, globalAngle);
        }
        else
        {
            return new ViewCastInfo(false, transform.position + dir * viewRadius, viewRadius, globalAngle);
        }
    }

    public Vector3 DirFromAngle(float angleInDegrees, bool angleIsGlobal)
    {
        if(!angleIsGlobal)
        {
            angleInDegrees += transform.eulerAngles.y;
        }
        return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0, Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
    }

    public struct ViewCastInfo
    {
        public bool hit;
        public Vector3 point;
        public float dist;
        public float angle;

        public ViewCastInfo(bool _hit, Vector3 _point, float _dist, float _angle)
        {
            hit = _hit;
            point = _point;
            dist = _dist;
            angle = _angle;
        }
    }
}
