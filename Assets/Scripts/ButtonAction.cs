using UnityEngine;

public class ButtonAction : MonoBehaviour
{

    [SerializeField] public MeshRenderer buttonMeshRenderer;
    string buttonName;
    ButtonManager buttonManager;

    private void Start() 
    {
            buttonName = transform.gameObject.name;
            buttonManager = GameObject.Find("Sequenzer").GetComponent<ButtonManager>();
    }

    public void ForwardButtonPush()
    {
        buttonManager.ManageButtonPush(buttonName, buttonMeshRenderer);
    }
}