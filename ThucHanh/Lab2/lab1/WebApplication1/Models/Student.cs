namespace WebApplication1.Models
{
    public class Student
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? PassWord { get; set; }
        public Branchs Branchs { get; set;  }
        public Gender Gender { get; set; }
        public bool IsRegular { get; set; }
        public string? Address { get; set; }
        public DateTime DateOfBorth { get; set; }
        public string? PhotoFileName { get; set; }
    }
}
