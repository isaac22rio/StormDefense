using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public class Bullet : MonoBehaviour
{
    
    [Header("References")]
    [SerializeField] private Rigidbody2D rb;

    [Header("Attributes")]
    [SerializeField] private float bulletSpeed = 8f;
    [SerializeField] private float bulletDamage;
    private float lifetime = 0;

    private Transform target;

    public void Update() {

        lifetime += Time.deltaTime;
        if (lifetime > 5) {
            Destroy(gameObject);
        }
    }

    public void SetTarget(Transform _target) {
        target = _target;
    }

    public void SetDamage(float damage) {
        bulletDamage = damage;
    }

    private void FixedUpdate() {
        if (!target) return;

        Vector2 direction = (target.position - transform.position).normalized;
        rb.velocity = direction * bulletSpeed;
    }

    private void OnCollisionEnter2D(Collision2D other) {
        other.gameObject.GetComponent<Enemy>().TakeDamage((int)bulletDamage);
        Destroy(gameObject);
    }
}
