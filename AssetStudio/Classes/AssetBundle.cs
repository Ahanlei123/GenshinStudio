﻿using Newtonsoft.Json;
using System.Collections.Generic;

namespace AssetStudio
{
    public class AssetInfo
    {
        public int preloadIndex;
        public int preloadSize;
        public PPtr<Object> asset;

        public AssetInfo(ObjectReader reader)
        {
            preloadIndex = reader.ReadInt32();
            preloadSize = reader.ReadInt32();
            asset = new PPtr<Object>(reader);
        }
    }

    [JsonObject(MemberSerialization.OptIn)]
    public sealed class AssetBundle : NamedObject
    {
        public static bool Exportable;

        [JsonProperty]
        public PPtr<Object>[] PreloadTable;
        [JsonProperty]
        public KeyValuePair<string, AssetInfo>[] Container;
        [JsonProperty]
        public AssetInfo MainAsset;
        [JsonProperty]
        public uint RuntimeComaptability;
        [JsonProperty]
        public string AssetBundleName;
        [JsonProperty]
        public int DependencyCount;
        [JsonProperty]
        public string[] Dependencies;
        [JsonProperty]
        public bool IsStreamedScenessetBundle;
        [JsonProperty]
        public int ExplicitDataLayout;
        [JsonProperty]
        public int PathFlags;
        [JsonProperty]
        public int SceneHashCount;
        [JsonProperty]
        public KeyValuePair<string, string>[] SceneHashes;

        public AssetBundle(ObjectReader reader) : base(reader)
        {
            var m_PreloadTableSize = reader.ReadInt32();
            PreloadTable = new PPtr<Object>[m_PreloadTableSize];
            for (int i = 0; i < m_PreloadTableSize; i++)
            {
                PreloadTable[i] = new PPtr<Object>(reader);
            }

            var m_ContainerSize = reader.ReadInt32();
            Container = new KeyValuePair<string, AssetInfo>[m_ContainerSize];
            for (int i = 0; i < m_ContainerSize; i++)
            {
                Container[i] = new KeyValuePair<string, AssetInfo>(reader.ReadAlignedString(), new AssetInfo(reader));
            }

            MainAsset = new AssetInfo(reader);
            RuntimeComaptability = reader.ReadUInt32();
            AssetBundleName = reader.ReadAlignedString();
            DependencyCount = reader.ReadInt32();
            Dependencies = new string[DependencyCount];
            for (int k = 0; k < DependencyCount; k++)
            {
                Dependencies[k] = reader.ReadAlignedString();
            }
            reader.AlignStream();
            IsStreamedScenessetBundle = reader.ReadBoolean();
            reader.AlignStream();
            ExplicitDataLayout = reader.ReadInt32();
            PathFlags = reader.ReadInt32();
            SceneHashCount = reader.ReadInt32();
            SceneHashes = new KeyValuePair<string, string>[SceneHashCount];
            for (int l = 0; l < SceneHashCount; l++)
            {
                SceneHashes[l] = new KeyValuePair<string, string>(reader.ReadAlignedString(), reader.ReadAlignedString());
            }
        }
    }
}
