namespace OrderFlow.Shared;

public class OrderModel
{
    public string Number { get; set; }

    public DateTime? Date { get; set; }

    public CustomerModel Customer { get; set; }

    public List<ItemOrder> Items { get; set; } = new List<ItemOrder>();

    public decimal TotalValue { get; set; }

    public OrderPaymentModel Payment { get; set; } = new OrderPaymentModel();
}

public class OrderPaymentModel
{
    public string PaymentMethod { get; set; }
    public string PaymentStatus { get; set; }
    public bool IsPaid { get; set; }

    public DateTime? DatePayment { get; set; }
}