using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    static public GameManager Instance;

    public GameObject LeftHandAnchor;
    public GameObject RightHandAnchor;
    public GameObject CenterEyeAnchor;
    public GameObject Rig;

    private void Awake()
    {
        Instance = this;
    }
    public void Replay()
    {
        SceneManager.LoadScene(0);
    }

}
