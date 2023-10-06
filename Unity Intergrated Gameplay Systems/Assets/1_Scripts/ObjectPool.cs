using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.Pool;
using System.Collections;

public class ObjectPool<T> where T : IPoolable
{
    private List<T> activePool = new List<T>();
    private List<T> inactivePool = new List<T>();
    private List<T> itemsToDeactivate = new List<T>();

    public T RequestObject(Vector2 _pos)
    {
        if (inactivePool.Count <= 0)
        {
            Debug.LogError("No More Inactive Pool Items. Increase Pool Size");
            return default (T);
        } 
        else
        {
			T curPool = inactivePool[0];
			ActivateItem(curPool);
			return curPool;
		}
    }

    public void UpdateItem()
    {
        for (int i = 0; i < itemsToDeactivate.Count; i++)
        {
            DeactivateItemPrivate(itemsToDeactivate[i]);
            itemsToDeactivate.RemoveAt(i);
            i--;
        }

        foreach (IUpdateable item in activePool)
        {
            item.OnUpdate();
        }
    }

    public void ActivateItem(T item)
    {
        item.EnablePoolabe();
        if (inactivePool.Contains(item))
        {
            inactivePool.Remove(item);
        }
        activePool.Add(item);
    }

    public void DeactivateItem(T item)
    {
        itemsToDeactivate.Add(item);
    }

    private void DeactivateItemPrivate(T item)
    {
        item.DisablePoolabe();
        if (activePool.Contains(item))
        {
            activePool.Remove(item);
        }
        inactivePool.Add(item);
    }
}
