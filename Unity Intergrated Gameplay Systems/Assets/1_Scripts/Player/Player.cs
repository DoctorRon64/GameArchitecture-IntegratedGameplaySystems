using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : IDamagable, IFixedUpdateable, IInstantiatable
{
    public int Health { get; set; }
    public int MaxHealth { get; set; }
    public GameObject Instance { get; set; }

    private Rigidbody2D rb2d;
    private float moveSpeed;
    private float rotateSpeed;
    private float damping;

    public Action FireBullet;

    //Reference
    private InputManager inputHandler;
    private BulletManager bulletManager;
    
    public Player(GameObject prefab, BulletManager _bulletManager, InputManager _input, Transform _parent) 
    {
        Instance = GameObject.Instantiate(prefab);
        Instance.transform.SetParent(_parent);

        rb2d = Instance.GetComponent<Rigidbody2D>();
        inputHandler = _input;
        bulletManager = _bulletManager;

        inputHandler.OnLeftMouseButton += FireGun;
        FireBullet += bulletManager.ReleaseBullet;

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
        FireBullet?.Invoke();
    }

    //IDamagable

    public void TakeDamage(int amount)
    {
        Health -= amount;       
    }

    public void Die()
    {
        //PlayerDie
    }
}
