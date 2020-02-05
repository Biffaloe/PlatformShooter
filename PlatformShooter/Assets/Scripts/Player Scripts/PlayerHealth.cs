using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public int startingHealth = 100;
    public int currentHealth;
    public Slider healthSlider;
    public Image damageImage;
    public Color flashColor = new Color(1f, 0f, 0f, 0.1f);

    bool isDead;
    bool damaged;
    AudioSource hurt;

    CameraControllerPlatfroming cameraController;
    GameObject mainCamera;

    private void Start()
    {
        hurt = GetComponent<AudioSource>();
        mainCamera = GameObject.Find("Main Camera");
        cameraController = mainCamera.GetComponent<CameraControllerPlatfroming>();
    }
    private void Awake()
    {
        currentHealth = startingHealth;
        isDead = false;
    }

    private void Update()
    {
        if (damaged)
        {
            damageImage.color = flashColor;
        }
        else
        {
            damageImage.color = Color.Lerp(damageImage.color, Color.clear, 2 * Time.deltaTime);
        }
        damaged = false;
    }

    public void TakeDamage (int amount)
    {
        damaged = true;
        currentHealth -= amount;
        healthSlider.value = currentHealth;
        hurt.Play();


        if(currentHealth <=0 && !isDead)
        {
            Death();
        }
    }

    void Death()
    {
        isDead = true;

        var playerController = this.GetComponentInParent<PlayerController>();
        var meshrender = this.GetComponentInParent<MeshRenderer>();

        meshrender.enabled = false;
        playerController.enabled = false;
        cameraController.enabled = false;
    }
}
