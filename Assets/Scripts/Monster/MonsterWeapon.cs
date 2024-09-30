using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterWeapon : MonoBehaviour
{
    int damage = 1;
    public Collider weaponCollider;

    private void Start()
    {
        weaponCollider = GetComponent<Collider>();
        weaponCollider.enabled = false;
    }
    //private void OnTriggerEnter(Collider other)
    //{
    //    Debug.Log(other.tag);
    //    Vector3 incomingDirection = transform.position - other.transform.position;
    //    incomingDirection = incomingDirection.normalized;

    //    Debug.Log(incomingDirection);

    //    other.GetComponent<PlayerController>().Hit(incomingDirection);
    //    other.GetComponent<IDamageable>().TakeDamage(damage);
    //}
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Vector3 hitDirection = collision.contacts[0].normal;
            hitDirection = -hitDirection.normalized;

            collision.gameObject.GetComponent<PlayerController>().Hit(hitDirection, damage);
        }
        else if(collision.gameObject.layer == LayerMask.NameToLayer("Shield"))
        {
        }
    }
    //private void OnCollisionStay(Collision collision)
    //{
    //    if (collision.gameObject.tag == "Player")
    //    {
    //        Vector3 hitDirection = collision.contacts[0].normal;

    //        hitDirection = -hitDirection.normalized;

    //        collision.gameObject.GetComponent<PlayerController>().Hit(hitDirection);
    //        collision.gameObject.GetComponent<IDamageable>().TakeDamage(damage);
    //    }
    //}
}
