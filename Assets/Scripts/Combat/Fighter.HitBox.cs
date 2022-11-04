using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlatformerGameKit;
using UnityEngine.Pool;

public partial class Fighter : MonoBehaviour
{
    // Base trigger class from PlatformerGameKit - by Kybernetik - https://kybernetik.com.au/platformer/docs/characters/animancer/
    
    /************************************************************************************************************************/
    #region Hit Boxes
    /************************************************************************************************************************/

    private ObjectPooling<HitBox> hitboxPool;
    private Dictionary<HitData, HitBox> _ActiveHits;
    //private HashSet<Hit.ITarget> _IgnoreHits;

    /************************************************************************************************************************/

    private void Start()
    {
        if(_ActiveHits == null)
            _ActiveHits = new Dictionary<HitData, HitBox>();

        hitboxPool = PoolsManager.hitboxPool;
    }

    public void AddHitBox(HitData data)
    {
        //if (_IgnoreHits == null)
        {
            //ObjectPool.Acquire(out _ActiveHits);
            //ObjectPool.Acquire(out _IgnoreHits);
        }

        // TODO: next up: fix hit triggers for non "Character"s
        HitBox hitBox = hitboxPool.ObtainObject(null, transform);

        hitBox.Activate(data, sprite.flipX);
        _ActiveHits.Add(data, hitBox);
    }

    public void RemoveHitBox(HitData data)
    {
        if (_ActiveHits.TryGetValue(data, out var hitBox))
        {
            hitboxPool.RecycleObject(hitBox);
            _ActiveHits.Remove(data);
        }
    }

    /************************************************************************************************************************/

    /// <summary>
    /// Clears all currently active <see cref="HitTrigger"/>s and the list of objects hit by the current attack.
    /// </summary>
    public void EndHitSequence()
    {
        //if (_IgnoreHits == null)   BIGGGGGG TODO
        //    return;

        ClearHitBoxes();
    }

    // clears active hit boxes
    public void ClearHitBoxes()
    {
        if (_ActiveHits != null)
        {
            foreach (var hitBox in _ActiveHits.Values)
            {
                hitboxPool.RecycleObject(hitBox);
            }

            _ActiveHits.Clear();
        }
    }
    
    /************************************************************************************************************************/
    #endregion
    /************************************************************************************************************************/
}
