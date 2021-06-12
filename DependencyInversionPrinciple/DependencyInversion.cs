using static System.Console ;
using System.Collections.Generic;
using System.Linq;

/**
 * 依赖倒置原则
 * 高层模块(程序的调用者通常指UI)不应该依赖于底层模块(被调用者比如数据访问层)
 * 两者应该依赖于抽象，而不是具体实现
 * 
 * 
 * 
 * 
 * **/
namespace DependencyInversionPrinciple
{

    public enum RelationShip
    { 
        Parent,
        Child,
        Sibling
    }

    public class Person
    {
        public string Name { get; set; }
    }

    public interface IRelationshipBrowser
    {
        IEnumerable<Person> FindAllChildernOf(string name);
    }

    //low-level
    public class RelationShips:IRelationshipBrowser
    {
        private List<(Person, RelationShip, Person)> relations
            = new List<(Person, RelationShip, Person)>();

        public void AddParentAndChild(Person parent, Person child)
        {
            relations.Add((parent,RelationShip.Parent,child));
            relations.Add((child,RelationShip.Child,parent));
        }

        public IEnumerable<Person> FindAllChildernOf(string name)
        {
            return relations.Where(
                 x => x.Item1.Name == name &&
                 x.Item2 == RelationShip.Parent
                ).Select(r => r.Item3);
        }

        //public List<(Person, RelationShip, Person)> Relations => relations;
    }
    public class DependencyInversion
    {
        //public DependencyInversion(RelationShips relationShips)
        //{
        //    var relations = relationShips.Relations;
        //    foreach (var r in relations.Where(
        //            x=>x.Item1.Name=="John"&&
        //            x.Item2==RelationShip.Parent
        //        ))
        //    {
        //        WriteLine($"John has a child called {r.Item3.Name}");
        //    }
        //}

        public DependencyInversion(IRelationshipBrowser browser,string name)
        {
            foreach (var p in browser.FindAllChildernOf(name))
            {
                WriteLine($"John has a child called {p.Name}");
            }
        }
        static void Main(string[] args)
        {
            var parent = new Person { Name="John"};
            var child1 = new Person { Name="Chris"};
            var child2 = new Person { Name="Mary"};

            var relationShips = new RelationShips();
            relationShips.AddParentAndChild(parent,child1);
            relationShips.AddParentAndChild(parent,child2);
            new DependencyInversion(relationShips,"John");
        }
    }
}
