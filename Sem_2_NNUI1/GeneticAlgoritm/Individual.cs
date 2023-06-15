using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Sem_2_NNUI1.GeneticAlgoritm
{
    public class Individual
    {
        public Pub[] Pubs { get; set; }
        public double Rating { get; set; } = 0;


        public Individual(Individual individual)
        {
                Pubs = individual.Pubs;
                Rating = individual.Rating;
        }

        public Individual(Pub[] pubs)
        {
            Pubs = pubs;
            Rating = GetSumOfDistances();
        }

        public Individual(List<Pub> pubs)
        {
            
            Random rnd = new Random();
            List<Pub> list = new List<Pub>(pubs);
            int max = pubs.Count;
            foreach (Pub pub in pubs)
            {
                list.Add(pub);
            }

            //Shuffle
            list = new List<Pub>(pubs.OrderBy(x => rnd.Next() % max));
            
            Pubs = list.ToArray();
            Rating = GetSumOfDistances();
        }

        public double GetSumOfDistances()
        {
            
            double sum = 0;
            for(int i = 0; i < Pubs.Length - 1; i++) {
                sum += Helper.CalculateDistance(Pubs[i], Pubs[i + 1]);
            }
            return sum;
        }

        public override string? ToString()
        {
            string s = "";
            foreach(Pub pub in Pubs)
            {

                s += pub.Id;
                if (pub != Pubs[Pubs.Length - 1])
                    s += ", ";

			}
            return s;
        }
    }
}
