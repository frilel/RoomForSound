using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public class TestController : MonoBehaviour
{
    public GameObject audiencePrefab;
    public Transform audienceParent;
    //public Transform lookAtTarget;
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
        //string result="{\"data\":[{\"id\":\"audience-bwzrdu4l81ym2l8\",\"name\":\"ss\",\"messages\":[]},{\"id\":\"audience-bwzrdu4l81ymv5h\",\"name\":\"ss\",\"messages\":[]}]}";
        //string result="{\"data\":[{\"id\":\"audience-bwzrdu4l81ym2l8\",\"name\":\"ss\"},{\"id\":\"audience-bwzrdu4l81ymv5h\",\"name\":\"sss\"}]}";
        //string result="{\"data\":[{\"id\":\"audience-bwzrdu4l81ym2l8\",\"name\":\"ss\",\"messages\":[]},{\"id\":\"audience-bwzrdu4l81ymv5h\",\"name\":\"ss\",\"messages\":[]}]}";
        Audiences audiences = JsonUtility.FromJson<Audiences>(result);
        //Debug.Log(audiences.data[0].id+","+audiences.data[0].name+audiences.data[0].messages.Count);

        //Audiences audiences = JsonUtility.FromJson<Audiences>(result);
        //Debug.Log(objects.data.Length);
        if (audiences.data != null)
        {
            foreach (Audience aud in audiences.data)
            {
                if (!IDs.Contains(aud.id))
                {
                    IDs.Add(aud.id);
                    Vector3 randomPos = audienceParent.GetChild(0).GetComponent<RandomPositionAllocator>().GenerateRandomPos();
                    GameObject go = Instantiate(audiencePrefab, randomPos, Quaternion.identity, audienceParent);
                    go.transform.LookAt(new Vector3(0,go.transform.position.y,0));
                    go.GetComponent<AudienceControl>().UpdateDisplay(aud.name);
                    go.GetComponent<AudienceControl>().ChangeAvatar(Random.Range(0,4));
                    Debug.Log(aud.name+" added");
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
            yield return new WaitForSeconds(2);
        }

    }

}
