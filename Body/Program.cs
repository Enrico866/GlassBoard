using GlassBoard.Abstractions.Config;
using GlassBoard.Abstractions.Provider;
using GlassBoard.Abstractions.Service;
using GlassBoard.Components;
using GlassBoard.Provider;
using GlassBoard.Services;

using MudBlazor.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();
builder.Services.AddHttpClient();
builder.Services.AddServerSideBlazor(options =>
{
    // CircuitOptions per Blazor (dove va DetailedErrors)
    options.DetailedErrors = true; 
});

builder.Services.AddMudServices();


builder.Services.Configure<ResourceApiOptions>(builder.Configuration.GetSection("ResourceApi"));
builder.Services.Configure<SchedulingApiOptions>(builder.Configuration.GetSection("ResourceApi"));
builder.Services.Configure<TenantApiOptions>(options => 
{
    var resSection = builder.Configuration.GetSection("ResourceApi");
    var adminSection = builder.Configuration.GetSection("Tenant:Admin");

    options.TenantUrl = resSection["TenantUrl"] ?? string.Empty;
    options.Channel = resSection["ChannelAdmin"] ?? "admin"; // Usiamo ChannelAdmin per i tenant
    options.AdminEmail = adminSection["User"] ?? string.Empty;
    options.AdminPassword = adminSection["Password"] ?? string.Empty;
});
builder.Services.Configure<ProbeApiOptions>(options => 
{
    var resSection = builder.Configuration.GetSection("ResourceApi");
    var adminSection = builder.Configuration.GetSection("Tenant:Admin");

    options.ProbesUrl = resSection["ProbesUrl"] ?? string.Empty;
    options.ChannelAdmin = resSection["ChannelAdmin"] ?? "admin";
    options.AdminEmail = adminSection["User"] ?? string.Empty;
    options.AdminPassword = adminSection["Password"] ?? string.Empty;
});
builder.Services.Configure<SecurityApiOptions>(builder.Configuration.GetSection("ResourceApi"));
builder.Services.Configure<ObservableApiOptions>(builder.Configuration.GetSection("ResourceApi"));
builder.Services.Configure<NamespaceApiOptions>(builder.Configuration.GetSection("ResourceApi"));
builder.Services.Configure<CollectorApiOptions>(options => 
{
    var config = builder.Configuration;
    options.CollectorsUrl = config["ResourceApi:CollectorsUrl"];
    options.ChannelAdmin = config["ResourceApi:ChannelAdmin"];
    options.AdminUser = config["Tenant:Admin:User"];
    options.AdminPassword = config["Tenant:Admin:Password"];
});
builder.Services.Configure<CheckApiOptions>(builder.Configuration.GetSection("ResourceApi"));
builder.Services.Configure<CheckPolicyApiOptions>(builder.Configuration.GetSection("ResourceApi"));
builder.Services.Configure<JobApiOptions>(options => {
    builder.Configuration.GetSection("ResourceApi").Bind(options);
    options.AdminUser = builder.Configuration["Tenant:Admin:User"] ?? "";
    options.AdminPassword = builder.Configuration["Tenant:Admin:Password"] ?? "";
});

builder.Services.AddScoped<INamespaceProvider, NamespaceProvider>();
builder.Services.AddScoped<IObservableProvider, ObservableProvider>();
builder.Services.AddScoped<ICheckPolicyProvider, CheckPolicyProvider>();
builder.Services.AddScoped<ICheckProvider, CheckProvider>();
builder.Services.AddScoped<IResourceProvider, ResourceProvider>();
builder.Services.AddScoped<ITenantProvider, TenantProvider>();
builder.Services.AddScoped<IProbeProvider, ProbeProvider>();
builder.Services.AddScoped<ICollectorProvider, CollectorProvider>();
builder.Services.AddScoped<ISchedulingProvider, SchedulingProvider>();
builder.Services.AddScoped<ISecurityProvider, SecurityProvider>();

builder.Services.AddScoped<AppContextService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IResourceService, ResourceService>();
builder.Services.AddScoped<ITenantService, TenantService>();
builder.Services.AddScoped<ISchedulingService, SchedulingService>();
builder.Services.AddScoped<IProbeService, ProbeService>();
builder.Services.AddScoped<ISecurityService, SecurityService>();
builder.Services.AddScoped<IObservableService, ObservableService>();
builder.Services.AddScoped<INamespaceService, NamespaceService>();
builder.Services.AddScoped<ICollectorService, CollectorService>();
builder.Services.AddScoped<ICheckService, CheckService>();
builder.Services.AddScoped<ICheckPolicyService, CheckPolicyService>();
builder.Services.AddScoped<IJobService, JobService>();


var app = builder.Build();

app.UseStaticFiles(); // wwwroot

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.MapControllers();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
