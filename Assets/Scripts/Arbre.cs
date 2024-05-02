using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Arbre : MonoBehaviour, IActionnable
{
    public GameObject logPrefab;
    public float tomber = 1.5f;
    public float delaiApparenceBuch = 1.0f;
    //Detection de la buche
    public float activation = 1f;

    public float maximumRotation = 40f;

    private bool tombe = false;
    private bool marche = false;

    void Start()
    {
    }

    /// <summary>
    /// Utilisation d'etat pour pousser l'arbre
    /// </summary>
    /// <param name="sujet"></param>
    /// <returns></returns>
    public EtatJoueur EtatAUtiliser(ComportementJoueur sujet)
    {
        if (Permis(sujet))
        {
            //Animation de marche
            Animator animator = sujet.GetComponent<Animator>();
            if (animator != null)
            {
                animator.SetBool("Walking", true);
                marche = true;
            }

            //Destination du joueur
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
        //Verification de la distance entre le joueur et l'arbre
        GameObject joueur = GameObject.Find("Joueur");
        Animator animator = joueur.GetComponent<Animator>();
        if (joueur != null && marche)
        {
            if (Vector3.Distance(transform.position, joueur.transform.position) <= activation)
            {
                animator.SetBool("Walking", false);
                animator.SetBool("Pousse", true);
                marche = false;
                joueur.GetComponent<ComportementJoueur>().ActiveCharacterController();
                Invoke("Tomber", 2f);
            }
        }

        // Rotation de l'arbre (tomber)
        if (tombe)
        {
            float vitesseTombee = 90.0f / tomber;
            float rotaionNb = Time.deltaTime * vitesseTombee;

            // Determeine la direction de l'arbre
            Vector3 directionToPlayer = transform.position - joueur.transform.position;
            Vector3 rotationAxis = Vector3.Cross(Vector3.up, directionToPlayer);

            // rotaion jusqu'a la terre
            float finRotaion = Mathf.Min(rotaionNb, maximumRotation);

            // Appliquer la rotation
            if (Mathf.Abs(transform.rotation.eulerAngles.y + finRotaion) < maximumRotation)
            {
                transform.Rotate(rotationAxis, finRotaion, Space.World);
            }
        }
    }

    /// <summary>
    /// Rotation de l'arbre (pour tomber)
    /// </summary>
    void Tomber()
    {
        GameObject joueur = GameObject.Find("Joueur");
        Animator animator = joueur.GetComponent<Animator>();
        Invoke("DetruireArbre", 3 );
        animator.SetBool("Pousse", false);
        tombe = true;
    }

    /// <summary>
    /// Création de la buche
    /// </summary>
    void SpawnBuche()
    {
        GameObject joueur = GameObject.Find("Joueur");

        //position de la buche dans le feuillet d'arbre
        Vector3 emplacement = joueur.transform.forward;
        Vector3 spawnOffset = emplacement * 5f; 
        Vector3 position = joueur.transform.position + spawnOffset; 
        Quaternion rotation = Quaternion.Euler(0, 90, 90);
        Instantiate(logPrefab, position, rotation);
    }

    /// <summary>
    /// Destruction de l'arbre
    /// </summary>
    void DetruireArbre()
    {

        GameObject joueur = GameObject.Find("Joueur");
        if (joueur != null)
        {
            ComportementJoueur comportementJoueur = joueur.GetComponent<ComportementJoueur>();
            if (comportementJoueur != null)
            {
                comportementJoueur.ChangerEtat(comportementJoueur.EtatNormal);
            }
        }
        tombe = false;
        SpawnBuche();
        Destroy(gameObject);
    }
}
