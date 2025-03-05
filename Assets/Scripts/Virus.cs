﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Virus : MonoBehaviour
{
    public Sprite[] animationSprites;
    public float animationTime = 1.0f;
    public ParticleSystem deathVfx;
    private SpriteRenderer spriteRenderer;
    private int animationFrame;
    public System.Action death;

    public GameObject powerUpPrefab; // 🔹 Prefab del Power-Up (se asigna en el Inspector)
    private float dropChance = 0.2f; // 🔹 Probabilidad del 20% de soltar Power-Up

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        InvokeRepeating(nameof(AnimateSprite), this.animationTime, this.animationTime);
    }

    private void AnimateSprite()
    {
        animationFrame++;

        if (animationFrame >= this.animationSprites.Length)
        {
            animationFrame = 0;
        }

        spriteRenderer.sprite = this.animationSprites[animationFrame];
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Bullet")) // Asegurar que el nombre de la capa es correcto
        {
            if (this.death != null)
            {
                this.death.Invoke();
            }

            // 🔹 Generar Power-Up con 20% de probabilidad
            if (powerUpPrefab != null && Random.value < dropChance)
            {
                Instantiate(powerUpPrefab, transform.position, Quaternion.identity);
                Debug.Log("🔹 Power-Up generado en: " + transform.position);
            }

            if (deathVfx != null)
            {
                deathVfx.transform.SetParent(null); // Separar el efecto del virus
                deathVfx.Play();
                AudioManager.Instance.PlaySFX("EnemyExplosion");
                Destroy(deathVfx.gameObject, deathVfx.main.duration); // Destruir la partícula después de su duración
            }

            this.gameObject.SetActive(false); // Desactivar el enemigo
            other.gameObject.SetActive(false); // Desactivar la bala
        }
    }
}
