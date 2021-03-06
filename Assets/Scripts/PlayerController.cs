﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 8f;
    public float jumpHeight = 1f;
    public float gravity = -9.81f;
    public int health = 100;
    public float hitCooldown = 2f;
    public Slider healthSlider;
    public GameObject settingsMenu;

    private Vector3 velocity;
    private CharacterController controller;
    private float lastHit;

    void Start()
    {
        controller = gameObject.GetComponent<CharacterController>();
        healthSlider.maxValue = health;
    }

    void Update()
    {
        bool isGrounded = controller.isGrounded;
        if (isGrounded && velocity.y < 0f)
        {
            velocity.y = -1f;
        }

        Vector3 move = transform.forward * Input.GetAxis("Vertical") + transform.right * Input.GetAxis("Horizontal");
        controller.Move(move.normalized * Time.deltaTime * moveSpeed);

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2.0f * gravity);
        }

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

        healthSlider.value = health;

        if (health <= 0)
        {
            Cursor.lockState = CursorLockMode.Confined;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }

        if (Input.GetButtonDown("Pause"))
        {
            settingsMenu.SetActive(!settingsMenu.activeSelf);
            if (settingsMenu.activeSelf)
            {
                Cursor.lockState = CursorLockMode.None;
            }
            else
            {
                Cursor.lockState = CursorLockMode.Locked;
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            if (Time.time - lastHit > hitCooldown)
            {
                health -= other.GetComponent<EnemyBehaviour>().damage;
                lastHit = Time.time;
            }
        }
    }
}
