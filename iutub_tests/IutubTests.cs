// folder created in the terminal with: dotnet new xunit -o iutub_tests

using System;
using Xunit;
using System.Collections.Generic;
using iutub;

namespace iutub_tests
{
    public class IutubTests
    {
        // Simple test
        [Fact]
        public void Test1()
        {
            Assert.True(true);
        }

        // Video tests
        [Fact]
        public void Test_VideoTitle()
        {
            string title = "titol";
            var tags = new List<string>{"sustainable","eco-friendly"};
            int id = 101;
            var vid = new Video(title, tags, id);
            title.Equals(vid.Title);
        }

        [Fact]
        public void Test_VideoTags()
        {
            string title = "titol";
            var tags = new List<string>{"sustainable","eco-friendly"};
            int id = 101;
            var vid = new Video(title, tags, id);
            tags.Equals(vid.Tags);
        }

        [Fact]
        public void Test_VideoId()
        {
            string title = "titol";
            var tags = new List<string>{"sustainable","eco-friendly"};
            int id = 101;
            var vid = new Video(title, tags, id);
            id.Equals(vid.Id);
        }

        [Fact]
        public void Test_VideoUrl()
        {
            string title = "titol";
            var tags = new List<string>{"sustainable","eco-friendly"};
            int id = 101;
            var vid = new Video(title, tags, id);

            string url = vid.getBaseUrl() + id.ToString();
            url.Equals(vid.Url);
        }

        // faltaria testejar: play(), pause() i stop()


        // User tests
        [Fact]
        public void Test_UserUserName()
        {
            string username = "palori";
            string password = "Zx09*";
            var usr = new User(username, "name", "surname", password);

            username.Equals(usr.UserName);
        }

        [Fact]
        public void Test_UserPassword()
        {
            string username = "palori";
            string password = "Zx09*";
            var usr = new User(username, "name", "surname", password);

            password.Equals(usr.Password);
        }

        [Fact]
        public void Test_UserAddVideoAndTags_getVideo()
        {
            string username = "palori";
            string password = "Zx09*";
            var usr = new User(username, "name", "surname", password);

            string title = "titol";
            var tags = new List<string>{"sustainable","eco-friendly"};
            int id = 101;
            var vid = new Video(title, tags, id);

            usr.addVideo(vid);

            var newTags = new List<string>{"food","beach"};
            usr.addTags(id, newTags);

            var newTags_full = new List<string>{"sustainable","eco-friendly","food","beach"};

            newTags_full.Equals(usr.getVideo(id).Tags);
        }

        [Fact]
        public void Test_UserIsUser()
        {
            string username = "palori";
            string password = "Zx09*";
            var usr = new User(username, "name", "surname", password);

            Assert.True(usr.isUser(usr));
        }

        [Fact]
        public void Test_UserIsNotUser()
        {
            string username = "palori";
            string password = "Zx09*";
            var usr = new User(username, "name", "surname", password);
            var usr2 = new User("pa1", "name", "surname", password);

            Assert.False(usr.isUser(usr2));
        }
    }
}
