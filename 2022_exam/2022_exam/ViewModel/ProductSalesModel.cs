namespace _2022_exam.ViewModel
{
    class ProductSalesModel
    {
        public int ProductId { get; set; }
        public decimal TotalSales { get; set; }

        public override string ToString()
        {
            return $"{ProductId} : {TotalSales}";
        }
    }
}

