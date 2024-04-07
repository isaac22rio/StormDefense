using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildManager : MonoBehaviour
{
    public static BuildManager main;
    
    [Header("References")]
    [SerializeField] private Tower[] towers;

    private int selectedTurretIndex = 0;

    private void Awake() {
        main = this;
    }

    public Tower GetSelectedTurret() {
        return towers[selectedTurretIndex];
    }

    public void SetSelectedTurret(int _selectedTurretIndex) {
        selectedTurretIndex = _selectedTurretIndex;
    }
}
