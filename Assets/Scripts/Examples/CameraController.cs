using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using UnityEngine;

namespace Examples
{
    [RequireComponent(typeof(Camera))]
    public class CameraController : MonoBehaviour
    {
        private Camera _camera;
        private IConfiguration _configuration;
        private ILogger<CameraController> _logger;

        private void Awake()
        {
            _camera = GetComponent<Camera>();
        }

        /// <summary>
        /// The default injection method for MonoBehaviour
        /// </summary>
        private void AwakeServices(IConfiguration configuration, ILogger<CameraController> logger)
        {
            _logger = logger;
            _configuration = configuration;
        }

        private void Start()
        {
            var fov = _configuration.GetValue<int>("Game:FoV");
            _camera.fieldOfView = fov;
            
            _logger.LogInformation("Camera Field of View set to {FoV}", fov.ToString());
        }
    }
}