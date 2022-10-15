using UnityEngine;

public class PassCurrentLocation : MonoBehaviour
{

    public ButtonManager buttonManager;

    private void Start() {
        buttonManager = GameObject.Find("Sequenzer").GetComponent<ButtonManager>();
    }


    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.CompareTag("Player"))
        {
            buttonManager.SetCurrentLocationName(this.gameObject.name);
        }
   }
}
