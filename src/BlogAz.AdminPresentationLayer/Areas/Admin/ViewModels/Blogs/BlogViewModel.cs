using System.ComponentModel.DataAnnotations;

namespace BlogAz.AdminPresentationLayer.Areas.Admin.ViewModels.Blogs
{
    public class BlogViewModel
    {
        public long? Id { get; set; }

        [Required(ErrorMessage = "Başlıq mütləq doldurulmalıdır.")]
        [StringLength(60, ErrorMessage = "Başlıq ən çox 60 simvol olmalıdır.")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Şəkil mütləq seçilməlidir.")]
        public IFormFile ImageFile { get; set; }

        [Required(ErrorMessage = "Məzmun mütləq doldurulmalıdır.")]
        [StringLength(600, ErrorMessage = "Məzmun ən çox 600 simvol olmalıdır.")]
        public string Content { get; set; }

        [Required(ErrorMessage = "Kateqoriyalar mütləq seçilməlidir.")]
        public List<long> CategoryIds { get; set; }

        public string ImageName { get; set; }
    }
}
