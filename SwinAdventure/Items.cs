using System;
namespace SwinAdventure;

public class Items
{
   private List<string> _identifiers;
   private string _description;
   private string _name;
   
   public Items(){} // for parameterless constructor error
   public Items(string[] idents, string name, string description)
   {
      _identifiers = new List<string>();
      
      foreach (string ident in idents)
      {
         AddIdentifier(ident);
      }
      
      _name = name;
      _description = description;
   }

   public string Name
   {
      get { return _name; }
   }

   public string ShortDescription
   {
      /*
       * if-else statement
       * is the length is greater than 5, take a substring from 0 to 4
       * else, take the entire string since it is short anyway
       */
      get { return _description.Length > 5 ?  _description.Substring(0, 4) : _description; }
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
         if (_identifiers.Count > 0)
         {
            return _identifiers[0];
         }
         return "";
      }
   }

   public void AddIdentifier(string id)
   {
      if (!_identifiers.Contains(id.ToLower()))
      {
         _identifiers.Add(id.ToLower());
      }
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

   public int PrintNumIdentifiers()
   {
      return _identifiers.Count;
   }
}