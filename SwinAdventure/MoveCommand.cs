namespace SwinAdventure;

public class MoveCommand :Command
{
    public MoveCommand():base(["move", "go"]){}
    public override string Execute(Player p, string[] text)
    {
        if (text.Length != 2 && text.Length != 3)
        {
            return "Unknown movement";
        }

        if (text[0] != "move" && text[0] != "go")
        {
            return "Unknown move input";
        }

        if (text.Length == 3 && text[1] != "to")
        {
            return "Only to where";
        }

        string destinationID = "";
        if (text.Length == 2)
        {
            destinationID = text[1];
        }
        if (text.Length == 3)
        {
            destinationID = text[2];
        }

        Path? path = p!.Locate(destinationID) as Path;
        if (path == null)
        {
            return $"{destinationID} unreachable";
        }

        if (path.IsBlocked)
        {
            return $"{destinationID} blocked";
        }

        p.Location = path.Destination;
        return $"{destinationID} reached";
    }
}