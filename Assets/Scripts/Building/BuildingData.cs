using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BuildingLevel
{
    public int cost;
    public GameObject bullet;
    public float fireRate;
}
public class BuildingData : MonoBehaviour
{
    public List<BuildingLevel> levels;
    private BuildingLevel currentLevel;

    public BuildingLevel CurrentLevel { get { return currentLevel; } }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnEnable()
    {
        currentLevel = levels[0];
    }

    public BuildingLevel getNextLevel()
    {
        int currentLevelIndex = levels.IndexOf(currentLevel);
        int maxLevelIndex = levels.Count - 1;
        if (currentLevelIndex < maxLevelIndex)
        {
            return levels[currentLevelIndex + 1];
        }
        else
        {
            return null;
        }
    }

    public void increaseLevel()
    {
        int currentLevelIndex = levels.IndexOf(currentLevel);
        if (currentLevelIndex < levels.Count - 1)
        {
            currentLevel = levels[currentLevelIndex + 1];
        }
    }
}
