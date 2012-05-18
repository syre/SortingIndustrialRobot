using System.Collections.Generic;

namespace ControlSystem
{
    public interface IUser
    {
        string userName { get; set; }
        Dictionary<string, bool> permissionDictionary { get; }
    }

    public class User : IUser
    {
        readonly private Dictionary<string, bool> _permissiondictionary;
        public Dictionary<string, bool> permissionDictionary { get { return _permissiondictionary; } }
        public string userName { get; set; }
        
        public User()
        {
            _permissiondictionary = new Dictionary<string, bool>();
            permissionDictionary.Add("Permissions", false);
            permissionDictionary.Add("Producers", false);
            permissionDictionary.Add("Components", false);
            permissionDictionary.Add("Software", false);
            permissionDictionary.Add("Position", true);
            permissionDictionary.Add("BoxInfo", false);
            permissionDictionary.Add("SystemComponentTable", false);
            permissionDictionary.Add("Users", false);
        }
    }
    
    public class Admin : IUser
    {
        private readonly Dictionary<string, bool> _permissiondictionary;
        public Dictionary<string, bool> permissionDictionary { get { return _permissiondictionary; } }
        public string userName { get; set; }

        public Admin()
        {
            _permissiondictionary = new Dictionary<string, bool>();
            permissionDictionary.Add("Permissions",true);
            permissionDictionary.Add("Producers", true);
            permissionDictionary.Add("Components", true);
            permissionDictionary.Add("Software", true);
            permissionDictionary.Add("Position", true);
            permissionDictionary.Add("BoxInfo", true);
            permissionDictionary.Add("SystemComponentTable", true);
            permissionDictionary.Add("Users", true);
        }
    }
}
