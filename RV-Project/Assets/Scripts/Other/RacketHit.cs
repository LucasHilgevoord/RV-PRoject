using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RacketHit : MonoBehaviour {
    RaycastCheck raycastCheck;
    RaycastHit2D hit;

    //private DuckDeath duckDeath;

    private Animator anim;
    private AudioSource aS;
    public Transform mHandMesh;
    private int killer;
    private bool isKilled;
    private bool isTargeted;

    float timer = 0f;
    float timeMultiplier = 1f;
    float duration;

    private Transform target = null;
    private Transform hitObj;

    void Start() {
        raycastCheck = GetComponent<RaycastCheck>();
    }

    void Update() {
        mHandMesh.position = Vector3.Lerp(mHandMesh.position, transform.position, Time.deltaTime * 15.0f);
        Shoot();
    }

    //Debug Target Drag
    private Vector3 screenPoint;
    private Vector3 offset;

    void OnMouseDown() {
        screenPoint = Camera.main.WorldToScreenPoint(gameObject.transform.position);
        offset = gameObject.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));
    }

    void OnMouseDrag() {
        Vector3 cursorPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
        Vector3 cursorPosition = Camera.main.ScreenToWorldPoint(cursorPoint) + offset;
        transform.position = cursorPosition;
    }

    void Shoot() {
        hit = raycastCheck.hit;
        hitObj = raycastCheck.RaycastChecker();

        if (hitObj != null) {
            target = hitObj;
            OnShoot();
            // initalisatie v/d timer
        } else if (target != null && (hitObj != target)) {
            // nieuwe target gevonden
            ResetTarget();
        } else if (hitObj == null && target != null) {
            // hand van target weg. timer resetten etc.
            ResetTarget();
        }
    }

    void ResetTarget() {
        anim.SetBool("Indicator", false);
        target = null;
        timer = 0;
    }

    void OnShoot() {
        //print(timer);
        //duration = target.GetComponent<DuckDeath>().killDuration;
        if (timer <= duration) {
            anim.SetBool("Indicator", true);
            timer += timeMultiplier * Time.deltaTime;
            //print(timer);
        } else {
            KillDuck();
            timer = 0;
        }
    }

    void KillDuck() {
        aS.Play();
        anim.SetBool("Indicator", false);

        if (gameObject.transform.childCount >= 3) {
            killer = 1;
        } else {
            killer = 2;
        }
        //hit.collider.gameObject.GetComponent<DuckDeath>().KillDuck(killer);
    }
}
