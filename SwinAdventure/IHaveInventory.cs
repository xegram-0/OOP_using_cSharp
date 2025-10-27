namespace SwinAdventure;

public interface IHaveInventory
{
    public GameObj Locate(string id);
    string Name { get; }
    string FullDescription { get; }
}