using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(PlayerMotor))]
public class PlayerController : MonoBehaviour
{
    public Interactable focus;
    public LayerMask movementMask;
    Camera cam;
    PlayerMotor motor;

    // Start is called before the first frame update
    void Awake()
    {
        DontDestroyOnLoad(gameObject);

        cam = Camera.main;
        motor = GetComponent<PlayerMotor>();
    }

    // Update is called once per frame
    void Update()
    {
        if (EventSystem.current.IsPointerOverGameObject() || DialogueManager.GetInstance().dialogueIsPlaying)
        {
            return;
         }

        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 100, movementMask))
            {
                motor.moveToPoint(hit.point,null);
                removeFocus();
            }
        }


        if (Input.GetMouseButtonDown(1))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 100))
            {
                //check if ray hit interactable object
                Interactable interactable = hit.collider.GetComponent<Interactable>();

                if (interactable != null)
                {
                    setFocus(interactable);
                }
            }
        }

    }

    void setFocus(Interactable newFocus)
    {
        if (newFocus != focus)
        {
            if (focus != null)
            {
                focus.onDefocused();
            }
            
            focus = newFocus;
            motor.moveToPoint(focus.transform.position,focus);
        }

        focus.onFocused(transform); 
    }

    void removeFocus()
    {
        if (focus != null)
        {
            focus.onDefocused();
        }

        focus = null;
    }
}
