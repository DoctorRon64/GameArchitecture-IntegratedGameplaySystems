using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    private InputHandler inputHandler;

    public void Awake()
    {
        if (gameSettings == null)
        {
            Debug.LogError("GameManager has no reference to gridSettings");
        }
    }

    public void Start()
    {
        gridManager = new GridManager(gameSettings);

        //bullet
        bulletManager = new BulletManager(gridManager, gameSettings);
        AddUpdate(bulletManager);
        AddFixedUpdate(bulletManager);

        //player and input
        inputHandler = new InputHandler();
        AddUpdate(inputHandler);

        AddFixedUpdate(new PlayerData(inputHandler));
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