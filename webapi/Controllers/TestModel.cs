namespace webapi.Controllers
{
    public class TestModel
    {
        public int Id { get; set; }
        public string ImageName { get; set; }
        public IFormFile? ImageFile { get; set; }
    }
}