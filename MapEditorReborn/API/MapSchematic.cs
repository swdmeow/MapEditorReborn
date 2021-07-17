﻿namespace MapEditorReborn.API
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// The MapSchematic class which is used to save and load maps.
    /// </summary>
    [Serializable]
    public class MapSchematic
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MapSchematic"/> class.
        /// </summary>
        public MapSchematic()
        {
        }

        /// <summary>
        /// Gets or sets the <see cref="MapSchematic"/> name.
        /// </summary>
        public string Name { get; set; } = "None";

        /// <summary>
        /// Gets or sets the list of <see cref="DoorObject"/>.
        /// </summary>
        public List<DoorObject> Doors { get; set; } = new List<DoorObject>();

        /// <summary>
        /// Gets or sets the list of <see cref="WorkStationObject"/>.
        /// </summary>
        public List<WorkStationObject> WorkStations { get; set; } = new List<WorkStationObject>();

        /// <summary>
        /// Gets or sets the list of <see cref="ItemSpawnPointObject"/>.
        /// </summary>
        public List<ItemSpawnPointObject> ItemSpawnPoints { get; set; } = new List<ItemSpawnPointObject>();

        /// <summary>
        /// Gets or sets the list of <see cref="PlayerSpawnPointObject"/>.
        /// </summary>
        public List<PlayerSpawnPointObject> PlayerSpawnPoints { get; set; } = new List<PlayerSpawnPointObject>();
    }
}