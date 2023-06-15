using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sem_2_NNUI1.GeneticAlgoritm
{
    public class Generation
    {
        public List<Individual> Individuals { get; set; } = new List<Individual>();

        private Random rnd = new Random();

        public Individual bestOneOfAll { get; set; }

        public List<Pair> MakePairs()
        {
            var pairs = new List<Pair>();
            var max = Individuals.Count;
            //shuffle
            List<Individual> list = new List<Individual>(Individuals).OrderBy(x => rnd.Next() % max).ToList();
            for (int i = 0; i < list.Count / 2; i++)
            {
                pairs.Add(new Pair {parent1 = Individuals[i], parent2 = Individuals[i+list.Count/2]});
            }
            return pairs;
        }

        public List<Individual> ApplyInverseTournament()
        {
            var bestInGeneration = Individuals.OrderBy(x => x.Rating).ToList()[0];
            if (bestOneOfAll == null || bestOneOfAll.Rating > bestInGeneration.Rating)
                bestOneOfAll = bestInGeneration;

            List<Individual> result = new List<Individual>();

            var pairs = MakePairs();
            foreach (Pair p in pairs)
            {
                result.Add(p.parent1.Rating > p.parent2.Rating ? p.parent2 : p.parent1);
            }

            Individuals = result;
            return result;
        }

        public List<Individual> MakeKids(List<Pair> parents)
        {
            List<Individual> kids = new List<Individual>();

            foreach(Pair p in parents)
            {
                var k1Pubs = p.parent1.Pubs.Select(x => x).ToArray();
                var k2Pubs = p.parent2.Pubs.Select(x => x).ToArray();
                
                int fromBeggining = rnd.Next(1,10);
                int fromEnd = rnd.Next(1,10);
                bool b = rnd.Next(0,1) == 0;
                
                for (int i = 0; i >= fromBeggining; i++)
                {
                    var temp = k1Pubs[i];
                    k1Pubs[i] = k2Pubs[i];
                    k2Pubs[i] = temp;
                }
                DoCorrection(k1Pubs, k2Pubs,true);
                
                 
                for (int i = k1Pubs.Length; i <= fromEnd; i--)
                {
                    var temp = k1Pubs[i];
                    k1Pubs[i] = k2Pubs[i];
                    k2Pubs[i] = temp;
                }
                DoCorrection(k1Pubs, k2Pubs,false);


                int x = rnd.Next(0, 100);
                if (x <= 60 && x >= 40)
                {
                    DoMutation(k1Pubs);
                }
                if (x <= 70 && x >= 50)
                {
                    DoMutation(k2Pubs);
                }

                kids.Add(new Individual(k1Pubs));
                kids.Add(new Individual(k2Pubs));
            }

            return kids;
        }

        private void DoMutation(Pub[] kidPubs)
        {
                var pub1Index = rnd.Next(1, kidPubs.Length -1);
                int pub2Index;
                while (true)
                {
                    pub2Index = rnd.Next(1, kidPubs.Length -1);
                    if (pub2Index != pub1Index) break;
                }

                var temp = kidPubs[pub1Index];
                kidPubs[pub1Index] = kidPubs[pub2Index];
                kidPubs[pub2Index] = temp;
        }

        private List<Pub[]> DoCorrection(Pub[] pubs1, Pub[] pubs2,bool fromBeg) {
            List<Pub[]> ret = new List<Pub[]>();
            var duplicates1 = pubs1.GroupBy(x => x).Where(x => x.Count() > 1).Select(y => y.Key).ToList();
            var duplicates2 = pubs2.GroupBy(x => x).Where(x => x.Count() > 1).Select(y => y.Key).ToList();
            List<int> indexes1 = new List<int>();
            List<int> indexes2 = new List<int>();
            if (fromBeg)
            {
                for(int i = 0; i < pubs1.Length; i++) {
                    if (duplicates1.Contains(pubs1[i]))
                    {
                        indexes1.Add(i);
                        duplicates1.Remove(pubs1[i]);
                    }
                    if (duplicates2.Contains(pubs2[i]))
                    {
                        indexes2.Add(i);
                        duplicates2.Remove(pubs2[i]);
                    }
                }
            }
            else
            {
                for (int i = pubs1.Length-1; i > 0; i--)
                {
                    if (duplicates1.Contains(pubs1[i]))
                    {
                        indexes1.Add(i);
                        duplicates1.Remove(pubs1[i]);
                    }
                    else if (duplicates2.Contains(pubs2[i]))
                    {
                        indexes2.Add(i);
                        duplicates2.Remove(pubs2[i]);
                    }
                }
            }
            for(int i = 0; i < indexes1.Count; i++)
            {
                var temp = pubs1[indexes1[i]];
                pubs1[indexes1[i]] = pubs2[indexes2[i]];
                pubs2[indexes2[i]] = temp;
            }
            return ret;
        }
    }
}
