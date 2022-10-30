/*
 * Copyright (c) Meta Platforms, Inc. and affiliates.
 * All rights reserved.
 *
 * Licensed under the Oculus SDK License Agreement (the "License");
 * you may not use the Oculus SDK except in compliance with the License,
 * which is provided at the time of installation or download, or which
 * otherwise accompanies this software in either electronic or hard copy form.
 *
 * You may obtain a copy of the License at
 *
 * https://developer.oculus.com/licenses/oculussdk/
 *
 * Unless required by applicable law or agreed to in writing, the Oculus SDK
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */


using UnityEngine;
using System.Collections;

public class TeleportPoint : MonoBehaviour {

    public float dimmingSpeed = 1f;
    public float fullIntensity = 2.4f;
    public float lowIntensity = 0.4f;

    public Transform destTransform;

    public string PlayerTag = "Player";
    public int IgnoreRaycastLayer = 2;
    public int TeleportLayer = 6;

    private float lastLookAtTime = 0f;

    public Transform GetDestTransform()
    {
        return destTransform;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(PlayerTag))
        {
            this.GetComponent<MeshRenderer>().enabled = false;
            this.gameObject.layer = IgnoreRaycastLayer;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag(PlayerTag))
        {
            this.GetComponent<MeshRenderer>().enabled = true;
            this.gameObject.layer = TeleportLayer;
        }
    }

    // Update is called once per frame
    void Update () {
        float intensity = Mathf.SmoothStep(fullIntensity, lowIntensity, (Time.time - lastLookAtTime) * dimmingSpeed);
        GetComponent<MeshRenderer>().material.SetFloat("_Intensity", intensity);
	}

    public void OnLookAt()
    {
        lastLookAtTime = Time.time;
    }
}
