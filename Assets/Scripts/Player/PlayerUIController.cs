using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerUIController : MonoBehaviour
{
    public InputAction PlayerMenuAction;

    PlayerController playerController;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        PlayerMenuAction.Enable();
        playerController = GetComponent<PlayerController>();
    }

    void HandlePlayerMenuInput () 
    {
        if(PlayerMenuAction.WasPressedThisFrame()){
            if(PlayerMenuCanvasController.Instance.gameObject.activeSelf){
                playerController.EnableMovementControls();
                PlayerMenuCanvasController.Instance.ClosePlayerMenu();
                PlayerMenuCanvasController.Instance.gameObject.SetActive(false);
            }
            else{
                PlayerMenuCanvasController.Instance.gameObject.SetActive(true);
                playerController.DisableMovementControls();
                PlayerMenuCanvasController.Instance.OpenItemsMenu();
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        HandlePlayerMenuInput();   
    }
}
