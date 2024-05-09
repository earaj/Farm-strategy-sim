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
    }

    public override void Handle()
    {

       

        // Si une autre poule est visible, renard va le poursuit
        if (PouleVisible())
        {
            AgentMouvement.destination = Poule.transform.position;
            Renard.ChangerEtat(new EtatPoursuit(Renard, Poule));
        }        
        // Si le poule n'est pas visible, renard retourne a la patrouille
        else
        {
            Renard.ChangerEtat(new EtatPatrouille(Renard, Poule, Renard.GetPointsPatrouille()));
        }
    }

    public override void Leave()
    {
    }
}
