using System;

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
        static char DemanderUneLettre() { 

            while (true) {
                // Rentrez une lettre
                Console.WriteLine("Deviner une lettre : ");
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
        static void DevinerMot(string mot)
        {
            var liste = new List<char>();
            // Boucler (true)
            while(true)
            {
                AfficherMot(mot, liste);
                Console.WriteLine();
                char lettre = DemanderUneLettre();
                Console.Clear();
                if(mot.Contains(lettre))
                {
                    Console.WriteLine("Cette lettre est dans le mot."); 
                    liste.Add(lettre);
                }
                else {
                    Console.WriteLine("Cette lettre n'est pas dans le mot.");
                }
                Console.WriteLine();
            }
            // AfficherMot
            // DemanderUneLettre
            //  -> cette lettre est dans le mot -> List<char> add()
            //  -> cette lettre n'est pas dans le mot 
        }
        static void Main(string[] args)
        {
            string mot = "ELEPHANT";
            // char lettre = DemanderUneLettre();
            // AfficherMot(mot, new List<char> {lettre});

           DevinerMot(mot);
        }
    }
}
