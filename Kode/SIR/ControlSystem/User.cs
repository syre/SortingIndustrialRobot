using System.Collections.Generic;

namespace ControlSystem
{
    /// <summary>
    /// Interface for an user with permissions for the database.
    /// </summary>
    public interface IUser
    {
        /// <summary>
        /// Name of the user.
        /// </summary>
        string userName { get; set; }
        
        /// <summary>
        /// Set of permissions.
        /// </summary>
        Dictionary<string, bool> permissionDictionary { get; }
    }

    /// <summary>
    /// Normal user.
    /// </summary>
    public class User : IUser
    {
        readonly private Dictionary<string, bool> _permissiondictionary;
        public Dictionary<string, bool> permissionDictionary { get { return _permissiondictionary; } }
        public string userName { get; set; }
        
        /// <summary>
        /// Default contructor setting up permissions.
        /// </summary>
        public User()
        {
            _permissiondictionary = new Dictionary<string, bool>();
            permissionDictionary.Add("Permissions", false);
            permissionDictionary.Add("Producers", false);
            permissionDictionary.Add("Components", false);
            permissionDictionary.Add("LogType", false);
            permissionDictionary.Add("Position", true);
            permissionDictionary.Add("BoxInfo", false);
            permissionDictionary.Add("Logs", false);
            permissionDictionary.Add("Users", false);
        }
    }
    
    /// <summary>
    /// Admin user with ability to see and edit all tables.
    /// </summary>
    public class Admin : IUser
    {
        private readonly Dictionary<string, bool> _permissiondictionary;
        public Dictionary<string, bool> permissionDictionary { get { return _permissiondictionary; } }
        public string userName { get; set; }

        /// <summary>
        /// Default constructor setting up permissions.
        /// </summary>
        public Admin()
        {
            _permissiondictionary = new Dictionary<string, bool>();
            permissionDictionary.Add("Permissions",true);
            permissionDictionary.Add("Producers", true);
            permissionDictionary.Add("Components", true);
            permissionDictionary.Add("LogType", true);
            permissionDictionary.Add("Position", true);
            permissionDictionary.Add("BoxInfo", true);
            permissionDictionary.Add("Logs", true);
            permissionDictionary.Add("Users", true);
        }
    }
}
