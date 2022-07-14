﻿using LastfmAPI.Exceptions;
using LastfmAPI.Responses;
using static System.String;
using static LastfmAPI.APIConnector;

namespace LastfmAPI.Tests.TrackResponses;

public class NoAlbumTrackTest
{
    private TrackResponse _response = null!;
    private const string Username = "mabdi36";
    private const string APIKey = "05467a3191853eb8da38dfb38ed3c733";

    [SetUp]
    public async Task Setup()
    {
        try
        {
            _response = (TrackResponse) await CallAPI(Username, APIKey, GetTracks);
        } catch (LastfmException)
        {
            Assert.Fail();
        } catch (HttpRequestException)
        {
            Assert.Fail();
        }
    }


    [Test]
    public void Name()
    {
        const string actualTrack = "Wicked (Confidence)";
        string? respondedTrackName = _response.Track?.Name;
        Assert.That(respondedTrackName, Is.EqualTo(actualTrack));
    }

    [Test]
    public void Artist()
    {
        const string actualArtist = "Rems";
        string? respondedTrackArtist = _response.Track?.Artist.Name;
        Assert.That(respondedTrackArtist, Is.EqualTo(actualArtist));
    }

    [Test]
    public void Album()
    {
        string? actualArtist = Empty;
        string? respondedTrackArtist = _response.Track?.Album.Name;
        Assert.That(actualArtist, Is.EqualTo(respondedTrackArtist));
    }

    [Test]
    public void NowPlaying()
    {
        const string actualNowPlaying = "true";
        string? respondedNowPlaying = _response.Track?.NowPlaying?.State;
        Assert.That(respondedNowPlaying, Is.EqualTo(actualNowPlaying));
    }

    [Test]
    public void Timestamp()
    {
        string? actualTimestamp = Empty;
        string? respondedTimestamp = _response.Track?.Date?.Timestamp;
        Assert.That(actualTimestamp, Is.EqualTo(respondedTimestamp));
    }

    [Test]
    public void Image()
    {
        string? actualImage = @"https://lastfm.freetls.fastly.net/i/u/174s/4128a6eb29f94943c9d206c08e625904.jpg";
        string? respondedImage = _response.Track?.ImageURL;
        Assert.That(actualImage, Is.EqualTo(respondedImage));
    }
}