using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using SeneOdev;
using System.Net.Sockets;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// PORTU ZORUNLU 7074 YAP
builder.WebHost.UseUrls("http://localhost:7074");

// --- 1. JSON YAPILANDIRMASI (Kritik!) ---
builder.Services.Configure<Microsoft.AspNetCore.Http.Json.JsonOptions>(options =>
{
    options.SerializerOptions.PropertyNameCaseInsensitive = true;
});

// CORS politikası
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

// 🔥 JWT DOĞRULAMA
var jwtKey = builder.Configuration["Jwt:Key"]
    ?? throw new Exception("JWT Key bulunamadı! appsettings.json kontrol et.");
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey))
        };
    });

builder.Services.AddAuthorization();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseCors("AllowAll");

// 🔥 AUTHENTICATION & AUTHORIZATION
app.UseAuthentication();
app.UseAuthorization();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// --- ENDPOINT'LER ---
// LOGIN
app.MapPost("/login", ([FromBody] LoginRequest request) =>
{
    Console.WriteLine($"LOGIN İSTEĞİ: {request.Username}");
    string sonuc = Login.GirisYap(request.Username, request.Password);

    if (sonuc == "OK")
    {
        var token = TokenService.GenerateToken(request.Username);
        return Results.Ok(new { success = true, message = "Giriş başarılı", token });
    }

    return Results.BadRequest(new { success = false, message = sonuc });
});

// SIGNUP
app.MapPost("/sign_up", ([FromBody] SignupRequest request) =>
{
    Console.WriteLine("SIGNUP ÇALIŞTI");

    if (request == null)
        return Results.BadRequest(new { success = false, message = "Veri gelmedi" });

    var user = new KayitOl
    {
        Name = request.Name,
        Surname = request.Surname,
        Username = request.Username,
        Email = request.Email,
        Phone = request.Phone,
        Gender = request.Gender,
        Sozlesme = request.Sozlesme,
        Password = request.Password,
        PasswordRepeat = request.PasswordRepeat
    };

    bool sonuc = user.Kayit();

    if (sonuc)
        return Results.Ok(new { success = true, message = "Kayıt başarılı" });

    return Results.BadRequest(new
    {
        success = false,
        message = "Kayıt başarısız (şifreler uyuşmuyor, kullanıcı var veya sözleşme kabul edilmedi)"
    });
});

// ADMIN LOGIN
app.MapPost("/adminlogin", ([FromBody] AdminLoginRequest request) =>
{
    Console.WriteLine($"ADMIN LOGIN: {request.Username}");
    string sonuc = admin.Login(request.Username, request.Password, request.Role);

    if (sonuc == "OK")
        return Results.Ok(new { success = true, message = $"Giriş başarılı. Hoş geldin {request.Username}" });

    return Results.BadRequest(new { success = false, message = sonuc });
});

// ŞİFRE GÜNCELLEME
app.MapPost("/updatepass", ([FromBody] PassUpdateRequest request) =>
{
    if (request == null) return Results.BadRequest(new { success = false, message = "Veri gelmedi." });

    Console.WriteLine($"ŞİFRE YENİLEME: {request.Username}");

    var model = new PassUpdate
    {
        Username = request.Username,
        NewPass = request.NewPass,
        NewPassRepeat = request.NewPassRepeat
    };

    var (success, message) = model.Update();

    return success
        ? Results.Ok(new { success = true, message = message })
        : Results.BadRequest(new { success = false, message = message });
});

// ─────────────────────────────────────────
// HOME SERVER BAĞLANTISI
// ─────────────────────────────────────────
try
{
    var homeClient = new TcpClient();
    await homeClient.ConnectAsync("192.168.1.115", 8587);

    string tanitim = "GYM PRO";
    byte[] data = Encoding.UTF8.GetBytes(tanitim);
    NetworkStream stream = homeClient.GetStream();
    await stream.WriteAsync(data, 0, data.Length);

    Console.WriteLine("GYM-PRO SUNUCUYA BAĞLANDI");
}
catch (Exception ex)
{
    Console.WriteLine($"GYM-PRO BAĞLANTI HATASI: {ex.Message}");
}

app.Run();

// --- DTO TANIMLARI ---
public record LoginRequest(string Username, string Password);

public record SignupRequest(
    string Name,
    string Surname,
    string Username,
    string Email,
    string Phone,
    string Gender,
    bool Sozlesme,
    string Password,
    string PasswordRepeat
);

public record AdminLoginRequest(string Username, string Password, string Role);

public record PassUpdateRequest(
    string Username,
    string NewPass,
    string NewPassRepeat
);