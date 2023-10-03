using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //ScriptableObjects
    [SerializeField] private GridSettings gridSettings;

    //Lists
    private List<IUpdateable> updateables = new List<IUpdateable>();
    private List<IFixedUpdateable> fixedUpdateables = new List<IFixedUpdateable>();

    //////////////////////////////////////////////////

    public void Awake()
    {
        if (gridSettings == null)
        {
            Debug.LogError("GameManager has no reference to gridSettings");
        }
    }

    public void Start()
    {
        Add(new GridManager(gridSettings));
    }

    /////////////////////////////////////////////////

    public void Add(IUpdateable script)
    {
        updateables.Add(script);

        if (script is IFixedUpdateable)
        {
            fixedUpdateables.Add(script as IFixedUpdateable);
        }
    }

    public void Remove(IUpdateable script)
    {
        updateables.Remove(script);

        if (script is IFixedUpdateable)
        {
            fixedUpdateables.Remove(script as IFixedUpdateable);
        }
    }

    ////////////////////////////////////////////////////////////

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