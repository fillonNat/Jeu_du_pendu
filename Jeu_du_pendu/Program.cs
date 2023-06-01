using System;
using System.Collections.Generic;
using AsciiArt;

namespace jeu_du_pendu // Note: actual namespace depends on the project name.
{
    class Program
    {
        static void AfficherMot(string mot, List<char> lettres) {
            // _ _ _ _ _ _ _ _ afficher le mot selon la longueur de celui-ci


            for (int i = 0; i < mot.Length; i++)
            {
                char lettre = mot[i];
                if (lettres.Contains(lettre)) {
                    Console.Write(lettre + " ");

                }
                else
                {
                    Console.Write("_ ");
                }

            }
            Console.WriteLine();

        }

        static bool ToutesLettresDevinees(string mot, List<char> lettres)
        {
            // true -> toutes les lettres ont été trouvées
            // false 
            foreach (char lettre in lettres) {
                mot = mot.Replace(lettre.ToString(), "");

            }
            if (mot.Length == 0) {
                return true;
            }
            return false;

        }
        static char DemanderUneLettre(string message = "Deviner une lettre : ") {

            while (true) {
                // Rentrez une lettre
                Console.WriteLine(message);
                string entree = Console.ReadLine();

                if (entree.Length == 1)
                {
                    entree = entree.ToUpper();
                    return entree[0];
                }
                else {

                    Console.WriteLine("ERREUR : Vous devez rentrer une lettre");
                }
            }

        }
        static void AfficherPendu(string mot, List<char> liste, int NB_VIES, int viesRestantes) {
            Console.WriteLine("Le nombre de vies restantes est : " + viesRestantes);
            Console.WriteLine();
            Console.WriteLine(Ascii.PENDU[NB_VIES - viesRestantes]);
            Console.WriteLine();
            AfficherMot(mot, liste);
            Console.WriteLine(); 
        }

        static void DevinerMot(string mot)
        {
            var liste = new List<char>();
            const int NB_VIES = 6;
            int viesRestantes = NB_VIES;
            var mauvaisesLettres = new List<char>();
            // Boucler 
            while (viesRestantes >0)
            {    
                           
                AfficherPendu(mot, liste, NB_VIES, viesRestantes);
                char lettre = DemanderUneLettre();
                Console.Clear(); 

                if (mot.Contains(lettre))
                {
                    liste.Add(lettre);
                    if (ToutesLettresDevinees(mot, liste))
                    {
                        AfficherPendu(mot, liste, NB_VIES, viesRestantes);
                        Console.WriteLine("Bravo ! Vous avez gagné");
                        break;
                    }
                    Console.WriteLine("Cette lettre est dans le mot.");
                }
                else { 
                    if(!mauvaisesLettres.Contains(lettre)) { 
                        mauvaisesLettres.Add(lettre);
                        viesRestantes--;
                    }
                    Console.WriteLine("Cette lettre n'est pas dans le mot.");
                }
                if (mauvaisesLettres.Count > 0) { 
                    Console.WriteLine("Le mot ne contient pas les lettres : " + String.Join(", ", mauvaisesLettres));
                   
                }
                Console.WriteLine();
                
               
                if (viesRestantes == 0)
                {
                    AfficherPendu(mot, liste, NB_VIES, viesRestantes);
                    Console.WriteLine("PERDU ! Le mot était : " + mot);
                }
            }
        }

        static string[] ChargerLesMots(string nomFichier)
        {
            try
            {
                return File.ReadAllLines(nomFichier);
            }
            catch(Exception ex) {
                Console.Write("ERREUR de lecture du fichier : " +nomFichier+ ex.Message);
            }
            return null;
        }

        static bool DemanderDeRejouer()
        {
            //Voulez-vous rejouer (o/n)
            char reponse = DemanderUneLettre("Voulez-vous rejouer (o/n) ? : ");

            if (reponse == 'O')
            {
                return true;
            }
            else if (reponse == 'N')
            {
                Console.WriteLine("Merci et à la prochaine");
                return false;
            }
            else
            {
                Console.WriteLine("Vous devez entrer o ou n");
                return DemanderDeRejouer();
            }        
        }
        static void Main(string[] args)
        {
            var mots = ChargerLesMots("mots.txt");
            var rnd = new Random();

            if ((mots == null) || (mots.Length == 0))
            {
                Console.WriteLine("La liste de mots est vide");
            }
            else
            {
                string mot = mots[rnd.Next(mots.Length)].Trim().ToUpper();
                DevinerMot(mot);
                if(DemanderDeRejouer())
                {
                    Console.Clear();
                    Main(args);
                }
            } 
        }
    }
}
