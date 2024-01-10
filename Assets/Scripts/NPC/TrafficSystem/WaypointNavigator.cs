using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace NPC.TrafficSystem
{
    public class WaypointNavigator : MonoBehaviour
    {
        [HideInInspector]
        public Waypoint CurrentWaypoint;
        [SerializeField] private NPCMover _npcMover;

        private int direction;

        public void StartPatrol(Waypoint waypoint)
        {
            direction = Mathf.RoundToInt(Random.Range(0f, 1f));
            CurrentWaypoint = waypoint;
            _npcMover.SetDestination(CurrentWaypoint.GetPosition());
            _npcMover.StartPatrol();
        }

        private void Update()
        {
            if (_npcMover.ReachedDestination)
            {
                if (direction == 0)
                {
                    if (CurrentWaypoint.NextWayPoint == null)
                    {
                        SwitchDirection();
                        return;
                    }
                    CurrentWaypoint = CurrentWaypoint.NextWayPoint;
                    
                        
                }
                else
                {
                    if (CurrentWaypoint.PreviousWaypoint == null)
                    {
                        SwitchDirection();
                        return;
                    }
                    CurrentWaypoint = CurrentWaypoint.PreviousWaypoint;
                    
                }
                
                _npcMover.SetDestination(CurrentWaypoint.GetPosition());
            }
        }

        private void SwitchDirection()
        {
            direction = direction == 0 ? 1 : 0;
        }
    }
}
