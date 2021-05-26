using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using UnityEngine;
using UnityEngine.UI;

namespace Examples
{
    public class MenuController : MonoBehaviour
    {
        [SerializeField] private Button stopButton;
        [SerializeField] private Text label;
        
        private IConfiguration _configuration;
        private CubesController _cubesController;

        private void Start()
        {
            _configuration = GameHost.ServiceProvider.GetRequiredService<IConfiguration>();
            _cubesController = GameHost.ServiceProvider.GetRequiredService<CubesController>();
            
            stopButton.onClick.AddListener(PrintWelcomeMessage);
        }

        private void PrintWelcomeMessage()
        {
            label.text = _configuration["Game:WelcomeMessage"] + $"/ {_cubesController.SpawnedCount.ToString()} cubes spawned";
        }
    }
}