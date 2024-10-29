using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;


public class Interaction : MonoBehaviour
{
    public float checkRate = 0.05f;
    private float lastCheckTime;
    public float maxCheckDistance;
    public LayerMask layerMask;

    public GameObject curInteractGameObjact;
    private IInteractable interactable;
    

    public TextMeshProUGUI promptText;
    private Camera camera;

    // Start is called before the first frame update
    void Start()
    {
        camera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.time - lastCheckTime > checkRate)
        {
            lastCheckTime = Time.time;
            Ray ray = camera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2));
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, maxCheckDistance, layerMask))
            {
                if (hit.collider.gameObject != curInteractGameObjact)
                {
                    curInteractGameObjact = hit.collider.gameObject;
                    interactable = curInteractGameObjact.GetComponent<IInteractable>();
                    SetPromptText();
                }
            }
            else
            {
                curInteractGameObjact = null;
                interactable = null;
                promptText.gameObject.SetActive(false);

            }
        }

       
    }

    private void SetPromptText()
    {
        promptText.gameObject.SetActive(true);
        promptText.text = interactable.GetInteractPrompt();
    }

    public void OnInteractInput(InputAction.CallbackContext context)
    {
        if(context.phase == InputActionPhase.Started && interactable != null)
        {
            interactable.OnInteract();
            curInteractGameObjact = null;
            interactable = null;
            promptText.gameObject.SetActive(false);
        }
    }
}