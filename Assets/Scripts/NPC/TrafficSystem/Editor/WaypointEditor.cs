using UnityEngine;
using UnityEditor;

namespace NPC.TrafficSystem.Editor
{
    [InitializeOnLoad()]
    public class WaypointEditor
    {
        [DrawGizmo(GizmoType.NonSelected | GizmoType.Selected | GizmoType.Pickable)]
        public static void OnDrawSceneGizmo(Waypoint waypoint, GizmoType gizmoType)
        {
            if((gizmoType & GizmoType.Selected)!=0)
            {
                Gizmos.color = Color.yellow;
            }
            else
            {
                Gizmos.color = Color.yellow*0.5f;
            }
            Gizmos.DrawSphere(waypoint.transform.position, 0.5f);

            Gizmos.color = Color.white;
            Gizmos.DrawLine(waypoint.transform.position + (waypoint.transform.up*waypoint.Width/2f),
                waypoint.transform.position-(waypoint.transform.up*waypoint.Width/2f));
            
            if (waypoint.PreviousWaypoint != null)
            {
                Gizmos.color = Color.red;
                Vector3 offset = waypoint.transform.up * waypoint.Width / 2f;
                Vector3 offsetTo = waypoint.PreviousWaypoint.transform.up * waypoint.PreviousWaypoint.Width / 2f;
                
                Gizmos.DrawLine(waypoint.transform.position+offset,waypoint.PreviousWaypoint.transform.position+offsetTo);
            }

            if (waypoint.NextWayPoint != null)
            {
                Gizmos.color = Color.green;
                Vector3 offset = waypoint.transform.up * -waypoint.Width / 2f;
                Vector3 offsetTo = waypoint.NextWayPoint.transform.up * -waypoint.NextWayPoint.Width / 2f;
                
                Gizmos.DrawLine(waypoint.transform.position+offset,waypoint.NextWayPoint.transform.position+offsetTo);
            }
        }
    }
}
