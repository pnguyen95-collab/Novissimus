using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnim : MonoBehaviour
{
    private Animator anim;
    public CharacterStats cs;
    public bool checkGotAttack;
    // Start is called before the first frame update
    void Start()
    {
        checkGotAttack = false;
        cs = this.GetComponent<CharacterStats>();
        anim = this.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        this.checkGotAttack = cs.checkGotAttack;

        if (checkGotAttack == true)
        {
            anim.SetBool("GotAttack", true);
        }
        else
        {
            anim.SetBool("GotAttack", false);
        }
    }
}
