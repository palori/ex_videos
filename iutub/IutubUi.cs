using System;
using System.Collections.Generic;
using System.Threading;

namespace iutub
{
    public class IutubUi // Iutub User Interface
    {
        private int WAIT_TIME = 1600; // [ms]
        private int newVideoId = 0;
        public User Usr {get;}
        // Improvement: make it a list of users, and store their data (include log off, delete user...)
        //List<User> Users {get;};

        public IutubUi()
        {
            Usr = new User();
            //Usr = new User("default","");
        }

        public void Run()
        {
            if (true)
            {
                Console.Clear();
                Header();
                Console.WriteLine("\tStarting Iutub...");
                Footer();
                Thread.Sleep(WAIT_TIME); 
                Welcome();
            }
            //Stop();            
        }

        private void Stop()
        {
            Header();
            Console.WriteLine("\tStopping Iutub...");
            Thread.Sleep(WAIT_TIME); 
            Console.WriteLine("\tThank you for watching videos with us");
            Thread.Sleep(WAIT_TIME); 
            Console.WriteLine("\tSee you soon :)");
            Footer();
            Thread.Sleep(WAIT_TIME); 
        }

        public void Welcome()
        {
            Header();
            Console.WriteLine("\tWelcome\n\n");
            //var options = new Dictionary<string,int>(){{"Log in",1}, {"Sing in",2}}; // to study...
            string[] options = {"Sing in","Log in"};
            //string[] options = {"Sing in"};
            Console.WriteLine(createMenuOptions(options, true)); // if dict, pass keys or values...
            Footer();
            int select = SelectOption(options.Length+1, true);
            if (select == 1)
            {
                SignIn();
            }
            else if (select == 2)
            {
                LogIn();
            }
            else
            {
                Stop();
            }
        }

        public void LogIn()
        {
            Header();
            Console.WriteLine("\tLog in\n");
            string[] formOptions = {"Username", "Password"};
            var form = SimpleFillForm(formOptions);

            Console.WriteLine("\n\n\tAction:");
            string[] options = {"Send"};
            Console.WriteLine(createMenuOptions(options, true));
            Footer();
            int select = SelectOption(options.Length+1, true);

            if (select == 1)
            {
                var possibleUsr = new User(form["Username"], form["Password"]);
                if (Usr.isUser(possibleUsr))
                {
                    Menu();
                }
                else
                {
                    Welcome();
                }
            }
            else
            {
                Welcome();
            }
        }

        public void SignIn()
        {
            Header();
            Console.WriteLine("\tLog in\n");
            string[] formOptions = {"Username", "Name", "Surname", "Password"};
            var form = SimpleFillForm(formOptions);

            Console.WriteLine("\n\n\tAction:");
            string[] options = {"Send"};
            Console.WriteLine(createMenuOptions(options, true));
            Footer();
            int select = SelectOption(options.Length+1, true);

            if (select == 1)
            {
                // update user info
                Usr.UserName = form["Username"];
                Usr.Name = form["Name"];
                Usr.Surname = form["Surname"];
                Usr.Password = form["Password"];
                Usr.updateRegisterDate();

                Menu();
            }
            else
            {
                Welcome();
            }
        }

        public void Menu()
        {
            Header();
            Console.WriteLine($"\tHi {Usr.UserName}!");
            Console.WriteLine("\n\tWhat do you want to do?");
            Thread.Sleep(WAIT_TIME); 

            string[] options = {"Add a new video","See list of videos","Watch a viedo","Add tags to video","Delete video","Log off"};
            Console.WriteLine(createMenuOptions(options, false));
            Footer();
            int select = SelectOption(options.Length+1, false);
            if (select == 1)
            {
                AddVideo();
            }
            else if (select == 2)
            {
                SeeVideoList();
            }
            else if (select == 3)
            {
                WatchVideo();
            }
            else if (select == 4)
            {
                AddTags();
            }
            else if (select == 5)
            {
                DeleteVideo();
            }
            else if (select == 6)
            {
                Stop();
            }
            else
            {
                Stop(); // just in case...
            }
        }

        private List<string> SplitTags(string strTags)
        {
            // editing tags
            var splitting = strTags.Split(",", StringSplitOptions.RemoveEmptyEntries);
            return new List<string>(splitting);
        }

        public void AddVideo()
        {
            Header();
            Console.WriteLine($"\tAdding a new video...\n");
            string[] formOptions = {"Title", "Tags (separated by comas)"};
            var form = SimpleFillForm(formOptions);

            var tags = SplitTags(form["Tags (separated by comas)"]);

            //new video
            newVideoId++; // auto assigning video IDs
            var newVid = new Video(form["Title"], tags, newVideoId);
            Usr.addVideo(newVid);

            Console.WriteLine($"\n\tNew video has been added.\n\tTitle: {form["Title"]}");

            string[] options = {"Back"};
            Console.WriteLine("\n\n\tAction:");
            Console.WriteLine(createMenuOptions(options, false));
            Footer();
            int select = SelectOption(options.Length+1, false);
            // only back option, so return to menu
            Menu();

        }

        public void AddTags()
        {
            if (Usr.Videos.Count == 0)
            {
                Header();
                Console.WriteLine($"\tThere are no videos, please add some.\n");
            }
            else
            {
                // Select video
                var vid = SelectVideo("\tSelect video");
                
                Header();
                Console.WriteLine($"\tAdding tags to {vid.Title}...\n");
                string[] formOptions = {"Tags (separated by comas)"};
                var form = SimpleFillForm(formOptions);

                var tags = SplitTags(form["Tags (separated by comas)"]);
                Usr.addTags(vid.Id, tags);

                Console.WriteLine($"\n\tNew tags have been added to video:\n\tTitle: {vid.Title}");
            }
            string[] options = {"Back"};
            Console.WriteLine("\n\n\tAction:");
            Console.WriteLine(createMenuOptions(options, false));
            Footer();
            int select = SelectOption(options.Length+1, false);
            // only back option, so return to menu
            Menu();
        }

        public void DeleteVideo()
        {
            if (Usr.Videos.Count == 0)
            {
                Header();
                Console.WriteLine($"\tThere are no videos, please add some.\n");
            }
            else
            {
                // Select video
                var vid = SelectVideo("\tSelect video to be deleted");
                
                Header();
                Console.WriteLine($"\tDeleting video: {vid.Title}...\n");
                Usr.deleteVideo(vid.Id);
                Thread.Sleep(WAIT_TIME); 
                Console.WriteLine($"\n\tThe video has been deleted\n");
            }

            string[] options = {"Back"};
            Console.WriteLine("\n\n\tAction:");
            Console.WriteLine(createMenuOptions(options, false));
            Footer();
            int select = SelectOption(options.Length+1, false);
            // only back option, so return to menu
            Menu();
        }

        private string[] PrintVideoList()
        {
            string[] options = new string[Usr.Videos.Count];
            for (int i = 0; i < options.Length; i++)
            {
                options[i] = Usr.Videos[i].Title;
            }
            Console.WriteLine(createMenuOptions(options, false));
            return options;
        }

        public void SeeVideoList()
        {
            Header();
            if (Usr.Videos.Count == 0)
            {
                Console.WriteLine($"\tThere are no videos, please add some.\n");
            }
            else
            {
                Console.WriteLine("\tVideo list");
                PrintVideoList();
            }   
            string[] options = {"Back"};
            Console.WriteLine("\n\n\tAction:");
            Console.WriteLine(createMenuOptions(options, false));
            Footer();
            int select = SelectOption(options.Length+1, false);
            // only back option, so return to menu
            Menu();
        }

        private Video SelectVideo(string action)
        {
            Header();
            Console.WriteLine(action);

            string[] options = PrintVideoList();
            Footer();
            int select = SelectOption(options.Length+1, false);
            var vid = Usr.Videos[select-1];
            return vid;
        }


        public void WatchVideo()
        {
            if (Usr.Videos.Count == 0)
            {
                Header();
                Console.WriteLine($"\tThere are no videos, please add some.\n");

                string[] options = {"Back"};
                Console.WriteLine("\n\n\tAction:");
                Console.WriteLine(createMenuOptions(options, false));
                Footer();
                int select = SelectOption(options.Length+1, false);
            }
            else
            {
                var vid = SelectVideo("\tWhich video do you want to watch?");
                bool isNotStopped = true;
                int select = 1;
                string[] options = {"Play", "Pause", "Stop"};
                do
                {
                    Header();
                    Console.WriteLine(vid.ToString());
                    
                    if (select == 1)
                    {
                        Console.WriteLine(vid.play());
                    }
                    else if (select == 2)
                    {
                        Console.WriteLine(vid.pause());
                    }
                    else if (select == 3)
                    {
                        Console.WriteLine(vid.stop());
                        isNotStopped = false;
                        Thread.Sleep(WAIT_TIME);
                    }
                    
                    if (select != 3)
                    {
                        Console.WriteLine("\n\n\tAction:");
                        Console.WriteLine(createMenuOptions(options, false));
                        Footer();
                        select = SelectOption(options.Length+1, false);
                    }
                } while(isNotStopped);
            }
            Menu();
        }


        // strings to print to make it nicer
        private string MakeLine(string symbol, int iterations)
        {
            string s = "";
            for (int i = 0; i < iterations; i++)
            {
                s += symbol;
            }
            return s;
        }
        private void PrintLine(string symbol, int iterations)
        {
            Console.Write($"\n  {MakeLine(symbol, iterations)}\n\n");
        }
        private void Header()
        {
            Console.Clear();
            PrintLine("~",50);
        }
        private void Footer()
        {
            PrintLine("~",50);
        }
        private string createMenuOptions(string[] options, bool isCancel)
        {
            string s = "";
            for (int i = 0; i < options.Length; i++)
            {
                s += "\t";
                if (i+1 < 10) { s += " "; }
                s += $"{i+1}. {options[i]}\n"; 
            }
            if (isCancel)
            {
                s += "\t 0. Cancel\n";
            }
            return s;
        }


        // User inputs
        private int SelectOption(int numberOfOptions, bool isCancel) // Considering the option 0, so options.Length+1
        {
            while (true)
            {
                Console.Write("\tSelect an option: ");
                string strInput = Console.ReadLine();
                try
                {
                    var intInput = int.Parse(strInput);
                    if (intInput > 0 && intInput <= numberOfOptions-1 || (isCancel && intInput == 0)){
                        Console.WriteLine($"\tOption {intInput} selected...");
                        Thread.Sleep(WAIT_TIME);
                        return intInput;
                    }
                    Console.WriteLine("\tPlease, the number must be in present in the list.\n");
                    Thread.Sleep(WAIT_TIME);
                }
                catch (System.Exception e1)
                {
                    Console.WriteLine("\tIt must be an integer (0, 1, 2...) from the list above...\n");
                    Thread.Sleep(WAIT_TIME);
                }
            }
            
        }

        private Dictionary<string,string> SimpleFillForm(string[] fields)
        {
            // So simple, should analyse all the fields, but it doesn't do that
            var form = new Dictionary<string,string>();
            foreach (var field in fields)
            {
                bool isEmpty = true;
                do
                {
                    Console.Write($"\t{field}: ");
                    form[field] = Console.ReadLine();
                    if (form[field] == "")
                    {
                        Console.WriteLine("\t--- Please the field must be filled in.");
                    }
                    else
                    {
                        isEmpty = false;
                    }
                }while(isEmpty);
            }
            return form;
        }
    }
}