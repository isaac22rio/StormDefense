using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Rigidbody2D rb;


    [Header("Attributes")]
    [SerializeField] private float movementSpeed = 4f;

    private Transform target;
    private int pathIndex = 0;

    private float defaultSpeed;

    private void Start() {
        defaultSpeed = movementSpeed;
        target = LevelManager.main.path[pathIndex];
    }

    private void Update() {
        if (Vector2.Distance(target.position, transform.position) <= 0.1f) {
            pathIndex++;

            if (pathIndex == LevelManager.main.path.Length) {
                EnemySpawner.onEnemyDestroy.Invoke();
                LevelManager.main.TakeDamage(20);
                Destroy(gameObject);
                return;
            } else {
                target = LevelManager.main.path[pathIndex];
            }

    }
    }

   private void FixedUpdate() {
        Vector2 direction = (target.position - transform.position).normalized;
        rb.velocity = direction * movementSpeed;
    }

    public void ChangeSpeed(float newSpeed) {
        movementSpeed = newSpeed;

    }

    public void ResetSpeed() {
        movementSpeed = defaultSpeed;
    }
}


