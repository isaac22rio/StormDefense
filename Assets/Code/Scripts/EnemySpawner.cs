using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemySpawner : MonoBehaviour
{
    public GameObject[] defaultEnemyPrefabs;

    [Header("References")]
    [SerializeField] private GameObject[] enemyPrefabs;

    [Header("Attributes")]
    [SerializeField] private int defaultEnemyNum = 8;
    [SerializeField] private float enemiesPerSecond = 1.8f;
    [SerializeField] private float waveInterval = 70f;

    [Header("Events")]
    public static UnityEvent onEnemyDestroy = new UnityEvent();
    private float timeSinceLastSpawn;
    private int enemiesAlive;
    private int enemiesLeft;
    private bool spawning = false;



    AudioManager audiomanager;


    private void Awake() {
        onEnemyDestroy.AddListener(EnemyDestroyed);

    }

    private void Start() {
        StartCoroutine(StartWave());
    }

    private void Update() {

        timeSinceLastSpawn += Time.deltaTime;

        if (!spawning) return;

        if ((timeSinceLastSpawn >= (1f / enemiesPerSecond)) && (enemiesLeft > 0)) {
            SpawnEnemy();
            enemiesLeft --;
            enemiesAlive ++;
            timeSinceLastSpawn = 0f;
        }

        if ((enemiesAlive == 0 && enemiesLeft == 0) || LevelManager.main.health <= 0) {
            EndWave();
            LevelManager.main.ChangeWeather();
        }
    }

    private void EndWave() {

        audiomanager.PlaySFX(audiomanager.newWave);

        spawning = false;
        timeSinceLastSpawn = 0f;
        LevelManager.main.waveNum++;
        StartCoroutine(StartWave());
    }

    private void EnemyDestroyed() {
        enemiesAlive--;
    }

    private void SpawnEnemy() {

        GameObject prefabToSpawn;

        if(LevelManager.main.waveNum  <= 2) {
            prefabToSpawn = enemyPrefabs[0];

        } else if (LevelManager.main.waveNum  <= 5) {
            int randint = Random.Range(0, 2);
            prefabToSpawn = enemyPrefabs[randint];

        } else if (LevelManager.main.waveNum  <= 10) {
            int randint = Random.Range(0, 3);
            prefabToSpawn = enemyPrefabs[randint];

        } else {
            int randint = Random.Range(0, 4);
            prefabToSpawn = enemyPrefabs[randint];
        }
        
        Instantiate(prefabToSpawn, LevelManager.main.startPoint.position, Quaternion.identity);
        
    }

    private IEnumerator StartWave() {


        yield return new WaitForSeconds(waveInterval);

        AudioManager audiomanager = GameObject.FindGameObjectWithTag("audio").GetComponent<AudioManager>();


        audiomanager.PlaySFX(audiomanager.newWave);
        spawning = true;
        enemiesLeft = EnemiesPerWave();
        Turret.onNewWave.Invoke();
    }

    private int EnemiesPerWave() {
        return Mathf.RoundToInt(defaultEnemyNum * Mathf.Pow(LevelManager.main.waveNum, LevelManager.main.diff_Factor));
    }
}
