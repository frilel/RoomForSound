using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ClickHandler : MonoBehaviour
{
    public UnityEvent triggerEvent;

        private void OnCollisionEnter(Collision other) {
            Debug.Log("Text to Speech!");
            triggerEvent?.Invoke();
        }

}
