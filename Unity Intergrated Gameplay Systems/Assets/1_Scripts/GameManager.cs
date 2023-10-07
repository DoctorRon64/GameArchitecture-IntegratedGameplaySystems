using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    //UI
    public int Score { get; set; }
    [SerializeField] private Text scoreText;

    //ScriptableObjects
    [SerializeField] private GameSettings gameSettings;
    [SerializeField] private SceneManagerObject sceneManagerObject;
    //Lists
    private List<IUpdateable> updateables = new List<IUpdateable>();
    private List<IFixedUpdateable> fixedUpdateables = new List<IFixedUpdateable>();

    //References
    private GridManager gridManager;
    private BulletManager bulletManager;
    private InputManager inputManager;

    private UI ui;

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
        //UI
        ui = new UI(scoreText, this);
        AddFixedUpdate(ui);

        //Input
        inputManager = new InputManager(camera);
        AddUpdate(inputManager);

        //Enemys
        EnemyManager enemyManager = new EnemyManager(gameSettings, this);
        AddFixedUpdate(enemyManager);

        gridManager = new GridManager(gameSettings, enemyManager, this);

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