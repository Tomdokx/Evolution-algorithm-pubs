namespace Sem_2_NNUI1
{
    public class Pub
    {
        public int? Id { get; set; }
        public string? Name { get; set; }
        public PointWithDouble? Position { get; set; }

        public override string? ToString()
        {
            return $"Id: {Id}, Name: {Name}, Position:{Position.ToString()}";
        }
    }

    
}
