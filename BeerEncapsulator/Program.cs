using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeerEncapsulator
{
    class Program
    {
        static void Main(string[] args)
        {
            BeerEncapsulator myFactory = new BeerEncapsulator();

            bool replay = true;

            while (replay)
            {
                Console.WriteLine();
                Console.WriteLine("######### C# Beer Factory #########");
                Console.WriteLine();
                myFactory.AddBeer();
                myFactory.SetCapsulesQuantity();
                myFactory.SetBottlesQuantity();

                int bottleToProduce = myFactory.GetBottlesToProduce();
                int bottleProduced = myFactory.ProduceEncapsulatedBeerBottles(bottleToProduce);
                
                Console.WriteLine();
                if(bottleProduced > 0)
                {
                    Console.WriteLine($"YASS ! {bottleProduced} bouteilles ont été produites.");
                } else if(bottleProduced == 0)
                {
                    Console.WriteLine("Aucune bouteille n'a été produite.");
                }

                // RETRIEVE QUANTITIES LEFT
                Console.WriteLine();
                double beerVolumeAvailable = myFactory.GetBeerVolume();
                Console.WriteLine($"Il reste {beerVolumeAvailable} litres de bière en stock.");

                int capsulesAvailables = myFactory.GetCapsulesQuantity();
                Console.WriteLine($"Il reste {capsulesAvailables} capsules en stock.");

                int bottleAvailables = myFactory.GetBottleQuantity();
                Console.WriteLine($"Il reste {bottleAvailables} bouteilles en stock.");

                // RESTART
                string restartAnswer = "";
                while(restartAnswer != "n" && restartAnswer != "o")
                {
                    Console.WriteLine();
                    Console.Write("Voulez-vous recommencer (o/n)? ");
                    restartAnswer = Console.ReadLine();
                }

                if (restartAnswer == "n") break;
                restartAnswer = "";
            }


        }

        public class BeerEncapsulator
        {
            private double _avalaibleBeerVolume;
            private int _avalaibleBottles;
            private int _avalaibleCapsules;
            private double _beerVolume = 0.33;

            public void AddBeer()
            {
                Console.Write("");
                double userInput = -1;
                while (userInput < 0)
                {
                    Console.Write($"Précisez en litres le volume de bière que vous ajoutez au volume actuel ({_avalaibleBeerVolume} L) (nombre positif): ");
                    userInput = Convert.ToDouble(Console.ReadLine());
                }
                _avalaibleBeerVolume += userInput;
            }

            public void SetBottlesQuantity()
            {
                Console.Write("");
                int userInput = -1;
                while (userInput < 0)
                {
                    Console.Write("Précisez le nombre de bouteilles disponibles (entier positif): ");
                    userInput = Convert.ToInt32(Console.ReadLine());
                }
                _avalaibleBottles = userInput;
            }

            public void SetCapsulesQuantity()
            {
                Console.Write("");
                int userInput = -1;
                while (userInput < 0)
                {
                    Console.Write("Précisez le nombre de capsules disponibles (entier positif): ");
                    userInput = Convert.ToInt32(Console.ReadLine());
                }
                _avalaibleCapsules = userInput;
            }

            public double GetBeerVolume()
            {
                return _avalaibleBeerVolume;
            }

            public int GetCapsulesQuantity()
            {
                return _avalaibleCapsules;
            }

            public int GetBottleQuantity()
            {
                return _avalaibleBottles;
            }

            private bool isEnoughBeer(int beersToPrepare)
            {
                return _avalaibleBeerVolume > (beersToPrepare * _beerVolume);
            }

            private bool isEnoughCapsules(int beersToPrepare)
            {
                return _avalaibleCapsules > beersToPrepare;
            }

            private bool isEnoughBottles(int beersToPrepare)
            {
                return _avalaibleBottles > beersToPrepare;
            }

            public int GetBottlesToProduce()
            {
                int userInput = -1;
                while (userInput < 0)
                {
                    Console.Write("Combien de bouteilles voulez-vous produire ? ");
                    userInput = Convert.ToInt32(Console.ReadLine());
                }
                return userInput;
            }

            public int ProduceEncapsulatedBeerBottles(int bottlesToPrepare)
            {
                bool isBeerOK = isEnoughBeer(bottlesToPrepare);
                bool capsulesOK = isEnoughCapsules(bottlesToPrepare);
                bool bottlesOK = isEnoughBottles(bottlesToPrepare);
                int bottlesProduced;

                if(isBeerOK && capsulesOK && bottlesOK)
                {
                    _avalaibleBeerVolume -= _beerVolume * bottlesToPrepare;
                    _avalaibleBottles -= bottlesToPrepare;
                    _avalaibleCapsules -= bottlesToPrepare;
                    bottlesProduced = bottlesToPrepare;
                } else
                {
                    Console.WriteLine();
                    Console.WriteLine("ALERTE ! Action impossible...");
                    Console.WriteLine();
                    if (!isBeerOK)
                    {
                        Console.WriteLine($"Il manque {Math.Round(bottlesToPrepare * _beerVolume - _avalaibleBeerVolume, 2)} litres de bière pour la fabrication.");
                    }
                    if (!capsulesOK)
                    {
                        Console.WriteLine($"Il manque {bottlesToPrepare - _avalaibleCapsules} capsules pour la fabrication.");
                    }
                    if (!bottlesOK)
                    {
                        Console.WriteLine($"Il manque {bottlesToPrepare - _avalaibleBottles} bouteilles pour la fabrication.");
                    }
                    bottlesProduced = 0;
                }

                return bottlesProduced;
            }
        }
    }
}
