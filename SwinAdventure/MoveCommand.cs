namespace SwinAdventure;
public class MoveCommand(string [] ids) :Command(ids)
{
    // Example ["move", "to", "north"]
    public override string Execute(Player player, string[] text)
    {
        if (text[0] != "move" && text[0] != "go") //1st word
        {
            return "Unknown move input";
        }
        if (text.Length != 2 && text.Length != 3) //
        {
            return "Unknown movement";
        }
        if (text.Length == 3 && text[1] != "to")
        {
            return "To where you want?";
        }
        //Return destination based on string length
        string destinationID = "";
        if (text.Length == 2)
        {
            destinationID = text[1];
        }
        if (text.Length == 3)
        {
            destinationID = text[2];
        }

        Path? path = player.Locate(destinationID) as Path; //Path can be null
        if (path == null)
        {
            return $"{destinationID} unreachable";
        }

        if (path.IsBlocked)
        {
            return $"{destinationID} blocked";
        }
    
        player.Location = path.Destination;
        return $"{destinationID} reached";
    }
}