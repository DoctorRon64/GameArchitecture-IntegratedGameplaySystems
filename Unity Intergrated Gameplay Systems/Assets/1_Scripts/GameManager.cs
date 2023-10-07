using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class GameManager : MonoBehaviour
{
    //ScriptableObjects
    [SerializeField] private GameSettings gameSettings;

    //Lists
    private List<IUpdateable> updateables = new List<IUpdateable>();
    private List<IFixedUpdateable> fixedUpdateables = new List<IFixedUpdateable>();

    //References
    private GridManager gridManager;
    private BulletManager bulletManager;
    private InputManager inputManager;
    [SerializeField] private SceneManagerObject sceneManagerObject;

    [Header("Cinemachine")]
    public new CinemachineVirtualCamera camera;

    private int score;

    public void Awake()
    {
        if (gameSettings == null)
        {
            Debug.LogError("GameManager has no reference to gridSettings");
        }
    }

    public void Start()
    {
        //Input
        inputManager = new InputManager();
        AddUpdate(inputManager);

        //Enemys
        EnemyManager enemyManager = new EnemyManager(gameSettings);
        AddFixedUpdate(enemyManager);

        gridManager = new GridManager(gameSettings, enemyManager);

        //Bullet
        bulletManager = new BulletManager(gridManager, gameSettings, enemyManager);
        AddUpdate(bulletManager);
        AddFixedUpdate(bulletManager);

        //Player
        PlayerManager playerManager = new PlayerManager(inputManager, bulletManager, gameSettings);
        AddFixedUpdate(playerManager);
        enemyManager.AddPlayerManager(playerManager);


        //set cinemachine camera to player
        Player player = playerManager.GetPlayer();
        camera.Follow = player.Instance.transform;

        player.OnDiePlayer += sceneManagerObject.LoadScene;
    }

    public void AddUpdate(IUpdateable script)
    {
        updateables.Add(script);
    }

    public void AddFixedUpdate(IFixedUpdateable script)
    {
        fixedUpdateables.Add(script);
    }

    public void Remove(IUpdateable script)
    {
        updateables.Remove(script);

        if (script is IFixedUpdateable)
        {
            fixedUpdateables.Remove(script as IFixedUpdateable);
        }
    }

    public void AddScore(int _amount)
    {
        score += _amount;
    }

    private void Update()
    {
        foreach (IUpdateable iUpdateble in updateables)
        {
            iUpdateble.OnUpdate();
        }
    }

    private void FixedUpdate()
    {
        foreach (IFixedUpdateable iFixedUpdateble in fixedUpdateables)
        {
            iFixedUpdateble.OnFixedUpdate();
        }
    }
}