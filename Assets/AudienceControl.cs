using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class AudienceControl : MonoBehaviour
{
    public Text nameText;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void UpdateDisplay(string name)
    {
        nameText.text = name;
    }
    public void ChangeAvatar(int index)
    {
        for (int i = 0; i < 4; i++)
        {
            if (i == index)
            {
                transform.GetChild(i).gameObject.SetActive(true);
            }
            else
            {
                transform.GetChild(i).gameObject.SetActive(false);
            }

        }
    }

}
