using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class ColorRainbow : MonoBehaviour
{
    private Image _image;
    
    private void Start()
    {
        _image = GetComponent<Image>();
        _image.color = new Color32(255, 255, 255, 255);
        StartCoroutine(ColorRainbowChange());
    }

    private void OnDisable()
    {
        StopCoroutine(ColorRainbowChange());
        
    }

    private void OnEnable()
    {
        StartCoroutine(ColorRainbowChange());
    }

    private IEnumerator ColorRainbowChange()
    {
        while (true)
        {
            _image.color = new Color32((byte)Random.Range(0, 255), 
                (byte)Random.Range(0, 255), 
                (byte)Random.Range(0, 255), 
                255);
            yield return new WaitForSeconds(0.3f);
        }
    }
}
