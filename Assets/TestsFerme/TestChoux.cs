using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEditor;
using UnityEngine;
using UnityEngine.TestTools;

public class TestChoux
{
    private GameObject chou, soleil;
    private Inventaire inventaire;

    [SetUp]
    public void CreerObjets()
    {
        soleil = new GameObject("Directional Light");
        soleil.AddComponent<Light>();
        soleil.AddComponent<Soleil>();
        chou = GameObject.Instantiate(PrefabUtility.LoadPrefabContents("Assets/Prefabs/Chou.prefab"));

        var joueur = new GameObject("Joueur");
        inventaire = joueur.AddComponent<Inventaire>();
    }

    [TearDown]
    public void DetruireObjets()
    {
        GameObject.Destroy(soleil);
        GameObject.Destroy(chou);
        GameObject.Destroy(inventaire.gameObject);
    }

    [UnityTest]
    public IEnumerator TestChouCueillir()
    {
        // ====== EXEMPLE DE TEST DÉJÀ FONCTIONNEL ======
        // Valide ce qui se passe quand on plante un chou, qu'on attend 3 jours, puis qu'on le cueille.
        // On vérifie que le nombre de choux


        // ARRANGE: dans le SetUp + ici
        var emplacement = chou.GetComponent<EmplacementChouVide>();

        // ACT
        inventaire.Graines = 1;
        inventaire.Choux = 0;
        emplacement.Planter(inventaire);
        yield return null;

        var chouCroissant = chou.GetComponent<ChouCroissant>();
        yield return null;

        // Trois jours pour pousser :
        chouCroissant.JourneePassee();
        yield return null;

        chouCroissant.JourneePassee();
        yield return null;

        chouCroissant.JourneePassee();
        yield return null;

        var chouPret = chou.GetComponent<ChouPret>();

        chouPret.Ramasser(inventaire);
        yield return null;

        // ASSERT
        Assert.AreEqual(inventaire.Choux, 1);
    }


    /// <summary>
    /// Teste si le joueur perd une graine lorsqu'il plante un chou
    /// </summary>
    /// <returns></returns>
    [UnityTest]
    public IEnumerator TestChouPerdGraine()
    {
        // ARRANGE
        var emplacement = chou.GetComponent<EmplacementChouVide>();

        // ACT
        inventaire.Graines = 1;
        inventaire.Choux = 0;
        emplacement.Planter(inventaire);
        yield return null;

        // ASSERT
        Assert.AreEqual(inventaire.Graines, 0);
    }


    /// <summary>
    /// Teste si le chou est prêt à être cueilli après trois jours
    /// </summary>
    /// <returns></returns>
    [UnityTest]
    public IEnumerator TestChouJourneesPassees()
    {
        // ARRANGE
        var emplacement = chou.GetComponent<EmplacementChouVide>();

        // ACT
        inventaire.Graines = 1;
        emplacement.Planter(inventaire);
        yield return null;

        var chouCroissant = chou.GetComponent<ChouCroissant>();
        yield return null;

        //Trois jour passe
        chouCroissant.JourneePassee();
        yield return null;

        chouCroissant.JourneePassee();
        yield return null;

        chouCroissant.JourneePassee();
        yield return null;

        // ASSERT
        Assert.IsTrue(chouCroissant.Pret);
    }


    /// <summary>
    /// Teste si le joueur peut replanter un chou en meme place après l'avoir cueilli
    /// </summary>
    /// <returns></returns>
    [UnityTest]
    public IEnumerator TestChouReplanter()
    {
        var emplacement = chou.GetComponent<EmplacementChouVide>();

        // ACT
        inventaire.Graines = 1;
        inventaire.Choux = 0;
        emplacement.Planter(inventaire);
        yield return null;

        var chouCroissant = chou.GetComponent<ChouCroissant>();
        yield return null;

        // Trois jours pour pousser :
        chouCroissant.JourneePassee();
        yield return null;

        chouCroissant.JourneePassee();
        yield return null;

        chouCroissant.JourneePassee();
        yield return null;

        var chouPret = chou.GetComponent<ChouPret>();

        chouPret.Ramasser(inventaire);
        yield return null;

        // ASSERT
        Assert.AreEqual(inventaire.Choux, 1);
    }
}