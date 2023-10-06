using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : IFixedUpdateable
{
    private PlayerFactory playerFactory;
    private Player player;

    public PlayerManager(InputManager inputManager)
    {
        playerFactory = new PlayerFactory(inputManager);

        player = playerFactory.Create("Player");
    }

    public void OnFixedUpdate()
    {
        player.OnFixedUpdate();
    }
}
