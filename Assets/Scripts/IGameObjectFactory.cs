using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IGameObjectFactory
{
    public GameObject CreateWaveEnemy(Wave wave, Vector3 position);
}
