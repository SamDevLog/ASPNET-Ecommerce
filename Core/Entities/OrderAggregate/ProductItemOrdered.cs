namespace Core.Entities.OrderAggregate
{
    //snapshot of the product item at the time of order in case its details change after the order was made.
    public class ProductItemOrdered 
    {
        public ProductItemOrdered () { }

        public ProductItemOrdered (int productItemId, string productName, string pictureUrl) {
            ProductItemId = productItemId;
            ProductName = productName;
            PictureUrl = pictureUrl;
        }

        public int ProductItemId { get; set; }
        public string ProductName { get; set; }
        public string PictureUrl { get; set; }
    }
}