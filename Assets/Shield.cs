using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

/* Script: A shield pickup that enables the player's shield flag.
   Cheat sheet: Inheritance reuses PowerUp behavior; override replaces a virtual method in the base class. */

/// <summary>Shield implementation of PowerUp; GameManager owns its duration and visuals.</summary>
public class Shield : PowerUp
{
    protected override void Apply(Player player)
    {
        player.isShieldEnable = true;
    }
}
