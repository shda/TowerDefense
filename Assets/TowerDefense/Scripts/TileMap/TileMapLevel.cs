using System.Linq;
using SuperTiled2Unity;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace TowerDefense.Scripts.TileMap
{
    public class TileMapLevel : MonoBehaviour
    {
        [SerializeField] private SuperMap map;
        [SerializeField] private BoxCollider2D limitation;
        [SerializeField] private Tilemap layerService;
        [SerializeField] private Tilemap layerObstacles;
        [SerializeField] private SuperCustomProperties properties;
        public SuperCustomProperties Properties => properties;


        public Tilemap LayerService => layerService;
        public Tilemap LayerObstacles => layerObstacles;

        private void Awake()
        {
            HideServiceLayer();
        }

        //Editor only
        private void OnValidate()
        {
            if (!map)
            {
                map = GetComponent<SuperMap>();
            }

            if (!layerService)
            {
                layerService = transform.GetComponentsInChildren<Tilemap>()
                    .FirstOrDefault(x => x.name == TilePropertyNames.ServiceMapLayer);
            }

            if (!layerObstacles)
            {
                layerObstacles = transform.GetComponentsInChildren<Tilemap>()
                    .FirstOrDefault(x => x.name == TilePropertyNames.ObstaclesMapLayer);
            }


            if (layerObstacles)
            {
                var spriteMask = layerObstacles.GetComponent<SpriteMask>();
                if (!spriteMask)
                {
                    spriteMask = layerObstacles.gameObject.AddComponent<SpriteMask>();
                }

                spriteMask.maskSource = SpriteMask.MaskSource.SupportedRenderers;
            }

            if (!properties)
            {
                properties = GetComponent<SuperCustomProperties>();
            }
        }

        public bool PointInLimitions(Vector3 position)
        {
            return limitation.bounds.Contains(position);
        }

        private void HideServiceLayer()
        {
            var render = LayerService.GetComponent<TilemapRenderer>();
            render.enabled = false;
        }
    }
}