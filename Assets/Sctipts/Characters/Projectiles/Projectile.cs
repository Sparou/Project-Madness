using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [Header("Damage")]
    [SerializeField] public float damage;
    [SerializeField] public float force;

    [Header("Life Time")]
    [SerializeField] public float lifeTime;
    [SerializeField] public float dangerTime;

    private Rigidbody2D rb;
    private Player target;
    private float lifeTimeTimer = 0f;
    private float dangerTimeTimer = 0f;

    protected Vector3 direction;

    protected virtual void Start()
    {
        target = FindFirstObjectByType<Player>();
        rb = GetComponent<Rigidbody2D>();
        direction = (target.transform.position - transform.position).normalized;
        rb.AddForce(direction * force, ForceMode2D.Force);
    }

    protected virtual void FixedUpdate()
    {
        lifeTimeTimer += Time.deltaTime;
        dangerTimeTimer += Time.deltaTime;

        if (dangerTimeTimer >= dangerTime) OnDangerTimeEnd();
        if (lifeTimeTimer >= lifeTime) OnLifeTimeEnd();
    }

    protected virtual void OnDangerTimeEnd()
    {
        GetComponent<AIDistanceAttackTrigger>().enabled = false;
        GetComponent<Collider2D>().enabled = false;
    }

    protected virtual void OnLifeTimeEnd()
    {
        Destroy(gameObject);
    }

    public Player GetTarget() { return target; }
}
