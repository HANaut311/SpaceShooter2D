using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_animtion : MonoBehaviour
{
    private Animator anim;
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            anim.SetBool("Turn_Left", true);
            anim.SetBool("Turn_Right", false);
        }
        else if(Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            anim.SetBool("Turn_Left", false);
            anim.SetBool("Turn_Right", false);

        }

        if(Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            anim.SetBool("Turn_Right", true);
            anim.SetBool("Turn_Left", false);
        }
        else if(Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            anim.SetBool("Turn_Right", false);
            anim.SetBool("Turn_Left", false);


        }

        




    }
}
