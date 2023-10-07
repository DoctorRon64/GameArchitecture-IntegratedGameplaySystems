using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.SceneManagement;

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

    [Header("Cinemachine")]
    public new CinemachineVirtualCamera camera;

    public void Awake()
    {
        if (gameSettings == null)
        {
            Debug.LogError("GameManager has no reference to gridSettings");
        }
    }

    public void Start()
    {
        //Enemys
        EnemyManager enemyManager = new EnemyManager(gameSettings);
        AddFixedUpdate(enemyManager);
        enemyManager.AddEnemy("Enemy", new Vector2(10, 10));

        gridManager = new GridManager(gameSettings, enemyManager);

        //Bullet
        bulletManager = new BulletManager(gridManager, gameSettings);
        AddUpdate(bulletManager);
        AddFixedUpdate(bulletManager);

        //Input
        inputManager = new InputManager();
        AddUpdate(inputManager);

        //Player
        PlayerManager playerManager = new PlayerManager(inputManager, bulletManager);
        AddFixedUpdate(playerManager);

        //set cinemachine camera to player
        Player player = playerManager.ReturnPlayer();
        camera.Follow = player.Instance.transform;
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

    public void GotoNextScene(string _sceneName)
    {
        SceneManager.LoadScene(_sceneName);
    }
}