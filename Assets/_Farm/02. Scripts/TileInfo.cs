using UnityEngine;

namespace _Farm._02._Scripts
{
    public class TileInfo
    {
        private GameObject tile;
        private float tileZSize;

        public TileInfo(GameObject tile, float tileZSize)
        {
            this.tile = tile;
            this.tileZSize = tileZSize;
        }

        public float GetTileZSize()
        {
            return tileZSize;
        }

        public void SetPosition(float offsetZ)
        {
            if (tile is null) return;
            tile.transform.position = new Vector3(0, 0, offsetZ);
        }

        public void Destroy()
        {
            if (tile is null) return;
            Object.Destroy(tile);
            tile = null;
        }
    }
}