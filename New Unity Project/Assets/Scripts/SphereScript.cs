using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereScript : MonoBehaviour
{
    private Camera cam;
    public float speed;
    Vector3 target;
    public GameObject hit;
    public MeshRenderer mesh;
    EnemyAiScript enemy;
    // Start is called before the first frame update
    void Start()
    {
        TargetPosition();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position = Vector3.MoveTowards(transform.position, target, speed);
        Invoke("DestroySphere", 1.5f);
    }
   
    void DestroySphere()
    {
        Destroy(gameObject);
    }
    private void OnTriggerEnter(Collider other)
    {
        hit.gameObject.SetActive(true);
        Invoke("DestroySphere", 0.2f);
        if (other.tag == "Enemy")
        {
            mesh.enabled = false;
            hit.gameObject.SetActive(true);
            enemy = other.gameObject.GetComponent<EnemyAiScript>();
        }
    }

    void TargetPosition()
    {
            cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit raycastHit))
            {
                target = raycastHit.point;
            }
    }
}
