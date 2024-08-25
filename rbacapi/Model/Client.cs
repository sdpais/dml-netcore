using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.OpenApi;

namespace rbacapi.Model
{
    public class Client
    {
        [JsonIgnore]
        public int Id { get; set; }
        public required string Name { get; set; }
        public bool IsActive{ get; set; }

        [JsonIgnore]
        public DateTime CreatedOn { get; set; }
        [JsonIgnore]
        public DateTime UpdatedOn { get; set; }
    }


public static class ClientEndpoints
{
	public static void MapClientEndpoints (this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/Client").WithTags(nameof(Client));

        group.MapGet("/", () =>
        {
            return new [] { new Client() };
        })
        .WithName("GetAllClients")
        .WithOpenApi();

        group.MapGet("/{id}", (int id) =>
        {
            //return new Client { ID = id };
        })
        .WithName("GetClientById")
        .WithOpenApi();

        group.MapPut("/{id}", (int id, Client input) =>
        {
            return TypedResults.NoContent();
        })
        .WithName("UpdateClient")
        .WithOpenApi();

        group.MapPost("/", (Client model) =>
        {
            //return TypedResults.Created($"/api/Clients/{model.ID}", model);
        })
        .WithName("CreateClient")
        .WithOpenApi();

        group.MapDelete("/{id}", (int id) =>
        {
            //return TypedResults.Ok(new Client { ID = id });
        })
        .WithName("DeleteClient")
        .WithOpenApi();
    }
}}
