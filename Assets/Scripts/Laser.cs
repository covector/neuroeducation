using UnityEngine;

public class Laser : MonoBehaviour
{
    public float urgency;
    public bool threat = false;
    public void Spawn()
    {
        threat = true;
    }
    public void Destroy()
    {
        Destroy(gameObject);
    }
}
