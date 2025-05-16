using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerUIController : MonoBehaviour
{
    public InputAction PlayerMenuAction;

    PlayerController playerController;
    PlayerAimController playerAimController;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        PlayerMenuAction.Enable();
        playerController = GetComponent<PlayerController>();
        playerAimController = GetComponent<PlayerAimController>();
    }

    void HandlePlayerMenuInput () 
    {
        if(PlayerMenuAction.WasPressedThisFrame()){
            if(PlayerMenuCanvasController.Instance.gameObject.activeSelf){
                playerController.EnableMovementControls();
                playerAimController.EnableAimControls();
                PlayerMenuCanvasController.Instance.ClosePlayerMenu();
                PlayerMenuCanvasController.Instance.gameObject.SetActive(false);
            }
            else{
                PlayerMenuCanvasController.Instance.gameObject.SetActive(true);
                playerAimController.DisableAimControls();
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
