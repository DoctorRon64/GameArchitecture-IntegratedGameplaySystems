using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : IFixedUpdateable
{
    private PlayerFactory playerFactory;
    public Player player;

    public PlayerManager(InputManager _inputManager, BulletManager _bulletManager, GameSettings _gameSettings)
    {
        playerFactory = new PlayerFactory(_inputManager, _bulletManager, _gameSettings);

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
