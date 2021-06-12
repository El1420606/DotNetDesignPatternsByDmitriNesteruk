using System;
using System.Collections.Generic;

/**
 * 开闭原则
 * 
 * 
 * 
 * **/
namespace OpenClosedPrinciple
{
    public enum Color
    { 
        Red,Green,Blue
    }

    public enum Size
    { 
        Small,Medium,Large,Yuge
    }

    public class Product
    {
        public string Name { get; set; }
        public Color Color { get; set; }
        public Size Size { get; set; }

        public Product()
        {

        }

        public Product(string name, Color color, Size size)
        {
            if (name==null)
            {
                throw new ArgumentNullException(paramName: nameof(name));
            }
            Name = name;
            Color = color;
            Size = size;
        }

        
    }

    #region 传统方式定义
    public class ProductFilter
    {
        /// <summary>
        ///  根据尺寸进行过滤，并返回符合条件的产品
        /// </summary>
        /// <param name="products"></param>
        /// <param name="size"></param>
        /// <returns></returns>
        public IEnumerable<Product> FilterProductBySize(IEnumerable<Product> products, Size size)
        {
            foreach (var p in products)
            {
                if (p.Size == size)
                    yield return p;
            }
        }

        /// <summary>
        /// 根据颜色进行过滤，并返回符合条件的产品
        /// </summary>
        /// <param name="products"></param>
        /// <param name="color"></param>
        /// <returns></returns>
        public IEnumerable<Product> FilterProductByColor(IEnumerable<Product> products, Color color)
        {
            foreach (var p in products)
            {
                if (p.Color == color)
                    yield return p;
            }
        }

        /// <summary>
        /// 根据颜色和尺寸进行过滤，并返回符合条件的产品
        /// </summary>
        /// <param name="products"></param>
        /// <param name="color"></param>
        /// <returns></returns>
        public IEnumerable<Product> FilterProductByColorAndSize(IEnumerable<Product> products, Color color, Size size)
        {
            foreach (var p in products)
            {
                if (p.Color == color && p.Size == size)
                    yield return p;
            }
        }
    }
    #endregion


    #region 通过接口定义，每次新增条件过滤只要实现接口即可
    public interface ISpecification<T>
    {
        bool IsSatisfied(T t);
    }

    public interface IFilter<T>
    {
        IEnumerable<T> Filter(IEnumerable<T> items, ISpecification<T> spec);
    }

    /// <summary>
    /// 指定颜色的类实现指定条件的接口
    /// </summary>
    public class ColorSpecification : ISpecification<Product>
    {
        private Color color;

        public ColorSpecification(Color color)
        {
            this.color = color;
        }

        public bool IsSatisfied(Product t)
        {
            return t.Color == this.color;
        }
    }

    /// <summary>
    /// 指定尺寸实现指定条件的接口
    /// </summary>
    public class SizeSpecification : ISpecification<Product>
    {
        private Size size;

        public SizeSpecification(Size size)
        {
            this.size = size;
        }

        public bool IsSatisfied(Product t)
        {
            return t.Size == this.size;
        }
    }

    /// <summary>
    /// 指定两个条件的类实现指定条件的接口
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class AndSpecification<T> : ISpecification<T>
    {
        private ISpecification<T> first, second;

        public AndSpecification(ISpecification<T> first, ISpecification<T> second)
        {
            this.first = first??throw new ArgumentNullException(paramName:nameof(first));
            this.second = second??throw new ArgumentNullException(paramName:nameof(second));
        }

        public bool IsSatisfied(T t)
        {
            return first.IsSatisfied(t) && second.IsSatisfied(t);
        }
    }

    public class BetterFilter<T> : IFilter<T> where T :class,new ()
    {
        public IEnumerable<T> Filter(IEnumerable<T> items, ISpecification<T> spec)
        {
            foreach (var i in items)
                if (spec.IsSatisfied(i))
                    yield return i;
        }
    } 
    #endregion

    public class OpenClosed
    {
        static void Main(string[] args)
        {
            #region 传统方式过滤输出
            //var apple = new Product("Apple", Color.Green, Size.Small);
            //var tree = new Product("Tree", Color.Green, Size.Large);
            //var house = new Product("House", Color.Blue, Size.Large);

            //Product[] products = { apple, tree, house };
            //var pf = new ProductFilter();

            //Console.WriteLine("Green products(Old)");
            //foreach (var p in pf.FilterProductByColor(products, Color.Green))
            //{
            //    Console.WriteLine($"- {p.Name} is {nameof(Color.Green)}");
            //} 
            #endregion

            #region 通过接口方式过滤输出
            var apple = new Product("Apple", Color.Green, Size.Small);
            var tree = new Product("Tree", Color.Green, Size.Large);
            var house = new Product("House", Color.Blue, Size.Large);

            Product[] products = { apple, tree, house };
            var pf = new ProductFilter();

            Console.WriteLine("Green products(old)");
            foreach (var p in pf.FilterProductByColor(products, Color.Green))
            {
                Console.WriteLine($"- {p.Name} is {nameof(Color.Green)}");                
            }
            Console.WriteLine(" ");

            var bf = new BetterFilter<Product>();
            Console.WriteLine("Green products (new)");
            foreach (var p in bf.Filter(products,new ColorSpecification(Color.Blue)))
            {
                Console.WriteLine($"- {p.Name} is {nameof(Color.Blue)}");
            }

            Console.WriteLine(" ");
            Console.WriteLine("Large blue items");
            foreach (var p in bf.Filter(products,
                new AndSpecification<Product>(
                    new ColorSpecification(Color.Blue),
                    new SizeSpecification(Size.Large)
                    )
                ))
            {
                Console.WriteLine($"- {p.Name} is {nameof(Color.Blue)} and {nameof(Size.Large)}");
            }
            #endregion
        }
    }
}
