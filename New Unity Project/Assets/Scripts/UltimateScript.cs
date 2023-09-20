using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UltimateScript : MonoBehaviour
{
    public GameObject ultiObject;
    public float damage = 30;
    float timeForActivation;
    float destroyTime;
    Rigidbody rb;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Invoke("ActivateUltiObject",0.65f);
    }

    void ActivateUltiObject()
    {
        ultiObject.SetActive(true);
        Invoke("DestroyUltiObject", 1.5f);
    }
    void DestroyUltiObject()
    {
        Destroy(gameObject);
    }
}
