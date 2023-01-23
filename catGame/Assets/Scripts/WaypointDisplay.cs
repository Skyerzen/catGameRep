using UnityEngine;
using System.Collections;

public class WaypointDisplay : MonoBehaviour
{
    public Color waypointColor = Color.black;
    void OnDrawGizmos()
    {
        Gizmos.color = waypointColor;
        Gizmos.DrawWireCube(transform.position, new Vector3(1f,1f,1f));
    }

}