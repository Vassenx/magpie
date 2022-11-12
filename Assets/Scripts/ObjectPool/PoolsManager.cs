using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.SceneManagement;
using UnityEngine;

public class PoolsManager : MonoBehaviour
{
    public static ObjectPooling<HitBox> hitboxPool;
    [SerializeField] private HitBox hitBoxPrefab;
    
    public static ObjectPooling<Projectile> projPool;
    [SerializeField] private Projectile defaultProjPrefab;
    
    public static PoolsManager Instance { get; private set; }
    
    private void Awake() 
    { 
        if (Instance != null && Instance != this) 
        { 
            Destroy(this); 
        } 
        else
        { 
            Instance = this; 
        }

        InitPools();
    }
    
    private void InitPools()
    {
        hitboxPool = new ObjectPooling<HitBox>(hitBoxPrefab);
        projPool = new ObjectPooling<Projectile>(defaultProjPrefab);
    }
}
