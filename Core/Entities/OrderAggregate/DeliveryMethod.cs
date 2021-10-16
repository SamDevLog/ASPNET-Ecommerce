namespace Core.Entities.OrderAggregate
{
    //BaseEntity to give this class a DB table to allow the client to retrieve a list of delivery methods.
    public class DeliveryMethod : BaseEntity {
        public string ShortName { get; set; }
        public string DeliveryTime { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
    }
}