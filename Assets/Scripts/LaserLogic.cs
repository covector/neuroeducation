using UnityEngine;

public class LaserLogic : MonoBehaviour
{
    LaserAgent agent;
    Animator animator;
    float damageTimer = 0f;
    public RoomSettings settings;

    void Start()
    {
        agent = GetComponent<LaserAgent>();
        animator = GetComponent<Animator>();
        animator.enabled = settings.blinkAnim;
    }

    void Update()
    {
        if (damageTimer >= 0f)
        {
            damageTimer -= Time.deltaTime;
        }
        else
        {
            animator.SetBool("Damaged", false);
        }
    }

    public void ResetLogic()
    {
        damageTimer = 0f;
        animator.SetBool("Damaged", false);
    }

    void OnTriggerStay2D(Collider2D collider)
    {
        if (collider.tag == "Laser" && damageTimer < 0f && collider.GetComponent<Laser>().threat)
        {
            damageTimer = settings.damageCoolDown;
            animator.SetBool("Damaged", true);
            agent.Damage();
        } 
    }
}
