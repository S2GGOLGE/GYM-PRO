using Azure.Core;
using Microsoft.AspNetCore.Mvc;
using SeneOdev;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseCors("AllowAll");

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
//LOGİN
app.MapPost("/login", ([FromBody] LoginRequest request) =>
{
    Console.WriteLine("LOGIN İSTEĞİ GELDİ");
    Console.WriteLine($"Username: {request.Username}");
    Console.WriteLine($"Password: {request.Password}");

    string sonuc = Login.GirisYap(request.Username, request.Password);

    if (sonuc == "OK")
        return Results.Ok(new { success = true, message = "Giriş başarılı" });

    return Results.BadRequest(new { success = false, message = sonuc });
});
//SİNG UP
app.MapPost("/sign_up", ([FromBody] SignupRequest request) =>
{
    Console.WriteLine("SIGNUP İSTEĞİ GELDİ");
    Console.WriteLine($"Name: {request.Name}");
    Console.WriteLine($"Surname: {request.Surname}");
    Console.WriteLine($"Username: {request.Username}");
    Console.WriteLine($"Email: {request.Email}");
    Console.WriteLine($"Phone: {request.Phone}");
    Console.WriteLine($"Gender: {request.Gender}");
    Console.WriteLine($"Password: {request.Password}");
    Console.WriteLine($"Password Repeat: {request.PasswordRepeat}");

    var user = new KayitOl
    {
        Name = request.Name,
        Surname = request.Surname,
        Username = request.Username,
        Email = request.Email,
        Phone = request.Phone,
        Gender = request.Gender,
        Password = request.Password,
        PasswordRepeat = request.PasswordRepeat
    };

    bool sonuc = user.Kayit();

    if (sonuc)
        return Results.Ok(new { success = true, message = "Kayıt başarılı" });

    return Results.BadRequest(new { success = false, message = "Kayıt başarısız" });
});
//ADMİN LOGİN
app.MapPost("/adminlogin", ([FromBody] AdminLoginRequest request) =>
{
    Console.WriteLine("ADMİN İSTEĞİ GELDİ");
    Console.WriteLine($"Username: {request.Username}");
    Console.WriteLine($"Password: {request.Password}");

    string sonuc = admin.Login(request.Username, request.Password, request.Role);

    if (sonuc == "OK")
        return Results.Ok(new { success = true, message = "Giriş başarılı" });

    return Results.BadRequest(new { success = false, message = sonuc });
});
app.MapGet("/sunucu", ([FromBody] AdminLoginRequest request) =>
{
    SUNUCU.Client("127.0.0.1", 8587);
});

app.Run();
// DTO
public record LoginRequest(string Username, string Password);
public record SignupRequest(
    string Name,
    string Surname,
    string Username,
    string Email,
    string Phone,
    string Gender,
    string Password,
    string PasswordRepeat
);

public record AdminLoginRequest(
    string Username,
    string Password,
    string Role
); 