using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GenerationArbreStrategie
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="arbre"></param>
    /// <param name="tailleForet"></param>
    /// <param name="exclusion"></param>
    /// <param name="parentForet"></param>
    /// <param name="nombreArbres"></param>
    public abstract void GenerationArbre(GameObject arbre, Vector2Int tailleForet, Collider[] exclusion, GameObject parentForet, int nombreArbres);

    /// <summary>
    /// Methode pour verifier si la position est dans une zone interdite ex maison, route et magasin.
    /// </summary>
    /// <param name="position"></param>
    /// <param name="exclusion"></param>
    /// <returns></returns>
    protected bool EstDansLaZoneInterdit(Vector3 position, Collider[] exclusion)
    {
        foreach (Collider zone in exclusion)
        {
            if (zone.bounds.Contains(position))
            {
                return true;
            }
        }
        return false;
    }

    /// <summary>
    /// Methode pour verifier si la position est proche d'un arbre.
    /// </summary>
    /// <param name="position"></param>
    /// <param name="positionsArbres"></param>
    /// <param name="espace"></param>
    /// <returns></returns>
    protected bool EstProcheDArbre(Vector3 position, List<Vector3> positionsArbres, float espace)
    {
        foreach (Vector3 positionArbre in positionsArbres)
        {
            if (Vector3.Distance(positionArbre, position) < espace)
            {
                return true;
            }
        }
        return false;
    }
}
