using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHandler : IUpdateable
{
    [SerializeField] private KeyCode shootButton;
    public float HorizontalInput;
    public float VerticalInput;

    public void OnUpdate()
    {
        HorizontalInput = Input.GetAxis("Horizontal");
        VerticalInput = Input.GetAxis("Vertical");
        UpdateButton();
    }
    
    public bool UpdateButton()
    {
        if (Input.GetKey(shootButton))
        {
            return true;
        }
        return false;
    }
}
