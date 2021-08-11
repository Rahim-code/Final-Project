namespace LabradogApp.Models
{
    public class Image:BaseEntity
    {
        public string DogImage { get; set; }
        public string DogName { get; set; }
        public int Old { get; set; }
    }
}