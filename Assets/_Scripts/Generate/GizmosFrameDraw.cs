using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GizmosFrameDraw : MonoBehaviour
{

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green; 
        Gizmos.DrawWireCube(transform.position, new Vector3(135f, 65f, 0f));
    }
#endif    
    
}
