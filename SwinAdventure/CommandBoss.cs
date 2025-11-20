namespace SwinAdventure;
public class CommandBoss()
{
    private List<Command> _commands = new();

    public void AddCommand(Command command)
    {
        _commands.Add(command);
    }

    public string Execute(Player player, string [] text)
    { 
        string commandWord = text[0].ToLower();

        foreach (var cmd in _commands)
        {
            if (cmd.AreYou(commandWord))
            {
                return cmd.Execute(player, text);
            }
        }

        return $"Unknown command: '{commandWord}'.";
    }
    
}