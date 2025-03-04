/*
 * A Startup class (there can be multiple ones in a module under different namespaces) will be called by the framework.
 * It's the same as the ASP.NET Startup class (https://docs.microsoft.com/en-us/aspnet/core/fundamentals/startup). In
 * there you can e.g. register injected services and change the request pipeline.
 */

using Fluid;
using Lombiq.TrainingDemo.Activities;
using Lombiq.TrainingDemo.Drivers;
using Lombiq.TrainingDemo.Events;
using Lombiq.TrainingDemo.Fields;
using Lombiq.TrainingDemo.Filters;
using Lombiq.TrainingDemo.Handlers;
using Lombiq.TrainingDemo.Indexes;
using Lombiq.TrainingDemo.Indexing;
using Lombiq.TrainingDemo.Liquid;
using Lombiq.TrainingDemo.Middlewares;
using Lombiq.TrainingDemo.Migrations;
using Lombiq.TrainingDemo.Models;
using Lombiq.TrainingDemo.Navigation;
using Lombiq.TrainingDemo.Permissions;
using Lombiq.TrainingDemo.Services;
using Lombiq.TrainingDemo.Settings;
using Lombiq.TrainingDemo.ViewModels;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using OrchardCore.BackgroundTasks;
using OrchardCore.ContentManagement;
using OrchardCore.ContentManagement.Display.ContentDisplay;
using OrchardCore.ContentTypes.Editors;
using OrchardCore.Data;
using OrchardCore.Data.Migration;
using OrchardCore.DisplayManagement;
using OrchardCore.DisplayManagement.Descriptors;
using OrchardCore.DisplayManagement.Handlers;
using OrchardCore.Environment.Shell;
using OrchardCore.Environment.Shell.Configuration;
using OrchardCore.Indexing;
using OrchardCore.Liquid;
using OrchardCore.Modules;
using OrchardCore.Navigation;
using OrchardCore.ResourceManagement;
using OrchardCore.Security.Permissions;
using OrchardCore.Users.Events;
using OrchardCore.Workflows.Helpers;
using System;
using System.IO;

namespace Lombiq.TrainingDemo;

// While the startup class doesn't need to derive from StartupBase and can just use conventionally named methods it's a
// bit less of a magic this way, and code analysis won't tell us to make it static.
public sealed class Startup : StartupBase
{
    private readonly IShellConfiguration _shellConfiguration;

    public Startup(IShellConfiguration shellConfiguration) => _shellConfiguration = shellConfiguration;

    public override void ConfigureServices(IServiceCollection services)
    {
        // NEXT STATION: Views/PersonPart.Edit.cshtml

        // Book
        services.AddDisplayDriver<Book, BookDisplayDriver>();
        services.AddScoped<IDisplayManager<Book>, DisplayManager<Book>>();
        services.AddDataMigration<BookMigrations>();
        services.AddIndexProvider<BookIndexProvider>();

        // Person Part
        services.AddContentPart<PersonPart>()
            .UseDisplayDriver<PersonPartDisplayDriver>()
            .AddHandler<PersonPartHandler>();
        services.AddDataMigration<PersonMigrations>();
        services.AddIndexProvider<PersonPartIndexProvider>();

        // Color Field
        services.AddContentField<ColorField>()
            .UseDisplayDriver<ColorFieldDisplayDriver>();
        services.AddScoped<IContentPartFieldDefinitionDisplayDriver, ColorFieldSettingsDriver>();
        services.AddScoped<IContentFieldIndexHandler, ColorFieldIndexHandler>();

        // Resources
        services.AddTransient<IConfigureOptions<ResourceManagementOptions>, ResourceManagementOptionsConfiguration>();

        // Permissions
        services.AddPermissionProvider<PersonPermissions>();

        // Admin Menu
        services.AddNavigationProvider<PersonsAdminMenu>();

        // Main Menu
        services.AddNavigationProvider<TrainingDemoNavigationProvider>();

        // Demo Settings
        services.Configure<DemoSettings>(_shellConfiguration.GetSection("Lombiq_TrainingDemo"));
        services.AddTransient<IConfigureOptions<DemoSettings>, DemoSettingsConfiguration>();
        services.AddSiteDisplayDriver<DemoSettingsDisplayDriver>();
        services.AddPermissionProvider<DemoSettingsPermissions>();
        services.AddNavigationProvider<DemoSettingsAdminMenu>();

        // Filters
        services.Configure<MvcOptions>((options) =>
        {
            options.Filters.Add(typeof(ShapeInjectionFilter));
            options.Filters.Add(typeof(ResourceInjectionFilter));
            options.Filters.Add(typeof(ResourceFromShapeInjectingFilter));
        });

        // Shape table provider
        services.AddScoped<IShapeTableProvider, ShapeHidingShapeTableProvider>();

        // File System
        services.AddSingleton<ICustomFileStore>(serviceProvider =>
        {
            // So our goal here is to have a custom folder in the tenant's own folder. The Media folder is also there
            // but we won't use that. To get tenant-specific data we need to use the ShellOptions and ShellSettings
            // objects.
            var shellOptions = serviceProvider.GetRequiredService<IOptions<ShellOptions>>().Value;
            var shellSettings = serviceProvider.GetRequiredService<ShellSettings>();

            // Necessary for the comment.
#pragma warning disable SA1114 // Parameter list should follow declaration
            var tenantFolderPath = PathExtensions.Combine(
                // This is the absolute path of the "App_Data" folder.
                shellOptions.ShellsApplicationDataPath,
                // This is the folder which contains the tenants which is Sites by default.
                shellOptions.ShellsContainerName,
                // This is the tenant name. We want our custom folder inside this folder.
                shellSettings.Name);
#pragma warning restore SA1114 // Parameter list should follow declaration

            // And finally our full base path.
            var customFolderPath = PathExtensions.Combine(tenantFolderPath, "CustomFiles");

            // Now register our CustomFileStore instance with the path given.
            return new CustomFileStore(customFolderPath);

            // NEXT STATION: Controllers/FileManagementController and find the CreateFileInCustomFolder method.
        });

        // Caching
        services.AddScoped<IDateTimeCachingService, DateTimeCachingService>();

        // Background tasks. Note that these have to be singletons.
        services.AddSingleton<IBackgroundTask, DemoBackgroundTask>();

        // Event handlers
        services.AddScoped<ILoginFormEvent, LoginGreeting>();

        // Workflows
        services.AddActivity<ManagePersonsPermissionCheckerTask, ManagePersonsPermissionCheckerTaskDisplayDriver>();

        // Liquid
        // To be able to access the properties inside these view models in display shapes rendered by the Liquid markup
        // engine you need to register them. To learn more about Liquid in Orchard Core see this documentation:
        // https://docs.orchardcore.net/en/latest/docs/reference/modules/Liquid/
        services.Configure<TemplateOptions>(options =>
            {
                options.MemberAccessStrategy.Register<PersonPartViewModel>();
                options.MemberAccessStrategy.Register<ColorField>();
                options.MemberAccessStrategy.Register<DisplayColorFieldViewModel>();
            })
            // You can create custom liquid filters with the following. You can check out Liquid/ShortDateFilter.cs and
            // come back here.
            .AddLiquidFilter<ShortDateFilter>("short_date");
    }
}

// A second Startup class, corresponding to our second feature (see Manifest.cs). Note how the Feature attribute tells
// Orchard to only activate the class if the feature is enabled. This way, you can register services corresponding to a
// feature only when necessary. Note that controllers aren't registered but activated automatically so you have to
// decorate them with the attribute too.
[Feature("Lombiq.TrainingDemo.Middlewares")]
public sealed class MiddlewaresStartup : StartupBase
{
    public override void Configure(IApplicationBuilder app, IEndpointRouteBuilder routes, IServiceProvider serviceProvider) =>
        // You can put service configuration here as you would do it in other ASP.NET Core applications. If you don't
        // need it you can skip overriding it. However, here we need it for our middleware.
        app.UseMiddleware<RequestLoggingMiddleware>();
}
