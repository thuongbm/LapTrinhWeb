using NuGet.Versioning;
using System.ComponentModel.DataAnnotations;

namespace Lab1.Models
{
    public class Student
    {
        public int Id { get; set; }//Mã sinh viên
        [Required(ErrorMessage = "Ho và tên phải được nhập")]
        [StringLength(100, MinimumLength = 4, ErrorMessage = "Họ tên phải từ 4 đến 100 kí tự") ]
        [Display(Name = "Họ và tên")]
        public string? Name { get; set; } //Họ tên
        [Required(ErrorMessage = "Email phải được nhập")]
        [RegularExpression(@"^[A-Za-z0-9._%+-]+@gmail\.com$", ErrorMessage = "Email phải có đuôi @gmail.com")]
        [Display(Name = "Địa chỉ Email")]
        public string? Email { get; set; } //Email
        [Required(ErrorMessage = "Mật khẩu phải được nhập")]
        [StringLength(100, MinimumLength = 8, ErrorMessage = "Mật khẩu phải ít nhất 8 kí tự")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$",
            ErrorMessage = "Mật khẩu phải có chữ hoa, chữ thường, chữ số và ký tự đặc biệt")]
        [DataType(DataType.Password)]
        public string? Password { get; set; }//Mật khẩu
        public Branch? Branch { get; set; }//Ngành học
        public Gender? Gender { get; set; }//Giới tính
        public bool IsRegular { get; set; }//Hệ: true chính qui, false-phi cq
        public string? Address { get; set; }//Địa chỉ
        [Range(typeof(DateTime), "1/1/1963", "1/1/2006", ErrorMessage = "Ngày sinh không hợp lệ")]
        [DataType(DataType.Date)]
        [Required(ErrorMessage = "Ngày sinh phải được nhập")]
        public DateTime DateOfBirth { get; set; }//Ngày sinh
        public string? Photo { get; set; }//Ảnh
        [Required(ErrorMessage = "Điểm phải được nhập")]
        [Range(0.0, 10.0, ErrorMessage = "Điểm từ 0 đến 10")]
        [Display(Name = "Điểm ")]
        public double Score { get; set; }//Điểm trung bình
    }
}
