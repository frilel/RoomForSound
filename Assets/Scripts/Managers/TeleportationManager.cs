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

    //private LocomotionController lc;

    private LocomotionTeleport locomotionTeleport;


    void Start()
    {
        locomotionTeleport = GetComponent<LocomotionTeleport>();

        SetupNodeTeleport();
    }

    // Teleport between node with A buttons. Display laser to node. Allow snap turns.
    void SetupNodeTeleport()
    {
        SetupTeleportDefaults();
        SetupNonCap(); //??

        locomotionTeleport.EnableRotation(true, false, false, true);
        //var input = locomotionTeleport.GetComponent<TeleportInputHandlerTouch>();
        //input.AimingController = AimingController;

        //var input = TeleportController.GetComponent<TeleportAimHandlerLaser>();
        //input.AimingController = OVRInput.Controller.RTouch;
    }

    void SetupTeleportDefaults()
    {
        locomotionTeleport.enabled = true;
        locomotionTeleport.EnableMovement(false, false, false, false);
        locomotionTeleport.EnableRotation(false, false, false, false);

        var input = locomotionTeleport.GetComponent<TeleportInputHandlerTouch>();
        input.InputMode = TeleportInputHandlerTouch.InputModes.CapacitiveButtonForAimAndTeleport;
        input.AimingController = AimingController;
        input.AimButton = AimButton;
        input.TeleportButton = AimButton;
        input.CapacitiveAimAndTeleportButton = TeleportInputHandlerTouch.AimCapTouchButtons.A; //??
        input.FastTeleport = false;

        var hmd = locomotionTeleport.GetComponent<TeleportInputHandlerHMD>();
        hmd.AimButton = AimButton;
        hmd.TeleportButton = AimButton;

        var orient = locomotionTeleport.GetComponent<TeleportOrientationHandlerThumbstick>();
        orient.Thumbstick = AimingController == OVRInput.Controller.LTouch ? OVRInput.Controller.RTouch : OVRInput.Controller.LTouch;
    }

    void SetupNonCap()
    {
        var input = locomotionTeleport.GetComponent<TeleportInputHandlerTouch>();
        input.InputMode = TeleportInputHandlerTouch.InputModes.SeparateButtonsForAimAndTeleport;
        input.AimButton = AimButton;
        input.TeleportButton = AimButton;
    }


}
