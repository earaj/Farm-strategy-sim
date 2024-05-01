using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ContolleurJoueur : MonoBehaviour
{
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                GameObject joueur = GameObject.Find("Joueur");
                if (joueur != null)
                {
                    ComportementJoueur sujet = joueur.GetComponent<ComportementJoueur>();
                    if (sujet != null)
                    {
                        Arbre arbre = hit.collider.GetComponent<Arbre>();
                        if (arbre != null)
                        {
                            sujet.ActiveNavMeshAgent();
                            arbre.EtatAUtiliser(sujet);
                            StartCoroutine(ReactiveCharacterController(sujet));
                        }
                    }
                }
            }
        }
    }
    IEnumerator ReactiveCharacterController(ComportementJoueur sujet)
    {
        while (!sujet.EtatNormal.EstActif)
        {
            yield return null;
        }
        sujet.ActiveCharacterController();
        sujet.DeactiveNavMeshAgent();
    }
}
