using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFighter : Fighter
{
    protected override void KillEntity()
    {
        base.KillEntity();
        
        if (controller)
        {
            controller.enabled = false;
        }
    }
}
