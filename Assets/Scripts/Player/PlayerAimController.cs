using System.Numerics;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAimController : MonoBehaviour
{
    [Header("Aim Inputs")]
    public InputAction AimUpOrDownAction;
    public InputAction AimAction;
    public InputAction ShootAction;

    PlayerController playerController;

    bool shouldAim = false;

    public void EnableAimControls()
    {
        AimUpOrDownAction.Enable();
        AimAction.Enable();
        ShootAction.Enable();
    }
    public void DisableAimControls()
    {
        AimUpOrDownAction.Disable();
        AimAction.Disable();
        ShootAction.Disable();
    }

    void HandleAimInput ()
    {
        if (playerController.NoMovementsStarted() && AimAction.IsPressed() && !shouldAim)
        {
            playerController.DisableMovementControls();
            shouldAim = true;
        }
        else if (AimAction.WasReleasedThisFrame() && shouldAim)
        {
            Debug.Log("No Longer Aiming!");
            shouldAim = false;
            playerController.EnableMovementControls();
        }

        if (!AimAction.enabled && shouldAim)
        {
            shouldAim = false;
        }

        Debug.Log("Aim Up or Down Input: " + AimUpOrDownAction.ReadValue<float>());
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerController = GetComponent<PlayerController>();
        EnableAimControls();
    }

    // Update is called once per frame
    void Update()
    {
        HandleAimInput();
    }
}
