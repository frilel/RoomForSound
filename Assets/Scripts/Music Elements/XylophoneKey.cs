using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XylophoneKey : MonoBehaviour
{
    FMOD.Studio.EventInstance xylophoneHitSFXInstance;
    public FMODUnity.EventReference eventPathInteractionSoundOne;
    // public FMODUnity.EventReference eventPathInteractionSoundTwo;

    [SerializeField] private VFXController _VFXController;

    private float impactSpeed;

    private void Start()
    {
        if (_VFXController == null)
            _VFXController = GetComponentInParent<VFXController>();
        // ImpactSpeedText = GameObject.Find("ImpactSpeedText").GetComponent<Text>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        // get the xylophone stick object if the interaction on the single xylophone is noticed = interaction
        if (collision.transform.TryGetComponent<DrumStick>(out DrumStick drumStick))
        {
            xylophoneHitSFXInstance = FMODUnity.RuntimeManager.CreateInstance(eventPathInteractionSoundOne);
            xylophoneHitSFXInstance.setParameterByName("Volume", 1);
            xylophoneHitSFXInstance.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(gameObject));
            xylophoneHitSFXInstance.start();
            xylophoneHitSFXInstance.release();
            StartCoroutine("hitEffect");
            if (_VFXController != null)
            {
                _VFXController.triggerOne(collision.transform);
                _VFXController.triggerVibration(drumStick.GetGrabber(), 0.1f, 0.1f, 1);
            }
            if (drumStick != null)
            {
                //_VFXController.triggerVibration(drumStick.getGrabber(), 0.1f, 0.1f, 1);
            }
        }

        //FMODUnity.RuntimeManager.PlayOneShot(_eventPath, transform.position);

    }
    IEnumerator hitEffect()
    {
        transform.localScale=new Vector3(108,108,108);
        yield return new WaitForSeconds(0.15f);
        transform.localScale= new Vector3(100,100,100);
    }
}
