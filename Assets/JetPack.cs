using System;
using System.Collections;
using System.Collections.Generic;

using Unity.VisualScripting;
using UnityEngine;

/// <summary>Jetpack implementation of PowerUp; GameManager owns its timed presentation.</summary>
public class JetPack : PowerUp
{
    protected override void Apply(Player player)
    {
        player.isJetpackenable = true;
    }
}
