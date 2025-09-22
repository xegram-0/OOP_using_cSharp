using System;
namespace SwinAdventure;

public class Items
{
   private List<string> _identifiers;
   private string _description;
   private string _name;

   public Items(string[] idents, string name, string description)
   {
      _identifiers = new List<string>();
      foreach (string ident in idents)
      {
         AddIdentifier(ident);
      }
      _name = name;
      //AddIdentifier(name);
      
      _description = description;
      //AddIdentifier(description);
   }

   public string Name
   {
      get { return _name; }
   }

   public string ShortDescription
   {
      get { return ""; }
      //Console.Writeline("{0} {1}", _name, _identifiers[0])
   }
   public string LongDescription
   {
      get{ return _description;}
   }

   public bool AreYou(string id)
   {
      return _identifiers.Contains(id);
   }

   public string FirstId
   {
      get
      {
         if (_identifiers.Count == 0)
         {
            return _identifiers[0];
         }

         return "";
      }
   }

   public void AddIdentifier(string id)
   {
      _identifiers.Add(id.ToLower());
   }

   public void RemoveIdentifier(string id)
   {
      if (_identifiers.Contains(id.ToLower()))
      {
         _identifiers.Remove(id.ToLower());
      }
   }

   public void PrivilegeEscalation(string pin)
   {
      if (pin == "2476")
      {
         _identifiers[0] = "0007";
      }
   }
}