using Core.Economy;
using System;
using TowerDefense.Level;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

public class LevelManager : Singleton<LevelManager>
{
    public WaveManager2 waveManager { get; protected set; }

    /// <summary>
    /// Fired when all the waves are done and there are no more enemies left
    /// </summary>
    public event Action levelCompleted;

    /// <summary>
    /// Fired when all of the home bases are destroyed
    /// </summary>
    public event Action levelFailed;

    public event Action onDamage;

    public int startingCurrency;

    /// <summary>
    /// The currency controller
    /// </summary>
    public Currency currency { get; protected set; }

    public int startingLimit;
    public TowerLimit limit { get; protected set; }

    public int startingHealth;
    public int Health { get; private set; }

    /// <summary>
    /// Caches the attached wave manager and subscribes to the spawning completed event
    /// Sets the level state to intro and ensures that the number of enemies is set to 0
    /// </summary>
    protected override void Awake()
    {
        base.Awake();

        currency = new Currency(startingCurrency);
        waveManager = GetComponent<WaveManager2>();
        limit = new TowerLimit(startingLimit);
        Health = startingHealth;
    }

    /// <summary>
    /// Calls the <see cref="levelCompleted"/> event
    /// </summary>
    public virtual void SafelyCallLevelCompleted()
    {
        if (levelCompleted != null)
        {
            levelCompleted();
        }
    }

    /// <summary>
    /// Calls the <see cref="levelFailed"/> event
    /// </summary>
    public virtual void SafelyCallLevelFailed()
    {
        if (levelFailed != null)
        {
            levelFailed();
        }
    }

    internal void TakeDamage()
    {
        Health--;
        if (onDamage!= null)
        {
            onDamage();
        }
        if (Health < 1)
        {
            GameOver();
        }
    }

    public void GameOver()
    {
        bool win = Health > 0;
        if (win)
        {
            SafelyCallLevelCompleted();
        }
        else
        {
            SafelyCallLevelFailed();
        }
    }
}
