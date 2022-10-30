using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicianControl : MonoBehaviour
{
    private Animator animator;
    public int animIndex;//0 drum, 1 singing, 2 guitar
    // Start is called before the first frame update
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    void OnEnable()
    {
        switch (animIndex)
        {
            case 0:
                animator.Play("Drums");
                break;
            case 1:
                animator.Play("Singing");
                break;
            case 2:
                animator.Play("Guitar");
                break;
            default:
                break;
        }
    }
    // Update is called once per frame
    void Update()
    {

    }
}
