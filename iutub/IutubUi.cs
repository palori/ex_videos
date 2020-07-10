using System;
using System.Collections.Generic;
using System.Threading;

namespace iutub
{
    public class IutubUi // Iutub User Interface
    {
        User Usr {get;}
        // Improvement: make it a list of users, and store their data (include log off, delete user...)
        //List<User> Users {get;};

        public IutubUi()
        {
            Usr = new User("default","");
        }

        public void Run()
        {
            if (true)
            {
                Console.Clear();
                Header();
                Console.WriteLine("\tStarting Iutub...");
                Footer();
                Thread.Sleep(2000); // [ms]
                Welcome();
            }
            //Stop();            
        }

        private void Stop()
        {
            Header();
            Console.WriteLine("\tStopping Iutub...");
            Thread.Sleep(2000); // [ms]
            Console.WriteLine("\tThank you for watching videos with us");
            Thread.Sleep(2000); // [ms]
            Console.WriteLine("\tSee you soon :)");
            Footer();
            Thread.Sleep(2000); // [ms]
        }

        public void Welcome()
        {
            Header();
            Console.WriteLine("\tWelcome\n\n");
            //var options = new Dictionary<string,int>(){{"Log in",1}, {"Sing in",2}}; // to study...
            string[] options = {"Sing in","Log in"};
            //string[] options = {"Sing in"};
            Console.WriteLine(createMenuOptions(options)); // if dict, pass keys or values...
            Footer();
            int select = SelectOption(options.Length+1);
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

            Console.WriteLine("\n\n\tAction\n");
            string[] options = {"Send"};
            Console.WriteLine(createMenuOptions(options));
            Footer();
            int select = SelectOption(options.Length+1);

            if (select == 1)
            {
                var possibleUsr = new User(form["Username"], form["Username"]);
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
            Console.WriteLine("Sing in");
            //Usr = new User();

        }

        public void Menu()
        {
            Console.WriteLine("Menu");
        }

        public Video SelectVideo(int userInput)
        {
            Console.WriteLine("SelectVideo");
            var vid = new Video("",new List<string>(),0);
            return vid;
        }

        private bool IsInputEmpty()
        {
            Console.WriteLine("IsEmpty?");
            return false;
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
        private string createMenuOptions(string[] options)
        {
            string s = "";
            for (int i = 0; i < options.Length; i++)
            {
                s += "\t";
                if (i+1 < 10) { s += " "; }
                s += $"{i+1}. {options[i]}\n"; 
            }
            s += "\n\t 0. Cancel\n";
            return s;
        }


        // User inputs
        private int SelectOption(int numberOfOptions) // Considering the option 0, so options.Length+1
        {
            while (true)
            {
                Console.Write("\tSelect an option: ");
                string strInput = Console.ReadLine();
                try
                {
                    var intInput = int.Parse(strInput);
                    if (intInput >= 0 && intInput <= numberOfOptions-1){
                        Console.WriteLine($"\tOption {intInput} selected...");
                        Thread.Sleep(2000); // [ms]
                        return intInput;
                    }
                    Console.WriteLine("\tPlease, the number must be in present in the list.\n");
                    Thread.Sleep(2000); // [ms]
                }
                catch (System.Exception e1)
                {
                    Console.WriteLine("\tIt must be an integer (0, 1, 2...) from the list above...\n");
                    Thread.Sleep(2000); // [ms]
                }
            }
            
        }

        private Dictionary<string,string> SimpleFillForm(string[] fields)
        {
            // So simple, should analyse all the fields, but it doesn't do that
            var form = new Dictionary<string,string>();
            foreach (var field in fields)
            {
                Console.Write($"\t{field}: ");
                form[field] = Console.ReadLine();
            }
            return form;
        }
    }
}