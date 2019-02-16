using Okroma.TileEngine;
using System;
using System.Collections.Generic;
using System.IO;

namespace Okroma
{
    public class SaveGame
    {
        public string Name { get; }
        public const string Folder = "saves";

        Dictionary<int, MapState> _mapStates = new Dictionary<int, MapState>();

        public void SaveMap(Map map)
        {
            _mapStates[map.Id] = map.CurrentState;
        }

        public MapState GetMapState(int mapId)
        {
            return _mapStates[mapId];
        }

        protected SaveGame(string name)
        {
            this.Name = name;
        }

        private void Load()
        {
            using (var fileStream = new FileStream(
                Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), Folder), FileMode.Open))
            {
                using (var reader = new BinaryReader(fileStream))
                {
                    // read map states
                    var mapStateCount = reader.ReadInt32();
                    for (int i = 0; i < mapStateCount; i++)
                    {
                        var mapId = reader.ReadInt32();
                        var mapState = MapState.ReadFromBinary(reader);

                        _mapStates.Add(mapId, mapState);
                    }
                }
            }
        }

        public static SaveGame Load(string path)
        {
            var saveGame = new SaveGame(path);

            saveGame.Load();

            return saveGame;
        }

        public void Save()
        {
            using (var fileStream = new FileStream(
                Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), Folder), FileMode.Open))
            {
                using (var writer = new BinaryWriter(fileStream))
                {
                    // write map states
                    writer.Write(_mapStates.Count); // map state count.
                    
                    foreach (var mapstate in _mapStates)
                    {
                        writer.Write(mapstate.Key);
                        mapstate.Value.WriteToBinary(writer);
                    }
                }
            }
        }
    }
}
