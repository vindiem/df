using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextAnimation : MonoBehaviour
{
    private string thisText;

    private void Start()
    {
        thisText = GetComponent<Text>().text;
        GetComponent<Text>().text = "";
        StartCoroutine(TextCorountine());
    }

    private IEnumerator TextCorountine()
    {
        foreach (var variable in thisText)
        {
            GetComponent<Text>().text += variable;
            yield return new WaitForSeconds(0.075f);
        }
    }
}
