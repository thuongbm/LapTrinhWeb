using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace SoPhucw
{
    class SoPhuc
    {
        public float A { get; set; }
        public float B { get; set; }

        public SoPhuc()
        {
            A = 0;
            B = 0;
        }
        public SoPhuc(float a, float b)
        {
            this.A = a;
            this.B = b;
        }

        public void Nhap()
        {
            Console.Write("Nhap phan thuc: ");
            A = float.Parse(Console.ReadLine());    
            Console.Write("Nhap phan ao: ");
            B = float.Parse(Console.ReadLine());
        }
        public void Xuat()
        {
            if (B > 0)
                Console.WriteLine($"{A} + {B}i");
            else if (B < 0)
                Console.WriteLine($"{A} - {-B}i");
            else
                Console.WriteLine($"{A}");
        }

        public SoPhuc Cong(SoPhuc other)
        {
            return new SoPhuc(this.A + other.A, this.B + other.B);
        }

        public SoPhuc Tru(SoPhuc other)
        {
            return new SoPhuc(this.A - other.A, this.B - other.B);
        }

        public SoPhuc Nhan(SoPhuc other)
        {
            return new SoPhuc(this.A * other.A - this.B * other.B, this.A * other.B + this.B * other.A);
        }

        public SoPhuc Chia(SoPhuc other)
        {
            float denominator = other.A * other.A + other.B * other.B;
            if (denominator == 0)
            {
                throw new DivideByZeroException("Khong the chia cho so phuc co phan thuc va phan ao bang 0.");
            }
            return new SoPhuc((this.A * other.A + this.B * other.B) / denominator, (this.B * other.A - this.A * other.B) / denominator);
        }

    }

    internal class Program
    {
        static void Main(string[] args)
        {
            //SoPhuc soPhuc1 = new SoPhuc(3, 4);
            //SoPhuc soPhuc2 = new SoPhuc(5, -6);
            //SoPhuc soPhuc3 = new SoPhuc(7, 0);

            SoPhuc soPhuc1 = new SoPhuc();
            soPhuc1.Nhap();
            SoPhuc soPhuc2 = new SoPhuc();
            soPhuc2.Nhap();
            
            soPhuc1.Xuat();
            soPhuc2.Xuat();

            SoPhuc soPhuc3 = soPhuc1.Cong(soPhuc2);
            Console.Write("Tong hai so phuc: ");
            soPhuc3.Xuat();

        }
    }
}
