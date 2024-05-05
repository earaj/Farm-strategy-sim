using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using static TMPro.SpriteAssetUtilities.TexturePacker_JsonArray;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Soleil _soleil;
    [SerializeField] private GameObject prefabRenard;
    private GameObject cloneRenard;

    private ComportementJoueur _joueur;

    private const float DISTANCE_ACTION = 3.0f;

    private Inventaire _inventaireJoueur;
    private EnergieJoueur _energieJoueur;
    private ChouMesh3D[] _chous;
    public int NumeroJour = 1;

    private bool estInstantiate = false;
    public bool estNuit = false;


    void Start()
    {
        _joueur = GameObject.Find("Joueur").GetComponent<ComportementJoueur>();
        _inventaireJoueur = _joueur.GetComponent<Inventaire>();
        _energieJoueur = _joueur.GetComponent<EnergieJoueur>();
        _chous = FindObjectsByType<ChouMesh3D>(FindObjectsSortMode.None);

        // Patron de conception: Observateur
        FindObjectOfType<Soleil>().OnJourneeTerminee += NouvelleJournee;

        _energieJoueur.OnEnergieVide += EnergieVide;
    }

    void NouvelleJournee()
    {
        NumeroJour++;

        GameObject[] poules = GameObject.FindGameObjectsWithTag("Poule");
        foreach (var poule in poules)
        {
            poule.GetComponent<PondreOeufs>().DeterminerPonte();
        }
    }

    void Update()
    {
        _joueur = GameObject.Find("Joueur").GetComponent<ComportementJoueur>();
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("MenuConfiguration");
        }

        if (Input.GetKey(KeyCode.Tab))
        {
            Time.timeScale = 45;
        }
        else
        {
            Time.timeScale = 1;
        }

        // L'?tat du joueur peut affecter le passage du temps (ex.: Dodo: tout va vite, menus: le temps est stopp?, etc)
        Time.timeScale *= _joueur.GetComponent<ComportementJoueur>().MultiplicateurScale;

        //Distruction de renard

            if (TempPourDetruire())
            {
                DetruireRenard();
     }

        //Creation de renard
        if (!estInstantiate && TempPourInstantiate())
        {
            InstantiateRenard();
            estInstantiate = true;
        }
        
    }

    /// <summary>
    /// Creation de renard
    /// </summary>
    private void InstantiateRenard()
    {
        Vector3 randomPosition = TrouverPositionSansArbre();
        cloneRenard = Instantiate(prefabRenard, randomPosition, Quaternion.identity);
    }

    /// <summary>
    /// Trouver une position sans arbre (Eviter les collisions)
    /// </summary>
    /// <returns></returns>
    private Vector3 TrouverPositionSansArbre()
    {
        Vector3 positionAleatoire = Vector3.zero;
        bool positionTrouvee = false;
        int maxessaie = 50;
        int essaie = 0;
        float minDistance = 2f;

        // Trouver une position aleatoire sans arbre (avec une limit)
        while (!positionTrouvee && essaie < maxessaie)
        {
            positionAleatoire = new Vector3(UnityEngine.Random.Range(-50f, 50f), 0f, UnityEngine.Random.Range(-50f, 50f));

            Collider[] allColliders = UnityEngine.Object.FindObjectsOfType<Collider>();

            bool estProcheDeQqch = false;
            foreach (Collider collider in allColliders)
            {

                if (collider != this.GetComponent<Collider>())
                {

                    if (collider.isTrigger && Vector3.Distance(collider.transform.position, positionAleatoire) < minDistance)
                    {
                        estProcheDeQqch = true;
                        break;
                    }
                }
            }

            // S'il y a aucune collision, on a trouve une position
            if (!estProcheDeQqch)
            {
                positionTrouvee = true;
            }
            else
            {
                essaie++;
            }
        }

        // Si on a pas trouve de position, on retourne une position null
        if (!positionTrouvee)
        {
            positionAleatoire = new Vector3(0f, 0f, 0f);
        }

        return positionAleatoire;
    }

    /// <summary>
    /// Methode pour determiner si on doit instancier un renard (nuit)
    /// </summary>
    /// <returns></returns>
    private bool TempPourInstantiate()
    {
        int heureCourrant = HeuresActuelle();
        if (heureCourrant == 2) {
            estNuit=true;

            return true; 
                }
        else {             
            return false;
        }
    }

    /// <summary>
    /// Methode pour determiner l'heure actuelle
    /// </summary>
    /// <returns></returns>
    private int HeuresActuelle()
    {
        float jour = _soleil.ProportionRestante;
        int nbSecDansJour = 24 * 60 * 60;
        int secondCourrant = (int)(jour * nbSecDansJour);

        // Calculation de l'heure actuelle
        int heureCourrant = secondCourrant / 3600;

        return heureCourrant;
    }

    /// <summary>
    /// Methode pour detruire le renard
    /// </summary>
    private void DetruireRenard()
    {
          Destroy(cloneRenard);
          estInstantiate = false;
    }

    /// <summary>
    /// Methode pour determiner si on doit detruire le renard (jour)
    /// </summary>
    /// <returns></returns>
    private bool TempPourDetruire()
    {
        int heureCourrant = HeuresActuelle();
        if (heureCourrant == 15)
        {
            estNuit=false;
            return true;
        }
        else
        {
            return false;
        }
    }


    /// <summary>
    /// Partie perdue quand l'?nergie tombe ? z?ro
    /// </summary>
    private void EnergieVide()
    {
        _joueur.ChangerEtat(new EtatDansMenu(_joueur));
        GestionnaireMessages.Instance.AfficherMessageFin(
            "Plus d'?nergie!",
            "Vous n'avez pas r?ussi ? vous garder en vie, vous tombez sans connaissance au milieu du champ." +
            "Un loup passe et vous d?guste en guise de d?ner. Meilleure chance la prochaine partie!");
        Time.timeScale = 0;
    }
}