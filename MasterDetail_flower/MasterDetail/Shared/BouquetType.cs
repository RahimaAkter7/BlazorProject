using System.Text.Json.Serialization;

namespace MasterDetail.Shared
{
    public class BouquetType
    {
        public int BouquetTypeId { get; set; }
        public string? BouquetTypeName { get; set; } = default!;
        //Nev
        [JsonIgnore]
        public virtual ICollection<StoreEntry>? StoreEntries { get; set; } = new List<StoreEntry>();
    }
}
