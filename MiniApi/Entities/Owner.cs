
namespace MiniApi.Entities
{
    public class Owner
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Raccoon> Raccoons { get; set; }
    }
}
