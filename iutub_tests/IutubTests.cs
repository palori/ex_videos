// folder created in the terminal with: dotnet new xunit -o iutub_tests

using System;
using Xunit;
using System.Collections.Generic;
using iutub;

namespace iutub_tests
{
    public class IutubTests
    {
        [Fact]
        public void Test1()
        {
            Assert.True(true); // hello world test
        }

        [Fact]
        public void Test2()
        {
            /*var title = "titol";
            var tags = new List<string>{"sustainable","eco-friendly"};
            var vid = new Video(title, tags, 101);
            //Console.WriteLine($"New video: {vid.Title}");
            title.Equals(vid.Title);*/
            var h = new Hola("salut!");
            h.saluda.Equals("salut!");
        }
    }
}
