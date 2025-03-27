using System.Configuration;
using System.Windows;
using Microsoft.EntityFrameworkCore;
using NotasAPI.Data;
using NotasAPI.Models;

var builder = WebApplication.CreateBuilder(args);

var connectionString = "Server=localhost;Port=3308;Database=umfg_venda_api;Uid=root;Pwd=root;";

builder.Services.AddDbContext<AppDbContext>(options => options.UseMySQL(connectionString));

// Adi��o de servi�os e configura��o do pipeline HTTP
var app = builder.Build();

app.MapGet("/nota", async (AppDbContext context) => await context.Notas.ToListAsync());

app.MapGet("/nota/{id}", async (Guid id, AppDbContext context) => {
var nota = await context.Notas.FindAsync(id);
return nota != null ? Results.Ok(nota) : Results.NotFound("Nota n�o encontrada.");
});

app.MapPost("/nota", async (Nota nota, AppDbContext context) =>
{
if (nota.Valor < 0 || nota.Valor > 10)
{
return Results.BadRequest("O valor da nota deve estar entre 0 e 10.");
}

context.Notas.Add(nota);
await context.SaveChangesAsync();
return Results.Created($"/notas/{nota.Id}", nota);
});

app.MapPut("/nota/{id}", async (Guid id, Nota notaAtualizada, AppDbContext context) => {
var nota = await context.Notas.FindAsync(id);
if (nota == null)
{
return Results.NotFound("Nota n�o encontrada.");
}

nota.Aluno = notaAtualizada.Aluno;
nota.Disciplina = notaAtualizada.Disciplina;
nota.Valor = notaAtualizada.Valor;

await context.SaveChangesAsync();
return Results.Ok(nota);
});

app.MapDelete("/nota/{id}", async (Guid id, AppDbContext context) =>
{
    var nota = await context.Notas.FindAsync(id);
    if (nota == null)
    {
        return Results.NotFound("Nota n�o encontrada.");
    }

    context.Notas.Remove(nota);
    await context.SaveChangesAsync();
    return Results.NoContent();

});

app.UseHttpsRedirection();
app.Run();
