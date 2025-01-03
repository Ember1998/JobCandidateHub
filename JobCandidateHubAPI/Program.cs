using JobCandidateHubAPI;
using JobCandidateHubAPI.DataAccess;
using JobCandidateHubAPI.Repository;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<CandidateDbContext>(opt => opt.UseSqlServer(builder.Configuration.GetConnectionString("CandidateDb")));
builder.Services.AddScoped<ICandidateRepository, CandidateRepository>();

var app = builder.Build();

app.MapPost("/api/candidates", (Candidate candidate, ICandidateRepository repository) => {
    var existingCandidate = repository.ExistsByEmail(candidate.Email);
    if (candidate.Id > 0 || existingCandidate)
    {
        repository.Update(candidate);
        return Results.Ok(candidate);
    }
    else
    {
        repository.Add(candidate);
        return Results.Created($"/api/candidates/{candidate.Id}", candidate);
    }
});
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
