// C# OOP source: https://docs.microsoft.com/es-es/dotnet/csharp/programming-guide/concepts/object-oriented-programming
// Videos:  https://dotnet.microsoft.com/learn/videos
//          https://www.youtube.com/watch?v=TzgxcAiHCWA&list=PLdo4fOcmZ0oVxKLQCHpiUWun7vlJJvUiN&index=17&t=0s
using System;
using System.Collections.Generic;

namespace iutub
{
    class Video
    {
        private const string BASE_URL = "iutub.com/watch?v=";

        /*private int _id;
        public int Id
        {
            // option 1:
            get => _id;
            set =>  _id = value;

            // option 2:
            get { return _seconds; }
            set { _seconds = value; }
        }*/
        public int Id { get; set; } // option 3

        public string Url { get; set; } // set may never be used
        public string Title { get; set; }
        //public string[] Tags { get; set; }
        public List<string> Tags { get; set; }

        //public Video(string Title, string[] Tags, int Id)
        public Video(string Title, List<string> Tags, int id)
        {
            Console.WriteLine("new video");
            this.Title = Title;
            this.Tags = Tags;
            //this.addTags(Tags);
            this.Url = BASE_URL + Id.ToString();
            this.Id = Id;
        }

        public Video(int id) // 2nd constructor
        {
            this.Id = Id;
        }

        //public void addTags(string new_tags)
        public void addTags(List<string> new_tags)
        {
            //Tags = new_tags;
            Tags.AddRange(new_tags);
        }

        public string play()
        {
            //return $"Playing... {Title}";
            return $"Playing...";
        }

        public string pause()
        {
            //return $"Video {Title} paused...";
            return $"Video paused...";
        }

        public string stop()
        {
            //return $"Video {Title} stopped...";
            return $"Video stopped...";
        }
    }
}