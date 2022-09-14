using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class Audiences
{
   public Audience [] audiences;
}
[System.Serializable]
public class Audience
{
   public string id;
   public string name;
   public Message []messages;
}
[System.Serializable]
public class Message
{
   public string messageID;
   public string audienceID;
   public string text;
}


