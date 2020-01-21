namespace ReflectionDynamicSort.Models
{
    using Enumerations;

    public class OrderModel
    {
        public OrderModel(string propertyName)
        {
            PropertyName = propertyName;
            OrderType = OrderTypeEnum.Ascending;
        }

        public OrderModel(
            string propertyName, 
            OrderTypeEnum orderType) : this(propertyName)
        {
            OrderType = orderType;
        }

        public string PropertyName { get; set; }
        public OrderTypeEnum OrderType { get; set; }
    }
}
