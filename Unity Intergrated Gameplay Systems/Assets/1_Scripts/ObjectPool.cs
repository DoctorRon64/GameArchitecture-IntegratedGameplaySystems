using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.Pool;

public class ObjectPool<T> where T : IPoolable
{
    private List<T> activePool = new List<T>();
    private List<T> inactivePool = new List<T>();

    public T RequestObject(Vector2 _pos)
    {
        if (inactivePool.Count <= 0)
        {
            return default (T);
        } 
        else
        {
			T curPool = inactivePool[0];
			ActivateItem(curPool);
			return curPool;
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
        item.DisablePoolabe();
        if (activePool.Contains(item))
        {
            activePool.Remove(item);
        }
        inactivePool.Add(item);
    }
}
