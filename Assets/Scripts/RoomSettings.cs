using UnityEngine;

public class RoomSettings : MonoBehaviour
{
    public float moveForce;
    public float spawnCoolDown;
    public float damageCoolDown;
    public int maxHealth;
    public bool staticLaser;
    public bool blinkAnim;
    [HideInInspector]
    public Vector2 minSpawnPos;
    [HideInInspector]
    public Vector2 maxSpawnPos;
    [HideInInspector]
    public float playerRad;

    private void Start()
    {
        minSpawnPos = new Vector2(-8.8f, -4.9f);
        maxSpawnPos = new Vector2(8.8f, 4.9f);
        playerRad = 0.6f;
    }
}
