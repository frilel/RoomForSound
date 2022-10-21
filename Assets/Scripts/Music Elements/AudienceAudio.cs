using UnityEngine;

public class AudienceAudio : MonoBehaviour
{
    FMOD.Studio.EventInstance clap;
    public GameObject fireworks;
    public GameObject heartPrefab;
    private Transform fireworksSpawnPos;
    private float fireworkForce = 1.7f;
    private Transform parentObejct;

    FMOD.Studio.EventInstance fireworksInstance;

    private void Start()
    {
        
        parentObejct=transform.parent.parent;
        fireworksSpawnPos =parentObejct.GetChild(5);

        fireworksInstance = FMODUnity.RuntimeManager.CreateInstance("event:/Audience/Fireworks");

        fireworksInstance.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(gameObject));
    }
    public void StartClap()
    {
        clap = FMODUnity.RuntimeManager.CreateInstance("event:/Audience/Clapping");
        clap.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(gameObject));
        clap.start();
        clap.release();
    }
    public void SendFirework()
    {
        Invoke("PlayFireworksSound", 0f);
        GameObject firework = Instantiate(fireworks, fireworksSpawnPos.position, Quaternion.identity);
        firework.transform.LookAt(firework.transform.position + parentObejct.forward + parentObejct.up * 2);
        firework.GetComponent<Rigidbody>().AddForce(firework.transform.forward * fireworkForce, ForceMode.Impulse);
        //Invoke("DisableTrail", 2f);
        Destroy(firework, 2.5f);
    }

    private void PlayFireworksSound()
    {
        fireworksInstance.start();
    }

    private void DisableTrail()
    {
        GetComponent<TrailRenderer>().enabled = false;
    }
    public void SendHeart()
    {
        GameObject heart = Instantiate(heartPrefab, transform.position+Vector3.up*2.2f, Quaternion.identity);
        heart.transform.LookAt(heart.transform.position + parentObejct.forward);
        //firework.GetComponent<Rigidbody>().AddForce(firework.transform.forward * fireworkForce, ForceMode.Impulse);
        //Invoke("DisableTrail", 2f);
        Destroy(heart, 2.5f);
    }
    public void StartCheering()
    {
        clap = FMODUnity.RuntimeManager.CreateInstance("event:/Audience/Cheering");
        clap.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(gameObject));
        clap.start();
        clap.release();
    }

    private void OnDestroy() {
        fireworksInstance.release();
    }
}
