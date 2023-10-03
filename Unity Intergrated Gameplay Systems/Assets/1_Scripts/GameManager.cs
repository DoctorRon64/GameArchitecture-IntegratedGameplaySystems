using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    TileManager tileManager;

    private void Awake()
    {
        
    }

    void Start()
    {
        
    }

    void Update()
    {
        tileManager.OnUpdate();
    }

    private void FixedUpdate()
    {
        
    }
}
