using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    float timeStart;
    public Text timeText;

    private void Start()
    {
        timeText.text = timeStart.ToString("F2");
    }

    private void Update()
    {
        timeStart += Time.deltaTime;
        timeText.text = timeStart.ToString("F2");
    }
}
