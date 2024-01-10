using UnityEngine;
using UnityEngine.Serialization;

namespace NPC.TrafficSystem
{
    public class Waypoint : MonoBehaviour
    {
        public Waypoint PreviousWaypoint;
        public Waypoint NextWayPoint;

        [Range(0f,5f)]
        public float Width = 1f;

        public Vector3 GetPosition()
        {
            Vector3 minBound = transform.position + transform.up * Width / 2f;
            Vector3 maxBound = transform.position -transform.up * Width / 2f;

            return Vector3.Lerp(minBound, maxBound, Random.Range(0f, 1f));
        }
    }
}
