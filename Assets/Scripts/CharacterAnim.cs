using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnim : MonoBehaviour
{
    private Animator anim;
    public CharacterStats cs;
    public PlayerBehavior pb;
    public bool checkGotAttack;
    public bool checkHealing;
    // Start is called before the first frame update
    void Start()
    {
        checkGotAttack = false;
        checkHealing = false;
        cs = this.GetComponent<CharacterStats>();

        if(this.gameObject.tag=="Player")
        pb = this.GetComponent<PlayerBehavior>();

        anim = this.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        this.checkGotAttack = cs.checkGotAttack;

        if (this.gameObject.tag == "Player")
        {
            this.checkHealing = pb.checkHealing;

            if (checkHealing == true)
            {
                anim.SetBool("Healing", true);
            }
            else
            {
                anim.SetBool("Healing", false);
            }
        }
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
