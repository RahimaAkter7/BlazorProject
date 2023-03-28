using System.ComponentModel.DataAnnotations.Schema;

namespace MasterDetail.Shared
{
    public class StoreEntry
    {
        public int StoreEntryId { get; set; }
        [ForeignKey("Flower")]
        public int FlowerId { get; set; }
        [ForeignKey("BouquetType")]
        public int BouquetTypeId { get; set; }

        //Navigation
        public virtual Flower? Flower { get; set; }
        public virtual BouquetType? BouquetType { get; set; }
    }
}
