using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;
    public InputScheme controls;

    public float speed = 6;

    void Awake()
    {
        controls.Player.Attack.performed += context => Attack();
    }

    void OnEnable()
    {
        controls.Enable();
    }

    void OnDisable()
    {
        controls.Disable();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Attack()
    {
        Debug.Log("Watch where you're swinging that thing!");
    }
}
