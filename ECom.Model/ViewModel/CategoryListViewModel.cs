//////using System.ComponentModel.DataAnnotations;

using System.ComponentModel.DataAnnotations;

namespace ECom.Model.ViewModel
{
    public class CategoryListViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int? CategoryId { get; set; }
        public string? ParentCategoryName { get; set; }

    }
    public class CategoryCreateViewModel
    {
        [Required(ErrorMessage = "Name is Required")]
        [StringLength(30)]
        public string Name { get; set; }
        [Required(ErrorMessage = "Description is Required")]
        [StringLength(200)]
        public string Description { get; set; }
        public int? CategoryId { get; set; }

    }
}