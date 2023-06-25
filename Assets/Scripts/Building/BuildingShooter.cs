/*
    Author: Maxwel Santana
    Student Number: 301294337
    File: BuildingShooter
*/


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingShooter : MonoBehaviour
{
    public List<GameObject> enemiesInRange;

    private float lastShotTime;
    private BuildingData buildingData;
    private Building building;

    // Use this for initialization
    void Start()
    {
        enemiesInRange = new List<GameObject>();
        lastShotTime = Time.time;
        buildingData = gameObject.GetComponentInChildren<BuildingData>();
        building = gameObject.GetComponentInChildren<Building>();
    }

    // Update is called once per frame
    void Update()
    {
        if (building.buildingState != Building.BuildingState.staying) return;

        GameObject target = null;

        float minimalEnemyDistance = float.MaxValue;
        foreach (GameObject enemy in enemiesInRange)
        {
            float distanceToGoal = enemy.GetComponent<MoveEnemy>().DistanceToGoal();
            if (distanceToGoal < minimalEnemyDistance)
            {
                target = enemy;
                minimalEnemyDistance = distanceToGoal;
            }
        }

        if (target != null)
        {
            if (Time.time - lastShotTime > buildingData.CurrentLevel.fireRate)
            {
                Shoot(target.GetComponent<Collider2D>());
                lastShotTime = Time.time;
            }
            
            /*
            Vector3 direction = gameObject.transform.position - target.transform.position;
            gameObject.transform.rotation = Quaternion.AngleAxis(
                Mathf.Atan2(direction.y, direction.x) * 180 / Mathf.PI,
                new Vector3(0, 0, 1));
            */
        }
    }

    private void OnEnemyDestroy(GameObject enemy)
    {
        enemiesInRange.Remove(enemy);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag.Equals("Enemy"))
        {
            enemiesInRange.Add(other.gameObject);
            //EnemyDestructionDelegate del = other.gameObject.GetComponent<EnemyDestructionDelegate>();
            //del.enemyDelegate += OnEnemyDestroy;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag.Equals("Enemy"))
        {
            enemiesInRange.Remove(other.gameObject);
            //EnemyDestructionDelegate del = other.gameObject.GetComponent<EnemyDestructionDelegate>();
            //del.enemyDelegate -= OnEnemyDestroy;
        }
    }

    private void Shoot(Collider2D target)
    {
        GameObject bulletPrefab = buildingData.CurrentLevel.bullet;
        
        Vector3 startPosition = gameObject.transform.position;
        Vector3 targetPosition = target.transform.position;
        startPosition.z = bulletPrefab.transform.position.z;
        targetPosition.z = bulletPrefab.transform.position.z;

        GameObject newBullet = (GameObject)Instantiate(bulletPrefab);
        newBullet.transform.position = startPosition;
        Bullet bulletComp = newBullet.GetComponent<Bullet>();
        bulletComp.target = target.gameObject;
        bulletComp.startPosition = startPosition;
        bulletComp.targetPosition = targetPosition;

        /*
        Animator animator = buildingData.CurrentLevel.visualization.GetComponent<Animator>();
        animator.SetTrigger("fireShot");
        AudioSource audioSource = gameObject.GetComponent<AudioSource>();
        audioSource.PlayOneShot(audioSource.clip);
        */
    }
}
