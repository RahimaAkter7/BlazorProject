namespace MasterDetail.Shared
{
    public class Flower
    {
        public int FlowerId { get; set; }
        public string FlowerName { get; set; } = default!;
        public DateTime StoreDate { get; set; }
        public int Quantity { get; set; }
        public string? Picture { get; set; } = null!;
        public bool StoreAvaible { get; set; }
        //Nev
        public virtual ICollection<StoreEntry>? StoreEntries { get; set; } = new List<StoreEntry>();
    }
}
