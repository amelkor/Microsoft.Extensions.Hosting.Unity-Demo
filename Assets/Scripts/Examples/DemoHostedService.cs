using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Examples
{
    /// <summary>
    /// Just for the demo sake
    /// </summary>
    public interface IDemoHostedService
    {
        
    }
    
    public class DemoHostedService : IDemoHostedService, IHostedService
    {
        private readonly ILogger<DemoHostedService> _logger;
        private readonly CubesController _cubesController;

        /// <summary>
        /// Inject services as usual into the ctor
        /// </summary>
        public DemoHostedService(ILogger<DemoHostedService> logger, CubesController cubesController)
        {
            _logger = logger;
            _cubesController = cubesController;
        }
        
        public Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("DemoHostedService started");
            
            _cubesController.SpawnCubes();
            
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("DemoHostedService stopped");
            return Task.CompletedTask;
        }
    }
}