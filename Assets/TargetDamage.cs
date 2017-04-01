using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetDamage : MonoBehaviour {
    public int hitpoints = 1;
    public Sprite damagedSprite;
    public float damageImpactSpeed;
    private int currentHitPoints;
    private float damageImpactSpeedSqr;
    private SpriteRenderer spriteRenderer;
    public ParticleSystem playerKillEffect;
    public AudioSource playerKillSound;
    private bool iskill;
    private float howmanykill;
    // Use this for initialization
    void Start () {
        spriteRenderer = GetComponent<SpriteRenderer>();
        currentHitPoints = hitpoints;
        damageImpactSpeedSqr = damageImpactSpeed * damageImpactSpeed;
        iskill = false;
        howmanykill = 0f;

    }
    public void Reset()
    {
        iskill = false;
        spriteRenderer.enabled = true;
        GetComponent<Collider2D>().enabled = true;
        playerKillEffect.gameObject.SetActive(false);
    }
    // Update is called once per frame
    void OnCollisionEnter2D (Collision2D collision) {
        if (collision.collider.tag != "Damager") {
            return;
        }
        if (collision.relativeVelocity.sqrMagnitude < damageImpactSpeedSqr) {
            return;
        }

        currentHitPoints--;

        if (currentHitPoints <= 0) {
            Kill();
        }


	}
    public bool getisKill() {
        return iskill;
    }

    public float gethowmanykill() {
        return howmanykill;
    }

    void Kill() {        

        playerKillEffect.transform.position = this.transform.position;
        playerKillEffect.gameObject.SetActive(true);
        playerKillSound.Play();
        spriteRenderer.enabled = false;
        GetComponent<Collider2D>().enabled = false;
        iskill = true;
        howmanykill++;



    }
}
