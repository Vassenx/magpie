using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFighter : Fighter
{
    protected override void KillEntity()
    {
        base.KillEntity();
        
        CharacterController2D cc2D = GetComponent<CharacterController2D>();
        if (cc2D)
        {
            cc2D.enabled = false;
        }
    }
}
