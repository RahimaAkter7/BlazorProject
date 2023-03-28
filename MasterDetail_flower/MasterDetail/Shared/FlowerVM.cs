using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace MasterDetail.Shared
{
    public class FlowerVM
    {
        public int FlowerId { get; set; }
        public string FlowerName { get; set; } = default!;
        [Required, DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime StoreDate { get; set; } = DateTime.Now;
        public int Quantity { get; set; }
        public string? Picture { get; set; }
        public IFormFile? PictureFile { get; set; }
        public bool StoreAvaible { get; set; }
        public List<BouquetType> BouquetTypeList { get; set; } = new List<BouquetType>();

        public virtual ICollection<StoreEntry>? StoreEntries { get; set; } = new List<StoreEntry>();

    }
}
