using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VecrticalGizmosDraw : MonoBehaviour
{
    private Vector3 areaCube = new Vector3(4, 8, 0); // horizontal cube
    // 16 x 10 +-

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(transform.position, areaCube);
    }
}