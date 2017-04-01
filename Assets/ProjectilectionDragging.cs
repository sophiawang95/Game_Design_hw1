using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectilectionDragging : MonoBehaviour {
    public float maxStretch = 3.0f;
    public LineRenderer catapultLineFront;
    public LineRenderer catapultLineBack;
    private SpringJoint2D spring;
    private Transform catapult;
    private bool clickedOn = false;
    private bool hadclicked = false;
    private Ray rayToMouse;
    private Ray leftC;
    private float max;
    private Vector2 preVelocity;
    private Vector2 oriVelocity;
    private float circleRadius;
    public AudioSource springSound;
    private Vector3 oriP;
    private bool isthereSpring;


    private void Awake()
    {
        spring = GetComponent<SpringJoint2D>();
        catapult = spring.connectedBody.transform;
        
    }
    // Use this for initialization
    void Start () {
        isthereSpring = true;
        LineRendererSetup();
        oriP = this.transform.position;
        oriVelocity = GetComponent<Rigidbody2D>().velocity;
        rayToMouse = new Ray(catapult.position, Vector3.zero);
        leftC = new Ray(catapultLineFront.transform.position, Vector3.zero);
        max = maxStretch * maxStretch;
        circleRadius = this.GetComponent<CircleCollider2D>().radius;
        hadclicked = false;
        catapultLineBack.sortingLayerName = "BackBall";
        catapultLineFront.sortingLayerName = "BeforeBall";
    }

    // Update is called once per frame
    void Update() {
        if (clickedOn) {
            Dragging();
        }

        if (isthereSpring == true)
        {
            if (hadclicked && (this.transform.position - catapult.position).sqrMagnitude < 1)
            {
                GetComponent<Rigidbody2D>().gravityScale = 2;
                GetComponent<Rigidbody2D>().velocity = preVelocity;
                hadclicked = false;
                springSound.Play();
                spring.enabled = false;
                isthereSpring = false;

            }

            if (!clickedOn)
            {
                preVelocity = GetComponent<Rigidbody2D>().velocity;
            }
            LineRendereUpdate();
        }
        else
        {
            catapultLineBack.enabled = true;
            catapultLineFront.enabled = true;
        }

	}
    public void Reset()
    {
        isthereSpring = true;
        this.transform.position = oriP;
        GetComponent<Rigidbody2D>().velocity = oriVelocity;
        GetComponent<Rigidbody2D>().gravityScale = 0;
        catapultLineBack.enabled = true;
        catapultLineFront.enabled = true;
        spring.enabled = true;
        LineRendererSetup();
        rayToMouse = new Ray(catapult.position, Vector3.zero);
        leftC = new Ray(catapultLineFront.transform.position, Vector3.zero);
    }
    void LineRendererSetup() {
        catapultLineFront.SetPosition(0, catapultLineFront.transform.position);
        catapultLineBack.SetPosition(0, catapultLineBack.transform.position);

    }

    private void OnMouseDown()
    {
        spring.enabled = false;
        clickedOn = true;
    }

    private void OnMouseUp()
    {
        spring.enabled = true;
        clickedOn = false;
        hadclicked = true;
}

    void Dragging() {
        Vector3 mouseWorldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 catapultToMouse = mouseWorldPoint - catapult.position;
        if (catapultToMouse.sqrMagnitude > max) {
            rayToMouse.direction = catapultToMouse;
            mouseWorldPoint = rayToMouse.GetPoint(maxStretch);
        }
        mouseWorldPoint.z = 0f;
        this.transform.position = mouseWorldPoint;
    }

    void LineRendereUpdate(){
        Vector2 catapultToProjectile = this.transform.position - catapultLineFront.transform.position;
        leftC.direction = catapultToProjectile;
        Vector3 holdPoint = leftC.GetPoint(catapultToProjectile.magnitude + circleRadius);
        catapultLineFront.SetPosition(1, holdPoint);
        catapultLineBack.SetPosition(1, holdPoint);

        }

}
