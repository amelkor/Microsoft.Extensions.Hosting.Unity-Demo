using System;
using Examples;
using Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting.Unity;
using Microsoft.Extensions.Logging;
using UnityEngine;

[DefaultExecutionOrder(-65500)]
public class GameHost : HostManager
{
    [Tooltip("Register existing game objects on the scene. The component what represents the type must be at the top of the inspector hierarchy")]
    [SerializeField] private MonoBehaviour[] persistentMonoBehavioursOnScene;
    
    /// <summary>
    /// Allows to get access to the Services via static reference
    /// </summary>
    public static IServiceProvider ServiceProvider => _this.Services;
    private static GameHost _this;

    /// <summary>
    /// MonoBehaviour Awake()
    /// </summary>
    protected override void OnAwake()
    {
        _this = this;
        DontDestroyOnLoad(this);
    }

    /// <summary>
    /// MonoBehaviour Start()
    /// </summary>
    protected override void OnStart()
    {

    }

    protected override void ConfigureAppConfiguration(IConfigurationBuilder builder)
    {
        // optionally add configuration
        // in this example it's an addressable json file
        builder.AddUtf8JsonAddressable("Assets/Settings/appsettings.json");
    }

    protected override void ConfigureLogging(ILoggingBuilder builder)
    {
        // add optional loggers
    }

    protected override void ConfigureServices(IServiceCollection services)
    {
        // configure general C# services
        services.AddHostedService<IDemoHostedService, DemoHostedService>();
    }

    protected override void ConfigureMonoBehaviours(IMonoBehaviourServiceCollectionBuilder services)
    {
        // configure MonoBehaviours
        
        // add referenced components via Inspector. The actual type neither constrained nor selectable, this way the first component will be picked,
        // so the component should be put at the top
        foreach (var monoBehaviour in persistentMonoBehavioursOnScene)
        {
            services.AddMonoBehaviourSingleton(monoBehaviour);
            DontDestroyOnLoad(monoBehaviour);

            Debug.Log($"Added persistent service {monoBehaviour.name} of type {monoBehaviour.GetType()} from the current scene");
        }
        
        // register MonoBehaviour types
        services.AddMonoBehaviourSingleton<CubesController>();
    }
}