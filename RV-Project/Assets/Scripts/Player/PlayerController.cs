using UnityEngine.EventSystems;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public Interactable focus;

    public Rigidbody rigidbody;
    public float speed;

    Camera cam;
    PlayerMotor motor;


	// Use this for initialization
	void Start () {
        cam = Camera.main;
        motor = GetComponent<PlayerMotor>();
	}
	
	// Update is called once per frame
	void Update () {

        if (EventSystem.current.IsPointerOverGameObject())
            return;

        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
        rigidbody.velocity = movement * speed;

        //RemoveFocus();

        if (Input.GetMouseButtonDown(1))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 100))
            {
                Interactable interactable = hit.collider.GetComponent<Interactable>();
                if(interactable != null)
                {
                    SetFocus(interactable);
                }
            }
        }
    }

    void SetFocus(Interactable newFocus)
    {
        if(newFocus != focus)
        {
            if(focus != null)
            {
                focus.OnDeFocused();
            }
            motor.FollowTarget(newFocus);
            focus = newFocus;
        }
        newFocus.OnFocused(transform);
    }

    void RemoveFocus()
    {
        if(focus != null)
            focus.OnDeFocused();

        focus = null;
        motor.StopFollowingTarget();
        
    }
}
