namespace ECom.Model.ViewModel
{
    public class ProductListViewModel
    {

        public int Id { get; set; }
        public string Name { get; set; }
        public string? PicturePath { get; set; }
        public string Description { get; set; }
        public double price { get; set; }
        public double Stock { get; set; }
        public Units Units { get; set; }
        public int CategoryId { get; set; }
        public string? CategoryName { get; set; }


     

    }
}