using Microsoft.EntityFrameworkCore;
using WebApp;
using WebApplicationCropProduct;

var builder = WebApplication.CreateBuilder(args);

//подключение к бд
string connection = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationContext>(options => options.UseNpgsql(connection));

var app = builder.Build();
app.UseDefaultFiles();
app.UseStaticFiles();
//культуры
app.MapGet("/api/crops", async (ApplicationContext db) => await db.t_crop.ToListAsync());

app.MapGet("/api/crops/{id:int}", async (int id, ApplicationContext db) =>
{
    t_crops? crop = await db.t_crop.FirstOrDefaultAsync(u => u.id == id);
    if (crop == null) return Results.NotFound(new { message = "crops не найден" });
    return Results.Json(crop);
});

app.MapDelete("/api/crops/{id:int}", async (int id, ApplicationContext db) =>
{
    t_crops? crop = await db.t_crop.FirstOrDefaultAsync(u => u.id == id);
    if (crop == null) return Results.NotFound(new { message = "crop не найден" });
    db.t_crop.Remove(crop);
    await db.SaveChangesAsync();
    return Results.Json(crop);
});

app.MapPost("/api/crops", async (t_crops crop, ApplicationContext db) =>
{
    await db.t_crop.AddAsync(crop);
    await db.SaveChangesAsync();
    return crop;
});

app.MapPut("/api/crops", async (t_crops cropData, ApplicationContext db) =>
{
    var crop = await db.t_crop.FirstOrDefaultAsync(u => u.id == cropData.id);
    if (crop == null) return Results.NotFound(new { message = "crops не найден" });
    crop.crop_name = cropData.crop_name;
    await db.SaveChangesAsync();
    return Results.Json(crop);
});


//показатели почвы справочник   
app.MapGet("/api/soil_indicators", async (ApplicationContext db) => await db.t_soil_indicator_ref.ToListAsync());

app.MapGet("/api/soil_indicators/{id:int}", async (int id, ApplicationContext db) =>
{
    t_soil_indicators_ref? S_Ref = await db.t_soil_indicator_ref.FirstOrDefaultAsync(u => u.id == id);
    if (S_Ref == null) return Results.NotFound(new { message = "soil_indicators не найден" });
    return Results.Json(S_Ref);
});

app.MapDelete("/api/soil_indicators/{id:int}", async (int id, ApplicationContext db) =>
{
    t_soil_indicators_ref? S_Ref = await db.t_soil_indicator_ref.FirstOrDefaultAsync(u => u.id == id);
    if (S_Ref == null) return Results.NotFound(new { message = "soil_indicators не найден" });
    db.t_soil_indicator_ref.Remove(S_Ref);
    await db.SaveChangesAsync();
    return Results.Json(S_Ref);
});

app.MapPost("/api/soil_indicators", async (t_soil_indicators_ref S_Ref, ApplicationContext db) =>
{
    await db.t_soil_indicator_ref.AddAsync(S_Ref);
    await db.SaveChangesAsync();
    return S_Ref;
});

app.MapPut("/api/soil_indicators", async (t_soil_indicators_ref S_Ref_Data, ApplicationContext db) =>
{
    var S_Ref = await db.t_soil_indicator_ref.FirstOrDefaultAsync(u => u.id == S_Ref_Data.id);
    if (S_Ref == null) return Results.NotFound(new { message = "soil_indicators не найден" });
    S_Ref.indicator_name = S_Ref_Data.indicator_name;
    await db.SaveChangesAsync();
    return Results.Json(S_Ref);
});

app.Run();
