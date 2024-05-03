using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EtatManger : EtatRenard
{
    public EtatManger(MouvementRenard _renard, GameObject poule) : base(_renard, poule)
    {
    }

    public override void Enter()
    {
        Animateur.SetBool("Attack", true);
    }

    public override void Handle()
    {
    }

    public override void Leave()
    {
        Animateur.SetBool("Attack", false);
    }
}
