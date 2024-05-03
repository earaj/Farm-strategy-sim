using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EtatPoursuit : EtatRenard
{
    public EtatPoursuit(MouvementRenard renard, GameObject poule) : base(renard, poule)
    {

    }

    public override void Enter()
    {
        AgentMouvement.destination = Poule.transform.position;
    }

    public override void Handle()
    {
        bool manger = false;
        if (PouleVisible())
        {
            AgentMouvement.destination = Poule.transform.position;
            manger = Vector3.Distance(Renard.transform.position, Poule.transform.position) <= 3.0f;
        }

        if (manger)
        {
            Renard.ChangerEtat(new EtatManger(Renard, Poule));
            Animateur.SetBool("Attack", manger);
        }
    }

    public override void Leave()
    {
        Animateur.SetBool("Walk", false);

    }
}
