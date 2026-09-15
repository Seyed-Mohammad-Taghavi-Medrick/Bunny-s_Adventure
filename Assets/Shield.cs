using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

/// <summary>Shield implementation of PowerUp; GameManager owns its duration and visuals.</summary>
public class Shield : PowerUp
{
    protected override void Apply(Player player)
    {
        player.isShieldEnable = true;
    }
}
