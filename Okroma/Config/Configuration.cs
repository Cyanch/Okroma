using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.IO;
using XnaGame = Microsoft.Xna.Framework.Game;

namespace Okroma.Config
{
    interface IConfigurationService
    {
        string GetPath(string key);
    }
    
    enum ConfigurationPaths
    {
        DefaultFont,
    }

    class Configuration : GameComponent, IConfigurationService
    {
        private Dictionary<string, string> _paths = new Dictionary<string, string>();

        public Configuration(XnaGame game) : base(game)
        {
            _paths.Add(ConfigurationPaths.DefaultFont.ToString(), Path.Combine("fonts", "Okroma48"));
        }

        public string GetPath(string key)
        {
            return _paths[key];
        }
    }
}
