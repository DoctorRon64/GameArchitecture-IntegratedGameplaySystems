using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : IDamagable, IUpdateable, IFixedUpdateable
{
    [SerializeField] private Rigidbody2D rb2d;
    private float moveSpeed;
    private float rotateSpeed;
    private float damping;

    private InputHandler input;
    public Action OnFireGun;

    public int Health { get; set; }
    public int MaxHealth { get; set; }
    public PlayerData() 
    {
        input = new InputHandler();
        moveSpeed = 5f;
        rotateSpeed = 2f;
        damping = 0.7f;
    }

    public void OnFixedUpdate()
    {
        Vector2 moveDirection = new Vector2(input.HorizontalInput, input.VerticalInput).normalized;

        if (moveDirection != Vector2.zero)
        {
            float angle = Mathf.Atan2(moveDirection.y, moveDirection.x) * Mathf.Rad2Deg;
            rb2d.rotation = Mathf.LerpAngle(rb2d.rotation, angle, rotateSpeed * Time.deltaTime);
        }

        Vector2 targetVelocity = moveDirection * moveSpeed;

        rb2d.velocity = Vector2.Lerp(rb2d.velocity, targetVelocity, 1 - damping);
    }

    public void OnUpdate()
    {
        input.OnUpdate();

        if (input.UpdateButton())
        {
            FireGun();
        }
    }
    
    private void FireGun()
    {
        OnFireGun?.Invoke();
    }

    public void TakeDamage(int amount)
    {
           
    }
}
