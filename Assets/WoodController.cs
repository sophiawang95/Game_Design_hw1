using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WoodController : MonoBehaviour {
    public AudioSource woodSound;
    private float timeNow = 0f;
    private Vector3 oriP;
    private Quaternion oriR;
    private SpriteRenderer spriteRenderer;
    private Vector2 oriVelocity;
    // Use this for initialization
    private void Awake()
    {
        timeNow = 0f;
        spriteRenderer = GetComponent<SpriteRenderer>();
        oriP = this.transform.position;
        oriR = this.transform.rotation;
        oriVelocity = GetComponent<Rigidbody2D>().velocity;
    }
    void Start () {
        timeNow = 0f;
        spriteRenderer = GetComponent<SpriteRenderer>();
        oriP = this.transform.position;
        oriR = this.transform.rotation;
        oriVelocity = GetComponent<Rigidbody2D>().velocity;
    }
    public void Hide() {
        spriteRenderer.enabled = false;
        GetComponent<Collider2D>().enabled = false;
    }
    public void Reset()
    {
        GetComponent<Rigidbody2D>().velocity = oriVelocity;
        timeNow = 0f;
        this.transform.position = oriP;
        this.transform.rotation = oriR;
        spriteRenderer.enabled = true;
        GetComponent<Collider2D>().enabled = true;
    }
    // Update is called once per frame
    void Update () {
        timeNow += Time.deltaTime;
	}
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag != "Damager")
        {
            return;
        }
        if(timeNow >2f)
            woodSound.Play();
    }
    }
