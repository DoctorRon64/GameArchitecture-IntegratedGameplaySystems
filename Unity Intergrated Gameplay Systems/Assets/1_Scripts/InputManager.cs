using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : IUpdateable
{
    public Action OnLeftMouseButton;

    [SerializeField] private KeyCode shootButton = KeyCode.F;
    public float HorizontalInput;
    public float VerticalInput;

    public void OnUpdate()
    {
        HorizontalInput = Input.GetAxis("Horizontal");
        VerticalInput = Input.GetAxis("Vertical");
        UpdateButton();
    }
    
    public void UpdateButton()
    {
        if (Input.GetKey(shootButton))
        {
            OnLeftMouseButton?.Invoke();
        }
    }
}
