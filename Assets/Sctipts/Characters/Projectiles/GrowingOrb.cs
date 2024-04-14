using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrowingOrb : Projectile
{
    [Header("GrowingOrb")]
    public float growSpeed;
    public float explosiveMultiplier;
    public int maxIncreaseRate;

    private CircleCollider2D projectileCollider;
    private float initialScale;

    protected override void Start()
    {
        projectileCollider = GetComponent<CircleCollider2D>();
        initialScale = transform.localScale.x;
        base.Start();
    }
    protected override void FixedUpdate()
    {
        if(initialScale * maxIncreaseRate >= transform.localScale.x) 
        {
            transform.localScale += new Vector3(growSpeed, growSpeed);
            projectileCollider.radius += growSpeed / 64;
        }
        base.FixedUpdate();
    }
    protected override void OnDangerTimeEnd()
    {
        Physics2D.OverlapCircleAll(transform.position, transform.localScale.x);
        base.OnDangerTimeEnd();
    }
}
