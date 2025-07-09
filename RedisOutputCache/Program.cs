

var builder = WebApplication.CreateBuilder(args);

// add Redis Output Cache Middleware service
builder.Services.AddStackExchangeRedisOutputCache(options => {
    options.Configuration = builder.Configuration["RedisCacheConnection"];
});
builder.Services.AddOutputCache(options => {
    // optional: named output-cache profiles
});


var app = builder.Build();

app.MapGet("/", () => "Hello World!");

//Added for caching HTTP response of one end point
app.MapGet("/cached",()=> "Hello Redis Output Cache" + DateTime.Now).CacheOutput();

// use Redis Output Caching Middleware service
app.UseOutputCache();

app.Run();