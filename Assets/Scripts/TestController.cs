using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public class TestController : MonoBehaviour
{
    public GameObject audiencePrefab;
    public Transform audienceParent;
    //public Transform lookAtTarget;
    //public List<string> IDs = new List<string>();
    Dictionary<string,GameObject> idsStored=new Dictionary<string, GameObject>();
    string url;
    HttpClient httpClient = new HttpClient(new JsonSerializationOption());
    private void Start()
    {
        url = "https://roomforsound-server.herokuapp.com/audiences";
        StartCoroutine(FetchData());
    }
    [ContextMenu("Test Get")]
    public async void TestGet()
    {
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
                if (idsStored.Count <= 44)
                {
                    if (!idsStored.ContainsKey(aud.id))
                    {
                        //IDs.Add(aud.id);
                        Vector3 randomPos = audienceParent.GetChild(0).GetComponent<RandomPositionAllocator>().GenerateRandomPos();
                        GameObject go = Instantiate(audiencePrefab, randomPos, Quaternion.identity, audienceParent);
                        idsStored.Add(aud.id,go);
                        go.transform.LookAt(new Vector3(0, go.transform.position.y, 0));
                        go.GetComponent<AudienceControl>().UpdateAuience(aud);
                        go.GetComponent<AudienceControl>().ChangeAvatar(Random.Range(0, 4));
                        Debug.Log(aud.name + " added");
                    }
                    else
                    {
                        if(aud.messages.Count>0)
                        {
                            GameObject go;
                            idsStored.TryGetValue(aud.id,out go);
                            go.GetComponent<AudienceControl>().audience=aud;
                            go.GetComponent<AudienceControl>().UpdateMessage();
                        }
                    }
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
