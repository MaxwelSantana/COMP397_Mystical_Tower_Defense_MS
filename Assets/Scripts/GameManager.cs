/*
    Author: Maxwel Santana
    Student Number: 301294337
    File: GameManager
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [Header("Arrays")]
    public GameObject[] structureArray;

    public ShopTowerButton[] shopTowerButtons;

    [Header("Game Objects")]
    public GameObject mainPanel;
    public GameObject shop;
    public GameObject spawnPointOfBuildings;
    public GameObject gameOver;
    public GameObject youWinLabel;
    public GameObject youLostLabel;

    //Variables
    [HideInInspector]
    public bool buildingSelected;
    [HideInInspector]
    public int buildingNumber;
    [HideInInspector]
    public bool replaceMode;
    [HideInInspector]
    public bool destroyMode;

    [HideInInspector]
    public GameObject selectedBuilding;

    public int Wave { get; set; }

    void Start()
    {
        mainPanel.SetActive(true);
        shop.SetActive(false);
        gameOver.SetActive(false);
        youLostLabel.SetActive(false);
        youWinLabel.SetActive(false);

        Wave = 0;
    }


    //Buttons

    public void OpenShop()
    {
        mainPanel.SetActive(false);
        shop.SetActive(true);
        buildingSelected = true;

        foreach (var item in shopTowerButtons)
        {
            int currentCurrency = LevelManager.instance.currency.currentCurrency;
            bool enableItem = currentCurrency >= item.cost;
            item.EnableButton(enableItem);
        }
    }

    public void PlayAgain()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene("Level1");
        Time.timeScale = 1.0f;
    }

    public void BuyBuilding(int number)
    {
        int buildingNumber = number;
        shop.SetActive(false);
        mainPanel.SetActive(true);
        GameObject house = Instantiate(structureArray[buildingNumber], spawnPointOfBuildings.transform.position, Quaternion.identity);

        selectedBuilding = house;
        Building buildingScript = house.GetComponent<Building>();
        buildingScript.buildingState = Building.BuildingState.firstStart;
        buildingScript.mouseDrag = true;
        buildingSelected = true;
    }
}