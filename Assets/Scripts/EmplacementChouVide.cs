using System;
using UnityEngine;

public class EmplacementChouVide : MonoBehaviour, IPlantable
{
    private ChouMesh3D _chouMesh3D;

    private void Start()
    {

    }

    public void Planter(Inventaire inventaireJoueur)
    {
        inventaireJoueur.Graines--;
        if (_chouMesh3D == null)
        {
            _chouMesh3D = GetComponent<ChouMesh3D>();
        }

        if (_chouMesh3D != null)
        {

        _chouMesh3D.ObjetBebe.SetActive(true);
        gameObject.AddComponent<ChouCroissant>();
        Destroy(this);
        }
        else
        {
            Debug.LogError("Le composant _chouMesh3D est null dans la méthode Planter().");
        }
    }


    public EtatJoueur EtatAUtiliser(ComportementJoueur sujet)
    {
        return new EtatPlanter(sujet, this);
    }

    public bool Permis(ComportementJoueur sujet)
    {
        return sujet.GetComponent<Inventaire>().Graines > 0;
    }
}