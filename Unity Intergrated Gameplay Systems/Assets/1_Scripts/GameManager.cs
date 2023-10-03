using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //Lists
    private List<IUpdateable> updateables = new List<IUpdateable>();
    private List<IFixedUpdateable> fixedUpdateables = new List<IFixedUpdateable>();

    public void Start()
    {
        Add(new TileManager());
    }

    public void Add(IStartable script)
    {
        script.OnStart();

        if (script is IUpdateable)
        {
            updateables.Add(script as IUpdateable);
        }

        if (script is IFixedUpdateable)
        {
            fixedUpdateables.Add(script as IFixedUpdateable);
        }
    }

    public void Remove(IStartable script)
    {
        if (script is IUpdateable)
        {
            updateables.Remove(script as IUpdateable);
        }

        if (script is IFixedUpdateable)
        {
            fixedUpdateables.Remove(script as IFixedUpdateable);
        }
    }

    /////////////////////////////////////////////////

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