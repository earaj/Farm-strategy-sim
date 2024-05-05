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
        //Si le poule est visible, renard va le manger
        if (PouleVisible())
        {
            AgentMouvement.destination = Poule.transform.position;
            manger = Vector3.Distance(Renard.transform.position, Poule.transform.position) <= 3.0f;
        }

        //Changement d'etat a manger
        if (manger)
        {
            Renard.ChangerEtat(new EtatManger(Renard, Poule));
        }
    }

    public override void Leave()
    {
    }
}
