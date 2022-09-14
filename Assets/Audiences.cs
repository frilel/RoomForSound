using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class Audiences
{
   public List<Audience> data=new List<Audience>();
}
[System.Serializable]
public class Audience
{
   public string id;
   public string name;
   public List<Message> messages=new List<Message>();
}
[System.Serializable]
public class Message
{
   public string messageID;
   public string audienceID;
   public string text;
}


