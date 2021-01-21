using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using Unity.MLAgents.Actuators;

public class LaserAgent : Agent
{
    Rigidbody2D rb;

    public override void Initialize()
    {
        Debug.Log("init");
        rb = GetComponent<Rigidbody2D>();
    }

    public override void OnEpisodeBegin()
    {
        Debug.Log("episodebegin");
        transform.localPosition = new Vector3(0f, 0f, 0f);
        transform.localEulerAngles = new Vector3(0f, 0f, 0f);
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        Debug.Log("observe");
        sensor.AddObservation(0);
    }

    public override void OnActionReceived(ActionBuffers actionBuffers)
    {
        Debug.Log("action");
        MoveAgent(actionBuffers.DiscreteActions);
    }

    void MoveAgent(ActionSegment<int> act)
    {
        // 0: left, 1: up, 2: right, 3: down
        int horiz = act[2] - act[0];
        int verti = act[1] - act[3];
        Debug.Log(horiz);
        rb.AddForce(new Vector2(10 * horiz, 100 * verti));
    }

    public override void Heuristic(in ActionBuffers actionsOut)
    {
        Debug.Log("heru");
        int left = 0;
        int up = 0;
        int right = 0;
        int down = 0;

        float horiz = Input.GetAxisRaw("Horizontal");
        if (horiz > 0) { right = 1; }
        if (horiz < 0) { left = 1; }
        float verti = Input.GetAxisRaw("Vertical");
        if (verti > 0) { up = 1; }
        if (verti > 0) { down = 1; }
        Debug.Log(left);

        var discreteActionsOut = actionsOut.DiscreteActions;
        discreteActionsOut[0] = left;
        discreteActionsOut[1] = up;
        discreteActionsOut[2] = right;
        discreteActionsOut[3] = down;
    }
}
