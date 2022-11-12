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
    private Dictionary<HitData, HitBox> activeHits;
    private List<Fighter> fightersToIgnore;

    /************************************************************************************************************************/

    private void Start()
    {
        if(activeHits == null)
            activeHits = new Dictionary<HitData, HitBox>();

        hitboxPool = PoolsManager.hitboxPool;
    }

    public void AddHitBox(HitData data)
    {
        HitBox hitBox = hitboxPool.ObtainObject(sprite.transform);
        
        hitBox.Activate(this, data, fightersToIgnore, controller.facingRight);
        activeHits.Add(data, hitBox);
    }

    public void RemoveHitBox(HitData data)
    {
        if (activeHits.TryGetValue(data, out var hitBox))
        {
            hitboxPool.RecycleObject(hitBox);
            activeHits.Remove(data);
        }
    }

    /************************************************************************************************************************/

    /// <summary>
    /// Clears all currently active <see cref="HitTrigger"/>s and the list of objects hit by the current attack.
    /// </summary>
    public void EndHitSequence()
    {
        //if (_FightersToIgnore == null)
        //    return;

        ClearHitBoxes();
    }

    // clears active hit boxes
    public void ClearHitBoxes()
    {
        if (activeHits != null)
        {
            foreach (var hitBox in activeHits.Values)
            {
                hitboxPool.RecycleObject(hitBox);
            }

            activeHits.Clear();
        }
    }
    
    /************************************************************************************************************************/
    #endregion
    /************************************************************************************************************************/
}
