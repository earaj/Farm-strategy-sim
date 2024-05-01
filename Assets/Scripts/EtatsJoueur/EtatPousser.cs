using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EtatPousser : EtatJoueur
{
    private IActionnable arbre;
    private float tempsDePousser = 0.0f;
    private bool estPousse;

    public override bool EstActif => true;
    public override bool DansDialogue => false;
    public override float EnergieDepensee => ConstantesJeu.COUT_POUSSER;

    public EtatPousser(ComportementJoueur sujet, IActionnable arbre) : base(sujet)
    {
        this.arbre = arbre;
        estPousse = false;
        Time.timeScale = 2f;
    }

    public override void Enter()
    {
        Animateur.SetBool("Pousse", true);
    }

    public override void Handle()
    {
        tempsDePousser += Time.deltaTime;

        if (tempsDePousser >= 1.5f && !estPousse)
        {
            arbre.EtatAUtiliser(Sujet);
            estPousse = true;
        }
        else if (tempsDePousser >= 3.0f)
        {
            Sujet.ChangerEtat(Sujet.EtatNormal);
        }
    }

    public override void Exit()
    {
        Animateur.SetBool("Pousse", false);
        Time.timeScale = 1f;
    }
}