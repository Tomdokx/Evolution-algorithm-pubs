using OfficeOpenXml;
using Sem_2_NNUI1;
using Sem_2_NNUI1.GeneticAlgoritm;
using System.Text;

public static class Program
{
    static void Main(string[] args)
    {
        const int numberOfIndividuals = 8000;
        const int numberOfGenerations = 5000;

        DateTime start = DateTime.Now;
        ExcelReaderAndParser exRAP = new ExcelReaderAndParser();
        string[] lines = exRAP.ReadTheExcel(@"<data file>");
        List<Pub> data = exRAP.ParseLinesToListOfPubs(lines);
        List<Individual> list = new List<Individual>();


        for (int i = 0; i < numberOfIndividuals; i++)
        {
            list.Add(new Individual(data));
        }

        Generation generation = new Generation { Individuals= list };

        for(int i = 0; i< numberOfGenerations; i++)
        {
            generation.ApplyInverseTournament();
            var kids = generation.MakeKids(generation.MakePairs());
            generation.Individuals = generation.Individuals.Union(kids).ToList();

            // writes the best from every 250 generations.
            if (i%250 == 0)
            {
                Console.WriteLine(i + ") " + generation.bestOneOfAll.Rating);
                Console.WriteLine(generation.bestOneOfAll.ToString());
            }
                
        }
        Console.WriteLine(numberOfGenerations + ") " + generation.bestOneOfAll.Rating);

        DateTime end = DateTime.Now;
        Console.WriteLine($"{end - start}");
    }
}

// 10000 number, 8000 individuals -> 16.286697 -> 1-10 from both sides, 100% mutation.
// 10000 number, 8000 individuals -> 16.16 -> 1-10 from both sides, 100% mutation.
