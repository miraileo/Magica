using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioScript : MonoBehaviour
{
    public AudioSource source;
    public SpawnerForFinalScene finalScene;
    void Start()
    {
        
    }

    void Update()
    {
        if(finalScene.numOfWave % 4 == 0 && finalScene.numOfWave != 0)
        {
            source.enabled = false;
        }
        if(finalScene.numOfWave % 4 != 0)
        {
            source.enabled = true;
        }    
    }
}
