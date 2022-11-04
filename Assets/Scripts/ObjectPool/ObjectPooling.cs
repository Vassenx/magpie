using System;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooling<TObject> : IDisposable where TObject : MonoBehaviour // TObject is a component, but will pool its whole object
{
    private Stack<TObject> pooledObjects;
    
    private TObject prefab;
    private int initialCount;
        
    public ObjectPooling(TObject prefab, int count = 5)
    {
        pooledObjects = new Stack<TObject>();
        
        this.prefab = prefab;
        initialCount = count;
        IncreaseSize(initialCount);
    }
    
    public TObject ObtainObject(Transform transf, Transform parent = null)
    {
        TObject obj = PopObject();
        if (obj != default)
        {
            if(!obj.isActiveAndEnabled)
            {
                obj.gameObject.SetActive(true);
            }

            Transform objTransf = obj.transform;
            if (parent != null)
            {
                objTransf.SetParent(parent);
            }

            if (transf != null)
            {
                objTransf.position = transf.position;
                objTransf.rotation = transf.rotation;
            }
        }

        return obj;
    }

    public void RecycleObject(TObject obj)
    {
        if(obj.isActiveAndEnabled)
        {
            obj.gameObject.SetActive(false);
        }
        
        obj.transform.SetParent(PoolsManager.Instance.transform);
        
        PushObject(obj);
    }
    
    /**************************************************************************/
    
    private TObject PopObject()
    {
        if (pooledObjects.Count == 0)
        {
            IncreaseSize(initialCount);
        }

        TObject obj;
        if (pooledObjects.TryPop(out obj))
        {
            return obj;
        }
        
        return default;
    }

    private void PushObject(TObject obj)
    {
        if (!pooledObjects.Contains(obj))
        {
            pooledObjects.Push(obj);
        }
        else
        {
            Debug.LogError("Pushed object to obj pool that already exists in pool" + obj.name);
        }
    }

    private void IncreaseSize(int amountToIncrease)
    {
        for (int i = 0; i < amountToIncrease; i++)
        {
            TObject newObj;
            
            if (prefab != null)
            {
                newObj = GameObject.Instantiate(prefab);
            }
            else
            {
                var t = typeof(TObject);
                newObj = new GameObject(t.Name  + "ObjPool").AddComponent<TObject>();
            }

            RecycleObject(newObj);
        }
    }
    
    public void Clear()
    {
        foreach (TObject obj in pooledObjects)
            GameObject.Destroy(obj);
        
        pooledObjects.Clear();
    }

    public void Dispose() => this.Clear();
}
