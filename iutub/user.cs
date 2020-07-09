using System;
using System.Collections.Generic;

namespace iutub
{
    public class User
    {
        const int ID_NO_VIDEO_FOUND = -1; // veure on s'ha de posar...


        public string UserName {get; set;}
        public string Name {get; set;}
        public string Surname {get; set;}
        public string Password {get; set;}
        public DateTime RegisterDate {get;}
        public List<Video> Videos {get;}

        public User(string UserName, string Name, string Surname, string Password)
        {
            this.UserName = UserName;
            this.Name = Name;
            this.Surname = Surname;
            this.Password = Password;
            this.RegisterDate = DateTime.Now;
        }

        public void addVideo(Video newVideo)
        {
            Videos.Add(newVideo);
        }

        private Video findVideo(int videoId)
        {
            foreach (var video in Videos)
            {
                if (video.Id == videoId)
                {
                    // video found
                    return video;
                }
            }

            // no video with this ID has been found
            return new Video(ID_NO_VIDEO_FOUND);
        }

        public Video getVideo(int videoId)
        {
            return findVideo(videoId);
        }

        public bool deleteVideo(int videoId)
        {
            Video vid = findVideo(videoId);
            if (vid.Id != ID_NO_VIDEO_FOUND)
            {
                return Videos.Remove(vid); // true = removed, false = couldn't
            }
            return false;
        }

        public bool addTags(int videoId, List<string> newTags)
        {
            foreach (var video in Videos)
            {
                if (video.Id == videoId)
                {
                    // video found
                    video.addTags(newTags);
                    return true;
                }
            }
            // true = removed, false = couldn't
            return false;
        }

        public bool isUser(User possibleUser)
        {
            // check if it is the same user
            if (UserName == possibleUser.UserName &&
                Password == possibleUser.Password) return true;
            return false;
        }
    }
}