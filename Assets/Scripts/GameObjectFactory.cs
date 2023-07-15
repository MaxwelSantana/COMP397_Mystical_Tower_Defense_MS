using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameObjectFactory : IGameObjectFactory
{
    public GameObject CreateWaveEnemy(Wave wave, Vector3 position)
    {
        return GameObject.Instantiate(wave.enemyPrefab, position, Quaternion.identity);
    }
}
