﻿using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using RestSharp;
using static LastfmDiscordRPC.Models.LastfmException;

namespace LastfmDiscordRPC.Models;

public static class LastfmClient
{
    private readonly static RestClient Client;
    private static RestRequest? _request;

    static LastfmClient()
    {
        Client = new RestClient(@"https://ws.audioscrobbler.com/2.0/");
        Client.AddDefaultHeader("User-Agent", "LastfmDiscordRPC 1.0.0");
    }

    public static async Task<LastfmResponse?> CallAPI(string username, string apiKey)
    {
        _request = new RestRequest();

        _request.AddParameter("format", "json");
        _request.AddParameter("method", "user.getrecenttracks");
        _request.AddParameter("limit", "1");
        _request.AddParameter("user", username);
        _request.AddParameter("api_key", apiKey);
        _request.Timeout = 5;

        RestResponse response = await Client.ExecuteAsync(_request);
        _request = null;
        
        if (response.Content != null) return GetResponse(response.Content);
        throw new HttpRequestException(Enum.GetName(response.StatusCode), null, response.StatusCode);
    }

    private static LastfmResponse GetResponse(string response)
    {
        LastfmError e = JsonConvert.DeserializeObject<LastfmError>(response)!;
        if (e.Error == ErrorEnum.OK) return JsonConvert.DeserializeObject<LastfmResponse>(response)!;
        throw new LastfmException(e.Message, e.Error);
    }
    
    public static void Dispose() => Client.Dispose();
}