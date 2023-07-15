/*
    Author: Maxwel Santana
    Student Number: 301294337
    File: Building
*/

using Core.Economy;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Building : MonoBehaviour
{
    public enum BuildingState { staying, replacing, updating, firstStart, destroying };
    [HideInInspector]
    public BuildingState buildingState;
    
    //Variables
    [HideInInspector]
    public bool notToMove;
    [HideInInspector]
    public bool mouseDrag;
    [HideInInspector]
    Vector3 currentPosition;
    private float zCordinateOfMouse;
    private Vector3 mouseOffset;

    [Header("Scripts")]
    private GameManager gameManager;
    private SpriteRenderer spriteRenderer;

    [Header("Game Objects")]
    public Button yesButton;
    public GameObject yesNoPanel;
    public GameObject updatePanel;

    public Tile tile;

    [System.Serializable]
    public class Update
    {
        public Sprite updateLevelSprite;
    }
    public Update[] updates;
    public int nextUpdate;

    private BuildingData buildingData;
    private Currency m_Currency;
    private TowerLimit m_TowerLimit;


    void Start()
    {
        currentPosition = transform.position;

        //Get the scripts
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        buildingData = gameObject.GetComponentInChildren<BuildingData>();
        m_Currency = LevelManager.instance.currency;
        m_TowerLimit = LevelManager.instance.limit;
    }

    void FixedUpdate()
    {
        if (buildingState == BuildingState.staying)
        {
            yesNoPanel.SetActive(false);
            updatePanel.SetActive(false);
            //slayers.number = -0.2f;
            tile.stayingState = true;
        }
        if (buildingState == BuildingState.replacing)
        {
            yesNoPanel.SetActive(true);
            updatePanel.SetActive(false);
            //slayers.number = 0.2f;
            tile.stayingState = false;

            if (tile.isOcupied == true)
            {
                yesButton.interactable = false;
            }
            else
            {
                yesButton.interactable = true;
            }
        }
        if (buildingState == BuildingState.updating)
        {
            yesNoPanel.SetActive(false);
            if (nextUpdate + 1 != updates.Length)
            {
                updatePanel.SetActive(true);
            }
            else
            {
                updatePanel.SetActive(false);
            }

            tile.stayingState = true;
        }
        if (buildingState == BuildingState.firstStart)
        {
            yesNoPanel.SetActive(true);
            updatePanel.SetActive(false);
            tile.stayingState = false;
        }
        if (buildingState == BuildingState.destroying)
        {
            yesNoPanel.SetActive(true);
            updatePanel.SetActive(false);
            tile.stayingState = true;
        }
        if (gameManager.selectedBuilding != gameObject)
        {
            buildingState = BuildingState.staying;
        }

        spriteRenderer.sprite = updates[nextUpdate].updateLevelSprite;

    }

    public void YesButton()
    {
        if (buildingState == BuildingState.firstStart)
        {
            buildingState = BuildingState.staying;
            currentPosition = transform.position;
            mouseDrag = false;
            gameManager.mainPanel.SetActive(true);
            m_TowerLimit.AddTower();

            if (!m_Currency.TryPurchase(buildingData.CurrentLevel.cost))
            {
                Destroy(gameObject);
            }
        }
        if (buildingState == BuildingState.replacing)
        {
            buildingState = BuildingState.staying;
            mouseDrag = false;
            gameManager.mainPanel.SetActive(false);

            currentPosition = transform.position;
        }
        if (buildingState == BuildingState.destroying)
        {
            buildingState = BuildingState.staying;
            mouseDrag = false;
            gameManager.mainPanel.SetActive(false);

            Destroy(gameObject);
        }
        gameManager.buildingSelected = false;
        gameManager.selectedBuilding = null;
    }

    public void NoButton()
    {
        if (buildingState == BuildingState.firstStart)
        {
            Destroy(gameObject);
            gameManager.mainPanel.SetActive(true);
        }
        if (buildingState == BuildingState.replacing)
        {
            transform.position = currentPosition;
            buildingState = BuildingState.staying;
            mouseDrag = false;
        }
        if (buildingState == BuildingState.destroying)
        {
            buildingState = BuildingState.staying;
            gameManager.mainPanel.SetActive(false);
            mouseDrag = false;
        }
        gameManager.buildingSelected = false;
        gameManager.selectedBuilding = null;
    }

    public void UpdateButton()
    {
        if (m_Currency.TryPurchase(buildingData.getNextLevel().cost))
            nextUpdate++;
        buildingState = BuildingState.staying;
        gameManager.selectedBuilding = null;
        gameManager.buildingSelected = false;
    }

    void OnMouseDown()
    {
        zCordinateOfMouse = Camera.main.WorldToScreenPoint(transform.position).z;
        mouseOffset = transform.position - mouseWorldPos();
        if (gameManager.buildingSelected == false)
        {
            if (gameManager.replaceMode == true)
            {
                buildingState = BuildingState.replacing;
                gameManager.buildingSelected = true;
                mouseDrag = true;
            }
            else if (gameManager.destroyMode == true)
            {
                buildingState = BuildingState.destroying;
                gameManager.buildingSelected = true;
                mouseDrag = false;
            }
            else
            {
                if (nextUpdate + 1 != updates.Length)
                {
                    buildingState = BuildingState.updating;
                    gameManager.buildingSelected = true;
                }
                mouseDrag = false;
            }
            gameManager.selectedBuilding = gameObject;
        }
    }

    void OnMouseDrag()
    {
        if (mouseDrag == true)
        {
            transform.position = mouseWorldPos() + mouseOffset;
        }
    }

    private Vector3 mouseWorldPos()
    {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = zCordinateOfMouse;
        return Camera.main.ScreenToWorldPoint(mousePosition);
    }
}
