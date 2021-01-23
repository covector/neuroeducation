using UnityEngine;

public class RoomSettings : MonoBehaviour
{
    [Header("Environment")]
    public float moveForce;
    public float spawnCoolDown;
    public float damageCoolDown;
    public int maxHealth;
    public bool staticLaser;
    [Header("Cosmetics")]
    public bool blinkAnim;
    [Header("Rewards")]
    public float surviveReward;
    public float damagePenalty;
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
