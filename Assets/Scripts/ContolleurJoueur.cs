using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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
                        Buche buche= hit.collider.GetComponent<Buche>();
                        Collation collation = hit.collider.GetComponent<Collation>();

                            if (arbre != null)
                            {
                                sujet.ActiveNavMeshAgent();
                                arbre.EtatAUtiliser(sujet);
                            }
                            if (buche != null)
                            {
                                sujet.ActiveNavMeshAgent();
                                buche.EtatAUtiliser(sujet);
                            }
                            if(collation != null)
                            {
                                sujet.ActiveNavMeshAgent();
                                collation.EtatAUtiliser(sujet);
                        }
                    }
                }
            }
        }
    }
}