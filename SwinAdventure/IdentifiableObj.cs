namespace SwinAdventure;
public class IdentifiableObj 
{
   private readonly List<string> _identifiers;
   public IdentifiableObj(string[] indents)
   {
      _identifiers = new List<string>();
      foreach (string ident in indents)
      {
         AddIdentifier(ident);
      }
   }
   public bool AreYou(string id)
   {
      if (_identifiers.Contains(id.ToLower()))
      {
         return true;
      }
      return false;
   }
   public string FirstId => _identifiers.Count > 0 ? _identifiers[0] : string.Empty;
   public void AddIdentifier(string id)
   {
     _identifiers.Add(id.ToLower());
   }
   public void PrivilegeEscalation(string pin)
   {
      if (pin == "2476")
      {
         _identifiers[0] = "0007";
      }
   }
   public void RemoveIdentifier(string id)
   {
      _identifiers.Remove(id.ToLower());
   }
   public int IdentCount => _identifiers.Count;
}
