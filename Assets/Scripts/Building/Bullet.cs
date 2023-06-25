/*
    Author: Maxwel Santana
    Student Number: 301294337
    File: Bullet
*/

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 10;
    public int damage  = 10;
    public GameObject target;
    public Vector3 startPosition;
    public Vector3 targetPosition;

    private float distance;
    private float startTime;

    private GameManager gameManager;

    void Start()
    {
        startTime = Time.time;
        distance = Vector2.Distance(startPosition, targetPosition);
        GameObject gm = GameObject.Find("GameManager");
        gameManager = gm.GetComponent<GameManager>();
    }

    void Update()
    {
        float timeInterval = Time.time - startTime;
        gameObject.transform.position = Vector3.Lerp(startPosition, targetPosition, timeInterval * speed / distance);

        if (gameObject.transform.position.x == targetPosition.x && gameObject.transform.position.y == targetPosition.y)
        {
            try {
                if (target != null)
                {
                    Transform healthBarTransform = target.transform.Find("HealthBar");
                    HealthBar healthBar = healthBarTransform.gameObject.GetComponent<HealthBar>();
                    healthBar.currentHealth -= Mathf.Max(damage, 0);

                    if (healthBar.currentHealth <= 0)
                    {
                        Destroy(target);
                        if (LevelManager.instance != null)
                            LevelManager.instance.currency.AddCurrency(1);
                    }
                }
            } catch (Exception e) { print(e.Message); }
            Destroy(gameObject);
        }
    }

    private bool IsInsideRange(Vector3 position, Vector3 targetPosition)
    {
        return true;
    }
}
