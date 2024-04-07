using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    [Header("Attribute")] 
    [SerializeField] private int defaultHP = 1;
    [SerializeField] private int coinValue = 15;


    private bool isDestroyed = false;
    private int HP;

    public void Awake() {
        getHP();
    }

    public void getHP() {
        HP = defaultHP;
    }

    public void TakeDamage(int dmg) {
        HP -= dmg;

        if (HP <= 0 && !isDestroyed) {
            EnemySpawner.onEnemyDestroy.Invoke();
            LevelManager.main.addCoins(coinValue);
            isDestroyed = true;
            Destroy(gameObject);

        }
    }
}
