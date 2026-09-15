using System;
using System.Collections;
using System.Collections.Generic;

using Unity.VisualScripting;
using UnityEngine;

/* Script: A jetpack pickup that enables the player's jetpack flag.
   Cheat sheet: Inheritance reuses PowerUp behavior; override supplies this pickup's specific effect. */

/// <summary>Jetpack implementation of PowerUp; GameManager owns its timed presentation.</summary>
public class JetPack : PowerUp
{
    protected override void Apply(Player player)
    {
        player.isJetpackenable = true;
    }
}
