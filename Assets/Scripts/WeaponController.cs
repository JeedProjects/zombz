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

    private Animator animator;
    private AudioSource sound;

    private void Start()
    {
        animator = gameObject.GetComponent<Animator>();
        sound = gameObject.GetComponent<AudioSource>();
    }

    void Update()
    {
        if (Input.GetButtonDown("Fire"))
        {
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
                    if (hit.transform.gameObject.GetComponent<EnemyBehaviour>().health <= 0)
                    {
                        Destroy(hit.transform.gameObject);
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
