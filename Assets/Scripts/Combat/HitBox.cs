using System.Collections.Generic;
using PlatformerGameKit;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(PolygonCollider2D))]
public class HitBox : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;

    [SerializeField] private PolygonCollider2D polyCollider2D;
    public PolygonCollider2D PolyCollider2D => polyCollider2D;

    [SerializeField] private Fighter ownerFighter;
    private HashSet<Fighter> fightersToIgnore;
    private HitData hitData;
    
    private void Awake()
    {
        polyCollider2D.isTrigger = true;
    }
    
    public void Activate(HitData data, Fighter fighter, HashSet<Fighter> ignore, bool isRight)
    {
        hitData = data;
        ownerFighter = fighter;
        fightersToIgnore = ignore;
        UpdateColliderDirection(isRight, hitData.Area);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        Fighter target = GetTarget(col);
        
        if (target == null || target == ownerFighter || 
            (fightersToIgnore != null && fightersToIgnore.Contains(target))) // !target.CanBeHit(ref this)
            return;

        //if (dontHitAgain)
        fightersToIgnore?.Add(target);
        
        target.OnTakeDamage(hitData);
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
            PolyCollider2D.points = area;
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

    public static Fighter GetTarget(Component component)
        => GetTarget(component.gameObject);

    public static Fighter GetTarget(GameObject gameObject)
        => gameObject.transform.parent == null ? gameObject.GetComponent<Fighter>() : gameObject.GetComponentInParent<Fighter>();
}
