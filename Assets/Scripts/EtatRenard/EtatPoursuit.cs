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
        //Verfier si le poule est visible
        if (PouleVisible())
        {

            GameObject[] lesPoules = GameObject.FindGameObjectsWithTag("Poule");

            // Trouver le plus proche poule
            GameObject pouleProche = null;
            float disProche = Mathf.Infinity;
            foreach (GameObject poules in lesPoules)
            {
                float distance = Vector3.Distance(Renard.transform.position, poules.transform.position);
                if (distance < disProche)
                {
                    pouleProche = poules;
                    disProche = distance;
                }
            }

            //Distintaion a poule
            if (pouleProche != null)
            {
                AgentMouvement.destination = pouleProche.transform.position;
                Renard.ChangerEtat(new EtatPoursuit(Renard, pouleProche));
                return;
            }
        }

        else {          
            Renard.ChangerEtat(new EtatPatrouille(Renard, Poule, Renard.GetPointsPatrouille()));
               }
        
    }

    public override void Leave()
    {
    }
}
