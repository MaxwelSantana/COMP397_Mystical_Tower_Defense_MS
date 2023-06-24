using Core.Economy;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LimitTowerUI : MonoBehaviour
{
    private TowerLimit limit;

    /// <summary>
    /// The text element to display information on
    /// </summary>
    public Text display;
    // Start is called before the first frame update
    void Start()
    {
        if (LevelManager.instance != null)
        {
            limit = LevelManager.instance.limit;

            UpdateDisplay();
            limit.towerCountChanged += UpdateDisplay;
        }
        else
        {
            Debug.LogError("[UI] No level manager to get currency from");
        }
    }

    protected void UpdateDisplay()
    {
        display.text = $"{limit.towerCount}/{limit.limit}";
    }
}
