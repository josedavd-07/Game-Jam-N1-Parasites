using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Virus : MonoBehaviour
{
    public Sprite[] animationSprites;
    public float animationTime = 1.0f;

    private SpriteRenderer spriteRenderer;
    private int animationFrame;
    public System.Action death;

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
            // Evitar error si death es null
            if (this.death != null)
            {
                this.death.Invoke();
            }

            // Desactivar el enemigo y la bala
            this.gameObject.SetActive(false);
            other.gameObject.SetActive(false);
        }
    }
}

