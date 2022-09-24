using UnityEngine;

public class GameManager : MonoBehaviour
{
    static public GameManager Instance;

    public GameObject LeftHandAnchor;
    public GameObject RightHandAnchor;
    public GameObject Rig;

    private void Awake()
    {
        Instance = this;
    }

}
