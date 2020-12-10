using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WeaponController : MonoBehaviour
{
    public float range = 100f;
    public float knockback = 3f;
    public int damage = 34;
    public AudioClip fireSound;
    public float fireDelay;
    public int magazineSize = 6;
    public int remainingBullets;
    public GameObject ammoCountText;

    private Animator animator;
    private AudioSource sound;
    private float lastShot = 0;

    private void Start()
    {
        animator = gameObject.GetComponent<Animator>();
        sound = gameObject.GetComponent<AudioSource>();
        remainingBullets = magazineSize;
    }

    void Update()
    {
        if (Input.GetButtonDown("Reload"))
        {
            animator.SetTrigger("Reload");
            remainingBullets = magazineSize;
            ammoCountText.GetComponent<TextMeshProUGUI>().SetText(remainingBullets.ToString());
        }
        
        if (Input.GetButtonDown("Fire"))
        {
            if (Time.time - lastShot >= fireDelay && remainingBullets > 0 && animator.GetCurrentAnimatorClipInfo(0)[0].clip.name != "Reload" && transform.parent.transform.parent.GetComponent<PlayerController>().settingsMenu.activeSelf == false)
            {
                remainingBullets -= 1;
                ammoCountText.GetComponent<TextMeshProUGUI>().SetText(remainingBullets.ToString());
                lastShot = Time.time;
                animator.SetTrigger("Shoot");
                sound.PlayOneShot(fireSound);
                RaycastHit hit;
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out hit, range))
                {
                    if (hit.rigidbody != null)
                    {
                        hit.rigidbody.velocity = (ray.direction + Vector3.up) * knockback;
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
