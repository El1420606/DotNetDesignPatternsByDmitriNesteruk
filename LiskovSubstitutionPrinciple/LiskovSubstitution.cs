using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/**
    * 里氏替换原则
    * 
    * 
    * **/
namespace LiskovSubstitutionPrinciple
{

    public class Rectangle
    {
        private double height;
        private double width;

        public Rectangle()
        {

        }
        public Rectangle(double width, double height)
        {
            Width = width;
            Height = height;
        }

        
        public virtual double Width { get => width; set => width = value; }
        public virtual double Height { get => height; set => height = value; }

        public override string ToString()
        {
            return $"{nameof(Width)}:{Width},{nameof(Height)}:{Height}";
        }
    }

    /// <summary>
    /// 正方形 重写父类的属性
    /// </summary>
    public class Square : Rectangle
    {
        public override double Width {  set => base.Width=base.Height = value; }
        public override double Height { set => base.Width = base.Height = value; }
    }
    public class LiskovSubstitution
    {
        public static double Area(Rectangle r) => r.Height * r.Width;
        static void Main(string[] args)
        {
            Rectangle rc = new Rectangle(10,20);
            Console.WriteLine($"{rc} has area {Area(rc)}");

            Rectangle sq = new Square();
            sq.Width = 4;
            Console.WriteLine($"{sq} has area {Area(sq)}");
        }
    }
}
