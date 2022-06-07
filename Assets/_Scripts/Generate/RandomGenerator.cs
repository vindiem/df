using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class RandomGenerator : MonoBehaviour
{
    public GameObject[] tileSet;

    private Vector3 areaCube = new Vector3(15, 9, 1);
    // 16 x 10 +-
    
    private void Start()
    {
        tileSetRandomInstantiate();
    }

    private void tileSetRandomInstantiate()
    {
        int randomTiles = Random.Range(0, tileSet.Length);
        Instantiate(tileSet[randomTiles], transform.position, Quaternion.identity);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(transform.position, areaCube);
    }
    
}
