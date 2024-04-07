using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Shop : MonoBehaviour
{
    [Header("References")]
    [SerializeField] TextMeshProUGUI coinsUI;
    [SerializeField] TextMeshProUGUI levelUI;
    [SerializeField] Animator _animation;
    private bool shopOpen = true;

    public void ToggleMenu() {
        shopOpen = !shopOpen;
        _animation.SetBool("MenuOpen", shopOpen);
    }

    private void OnGUI() {
        coinsUI.text = LevelManager.main.coins.ToString();
        levelUI.text = LevelManager.main.waveNum.ToString();
    }
}
