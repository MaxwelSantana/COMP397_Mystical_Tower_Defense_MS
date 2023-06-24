using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerLimit
{
    public int limit { get; private set; }
    public int towerCount { get; private set; }

    public Action towerCountChanged;

    public TowerLimit(int startingLimit)
    {
        this.limit = startingLimit;
    }

    public void AddTower()
    {
        towerCount++;
        if (towerCountChanged!=null)
        {
            towerCountChanged();
        }
    }
}
