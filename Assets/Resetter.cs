using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;

public class Resetter : MonoBehaviour {
    public Rigidbody2D projectile;
    public float resetSpeed = 0.025f;
    private float resetSpeedSqr;
    private SpringJoint2D spring;
    private float resetTimes = 0f;
    public ProjectilectionDragging projectilectionDragging;
    public Text gameoverui;
    public TargetDamage targetDamage;
    public List<WoodController> woodC;
    // Use this for initialization
    void Start () {
        resetSpeedSqr = resetSpeed * resetSpeed;
        spring = projectile.GetComponent<SpringJoint2D>();
        resetTimes = 0f;
        woodC[1].Hide();
        woodC[2].Hide();
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.R)) {
            Reset();
        }
        if (spring.enabled == false && projectile.velocity.sqrMagnitude < resetSpeedSqr) {
            Reset();
        }

	}
    void OnTriggerExit2D(Collider2D other) {
        if (other.GetComponent<Rigidbody2D>() == projectile) {
            Reset();
        }
    }
    private void Reset()
    {
        resetTimes++;
        if (!targetDamage.getisKill())
        {
            if (resetTimes <= 2f)
            {
                projectilectionDragging.Reset();
            }
            else
            {
                gameoverui.text = "Game Over !";
            }
        }
        else {

            if (targetDamage.gethowmanykill() >= 2) {
                gameoverui.text = "Win !";
            }else{
                resetTimes = 0;
                gameoverui.text = "Game2 !";
                projectilectionDragging.Reset();
                for (int i = 0; i < woodC.Count; i++)
                {
                    woodC[i].Reset();
                }
                targetDamage.Reset();
            }
            

        }
    }


}

