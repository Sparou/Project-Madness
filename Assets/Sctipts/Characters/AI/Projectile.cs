using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] public float damage;
    [SerializeField] public float force;
    [SerializeField] public float lifeTime;

    private Rigidbody2D rb;
    private Player target;
    private float lifeTimeTimer = 0f;

    void Start()
    {
        target = FindFirstObjectByType<Player>();

        rb = GetComponent<Rigidbody2D>();
        var direction = (target.transform.position - transform.position).normalized;
        rb.AddForce(direction * force);
    }

    void FixedUpdate()
    {
        lifeTimeTimer += Time.deltaTime;
        if (lifeTimeTimer >= lifeTime) 
        { 
            Destroy(gameObject); 
        }
    }

    public Player GetTarget() { return  target; }
}
