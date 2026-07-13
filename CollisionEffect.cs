using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionEffect : MonoBehaviour
{
    [Header("Efek Tabrakan")]
    public GameObject collisionEffectPrefab;

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            if (collisionEffectPrefab != null)
            {
                ContactPoint contact = collision.contacts[0];
                Instantiate(collisionEffectPrefab, contact.point, Quaternion.identity);
            }

            Debug.Log("Efek tabrakan!");
        }
    }
}