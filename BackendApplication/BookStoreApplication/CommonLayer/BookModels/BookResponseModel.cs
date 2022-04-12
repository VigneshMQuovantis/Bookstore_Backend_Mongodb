using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLayer.BookModels
{
    public class BookResponseModel
    {
        public string BookId { get; set; }
        public string BookName { get; set; }

        public string BookAuthor { get; set; }

        public long OriginalPrice { get; set; }

        public long DiscountPrice { get; set; }

        public long BookQuantity { get; set; }

        public string BookDetails { get; set; }

        public long TotalRatings { get; set; }

        public long NoOfPeopleRated { get; set; }

        public string AddedAt { get; set; }
    }
}
