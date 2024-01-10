using UnityEditor;
using UnityEngine;

namespace NPC.TrafficSystem.Editor
{
    public class WaypointManagerWindow:EditorWindow
    {
        [MenuItem("Tools/Waypoint Editor")]
        public static void Open()
        {
            GetWindow<WaypointManagerWindow>();
        }

        public Transform WaypointRoot;

        private void OnGUI()
        {
            SerializedObject obj = new SerializedObject(this);

            EditorGUILayout.PropertyField(obj.FindProperty("WaypointRoot"));

            if (WaypointRoot == null)
            {
                EditorGUILayout.HelpBox("Root transform must be selected. Please assign a root transform", MessageType.Warning);
            }
            else
            {
                EditorGUILayout.BeginVertical("box");
                DrawButtons();
                EditorGUILayout.EndVertical();
            }

            obj.ApplyModifiedProperties();
        }

        private void DrawButtons()
        {
            if (GUILayout.Button("Create Waypoint"))
            {
                CreateWaypoint();
            }

            if (Selection.activeGameObject != null && Selection.activeGameObject.GetComponent<Waypoint>())
            {
                if (GUILayout.Button("Create Waypoint Before"))
                {
                    CreateWaypointBefore();
                }
                if (GUILayout.Button("Create Waypoint After"))
                {
                    CreateWaypointAfter();
                }
                if (GUILayout.Button("Remove Waypoint"))
                {
                    RemoveWaypoint();
                }
                
            }
        }

        private void CreateWaypoint()
        {
            GameObject waypointObject = new GameObject("Waypoint " + WaypointRoot.childCount, typeof(Waypoint));
            waypointObject.transform.SetParent(WaypointRoot,false);

            Waypoint waypoint = waypointObject.GetComponent<Waypoint>();
            if (WaypointRoot.childCount > 1)
            {
                waypoint.PreviousWaypoint = WaypointRoot.GetChild(WaypointRoot.childCount - 2).GetComponent<Waypoint>();
                waypoint.PreviousWaypoint.NextWayPoint = waypoint;
                //Place the waypoint at last position
                waypoint.transform.position = waypoint.PreviousWaypoint.transform.position;
                waypoint.transform.rotation = waypoint.PreviousWaypoint.transform.rotation;
            }

            Selection.activeGameObject = waypoint.gameObject;
        }

        private void CreateWaypointBefore()
        {
            GameObject waypointObject = new GameObject("Waypoint " + WaypointRoot.childCount, typeof(Waypoint));
            waypointObject.transform.SetParent(WaypointRoot,false);
            
            Waypoint newWaypoint = waypointObject.GetComponent<Waypoint>();
            
            Waypoint selectedWaypoint = Selection.activeGameObject.GetComponent<Waypoint>();

            waypointObject.transform.position = selectedWaypoint.transform.position;
            waypointObject.transform.rotation = selectedWaypoint.transform.rotation;

            if (selectedWaypoint.PreviousWaypoint != null)
            {
                newWaypoint.PreviousWaypoint = selectedWaypoint.PreviousWaypoint;
                selectedWaypoint.PreviousWaypoint.NextWayPoint = newWaypoint;
            }

            newWaypoint.NextWayPoint = selectedWaypoint;

            selectedWaypoint.PreviousWaypoint = newWaypoint;
            
            newWaypoint.transform.SetSiblingIndex(selectedWaypoint.transform.GetSiblingIndex());

            Selection.activeGameObject = newWaypoint.gameObject;
        }
        private void CreateWaypointAfter()
        {
            GameObject waypointObject = new GameObject("Waypoint " + WaypointRoot.childCount, typeof(Waypoint));
            waypointObject.transform.SetParent(WaypointRoot,false);
            
            Waypoint newWaypoint = waypointObject.GetComponent<Waypoint>();
            
            Waypoint selectedWaypoint = Selection.activeGameObject.GetComponent<Waypoint>();

            waypointObject.transform.position = selectedWaypoint.transform.position;
            waypointObject.transform.rotation = selectedWaypoint.transform.rotation;

            newWaypoint.PreviousWaypoint = selectedWaypoint;

            if (selectedWaypoint.NextWayPoint != null)
            {
                selectedWaypoint.NextWayPoint.PreviousWaypoint = newWaypoint;
                newWaypoint.NextWayPoint = selectedWaypoint;
            }

            selectedWaypoint.NextWayPoint = newWaypoint;
            
            newWaypoint.transform.SetSiblingIndex(selectedWaypoint.transform.GetSiblingIndex());

            Selection.activeGameObject = newWaypoint.gameObject;
        }
        private void RemoveWaypoint()
        {
            Waypoint selectedWaypoint = Selection.activeGameObject.GetComponent<Waypoint>();

            if (selectedWaypoint.NextWayPoint != null)
            {
                selectedWaypoint.NextWayPoint.PreviousWaypoint = selectedWaypoint.PreviousWaypoint;
            }
            if (selectedWaypoint.PreviousWaypoint != null)
            {
                selectedWaypoint.PreviousWaypoint.NextWayPoint = selectedWaypoint.NextWayPoint;
                Selection.activeGameObject = selectedWaypoint.PreviousWaypoint.gameObject;
            }
            DestroyImmediate(selectedWaypoint.gameObject);
        }
    }
}