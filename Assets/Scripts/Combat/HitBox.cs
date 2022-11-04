using System;
using System.Collections;
using System.Collections.Generic;
using PlatformerGameKit;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(PolygonCollider2D))]
public class HitBox : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;

    [SerializeField] private PolygonCollider2D polyCollider2D;
    public PolygonCollider2D PolyCollider2D => polyCollider2D;

    [SerializeField] private Fighter fighter;
    private HitData hitData;
    
    private void Awake()
    {
        polyCollider2D.isTrigger = true;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        Fighter target = col.GetComponentInParent<Fighter>();
        if (target != null && target != fighter)
        {
            target.OnTakeDamage(hitData);
        }
    }

    public void Activate(HitData data, bool isLeft)
    {
        hitData = data;
        UpdateColliderDirection(isLeft, hitData.Area);
    }
    
    private void UpdateColliderDirection(bool isLeft, Vector2[] area)
    {
        if (!isLeft)
        {
            PolyCollider2D.points = area;
        }
        else
        {
            var count = area.Length;
            Vector2[] points = new Vector2[count];

            for (int i = 0; i < count; i++)
            {
                var point = area[i];
                point.x = -point.x;
                points[i] = point;
            }

            PolyCollider2D.SetPath(0, points);
        }
    }
}
