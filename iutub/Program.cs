using System;
using System.Collections.Generic;

namespace iutub
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            var salut = new Hola("Salut!");
            Console.WriteLine(salut.saluda);
            //string[] tags = {"sustainable","eco-friendly"};
            var tags2 = new List<string>{"sustainable","eco-friendly"};
            var vid = new Video("titol", tags2, 101);
            //var vid = new Video("titol", tags, 101);
            //var vid = new Video("titol", "tag1,tag2", 101);
            Console.WriteLine($"New video: {vid.Title}");
        }
    }
}
