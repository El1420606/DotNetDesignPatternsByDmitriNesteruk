using System;

/**
 * 接口隔离原则
 *  
 * 
 * 
 * 
 * 
 * 
 * **/
namespace InterfaceSegregationPrinciple
{

    public class Document
    {

    }

    #region 胖接口，一次定义很多功能，不灵活
    public interface IMachine
    {
        void Print(Document d);
        void Scan(Document d);
        void Fax(Document d);
    }

    /// <summary>
    /// 新式多功能打印机支持接口的所有功能
    /// </summary>
    public class MultiFunctionPrinter : IMachine
    {
        public void Fax(Document d)
        {
            //
        }

        public void Print(Document d)
        {
            //
        }

        public void Scan(Document d)
        {
            //
        }
    }

    /// <summary>
    /// 老式打印机不具备这么多功能，因此之前定义的接口是有问题的
    /// </summary>
    public class OldFashionedPrinter : IMachine
    {
        public void Fax(Document d)
        {
            //
        }

        public void Print(Document d)
        {
            //
        }

        public void Scan(Document d)
        {
            //
        }
    }
    #endregion

    #region 遵循接口隔离原则，每个接口定义少一点功能通过组合的方式，减少依赖
    public interface IPrinter
    {
        void Print(Document d);
    }

    public interface IScaner
    {
        void Scan(Document d);
    }

    public interface IFaxer
    {
        void Fax(Document d);
    }
    #endregion

    public class Photocopier : IPrinter, IScaner
    {
        public void Print(Document d)
        {
            throw new NotImplementedException();
        }

        public void Scan(Document d)
        {
            throw new NotImplementedException();
        }
    }

    #region 根据不同的需求实现不同的接口
    public interface IMultiFunctionDevice : IPrinter, IScaner
    {

    }

    /// <summary>
    /// 多功能设备
    /// </summary>
    public class MultiFunctionMachine : IMultiFunctionDevice
    {
        private IPrinter printer;
        private IScaner scaner;

        public MultiFunctionMachine(IPrinter printer, IScaner scaner)
        {
            this.printer = printer;
            this.scaner = scaner;
        }

        public void Print(Document d)
        {
            printer.Print(d);
        }

        public void Scan(Document d)
        {
            scaner.Scan(d);
        }
    } 
    #endregion

    public class InterfaceSegregation
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
        }
    }
}
