namespace WebApplicationXml.Models;

public class Student
{
    public required string Masv { get; set; }
    public required string Hoten { get; set; }

    private string _lop = string.Empty; // Khởi tạo giá trị mặc định

    public required string Lop
    {
        get => _lop;
        set => _lop = value?.Trim().ToLower() ?? string.Empty; // Chuyển thành chữ thường và loại bỏ khoảng trắng
    }

    public required string Namsinh { get; set; }
    public required string Gioitinh { get; set; }
    public required string Sdt { get; set; }
    public required string Diachi { get; set; }
}
