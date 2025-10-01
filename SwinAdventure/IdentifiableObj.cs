using System;

namespace SwinAdventure;

public class IdentifiableObj
{
   private List<string> _identifiers;

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
      else
      {
         return false;
      }
   }
   
   public string FirsId
   {
      get
      {
         if (_identifiers.Count > 0)
         {
            return _identifiers[0];
         }
         else
         {
            return null;
         }
      }
   }
   
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
}