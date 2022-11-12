using System.Collections.Generic;
using PlatformerGameKit;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(PolygonCollider2D))]
public class HitBox : IHitBox
{
    private HitData hitData;
    
    public void Activate(Fighter fighter, HitData data, List<Fighter> ignore, bool isRight)
    {
        hitData = data;
        Activate(fighter, hitData.Damage, ignore);
        
        UpdateColliderDirection(isRight, hitData.Area);
    }

    private void FixedUpdate()
    {
        // If the parent has been destroyed, they can no longer hit anything
        var parent = transform.parent;
        if (parent == null)
        {
            PoolsManager.hitboxPool.RecycleObject(this);
            return;
        }

        transform.SetPositionAndRotation(parent.position, parent.rotation);
    }
    
    private void UpdateColliderDirection(bool isRight, Vector2[] area)
    {
        if (isRight)
        {
            PolygonCollider2D polyTrigger = (PolygonCollider2D)trigger;
            polyTrigger.points = area;
        }
        else
        {
            // flip facing
            /*Vector3 scale = transform.localScale;
            transform.localScale = new Vector3(-scale.x, scale.y, scale.z);
            
            // flip shape 
            var count = area.Length;
            Vector2[] points = new Vector2[count];

            for (int i = 0; i < count; i++)
            {
                var point = area[i];
                point.x = -point.x;
                points[i] = point;
            }

            PolyCollider2D.enabled = false;
            PolyCollider2D.SetPath(0, points);
            PolyCollider2D.enabled = true;*/
        }
    }
}
