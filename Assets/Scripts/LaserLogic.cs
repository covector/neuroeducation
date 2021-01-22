using UnityEditor.Animations;
using UnityEngine;

public class LaserLogic : MonoBehaviour
{
    LaserAgent agent;
    Animator animator;
    float damageTimer = 0f;
    public int health;
    public RoomSettings settings;

    void Start()
    {
        agent = GetComponent<LaserAgent>();
        health = settings.maxHealth;
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
        damageTimer = 0;
        health = settings.maxHealth;
        animator.SetBool("Damaged", false);
    }

    void OnTriggerStay2D(Collider2D collider)
    {
        if (collider.tag == "Laser" && damageTimer < 0f && collider.GetComponent<Laser>().threat)
        {
            health--;
            damageTimer = settings.damageCoolDown;
            animator.SetBool("Damaged", true);
        }
        if (health <= 0)
        {
            agent.Damaged(health);
        }
    }
}
