using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plot : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Color hoverColor;

    private GameObject turret;
    private Color startColor;



    AudioManager audioManager;


    private void Start() {
        startColor = spriteRenderer.color;

    }

    private void OnMouseEnter() {
        spriteRenderer.color = hoverColor;
    }

    private void OnMouseExit() {
        spriteRenderer.color = startColor;
    }

    public void OnMouseDown() {
        if (turret != null) return;
        Tower currentTurret = BuildManager.main.GetSelectedTurret();

        if (currentTurret.price > LevelManager.main.coins) {
            AudioManager audiomanager = GameObject.FindGameObjectWithTag("audio").GetComponent<AudioManager>();

            audiomanager.PlaySFX(audiomanager.noCoins);


            Debug.Log("You cant afford this");
            return;
        }

        LevelManager.main.subtractCoins(currentTurret.price);

        turret = Instantiate(currentTurret.prefab, transform.position, Quaternion.identity);


    }
}