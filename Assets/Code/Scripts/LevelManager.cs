using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    public static LevelManager main;
    public Transform[] path;
    public Transform startPoint;
    private Camera mainCamera;
    public string weather;

    [Header("Attributes")]
    [SerializeField] public float diff_Factor = 0.75f;
    [SerializeField] private GameObject LoseMenu;
    public int waveNum = 1;
    public int coins;
    public GameObject RainEffect;
    public GameObject SnowEffect;
    public GameObject Fire;
    public Image healthBar;
    private AudioManager audiomanager;




    private GameObject currentWeatherEffect;
    public float health = 100f;

    private void Start() {
        coins = 100;
    }

    private void Update() {
        if (LevelManager.main.health <= 0) {
            Time.timeScale = 0;
            LoseMenu.SetActive(true);
        }
    }

    public void addCoins(int amount) {
        coins += amount;
    }

    public void Home() {
        Time.timeScale = 1;
        SceneManager.LoadSceneAsync(0);

    }

    public void TakeDamage(float damage) {
        LevelManager.main.health -= damage;
        healthBar.fillAmount = LevelManager.main.health / 100f;
    }

    public bool subtractCoins(int amount) {
        if (amount > coins) {
            Debug.Log("You do not have enough coins to purchase this.");
            return false;
        } else {
            coins -= amount;
            return true;
        }
    }

    public string GetCurrentWeather()
    {
        return weather;
    }


    private void Awake()
    {
        main = this;
        mainCamera = Camera.main;
        audiomanager = GameObject.FindGameObjectWithTag("audio").GetComponent<AudioManager>();

    }

    void SpawnWeather(GameObject weatherEffect)
    {
        if (currentWeatherEffect != null)
        {
            Destroy(currentWeatherEffect);
        }

        int middleIndex = path.Length / 2;
        Vector3 middlePoint = path[middleIndex].position;
        currentWeatherEffect = Instantiate(weatherEffect, middlePoint, Quaternion.identity);
    }

    public void ChangeWeather()
    {
        int randint;
        if (LevelManager.main.waveNum <= 8) {
            randint = Random.Range(0, 4);
            switch (randint)
            {
                case 0:
                    weather = "Clear";
                    SpawnWeather(null);
                    break;
                case 1:
                    weather = "Snowstorm";
                    SpawnWeather(SnowEffect);
                    break;
                case 2:
                    weather = "Heatwave";
                    SpawnWeather(Fire);
                    break;
            } 

        } else {
            randint = Random.Range(0, 3);
            switch (randint)
            {
                case 0:
                    weather = "Snowstorm";
                    SpawnWeather(SnowEffect);
                    break;
                case 1:
                    weather = "Heatwave";
                    SpawnWeather(Fire);
                    break;
                case 2:
                    weather = "Rainstorm";
                    SpawnWeather(RainEffect);
                    break;
            } 

        }
    }
}
