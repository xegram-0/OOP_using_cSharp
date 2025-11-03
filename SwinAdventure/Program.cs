namespace SwinAdventure
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            Player testPlayer = new Player("James", "an explorer");

            Item item1 = new Item([ "silver", "hat" ], "A silver hat", "A very shiny silver hat");
            Item item2 = new Item(["light", "torch" ], "a torch", "a torch to light the path");
            
            testPlayer.Inventory.Put(item1);
            testPlayer.Inventory.Put(item2);
            
            Console.WriteLine(testPlayer.AreYou("me"));
            Console.WriteLine(testPlayer.AreYou("inventory"));

            if (testPlayer.Locate("torch") != null)
            {
                Console.WriteLine("The object torch exists");
                Console.WriteLine(testPlayer.Inventory.HasItem("torch"));
            }
            else
            {
                Console.WriteLine("The object torch does not exist");
            }
            StreamWriter writer = new StreamWriter("TestPlayer.txt");
            
            try{testPlayer.SaveTo(writer); }
            
            finally{ writer.Close();}
            /*
            //testPlayer = null;
            StreamReader reader = new StreamReader("a.txt"); //Call the non-existing file would result in error, thus need to put this in using block.
            try{testPlayer.LoadFrom(reader);}
            catch(Exception problem) {Console.Error.WriteLine("Loading file error {0}", problem.Message);}
            finally { reader.Close();}
            */
            try
            {
                using (StreamReader reader = new StreamReader("a.txt"))
                {
                    testPlayer.LoadFrom(reader);
                }
            }
            catch(Exception problem) {Console.Error.WriteLine("Loading file error {0}", problem.Message);}

            List<IHaveInventory> myContainers = new List<IHaveInventory>();
            myContainers.Add(testPlayer);
            Bag testBag = new Bag(["bag", "tool"], "Tool bag", "A bag with tools");
            Item testItem = new Item(["stew", "beef"], "A beef stew", "A hearty beef stew");
            testBag.Inventory().Put(testItem);
            myContainers.Add(testBag);

            for (int i = 0; i < myContainers.Count; i++)
            {
                Console.WriteLine(myContainers[i].FullDescription);
               
            }
            
            Console.WriteLine("Enter your name");
            string userName = Console.ReadLine();
            Console.WriteLine("Describe your character");
            string userDescription = Console.ReadLine();
            Player userPlayer = new Player(userName, userDescription);
            userPlayer.Inventory.Put(item1);
            userPlayer.Inventory.Put(item2);
            Bag userBag = new Bag(["bag", "money"], "A money bag", "A bag full of money");
            userPlayer.Inventory.Put(userBag);
            userBag.Inventory().Put(testItem);
            LookCommand userCommand = new LookCommand([""]);
            while (true)
            {
                Console.WriteLine("Enter a command");
                string command = Console.ReadLine();
                if (command.ToLower() == "exit")
                {
                    break;
                }

                string[] splitCommand = command.Split(" ");
                Console.WriteLine(userCommand.Execute(userPlayer, splitCommand));
            }
            /*
                * for (int i = 0; i < myContainers.Count; i++)
                  {
                      if (myContainers[i] is Player testPlayer)
                          Console.WriteLine(testPlayer.FullDescription);
                      else if (myContainers[i] is Bag testBag)
                          Console.WriteLine(testBag.FullDescription);
                  }

                */
            /*
             2 ref points to 1 obj, if 1 ref take the stuff, the obj loses the stuff too
            Player player1 = new Player("God", "an explorer");
            Player player2 = player1;

            player1.Inventory.Put(item1);
            player2.Inventory.Take("silver");
            Console.WriteLine(player1.FullDescription);
            Console.WriteLine(player2.FullDescription);



            //Verification
            List<IHaveInventory> VerificationContainer = new List<IHaveInventory>();
            Player p1 = new Player("Abe", "Construstor");
            Player p2 = new Player("Ben", "Teacher");
            Player p3 = new Player("Caleb", "Banker");
            Player p4 = new Player("Diana", "Warrior");
            Player p5 = new Player("Evan", "Dog");
            Player p6 = new Player("Fiona", "Game maker");
            Player p7 = new Player("Grace", "Gym dude");

            Bag b1 = new Bag(["bag", "tool"], "Tool bag", "A bag with tools");
            Bag b2 = new Bag(["bag", "medical"], "First Aid Bag", "A bag containing basic medical supplies.");
            Bag b3 = new Bag(["bag", "food"], "Lunch Bag", "A small bag for carrying meals and snacks.");
            Bag b4 = new Bag(["bag", "travel"], "Travel Bag", "A large bag designed for carrying clothes and essentials during trips.");
            Bag b5 = new Bag(["bag", "school"], "School Bag", "A backpack used by students to carry books and stationery.");
            Bag b6 = new Bag(["bag", "sport"], "Gym Bag", "A lightweight bag for carrying workout clothes and gear.");
            Bag b7 = new Bag(["bag", "magic"], "Mystic Bag", "A mysterious bag filled with magical items and potions.");

            //p4 has 1 more id, p2 has 2 more, p3 has 3 more, p1 has 4 more
            p4.AddIdentifier("A worker");
            p2.AddIdentifier("A musician");
            p2.AddIdentifier("A mother");
            p3.AddIdentifier("A father");
            p3.AddIdentifier("A lover");
            p3.AddIdentifier("A monster");
            p1.AddIdentifier("A worker");
            p1.AddIdentifier("A mouse");
            p1.AddIdentifier("A rabbit");
            p1.AddIdentifier("A ricer");

            //9 more for b7
            b7.AddIdentifier("Fire");
            b7.AddIdentifier("Water");
            b7.AddIdentifier("Ice");
            b7.AddIdentifier("Wind");
            b7.AddIdentifier("Rock");
            b7.AddIdentifier("Paper");
            b7.AddIdentifier("Shock");
            b7.AddIdentifier("Poison");
            b7.AddIdentifier("Void");

            VerificationContainer.Add(p1);
            VerificationContainer.Add(p2);
            VerificationContainer.Add(p3);
            VerificationContainer.Add(p4);
            VerificationContainer.Add(p5);
            VerificationContainer.Add(p6);
            VerificationContainer.Add(p7);

            VerificationContainer.Add(b1);
            VerificationContainer.Add(b2);
            VerificationContainer.Add(b3);
            VerificationContainer.Add(b4);
            VerificationContainer.Add(b5);
            VerificationContainer.Add(b6);
            VerificationContainer.Add(b7);

            //IdentCount is already present in IdenfitiableObj
            //a, b are temporary variables for the whole stuff, it runs on introsort (n log n)
            VerificationContainer.Sort((a, b) => a.IdentCount.CompareTo(b.IdentCount));
            foreach (var item in VerificationContainer)
            {
                Console.WriteLine(item.Name);
            }

            //Console.WriteLine(p1.IdentCount);
            */
        }
    }
}