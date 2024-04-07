using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;

public class FreezeTower : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private LayerMask enemyMask;

    [Header("Attribute")] 
    [SerializeField] private float targetingRange;
    [SerializeField] private float aps; //attacks per second
    [SerializeField] private float slowDuration = 0.5f;

    private float timeUntilFire;
    private float defaultAPS = 1f;
    private float defaultTargetingRange = 5f;
    public static UnityEvent onNewWave = new UnityEvent();

     private void Awake() {
        onNewWave.AddListener(ApplyNerfs);
    }

    private void Update() {

        timeUntilFire += Time.deltaTime;

        if (timeUntilFire >= 1f/aps) {
            SlowSpeed();
            timeUntilFire = 0f;
        }
    }

    private void SlowSpeed() {
        RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, targetingRange, 
        (Vector2)transform.position, targetingRange, enemyMask);

        if (hits.Length > 0) {
            for (int i = 0; i < hits.Length; i++) {
                RaycastHit2D hit = hits[i];
                EnemyMovement movement = hit.transform.GetComponent<EnemyMovement>();
                movement.ChangeSpeed(0.5f);

                StartCoroutine(ResetSpeed(movement));
            }
        }
    }

    private IEnumerator ResetSpeed(EnemyMovement movement) {
        yield return new WaitForSeconds(slowDuration);
        movement.ResetSpeed();
    }

    private void ApplyNerfs() {
        if (LevelManager.main.weather == "Heatwave") {
            aps = defaultAPS * LevelManager.main.diff_Factor;
            targetingRange = defaultTargetingRange;

        } else if (LevelManager.main.weather == "Snowstorm") {
            targetingRange = defaultTargetingRange * LevelManager.main.diff_Factor;
            aps = defaultAPS;

        // } else if (LevelManager.main.weather == "Rainstorm") {
        }
    }
    
    private void OnDrawGizmosSelected() {
        Handles.color = Color.cyan;
        Handles.DrawWireDisc(transform.position, transform.forward, targetingRange);
    }
}
