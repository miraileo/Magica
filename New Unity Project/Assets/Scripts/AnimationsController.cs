using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationsController : MonoBehaviour
{
    private InputHandler _input;
    private Movement movement;
    [SerializeField] private Animator _animator;
    public AudioSource audio;
    // Start is called before the first frame update
    void Start()
    {
        _input = GetComponent<InputHandler>();
        movement = GetComponent<Movement>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_input.v != 0 || _input.h != 0)
        {
            _animator.SetBool("run", true);
           audio.enabled = true;
        }
        else
        {
            _animator.SetBool("run", false);
            audio.enabled = false;
        }
    }
}
