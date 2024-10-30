using System;
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
        promptText = SceneLoader.Instance.uiModule.TextPromptObject.GetComponent<TextMeshProUGUI>();
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
                    curInteractGameObjact.GetComponent<ItemObject>().InitPrompt += SetPromptText;
                    curInteractGameObjact.GetComponent<ItemObject>().Init();
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

    private void SetPromptText(string str)
    {
        promptText.gameObject.SetActive(true);
        promptText.text = str;
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
