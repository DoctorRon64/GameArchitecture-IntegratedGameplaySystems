using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : IUpdateable
{
    public Action OnLeftMouseButton;

    public float HorizontalInput;
    public float VerticalInput;
    
    private float scrollInput;
    private KeyCode shootButton = KeyCode.F;
    private CinemachineVirtualCamera camera;

    public InputManager(CinemachineVirtualCamera _camera)
    {
        camera = _camera;
    }

    public void OnUpdate()
    {
        HorizontalInput = Input.GetAxis("Horizontal");
        VerticalInput = Input.GetAxis("Vertical");
        scrollInput = Input.mouseScrollDelta.y;

        if (camera.m_Lens.OrthographicSize >= 5 && scrollInput > 0 || camera.m_Lens.OrthographicSize <= 20 && scrollInput < 0)
        {
            camera.m_Lens.OrthographicSize -= scrollInput;
        }

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
