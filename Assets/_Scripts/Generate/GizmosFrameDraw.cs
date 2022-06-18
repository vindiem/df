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
        Gizmos.DrawWireCube(transform.position, new Vector3(150f, 80f, 0f));
        
    }
#endif    
    
}
