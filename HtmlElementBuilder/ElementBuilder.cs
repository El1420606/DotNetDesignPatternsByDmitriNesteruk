using System;
using System.Collections.Generic;
using System.Text;
using static System.Console;

/**
 * 建造者模式
 * 
 * 
 * 
 * **/
namespace HtmlElementBuilder
{
    public class HtmlElement
    {
       
        public string Name, Text;
        public List<HtmlElement> Elements = new List<HtmlElement>();
        private const int indentSize = 2;

        public HtmlElement()
        {

        }
        public HtmlElement(string name, string text)
        {
            Name = name?? throw new ArgumentNullException(paramName:nameof(name));
            Text = text??throw new ArgumentNullException(paramName:nameof(text));
        }

        private string ToStringImpl(int indent)
        {
            var sb = new StringBuilder();
            var i = new string(' ', indentSize * indent);
            sb.AppendLine($"{i}<{Name}>");
            if (!string.IsNullOrWhiteSpace(Text))
            {
                sb.Append(new string(' ', indentSize * (indent + 1)));
                sb.AppendLine(Text);
            }

            foreach (var e in Elements)
            {
                sb.Append(e.ToStringImpl(indent + 1));
            }
            sb.AppendLine($"{i}</{Name}>");
            return sb.ToString();
        }

        public override string ToString()
        {
            return ToStringImpl(0);
        }
    }

    public class HtmlBuilder
    {
        private readonly string rootName;
        HtmlElement root = new HtmlElement();
        public HtmlBuilder(string rootName)
        {
            this.rootName = rootName;
            root.Name = rootName;
        }
        /// <summary>
        /// 追加子元素，将当前对象返回去
        /// </summary>
        /// <param name="childName"></param>
        /// <param name="childText"></param>
        /// <returns></returns>
        public HtmlBuilder AddChild(string childName, string childText)
        {
            var e = new HtmlElement(childName, childText);
            root.Elements.Add(e);
            return this;
        }

        public override string ToString()
        {
            return root.ToString();
        }

        public void Clear()
        {
            root = new HtmlElement() { Name=rootName};
        }
    }
    class ElementBuilder
    {
        static void Main(string[] args)
        {
            GoBuildElments();
        }

        static void GoBuildElments()
        {
            //string hello = "helloworld";
            //var sb = new StringBuilder();
            //sb.Append("<p>");
            //sb.Append(hello);
            //sb.Append("</p>");
            //WriteLine(sb);

            //sb.Clear();

            //sb.AppendLine("<ul>");
            //string[] words = { "hello", "world" };
            //foreach (var item in words)
            //{
            //    sb.AppendFormat("<li>{0}</li>{1}", item, Environment.NewLine);
            //}
            //sb.Append("</ul>");
            //WriteLine(sb);

            var builder = new HtmlBuilder("ul");
            builder.AddChild("li", "Hello")
            .AddChild("li","World")
            .AddChild("h1", "标题");
            WriteLine(builder.ToString());
        }
    }
}
