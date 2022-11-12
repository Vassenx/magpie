using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

namespace Magpie
{
    public class ObjectPooling<TObject> : IDisposable
        where TObject : MonoBehaviour // TObject is a component, but will pool its whole object
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

        public TObject ObtainObject(Transform parent = null)
        {
            TObject obj = PopObject();
            if (obj != default)
            {
                if (parent != null)
                {
                    obj.gameObject.SetActive(false);
                    var objTransform = obj.transform;

                    objTransform.SetParent(parent, false);

                    // otherwise won't take on the parent's scale (ex: parent = (2,2,2), normally it goes to (0.5,0.5,0.5))
                    // probs not always wanted, TODO
                    objTransform.localScale = Vector3.one;
                }

                obj.gameObject.SetActive(true);
            }

            return obj;
        }

        public void RecycleObject(TObject obj)
        {
            if (PoolsManager.Instance != null && PoolsManager.Instance.isActiveAndEnabled)
            {
                obj.transform.SetParent(PoolsManager.Instance.transform);
            }

            if (obj.isActiveAndEnabled)
            {
                obj.gameObject.SetActive(false);
            }

            obj.transform.localPosition = Vector3.zero;

            PushObject(obj);
        }

        public void RecycleObject(TObject obj, float waitTime) => obj.StartCoroutine(WaitToRecycle(waitTime, obj));

        private IEnumerator WaitToRecycle(float waitTime, TObject obj)
        {
            yield return new WaitForSeconds(waitTime);
            RecycleObject(obj);
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
                    newObj = new GameObject(t.Name + "ObjPool").AddComponent<TObject>();
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

        public void Dispose() => Clear();
    }
}
