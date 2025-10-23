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
            
            /*
             2 ref points to 1 obj, if 1 ref take the stuff, the obj loses the stuff too
            Player player1 = new Player("God", "an explorer");
            Player player2 = player1;
            
            player1.Inventory.Put(item1);
            player2.Inventory.Take("silver");
            Console.WriteLine(player1.FullDescription);
            Console.WriteLine(player2.FullDescription);
            */
        }
    }
}