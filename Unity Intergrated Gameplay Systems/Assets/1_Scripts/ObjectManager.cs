using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectManager : IUpdateable, IFixedUpdateable
{
    private ObjectsInScene objectsInScene;

    public ObjectManager(ObjectsInScene _allObjects)
    {
        objectsInScene = _allObjects;
    }
    
    public void OnUpdate()
    {
        objectsInScene.player.OnUpdate();
    }

    public void OnFixedUpdate()
    {
        objectsInScene.player.OnFixedUpdate();
    }
}
