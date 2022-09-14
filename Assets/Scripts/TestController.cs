using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public class TestController : MonoBehaviour
{
    public GameObject audiencePrefab;
    public Transform audienceParent;
    public List<string> IDs = new List<string>();
    string url;
    private void Start()
    {
        url = "https://roomforsound-server.herokuapp.com/audiences";
        StartCoroutine(FetchData());
    }
    [ContextMenu("Test Get")]
    public async void TestGet()
    {
        var httpClient = new HttpClient(new JsonSerializationOption());
        //var result = await httpClient.Get<TestUIClasses>(url);
        string result = await httpClient.GetText(url);
        Debug.Log(result);

        Audiences audiences = JsonUtility.FromJson<Audiences>(result);
        //Debug.Log(objects.data.Length);
        if (audiences.audiences != null)
        {
            foreach (Audience aud in audiences.audiences)
            {
                if (!IDs.Contains(aud.id))
                {
                    IDs.Add(aud.id);
                    Vector3 randomPos = audienceParent.GetChild(0).GetComponent<RandomPositionAllocator>().GenerateRandomPos();
                    GameObject go = Instantiate(audiencePrefab, randomPos, Quaternion.identity, audienceParent);
                    go.GetComponent<AudienceControl>().UpdateDisplay(aud.name);
                }
            }
        }
        else
        {
            Debug.LogError("audiences.audiences is null");
        }

    }
    IEnumerator FetchData()
    {
        while (true)
        {
            TestGet();
            yield return new WaitForSeconds(3);
        }

    }

}
