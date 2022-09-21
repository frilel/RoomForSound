using UnityEngine;

public class GameManager : MonoBehaviour
{
    static public GameManager Instance;

    public GameObject LeftHandControllerRoot;
    public GameObject RightHandControllerRoot;
    public GameObject ControllerTrackingSpace;

    private void Awake()
    {
        Instance = this;
    }

}
