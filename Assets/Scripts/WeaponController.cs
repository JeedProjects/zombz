using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    public Transform cameraTransform;
    public float range = 100f;
    public float knockback = 3f;
    public int damage = 34;
    public AudioClip fireSound;
    public float fireDelay;

    private Animator animator;
    private AudioSource sound;
    private float lastShot = 0;

    private void Start()
    {
        animator = gameObject.GetComponent<Animator>();
        sound = gameObject.GetComponent<AudioSource>();
    }

    void Update()
    {
        if (Input.GetButtonDown("Fire"))
        {
            if (Time.time - lastShot >= fireDelay)
            {
                lastShot = Time.time;
                animator.SetTrigger("Shoot");
                sound.PlayOneShot(fireSound);
                RaycastHit hit;
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out hit, range))
                {
                    if (hit.rigidbody != null)
                    {
                        hit.rigidbody.velocity = (ray.direction + Vector3.up/2) * knockback;
                    }
                    if (hit.collider.tag == "Enemy")
                    {
                        hit.transform.gameObject.GetComponent<EnemyBehaviour>().health -= damage;
                    }
                }
            }
        }

        if (Input.GetButtonDown("Inspect"))
        {
            animator.SetTrigger("Inspect");
        }
    }
}
