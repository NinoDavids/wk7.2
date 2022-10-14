using System.Net;
using System.Threading.Tasks;
using System.Collections.Generic;
using Planner.Models;
using Planner.Tests.Hooks;
using RestSharp;
using TechTalk.SpecFlow;
using Xunit;

namespace Planner.Tests.Steps;

[Binding]
public sealed class AttractieStepDefinitions
{
    private readonly RestClient _client;
    private readonly DatabaseData _databaseData;
    private RestResponse? response;

    public AttractieStepDefinitions(DatabaseData databaseData)
    {
        _databaseData = databaseData;
        _client = new RestClient("https://localhost:5002/");

        // Het HTTPS certificaat hoeft niet gevalideerd te worden, dus return true
        ServicePointManager.ServerCertificateValidationCallback +=
            (sender, cert, chain, sslPolicyErrors) => true;
    }

    [Given("attractie (.*) bestaat")]
    public async Task AttractieBestaat(string naam)
    {
        await _databaseData.Context.Attractie.AddAsync(new Attractie { Naam = naam });
        await _databaseData.Context.SaveChangesAsync();
    }

	[Given("attractie (.*) bestaatniet")]
    public async Task AttractieBestaatNiet(string naam)
    {
		return;
    }

    [When("attractie (.*) wordt opgezocht")]
    public async Task AttractieOpvragen(string naam)
    {
        var request = new RestRequest("api/Attracties/" + naam);
        response = await _client.ExecuteGetAsync(request);
    }

    [When("attractie (.*) wordt toegevoegd")]
    public async Task AttractieToevoegen(string naam)
    {
        var request = new RestRequest("api/Attracties").AddJsonBody(new { Naam = naam, Reserveringen = new List<string>() });
        response = await _client.ExecutePostAsync(request);
    }

	[When("attractie (.*) wordt verwijderd")]
    public async Task AttractieVerwijderd(string naam)
    {
        var request = new RestRequest("api/Attracties/delete/" + naam, Method.Delete);
        response = await _client.ExecuteAsync(request);
    }

    [Then("moet er een error (.*) komen")]
    public void Error(int httpCode)
    {
        Assert.Equal(httpCode, (int)response!.StatusCode);
    }


}

class AttractieToegevoegd
{
    public int Id { get; set; }
}
