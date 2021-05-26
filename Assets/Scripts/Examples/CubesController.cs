using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using UnityEngine;

namespace Examples
{
    public class CubesController : MonoBehaviour
    {
        private const int DEFAULT_CAPACITY = 15;
        
        private ILogger<CubesController> _logger;
        private IConfiguration _configuration;

        private readonly List<GameObject> _spawnedCubes = new List<GameObject>(DEFAULT_CAPACITY);
        public int SpawnedCount => _spawnedCubes.Count;

        /// <summary>
        /// The default injection method for MonoBehaviour
        /// </summary>
        private void AwakeServices(ILogger<CubesController> logger, IConfiguration configuration)
        {
            _configuration = configuration;
            _logger = logger;
        }

        private void Start()
        {
            _logger.LogDebug("CubesController started");
        }
        
        private void OnDisable()
        {
            _logger.LogDebug("CubesController disabled");
        }

        public void SpawnCubes()
        {
            var cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
            
            for (var i = 0; i < _configuration.GetValue<int>("Game:CubesToSpawn"); i++)
            {
                var c = Instantiate(cube, Vector3.up * (i + 1), Quaternion.identity);
                _spawnedCubes.Add(c);
                _logger.LogTrace("cube instantiated at {Position}", c.transform.position.ToString());
            }
        }
    }
}