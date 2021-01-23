using UnityEngine;
using System.Collections;

public class Room : MonoBehaviour
{
    public GameObject laserObj;
    public Transform staticLasers;
    public Transform laserContainer;
    float spawnTimer = 0f;
    RoomSettings settings;

    void Start()
    {
        settings = GetComponent<RoomSettings>();
        foreach (Transform laser in staticLasers)
        {
            laser.gameObject.SetActive(settings.staticLaser);
        }
    }

    void Update()
    {
        spawnTimer += Time.deltaTime;
        if (spawnTimer > settings.spawnCoolDown)
        {
            SpawnLaser();
            spawnTimer -= settings.spawnCoolDown;
        }
    }

    public void ResetRoom()
    {
        // Destroy all remaining lasers
        foreach (Transform laser in laserContainer)
        {
            Destroy(laser.gameObject);
        }

        // Reset timer
        spawnTimer = 0f;
    }

    void SpawnLaser()
    {
        // Random position
        float randX = Random.Range(settings.minSpawnPos.x, settings.maxSpawnPos.x);
        float randY = Random.Range(settings.minSpawnPos.y, settings.maxSpawnPos.y);
        Vector3 randpos = transform.position + new Vector3(randX, randY, 0);

        // Random rotation
        Quaternion randrot = Quaternion.Euler(0f, 0f, Random.Range(-90f, 90f) + transform.eulerAngles.z);

        // Spawn
        Instantiate(laserObj, randpos, randrot, laserContainer);
    }
}
