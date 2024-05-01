using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Arbre : MonoBehaviour, IActionnable
{
    public GameObject logPrefab;
    public float tomber = 2.0f;
    public float delaiApparenceBuch = 1.0f;
    public float desaparenceBuch = 0.5f;
    public float activation = 2.0f; // active le stop

    private bool isFalling = false;
    private bool isWalking = false;

    void Start()
    {
    }

    public void Ramasser(Inventaire inventaireJoueur)
    {
        //peut ajouter inventaire
        SpawnBuche();
        DetruireArbre();
    }

    public EtatJoueur EtatAUtiliser(ComportementJoueur sujet)
    {
        if (Permis(sujet))
        {
            Animator animator = sujet.GetComponent<Animator>();
            if (animator != null)
            {
                animator.SetBool("Walking", true);
                isWalking = true;
            }

            GameObject joueur = GameObject.Find("Joueur");
            if (joueur != null)
            {
                NavMeshAgent playerAgent = joueur.GetComponent<NavMeshAgent>();
                if (playerAgent != null)
                {
                    sujet.ActiveNavMeshAgent();
                    playerAgent.SetDestination(transform.position);
                }
            }

            return null;
        }
        else
        {
            return null;
        }
    }

    public bool Permis(ComportementJoueur sujet)
    {
        return true; 
    }

    void Update()
    {
        GameObject joueur = GameObject.Find("Joueur");
        Animator animator = joueur.GetComponent<Animator>();
        if (joueur != null && isWalking)
        {
            if (Vector3.Distance(transform.position, joueur.transform.position) <= activation)
            {
                animator.SetBool("Walking", false);
                animator.SetBool("Pousse", true);
                isWalking = false;
                GameObject.Find("Joueur").GetComponent<ComportementJoueur>().ActiveCharacterController();
                StartFalling();
            }
        }

        if (isFalling)
        {
            float vitesseTombee = 90.0f / tomber;
            transform.Rotate(Vector3.right, Time.deltaTime * vitesseTombee, Space.World);
        }
    }

    void StartFalling()
    {
        Invoke("SpawnBuche", delaiApparenceBuch);
        Invoke("DetruireArbre", tomber + desaparenceBuch);
        isFalling = true;
    }

    void SpawnBuche()
    {

        Vector3 spawnPosition = transform.position + new Vector3(0f, 0.5f, 0f); 
        Instantiate(logPrefab, spawnPosition, Quaternion.identity);
    }

    void DetruireArbre()
    {

        GameObject joueur = GameObject.Find("Joueur");
        Animator animator = joueur.GetComponent<Animator>();
        if (joueur != null)
        {
            ComportementJoueur comportementJoueur = joueur.GetComponent<ComportementJoueur>();
            if (comportementJoueur != null)
            {
                // Change the state to the default state (or any appropriate state)
                comportementJoueur.ChangerEtat(comportementJoueur.EtatNormal);
            }
        }
        animator.SetBool("Pousse", false);
        Destroy(gameObject);

    }
}
