using UnityEngine;

public class TeleportationManager : MonoBehaviour
{

    /// <summary>
    /// The button used to begin aiming for a teleport.
    /// </summary>
    [Tooltip("The button used to begin aiming for a teleport.")]
    public OVRInput.RawButton AimButton = OVRInput.RawButton.A;

    /// <summary>
    /// The controller used to aim.
    /// </summary>
    [Tooltip("The button used to begin aiming for a teleport.")]
    public OVRInput.Controller AimingController = OVRInput.Controller.RTouch;

    [Header("Capacitive button")]
    [Tooltip("Use capacitive detection of teleport aim? (I.e., register when just resting thumb on button)")]
    public bool UseCapacitiveTriggering = false;

    [Tooltip("If above is selected, this button will be used to start aim and teleport.")]
    public TeleportInputHandlerTouch.AimCapTouchButtons CapTouchButton = TeleportInputHandlerTouch.AimCapTouchButtons.A;

    //private LocomotionController lc;

    private LocomotionTeleport locomotionTeleport;


    void Start()
    {
        locomotionTeleport = GetComponent<LocomotionTeleport>();

        SetupTeleportDefaults();

    }
    void SetupTeleportDefaults()
    {
        locomotionTeleport.enabled = true;
        //locomotionTeleport.EnableMovement(false, false, false, false);
        //locomotionTeleport.EnableRotation(false, false, false, false);

        var input = locomotionTeleport.GetComponent<TeleportInputHandlerTouch>();
        input.AimingController = AimingController;
        input.AimButton = AimButton;
        input.TeleportButton = AimButton;
        input.CapacitiveAimAndTeleportButton = CapTouchButton;
        input.FastTeleport = false;

        if (UseCapacitiveTriggering)
            input.InputMode = TeleportInputHandlerTouch.InputModes.CapacitiveButtonForAimAndTeleport;
        else
            input.InputMode = TeleportInputHandlerTouch.InputModes.SeparateButtonsForAimAndTeleport;

        //var hmd = locomotionTeleport.GetComponent<TeleportInputHandlerHMD>();
        //hmd.AimButton = AimButton;
        //hmd.TeleportButton = AimButton;

        var orient = locomotionTeleport.GetComponent<TeleportOrientationHandlerThumbstick>();
        orient.Thumbstick = (AimingController == OVRInput.Controller.LTouch) ? OVRInput.Controller.RTouch : OVRInput.Controller.LTouch; // opposite controller to rotate
    }

}
