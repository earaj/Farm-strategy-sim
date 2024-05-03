using UnityEngine;
using UnityEngine.SceneManagement;
using static TMPro.SpriteAssetUtilities.TexturePacker_JsonArray;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Soleil _soleil;
    [SerializeField] private GameObject prefabToInstantiate;

    private ComportementJoueur _joueur;

    private const float DISTANCE_ACTION = 3.0f;

    private Inventaire _inventaireJoueur;
    private EnergieJoueur _energieJoueur;
    private ChouMesh3D[] _chous;
    public int NumeroJour = 1;

    private bool estInstantiate = false;


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
        if (!estInstantiate && TempPourInstantiate())
        {
            InstantiateRenard();
            estInstantiate = true;
        }
    }
    private void InstantiateRenard()
    {
        Vector3 randomPosition = TrouverPositionSansArbre();

        Instantiate(prefabToInstantiate, randomPosition, Quaternion.identity);
    }

    private Vector3 TrouverPositionSansArbre()
    {
        Vector3 positionAleatoire = Vector3.zero;
        bool found = false;
        int maxessaie = 50;
        int essaie = 0;
        float minDistance = 2f;

        while (!found && essaie < maxessaie)
        {
            positionAleatoire = new Vector3(UnityEngine.Random.Range(-50f, 50f), 0f, UnityEngine.Random.Range(-50f, 50f));

            Collider[] allColliders = UnityEngine.Object.FindObjectsOfType<Collider>();

            bool isNearTrigger = false;
            foreach (Collider collider in allColliders)
            {

                if (collider != this.GetComponent<Collider>())
                {

                    if (collider.isTrigger && Vector3.Distance(collider.transform.position, positionAleatoire) < minDistance)
                    {
                        isNearTrigger = true;
                        break;
                    }
                }
            }

            if (!isNearTrigger)
            {
                found = true;
            }
            else
            {
                essaie++;
            }
        }

        if (!found)
        {
            positionAleatoire = new Vector3(0f, 0f, 0f);
        }

        return positionAleatoire;
    }

    private bool TempPourInstantiate()
    {
        Debug.Log("TEmpsss");
        int currentHour = GetCurrentHour();
        return currentHour == 21;
    }
    private int GetCurrentHour()
    {
        float proportionOfDay = _soleil.ProportionRestante;
        int numberOfSecondsInADay = 24 * 60 * 60;
        int currentSecond = (int)(proportionOfDay * numberOfSecondsInADay);

        // Calculate current hour
        int currentHour = currentSecond / 3600;

        return currentHour;
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