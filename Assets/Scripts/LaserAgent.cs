using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors.Reflection;

public class LaserAgent : Agent
{
    public Room room;
    Rigidbody2D rb;
    LaserLogic logic;
    [SerializeField]
    int health;
    public RoomSettings settings;
    [Observable]
    public Vector2 RigidBodyVelocity
    {
        get { return (Vector2)rb.velocity; }
    }

    public override void Initialize()
    {
        rb = GetComponent<Rigidbody2D>();
        logic = GetComponent<LaserLogic>();
        health = settings.maxHealth;
    }

    public override void OnEpisodeBegin()
    {
        room.ResetRoom();
        logic.ResetLogic();
        health = settings.maxHealth;
        transform.localPosition = Vector3.zero;
        transform.localEulerAngles = Vector3.zero;
        rb.velocity = Vector2.zero;
    }

    public override void OnActionReceived(ActionBuffers actionBuffers)
    {
        ActionSegment<int> act = actionBuffers.DiscreteActions;

        // 0: left, 1: up, 2: right, 3: down
        Vector2 vec = new Vector2(act[2] - act[0], act[1] - act[3]);

        float len = vec.magnitude;
        if (len == 0) { len = 1; }

        rb.AddForce(vec / len * settings.moveForce);
        TurnTo(rb.velocity);
        Constraint();

        AddReward(health * settings.surviveReward * Time.fixedDeltaTime);
    }

    void TurnTo(Vector2 vel)
    {
        float angle = Mathf.Atan2(vel.y, vel.x) * 180 / Mathf.PI - 90;
        transform.localEulerAngles = new Vector3(0f, 0f, angle);
    }

    void Constraint()
    {
        float x = transform.position.x;
        float y = transform.position.y;
        float rad = settings.playerRad;

        x = Mathf.Clamp(x, settings.minSpawnPos.x + rad, settings.maxSpawnPos.x - rad);
        y = Mathf.Clamp(y, settings.minSpawnPos.y + rad, settings.maxSpawnPos.y - rad);

        transform.position = new Vector3(x, y, 0f);
    }

    public override void Heuristic(in ActionBuffers actionsOut)
    {
        int left = 0;
        int up = 0;
        int right = 0;
        int down = 0;

        float horiz = Input.GetAxisRaw("Horizontal");
        if (horiz > 0) { right = 1; }
        if (horiz < 0) { left = 1; }
        float verti = Input.GetAxisRaw("Vertical");
        if (verti > 0) { up = 1; }
        if (verti < 0) { down = 1; }

        var discreteActionsOut = actionsOut.DiscreteActions;
        discreteActionsOut[0] = left;
        discreteActionsOut[1] = up;
        discreteActionsOut[2] = right;
        discreteActionsOut[3] = down;
    }

    public void Damage()
    {
        health--;
        AddReward(-1 * settings.damagePenalty);
        if (health <= 0)
        {
            EndEpisode();
        }
    }
}
