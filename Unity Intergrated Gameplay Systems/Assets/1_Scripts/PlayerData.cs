using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : IDamagable, IFixedUpdateable
{
    [SerializeField] private Rigidbody2D rb2d;
    private InputHandler inputHandler;
    private float moveSpeed;
    private float rotateSpeed;
    private float damping;
    
    public int Health { get; set; }
    public int MaxHealth { get; set; }
    public PlayerData(InputHandler _input) 
    {
        inputHandler = _input;

        inputHandler.OnLeftMouseButton += FireGun;
        moveSpeed = 5f;
        rotateSpeed = 2f;
        damping = 0.7f;
    }

    public void OnFixedUpdate()
    {
        Vector2 moveDirection = new Vector2(inputHandler.HorizontalInput, inputHandler.VerticalInput).normalized;

        if (moveDirection != Vector2.zero)
        {
            float angle = Mathf.Atan2(moveDirection.y, moveDirection.x) * Mathf.Rad2Deg;
            rb2d.rotation = Mathf.LerpAngle(rb2d.rotation, angle, rotateSpeed * Time.deltaTime);
        }

        Vector2 targetVelocity = moveDirection * moveSpeed;

        rb2d.velocity = Vector2.Lerp(rb2d.velocity, targetVelocity, 1 - damping);
    }

    private void FireGun()
    {

    }

    public void TakeDamage(int amount)
    {
        Health -= amount;       
    }
}
