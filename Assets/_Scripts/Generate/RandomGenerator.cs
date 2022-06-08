using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class RandomGenerator : MonoBehaviour
{
    public GameObject[] tileSet;

    private Vector3 areaCube = new Vector3(16, 4, 0); // horizontal cube
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

    // Horizontal Gizmos Draw
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(transform.position, areaCube);
    }

}
