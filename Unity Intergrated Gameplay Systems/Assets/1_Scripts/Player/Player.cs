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
    private float ShootCooldownTimer;

    //Events
    public Action<Vector2, Vector2> FireBullet;
    public Action<string> OnDiePlayer;

    //Reference
    private InputManager inputHandler;
    private BulletManager bulletManager;
    private GameSettings gameSettings;
    
    public Player(GameObject _prefab, BulletManager _bulletManager, InputManager _input, GameSettings _gameSettings, Transform _parent) 
    {
        inputHandler = _input;
        bulletManager = _bulletManager;
        gameSettings = _gameSettings;
        MaxHealth = gameSettings.PlayerHealth;
        Health = MaxHealth;

        Instantiate(_prefab, _parent);
        rb2d = Instance.GetComponent<Rigidbody2D>();

        inputHandler.OnLeftMouseButton += FireGun;
        FireBullet += bulletManager.FireBullet;
    }

    public void OnFixedUpdate()
    {
        MovePlayer();
        RotatePlayer();

        if (ShootCooldownTimer >= 0)
        {
            ShootCooldownTimer -= Time.deltaTime;
        }
    }

    public void Instantiate(GameObject _prefab, Transform _parent)
    {
        Instance = GameObject.Instantiate(_prefab);
        Instance.transform.SetParent(_parent);
    }

    private void MovePlayer()
    {
        Vector2 moveDirection = new Vector2(inputHandler.HorizontalInput, inputHandler.VerticalInput).normalized;

        if (moveDirection != Vector2.zero)
        {
            float angle = Mathf.Atan2(moveDirection.y, moveDirection.x) * Mathf.Rad2Deg;
            rb2d.rotation = Mathf.LerpAngle(rb2d.rotation, angle, gameSettings.PlayerRotateSpeed * Time.deltaTime);
        }

        Vector2 targetVelocity = moveDirection * gameSettings.PlayerSpeed;

        rb2d.velocity = Vector2.Lerp(rb2d.velocity, targetVelocity, 1 - gameSettings.PlayerDamping);
    }

    private void RotatePlayer()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = mousePos - Instance.transform.position;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Instance.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle - 90));
    }

    private void FireGun()
    {
        if (ShootCooldownTimer <= 0)
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 distance = CalcDirection(mousePos, Instance.transform.position);

            FireBullet?.Invoke(CalcBulletPointRotation(distance), distance);

            ShootCooldownTimer = gameSettings.ShootCooldown;
        }
    }

    private Vector2 CalcBulletPointRotation(Vector2 _distance)
    {
        float angle = MathF.Atan2(_distance.y, _distance.x);
        
        Vector2 circlePoint = CalculatePointOnCircle(Instance.transform.position, gameSettings.bulletSpawnDistance, angle);
        return circlePoint;
    }

    private Vector2 CalculatePointOnCircle(Vector2 center, float radius, float angleInRadians)
    {
        float x = center.x + radius * Mathf.Cos(angleInRadians);
        float y = center.y + radius * Mathf.Sin(angleInRadians);

        return new Vector2(x, y);
    }

    private Vector2 CalcDirection(Vector2 _pos1, Vector2 _pos2)
    {
        return new Vector2(_pos1.x - _pos2.x, _pos1.y - _pos2.y);
    }

    public void TakeDamage(int amount)
    {
        Health -= amount;       

        if (Health <= 0)
        {
            OnDie();
        }
    }

    public void OnDie()
    {
        OnDiePlayer?.Invoke("GameOverScene");
    }
}
