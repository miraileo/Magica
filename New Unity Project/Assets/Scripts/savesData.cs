using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class savesData : MonoBehaviour
{
    Movement player;
    public float lvl;
    void Start()
    {
        player = GameObject.Find("Char").GetComponent<Movement>();   
    }

    // Update is called once per frame
    void Update()
    {
        lvl = player.lvl;
    }
}
