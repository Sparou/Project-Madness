using UnityEngine;

public class AttackColliderTrigger : MonoBehaviour
{
    private float damage;

    private void Start()
    {
        damage = GetComponentInParent<Weapon>().WeaponDamage;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (LayerMask.LayerToName(collision.gameObject.layer) == "Characters")
        {
            Debug.Log(collision.gameObject.name);
            Character character = collision.GetComponent<Character>();
            character.healthController.TakeDamage(damage);
        }
    }
}
