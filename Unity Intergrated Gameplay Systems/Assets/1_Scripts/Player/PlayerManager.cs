using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : IFixedUpdateable
{
    private PlayerFactory playerFactory;
    public Player player;

    public PlayerManager(InputManager inputManager, BulletManager bulletManager)
    {
        playerFactory = new PlayerFactory(inputManager, bulletManager);

        player = playerFactory.Create("Player");
    }

    public Player ReturnPlayer()
    {
        return player;
    }

    public void OnFixedUpdate()
    {
        player.OnFixedUpdate();
    }
}
