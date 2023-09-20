using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerScript : MonoBehaviour
{
    public Slider timerBar;
    float time = 9.5f;
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        time -= Time.deltaTime;
        timerBar.value = time;
        if(time<=0)
        {
            gameObject.SetActive(false);
            time = 9.5f;
        }
    }
}
